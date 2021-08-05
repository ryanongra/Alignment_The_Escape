using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyContrroller : MonoBehaviour
{

    public PlayerController player;
    private Animator animator;

    public float attackCooldown = 5;
    public float attackDuration = 0.5f;

    public float remainingAttackCooldown = 5;
    public float remainingAttackDuration = 0.5f;

    //public Slider healthbar;
    public const float maxHealth = 20;
    //public Text healthNum;
    public float remainingHealth = 20;

    public float deathDelay = 2;
    public bool dead = false;

    public GameObject levelController;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            //healthNum.text = remainingHealth.ToString();
            //healthbar.value = remainingHealth / maxHealth;

            if (remainingHealth <= 0)
            {
                dead = true;
                player.killCount++;
            }

            if (remainingAttackCooldown < 0)
            {
                animator.SetTrigger("Attack");
                remainingAttackDuration = attackDuration;
                remainingAttackCooldown = attackCooldown;
            }
            else
            {
                remainingAttackCooldown -= Time.deltaTime;
                remainingAttackDuration -= Time.deltaTime;
            }
        } else
        {
            animator.SetTrigger("Death");
            if (deathDelay < 0)
            {
                //healthbar.gameObject.SetActive(false);
                this.gameObject.SetActive(false);
            } else
            {
                deathDelay = Time.deltaTime;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (!dead)
        {
            if (remainingAttackDuration > 0 && remainingAttackDuration < 0.15 && collision.transform.CompareTag("Player"))
            {
                player.TakeDamage(30);
                remainingAttackDuration = -1;
            }
            if (player.attacking && collision.transform.CompareTag("Player"))
            {
                this.remainingHealth -= 20;
                player.attacking = false;
                animator.SetTrigger("Hurt");
            }
        }
    }
}
