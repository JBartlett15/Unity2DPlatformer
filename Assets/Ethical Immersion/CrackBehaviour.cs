using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackBehaviour : MonoBehaviour
{
    int crackCount = 0;

    [SerializeField] SpriteRenderer render;
    [SerializeField] Sprite[] sprites;

    void Start()
    {
        crackCount = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Crack"))
        {
            collision.gameObject.SetActive(false);
            GetCracked();
        }
    }

    void GetCracked()
    {
        crackCount++;
        render.sprite = sprites[crackCount];
    }
}
