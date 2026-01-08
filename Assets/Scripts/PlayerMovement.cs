using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb => this.GetComponent<Rigidbody2D>();

    float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] public float jumpingPower = 16f;
    public bool isFacingRight = true;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float coyoteTimer;
    private float jumpBufferTimer;
    [SerializeField] private float fallClamp;
    [SerializeField] private float apexBonus;
    [SerializeField] private float wallJumpOutForce;
    [SerializeField] private float wallJumpUpForce;
    [SerializeField] private float wallSlideSpeed = 2f;
    [SerializeField] private float wallJumpDirection = 1;

    //public Animator anim;

    private bool isJumping;
    private bool isWallJumping;
    private float wallJumpDeadTime;

    public bool wallJumpAllowed = false;

    private float lastVerticalVelocity;

    private void Start()
    {
        coyoteTimer = 0f;
        jumpBufferTimer = 0f;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            jumpBufferTimer = 0.1f;
        else if (jumpBufferTimer > 0f)
            jumpBufferTimer -= Time.deltaTime;

        if (IsGrounded())
            coyoteTimer = 0.2f;
        else
            coyoteTimer -= Time.deltaTime;

        if (coyoteTimer > 0f && jumpBufferTimer > 0f && !isJumping && !isWallJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferTimer = 0f;
            StartCoroutine(JumpCooldown());
        }

        if (rb.velocity.y > 0f && Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimer = 0f;
        }

        if (rb.velocity.y < fallClamp) rb.velocity = new Vector2(rb.velocity.x, fallClamp);

        if (lastVerticalVelocity > 0 && rb.velocity.y <= 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - apexBonus);
        lastVerticalVelocity = rb.velocity.y;

        /*
        if (rb.velocity.x > 0 || rb.velocity.x < 0) anim.SetBool("Walking", true);
        else anim.SetBool("Walking", false);
        anim.SetBool("Grounded", IsGrounded());
        if (rb.velocity.y > 0) anim.SetBool("Ascent", true);
        else anim.SetBool("Ascent", false);
        */

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) Flip();


        if (IsTouchingWall() && !IsGrounded() && horizontal != 0)
        {
            wallJumpDeadTime = 0f;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
            if (wallJumpDeadTime < 0.2f) wallJumpDeadTime += Time.deltaTime;

        if (Input.GetButtonDown("Jump") && wallJumpDeadTime < 0.1f)
        {
            if (!wallJumpAllowed) return;
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpOutForce, wallJumpUpForce);
            StartCoroutine(WallJumpCooldown());
        }

        if (IsTouchingWall(Vector2.left)) wallJumpDirection = 1;
        else if (IsTouchingWall(Vector2.right)) wallJumpDirection = -1;
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsTouchingWall()
    {
        RaycastHit2D lWallHit = Physics2D.Raycast(transform.position, Vector2.left, 0.7f, groundLayer);
        RaycastHit2D rWallHit = Physics2D.Raycast(transform.position, Vector2.right, 0.7f, groundLayer);
        return lWallHit.collider != null || rWallHit.collider != null;
    }
    private bool IsTouchingWall(Vector2 direction)
    {
        RaycastHit2D wallHit = Physics2D.Raycast(transform.position, direction, 0.7f, groundLayer);

        return wallHit.collider != null;
    }

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private IEnumerator WallJumpCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        isWallJumping = false;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
