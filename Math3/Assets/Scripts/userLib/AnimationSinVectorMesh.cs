using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AnimationSinVectorMesh : VectorMesh {

	//グラフ描画用の各頂点をパーティクルのように扱う為のリストクラス
	LinkedList<AnimMesh> verticesList;
	SceneControler sc;

	public enum Curve{
		sin,
		cos
	};

	//カーブ制御
	public float 	emissionRate = 		24.0f;		//１秒間に24回産む
	public float 	startLifeTimes =	6.28f*2.0f;
	public int	 	maxParticle = 		1000;
	public Curve	curve;  
	
	bool pointLink;
	bool closeLink;
	float emissionTimer;

	public override void Awake ()
	{
		curve = Curve.sin;
		GameObject controler = GameObject.Find("Level");
		sc = controler.GetComponent<SceneControler>() as SceneControler;
		
		base.Awake ();
	}

	void Start ()
	{
		verticesList =new LinkedList<AnimMesh>();
		emissionTimer = 1 / emissionRate;
	}

	public override void Update ()
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
		base.Update ();
	}


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
		Vector3 RespawnPosition;
		if ( maxParticle<verticesList.Count){return;}

		switch (curve) {
		case Curve.sin:
			RespawnPosition = new Vector3(0,sc.sinPos,0);
			verticesList.AddLast(new AnimMesh(){
				deathFlag = false,
				lifeTimes = startLifeTimes,
				speed = 1,
				direction = Vector3.right,
				position = RespawnPosition});
			break;
		case Curve.cos:
			RespawnPosition = new Vector3(sc.cosPos,0,0);
			verticesList.AddLast(new AnimMesh(){
				deathFlag = false,
				lifeTimes = startLifeTimes,
				speed = 1,
				direction = Vector3.down,
				position = RespawnPosition});
			break;
		default:
				break;
		}
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




}
