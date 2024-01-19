using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightningBall : MonoBehaviour
{
    public int damage;
    public float stormDuration;
    public GameObject thunderStrikePrefab;
    // Start is called before the first frame update
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
            Strike();
            Destroy(this.gameObject);
        }
    }

    private void Strike()
    {
        GameObject thunderStrike = Instantiate(thunderStrikePrefab, transform.position, Quaternion.identity);
        thunderStrike.GetComponent<ThunderStrike>().Strike(stormDuration);
    }
}
