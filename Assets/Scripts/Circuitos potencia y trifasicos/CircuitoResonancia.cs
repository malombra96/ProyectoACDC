using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircuitoResonancia : MonoBehaviour
{
    /// Objeto de Unity asociado al voltaje de la fuente
	public Transform OVs;
	/// Objeto de Unity asociado al resistor
	public Transform OR;
	/// Objeto de Unity asociado al voltaje en el capacitor
	public Transform OVc;
	/// Objeto de Unity asociado al voltaje en el resistor
	public Transform OVr;
	/// Objeto de Unity asociado al voltaje en el inductor
	public Transform OVl;

	/// Objeto de Unity que representa los ejes de graficación
	public GameObject axes;
	/// variable para almacenar el valor asignado al componente resistivo
	float R;
	/// variable para almacenar el valor asignado al componente capacitivo
	float c;
	/// variable para almacenar el valor asignado al componente inductivo
	float l;
	/// variable para almacenar el valor del voltaje en el capacitor
	float vc;
	/// variable para almacenar el valor del voltaje en el resitor
	float vr;
	/// variable para almacenar el valor del voltaje en el inductor
	float vl;
	/// variable para almacenar la proporción altura/ancho de las letras
    float ratio;

	/// variable para almacenar el valor asignado al periodo de muestreo
	float Tm;
	/// variable para almacenar el valor 2/Tm
	float Tmedios;
	/// variable para almacenar el valor de la entrada U[k] (entrada iteración actual)
	float uk; 
	/// variable para almacenar el valor de la entrada U[k - 1] (entrada en la iteración anterior)
	float uk_1;
	/// variable para almacenar el valor de la entrada U[k - 2] (entrada en la iteración anterior a la anterior)
	float uk_2;
	/// variable para almacenar valor de denominador temporal
	float tempDen;

	/// variable para almacenar valor del coeficiente a0 del denominador de la función de transferencia continua
	float a1;
	/// variable para almacenar valor del coeficiente a0 del denominador de la función de transferencia continua
	float a0;
	/// variable para almacenar valor del coeficiente d1 del denominador de la función de transferencia discreta
	float d1;
	/// variable para almacenar valor del coeficiente d0 del denominador de la función de transferencia discreta
	float d0;
	// coeficientes para la ecuación en diferencias de la salida Vc
	float cyk, cyk_1, cyk_2, cb2, cb1, cb0, cn2, cn1, cn0, cd1, cd0;
	// coeficientes para la ecuación en diferencias de la salida Vr
	float ryk, ryk_1, ryk_2, rb2, rb1, rb0, rn2, rn1, rn0, rd1, rd0;
	// coeficientes para la ecuación en diferencias de la salida Vl
	float lyk, lyk_1, lyk_2, lb2, lb1, lb0, ln2, ln1, ln0, ld1, ld0;

    // Variables para generar la señal de entrada a la matrices de estados 
    // varible para almacenar la magnitud de la señal
    float w;
    // varible para almacenar la magnitud de la señal
    float A;
    // variable para almacenar la línea de tiempo 
    float linetime;
    // variable para almacenar la magnitud de la frecuencia
    public float frecuencia ;
    //lista de los selecctores
    public List<Toggle> check;


// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles, toma los valores asignados a los componentes y cálcula los parámetros del
  modelo discreto del sistema usando transformación bilineal y ecuaciones en diferencias
*/
    void Start () {

		for (int i = 0; i < check.Count; i++)
			check[i].onValueChanged.AddListener(delegate(bool arg0) { CheckToggle(); });

		frecuencia = 2.245f;
	
        ratio = OVc.transform.localScale.x / OVc.transform.localScale.y;
        // iScale = I.transform.localScale;
        // Vcpos = OVc.transform.position;
		// Vrpos = OVr.transform.position;

        w = OVs.localScale.y;
        A = 0.9f * w + 0.0517f;
        linetime = 0;

        R = 1;
        c = OR.localScale.y;
        //c = 0.5f;
        l = 0.5f;
		Tm = 0.02f;
		Tmedios = 2 / Tm;

        uk = A * Mathf.Sin(frecuencia * linetime);
        print("SEÑAL DE ENTRADA "+uk);
		uk_1 = 0;
		uk_2 = 0;

		a1 = R / l;
		a0 = 1 / (l * c);

		cb2 = 0;
		cb1 = 0;
		cb0 = 1 / (l * c);

		rb2 = 0;
		rb1 = R/l;
		rb0 = 0;

		lb2 = 1;
		lb1 = 0;
		lb0 = 0;

    }
// =====================================================================================================
/// Método FixedUpdate. Se ejecuta una vez cada 0.02 segundos
/**
  Se encarga de graficar las variables de entrada y salida, lee las entradas y luego discretiza el modelo, 
  realiza la iteración de cada una de las ecuaciones en diferencias
*/
    void Update () {

        axes.GetComponent<AxisSin>().ReferenceAssignment(uk,vr,vr,vr,2);
//        print("SEÑAL DE ENTRADA "+uk);
        w = OVs.localScale.y;
        A = 2.04f * w - 1.327f;
        linetime += Time.deltaTime;

		uk = A * Mathf.Sin(frecuencia * linetime);
		R = 1;

		a1 = R / l;

		c = OR.localScale.y;

		a0 = 1 / (l * c);
		
		tempDen = Tmedios*Tmedios + a1*Tmedios + a0;

		d1 = (2*a0 -2*Tmedios*Tmedios) /tempDen;
		d0 = (Tmedios*Tmedios - a1*Tmedios + a0)/tempDen;

		// capacitor
		cn2 = (cb2*Tmedios*Tmedios + cb1*Tmedios + cb0)/tempDen;
		cn1 = (2*cb0 -2*cb2*Tmedios*Tmedios) /tempDen;
		cn0 = (cb2*Tmedios*Tmedios - cb1*Tmedios + cb0)/tempDen;

		cyk = -d1*cyk_1 - d0*cyk_2 + cn2*uk + cn1*uk_1 + cn0*uk_2;
		vc = cyk;

		cyk_2 = cyk_1;
		cyk_1 = cyk;

		// resistor
		rb1 = R / l;
		rn2 = (rb2*Tmedios*Tmedios + rb1*Tmedios + rb0)/tempDen;
		rn1 = (2*rb0 -2*rb2*Tmedios*Tmedios) /tempDen;
		rn0 = (rb2*Tmedios*Tmedios - rb1*Tmedios + rb0)/tempDen;

		ryk = -d1*ryk_1 - d0*ryk_2 + rn2*uk + rn1*uk_1 + rn0*uk_2;
		vr = ryk;

		//print("vr "+vr);
		ryk_2 = ryk_1;
		ryk_1 = ryk;

		// inductor
		ln2 = (lb2*Tmedios*Tmedios + lb1*Tmedios + lb0)/tempDen;
		ln1 = (2*lb0 -2*lb2*Tmedios*Tmedios) /tempDen;
		ln0 = (lb2*Tmedios*Tmedios - lb1*Tmedios + lb0)/tempDen;

		lyk = -d1*lyk_1 - d0*lyk_2 + ln2*uk + ln1*uk_1 + ln0*uk_2;
		vl = lyk;

		lyk_2 = lyk_1;
		lyk_1 = lyk;

		uk_2 = uk_1;
		uk_1 = uk;

        OVc.localScale = new Vector3(ratio * vc,vc,vc);
		OVr.localScale = new Vector3(ratio * vr,vr,vr)+ 0.001f*Vector3.one;
		OVl.localScale = new Vector3(ratio * vl,vl,vl)+ 0.001f*Vector3.one;

    }

	public void CheckToggle()
	{
		print("entro");

		if (check[0].isOn)
			frecuencia = 1.82f;

		if (check[1].isOn)
			frecuencia = 0.91f;

		if (check[2].isOn)
			frecuencia = 1.15f;
	}

}
