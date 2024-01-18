using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleObject : MonoBehaviour
{
    public Triangle tri {  get; set; }
    [SerializeField] LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = 4;
        lineRenderer.SetPositions(new Vector3[] { tri.p1, tri.p2, tri.p3, tri.p1 });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
