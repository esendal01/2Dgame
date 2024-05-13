using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


public class EnemyBehaviour : MonoBehaviour
{
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject HotZone;
    public GameObject triggerArea;
    public Transform hitBox;
    public float damage;
    
    
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool cooling;
    private float intTimer;
    private EnemyManager enemyParent;
    public Transform leftLimit;
    public Transform rightLimit;
    //[SerializeField] private AudioSource Sound;
     void Awake()
     {
         intTimer = timer;
         SelectTarget();
         anim = GetComponent<Animator>();
         enemyParent = GetComponentInParent<EnemyManager>();
         //Sound = GetComponent<AudioSource>();
     }
   


    // Update is called once per frame
    void Update()
    {
    
        if (!attackMode)
        {
            Move();
        }

        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAnimAttack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }
        
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position ,target.position);
        if (distance > attackDistance)
        {
            StopAttack();
        }
        if (attackDistance >= distance && cooling == false)
        {
            AttackAndBite();
        }
        if (cooling)
        {
            Cooldown();
            anim.SetBool("isAttack", false);
            anim.SetBool("isBite", false);
        }
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("isAttack", false);
        anim.SetBool("isBite",false);
        
    }

    void AttackAndBite()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("isWalk", false);
        //anim.SetBool("isAttack", false);
        anim.SetBool("isBite", false);
        anim.SetBool("isAttack", false);
        // Rastgele olarak hangi animasyonu oynatacağınızı belirleyin
        int isAttack = Random.Range(0, 100); 
        

        if (isAttack % 2 == 0)
        {
            // Attack
            anim.SetBool("isAttack", true);
            anim.SetBool("isBite", false);
           
        }
        else
        {
            // Bite
            anim.SetBool("isBite", true);
            anim.SetBool("isAttack", false);
            
        }
    }

    void Move()
    {
        if (enemyParent.health > 0)
        {
            anim.SetBool("isWalk",true);
            // if (!Sound.isPlaying)
            // {
            //     Sound.Play();
            // }
        }
        anim.SetBool("isAttack",false);
        anim.SetBool("isBite", false);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("EnemyAnimAttack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);
        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }
        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    public void TriggerCooling()
    {
        cooling = true;
    }
    
    
}


