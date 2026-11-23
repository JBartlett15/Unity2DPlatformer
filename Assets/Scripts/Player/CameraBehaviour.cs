using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    void LateUpdate()
    {
            transform.position = Vector3.SmoothDamp(transform.position, player.position + offset, ref velocity, 0.1f);
    }
}
