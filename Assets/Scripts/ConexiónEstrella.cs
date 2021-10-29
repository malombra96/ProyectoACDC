using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConexiónEstrella : MonoBehaviour
{
	/// Objeto de Unity asociado al voltaje de fase Van
	public Transform OVan;
	/// Objeto de Unity asociado al voltaje de fase Vbn
	public Transform OVbn;
	/// Objeto de Unity asociado al voltaje de fase Vcn
	public Transform OVcn;

	/// Objeto de Unity asociado a la corriente de linea Ia
	public Transform OIa;
	/// Objeto de Unity asociado a la corriente de linea Ib
	public Transform OIb;
	/// Objeto de Unity asociado a la corriente de linea Ic
	public Transform OIc;
	/// Objeto de Unity asociado a la corriente de neutro
	public Transform OIn;

	/// Objetos de Unity asociados a la impedancia Z1
	public Transform OZR1, OZX1;
	/// Objetos de Unity asociados a la impedancia Z2
	public Transform OZR2, OZX2;
	/// Objetos de Unity asociados a la impedancia Z3
	public Transform OZR3, OZX3;

	/// Objeto de Unity que representa los ejes de graficación
	public GameObject axes;


	/// variable para almacenar el valor asignado al voltaje de fase Van, Vbn y Vcn
	private float Van, Vbn, Vcn;
	/// variable para la magnitud o voltaje pico Van, Vbn y Vcn
	private float Vpa, Vpb, Vpc;
	/// variable para almacenar el valor asignado al desfase Van, Vbn y Vcn
	private float Fan, Fbn, Fcn;


	/// variable para almacenar el valor asignado a la corriente Ia, Ib e Ic.
	private float Ia, Ib, Ic;
	/// Variable para almacenar la magnitud o corriente pico de Ia, Ib e Ic.
	private float Ipa, Ipb, Ipc;
	/// variable para almacenar el valor asignado al desfase Ia, Ib e Ic
	private float FIa, FIb, FIc;

	/// variable para almacenar el valor asignado a la corriente de neutro
	private float In;
	/// variable para almacenar la magnitud o corriente pico de In.

	/// valor real y reactancia del Z1, Z2 y Z3
	private float ZR1, ZR2, ZR3;
	private float ZX1, ZX2, ZX3;
	/// Representacion polar de Z1, Z2 y Z3
	private float MagZ1, MagZ2, MagZ3;
	private float FaseZ1, FaseZ2, FaseZ3;


	/// variable para la amplitud
	private float w1;
	private float w2;
	private float w3;

	/// vector para almacenar la escala de las corrientes
	Vector3 IaScale, IbScale, IcScale, InScale;

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
		IaScale = OIa.transform.localScale;
		IbScale = OIb.transform.localScale;
		IcScale = OIc.transform.localScale;
		InScale = OIn.transform.localScale;

		// Se adquieren las escalas de los objetos para el voltaje.
		w1 = OVan.localScale.y;
		w2 = OVbn.localScale.y;
		w3 = OVcn.localScale.y;

		Vpa =  w1 ;
		Vpb =  w2 ;
		Vpc =  w3 ;

		//Se adquieren las escalas de los objetos para las impedancias Z1, Z2 y Z3. 
		ZR1 = OZR1.localScale.y;
		ZX1 = OZX1.localScale.y;
		ZR2 = OZR2.localScale.y;
		ZX2 = OZX2.localScale.y;
		ZR3 = OZR3.localScale.y;
		ZX3 = OZX3.localScale.y;

		//Se hace una representacion polar para cada una de las impedancias.
		MagZ1 = Mathf.Sqrt(Mathf.Pow(ZR1, 2) + Mathf.Pow(ZX1, 2));
		FaseZ1 = Mathf.Atan2(ZX1, ZR1);

		MagZ2 = Mathf.Sqrt(Mathf.Pow(ZR2, 2) + Mathf.Pow(ZX2, 2));
		FaseZ2 = Mathf.Atan2(ZX2, ZR2);

		MagZ3 = Mathf.Sqrt(Mathf.Pow(ZR3, 2) + Mathf.Pow(ZX3, 2));
		FaseZ3 = Mathf.Atan2(ZX3, ZR3);

		//Se declaran los desfases de las fuentes en una secuencia positiva.
		Fan = 0;
		Fbn = Mathf.Deg2Rad * (Fan - 120);
		Fcn = Mathf.Deg2Rad * (Fan + 120);

		//Representacion de los voltajes de fase en el domunio del tiempo.
		Van = Vpa * Mathf.Sin(Frecuency * linetime + Fan);
		Vbn = Vpb * Mathf.Sin(Frecuency * linetime + Fbn);
		Vcn = Vpc * Mathf.Sin(Frecuency * linetime + Fcn);

		//Setear escala de las flechas de corriente a 0
		OIa.localScale = new Vector3(0, 0, 0);
		OIb.localScale = new Vector3(0, 0, 0);
		OIc.localScale = new Vector3(0, 0, 0);
		OIn.localScale = new Vector3(0, 0, 0);

	}


	// =====================================================================================================
	/// Método FixedUpdate. Se ejecuta una vez cada 0.02 segundos
	/**
		Se encarga de graficar las variables de entrada y salida, lee las entradas y luego discretiza el modelo, 
		realiza la iteración del espacio de estados discreto
*/
	void FixedUpdate()
	{

		axes.GetComponent<AxisSin>().ReferenceAssignment(Van, Vbn, Vcn, In, 4);

		linetime += Time.deltaTime;

		// Se adquieren las escalas de los objetos para el voltaje.
		w1 = OVan.localScale.y;
		w2 = OVbn.localScale.y;
		w3 = OVcn.localScale.y;

		Vpa =  w1 ;
		Vpb =  w2 ;
		Vpc =  w3 ;

		//Se adquieren las escalas de los objetos para las impedancias Z1, Z2 y Z3. 
		ZR1 = OZR1.localScale.y;
		ZX1 = OZX1.localScale.y;
		ZR2 = OZR2.localScale.y;
		ZX2 = OZX2.localScale.y;
		ZR3 = OZR3.localScale.y;
		ZX3 = OZX3.localScale.y;

		//Se hace una representacion polar para cada una de las impedancias.
		MagZ1 = Mathf.Sqrt(Mathf.Pow(ZR1, 2) + Mathf.Pow(ZX1, 2));
		FaseZ1 = Mathf.Atan2(ZX1, ZR1);

		MagZ2 = Mathf.Sqrt(Mathf.Pow(ZR2, 2) + Mathf.Pow(ZX2, 2));
		FaseZ2 = Mathf.Atan2(ZX2, ZR2);

		MagZ3 = Mathf.Sqrt(Mathf.Pow(ZR3, 2) + Mathf.Pow(ZX3, 2));
		FaseZ3 = Mathf.Atan2(ZX3, ZR3);

		//Se declaran los desfases de las fuentes en una secuencia positiva.
		Fan = 0;
		Fbn = Mathf.Deg2Rad * (Fan - 120);
		Fcn = Mathf.Deg2Rad * (Fan + 120);

		//Representacion de los voltajes de fase en el domunio del tiempo.
		Van = Vpa * Mathf.Sin(Frecuency * linetime + Fan);
		Vbn = Vpb * Mathf.Sin(Frecuency * linetime + Fbn);
		Vcn = Vpc * Mathf.Sin(Frecuency * linetime + Fcn);

		//Resolvemos las ecuaciones para las corrientes Ia = Van/Z1, Ib = Vbn/Z2 y  Ic = Van/Z1

		Ipa = Vpa / MagZ1;
		FIa = Fan - FaseZ1;
		Ia = Ipa * Mathf.Sin(Frecuency * linetime + FIa);

		Ipb = Vpb / MagZ2;
		FIb = Fbn - FaseZ2;
		Ib = Ipb * Mathf.Sin(Frecuency * linetime + FIb);

		Ipc = Vpc / MagZ3;
		FIc = Fcn - FaseZ3;
		Ic = Ipc * Mathf.Sin(Frecuency * linetime + FIc);

		//Se halla la corriente de neutro In

		In = -1 * (Ia + Ib + Ic);

		//Setear escala de las flechas de corrientes
        if (Ia > 0)
        {
			OIa.localScale = new Vector3(IaScale.x, IaScale.y, Ia * 1/100);
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

		if (In > 0)
		{
			OIn.localScale = new Vector3(InScale.x, InScale.y, In * 1 / 100);
		}
		else
		{
			OIn.localScale = new Vector3(-InScale.x, InScale.y, In * 1 / 100);
		}
	}
}
