using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// Controla el comportamiento del panel inferior de la Interfaz gráfica de usuario
/**
	Maneja el panel inferior de la interfaz gráfica de usuario y sus contenidos, 
	contiene pestañas que presentan las \a orientaciones y \a discusiones y los créditos,
	permite mover la cámara cuando hay un movimiento en el panel
*/
public class DownPanelBehaviour : MonoBehaviour {

	/// Objeto UI del texto del panel
    public Text HelpButtonText;
	/// variable que define si el panel debe ser o no mostrado
    bool showingPanel;
	/// variable para el tiempo para que el movimiento del panel sea continuo cuando aparece o desaparece
    float tPanel;
	/// variable posición X del panel cuando está visible
    float panelPosShow;
	/// variable posición X del panel cuando está oculto
	float panelPosHide;
	/// variable posición en la cual se va a mover el panel
    float panelPosTarget;
	/// variable temporal
    RectTransform rectTrans;
	/// Objeto de Unity asociado a la cámara principal
    public Transform appCamera;
	/// variable posición Z de la cámara cuando hay movimiento del panel
    float cameraPosTarget;

    public List<GameObject>  tags;

// =====================================================================================================
/// Método Start. Se ejecuta una vez al iniciar la ejecución del programa
/**
  Inicializa las varibles y selecciona la posición inicial del panel.
*/
    void Start () {

        showingPanel = false;
        panelPosShow = 60;
        panelPosHide = -35;
        
        tPanel = 1;
        HideAndShowDPanel(true);

        rectTrans = gameObject.GetComponent<RectTransform>();

    }
// =====================================================================================================
/// Método HideAndShowDPanel. Se encarga de dar la orden de movimiento del panel 
/**
  Se encarga de dar la orden de movimiento del panel, además actualiza la información de los textos de las pestañas
*/
    public void HideAndShowDPanel(bool bTtype)
    {

        if (bTtype) {
            showingPanel = !showingPanel;
        }
        else {
            showingPanel = true;
        }
        
        if (showingPanel) {
            panelPosTarget = panelPosShow;
            cameraPosTarget = -1;
            HelpButtonText.text = "▼";
        }
        else {
            panelPosTarget = panelPosHide;
            cameraPosTarget = 0;
            HelpButtonText.text = "▲";
        }

        tPanel = 0;

        HierarchyTags(0);

    }
// =====================================================================================================
/// Método RefleshPos. Se encarga del movimiento continuo del panel 
/**
  Se encarga ejecutar el movimiento contínuo del panel cuando hay orden de ocultar o visualizar 
  y ajusta el zoom de la cámara
*/
    void RefleshPos() {

    // HideAndShowPanel
        tPanel +=  Time.deltaTime;
        Vector3 tempPos = rectTrans.anchoredPosition;
        float tempY = Mathf.Lerp(tempPos.y, panelPosTarget, 2*tPanel);
        rectTrans.anchoredPosition = new Vector3(tempPos.x, tempY, tempPos.z);

    // Cam Movement
        Vector3 tempCamPos = appCamera.position;
        float tempCamY = Mathf.Lerp(tempCamPos.y, cameraPosTarget, 0.1f*tPanel);
        appCamera.position = new Vector3(tempCamPos.x, tempCamY, tempCamPos.z);

    }
// =====================================================================================================
/// Método Update. Se ejecuta una vez cada frame
/**
  Se encarga ejecutar el movimiento contínuo del panel.
*/
    void Update () {

    // Movimiento del panel
        if (tPanel<10.05f) {
            RefleshPos();
        }

    }


    public void HierarchyTags(int index)
    {
        for (int i = 0; i < tags.Count; i++)
        {
            tags[i].SetActive(index == i);
        }
    }
}
