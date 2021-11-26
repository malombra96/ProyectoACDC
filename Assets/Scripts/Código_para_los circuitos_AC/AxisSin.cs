using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Controla el comportamiento de los ejes de graficación
/**
	Toma las variables de entrada y de salida de cada circuito y las gráfica
*/
public class AxisSin : MonoBehaviour {
	/// Objeto de Unity usado como referencia para la gráfica Roja
	public  float reference1,reference2, reference3, reference4;

	///Colores para cada una de las señales
	public Color colorSignal1, colorSignal2, colorSignal3, colorSignal4; 
	/// Objeto de Unity para instanciar la linea roja
	[HideInInspector] public LineRenderer axesLineRenderer1;
	/// Objeto de Unity para instanciar la linea azul
	[HideInInspector] public LineRenderer axesLineRenderer2;
	/// Objeto de Unity para instanciar la linea roja
	[HideInInspector] public LineRenderer axesLineRenderer3;
	/// Objeto de Unity para instanciar la linea azul
	[HideInInspector] public LineRenderer axesLineRenderer4;
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
	/// variable para almacenar el numero de señales a graficar
	public int numberSignal = 0;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles, instancia las lineas para las gráficas, almacena la posición de los ejes.
*/
	void Start () {

		// axesLineRenderer1 = gameObject.AddComponent<LineRenderer>();
		axesLineRenderer1 = transform.GetChild(0).gameObject.AddComponent<LineRenderer>();
		axesLineRenderer1.material = new Material(Shader.Find("Sprites/Default"));
		axesLineRenderer1.material.color = colorSignal1;
		axesLineRenderer1.startColor = colorSignal1;
		axesLineRenderer1.startWidth = 0.03f;
		axesLineRenderer1.positionCount = 1;

		axesLineRenderer2 = transform.GetChild(1).gameObject.AddComponent<LineRenderer>();
		axesLineRenderer2.material = new Material(Shader.Find("Sprites/Default"));
		axesLineRenderer2.material.color = colorSignal2;
		axesLineRenderer2.startColor = colorSignal2;
		axesLineRenderer2.startWidth = 0.03f;
		axesLineRenderer2.positionCount = 1;
		
		axesLineRenderer3 = transform.GetChild(2).gameObject.AddComponent<LineRenderer>();
		axesLineRenderer3.material = new Material(Shader.Find("Sprites/Default"));
		axesLineRenderer3.material.color = colorSignal3;
		axesLineRenderer3.startColor = colorSignal3;
		axesLineRenderer3.startWidth = 0.03f;
		axesLineRenderer3.positionCount = 1;
		
		axesLineRenderer4 = transform.GetChild(4).gameObject.AddComponent<LineRenderer>();
		axesLineRenderer4.material = new Material(Shader.Find("Sprites/Default"));
		axesLineRenderer4.material.color = colorSignal4;
		axesLineRenderer4.startColor = colorSignal4;
		axesLineRenderer4.startWidth = 0.03f;
		axesLineRenderer4.positionCount = 1;

		xp = transform.position.x;
		yp = transform.position.y;// - transform.localScale.z/2;
		zp = transform.position.z - 0.01f;

		sx = 5*transform.localScale.x * transform.parent.localScale.x;
		sy = 5*transform.localScale.z * transform.parent.localScale.y;
		
		/*print("sx = " + sx);
		print("transform.position.x"+transform.position.x);
		print("transform.localScale.x = " + transform.localScale.x);
		print("transform.parent.localScale.x = " + transform.parent.localScale.x);*/
		
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
	public void ReferenceAssignment (float O1, float O2, float O3, float O4, int sign = 1){

		reference1 = O1;
		reference2 = O2;
		reference3 = O3;
		reference4 = O4;
		numberSignal = sign;
		//outSign = sign;
}
// =====================================================================================================
/// Método Update. Se ejecuta una vez cada frame
/**
  Se encarga de graficar, actualiza la información de la entrada y la salida cada frame
*/
	void Update () {

		lineTime += Time.deltaTime;
		if(lineTime>20){
			Reset();
		}
		else{
			k++;
		}

		axesLineRenderer1.positionCount = k;
		axesLineRenderer2.positionCount = k;
		axesLineRenderer3.positionCount = k;
		axesLineRenderer4.positionCount = k;


		switch (numberSignal)
		{
			case 1:
				axesLineRenderer1.SetPosition(k-1,new Vector3(xp - sx + (lineTime/5)*(sx/2), yp-sy+reference1/1.5f*sy, zp));
				axesLineRenderer2.positionCount = 1;
				axesLineRenderer3.positionCount = 1;
				axesLineRenderer4.positionCount = 1;
				break;
			case 2:
				axesLineRenderer1.SetPosition(k-1,new Vector3(xp - sx + (lineTime/5)*(sx/2), yp-sy+reference1/1.5f*sy, zp));
				axesLineRenderer2.SetPosition(k-1,new Vector3(xp - sx + lineTime/5*sx/2, yp-sy+reference2/1.5f*sy, zp));
				axesLineRenderer3.positionCount = 1;
				axesLineRenderer4.positionCount = 1;
				break;
			case 3:
				axesLineRenderer1.SetPosition(k-1,new Vector3(xp - sx + (lineTime/5)*(sx/2), yp-sy+reference1/1.5f*sy, zp));
				axesLineRenderer2.SetPosition(k-1,new Vector3(xp - sx + lineTime/5*sx/2, yp-sy+reference2/1.5f*sy, zp));
				axesLineRenderer3.SetPosition(k-1,new Vector3(xp - sx + lineTime/5*sx/2, yp-sy+reference3/1.5f*sy, zp));
				axesLineRenderer4.positionCount = 1;
				break;
			case 4:
				axesLineRenderer1.SetPosition(k-1,new Vector3(xp - sx + (lineTime/5)*(sx/2), yp-sy+reference1/1.5f*sy, zp));
				axesLineRenderer2.SetPosition(k-1,new Vector3(xp - sx + lineTime/5*sx/2, yp-sy+reference2/1.5f*sy, zp));
				axesLineRenderer3.SetPosition(k-1,new Vector3(xp - sx + lineTime/5*sx/2, yp-sy+reference3/1.5f*sy, zp));
				axesLineRenderer4.SetPosition(k-1,new Vector3(xp - sx + lineTime/5*sx/2, yp-sy+reference4/1.5f*sy, zp));
				break;
		}
		
		
		/*if (outSign) {
		    axesLineRenderer2.SetPosition(k-1,new Vector3(xp - sx + lineTime/5*sx/2, yp-sy+reference2/1.5f*sy, zp));
		} 
		else {
			axesLineRenderer2.SetPosition(k-1,new Vector3(xp - sx + lineTime/5*sx/2, yp-sy-reference2/1.5f*sy, zp));
		}*/
	
	}

	public void Reset()
	{
		print("RESET");
		lineTime = 0;
		k = 1;
		axesLineRenderer1.positionCount = k;
		axesLineRenderer2.positionCount = k;
		axesLineRenderer3.positionCount = k;
		axesLineRenderer4.positionCount = k;
	}
}
