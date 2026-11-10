using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemy : MonoBehaviour
{
    Rigidbody2D rb => this.GetComponent<Rigidbody2D>();
    public bool isFacingRight;
    Vector2 direction;
    [SerializeField] float speed;

    Vector2 startPos;
    private void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (isFacingRight)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            direction = Vector2.right;
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            direction = Vector2.left;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f);
        Debug.DrawRay(transform.position, direction, Color.green);
        if(hit.collider != null) if (hit.collider.CompareTag("Ground")) isFacingRight = !isFacingRight;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = startPos;
        isFacingRight = false;
    }
}
