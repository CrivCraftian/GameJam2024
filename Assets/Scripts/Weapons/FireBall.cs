using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject explosionPrefab;
    private Rigidbody2D rb;

    public int damage;
    public int explosionDamage;
    public float explosionRadius;

    public bool splitEnabled;
    public int splitAmount;

    public bool hasExploded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            Explode();
        }
    }

    private void Explode()
    {
        if (!hasExploded)
        {
            hasExploded = true;
            GameObject newExplosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            newExplosion.transform.parent = null;
            Explosion explosion = newExplosion.GetComponent<Explosion>();
            explosion.Explode(explosionDamage, explosionRadius);
            if (splitEnabled)
            {
                explosion.Split(splitAmount);
            }
        }
        Destroy(this.gameObject);
    }
}