using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBaseScript : MonoBehaviour
{
    public Button defaultButton;

    private void OnEnable()
    {
        defaultButton.Select();
    }
}
