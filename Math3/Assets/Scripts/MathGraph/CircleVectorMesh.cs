using UnityEngine;
using System.Collections;

public class CircleVectorMesh : VectorMesh {

	public bool 	vectorCircleOn;
	public Vector3 	position;
	public float 	radius;
	public int 		numberOfPoints;

	//値変化検出用 
	bool vectorCircleOnPrev;
	Vector3 positionPrev;
	float radiusPrev;
	int numberOfPointsPrev;

	public override void Awake ()
	{
		vectorCircleOn = true;
		position = Vector3.zero;
		radius =1;
		numberOfPoints=32;

		vectorCircleOnPrev = vectorCircleOn;
		positionPrev = position;
		radiusPrev = radius;
		numberOfPointsPrev = numberOfPoints;

		base.Awake ();
	}

	public override void Update ()
	{
		if(vectorCircleOn!=vectorCircleOnPrev || position!=positionPrev || radius!=radiusPrev || numberOfPoints!=numberOfPointsPrev){
			if(radius < 0.00000001f){radius=0.00000001f;}
			if(numberOfPoints < 3){numberOfPoints = 3;}
			ReCircleVectorMesh();
		}

		base.Update ();
	}

	public void CreateCircleVectorMesh(string name, Vector3 position, float radius, int numberOfPoints, Color color, bool vectorCircleOn)
	{
		this.vectorCircleOn = vectorCircleOn;
		this.position = position;
		this.radius = radius;
		this.numberOfPoints = numberOfPoints;

		//テンプレ部分
		gameObject.name = name;	//オブジェクトネーム更新
		this.color = color;		//継承元のカラーへ代入
		transform.parent = root.transform;		//呼び出し元のDrawGraphオブジェクトの子にする 

		ReCircleVectorMesh ();
	}

	//メッシュ構造の再構築
	void ReCircleVectorMesh ()
	{

		Vector3 point = Vector3.up * radius;
		if (vectorCircleOn) {
			Vector3[] vertices = new Vector3[numberOfPoints];
			Vector2[] uvs = new Vector2[numberOfPoints];
			int[] lines = new int[numberOfPoints * 2];

			float angle = -360.0f / numberOfPoints;
			for (int v = 0; v < vertices.Length; v++) {
				vertices [v] = Quaternion.Euler (0, 0, angle * (v - 1)) * point;
				uvs [v] = Vector2.zero;
			}

			MakeTopologyCloseMesh (lines);

			this.vertices = vertices;
			this.lines = lines;
			this.uvs = uvs;
		}
		else {
			Vector3[] vertices = new Vector3[numberOfPoints + 1];
			Vector2[] uvs = new Vector2[numberOfPoints + 1];
			int[] lines = new int[numberOfPoints * 3];

			float angle = -360.0f / numberOfPoints;

			for (int v = 1, t = 1; v < vertices.Length; v++, t += 3) {
				vertices [v] = Quaternion.Euler (0, 0, angle * (v - 1)) * point;
				uvs [v] = Vector2.zero;
			}

			MakeTopologyConvertMesh (vertices,lines);

			this.vertices = vertices;
			this.lines = MakeIndices (lines);
			this.uvs = uvs;
		}

		RefreshMesh();
		
	}


}
