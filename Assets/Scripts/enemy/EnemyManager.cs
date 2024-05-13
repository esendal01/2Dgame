using System.Collections;
using EthanTheHero;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float health;
    bool dead = false;
    Animator playerAnimation;
    private EnemyBehaviour enemyParent;

    private Collider2D enemyCollider;
    private Collider2D playerCollider;
    private bool ignoreCollision = false;

    private void Awake()
    {
        enemyParent = GetComponentInParent<EnemyBehaviour>();
        enemyCollider = GetComponent<Collider2D>();
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    void Start()
    {
        playerAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        AmIdead();
    }

    public void TakeDamage(float damage)
    {
        if (health - damage > 0)
        {
            health -= damage;
            playerAnimation.SetTrigger("isHurt");
        }
        else
        {
            health = 0;
        }
        AmIdead();
    }

    void AmIdead()
    {
        if (health <= 0)
        {
            dead = true;
            playerAnimation.SetTrigger("isDead");
            playerAnimation.SetBool("isWalk", false);
            playerAnimation.SetBool("isAttack", false);
            playerAnimation.SetBool("isBite", false);
            enemyParent.moveSpeed = 0;

            // Çarpışmayı devre dışı bırak ve belirli bir süre sonra tekrar etkinleştir
            StartCoroutine(DisableCollisionTemporarily());

            Destroy(gameObject, 3);
        }
    }

    IEnumerator DisableCollisionTemporarily()
    {
        Physics2D.IgnoreCollision(enemyCollider, playerCollider, true);
        ignoreCollision = true;

        yield return new WaitForSeconds(3); // 3 saniye boyunca çarpışma devre dışı bırakılır

        Physics2D.IgnoreCollision(enemyCollider, playerCollider, false);
        ignoreCollision = false;
    }
}