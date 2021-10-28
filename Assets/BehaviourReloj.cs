using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BehaviourReloj : MonoBehaviour,  IPointerEnterHandler, IPointerExitHandler
{
    /// variables para la amplitud de la frecuencia de la señal
    public Slider sliderFrecuencia;
    //alamacenamiento de cambio de frecuencia 
    public float dato;

    //public tipoCircuito Circuito;
    
    void Start()
    {
        dato = sliderFrecuencia.value;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        dato = sliderFrecuencia.value;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //print("salida");
        dato = sliderFrecuencia.value;
    }
}
