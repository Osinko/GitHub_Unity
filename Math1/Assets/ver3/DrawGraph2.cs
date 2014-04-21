using UnityEngine;
using System.Collections;

using System.Collections.Generic;

class DrawGraph2 :MonoBehaviour
{

	//このクラス自身が複数のゲームオブジェクトメッシュを管理している
	Dictionary<string , VectorMesh> with = new Dictionary<string, VectorMesh>();

	public void AddLine (string name, Vector3 start, Vector3 end, Color color)
	{
		//VectorMesh vectorMesh = new VectorMesh();
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();

		vectorMesh.vertices = new Vector3[]{start,end,};
		vectorMesh.uvs = new Vector2[]{Vector2.zero,Vector2.zero,};
		vectorMesh.lines = new int[]{0,1,1,2,};
		vectorMesh.color = color;
		vectorMesh.VectorMeshCreate();

		with.Add(name,vectorMesh);
	}


	public void AddBox (string name, Rect rect, Color color)
	{
		//VectorMesh vectorMesh = new VectorMesh();
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();
		
		vectorMesh.vertices = new Vector3[]{
			new Vector3(rect.xMin,rect.yMin,0),
			new Vector3(rect.xMax,rect.yMin,0),
			new Vector3(rect.xMax,rect.yMax,0),
			new Vector3(rect.xMin,rect.yMax,0),
			};

		vectorMesh.uvs = new Vector2[]{Vector2.zero,Vector2.zero,Vector2.zero,Vector2.zero,};
		vectorMesh.lines = new int[]{0,1,1,2,2,3,3,4};
		vectorMesh.color = color;
		
		with.Add(name,vectorMesh);
	}


//	public void AddDrawGraph (string graph, object animMesh, Color white, object xy)
//	{
//		VectorMesh vectorMesh = new VectorMesh();
//		
//		vectorMesh.vertices = new Vector3[]{start,end,};
//		vectorMesh.uvs = new vector2[]{Vector2.zero,Vector2.zero,};
//		vectorMesh.lines = new int[]{0,1,1,2,};
//		vectorMesh.color = color;
//		
//		with.Add(name,vectorMesh);
//	}



}

