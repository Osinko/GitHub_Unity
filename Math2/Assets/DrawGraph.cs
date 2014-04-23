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


	public void AddCircle (string name, Vector3 position, float radius, int numberOfPoints, Color color,bool vectorCircleOn = true)
	{
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();
		
		vectorMesh.root = gameObject;

		Vector3[] vertices;
		Vector2[] uvs;
		int[] lines;

		Vector3 point = Vector3.up*radius;
		if(vectorCircleOn){

			float angle = -360.0f / numberOfPoints;
			vertices = new Vector3[numberOfPoints];
			uvs = new Vector2[numberOfPoints];
			lines = new int[numberOfPoints*2];
			for (int v = 0; v < vertices.Length; v++) {
				vertices[v]= Quaternion.Euler(0,0,angle*(v-1))*point;
				uvs[v]=Vector2.zero;
			}

			//連続する線のトポロジを作成
			for (int i = 0, j = 0 ; i < lines.Length; i+=2,j++) {
				lines[i]=j;
				lines[i+1]=j+1;
			}
			lines[lines.Length-1]=0;

			vectorMesh.vertices = vertices;
			vectorMesh.lines = lines;
			vectorMesh.uvs = uvs;

		}else{

			vertices = new Vector3[numberOfPoints+1];
			uvs = new Vector2[numberOfPoints+1];
			lines = new int[numberOfPoints*3];

			float angle = -360.0f / numberOfPoints;
			for (int v = 1,t = 1 ; v < vertices.Length; v++,t+=3 ) {
				vertices[v]= Quaternion.Euler(0,0,angle*(v-1))*point;
				
				lines[t] = v;        //0,1,2, 0,2,3 0,3,4 0,4,5 のようなインデックスが出来る
				lines[t+1]= v+1;
				
				uvs[v]=Vector2.zero;
			}
			lines[lines.Length-1]=1;
			vectorMesh.vertices = vertices;
			vectorMesh.lines = MakeIndices(lines);
			vectorMesh.uvs = uvs;

		}

		with.Add(name,vectorMesh);

	}


	public void AddGrid(string name , Color color , float gridSize = 1.0f , int size = 8)
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


	//メッシュのトポロジーをMeshTopology.Trianglesから2点一組のMeshTopology.Linesに変換する
	public int[] MakeIndices(int[] triangles)
	{
		int[] indices = new int[2 * triangles.Length];
		int i = 0;
		for( int t = 0; t < triangles.Length; t+=3 )
		{
			indices[i++] = triangles[t];        //start
			indices[i++] = triangles[t + 1];   //end
			indices[i++] = triangles[t + 1];   //start
			indices[i++] = triangles[t + 2];   //end
			indices[i++] = triangles[t + 2];   //start
			indices[i++] = triangles[t];        //end
		}
		return indices;
	}


	public VectorMesh Set(string name)
	{
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

