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
    private float mevcutÖzelYetenekSeviye = 0f;
    public float özelYetenekDolumHýzý = 3f;
    public float özelYetenekSeviye = 100f;
    public Slider özelYetenekBar;
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
        
        if (özelYetenekBar != null)
        {
            özelYetenekBar.maxValue = özelYetenekSeviye;
            özelYetenekBar.value = mevcutÖzelYetenekSeviye;
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
        GüncelleUI();
       
        
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
        // Düþmanlarý tespit et
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Attackpoint.position, AttackRange, enemyLayer);

        // Her tespit edilen düþmana hasar ver
        foreach (Collider2D enemy in hitEnemies)
        {
            // Düþmanýn saðlýk bileþenini al
            EnemyManager enemyHealth = enemy.GetComponent<EnemyManager>();

            if (enemyHealth != null)
            {
                // Hasarý düþmana uygula
                enemyHealth.TakeDamage(attackdamage);
            }
        }

    }
    void FullAttack()
    {
        // Düþmanlarý tespit et
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(Attackpoint.position, AttackRange, enemyLayer);

        // Her tespit edilen düþmana hasar ver
        foreach (Collider2D enemy in hitEnemies)
        {
            // Düþmanýn saðlýk bileþenini al
            EnemyManager enemyHealth = enemy.GetComponent<EnemyManager>();

            if (enemyHealth != null)
            {
                // Hasarý düþmana uygula
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
        mevcutÖzelYetenekSeviye += özelYetenekDolumHýzý * Time.deltaTime;
        mevcutÖzelYetenekSeviye = Mathf.Clamp(mevcutÖzelYetenekSeviye, 0f, özelYetenekSeviye);
    }

    void KullanmaKontrolu()
    {
        if (Input.GetKeyDown(KeyCode.E) && mevcutÖzelYetenekSeviye >= özelYetenekSeviye)
        {
            KullanÖzelYetenek();
            FullAttackEffect.Play();
            mevcutÖzelYetenekSeviye = 0f;
        }
    }

    void KullanÖzelYetenek()
    {
        // Özel yetenek animasyonunu baþlat
        if (PlayerAnimation != null)
        {
            PlayerAnimation.SetTrigger("FullAttack");
        }
        FullAttack();

        // Özel yetenekle ilgili diðer iþlemleri buraya ekleyebilirsiniz.
    }

    void GüncelleUI()
    {
        // UI barýný güncelle
        if (özelYetenekBar != null)
        {
            özelYetenekBar.value = mevcutÖzelYetenekSeviye;
        }
    }

    public void respawn()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckPointPosition;
        
    }

   
    




}
