using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int damage;
    private CircleCollider2D collider;

    public GameObject fireBallPrefab;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        collider.enabled = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    public void Explode(int Damage, float Radius)
    {
        collider.enabled = true;

        damage = Damage;
        collider.radius = Radius;

        StartCoroutine(Burnout());
    }

    IEnumerator Burnout()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }

    public void Split(int splitAmount)
    {
        float angle = 360 / splitAmount;

        for (int i = 0; i < splitAmount; i++)
        {
            Vector2 direction = new Vector2(Mathf.Sin((transform.rotation.z + angle * i) * Mathf.Deg2Rad), Mathf.Cos((transform.rotation.z + angle * i) * Mathf.Deg2Rad));
            GameObject fireBall = Instantiate(fireBallPrefab, transform.position + Vector3.forward * 3, Quaternion.identity);
            FireBall fireBallScript = fireBall.GetComponent<FireBall>();
            fireBallScript.splitEnabled = false;
            fireBallScript.superCollider.enabled = false;
            
            fireBall.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            Rigidbody2D fireballRB = fireBall.GetComponent<Rigidbody2D>();
            fireballRB.AddForce(direction.normalized * 200);
        }
    }
}