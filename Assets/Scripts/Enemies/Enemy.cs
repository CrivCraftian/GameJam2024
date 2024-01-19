using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public SpriteRenderer sr;

    public int health;
    public int damage;

    public float moveSpeed;

    public bool stunned;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    public void MoveTowardsPlayer()
    {
        if (!stunned)
        {
            direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;

            if (player.transform.position.x < transform.position.x )
            {
                sr.flipX = true;
            }
            if (player.transform.position.x > transform.position.x)
            {
                sr.flipX = false;
            }
        }
    }

    public void KnockBack(Vector3 Velocity)
    {
        stunned = true;
        rb.velocity = Vector3.zero;
        rb.AddForce(Velocity);
        StartCoroutine(WakeUp());
    }

    public void TakeDamage(int amount)
    {
        health-= amount;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Slash"))
        {
            stunned = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Storm"))
        {
            stunned = false;
        }
    }

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(0.5f);
        stunned = false;
    }
}