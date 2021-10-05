using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
/// Controla el comportamiento del circuito RC con fuente de voltaje
/**
	Control del circuito RC con fuente de voltaje el cual es modelado como un 
	sistema de primer orden. El modelado se realiza usando una representación 
	discreta en variables de estado.
*/
public class CRsin : MonoBehaviour
{
	/// Objeto de Unity asociado al voltaje de la fuente
	public Transform OVs;
	/// Objeto de Unity asociado a la resistor
	public Transform OR;
	/// Objeto de Unity asociado al voltaje en el capacitor
	public Transform OVc;
	/// Objeto de Unity asociado a la corriente
	public Transform I;

	/// Objeto de Unity que representa los ejes de graficación
    public GameObject axes;
	/// variable para almacenar el valor asignado al componente resistivo
	float R;
	/// variable para almacenar el valor asignado al componente capacitivo
	float c;
	/// variable para almacenar el valor del voltaje en la resistencia
	float vr;
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
	Vector3 Vcpos;
	
	// variables para generar la funcion que nos servira como entrada de las variables de estado
	/// variable para la amplitud
	private float w;
	/// variable para la amplitud de la señal
	private float A;
	/// variable para el tiempo de la señal 
	private float linetime;

	///variable para la amplitud de la frecuencia de la señal
	public BehaviourReloj memoria;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles, toma los valores asignados a los componentes y crea los valores de las matrices
  de la ecuación de estado discreta
*/
    void Start () {

        ratio = OVc.transform.localScale.x / OVc.transform.localScale.y;
        iScale = I.transform.localScale;
        Vcpos = OVc.transform.position;

        w = OVs.localScale.y;
        A = 0.9f * w + 0.0517f;
        linetime = 0;
        
        R = OR.localScale.y;
        c = 2;

		U = A * Mathf.Sin(memoria.dato * linetime);
		Xp = 0;
		Tm = 0.02f;
		
		Ad = 1+(-1/(R * c) * Tm);
		Bd = 1;
		C = 1;
		D = 1;

		OVc.localScale = new Vector3(0,0,0);
        
	}
// =====================================================================================================
/// Método FixedUpdate. Se ejecuta una vez cada 0.02 segundos
/**
    Se encarga de graficar las variables de entrada y salida, lee las entradas y luego discretiza el modelo, 
    realiza la iteración del espacio de estados discreto
*/
    void FixedUpdate () {

        axes.GetComponent<AxisSin>().ReferenceAssignment(U,vr,true);

		X = Xp;
		w = OVs.localScale.y;
		A = 2.04f * w - 1.327f;
		linetime += Time.deltaTime;
		U = A * Mathf.Sin(memoria.dato * linetime);
		R = OR.localScale.y;

        Ad = 1 + (-1 / (R * c) * Tm);
        Bd = 1;
		
        //print("R = "+R+" AB = "+Ad+" Bd = "+Bd);
        
        Xp = Ad * X + Bd * U;
		Y  =  C * Xp - X;

		vr = Y;

        float i = 0.02f*(U - vr) / R;

        if (i > 0) {
            I.localScale = new Vector3(i, iScale.y, iScale.z);
        }
        else {
            I.localScale = new Vector3(-i, iScale.y, -iScale.z);
        }
		OVc.localScale = new Vector3( vr/2,(vr*2/4),vr/2);
        //OVc.position = Vcpos - new Vector3(0, vr / 2, 0);
       

    }
}
