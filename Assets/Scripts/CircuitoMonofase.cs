using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitoMonofase : MonoBehaviour
{
    // objeto de unity como referencia para la frecuencia angular
    public Transform V1; // frecuencia angular 
    public Transform V2; // frecuencia angular 
    /// Objeto de Unity asociado al voltaje en el capacitor
    public Transform OVf;
    /// variable para almacenar la proporción altura/ancho de las letras
    float ratio;
    // magnitud de la frecuencia angular
    private float V_1, V_2;
    //valores para recrear dos señal senosoidal 
    private float signal_int1, signal_int2, signal_Out1, signal_coseno2;
    // variable para determinar el periodo de la señal y generar el desfase entre las 2 señales 
    private float periodo;
    // objeto unity scope 
    public GameObject axes;
    // variable para generar el tiempo de las señales
    private float linetime_int1, linetime_int2, linetime_Out1, linetime_c2;
    /// vector para almacenar la posición de la variable de salida
    Vector3 Vcpos;
    ////slider para manejar la frecuencia
    //public Slider sliderFrecuencia;
    //public BehaviourReloj memoria;
	
// =====================================================================================================
// inicializa todas las variables 
// ======================================================================================================
    void Start()
    {
        signal_int1 = 0;     					              	// linea de tiempo en cero
        V_1 = V1.localScale.y;							// frecuencia angular iniciada 
        V_2 = V2.localScale.y;							// frecuencia angular iniciada 
        signal_int1 = Mathf.Sin(V_1);					// inicializamos la señal seno
        //memoria.dato = 1;
    }

    // =====================================================================================================

    void FixedUpdate()
    {
	    
	    
        axes.GetComponent<AxisSin>().ReferenceAssignment(signal_int1, signal_int2, signal_Out1, signal_coseno2,3);
	    
        linetime_int1 += Time.deltaTime;										//tiempo de la señal seno 
        linetime_int2 += Time.deltaTime;										//tiempo de la señal seno 
        linetime_Out1 += Time.deltaTime;										//tiempo de la señal seno 

        V_1 = V1.localScale.y; 												//actualizamos la frecuencia 
        V_2 = V2.localScale.y; 												//actualizamos la frecuencia 
	    
        float amplitud1 = 2.04f * V_1 - 1.33f;
        float amplitud2 = 2.04f * V_2 - 1.33f;
        float amplitud3 = amplitud1 + amplitud2;
        
        signal_int1 = amplitud1*Mathf.Sin(/*memoria.dato */ linetime_int1);		//calculamos la señal seno
        signal_int2 = amplitud2*Mathf.Sin(/*memoria.dato */ linetime_int2);		//calculamos la señal seno
        signal_Out1 = amplitud3*Mathf.Sin(/*memoria.dato */ linetime_Out1);		//calculamos la señal seno
        
        OVf.localScale = new Vector3( signal_Out1,signal_Out1,signal_Out1);
        OVf.position = Vcpos - new Vector3(0, signal_Out1/2, 0);
        
    }
}
