using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFunctions : MonoBehaviour
{
    public float damageCalculator(float  damage, float defence, float ap)
    {
        float x;

        x = Mathf.Abs(damage - Mathf.Clamp((defence - ap), 0, Mathf.Infinity));

        return x;
    }
}
