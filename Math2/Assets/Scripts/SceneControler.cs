using UnityEngine;
using System.Collections;

public class SceneControler : MonoBehaviour {

	public float Amplitude=3;	//振幅
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

	public void Awake ()
	{
		GameObject drawGraph = GameObject.Find("AnimationSinVectorMesh1") as GameObject;

		sinCosPS = gameObject.AddComponent<ParticleSystem>() as ParticleSystem;
		sinCosPS.Stop();
		sinCosPSpoints = new ParticleSystem.Particle[3];

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
