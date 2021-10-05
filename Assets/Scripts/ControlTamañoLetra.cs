using UnityEngine;
using System.Collections;

/// Controla el comportamiento de las letras cuando interactuan con las entradas del ratón
/**
	Permite cambian el ícono del cursor cuando entra o sale de una letra variable, 
	permite cambiar el tamaño de las letras varible, 
	permite cambiar la posición de las letras varible
*/
public class ControlTamañoLetra : MonoBehaviour {

	/// Objeto de Unity que contiene la textura para el cursor
    Texture2D cursorTexture;
	/// variable para saber si se activó el evento clic sostenido
    bool clickHold;
	/// Objeto de Unity para almecenar la representación de selección de una letra variable
	GameObject highlightObject;
	/// variable para alamcenar el color de la letra seleccionada
    Color colorLetra;
	/// variable para el centro de la letra varible seleccionada
    Vector3 posFija;
	/// variable para el valor de escala mínimo que tomará una letra
    public float valorMin = 0.5f;
	/// variable para el valor de escala máximo que tomará una letra
	public float valorMax = 2.5f;
	/// variable para almacenar la proporción altura/ancho de las letras
    float ratio;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles
*/
	void Start () {

        ratio = transform.localScale.x / transform.localScale.y;

        posFija = transform.position + new Vector3(0,transform.localScale.y/2, 0);
        //posFija = transform.position + new Vector3(0, 0.5f, 0);

        colorLetra = gameObject.GetComponent<Renderer>().material.color;
        cursorTexture = GameObject.Find("Control").GetComponent<General>().cursorTexture;

	}
// =====================================================================================================
/// Método evento OnMouseOver. Se ejecuta mientras el cursor se encuentra ubicado dentro del una letra variable
/**
  Cuando el cursor se encuentra sobre una letra varible cambiar la textura del cursor
*/
    void OnMouseOver() {

            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
			print("esta sobre la letra");
        
    }
// =====================================================================================================
/// Método evento OnMouseExit. Se ejecuta una vez cuando el cursor sale de una letra variable
/**
  Cuando el cursor sale de una letra varible cambia la textura del cursor solo si no se ha dado clic
*/
    void OnMouseExit()  {

        if (!clickHold) {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

    }
// =====================================================================================================
/// Método evento ClickDownHold. Se ejecuta mientras el cursor se encuentra en letra variable y se hace clic sostenido
/**
  Cambia el tamaño de la letra variable según el movimiento del cursor
*/
    void  ClickDownHold(){
		
		if(highlightObject){
			
			float h = 0.2f * Input.GetAxis("Mouse X");
			float v = 0.2f * Input.GetAxis("Mouse Y");
			
			if(h !=0 | v !=0){

                transform.localScale = (1 + (h + v)) * transform.localScale;

                if (transform.localScale.y < valorMin){
					transform.localScale = new Vector3(ratio * valorMin,valorMin,1);
				}
				if(transform.localScale.y > valorMax){
					transform.localScale = new Vector3(ratio * valorMax,valorMax,1);
				}


				//transform.position = posFija - new Vector3(0,transform.localScale.y/2, 0);


			}
		}
		
	}
// =====================================================================================================
/// Método evento ClickDown. Se ejecuta una vez cuando se hace clic abajo
/**
  Verifica si se está haciendo clic sobre una letra variable, genera una transparencia sobre la
  letra seleccionada
*/
	void  ClickDown(){
		
		RaycastHit hitt;
		
		if( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out  hitt, 100 ) ) {
			
			if(hitt.transform.gameObject.name == gameObject.name){
				highlightObject = gameObject;
                //gameObject.GetComponent<Renderer>().material.color = new Color(1f, 0.2f, 0.4f, 0.5f);
                gameObject.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f, 0.6f);
            }
            
		}
	}
// =====================================================================================================
/// Método evento ClickUp. Se ejecuta una vez cuando se termina un clic 
/**
  Cambia el cursor si antes estaba sobre una letra variable, deja de seleccionar objetos
*/
	void ClickUp(){
		
		if(clickHold){

            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            if (highlightObject){

				//highlightObject.GetComponent<Renderer>().material.color = new Color(1f, 0f, 0f);
				highlightObject.GetComponent<Renderer>().material.color = colorLetra;
				highlightObject = null;

			}
		}
	}
// =====================================================================================================
/// Método ClickEventControl. Monitorea el ratón
/**
  Llama los métodos de los eventos que genera el ratón
*/
	void ClickEventControl(){
		
		if(Input.GetKey(KeyCode.Mouse0)){
			if(clickHold){
				ClickDownHold();
			}
			else{
				ClickDown();
				clickHold = true;
			}
		}
		else{
			ClickUp();
			clickHold = false;
		}
	}
// =====================================================================================================
/// Método Update. Se ejecuta una vez cada frame
/**
  Se encarga de llamar el método que controla los eventos del ratón
*/
	void Update () {
		
		ClickEventControl();
		
	}
}
