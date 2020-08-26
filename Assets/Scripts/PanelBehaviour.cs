using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// Controla el comportamiento del panel izquierdo de la Interfaz gráfica de usuario
/**
	Maneja el panel izquierdo de la interfaz gráfica de usuario y sus contenidos, 
	contiene el menú que activa o desactiva cada Circuito y sus componentes, permite 
	mover la cámara cuando hay un movimiento en el panel
*/
public class PanelBehaviour : MonoBehaviour {


	/// Objeto UI de unity para el desplazamiento del menú de circuitos
    public  ScrollRect myScrollRect;
	/// variable para la dirección de desplazamiento del menú de circuitos
    float scrollDir;
	/// Texto de la pestaña del panel izquierdo
    public Text ShowHideText;
	/// variable que define si el panel debe ser o no mostrado
    bool  showingPanel;
	/// variable para el tiempo para que el movimiento del panel sea continuo cuando aparece o desaparece
    float tPanel;
	/// variable posición X del panel cuando está oculto
    float panelXleft;
	/// variable posición X del panel cuando está visible
	float panelXrigth;
	/// variable posición X en la cual se va a mover el panel
    float panelXTarget;
	/// Objeto de Unity asociado a la cámara principal
    public Transform appCamera;
	/// variable posición Z de la cámara cuando hay movimiento del panel
    float cameraPosTarget;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles y selecciona la posición inicial del panel.
*/
    void Start () {

        scrollDir = 0;

        showingPanel = false;
        panelXleft = -58;
        panelXrigth = 65;
        panelXTarget = panelXrigth;
        tPanel = 1;

        HideAndShowPanel();

    }
// =====================================================================================================
/// Método Scrolling. apoya el movimiento del menú cuando se utiliza la rueda del ratón
/**
  \param direction dirección del desplazamiento del menú
*/
    public void Scrolling(float direction) {

        scrollDir = 0.5f * direction;
        
    }
// =====================================================================================================
/// Método HideAndShowPanel. Se encarga de dar la orden de movimiento del panel 
/**
  Se encarga de dar la orden de movimiento del panel, además actualiza la información de los textos de las pestañas
*/
    public void HideAndShowPanel() {

        showingPanel = !showingPanel;

        if (showingPanel) {
            panelXTarget = panelXrigth;
            cameraPosTarget = -1;
            ShowHideText.text = "▲";
        }
        else {
            panelXTarget = panelXleft;
            cameraPosTarget = 0;
            ShowHideText.text = "Menú";
        }
        tPanel = 0;

    }
// =====================================================================================================
/// Método RefleshPos. Se encarga del movimiento continuo del panel 
/**
  Se encarga ejecutar el movimiento contínuo del panel cuando hay orden de ocultar o visualizar, el 
  desplazamiento del contenido del menú y ajusta el zoom de la cámara
*/
    void RefleshPos() {

    // Panel Scrolling
        if (scrollDir != 0) {
            myScrollRect.verticalNormalizedPosition += scrollDir * Time.deltaTime;
        }

    // HideAndShowPanel
        tPanel += Time.deltaTime;
        Vector3 tempPos = transform.position;
        float tempX = Mathf.Lerp(tempPos.x, panelXTarget, 2*tPanel);
        transform.position = new Vector3(tempX, tempPos.y, tempPos.z);

    // Cam Movement
        Vector3 tempCamPos = appCamera.position;
        float tempCamX = Mathf.Lerp(tempCamPos.x, cameraPosTarget, 0.1f*tPanel);
        appCamera.position = new Vector3(tempCamX, tempCamPos.y, tempCamPos.z);

    }
// =====================================================================================================
/// Método Update. Se ejecuta una vez cada frame
/**
  Se encarga ejecutar el movimiento contínuo del panel.
*/
    void Update () {

    // Movimiento del panel
            RefleshPos();

    }
}
