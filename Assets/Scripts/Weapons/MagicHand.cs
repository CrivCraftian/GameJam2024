using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MagicHand : MonoBehaviour
{
    GameObject player;
    public GameObject spellrefab;
    private Transform spellPoint;

    Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spellPoint = GetComponentInChildren<Transform>();
    }

    void Update()
    {
        Shoot();
    }

    private void Aim()
    {
        direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        Debug.Log(transform.forward);
        transform.position = player.transform.position + (transform.up);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Aim();
            GameObject spell = Instantiate(spellrefab, spellPoint);
            Rigidbody2D spellRB = spell.GetComponent<Rigidbody2D>();
            spell.transform.parent = null;
            spellRB.AddForce(direction.normalized * 200);
            spell.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
        }
    }
}
