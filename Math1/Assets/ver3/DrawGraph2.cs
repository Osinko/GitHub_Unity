using UnityEngine;
using System.Collections;

using System.Collections.Generic;

class DrawGraph2
{


	//このクラス自身が複数のゲームオブジェクトメッシュを管理している
	Dictionary<string , VectorMesh> with = new Dictionary<string, VectorMesh>();

	public void AddLine (string name, Vector3 start, Vector3 end, Color color, Face face)
	{
		VectorMesh vectorMesh = new VectorMesh();

		vectorMesh.vertices = new Vector3[]{start,end,};
		vectorMesh.uvs = new vector2[]{Vector2.zero,Vector2.zero,};
		vectorMesh.lines = new int[]{0,1,1,2,};
		vectorMesh.color = color;

		with.Add(name,vectorMesh);

	}

	public void AddDrawGraph (string graph, object animMesh, Color white, object xy)
	{
		throw new System.NotImplementedException ();
	}



}

