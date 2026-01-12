using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStatBounds : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject cam;

    [SerializeField] float insideJump;
    [SerializeField] float insideSize;

    [SerializeField] bool wallJump = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != player) return; 
        player.GetComponent<PlayerMovement>().jumpingPower = insideJump;
        cam.GetComponent<Camera>().orthographicSize = insideSize;
        gameObject.SetActive(false);

        if (wallJump) player.GetComponent<PlayerMovement>().wallJumpAllowed = true;
    }


}
