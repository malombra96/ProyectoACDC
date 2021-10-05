using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourZz : MonoBehaviour
{
    public float z;
    private Vector3 posicion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        posicion = transform.position;
        transform.position = new Vector3(posicion.x,posicion.y,0);
    }
}
