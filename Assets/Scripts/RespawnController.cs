using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class RespawnController : MonoBehaviour
{
    Vector2 startPos;
    Rigidbody2D rb => this.GetComponent<Rigidbody2D>();

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("DeathPlane")) StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.5f);
        transform.position = startPos;
        transform.localScale = new Vector3(1, 1, 1);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<PlayerMovement>().isFacingRight = true;
    }

}
