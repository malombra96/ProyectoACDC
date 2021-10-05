using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Controla el comportamiento de los ejes de graficación
/**
	Toma las variables de entrada y de salida de cada circuito y las gráfica
*/
public class AxisSin : MonoBehaviour {
	/// Objeto de Unity usado como referencia para la gráfica Roja
	public  float reference1,reference2;
	/// Objeto de Unity para instanciar la linea roja
	LineRenderer axesLineRenderer1;
	/// Objeto de Unity para instanciar la linea azul
	LineRenderer axesLineRenderer2;
	/// variable para almacenar la posición x de los ejes
	float xp;
	/// variable para almacenar la posición y de los ejes
	float yp;
	/// variable para almacenar la posición z de los ejes
	float zp;
	/// variable para almacenar la escala x de los ejes
	float sx;
	/// variable para almacenar la escala y de los ejes
	float sy;
	/// variable para almacenar el tiempo transcurrido, corresponde con la información del eje x
	float lineTime;
	/// variable para almacenar el ciclo del Update para graficar
	int k;
	/// variable para almacenar el signo de la señal azul
	bool outSign = true;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles, instancia las lineas para las gráficas, almacena la posición de los ejes.
*/
	void Start () {

		// axesLineRenderer1 = gameObject.AddComponent<LineRenderer>();
		axesLineRenderer1 = transform.GetChild(0).gameObject.AddComponent<LineRenderer>();
		axesLineRenderer1.material = new Material(Shader.Find("Sprites/Default"));
		axesLineRenderer1.material.color = Color.red;
		axesLineRenderer1.startColor = Color.red;
		axesLineRenderer1.startWidth = 0.03f;
		axesLineRenderer1.positionCount = 1;

		axesLineRenderer2 = transform.GetChild(1).gameObject.AddComponent<LineRenderer>();
		axesLineRenderer2.material = new Material(Shader.Find("Sprites/Default"));
		axesLineRenderer2.material.color = new Color(0.02F, 0.5F, 0.243F, 1F);
		axesLineRenderer2.startColor = new Color(0.02F, 0.5F, 0.243F, 1F);
		axesLineRenderer2.startWidth = 0.03f;
		axesLineRenderer2.positionCount = 1;


		xp = transform.position.x;
		yp = transform.position.y;// - transform.localScale.z/2;
		zp = transform.position.z - 0.01f;

		sx = 5*transform.localScale.x * transform.parent.localScale.x;
		sy = 5*transform.localScale.z * transform.parent.localScale.y;
		
		print("sx = " + sx);
		print("transform.position.x"+transform.position.x);
		print("transform.localScale.x = " + transform.localScale.x);
		print("transform.parent.localScale.x = " + transform.parent.localScale.x);
		
		ResetLineAxes();

	}
// =====================================================================================================
/// Método ResetLineAxes. Se ejecuta cada vez que se carga un nuevo circuito
/**
  Reinicia el tiempo, que correponde al eje X
*/
	public void ResetLineAxes(){

		lineTime = 0;
		k = 0;
	}
// =====================================================================================================
/// Método ReferenceAssignment. Asigna la variable de entrada y de salida
/**
  \param O1 Objeto que representa la variable roja
  \param O2 Objeto que representa la variable azul
  \param sign variable que representa el signo de la variable azul
*/
	public void ReferenceAssignment (float O1, float O2, bool sign){

		reference1 = O1;
		reference2 = O2;
		outSign = sign;
}
// =====================================================================================================
/// Método Update. Se ejecuta una vez cada frame
/**
  Se encarga de graficar, actualiza la información de la entrada y la salida cada frame
*/
	void Update () {

		lineTime += Time.deltaTime;
		if(lineTime>20){
			lineTime = 0;
			k = 1;
			axesLineRenderer1.positionCount = k;
			axesLineRenderer2.positionCount = k;
		}
		else{
			k++;
		}

		axesLineRenderer1.positionCount = k;
		axesLineRenderer2.positionCount = k;

		axesLineRenderer1.SetPosition(k-1,new Vector3(xp - sx + (lineTime/5)*(sx/2), yp-sy+reference1/1.5f*sy, zp));

		if (outSign) {
		    axesLineRenderer2.SetPosition(k-1,new Vector3(xp - sx + lineTime/5*sx/2, yp-sy+reference2/1.5f*sy, zp));
		} 
		else {
			axesLineRenderer2.SetPosition(k-1,new Vector3(xp - sx + lineTime/5*sx/2, yp-sy-reference2/1.5f*sy, zp));
		}


	}
}
