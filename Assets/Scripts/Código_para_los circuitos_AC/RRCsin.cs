using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// Controla el comportamiento del circuito RRC con fuente de voltaje
/**
	Control del circuito RRC con fuente de voltaje el cual es modelado como un 
	sistema de primer orden. El modelado se realiza usando una representación 
	en ecuación en diferencias
*/

public class RRCsin : MonoBehaviour
{
	/// Objeto de Unity asociado al voltaje de la fuente
	public Transform OVs;
	/// Objeto de Unity asociado al resistor 1
	public Transform OR1;
	/// Objeto de Unity asociado al resistor 2
	public Transform OR2;
	/// Objeto de Unity asociado al voltaje en el capacitor
	public Transform OVc;
	/// Objeto de Unity asociado a la corriente
	public Transform I;
	
	/// Objeto de Unity que representa los ejes de graficación
	public GameObject axes;
	/// variable para almacenar el valor asignado al componente resistivo 1
	float r;
	/// variable para almacenar el valor asignado al componente resistivo 2
	float R;
	/// variable para almacenar el valor asignado al capacitor
	float c;
	/// variable para almacenar el valor del voltaje en el capacitor
	float vc;
	/// variable para almacenar la proporción altura/ancho de las letras
	float ratio;

	/// variable para almacenar el valor asignado al periodo de muestreo
	float T;
	/// variable para almacenar el valor de la entrada U[k] (entrada iteración actual)
	float uk; 
	/// variable para almacenar el valor de la entrada U[k - 1] (entrada en la iteración anterior)
	float uk_1;
	/// variable para almacenar el valor de la salida Y[k] (salida iteración actual)
	float yk; 
	/// variable para almacenar el valor de la salida Y[k - 1] (salida en la iteración anterior)
	float yk_1;

	/// variable para almacenar valor del coeficiente b0 del numerador de la función de transferencia continua
	float b0;
	/// variable para almacenar valor del coeficiente a0 del denominador de la función de transferencia continua
	float a0;
	/// vector para almacenar la proporción de la corriente
	Vector3 iScale;
	/// vector para almacenar la posición de la variable de salida
	Vector3 Vcpos;
    // Variables para generar la señal de entrada a la matrices de estados 
    // varible para almacenar la magnitud de la señal
    float w;
    // varible para almacenar la magnitud de la señal
    float A;
    // variable para almacenar la línea de tiempo 
    float linetime;
    //variable para almacenar la magnitud de la frecuencia
    public BehaviourReloj memoria;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles, toma los valores asignados a los componentes y cálcula los parámetros del
  modelo discreto del sistema usando transformación bilineal y ecuaciones en diferencias
*/
	void Start () {

		iScale = I.transform.localScale;
		ratio = OVc.transform.localScale.x / OVc.transform.localScale.y;
		Vcpos = OVc.transform.position;

		r = OR1.localScale.y;
		R = OR2.localScale.y;
		c = 1.5f;

        w = OVs.localScale.y;
        A = 0.9f * w + 0.0517f;
        linetime = 0;

		uk = A * Mathf.Sin(memoria.dato * linetime);
		uk_1 = uk;
		yk = 0;
		yk_1 = 0;

		T = 0.02f;

		b0 = 1 / (r * c);
		a0 = (r + R) / (r * R * c);

	}
// =====================================================================================================
/// Método FixedUpdate. Se ejecuta una vez cada 0.02 segundos
/**
  Se encarga de graficar las variables de entrada y salida, lee las entradas y luego discretiza el modelo, 
  realiza la iteración de la ecuación en diferencias
*/
	void Update () {


		axes.GetComponent<AxisSin>().ReferenceAssignment(uk,vc,true);

		w = OVs.localScale.y;
		A = 2.04f * w - 1.327f;
        linetime += Time.deltaTime;
        uk = A * Mathf.Sin(memoria.dato * linetime);
		r = OR1.localScale.y;
		R = OR2.localScale.y;

		b0 = 1 / (r * c);
		a0 = (r + R) / (r * R * c);

		yk = ((2/T-a0)*yk_1 + b0*uk + b0*uk_1) /(2/T+a0);

		vc = 1*yk;
		
		float vr = uk * R / (R * r); 

		float i = 0.02f*(uk - vr) / R;

		yk_1 = yk;
		uk_1 = uk;
		
		if (i > 0) {
			I.localScale = new Vector3(iScale.x, i, iScale.z);
		}
		else {
			I.localScale = new Vector3(-iScale.x, -i, iScale.z);
		}


		OVc.localScale = new Vector3(ratio * vc,vc,vc);
		//OVc.position = Vcpos - new Vector3(0, vc/2, 0);


	}
}
