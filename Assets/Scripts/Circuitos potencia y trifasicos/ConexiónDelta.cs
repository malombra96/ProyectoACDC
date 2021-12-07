using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ConexiónDelta : MonoBehaviour
{
	/// Colores para las señales
	public Color colorSignal1, colorSignal2, colorSignal3, colorSignal4, colorSignal5, colorSignal6;
	/// Objeto de Unity asociado al voltaje de linea Van
	public Transform OVab;
	/// Objeto de Unity asociado al voltaje de linea Vbn
	public Transform OVbc;
	/// Objeto de Unity asociado al voltaje de linea Vcn
	public Transform OVca;

	/// Objeto de Unity asociado a la corriente de linea Ia
	public Transform OIa;
	/// Objeto de Unity asociado a la corriente de linea Ib
	public Transform OIb;
	/// Objeto de Unity asociado a la corriente de linea Ic
	public Transform OIc;

	///// Objetos de Unity asociados a la impedancia Z1
	//public Transform OZR1, OZX1;
	///// Objetos de Unity asociados a la impedancia Z2
	//public Transform OZR2, OZX2;
	///// Objetos de Unity asociados a la impedancia Z3
	//public Transform OZR3, OZX3;
	
	/// Objetos de Unity asociados a la impedancia Z1
	public Transform OZ1;
	/// Objetos de Unity asociados a la impedancia Z2
	public Transform OZ2;
	/// Objetos de Unity asociados a la impedancia Z3
	public Transform OZ3;

	/// Objeto de Unity que representa los ejes de graficación
	public GameObject axes;

	/// variable para almacenar los voltajes de linea Vab, Vbc y Vca
	private float Vab, Vbc, Vca;
	/// variable para la magnitud o voltaje pico Van, Vbn y Vcn
	private float Vpab, Vpbc, Vpca;
	/// variable para almacenar el valor asignado al desfase Van, Vbn y Vcn
	private float FVab, FVbc, FVca;

	///Variables para almacenar las corrientes de carga Iab, Ibc e Ica
	private float Iab, Ibc, Ica;
	/// Variable para almacenar la magnitud o corriente pico de Ia, Ib e Ic.
	private float Ipab, Ipbc, Ipca;
	/// variable para almacenar el valor asignado al desfase Ia, Ib e Ic
	private float FIab, FIbc, FIca;

	/// variable para almacenar el valor asignado a la corriente Ia, Ib e Ic.
	private float Ia, Ib, Ic;
	/// Variable para almacenar la magnitud o corriente pico de Ia, Ib e Ic.
	private float Ipa, Ipb, Ipc;
	/// variable para almacenar el valor asignado al desfase Ia, Ib e Ic
	private float FIa, FIb, FIc;


	///// valor real y reactancia del Z1, Z2 y Z3
	//private float ZR1, ZR2, ZR3;
	//private float ZX1, ZX2, ZX3;
	/// valor real y reactancia del Z1, Z2 y Z3
	private float Z1, Z2, Z3;

	/// Representacion polar de Z1, Z2 y Z3
	private float MagZ1, MagZ2, MagZ3;
	private float FaseZ1, FaseZ2, FaseZ3;

	/// variable para la amplitud
	private float w1;
	private float w2;
	private float w3;

	/// vector para almacenar la escala de las corrientes
	Vector3 IaScale, IbScale, IcScale;

	/// variable para el tiempo de la señal 
	private float linetime;

	///variable para la frecuencia de la señal
	//public BehaviourReloj memoria;
	public float Frecuency;

	//lista de los selecctores
	public List<Toggle> check;

	/// variable para el cambio de señales
	private int signalCase;
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
		IaScale = OIa.transform.localScale;
		IbScale = OIb.transform.localScale;
		IcScale = OIc.transform.localScale;

		// Se adquieren las escalas de los objetos para el voltaje.
		w1 = OVab.localScale.y;
		w2 = OVbc.localScale.y;
		w3 = OVca.localScale.y;

		Vpab = w1;
		Vpbc = w2;
		Vpca = w3;

        ////Se adquieren las escalas de los objetos para las impedancias Z1, Z2 y Z3. 
        //ZR1 = OZR1.localScale.y;
        //ZX1 = OZX1.localScale.y;
        //ZR2 = OZR2.localScale.y;
        //ZX2 = OZX2.localScale.y;
        //ZR3 = OZR3.localScale.y;
        //ZX3 = OZX3.localScale.y;

        //Se adquieren las escalas de los objetos para las impedancias Z1, Z2 y Z3.
        Z1 = OZ1.localScale.y;
        Z2 = OZ2.localScale.y;
        Z3 = OZ3.localScale.y;

        //Se hace una representacion polar para cada una de las impedancias.
        MagZ1 = Mathf.Sqrt(Mathf.Pow(Z1, 2) + Mathf.Pow(Z1, 2));
		FaseZ1 = Mathf.Atan2(Z1, Z1) * Mathf.Rad2Deg;

		MagZ2 = Mathf.Sqrt(Mathf.Pow(Z2, 2) + Mathf.Pow(Z2, 2));
		FaseZ2 = Mathf.Atan2(Z2, Z2) * Mathf.Rad2Deg;

		MagZ3 = Mathf.Sqrt(Mathf.Pow(Z3, 2) + Mathf.Pow(Z3, 2));
		FaseZ3 = Mathf.Atan2(Z3, Z3) * Mathf.Rad2Deg;

		//Se declaran los desfases de las fuentes en una secuencia positiva.
		FVab = 0;
		FVbc = Mathf.Deg2Rad * (FVab - 120);
		FVca = Mathf.Deg2Rad * (FVab + 120);

		//Representacion de los voltajes de linea en el domunio del tiempo.
		Vab = Vpab * Mathf.Sin(Frecuency * linetime + FVab);
		Vbc = Vpbc * Mathf.Sin(Frecuency * linetime + FVbc);
		Vca = Vpca * Mathf.Sin(Frecuency * linetime + FVca);

		//Setear escala de las flechas de corriente a 0
		OIa.localScale = new Vector3(0, 0, 0);
		OIb.localScale = new Vector3(0, 0, 0);
		OIc.localScale = new Vector3(0, 0, 0);


		signalCase = 3;

		for (int i = 0; i < check.Count; i++)
			check[i].onValueChanged.AddListener(delegate (bool arg0) { CheckToggle(); });

		
		axes.GetComponent<AxisSin>().colorSignal1 = colorSignal2;
        axes.GetComponent<AxisSin>().colorSignal2 = colorSignal4;
        axes.GetComponent<AxisSin>().colorSignal3 = colorSignal6;
    }


	// =====================================================================================================
	/// Método FixedUpdate. Se ejecuta una vez cada 0.02 segundos
	/**
		Se encarga de graficar las variables de entrada y salida, lee las entradas y luego discretiza el modelo, 
		realiza la iteración del espacio de estados discreto
*/
	void FixedUpdate()
	{

		switch (signalCase)
		{
			case 0:
				axes.GetComponent<AxisSin>().ReferenceAssignment(Iab, Ia, 0, 0, 2);
				break;
			case 1:
				axes.GetComponent<AxisSin>().ReferenceAssignment(Ibc, Ib, 0, 0, 2);
				break;
			case 2:
				axes.GetComponent<AxisSin>().ReferenceAssignment(Ica, Ic, 0, 0, 2);
				break;
			case 3:
				axes.GetComponent<AxisSin>().ReferenceAssignment(Ia, Ib, Ic, 0, 3);
				break;
		}

		linetime += Time.deltaTime;

		// Se adquieren las escalas de los objetos para el voltaje.
		w1 = OVab.localScale.y;
		w2 = OVbc.localScale.y;
		w3 = OVca.localScale.y;

		Vpab = w1;
		Vpbc= w2;
		Vpca = w3;

		////Se adquieren las escalas de los objetos para las impedancias Z1, Z2 y Z3. 
		//ZR1 = OZR1.localScale.y;
		//ZX1 = OZX1.localScale.y;
		//ZR2 = OZR2.localScale.y;
		//ZX2 = OZX2.localScale.y;
		//ZR3 = OZR3.localScale.y;
		//ZX3 = OZX3.localScale.y;

		//Se adquieren las escalas de los objetos para las impedancias Z1, Z2 y Z3.
		Z1 = OZ1.localScale.y;
		Z2 = OZ2.localScale.y;
		Z3 = OZ3.localScale.y;

		//Se hace una representacion polar para cada una de las impedancias.
		MagZ1 = Mathf.Sqrt(Mathf.Pow(Z1, 2) + Mathf.Pow(Z1, 2));
		FaseZ1 = Mathf.Atan2(Z1, Z1) * Mathf.Rad2Deg;

		MagZ2 = Mathf.Sqrt(Mathf.Pow(Z2, 2) + Mathf.Pow(Z2, 2));
		FaseZ2 = Mathf.Atan2(Z2, Z2) * Mathf.Rad2Deg;

		MagZ3 = Mathf.Sqrt(Mathf.Pow(Z3, 2) + Mathf.Pow(Z3, 2));
		FaseZ3 = Mathf.Atan2(Z3, Z3) * Mathf.Rad2Deg;

		//Se declaran los desfases de las fuentes en una secuencia positiva.
		FVab = 0;
		FVbc = Mathf.Deg2Rad * (FVab - 120);
		FVca = Mathf.Deg2Rad * (FVab + 120);

		//Representacion de los voltajes de fase en el domunio del tiempo.
		Vab = Vpab * Mathf.Sin(Frecuency * linetime + FVab);
		Vbc = Vpbc * Mathf.Sin(Frecuency * linetime + FVbc);
		Vca = Vpca * Mathf.Sin(Frecuency * linetime + FVca);

		//Hallar las corrientes de carga;
		Ipab = Vpab / MagZ1;
		FIab = FVab - FaseZ1;
		Iab = Ipab * Mathf.Sin(Frecuency * linetime + FIab);

		Ipbc = Vpbc / MagZ2;
		FIbc = FVbc - FaseZ2;
		Ibc = Ipbc * Mathf.Sin(Frecuency * linetime + FIbc);

		Ipca = Vpca / MagZ3;
		FIca = FVca - FaseZ3;
		Ica = Ipca * Mathf.Sin(Frecuency * linetime + FIca);


		//Resolvemos las ecuaciones para las corrientes Ia, Ib e Ic

		Ia = Iab - Ica;

		Ib = Ibc - Iab;

		Ic = Ica - Ibc;


		//Setear escala de las flechas de corrientes
		if (Ia > 0)
		{
			OIa.localScale = new Vector3(IaScale.x, IaScale.y, Ia * 1 / 100);
		}
		else
		{
			OIa.localScale = new Vector3(-IaScale.x, IaScale.y, Ia * 1 / 100);
		}

		if (Ib > 0)
		{
			OIb.localScale = new Vector3(IbScale.x, IbScale.y, Ib * 1 / 100);
		}
		else
		{
			OIb.localScale = new Vector3(-IbScale.x, IbScale.y, Ib * 1 / 100);
		}

		if (Ic > 0)
		{
			OIc.localScale = new Vector3(IcScale.x, IcScale.y, Ic * 1 / 100);
		}
		else
		{
			OIc.localScale = new Vector3(-IcScale.x, IcScale.y, Ic * 1 / 100);
		}

	}

	public void CheckToggle()
	{
		if (check[0].isOn)
		{
			signalCase = 0;
			axes.GetComponent<AxisSin>().axesLineRenderer1.material.color = colorSignal1;
			axes.GetComponent<AxisSin>().axesLineRenderer2.material.color = colorSignal2;
			axes.GetComponent<AxisSin>().Reset();
		}

		if (check[1].isOn)
		{
			signalCase = 1;
			axes.GetComponent<AxisSin>().axesLineRenderer1.material.color = colorSignal3;
			axes.GetComponent<AxisSin>().axesLineRenderer2.material.color = colorSignal4;
			axes.GetComponent<AxisSin>().Reset();
		}

		if (check[2].isOn)
		{
			signalCase = 2;
			axes.GetComponent<AxisSin>().axesLineRenderer1.material.color = colorSignal5;
			axes.GetComponent<AxisSin>().axesLineRenderer2.material.color = colorSignal6;
			axes.GetComponent<AxisSin>().Reset();
		}

		if (check[3].isOn)
		{
			signalCase = 3;
			axes.GetComponent<AxisSin>().axesLineRenderer1.material.color = colorSignal1;
			axes.GetComponent<AxisSin>().axesLineRenderer2.material.color = colorSignal3;
			axes.GetComponent<AxisSin>().axesLineRenderer3.material.color = colorSignal6;
			axes.GetComponent<AxisSin>().Reset();
		}
	}
}
