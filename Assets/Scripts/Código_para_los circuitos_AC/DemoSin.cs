﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// Controla el comportamiento del demo
/**

*/
public class DemoSin : MonoBehaviour
{
	// objeto de unity como referencia para la frecuencia angular
	public Transform W; // frecuencia angular 
	// variable para la frecuencia 
	private float frecuencia;
	// magnitud de la frecuencia angular
	private float w;
	//valores para recrear dos señal senosoidal 
	private float signal_seno, signal_coseno;
	// variable para determinar el periodo de la señal y generar el desfase entre las 2 señales 
    private float periodo;
	// objeto unity scope 
    public GameObject axes;
	// variable para generar el tiempo de las señales
	private float linetime_s, linetime_c; 
	// objeto unity aguja del medidor 
	public Transform needle;
	////slider para manejar la frecuencia
	//public Slider sliderFrecuencia;
	public BehaviourReloj memoria;
	
// =====================================================================================================
// inicializa todas las variables 
// ======================================================================================================
	void Start()
	{
		linetime_s = 0;     					              	// linea de tiempo en cero
		w = W.localScale.y;							// frecuencia angular iniciada 
		frecuencia = (w) / (2 * Mathf.PI);			// determinamos la frecuencia de las señales
        periodo = 1 / frecuencia;					// obtenemos el periodo de las señales 
	    signal_seno = Mathf.Sin(w);					// inicializamos la señal seno
        linetime_c = periodo / 4;					// generamos el desfase entre las señales
	}

    // =====================================================================================================

    void FixedUpdate()
    {
	    
	    
	    axes.GetComponent<AxisSin>().ReferenceAssignment(signal_seno, signal_coseno, true);
	    
	    linetime_c += Time.deltaTime;										//tiempo de la señal coseno
        linetime_s += Time.deltaTime;										//tiempo de la señal seno 
        w = W.localScale.y; 												//actualizamos la frecuencia 
        float amplitud = 2.04f * w - 1.33f;	
        print("W = "+ w);
        print("amplitud = "+ amplitud);
        signal_coseno = amplitud*Mathf.Sin(memoria.dato * linetime_c);							//calculamos la señal coseno
        signal_seno = amplitud*Mathf.Sin(memoria.dato * linetime_s);							//calculamos la señal seno
        frecuencia = (w) / (2 * Mathf.PI);									//actualizamos la frecuencia
        //linealizamos para generar el movimiento de la aguja del medidor
        //float angle = -(700/23)*w + (490 / 23);	
        float pendiente = (-120-10)/(6-2);
        float corte =  -120 - (6 * pendiente);
        float angle = pendiente*memoria.dato + corte;
        needle.localEulerAngles = new Vector3(90, 150, angle);	//generamos el movimiento de la aguja 
    }


}

