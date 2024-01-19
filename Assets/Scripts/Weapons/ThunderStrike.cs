using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrike : MonoBehaviour
{
    public float stormDuration;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.stunned = true;
                enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            }
        }
    }

    public void Strike(float StormDuration)
    {
        stormDuration = StormDuration;
        StartCoroutine(Burnout());
    }

    IEnumerator Burnout()
    {
        yield return new WaitForSeconds(stormDuration);
        Destroy(this.gameObject);
    }
}
