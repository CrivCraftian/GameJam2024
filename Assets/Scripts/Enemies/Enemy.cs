using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;

    public int health;

    public float moveSpeed;

    public bool stunned;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (!stunned)
        {
            direction = (player.transform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    public void KnockBack(Vector3 Velocity)
    {
        stunned = true;
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

    IEnumerator WakeUp()
    {
        yield return new WaitForSeconds(0.5f);
        stunned = false;
    }
}