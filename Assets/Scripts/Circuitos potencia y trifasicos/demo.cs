using UnityEngine;
using System.Collections;

/// Controla el comportamiento del demo
/**
	El demo cuenta con un galvanómetro el cual es modelado como un sistema de primer orden.
	El modelado se realiza usando una representación discreta en variables de estado
*/
public class demo : MonoBehaviour {

// objeto de unity como referencia para la frecuencia angular
	public Transform V; // frecuencia angular 
	// variable para la frecuencia 
	private float frecuencia;
	// magnitud de la frecuencia angular
	private float v;
	//valores para recrear dos señal senosoidal 
	private float signal_seno, signal_coseno, signal_seno2, signal_coseno2;
	// variable para determinar el periodo de la señal y generar el desfase entre las 2 señales 
    private float periodo;
	// objeto unity scope 
	public GameObject axes;
	// variable para generar el tiempo de las señales
	private float linetime_s, linetime_c, linetime_s2, linetime_c2; 
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
		v = V.localScale.y;							// frecuencia angular iniciada 
		frecuencia = (v) / (2 * Mathf.PI);			// determinamos la frecuencia de las señales
        periodo = 1 / frecuencia;					// obtenemos el periodo de las señales 
        linetime_s = 0;     					     // linea de tiempo en cero
        linetime_c = periodo / 4;					// generamos el desfase entre las señales
        linetime_s2 = periodo * 2 / 4;				// generamos el desfase entre las señales
        linetime_c2 = periodo * 3 / 4;					// generamos el desfase entre las señales
        memoria.dato = 1;
	}

    // =====================================================================================================

    void FixedUpdate()
    {
	    //axes.GetComponent<AxisSin>().ReferenceAssignment(signal_seno, signal_coseno, signal_seno2, signal_coseno2,1);
	    
        linetime_s += Time.deltaTime;										//tiempo de la señal seno 
	    linetime_c += Time.deltaTime;										//tiempo de la señal coseno
        linetime_s2 += Time.deltaTime;										//tiempo de la señal seno 
        linetime_c2 += Time.deltaTime;										//tiempo de la señal seno 

       // print("DELTA "+linetime_s);
        v = V.localScale.y; 												
        //axes.GetComponent<CirculoUnitario>().MagnitudVectores(v);
        
        float amplitud = 0.7f * v - 0.68f;	
        
        signal_seno = Mathf.Sin(memoria.dato * linetime_s);						//calculamos la señal seno
        //print("seno "+signal_seno);
        signal_coseno = amplitud*Mathf.Sin(memoria.dato * linetime_c);			//calculamos la señal coseno
        signal_seno2 = amplitud*Mathf.Sin(memoria.dato * linetime_s2);			//calculamos la señal seno
		signal_coseno2  = amplitud*Mathf.Sin(memoria.dato * linetime_c2);		//calculamos la señal seno
		
		axes.GetComponent<Fasores>().Amplitud1 = amplitud;
        axes.GetComponent<Fasores>().omega = memoria.dato;

        //frecuencia = (w) / (2 * Mathf.PI);											//actualizamos la frecuencia
        //linealizamos para generar el movimiento de la aguja del medidor
        //float angle = -(700/23)*w + (490 / 23);	
        // float pendiente = (-120-10)/(6-2);
        // float corte =  -120 - (6 * pendiente);
        // float angle = pendiente*memoria.dato + corte;
        // needle.localEulerAngles = new Vector3(90, 150, angle);	//generamos el movimiento de la aguja 
    }

    
}
