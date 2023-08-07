using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;


    public Slider healthSlider;
    public Image healthfill;
    public Sprite healthNormal;
    public Sprite healthDamaged;

    public float stamina;
    public float maxStamina;

    public Slider staminaSlider;
    public Image staminafill;
    public Sprite staminaNormal;
    public Sprite staminaDamaged;

    public Image Head;
    public Sprite HeadNormal;
    public Sprite HeadDamaged;
    public bool isDamaged;

    private void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthSlider.value = health / maxHealth;
        staminaSlider.value = stamina / maxStamina;

        if (isDamaged)
        {
            Head.sprite = HeadDamaged;
            healthfill.sprite = healthDamaged;
            staminafill.sprite = staminaDamaged;

        } else
        {
            Head.sprite = HeadNormal;
            healthfill.sprite = healthNormal;
            staminafill.sprite = staminaNormal;
        }
    }
}
