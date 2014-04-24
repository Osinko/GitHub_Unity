using UnityEngine;
using System.Collections;

public class AnimationSinVectorMesh : VectorMesh {

	public float Amplitude;	//振幅
	public float speed;		//周期速度
	public float theta;		//現在のθ

	//カーブ制御用
	float 	emissionRate = 24.0f;	//１秒間に24回産む
	int 	maxParticle = 1000;
	float 	startLifeTimes= 6.28f;
	
	LinkedList<Vector3> verticesList;	//グラフの描画用の各頂点をパーティクルのように扱っている

	float diffTheta;
	float sinPos;
	float cosPos;

	float emissionTimer;

	ParticleSystem sinCosPS;
	ParticleSystem.Particle[] sinCosPSpoints;

	public override void Awake ()
	{
		Amplitude=3;
		speed =5;		//５にすると５秒で１回転＿(5Hz（１秒で5回転）とは逆の動作になるので注意。5Hzにしたいなら0.2にする)
		theta =0;

		sinCosPS = gameObject.AddComponent<ParticleSystem>() as ParticleSystem;
		sinCosPS.Stop();		//パーティクルを停めてコード制御するならコレが大事
	
		sinCosPSpoints = new ParticleSystem.Particle[3];

		base.Awake ();
	}

	void Start ()
	{
		verticesList =new LinkedList<Vector3>();
		emissionTimer = 1 / emissionRate;
		
	}


	public override void Update ()
	{
		diffTheta = Mathf.PI * 2 /speed;
		theta += diffTheta * Time.deltaTime;
		theta %= Mathf.PI * 2;

		sinPos = Amplitude * Mathf.Sin(theta);
		cosPos = Amplitude * Mathf.Cos(theta);
		
		PerticleMethod (sinPos,cosPos);
		SinWave (sinPos);

		base.Update ();
	}

	void SinWave (float sinPos)
	{
		Vector3[] vertices;
		Vector2[] uvs;
		int[] lines; 


	}

	void PerticleMethod (float sinPos,float cosPos)
	{
		Vector3 circlePos = new Vector3 (cosPos , sinPos , 0);
		Vector3 sinMovePos = new Vector3 (5f, sinPos , 0);
		Vector3 cosMovePos = new Vector3 (cosPos , -5f, 0);

		sinCosPSpoints [0].position = circlePos;
		sinCosPSpoints [0].color = Color.white;
		sinCosPSpoints [0].size = 1f;
		sinCosPSpoints [1].position = sinMovePos;
		sinCosPSpoints [1].color = Color.white;
		sinCosPSpoints [1].size = 1f;
		sinCosPSpoints [2].position = cosMovePos;
		sinCosPSpoints [2].color = Color.white;
		sinCosPSpoints [2].size = 1f;

		sinCosPS.SetParticles (sinCosPSpoints, sinCosPSpoints.Length);
	}
}
