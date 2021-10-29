using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitoTrifasico : MonoBehaviour
{
    /// Objeto de Unity asociado al voltaje de fase Van
	public Transform OVan;
	/// Objeto de Unity asociado al voltaje de fase Vbn
	public Transform OVbn;
	/// Objeto de Unity asociado al voltaje de fase Vcn
	public Transform OVcn;

	/// Objeto de Unity asociado al voltaje de fase Van
	public Transform OVoan;
	/// Objeto de Unity asociado al voltaje de fase Vbn
	public Transform OVobn;
	/// Objeto de Unity asociado al voltaje de fase Vcn
	public Transform OVocn;
	
	/// Objeto de Unity asociado al voltaje de linea Vab.
	public Transform OVab;
	/// Objeto de Unity asociado al voltaje de linea Vbc.
	public Transform OVbc;
	/// Objeto de Unity asociado al voltaje de linea Vca.
	public Transform OVca;

	/// Objeto de Unity que representa los ejes de graficación
	public GameObject axes;

	/// variable para almacenar el valor asignado al voltaje de fase Van
	private float Van;
	/// variable para almacenar el valor asignado al desfase Van
	private float Fan;

	/// variable para almacenar el valor asignado al voltaje de fase Vbn
	private float Vbn;
	/// variable para almacenar el valor asignado al desfase Vbn
	private float Fbn;

	/// variable para almacenar el valor asignado al voltaje de fase Vcn
	private float Vcn;
	/// variable para almacenar el valor asignado al desfase Vcn
	private float Fcn;

	/// variable para almacenar el valor asignado al voltaje de linea Vab.
	private float Vab;
	/// variable para almacenar el valor asignado al desfase Vab.
	private float Fab;

	/// variable para almacenar el valor asignado al voltaje de linea Vbc.
	private float Vbc;
	/// variable para almacenar el valor asignado al desfase Vbc.
	private float Fbc;

	/// variable para almacenar el valor asignado al voltaje de linea Vca.
	private float Vca;
	/// variable para almacenar el valor asignado al desfase Vca.
	private float Fca;

	/// variable para la amplitud
	private float w1;
	private float w2;
	private float w3;
	/// variable para la amplitud de la señal
	private float Vpa;
	private float Vpb;
	private float Vpc;

	/// variable para el tiempo de la señal 
	private float linetime;

	///variable para la frecuencia de la señal
	//private BehaviourReloj memoria;
	public float Frecuency;

	// =====================================================================================================
	/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
	/**
	  Inicializa las varibles, toma los valores asignados a los componentes y crea los valores de las matrices
	  de la ecuación de estado discreta
*/
	void Start()
	{
		
		Frecuency = 4;
		
		w1 = OVan.localScale.y;
		w2 = OVbn.localScale.y;
		w3 = OVcn.localScale.y;

		Vpa =  w1 ;
		Vpb =  w2 ;
		Vpc =  w3 ;

		linetime = 0;

		Fan = 0;
		Fbn = Mathf.Deg2Rad *(Fan - 120);
		Fcn = Mathf.Deg2Rad *(Fan + 120);

		Van = Vpa * Mathf.Sin(Frecuency * linetime + Fan);
		Vbn = Vpb * Mathf.Sin(Frecuency * linetime + Fbn);
		Vcn = Vpc * Mathf.Sin(Frecuency * linetime + Fcn);


		OVab.localScale = new Vector3(0, 0, 0);
		OVbc.localScale = new Vector3(0, 0, 0);
		OVca.localScale = new Vector3(0, 0, 0);


	}


	// =====================================================================================================
	/// Método FixedUpdate. Se ejecuta una vez cada 0.02 segundos
	/**
		Se encarga de graficar las variables de entrada y salida, lee las entradas y luego discretiza el modelo, 
		realiza la iteración del espacio de estados discreto
*/
	void FixedUpdate()
	{

		axes.GetComponent<AxisSin>().ReferenceAssignment(Van, Vab, Vcn, Vbc, 2);

		w1 = OVan.localScale.y;
		w2 = OVbn.localScale.y;
		w3 = OVcn.localScale.y;

		Vpa =  w1 ;
		Vpb =  w2 ;
		Vpc =  w3 ;

		linetime += Time.deltaTime;

		Van = Vpa * Mathf.Sin(Frecuency * linetime + Fan);
		Vbn = Vpb * Mathf.Sin(Frecuency * linetime + Fbn);
		Vcn = Vpc * Mathf.Sin(Frecuency * linetime + Fcn);


        float Rab, Xab, Rbc, Xbc, Rca, Xca;

        Rab = Vpa * Mathf.Cos(Fan) - Vpb * Mathf.Cos(Fbn);
        Rbc = Vpb * Mathf.Cos(Fbn) - Vpc * Mathf.Cos(Fcn);
        Rca = Vpc * Mathf.Cos(Fcn) - Vpa * Mathf.Cos(Fan);

        Xab = Vpa * Mathf.Sin(Fan) - Vpb * Mathf.Sin(Fbn);
        Xbc = Vpb * Mathf.Sin(Fbn) - Vpc * Mathf.Sin(Fcn);
        Xca = Vpc * Mathf.Sin(Fcn) - Vpa * Mathf.Sin(Fan);

        //Calculamos la magnitud |Vab|, |Vbc| y |Vca|

        Fab = Mathf.Atan2(Xab, Rab);
        Fbc = Mathf.Atan2(Xbc, Rbc);
        Fca = Mathf.Atan2(Xca, Rca);

        Vab = Mathf.Sqrt(Mathf.Pow(Rab, 2) + Mathf.Pow(Xab, 2)) * Mathf.Sin(Frecuency * linetime + Fab);
        Vbc = Mathf.Sqrt(Mathf.Pow(Rbc, 2) + Mathf.Pow(Xbc, 2)) * Mathf.Sin(Frecuency * linetime + Fbc);
        Vca = Mathf.Sqrt(Mathf.Pow(Rca, 2) + Mathf.Pow(Xca, 2)) * Mathf.Sin(Frecuency * linetime + Fca);


        OVab.localScale = new Vector3(Vab, Vab, Vab);
		OVbc.localScale = new Vector3(Vbc, Vbc, Vbc);
		OVca.localScale = new Vector3(Vca, Vca, Vca);
		
		OVoan.localScale = new Vector3(Van, Van, Van / 2);
		OVobn.localScale = new Vector3(Vbn, Vbn, Vbn / 2);
		OVocn.localScale = new Vector3(Vcn, Vcn, Vcn / 2);

	}
}
