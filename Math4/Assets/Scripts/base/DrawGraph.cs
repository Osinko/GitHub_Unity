using UnityEngine;
using System.Collections;

using System.Collections.Generic;


/// <summary>
/// ベクター描画管理用のオブジェクト
/// verticesにその形にふさわしい形状を作成して渡している
/// また複数のベクター要素を辞書リスト構造に収め管理する
/// </summary>
public class DrawGraph : MonoBehaviour {
	
	//このクラス自身が複数のゲームオブジェクトメッシュを管理している
	public VectorMeshDictionary with = new VectorMeshDictionary();

	void Awake ()
	{
		gameObject.name="DrawGraph";
	}

	public GameObject AddLine (string name, Vector3 start, Vector3 end, Color color)
	{
		GameObject go = Instantiate(Resources.Load("LineVectorMesh")) as GameObject;
		LineVectorMesh vectorMesh = go.GetComponent<LineVectorMesh>();

		vectorMesh.SetUpMesh( name, start, end, color);
		with.Add(name,go);
		return go;
	}

	public GameObject AddTriangle(string name,Vector3 a, Vector3 b, Vector3 c , Color color)
	{
		GameObject go = Instantiate(Resources.Load("TriangleVectorMesh")) as GameObject;
		TriangleVectorMesh vectorMesh = go.GetComponent<TriangleVectorMesh>();

		vectorMesh.SetUpMesh( name, a , b , c , color);
		with.Add(name,go);
		return go;
	}

	public GameObject AddBox (string name, Rect rect, Color color)
	{
		GameObject go = Instantiate(Resources.Load("BoxVectorMesh")) as GameObject;
		BoxVectorMesh vectorMesh = go.GetComponent<BoxVectorMesh>();

		vectorMesh.SetUpMesh( name, rect , color);
		with.Add(name,go);
		return go;
	}

	public GameObject AddCircle (string name, Vector3 position, float radius, int numberOfPoints, Color color,bool vectorCircleOn = true)
	{
		GameObject go = Instantiate(Resources.Load("CircleVectorMesh")) as GameObject;
		CircleVectorMesh vectorMesh = go.GetComponent<CircleVectorMesh>();

		vectorMesh.SetUpMesh(name,position,radius,numberOfPoints,color,vectorCircleOn);
		with.Add(name,go);
		return go;
	}

	public GameObject AddGrid(string name , int column , int row , Color color, float girdSizeX = 1,float girdSizeY = 1)
	{
		GameObject go = Instantiate(Resources.Load("GridVectorMesh")) as GameObject;
		GridVectorMesh vectorMesh = go.GetComponent<GridVectorMesh>();
		
		vectorMesh.SetUpMesh(name, column , row , color, girdSizeX ,girdSizeY);
		with.Add(name,go);
		return go;
	}

	public GameObject AddVectorMeshObjcect(string name,GameObject go)
	{
		VectorMeshObjcect vectorMesh = go.GetComponent<VectorMeshObjcect>();
		
		vectorMesh.SetUpMesh();
		with.Add(name,go);
		return go;
	}

}

