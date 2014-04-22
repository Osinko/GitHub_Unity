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
		vectorMesh.vertices = new Vector3[]{start,end,};
		vectorMesh.uvs = new Vector2[]{Vector2.zero,Vector2.zero,};
		vectorMesh.lines = new int[]{0,1,};
		vectorMesh.color = color;
		
		with.Add(name,vectorMesh);
	}


	public void AddTriangle(string name,Vector3 a, Vector3 b, Vector3 c , Color color){
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();
		
		vectorMesh.root = gameObject;
		vectorMesh.vertices = new Vector3[]{a,b,c,};
		vectorMesh.uvs = new Vector2[]{Vector2.zero,Vector2.zero,Vector2.zero,};
		vectorMesh.lines = new int[]{0,1,1,2,2,0,};
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
		vectorMesh.lines = new int[]{0,1,1,2,2,3,3,0,};
		vectorMesh.color = color;
		
		with.Add(name,vectorMesh);
	}


	public void AddGrid(string name , Color color , float gridSize = 1.0f , int size = 8 )
	{
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();
		
		vectorMesh.root = gameObject;

		int		drawSize;
		float	width;
		int		resolution;
		float	diff;

		Vector3[] vertices;
		Vector2[] uvs;
		int[] lines;

		drawSize = size * 2;
		width = gridSize * drawSize / 4.0f;
		Vector2 startPosition = new Vector2 (-width, -width);
		Vector2 endPosition = new Vector2 (width, width);
		diff = width / drawSize;
		resolution = (drawSize + 2) * 2;	//最期の２辺を追加している

		vertices = new Vector3[resolution];
		uvs = new Vector2[resolution];
		lines = new int[resolution];

		for (int i = 0; i < vertices.Length; i += 4) {
			vertices [i] = new Vector3 (startPosition.x + (diff * (float)i), startPosition.y, 0);
			vertices [i + 1] = new Vector3 (startPosition.x + (diff * (float)i), endPosition.y, 0);
			vertices [i + 2] = new Vector3 (startPosition.x, endPosition.y - (diff * (float)i), 0);
			vertices [i + 3] = new Vector3 (endPosition.x, endPosition.y - (diff * (float)i), 0);
		}
		
		for (int i = 0; i < resolution; i++) {
			lines [i] = i;
		}

		vectorMesh.vertices = vertices;
		vectorMesh.uvs = uvs;
		vectorMesh.lines = lines;
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

