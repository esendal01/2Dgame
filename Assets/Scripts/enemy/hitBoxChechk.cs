using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class hitBoxChechk: MonoBehaviour
{
    private EnemyBehaviour enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyBehaviour>();
        
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemyParent.hitBox = collider.transform;
            enemyParent.inRange = true;
            DealDamage(collider.gameObject);
        }
    }

    private void DealDamage(GameObject player)
    {
        // Oyuncuya hasar verme kodu buraya gelecek
         player.GetComponent<Playermanager>().Getdamage(enemyParent.damage);
    }
}
