using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFunctions : MonoBehaviour
{
    private float timer = 1;
    private float time = 1;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0 )
        {
            timer = time;

            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }
    }

    public float damageCalculator(float  damage, float defence, float ap)
    {
        float x;

        x = Mathf.Abs(damage - Mathf.Clamp((defence - ap), 0, Mathf.Infinity));

        return x + 1;
    }
}
