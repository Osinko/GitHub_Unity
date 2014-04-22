using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class DrawGraph : MonoBehaviour {
	
	//このクラス自身が複数のゲームオブジェクトメッシュを管理している
	VectorMeshDictionary with = new VectorMeshDictionary();

	public void AddLine (string name, Vector3 start, Vector3 end, Color color)
	{
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();

		vectorMesh.root = gameObject;
		vectorMesh.vertices = new Vector3[]{start,end,};
		vectorMesh.uvs = new Vector2[]{Vector2.zero,Vector2.zero,};
		vectorMesh.lines = new int[]{0,1,};
		vectorMesh.color = color;
		
		with.Add(name,vectorMesh);
	}
	
	
	public void AddBox (string name, Rect rect, Color color)
	{
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();

		vectorMesh.root = gameObject;
		vectorMesh.vertices = new Vector3[]{
			new Vector3(rect.xMin,rect.yMin,0),
			new Vector3(rect.xMax,rect.yMin,0),
			new Vector3(rect.xMax,rect.yMax,0),
			new Vector3(rect.xMin,rect.yMax,0),
		};
		
		vectorMesh.uvs = new Vector2[]{Vector2.zero,Vector2.zero,Vector2.zero,Vector2.zero,};
		vectorMesh.lines = new int[]{0,1,1,2,2,3,3,0};
		vectorMesh.color = color;
		
		with.Add(name,vectorMesh);
	}


	public VectorMesh Set(string name)
	{
		return with[name];
	}

	public void Remove(string name)
	{
		with.Remove(name);
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

