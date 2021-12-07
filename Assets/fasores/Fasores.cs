using UnityEngine;
using System.Collections;

public class Fasores : MonoBehaviour {

	public Transform fasor1;
	public Transform fasor2;
	public Transform point1;
	public Transform point2;

	public GameObject lineFasor1;
	public GameObject lineFasor2;
	public float omega;

	LineRenderer lineRenderer1;
	LineRenderer lineRenderer2;

	float 		tiempo 	= 0;
	Vector3[] 	lineData1 = new Vector3[22];
	Vector3[] 	lineData2 = new Vector3[22];


	float desfase1, desfase2;

	public GameObject Reference;
	public GameObject Reference1;
	public GameObject Reference2;

	private float ScaleFactor;

	public int WithLine;

	private float Angulo1;
	public float Angulo2;

	[Range(0.1f,1)]
	public float Amplitud1;
	[Range(0.1f, 1)]
	public float Amplitud2;


	// Use this for initialization
	void Start () {

		lineRenderer1 = lineFasor1.AddComponent<LineRenderer>();
		//lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		//lineRenderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
		lineRenderer1.material = new Material(Shader.Find("GUI/Text Shader"));
		lineRenderer1.material.color = Color.red;
		lineRenderer1.SetColors(Color.red,Color.red);
		lineRenderer1.SetWidth(0.02F, 0.02F);

		lineRenderer2 = lineFasor2.AddComponent<LineRenderer>();
		//lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		//lineRenderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
		lineRenderer2.material = new Material(Shader.Find("GUI/Text Shader"));
		lineRenderer2.material.color = Color.blue;
		lineRenderer2.SetColors(Color.blue,Color.blue);
		lineRenderer2.SetWidth(0.02F, 0.02F);


		Angulo1 = 0;
		Angulo2 = 90;

		desfase1 = Mathf.Deg2Rad * Angulo1;
		desfase2 = Mathf.Deg2Rad * Angulo2;

		omega = 1;

		Amplitud1 = 1;
		Amplitud2 = 1;

	}
//-----------------------------------------------------------------------------------------------
	void TraceLineRenderer( Vector3[] trajectory, LineRenderer line ){

		line.SetWidth(ScaleFactor / WithLine, ScaleFactor / WithLine);

		line.SetVertexCount(2*trajectory.Length );
		
		for (int i = 0; i < trajectory.Length; i++) {
			line.SetPosition(i,trajectory[i]);
			//Debug.Log(""+i);
		}
		
		for (int i = trajectory.Length ; i <= 2*trajectory.Length -1 ; i++) {
			line.SetPosition(i,trajectory[2*trajectory.Length - i - 1]);
		}
		
	}	
//-----------------------------------------------------------------------------------------------
	void Update () {


		desfase1 = Mathf.Deg2Rad * Angulo1;
		desfase2 = Mathf.Deg2Rad * Angulo2;
		fasor1.transform.localScale = new Vector3(fasor1.transform.localScale.x, Amplitud1, fasor1.transform.localScale.z);
		fasor2.transform.localScale = new Vector3(fasor2.transform.localScale.x, Amplitud2, fasor2.transform.localScale.z);

		float Rx = Reference.transform.position.x;
		float Ry = Reference.transform.position.y;

		float yPos1 = Amplitud1 * Mathf.Sin(tiempo + desfase1);
		float yPos2 = Amplitud2 * Mathf.Sin(tiempo + desfase2);
		

		tiempo += omega*Time.deltaTime;

		if (tiempo > 6.28){
			tiempo = 0;
		}
		else{

		}

		ScaleFactor = gameObject.transform.localScale.x ;

		lineData1[0] = new Vector3( (Reference1.transform.position.x), (Reference1.transform.position.y)  , 0);
		lineData2[0] = new Vector3( (Reference2.transform.position.x), (Reference2.transform.position.y)  , 0);

		for (float k = 1; k <= 21; k++) {
			float argum = 18*(k-1)/180*Mathf.PI;
			
			lineData1[(int)k] = new Vector3( (argum * ScaleFactor + Rx)  , (-Amplitud1 * Mathf.Sin(argum- (tiempo + desfase1)) * ScaleFactor + Ry) , 0);
			lineData2[(int)k] = new Vector3( (argum * ScaleFactor + Rx)  , (-Amplitud2 * Mathf.Sin(argum- (tiempo + desfase2)) * ScaleFactor + Ry) , 0);
		}


		fasor1.eulerAngles = new Vector3(0, 0, (tiempo + desfase1) * Mathf.Rad2Deg - 90);
		fasor2.eulerAngles = new Vector3(0,0,(tiempo + desfase2) * Mathf.Rad2Deg -90);
		
		point1.position = 	 new Vector3(Rx, (yPos1 * ScaleFactor + Ry)  ,0);
		point2.position = 	 new Vector3(Rx, (yPos2 * ScaleFactor + Ry)  ,0);

		TraceLineRenderer( lineData1, lineRenderer1);
		TraceLineRenderer( lineData2, lineRenderer2);

	}
}
