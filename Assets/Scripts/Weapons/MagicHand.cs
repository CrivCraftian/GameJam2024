using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MagicHand : MonoBehaviour
{
    GameObject player;
    public GameObject fireBallPrefab;
    public GameObject lightningBallPrefab;
    private GameObject spellPrefab;
    private Transform spellPoint;

    Vector2 direction;

    private float lastCastTime;
    public float castRate;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spellPoint = GetComponentInChildren<Transform>();
        spellPrefab = fireBallPrefab;
    }

    void Update()
    {
        Shoot();
        SpellInventory();
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
        if (Input.GetMouseButton(0) && Time.time > lastCastTime  + castRate)
        {
            Aim();
            GameObject spell = Instantiate(spellPrefab, spellPoint);
            Rigidbody2D spellRB = spell.GetComponent<Rigidbody2D>();
            spell.transform.parent = null;
            spellRB.AddForce(direction.normalized * 200);
            spell.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);

            lastCastTime = Time.time;
        }
    }

    private void SpellInventory()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            spellPrefab = fireBallPrefab;
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            spellPrefab = lightningBallPrefab;
        }
    }
}
