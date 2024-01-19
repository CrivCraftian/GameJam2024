using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    public int health;
    public bool vulnerable = true;
    public float iFrameLength = 2.5f;
    public float moveSpeed;  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.y += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x += 1;
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (moveDirection.x == 1 && stateInfo.IsName("Walk"))
        {
            sr.flipX = false;
        }
        else if (moveDirection.x == -1 && stateInfo.IsName("Walk"))
        {
            sr.flipX = true;
        }

        rb.velocity = moveDirection * moveSpeed;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        vulnerable = false;
        StartCoroutine(IFrames());
    }

    IEnumerator IFrames()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(iFrameLength);
        sr.color = Color.white;
        vulnerable = true;
    }
}
