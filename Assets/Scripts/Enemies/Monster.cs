using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Enemy
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Slash"))
        {
            stunned = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null && player.vulnerable)
            {
                player.TakeDamage(damage);
                animator.CrossFade("Attack", 0f);
            }
        }
    }

    public void EndAttackAnim()
    {
        animator.CrossFade("Walk", 0f);
    }
}
