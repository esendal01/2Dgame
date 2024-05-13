using EthanTheHero;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Playermanager : MonoBehaviour
{
    // Start is called before the first frame update
    public float health;
    bool dead =false;
    Animator playerAnimation;
    public Slider slider;
    [SerializeField] private AudioSource HurtEffect;
    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        slider.maxValue = health;
        slider.value = health;
    }

    // Update is called once per frame
    void Update()
    {

        AmIdead();
    }


        public void Getdamage(float damage)
    {
        if (health - damage > 0)
        {
            HurtEffect.Play();
            health -= damage;
            playerAnimation.SetTrigger("Hurtanim");

        }
        else
        {
            health = 0;
        }
        slider.value = health;
    }
    IEnumerator WaitAndLoadScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Time.timeScale = 0;
        SceneManager.LoadScene(2);
        // Sahne geçiþ kodu buraya gelecek
    }
    void AmIdead()
    {
        if (health <= 0)
        {

            dead = true;
            playerAnimation.SetBool("deadAnim", dead);
            float deathAnimationLength = playerAnimation.GetCurrentAnimatorStateInfo(0).length;

            // Coroutine'i baþlatýn
            StartCoroutine(WaitAndLoadScene(deathAnimationLength));


        }
    }


}