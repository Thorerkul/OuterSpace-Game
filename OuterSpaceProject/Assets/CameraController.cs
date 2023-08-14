using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PixelizeFeature;

public class CameraController : MonoBehaviour
{
    public Transform playerpos;
    public Vector3 offset;
    public Camera cam;
    public float pixelamount;
    public float camzoomSpeed;
    public float camZoomMaxSize;
    public PixelizeFeature pixelf;

    private void Start()
    {
        transform.position =  new Vector3(playerpos.position.x + offset.x, playerpos.position.y + offset.y, playerpos.position.z + offset.z);
    }

    private void Update()
    {
        float vertical = Input.GetAxisRaw("DpadVertical");
        cam.orthographicSize += vertical * camzoomSpeed * Time.deltaTime;
        if (cam.orthographicSize < 1)
        {
            cam.orthographicSize = 1;
        }
        if (cam.orthographicSize > camZoomMaxSize)
        {
            cam.orthographicSize = camZoomMaxSize;
        }

        //pixelf.screenHeight = (int)(144 / cam.orthographicSize);
        pixelf.settings.screenHeight = (int)(cam.orthographicSize * (pixelamount / 10) * 2);
        //Debug.Log((int)(cam.orthographicSize * 144));

        transform.position = new Vector3(playerpos.position.x + offset.x, playerpos.position.y + offset.y, playerpos.position.z + offset.z);
    }
}
