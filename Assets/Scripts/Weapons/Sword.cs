using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    GameObject player;
    public GameObject slashPrefab;
    public Transform slashlPoint;
    public float slashTimer;
    AudioSource audioSource;

    public int damage;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Swing();
    }

    private void Aim()
    {
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        Debug.Log(transform.forward);
    }

    private void Swing()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Aim();
            audioSource.Play();
            GameObject newSlash = Instantiate(slashPrefab, slashlPoint);
            Slash slash = newSlash.GetComponent<Slash>();
            Rigidbody2D slashRB = newSlash.GetComponent<Rigidbody2D>();

            slash.damage = this.damage;
            newSlash.transform.parent = null;
            slashRB.AddForce(direction.normalized * 200);
        }
    }
}
