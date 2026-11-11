using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb => this.GetComponent<Rigidbody2D>();

    float horizontal;
    [SerializeField] private float startingSpeed = 7f;
    [SerializeField] private float currentSpeed = 7f;
    [SerializeField] private float maxSpeed = 9f;
    [SerializeField] private float acceleration = 1f;
    [SerializeField] private float jumpingPower = 16f;
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

    public Animator anim;

    private bool isWalking;
    private bool isJumping;
    private bool isWallJumping;
    private float wallJumpDeadTime;

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
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
            jumpBufferTimer = 0f;
            StartCoroutine(JumpCooldown());
        }

        if (rb.linearVelocity.y > 0f && Input.GetButtonUp("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
            coyoteTimer = 0f;
        }

        if (rb.linearVelocity.y < fallClamp) rb.linearVelocity = new Vector2(rb.linearVelocity.x, fallClamp);

        if (lastVerticalVelocity > 0 && rb.linearVelocity.y <= 0)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y - apexBonus);
        lastVerticalVelocity = rb.linearVelocity.y;

        if (rb.linearVelocity.x > 0 || rb.linearVelocity.x < 0)
        {
            anim.SetBool("Walking", true);
            isWalking = true;
        }
        else
        {
            anim.SetBool("Walking", false);
            isWalking = false;
        }
        anim.SetBool("Grounded", IsGrounded());
        if (rb.linearVelocity.y > 0) anim.SetBool("Ascent", true);
        else anim.SetBool("Ascent", false);

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) Flip();


        if (IsTouchingWall() && !IsGrounded() && horizontal != 0)
        {
            wallJumpDeadTime = 0f;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
            if (wallJumpDeadTime < 0.2f) wallJumpDeadTime += Time.deltaTime;

        if (Input.GetButtonDown("Jump") && wallJumpDeadTime < 0.1f)
        {
            currentSpeed = maxSpeed;
            isWallJumping = true;
            rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpOutForce, wallJumpUpForce);
            StartCoroutine(WallJumpCooldown());
        }

        if (IsTouchingWall(Vector2.left)) wallJumpDirection = 1;
        else if (IsTouchingWall(Vector2.right)) wallJumpDirection = -1;
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontal * currentSpeed, rb.linearVelocity.y);
            if (isWalking)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (currentSpeed < maxSpeed) currentSpeed += Time.deltaTime * acceleration;
                }
                else if (currentSpeed > startingSpeed) currentSpeed -= Time.deltaTime * acceleration;
            }
            else
            {
                currentSpeed = startingSpeed;
            }
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
