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

		diffTheta = Mathf.PI * 2 /speed;
		theta += diffTheta * Time.deltaTime;
		theta %= Mathf.PI * 2;

		sinPos = Amplitude * Mathf.Sin(theta);
		cosPos = Amplitude * Mathf.Cos(theta);

		text_theta.text= string.Format("θ= {0,4}°",(int)(Mathf.Rad2Deg * theta));
		text_amplitude.text= string.Format("振幅= {0,9}",Amplitude);
		text_speed.text= string.Format("周期= {0,9}/1毎秒",speed);

		PerticleUpdate (sinPos,cosPos);
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
