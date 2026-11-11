using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ScoreManager>().ScoreChange(100);
            Destroy(gameObject);        
        }
    }
}
