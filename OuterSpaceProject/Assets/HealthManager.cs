using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider healthSlider;
    public Image fill;
    public Sprite healthNormal;
    public Sprite healthDamaged;

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

        if (isDamaged)
        {
            Head.sprite = HeadDamaged;
            fill.sprite = healthDamaged;

        } else
        {
            Head.sprite = HeadNormal;
            fill.sprite = healthNormal;
        }
    }
}
