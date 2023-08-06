using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerpos;
    public Vector3 offset;
    public Camera cam;
    public float camzoomSpeed;

    private void Update()
    {
        float vertical = Input.GetAxisRaw("DpadVertical");
        cam.orthographicSize += vertical * camzoomSpeed * Time.deltaTime;
        if (cam.orthographicSize < 1)
        {
            cam.orthographicSize = 1;
        }

        transform.position = new Vector3(playerpos.position.x + offset.x, playerpos.position.y + offset.y, playerpos.position.z + offset.z);
    }
}
