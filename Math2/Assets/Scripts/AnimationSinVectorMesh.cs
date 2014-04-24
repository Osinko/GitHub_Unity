using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AnimationSinVectorMesh : VectorMesh {

	public float Amplitude;	//振幅
	public float speed;		//周期速度
	public float theta;		//現在のθ

	//カーブ制御用
	float 	emissionRate = 24.0f;	//１秒間に24回産む
	int 	maxParticle = 1000;
	float 	startLifeTimes= 6.28f*2.0f;
	
	LinkedList<AnimMesh> verticesList;	//グラフの描画用の各頂点をパーティクルのように扱っている

	bool pointLink;
	bool closeLink;
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
		verticesList =new LinkedList<AnimMesh>();
		emissionTimer = 1 / emissionRate;
		transform.Translate(new Vector3(5,0,0));
	}


	public override void Update ()
	{
		diffTheta = Mathf.PI * 2 /speed;
		theta += diffTheta * Time.deltaTime;
		theta %= Mathf.PI * 2;

		sinPos = Amplitude * Mathf.Sin(theta);
		cosPos = Amplitude * Mathf.Cos(theta);
		
		PerticleMethod (sinPos,cosPos);
		SinWaveUpdate (sinPos);

		base.Update ();
	}

	void SinWaveUpdate (float sinPos)
	{

		emissionTimer -= Time.deltaTime;
		if(emissionTimer<0){
			respawnVertices(verticesList);
			emissionTimer = 1 / emissionRate;
		}
		
		moveVertices(verticesList,Time.deltaTime);
		RefreshList(verticesList);
		
		if(verticesList.Count!=0){
			draw(LinkedListVerticesPosition(verticesList));
		}

	}

	//TODO
	void draw (Vector3[] vertices)
	{
		int closeLinkPoly = 0;
		if(closeLink){closeLinkPoly = 1;}
		
		uvs = new Vector2[vertices.Length];

		if(pointLink){
			//連続する線のトポロジを作成
			lines = new int[(vertices.Length - 1 + closeLinkPoly)*2];
			for (int i = 0, j = 0 ; i < (lines.Length - (closeLinkPoly*2)); i+=2,j++) {
				lines[i]=j;
				lines[i+1]=j+1;
			}
			if(closeLink){
				//終端と始点をつなぐポリゴンのトポロジを作成
				lines[lines.Length-2]=(lines.Length/2)-1;
				lines[lines.Length-1]=0;
			}
		}else{
			lines = new int[vertices.Length + closeLinkPoly*2];
			for (int i = 0; i < (lines.Length - closeLinkPoly*2); i++) {
				lines[i]=i;
			}
			if(closeLink){
				//終端と始点をつなぐポリゴンのトポロジを作成
				lines[lines.Length-2]=lines.Length - 3;
				lines[lines.Length-1]=0;
			}
		}
		
		for (int i = 0; i < vertices.Length; i++) {
			uvs[i]=Vector2.zero;
		}

		this.vertices = vertices;
		this.uvs = uvs;
		this.lines = lines;
		
		RefreshMesh();

	}

	//リストに要素を加える
	void respawnVertices (LinkedList<AnimMesh> verticesList)
	{
		if ( maxParticle<verticesList.Count){return;}
		float rad = Time.realtimeSinceStartup % (2*Mathf.PI);
		Vector3 sinRespawnPosition = new Vector3(0,sinPos,0);
		
		verticesList.AddLast(new AnimMesh(){
			deathFlag = false,
			lifeTimes = startLifeTimes,
			speed = 1,
			direction = Vector3.right,
			position = sinRespawnPosition});
	}	
	
	//値に変更を加える
	//ここでLinkedListからはコレクションを加工できないのでLinkedListNodeを利用している点に注目
	void moveVertices (LinkedList<AnimMesh> verticesList, float deltaTime)
	{
		for ( LinkedListNode<AnimMesh> node = verticesList.Last ; node != null ; node = node.Previous) {		//この行は重要なテクニック
			AnimMesh v = new AnimMesh();
			v = node.Value;
			v.position += v.direction * v.speed * deltaTime;
			v.lifeTimes = v.lifeTimes - deltaTime;
			if(v.lifeTimes<0){
				v.deathFlag = true;
			}
			node.Value = v;
		}
	}
	
	//リストから寿命の来た要素を削除
	void RefreshList (LinkedList<AnimMesh> verticesList)
	{
		var removeList = verticesList.Where(node => node.deathFlag == true).ToList();

		foreach (var item in removeList) {
			verticesList.Remove(item);
		}
	}
	
	//描画用に頂点データーのみを取り出す
	Vector3[] LinkedListVerticesPosition(LinkedList<AnimMesh> verticesList){
		if(verticesList.Count==0){return null;}
		Vector3[] meshVertices = new Vector3[verticesList.Count];
		
		int i=0;
		foreach (AnimMesh item in verticesList) {
			meshVertices[i++] = item.position;
		}
		return meshVertices;
	}
	
	//各頂点をパーティクルのように扱う為のクラス
	class AnimMesh
	{
		public bool deathFlag;
		public float lifeTimes;
		public float speed;
		public Vector3 direction;
		public Vector3 position;
	}




	void PerticleMethod (float sinPos,float cosPos)
	{
		Vector3 circlePos = new Vector3 (cosPos , sinPos , 0);
		Vector3 sinMovePos = new Vector3 (0, sinPos , 0);
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
