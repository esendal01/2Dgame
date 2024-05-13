using EthanTheHero;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerConroller : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidPlayer;
    Animator PlayerAnimation;
  
    public float hiz = 1f;
    bool facingRight = true;

    public float JumpSpeed = 1f,  JumpTime;
    public float JumpFrekans = 1f;
    public bool isGronded = false;
    public float attackdamage=25;
    public float Fullattackdamage = 50;
    public float AttackRange=0.5f;
    public LayerMask enemyLayer;
    private float mevcut�zelYetenekSeviye = 0f;
    public float �zelYetenekDolumH�z� = 3f;
    public float �zelYetenekSeviye = 100f;
    public Slider �zelYetenekBar;
    [SerializeField] private AudioSource AttackEffect;
    [SerializeField] private AudioSource FullAttackEffect;
    [SerializeField] private AudioSource JumpEffect;

    public Transform groundposition;
    public Transform Attackpoint;
    public float cap;
    public LayerMask layercheck;

    public static Vector2 lastCheckPointPosition = new Vector2((float)-86.51,(float)41.49);
    public static Vector2 startposition=new Vector2((float)-86.51,(float)41.49);
    private Playermanager playerParent;

    private void Awake()
    {
        playerParent = GetComponentInParent<Playermanager>();
        respawn();
    }
    

    void Start()
    {
        
        if (�zelYetenekBar != null)
        {
            �zelYetenekBar.maxValue = �zelYetenekSeviye;
            �zelYetenekBar.value = mevcut�zelYetenekSeviye;
        }
        rigidPlayer = GetComponent<Rigidbody2D>();
        PlayerAnimation = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMove();
        OnGraounded();
        rigidPlayer.velocity = new Vector2(Input.GetAxis("Horizontal") * hiz, rigidPlayer.velocity.y);

        if (rigidPlayer.velocity.x < 0 && facingRight)
        {
            flipFace();
        }
        else if (rigidPlayer.velocity.x > 0 && !facingRight)
        {
            flipFace();
        }
        if (Input.GetAxis("Vertical") > 0 && isGronded && (JumpTime < Time.timeSinceLevelLoad))
        {
            JumpEffect.Play();
            JumpTime = Time.timeSinceLevelLoad +JumpFrekans;
            PlayerJump();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerAnimation.SetTrigger("Attackanim");
            AttackEffect.Play();
            Attack();

        }
        DolumKontrolu();
        KullanmaKontrolu();
        G�ncelleUI();
       
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            playerParent.health += 20;
            lastCheckPointPosition = transform.position;
        }
    }
    void HorizontalMove()
    {
        rigidPlayer.velocity = new Vector2(Input.GetAxis("Horizontal"), rigidPlayer.velocity.y);
        PlayerAnimation.SetFloat("PlayerSpeed", Mathf.Abs(rigidPlayer.velocity.x));
    }
    void flipFace()
    {
        facingRight = !facingRight;
        Vector3 tempLocakScaele = transform.localScale;
        tempLocakScaele.x *= -1;
        transform.localScale = tempLocakScaele;
    }
    void PlayerJump()
    {
        rigidPlayer.AddForce(new Vector2(0f, JumpSpeed));
    }
    void OnGraounded()
    {
        isGronded = Physics2D.OverlapCircle(groundposition.position, cap, layercheck);
        PlayerAnimation.SetBool("isGrounded", isGronded);
    }


    void Attack()
    {
        // D��manlar� tespit et
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Attackpoint.position, AttackRange, enemyLayer);

        // Her tespit edilen d��mana hasar ver
        foreach (Collider2D enemy in hitEnemies)
        {
            // D��man�n sa�l�k bile�enini al
            EnemyManager enemyHealth = enemy.GetComponent<EnemyManager>();

            if (enemyHealth != null)
            {
                // Hasar� d��mana uygula
                enemyHealth.TakeDamage(attackdamage);
            }
        }

    }
    void FullAttack()
    {
        // D��manlar� tespit et
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Attackpoint.position, AttackRange, enemyLayer);

        // Her tespit edilen d��mana hasar ver
        foreach (Collider2D enemy in hitEnemies)
        {
            // D��man�n sa�l�k bile�enini al
            EnemyManager enemyHealth = enemy.GetComponent<EnemyManager>();

            if (enemyHealth != null)
            {
                // Hasar� d��mana uygula
                enemyHealth.TakeDamage(Fullattackdamage);
            }
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Attackpoint.position, AttackRange);
    }
    void DolumKontrolu()
    {
        mevcut�zelYetenekSeviye += �zelYetenekDolumH�z� * Time.deltaTime;
        mevcut�zelYetenekSeviye = Mathf.Clamp(mevcut�zelYetenekSeviye, 0f, �zelYetenekSeviye);
    }

    void KullanmaKontrolu()
    {
        if (Input.GetKeyDown(KeyCode.E) && mevcut�zelYetenekSeviye >= �zelYetenekSeviye)
        {
            Kullan�zelYetenek();
            FullAttackEffect.Play();
            mevcut�zelYetenekSeviye = 0f;
        }
    }

    void Kullan�zelYetenek()
    {
        // �zel yetenek animasyonunu ba�lat
        if (PlayerAnimation != null)
        {
            PlayerAnimation.SetTrigger("FullAttack");
        }
        FullAttack();

        // �zel yetenekle ilgili di�er i�lemleri buraya ekleyebilirsiniz.
    }

    void G�ncelleUI()
    {
        // UI bar�n� g�ncelle
        if (�zelYetenekBar != null)
        {
            �zelYetenekBar.value = mevcut�zelYetenekSeviye;
        }
    }

    public void respawn()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPosition;
        
    }

   
    




}
