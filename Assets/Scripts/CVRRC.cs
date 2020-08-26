using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Controla el comportamiento del circuito RRC con fuente de voltaje
/**
	Control del circuito RRC con fuente de voltaje el cual es modelado como un 
	sistema de primer orden. El modelado se realiza usando una representación 
	en ecuación en diferencias
*/
public class CVRRC : MonoBehaviour {

	/// Objeto de Unity asociado al voltaje de la fuente
	public Transform OVs;
	/// Objeto de Unity asociado al resistor 1
	public Transform OR1;
	/// Objeto de Unity asociado al resistor 2
	public Transform OR2;
	/// Objeto de Unity asociado al voltaje en el capacitor
	public Transform OVc;

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

	/// vector para almacenar la posición de la variable de salida
	Vector3 Vcpos;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles, toma los valores asignados a los componentes y cálcula los parámetros del
  modelo discreto del sistema usando transformación bilineal y ecuaciones en diferencias
*/
	void Start () {


		ratio = OVc.transform.localScale.x / OVc.transform.localScale.y;
		Vcpos = OVc.transform.position;

		r = OR1.localScale.y;
		R = OR2.localScale.y;
		c = 1.5f;

		uk = OVs.localScale.y;
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


		axes.GetComponent<AxesBehaviour>().ReferenceAssignment(OVs,OVc,true);

		uk = OVs.localScale.y;
		r = OR1.localScale.y;
		R = OR2.localScale.y;

		b0 = 1 / (r * c);
		a0 = (r + R) / (r * R * c);

		yk = ((2/T-a0)*yk_1 + b0*uk + b0*uk_1) /(2/T+a0);

		vc = 1*yk;

		yk_1 = yk;
		uk_1 = uk;

		OVc.localScale = new Vector3(ratio * vc,vc,vc);
		OVc.position = Vcpos - new Vector3(0, vc/2, 0);


	}
}
