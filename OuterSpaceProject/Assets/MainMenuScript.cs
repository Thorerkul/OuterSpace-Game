using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public Button playButton;

    private void OnEnable()
    {
        playButton.Select();
    }
}
