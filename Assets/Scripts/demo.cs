using UnityEngine;
using System.Collections;

/// Controla el comportamiento del demo
/**
	El demo cuenta con un galvanómetro el cual es modelado como un sistema de primer orden.
	El modelado se realiza usando una representación discreta en variables de estado
*/
public class demo : MonoBehaviour {

	/// Objeto de Unity que representa los ejes de graficación
    public GameObject axes;
	/// Objeto de Unity que representa la letra variable
    public Transform V;
	/// Objeto de Unity que representa la aguja del galvanómetro
	public Transform needle;
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
	/// variable para almacenar el polo del sistema
	float p;
	/// variable para almacenar el periodo de muestreo
	float Tm;
	/// variable para almacenar el valor de la amplitud del voltaje
    float v;
	/// variable para almacenar el ángulo que tomará la aguja
	float angle;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles de estado
*/
    void Start () {


        p = 3;
        X = 43;
        Tm = 0.02f;

        Ad = 1 + (-p * Tm);
        Bd = p * Tm;
        C = 1;
        D = 0;

        Xp = Ad * X + Bd * U;
        Y = C * X + D * U;

    }
// =====================================================================================================
/// Método Update. Se ejecuta una vez cada frame
/**
  Se encarga de graficar las variables, realiza la iteración del espacio de estados discreto, actualiza 
  ángulo del galvanómetro
*/
    void Update () {

        axes.GetComponent<AxesBehaviour>().ReferenceAssignment(V,V,true);

        v = V.localScale.y;
        angle = -25 * v + 42;

        U = angle;
        X = Xp;
        Xp = Ad * X + Bd * U;
        Y = C * X + D * U;

        needle.localEulerAngles = new Vector3(90, 180, Y);

    }
}
