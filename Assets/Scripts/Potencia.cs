using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potencia : MonoBehaviour
{
	/// Objeto de Unity asociado al voltaje de fase Van
	public Transform OVs;

	/// Objeto de Unity asociado a la corriente de linea Ia
	public Transform OIs;

	/// Objetos de Unity asociados a la impedancia Z1
	public Transform OZR, OZX;

	/// Objeto de Unity que representa los ejes de graficación
	public GameObject axes;


	/// variable para almacenar el valor asignado al voltaje Vs
	private float Vs, Vrms;
	/// variable para la magnitud o voltaje pico Vs
	private float Vps;
	/// variable para almacenar el valor asignado al desfase Vs
	private float Fvs;

	/// variable para almacenar el valor asignado a la corriente Is
	private float Is, Irms;
	/// Variable para almacenar la magnitud o corriente pico de Is
	private float Ips;
	/// variable para almacenar el valor asignado al desfase Is
	private float Fis;

	/// Variable para almacenar la potencia apartente S
	private float S;
	private float MagS;
	private float FaseS;

	/// Representacion polar de la potencia aparente
	private float P;
	private float Q;

	/// valor real y reactancia del ZL
	private float ZR;
	private float ZX;
	/// Representacion polar de ZL
	private float MagZ;
	private float FaseZ;

	/// variable para la amplitud
	private float w;

	/// vector para almacenar la escala de las corrientes
	Vector3 IsScale;

	/// variable para el tiempo de la señal 
	private float linetime;

	///variable para la frecuencia de la señal
	//public BehaviourReloj memoria;
	public float Frecuency;

	// =====================================================================================================
	/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
	/**
	  Inicializa las varibles, toma los valores asignados a los componentes y crea los valores de las matrices
	  de la ecuación de estado discreta
*/
	void Start()
	{
		// Definir la frecuencia de operacion
		Frecuency = 2;
		
		// Setea la linea de tiempo a 0.
		linetime = 0;

		//Se adquieren las escalas de las corrientes
		IsScale = OIs.transform.localScale;

		// Se adquieren las escalas de los objetos para el voltaje.
		w = OVs.localScale.y;

		//Vps = 2.04f * w - 1.327f;
		Vps = w;

		//Se adquieren las escalas de los objetos para las impedancias Z1, Z2 y Z3. 
		ZR = OZR.localScale.y;
		ZX = OZX.localScale.y;

		//Se hace una representacion polar para cada una de las impedancias.
		MagZ = Mathf.Sqrt(Mathf.Pow(ZR, 2) + Mathf.Pow(ZX, 2));
		FaseZ = Mathf.Atan2(ZX, ZR);

		//Se declaran los desfases de las fuentes en una secuencia positiva.
		Fvs = Mathf.Deg2Rad * (0);

		//Representacion de los voltajes de fase en el domunio del tiempo.
		Vs = Vps * Mathf.Sin(Frecuency * linetime + Fvs);

		//Setear escala de las flechas de corriente a 0
		//OIs.localScale = new Vector3(0, 0, 0);

	}


	// =====================================================================================================
	/// Método FixedUpdate. Se ejecuta una vez cada 0.02 segundos
	/**
		Se encarga de graficar las variables de entrada y salida, lee las entradas y luego discretiza el modelo, 
		realiza la iteración del espacio de estados discreto
*/
	void FixedUpdate()
	{

		axes.GetComponent<AxisSin>().ReferenceAssignment(Q, P, S, P, 2);

		linetime += Time.deltaTime;

		//Se adquieren las escalas de las corrientes
		IsScale = OIs.transform.localScale;

		// Se adquieren las escalas de los objetos para el voltaje.
		w = OVs.localScale.y;

		//Vps = 2.04f * w - 1.327f;

		Vps = w;

		//Se adquieren las escalas de los objetos para las impedancias Z1, Z2 y Z3. 
		ZR = OZR.localScale.y;
		ZX = OZX.localScale.y;

		//Se hace una representacion polar para cada una de las impedancias.
		MagZ = Mathf.Sqrt(Mathf.Pow(ZR, 2) + Mathf.Pow(ZX, 2));
		FaseZ = Mathf.Atan2(ZX, ZR);

		//Se declaran los desfases de las fuentes en una secuencia positiva.
		Fvs = Mathf.Deg2Rad * (0);

		//Representacion de los voltajes de fase en el domunio del tiempo.
		Vs = Vps * Mathf.Sin(Frecuency * linetime + Fvs);

		//Resolvemos las ecuaciones para las corrientes Ia = Van/Z1, Ib = Vbn/Z2 y  Ic = Van/Z1

		Ips = Vps / MagZ;
		Fis = Fvs - FaseZ;
		Is = Ips * Mathf.Sin(Frecuency * linetime + Fis);

		//Se hallan los valores de Vrms e Irms

		Vrms = Vps / (Mathf.Sqrt(2));
		Irms = Ips / (Mathf.Sqrt(2));

		//Calculamos el valor de potencia compleja S = Vs(t) Is(t)

		S = Vs*Is;

		//Calculamos potencia real y potencia recativa

		MagS = Vrms * Irms;
		FaseS = Fvs - Fis;
		P = MagS * Mathf.Cos(FaseS);
		Q = MagS * Mathf.Sin(FaseS);

		//Setear escala de las flechas de corrientes
		if (Is > 0)
		{
			OIs.localScale = new Vector3(IsScale.x, IsScale.y, Is/50);
		}
		else
		{
			OIs.localScale = new Vector3(-IsScale.x, IsScale.y, Is/50);
		}

	}
}
