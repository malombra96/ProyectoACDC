using UnityEngine;
using System.Collections;

public class Fasores : MonoBehaviour {

	public Transform fasor1;
	public Transform fasor2;
	public Transform fasor3;
	public Transform fasor4;
	
	public GameObject pointEnd1;
	public GameObject pointEnd2;
	public GameObject pointEnd3;
	public GameObject pointEnd4;
	
	private GameObject cone1;
	private GameObject cone2;
	private GameObject cone3;
	private GameObject cone4;
	
	private GameObject Cylendir1;
	private GameObject Cylendir2;
	private GameObject Cylendir3;
	private GameObject Cylendir4;
	
	public Transform point1;
	public Transform point2;
	public Transform point3;
	public Transform point4;

	public GameObject lineFasor1;
	public GameObject lineFasor2;
	public GameObject lineFasor3;
	public GameObject lineFasor4;
	
	public float omega;

	LineRenderer lineRenderer1;
	LineRenderer lineRenderer2;
	LineRenderer lineRenderer3;
	LineRenderer lineRenderer4;

	float 		tiempo 	= 0;
	
	Vector3[] 	lineData1 = new Vector3[22];
	Vector3[] 	lineData2 = new Vector3[22];
	Vector3[] 	lineData3 = new Vector3[22];
	Vector3[] 	lineData4 = new Vector3[22];

	public float magnitudFasore1;
	public float magnitudFasore2;
	public float magnitudFasore3;
	public float magnitudFasore4;

	public float startGrafica;
	//public float desfase1, desfase2;

	// Use this for initialization
	void Start () {

		lineRenderer1 = lineFasor1.AddComponent<LineRenderer>();
		lineRenderer1.material = new Material(Shader.Find("GUI/Text Shader"));
		lineRenderer1.material.color = Color.red;
		lineRenderer1.SetColors(Color.red,Color.red);
		lineRenderer1.SetWidth(0.02F, 0.02F);

		lineRenderer2 = lineFasor2.AddComponent<LineRenderer>();
		lineRenderer2.material = new Material(Shader.Find("GUI/Text Shader"));
		lineRenderer2.material.color = Color.blue;
		lineRenderer2.SetColors(Color.blue,Color.blue);
		lineRenderer2.SetWidth(0.02F, 0.02F);

		cone1 = fasor1.transform.GetChild(1).gameObject;
		cone2 = fasor2.transform.GetChild(1).gameObject;

		cone1.transform.position = pointEnd1.transform.position;
		cone2.transform.position = pointEnd2.transform.position;

		Cylendir1 = pointEnd1.transform.parent.gameObject;
		Cylendir2 = pointEnd2.transform.parent.gameObject;
		
		//desfase1 = 0;
		//desfase2 = Mathf.PI/2;

		omega = 1;

	}
//-----------------------------------------------------------------------------------------------
	void TraceLineRenderer( Vector3[] trajectory, LineRenderer line ){
		
		line.SetVertexCount(2 * trajectory.Length );
		
		for (int i = 0; i < trajectory.Length; i++) {
			line.SetPosition(i,trajectory[i]);
			//Debug.Log(""+i);
		}
		
		for (int i = trajectory.Length ; i <= 2*trajectory.Length -1 ; i++) {
			line.SetPosition(i,trajectory[2 * trajectory.Length - i - 1]);
		}
		
		cone1.transform.position = pointEnd1.transform.position;
		cone2.transform.position = pointEnd2.transform.position;

		Cylendir1.transform.localPosition = new Vector3(0,magnitudFasore1,0);
		Cylendir1.transform.localScale = new Vector3(0.03f,magnitudFasore1,0.03f);
		
		Cylendir2.transform.localPosition = new Vector3(0,magnitudFasore2,0);
		Cylendir2.transform.localScale = new Vector3(0.03f,magnitudFasore2,0.03f);
		//print("scale "+Cylendir2.transform.);

	}	
//-----------------------------------------------------------------------------------------------
	void Update () {
	

		float yPos1 = magnitudFasore1*Mathf.Sin(tiempo);
		float yPos2 = magnitudFasore2*Mathf.Sin(tiempo);
		float yPos3 = magnitudFasore3*Mathf.Sin(tiempo);
		float yPos4 = magnitudFasore4*Mathf.Sin(tiempo);
		//float yPos2 = Mathf.Sin(tiempo - desfase2);

		float xPos1 = magnitudFasore1*Mathf.Cos(tiempo);
		float xPos2 = magnitudFasore2*Mathf.Cos(tiempo);
		float xPos3 = magnitudFasore3*Mathf.Cos(tiempo);
		float xPos4 = magnitudFasore4*Mathf.Cos(tiempo);
		//float xPos2 = Mathf.Cos(tiempo - desfase2);

		tiempo += omega*Time.deltaTime;

		if (tiempo > 6.28){
			tiempo = 0;
		}
		else{

		}

		lineData1[0] = new Vector3(xPos1,yPos1,0);
		lineData2[0] = new Vector3(xPos2,yPos2,0);
		lineData3[0] = new Vector3(xPos3,yPos3,0);
		lineData4[0] = new Vector3(xPos4,yPos4,0);

		for (float k = 1; k <= 21; k++) {
			float argum = 18*(k-1)/180*Mathf.PI;
			lineData1[(int)k] = new Vector3(argum + startGrafica,-magnitudFasore1 * Mathf.Sin(argum - tiempo),0);
			lineData2[(int)k] = new Vector3(argum + 1,-magnitudFasore2 * Mathf.Cos(argum - tiempo),0);
		}
		
		//fasor1.eulerAngles = new Vector3(0,0,tiempo*180/Mathf.PI - 90);
		fasor1.eulerAngles = new Vector3(0,0,((tiempo)*180/Mathf.PI) - 90);
		fasor2.eulerAngles = new Vector3(0,0,((tiempo)*180/Mathf.PI ));
		
		point1.position = new Vector3(1,yPos1,0);
		point2.position = new Vector3(1,yPos2,0);

		TraceLineRenderer( lineData1, lineRenderer1);
		TraceLineRenderer( lineData2, lineRenderer2);

	}
}
