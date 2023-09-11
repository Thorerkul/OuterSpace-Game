using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDController : MonoBehaviour
{
    public GameObject minimap;
    public bool canShowMinimap;
    public bool isShowingMinimap;

    private void Update()
    {
        if (Input.GetAxisRaw("MiniMap") > 0.1f)
        {
            if (canShowMinimap)
            {
                isShowingMinimap = true;
            }
        } else
        {
            isShowingMinimap = false;
        }

        if (isShowingMinimap)
        {
            minimap.SetActive(true);
        } else
        {
            minimap.SetActive(false);
        }
    }
}
