using UnityEngine;
using System.Collections;

using System.Collections.Generic;


/// <summary>
/// ベクター描画用のオブジェクト
/// verticesにその形にふさわしい形状を作成して渡している
/// また複数のベクター要素を辞書リスト構造に収め管理する
/// </summary>
public class DrawGraph : MonoBehaviour {
	
	//このクラス自身が複数のゲームオブジェクトメッシュを管理している
	public VectorMeshDictionary with = new VectorMeshDictionary();

	public void AddLine (string name, Vector3 start, Vector3 end, Color color)
	{
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();
		vectorMesh.root = gameObject;

		//テンプレ部分
		vectorMesh.gameObject.name = name;	//オブジェクトネーム更新
		vectorMesh.color = color;		//継承元のカラーへ代入

		vectorMesh.vertices = new Vector3[]{start,end,};
		vectorMesh.uvs = new Vector2[]{Vector2.zero,Vector2.zero,};
		vectorMesh.lines = new int[]{0,1,};
		vectorMesh.color = color;

		vectorMesh.RefreshMesh();
		with.Add(name,vectorMesh);
	}


	public void AddTriangle(string name,Vector3 a, Vector3 b, Vector3 c , Color color){
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();
		vectorMesh.root = gameObject;

		//テンプレ部分
		vectorMesh.gameObject.name = name;	//オブジェクトネーム更新
		vectorMesh.color = color;		//継承元のカラーへ代入

		vectorMesh.vertices = new Vector3[]{a,b,c,};
		vectorMesh.uvs = new Vector2[]{Vector2.zero,Vector2.zero,Vector2.zero,};
		vectorMesh.lines = new int[]{0,1,1,2,2,0,};
		vectorMesh.color = color;

		vectorMesh.RefreshMesh();
		with.Add(name,vectorMesh);
	}


	public void AddBox (string name, Rect rect, Color color)
	{
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();
		vectorMesh.root = gameObject;

		//テンプレ部分
		vectorMesh.gameObject.name = name;	//オブジェクトネーム更新
		vectorMesh.color = color;		//継承元のカラーへ代入

		vectorMesh.vertices = new Vector3[]{
			new Vector3(rect.xMin,rect.yMin,0),
			new Vector3(rect.xMax,rect.yMin,0),
			new Vector3(rect.xMax,rect.yMax,0),
			new Vector3(rect.xMin,rect.yMax,0),
		};
		
		vectorMesh.uvs = new Vector2[]{Vector2.zero,Vector2.zero,Vector2.zero,Vector2.zero,};
		vectorMesh.lines = new int[]{0,1,1,2,2,3,3,0,};
		vectorMesh.color = color;
		
		vectorMesh.RefreshMesh();
		with.Add(name,vectorMesh);
	}


	public CircleVectorMesh AddCircle (string name, Vector3 position, float radius, int numberOfPoints, Color color,bool vectorCircleOn = true)
	{
		GameObject go = Instantiate(Resources.Load("CircleVectorMesh")) as GameObject;
		CircleVectorMesh vectorMesh = go.GetComponent<CircleVectorMesh>();
		vectorMesh.root = gameObject;

		vectorMesh.CreateCircleVectorMesh(name,position,radius,numberOfPoints,color,vectorCircleOn);
		with.Add(name,vectorMesh);
		return vectorMesh;

	}

	public GridVectorMesh AddGrid(string name , int column , int row , Color color, float girdSizeX = 1,float girdSizeY = 1)
	{
		GameObject go = Instantiate(Resources.Load("GridVectorMesh")) as GameObject;
		GridVectorMesh vectorMesh = go.GetComponent<GridVectorMesh>();
		vectorMesh.root = gameObject;
		
		vectorMesh.CreateGridVectorMesh(name, column , row , color, girdSizeX ,girdSizeY);
		with.Add(name,vectorMesh);
		return vectorMesh;
	}

	public void AddVectorMeshObjcect(string name,VectorMesh obj)
	{
		obj.root = gameObject;
		with.Add (name,obj);
	}

	//以下辞書操作用関数---------------------------------------------------

	
	public VectorMesh Set(string name){
		return with[name];
	}

	public void Remove(string name)
	{
		with.Remove(name);
	}

	public void Clear(){
		string[] removeList = new string[with.Count];
		int i=0;

		//DictionaryBase内の辞書データーの名前やキーを取り出す際はDictionaryEntryクラスを利用する
		foreach (DictionaryEntry item in with) {
			removeList[i] = (string)item.Key;
			i++;
		}
		//foreachは基本的に非破壊操作なのでこうする必要がある
		for (int j = 0; j < removeList.Length; j++) {
			with.Remove(removeList[j]);
		}
	}

	public void DebugPrint(){
		string[] KeyList = new string[with.Count];
		string[] valueList = new string[with.Count];
		
		//DictionaryBase内の辞書データーの名前やキーを取り出す際はDictionaryEntryクラスを利用する
		int i=0;
		foreach (DictionaryEntry item in with) {
			KeyList[i] = (string)item.Key;
			valueList[i] = item.Value.ToString();
				i++;
		}

		foreach (var item in KeyList) {
			print(item);
		}

		foreach (var item in valueList) {
			print(item);
		}
	}

}

