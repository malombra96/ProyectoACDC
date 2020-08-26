using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Controla el comportamiento del circuito RL con fuente de corriente
/**
	Control del circuito RL con fuente de corriente el cual es modelado como un 
	sistema de primer orden. El modelado se realiza usando una representación 
	en ecuación en diferencias
*/
public class CIPRL : MonoBehaviour {

	/// Objeto de Unity asociado a la corriente de la fuente
	public Transform OIs;
	/// Objeto de Unity asociado al resistor
	public Transform OR;
	/// Objeto de Unity asociado a la corriente en el resistor
	public Transform OIr;
	/// Objeto de Unity asociado a la corriente en el inductor
	public Transform OIl;

	/// Objeto de Unity que representa los ejes de graficación
	public GameObject axes;
	/// variable para almacenar el valor asignado al componente resistivo
	float R;
	/// variable para almacenar el valor asignado al componente inductivo
	float l;
	/// variable para almacenar el valor de la corriente en el inductor
	float il;
	/// variable para almacenar el valor de la corriente en el resistor
	float ir;

	/// variable para almacenar el valor asignado al periodo de muestreo
	float Tm;
	/// variable para almacenar el valor 2/Tm
	float w;
	/// variable para almacenar el valor de la entrada U[k] (entrada iteración actual)
	float uk; 
	/// variable para almacenar el valor de la entrada U[k - 1] (entrada en la iteración anterior)
	float ukm1;
	/// variable para almacenar el valor de la salida Y[k] (salida iteración actual)
	float yk; 
	/// variable para almacenar el valor de la salida Y[k - 1] (salida en la iteración anterior)
	float ykm1;
	/// variable para almacenar valor de denominador temporal
	float d1Temp;

	/// variable para almacenar valor del coeficiente b1 del numerador de la función de transferencia continua
	float b1;
	/// variable para almacenar valor del coeficiente b0 del numerador de la función de transferencia continua
	float b0;
	/// variable para almacenar valor del coeficiente a0 del denominador de la función de transferencia continua
	float a0;
	/// variable para almacenar valor del coeficiente N1 del numerador de la función de transferencia discreta
	float N1;
	/// variable para almacenar valor del coeficiente N0 del numerador de la función de transferencia discreta
	float N0;
	/// variable para almacenar valor del coeficiente D0 del denominador de la función de transferencia discreta
	float D0;

	/// vector para almacenar la proporción de la corriente en el inductor
	Vector3 iScaleL;
	/// vector para almacenar la proporción de la corriente en el resistor
	Vector3 iScaleR;


// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles, toma los valores asignados a los componentes y cálcula los parámetros del
  modelo discreto del sistema usando transformación bilineal y ecuaciones en diferencias
*/
	void Start () {

		iScaleL = OIl.transform.localScale;
		iScaleR = OIr.transform.localScale;

		R = OR.localScale.y;
		l = 1;

		uk = OIs.localScale.y;
		Tm = 0.02f;
		w = 2 / Tm;

		yk = 0;
		ykm1 = 0;
		ukm1 = 0;

		b1 = 1;
		b0 = 0;
		a0 = R/l;

		d1Temp = w+a0;
		N1 = (b1*w+b0)/d1Temp;
		N0 = (b0-b1*w)/d1Temp;
		D0 = (a0-w)/d1Temp;


	}
// =====================================================================================================
/// Método FixedUpdate. Se ejecuta una vez cada 0.02 segundos
/**
  Se encarga de graficar las variables de entrada y salida, lee las entradas y luego discretiza el modelo, 
  realiza la iteración de la ecuación en diferencias
*/
	void FixedUpdate () {

		axes.GetComponent<AxesBehaviour>().ReferenceAssignment(OIs,OIr,true);

		R = OR.localScale.y;
		uk = OIs.localScale.y;

		a0 = R/l;

		d1Temp = w+a0;
		N1 = (b1*w+b0)/d1Temp;
		N0 = (b0-b1*w)/d1Temp;
		D0 = (a0-w)/d1Temp;

		yk = -D0 * ykm1 + N0 * ukm1 + N1 * uk;
		ir = yk;
		il = uk - ir;

		ukm1 = uk;
		ykm1 = yk;

		OIl.localScale = new Vector3 (iScaleL.x, il, iScaleL.z);

		if (ir > 0) {
			OIr.localScale = new Vector3 (iScaleR.x, ir, iScaleR.z);
		} 
		else {
			OIr.localScale = new Vector3 (-iScaleR.x, ir, iScaleR.z);
		}

	}
}
