using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;
using System.Linq;

public class stock : MonoBehaviour {
	
	float emissionRate = 24.0f;	//１秒間に24回産む
	int maxParticle = 1000;
	float startLifeTimes= 6.28f;
	
	float emissionTimer;
	
	LinkedList<Vertices> vertices;	//グラフの描画用の各頂点をパーティクルのように扱っている
	DrawGraph dg;
	
	void Start () {
		vertices =new LinkedList<Vertices>();
		dg = (Instantiate(Resources.Load("DrawGraph")) as GameObject).GetComponent<DrawGraph>();
		
		emissionTimer = 1 / emissionRate;
	}
	
	void Update () {
		emissionTimer -= Time.deltaTime;
		if(emissionTimer<0){
			respawnVertices(vertices);
			emissionTimer = 1 / emissionRate;
		}
		
		moveVertices(vertices,Time.deltaTime);
		refreshVertices(vertices);
		
		if(vertices.Count!=0){
			dg.draw(LinkedListVerticesPosition(vertices),Color.white,DrawGraph.Face.xy);
		}
	}
	
	//リストに要素を加える
	void respawnVertices (LinkedList<Vertices> vertices)
	{
		if ( maxParticle<vertices.Count){return;}
		float rad = Time.realtimeSinceStartup % (2*Mathf.PI);
		Vector3 sinRespawnPosition = new Vector3(0,Mathf.Sin(rad),0);
		
		vertices.AddLast(new Vertices(){
			deathFlag = false,
			lifeTimes = startLifeTimes,
			speed = 1,
			direction = Vector3.right,
			position = sinRespawnPosition});
	}	
	
	//値に変更を加える
	//ここでLinkedListからはコレクションを加工できないのでLinkedListNodeを利用している点に注目
	void moveVertices (LinkedList<Vertices> vertices, float deltaTime)
	{
		for ( LinkedListNode<Vertices> node = vertices.Last ; node != null ; node = node.Previous) {		//この行は重要なテクニック
			Vertices v = new Vertices();
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
	void refreshVertices (LinkedList<Vertices> vertices)
	{
		var removeList = vertices.Where(node => node.deathFlag == true).ToList();
		
		foreach (var item in removeList) {
			vertices.Remove(item);
		}
	}
	
	//描画用に頂点データーのみを取り出す
	Vector3[] LinkedListVerticesPosition(LinkedList<Vertices> vertices){
		if(vertices.Count==0){return null;}
		Vector3[] meshVertices = new Vector3[vertices.Count];
		
		int i=0;
		foreach (Vertices item in vertices) {
			meshVertices[i++] = item.position;
		}
		return meshVertices;
	}
	
	//各頂点をパーティクルのように扱う為のクラス
	class Vertices
	{
		public bool deathFlag;
		public float lifeTimes;
		public float speed;
		public Vector3 direction;
		public Vector3 position;
	}
}
