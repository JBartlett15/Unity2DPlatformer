using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStatBounds : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject cam;
    bool inside;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject == player) return;
        player.GetComponent<PlayerMovement>().jumpingPower = 9;
        cam.GetComponent<Camera>().orthographicSize = 5;
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject == player) return;

        player.GetComponent<PlayerMovement>().jumpingPower = 11;
        cam.GetComponent<Camera>().orthographicSize = 7.5f;
    }
}
