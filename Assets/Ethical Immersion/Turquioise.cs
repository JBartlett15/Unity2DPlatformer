using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turquioise : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 5f;
    public float moveSpeed = 2f;
    public Vector2 minBoundary;
    public Vector2 maxBoundary;
    public float smoothTime = 0.2f;

    private Vector2 velocity = Vector2.zero;

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            Vector2 directionAway = (transform.position - player.position).normalized;

            Vector2 targetPosition = (Vector2)transform.position + directionAway * moveSpeed * Time.deltaTime;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minBoundary.x, maxBoundary.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBoundary.y, maxBoundary.y);

            transform.position = Vector2.Lerp(transform.position, targetPosition, smoothTime);
        }
    }
}
