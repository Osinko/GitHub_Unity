using UnityEngine;
using System.Collections;

public class SceneControler : MonoBehaviour {

	[Range(0.2f,5.0f)]
	public float Amplitude=3;	//振幅
	[Range(0.3f,10.0f)]
	public float speed=5;		//周期速度
	public float sinPos;
	public float cosPos;

	float theta;		//現在のθ
	float diffTheta;

	public GameObject sinCurvePosition;
	public GameObject cosCurvePosition;
	public GameObject circlePosition;

	ParticleSystem sinCosPS;
	ParticleSystem.Particle[] sinCosPSpoints;

	public GameObject drawGraphSin;
	public GameObject drawGraphCos;

	public Material pirticleMat;

	Vector3 camStartPosition;
	Quaternion camStartRotation;

	public void Awake ()
	{
		drawGraphSin = GameObject.Find("AnimationSinVectorMesh1") as GameObject;
		AnimationSinVectorMesh aSin = drawGraphSin.GetComponent<AnimationSinVectorMesh>() as AnimationSinVectorMesh;

		drawGraphCos = GameObject.Find("AnimationSinVectorMesh2") as GameObject;
		AnimationSinVectorMesh aCos = drawGraphSin.GetComponent<AnimationSinVectorMesh>() as AnimationSinVectorMesh;

		aSin.curve = AnimationSinVectorMesh.Curve.sin;
		aCos.curve = AnimationSinVectorMesh.Curve.cos;

		drawGraphSin.transform.position = cosCurvePosition.transform.position;
		drawGraphCos.transform.position = sinCurvePosition.transform.position;


		sinCosPS = gameObject.GetComponent<ParticleSystem>() as ParticleSystem;
		sinCosPS.Stop();
		sinCosPSpoints = new ParticleSystem.Particle[3];

		GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
		camStartPosition = cam.transform.position;
		camStartRotation = cam.transform.rotation;

		AwakeGetSceneTextMesh();

	}

	TextMesh text_amplitude;
	TextMesh text_speed;
	TextMesh text_theta;
	TextMesh text_sin;
	TextMesh text_cos;

	void AwakeGetSceneTextMesh ()
	{
		text_amplitude = GameObject.Find("text_amplitude").GetComponent<TextMesh>();
		text_speed = GameObject.Find("text_speed").GetComponent<TextMesh>();
		text_theta = GameObject.Find("text_theta").GetComponent<TextMesh>();
		text_sin = GameObject.Find("text_sin").GetComponent<TextMesh>();
		text_cos = GameObject.Find("text_cos").GetComponent<TextMesh>();
	}

	void Start ()
	{
		ParticleStart ();
	}
	
	void Update () {
		SceneJoyControl();
		
		diffTheta = Mathf.PI * 2 /speed;
		theta += diffTheta * Time.deltaTime;
		theta %= Mathf.PI * 2;

		sinPos = Amplitude * Mathf.Sin(theta);
		cosPos = Amplitude * Mathf.Cos(theta);

		text_theta.text= string.Format("θ= {0,4}°",(int)(Mathf.Rad2Deg * theta));
		text_amplitude.text= string.Format("振幅= {0,9}",Amplitude);
		text_speed.text= string.Format("周期= {0,9}/1毎秒",speed);
		text_sin.text= string.Format("Sinθ={0:f4}",sinPos);
		text_cos.text= string.Format("Cosθ={0:f4}",cosPos);

		PerticleUpdate (sinPos,cosPos);
	}

	Vector3 stick;
	float pointLinkTimer;
	float mouseOrbitTimer;

	void SceneJoyControl ()
	{
		stick = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		speed += stick.x*0.1f;
		Amplitude += stick.y*0.05f;
		speed = Mathf.Clamp(speed,0.3f,10.0f);
		Amplitude = Mathf.Clamp(Amplitude,0.2f,5.0f);
		if( Input.GetButton("Fire1")){
			Amplitude=3f;
			speed=5f;
			GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
			cam.transform.position = camStartPosition;
			cam.transform.rotation = camStartRotation;
		}

		if( pointLinkTimer<0 && Input.GetButton("Fire2")){
			pointLinkTimer=0.5f;
			drawGraphCos.GetComponent<AnimationSinVectorMesh>().pointLink = !drawGraphCos.GetComponent<AnimationSinVectorMesh>().pointLink;
			drawGraphSin.GetComponent<AnimationSinVectorMesh>().pointLink = !drawGraphSin.GetComponent<AnimationSinVectorMesh>().pointLink;
		}
		pointLinkTimer -= Time.deltaTime;

		if( mouseOrbitTimer<0 && Input.GetMouseButton(1)){
			mouseOrbitTimer=0.5f;
			MouseOrbitImproved mo = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MouseOrbitImproved>();
			mo.enabled = !mo.enabled;
		}
		mouseOrbitTimer -= Time.deltaTime;
	}

	void ParticleStart ()
	{
		sinCosPSpoints [0].color = Color.white;
		sinCosPSpoints [0].size = 1f;
		sinCosPSpoints [1].color = Color.white;
		sinCosPSpoints [1].size = 1f;
		sinCosPSpoints [2].color = Color.white;
		sinCosPSpoints [2].size = 1f;
	}
	
	void PerticleUpdate (float sinPos,float cosPos)
	{
		Vector3 circlePos = new Vector3 (cosPos , sinPos , 0) + circlePosition.transform.position;
		Vector3 sinMovePos = new Vector3 (0, sinPos , 0) + sinCurvePosition.transform.position;
		Vector3 cosMovePos = new Vector3 (cosPos , 0, 0)+ cosCurvePosition.transform.position;
		
		sinCosPSpoints [0].position = circlePos;
		sinCosPSpoints [1].position = sinMovePos;
		sinCosPSpoints [2].position = cosMovePos;
		
		sinCosPS.SetParticles (sinCosPSpoints, sinCosPSpoints.Length);
	}
}
