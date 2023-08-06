using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider healthSlider;
    public Image leftImage;
    public Sprite leftFilled;
    public Sprite leftEmpty;
    public Image rightImage;
    public Sprite rightFilled;
    public Sprite rightEmpty;

    private void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthSlider.value = health / maxHealth;
        if (healthSlider.value <= 0)
        {
            leftImage.sprite = leftEmpty;
        } else
        {
            leftImage.sprite = leftFilled;
        }

        if (healthSlider.value >= 1)
        {
            rightImage.sprite = rightFilled;
        } else
        {
            rightImage.sprite = rightEmpty;
        }
    }
}
