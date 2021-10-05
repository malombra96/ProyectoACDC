using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PWMRL : MonoBehaviour
{
	
  /// Objeto de Unity asociado al voltaje de la fuente
	public Transform OVs;
	/// Objeto de Unity asociado a la resistor
	public Transform OR;
	/// Objeto de Unity asociado al voltaje en el inductor
	public Transform OVl;
	/// Objeto de Unity asociado a la corriente
	public Transform I;
	/// Objeto de Unity asociado a los signos de la salida
	public Transform Signs;
	/// Objeto de Unity que representa los ejes de graficación
	public GameObject axes;

	/// variable para almacenar el valor asignado al componente resistivo
	float r;
	/// variable para almacenar el valor asignado al componente inductivo
	float l;
	/// variable para almacenar el valor del voltaje en el inductor
	float vl;
	/// variable para almacenar A discreta en la representación de variable de estado
	float Ad;
	/// variable para almacenar B discreta en la representación de variable de estado
	float Bd;
	/// variable para almacenar C discreta en la representación de variable de estado
	float C;
	/// variable para almacenar D discreta en la representación de variable de estado
	float D;
	/// variable para almacenar la variable de estado x[k + 1] en la representación de variable de estado
	float Xp;
	/// variable para almacenar la variable de estado x[k] en la representación de variable de estado
	float X;
	/// variable para almacenar la salida en la representación de variable de estado
	float Y;
	/// variable para almacenar la entrada en la representación de variable de estado
	float U;
	/// variable para almacenar el periodo de muestreo
	float Tm;

	/// variable para almacenar la proporción altura/ancho de las letras
	float ratio;
	/// vector para almacenar la proporción de la corriente
	Vector3 iScale;
	/// vector para almacenar la posición de la variable de salida
	Vector3 Vlpos;


    // Variables para generar la señal de entrada a la matrices de estados 
    // variable para la magnitud de la señal
    float w;
    /// variable para linealizar la magnitud de la señal
    private float A;
    // variable para almacenar la línea de tiempo 
    float linetime;
    // variable para pasar de frecuencia angular (rad) a frecuencia (Hz) 
    float frecuencia;
    // variable para almacenar el periodo de la señal 
    float periodo;
    //slider para el valor util de la señal 
    private Slider _slider;
    //texto para saber el porcentaje de la señal util de pwm
    private Text _text;
    //slider para la amplitud de la frecuencia de la señal
    public BehaviourReloj memoria;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles, toma los valores asignados a los componentes y crea los valores de las matrices
  de la ecuación de estado discreta
*/
    void Start()
    {
	    _text = GameObject.Find("Text_Slider").GetComponent<Text>();
	    _slider = GameObject.Find("Slider").GetComponent<Slider>();

        ratio = OVl.transform.localScale.x / OVl.transform.localScale.y;
        iScale = I.transform.localScale;
        Vlpos = OVl.transform.position;
		
        frecuencia = (w) / (2 * Mathf.PI);			// determinamos la frecuencia de las señales
        periodo = 1 / frecuencia;					// obtenemos el periodo de las señales 
        
        w = OVs.localScale.y;
        linetime = 0;

        r = OR.localScale.y;
        l = 1;

        U = 0;
        Xp = 0;
        Tm = 0.02f;

        Ad = 1 + (-(r / l) * Tm);
        Bd = -(r / l) * Tm;
        C = 1;
        D = 1;

    }
// =====================================================================================================
/// Método FixedUpdate. Se ejecuta una vez cada 0.02 segundos
/**
    Se encarga de graficar las variables de entrada y salida, lee las entradas y luego discretiza el modelo, 
    realiza la iteración del espacio de estados discreto
*/
    void FixedUpdate()
    {
	    float divisor = 100 / _slider.value;
	    _text.text = Mathf.Round(_slider.value).ToString()+" %";
	    
        linetime += Time.deltaTime;
        w = OVs.localScale.y;

        X = Xp;
        frecuencia = (memoria.dato) / (2 * Mathf.PI);
        periodo = 1 / frecuencia;
        A = 2.04f * w - 1.327f;;
        U = (linetime % periodo < periodo/divisor) ? A : 0;
        r = OR.localScale.y;

        Ad = 1 + (-(r / l) * Tm);
        Bd = -(r / l) * Tm;

        Xp = Ad * X + Bd * U;
        Y =  C * X + D * U;

        vl = Y;

        float i = 0.02f * (U-vl) / r;

        //axes.GetComponent<AxisSin>().ReferenceAssignment(U,vl,true);

        if (vl >= 0) {
            OVl.localScale = new Vector3(ratio * vl, vl, vl);
            OVl.position = Vlpos - new Vector3(0, vl / 2, 0);
            Signs.localEulerAngles = new Vector3(90,-90,0);
            axes.GetComponent<AxisSin>().ReferenceAssignment(U,vl,true);
        }
        else
        {
            OVl.localScale = -new Vector3(ratio * vl, vl, vl);
            OVl.position = Vlpos - new Vector3(0, -vl / 2, 0);
            Signs.localEulerAngles = new Vector3(90, 90, 0);
            axes.GetComponent<AxisSin>().ReferenceAssignment(U,vl,true);
        }

        if (i > 0) {
            I.localScale = new Vector3(i, iScale.y, iScale.z);
        }
        else {
            I.localScale = new Vector3(-i, iScale.y, -iScale.z);
        }


    }
}
