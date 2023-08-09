using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsMenuScript : MonoBehaviour
{
    public Button soundMenuButton;

    private void OnEnable()
    {
        soundMenuButton.Select();
    }
}
