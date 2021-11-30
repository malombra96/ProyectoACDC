using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirculoUnitario : MonoBehaviour
{
    [System.Serializable]
    public struct fasor
    {
        public GameObject line;
        public GameObject pointEnd;
        public GameObject triangulo;
    }

    public List<fasor> fasores;
    
    public List<List<int>> lst = new List<List<int>>();

    void Start()
    {
        for (int i = 0; i < fasores.Count; i++)
        {
            List<int> datos = new List<int>(20);
            lst.Add(datos);
        }
    }

    public void MagnitudVectores(float scale)
    {
        float scaleLine = 0.5f  * scale - 0.25f;
        for (int i = 0; i < fasores.Count; i++)
            fasores[i].line.transform.localScale = new Vector3(scaleLine, 1, 1);
    }

    public void turn(float radian, int fasor)
    {
        float grados = radian * 180 / Mathf.PI;
        //print("grados "+grados);
        fasores[fasor].line.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,grados);
        fasores[fasor].triangulo.GetComponent<RectTransform>().rotation = Quaternion.Euler(0,0,grados - 90);
        fasores[fasor].triangulo.GetComponent<RectTransform>().position =
            fasores[fasor].pointEnd.GetComponent<RectTransform>().position;
    }
}
