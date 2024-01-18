using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeObject : MonoBehaviour
{
    public Vector2 p1;
    public Vector2 p2;

    [SerializeField] LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        lr.positionCount = 2;
        lr.SetPositions(new Vector3[] {p1, p2} );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
