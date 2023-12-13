using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TryGetComponent<DamageabaleEntity>(out DamageabaleEntity damageabaleEntity))
            damageabaleEntity.ApplyDamage(5f);
    }
}
