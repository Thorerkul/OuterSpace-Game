using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class WorldScript : MonoBehaviour
{
    [Header("World Rotation")]
    public World world = new World();
    public Vector3 rotation;

    [Header("Pixel Shader")]
    public Vector2 screenSize;
    public RenderTexture rt;
    public PixelPerfectCamera cam;

    private void Start()
    {
        world.Rotation = rotation;
        transform.Rotate(rotation, Space.World);

        rt.width = (int)screenSize.x;
        rt.height = (int)screenSize.y;

        cam.refResolutionX = (int)rt.width;
        cam.refResolutionY = (int)rt.height;
    }

    public Vector3 ReturnNewCoordinates(Vector3 originalCoordinates, float angleInDegrees)
    {
        // Convert angle from degrees to radians
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;

        // Calculate the sine and cosine of the angle
        float cosAngle = Mathf.Cos(angleInRadians);
        float sinAngle = Mathf.Sin(angleInRadians);

        // Rotate the coordinates around the world origin (Z-axis rotation)
        float x = originalCoordinates.x * cosAngle - originalCoordinates.y * sinAngle;
        float y = originalCoordinates.x * sinAngle + originalCoordinates.y * cosAngle;

        // Create a new Vector3 with the rotated coordinates and return it
        Vector3 rotatedCoordinates = new Vector3(x, y, originalCoordinates.z);
        return rotatedCoordinates;
    }
}
