using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerAreaCheck : MonoBehaviour
{
    private EnemyBehaviour enemyParent;
    private EnemyManager _enemyManager;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyBehaviour>();
        _enemyManager = GetComponentInParent<EnemyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.HotZone.SetActive(true);
        }

        if (_enemyManager.health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
}

    
