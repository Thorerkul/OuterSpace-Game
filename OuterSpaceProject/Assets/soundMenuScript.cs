using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soundMenuScript : MonoBehaviour
{
    public Slider masterVolumeSlider;

    private void OnEnable()
    {
        masterVolumeSlider.Select();
    }
}
