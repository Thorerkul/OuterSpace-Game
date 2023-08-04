using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerpos;
    public Vector3 offset;

    private void Update()
    {
        transform.position = new Vector3(playerpos.position.x + offset.x, playerpos.position.y + offset.y, playerpos.position.z + offset.z);
    }
}
