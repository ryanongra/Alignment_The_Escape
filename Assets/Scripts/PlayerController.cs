using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int speed = 5;
    public Animator animator;
    public bool facingRight = true;
    private int jumpForce = 550;
    public float attackTime = 2;
    public static float health = 100;
    public Slider healthbar;
    public static float maxHealth = 100;
    public Text healthNum;

    public float attackDuration = 0.5f;
    public float remainingAttack = 0;
    public bool attacking = false;

    public float currJumpCooldown = 0.8f;
    public float jumpCooldown = 0.8f;

    public Canvas pause;
    public bool paused;

    public Canvas deathMenu;

    public int killCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        healthbar.value = 0.8f;
        killCount = 0;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Rigidbody2D>().freezeRotation = true;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            if (paused)
            {
                pause.gameObject.SetActive(true);
            }
            else
            {
                pause.gameObject.SetActive(false);
            }
        }

        if (!paused)
        {
            pause.gameObject.SetActive(false);

            currJumpCooldown -= Time.deltaTime;

            if (remainingAttack < 0 && remainingAttack > 0.15)
            {
                attacking = false;
            }
            else
            {
                remainingAttack -= Time.deltaTime;
            }


            healthbar.value = health / maxHealth;
            healthNum.text = health.ToString();

            if (health <= 0)
            {
                healthNum.text = "0";
                animator.SetTrigger("Death");
                paused = true;
                deathMenu.gameObject.SetActive(true);
            }

            animator.SetBool("Running", false);
            animator.SetBool("Grounded", IsGrounded());

            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!facingRight)
                {
                    transform.Rotate(new Vector3(0, 180));
                    facingRight = true;
                }
                if (IsGrounded())
                {
                    animator.SetBool("Running", true);
                }
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (facingRight)
                {
                    transform.Rotate(new Vector3(0, 180));
                    facingRight = false;
                }
                if (IsGrounded())
                {
                    animator.SetBool("Running", true);
                }
                transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.UpArrow) && IsGrounded() && currJumpCooldown < 0)
            {
                /*
                Vector3 jump = new Vector3(0, jumpForce, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector3(0f, 0f, 0f));
                GetComponent<Rigidbody2D>().AddForce(jump, ForceMode2D.Impulse);
                */

                animator.SetTrigger("Jump");
                currJumpCooldown = jumpCooldown;
                transform.GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.up * jumpForce);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                animator.SetTrigger("Attack");
                remainingAttack = attackDuration;
            }
            if (remainingAttack > 0 && remainingAttack < 0.15)
            {
                attacking = true;
            }
            else
            {
                attacking = false;
            }

            if (!IsGrounded())
            {
                animator.SetTrigger("Jump");
            }
        }
    }

    private bool IsGrounded()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheck>().isGrounded;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("DamageObstacle"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        animator.SetTrigger("Hurt");
        PlayerController.health -= damage;
    }
}
