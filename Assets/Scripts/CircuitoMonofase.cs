using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircuitoMonofase : MonoBehaviour
{
    // objeto de unity como referencia para la frecuencia angular
    public Transform V1; // frecuencia angular 
    //public Transform V2; // frecuencia angular 
    /// Objeto de Unity asociado al voltaje en el capacitor
    //public Transform OVf;
    /// Objeto de Unity asociado a la corriente de linea Ia
    public Transform OIa;

    /// Objetos de Unity asociados a la impedancia Z1
	public Transform OZR, OZX;

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
    public BehaviourReloj memoria;
    /// vector para almacenar la escala de las corriente
    private Vector3 IaScale;

    /// valor real y reactancia del ZL
	private float ZR;
    private float ZX;
    /// Representacion polar de ZL
    private float MagZ;
    private float FaseZ;

    //lista de los selecctores
    public List<Toggle> check;

    // =====================================================================================================
    // inicializa todas las variables 
    // ======================================================================================================
    void Start()
    {
        signal_int1 = 0;     					              	// linea de tiempo en cero
        V_1 = V1.localScale.y;							// frecuencia angular iniciada 
        //V_2 = V2.localScale.y;							// frecuencia angular iniciada 
        signal_int1 = Mathf.Sin(V_1);					// inicializamos la señal seno
        memoria.dato = 1;
    }

    // =====================================================================================================

    void FixedUpdate()
    {
	    
	    
        axes.GetComponent<AxisSin>().ReferenceAssignment(signal_int1, signal_Out1, signal_Out1, signal_coseno2,2);
	    
        linetime_int1 += Time.deltaTime;										//tiempo de la señal seno 
        linetime_int2 += Time.deltaTime;										//tiempo de la señal seno 
        linetime_Out1 += Time.deltaTime;										//tiempo de la señal seno 

        V_1 = V1.localScale.y;                                              //actualizamos la frecuencia 
                                                                            //V_2 = V2.localScale.y; 												//actualizamos la frecuencia 

        //float amplitud1 = 2.04f * V_1 - 1.33f;
        //float amplitud2 = 2.04f * V_2 - 1.33f;
        //float amplitud3 = amplitud1 + amplitud2;

        //Se adquieren las escalas de los objetos para las impedancias Z1, Z2 y Z3. 


        CheckToggle();

        if (ZR == 0.5f)
        {
            ZR = 0;
        }

        if (ZX == 0.5f)
        {
            ZX = 0;
        }

        //Se hace una representacion polar para cada una de las impedancias.
        MagZ = Mathf.Sqrt(Mathf.Pow(ZR, 2) + Mathf.Pow(ZX, 2));
        FaseZ = Mathf.Atan2(ZX, ZR);


        signal_int1 = V_1*Mathf.Sin(memoria.dato * linetime_int1);		//calculamos la señal seno
        //signal_int2 = amplitud2*Mathf.Sin(memoria.dato * linetime_int2);		//calculamos la señal seno
        signal_Out1 = V_1*Mathf.Sin(memoria.dato * linetime_Out1 + FaseZ);		//calculamos la señal seno

        //OVf.localScale = new Vector3( signal_Out1,signal_Out1,signal_Out1);
        //OVf.position = Vcpos - new Vector3(0, signal_Out1/2, 0);
        if (signal_int1 > 0)
        {
            OIa.localScale = new Vector3(0.01f, 0.01f, signal_int1 * 1/100);
        }
        else
        {
            OIa.localScale = new Vector3(-0.01f, 0.01f, signal_int1 * 1 / 100);
        }
        
    }

    public void CheckToggle()
    {
        if (check[0].isOn)
        {
            ZR = OZR.localScale.y;
            ZX = OZX.localScale.y;
        }

        if (check[1].isOn)
        {
            ZR = OZR.localScale.y;
            ZX = - OZX.localScale.y;
        }

    }
}
