using UnityEngine;
using System.Collections;

using System.Collections.Generic;


public class VectorMesh : MonoBehaviour {

	MeshFilter mf;
	Mesh mesh;
	
	//表示面の方向　
	public enum Face{
		xy,
		zx,
		yz,
	};

	public Vector3[]	vertices;
	public Vector2[]	uvs;
	public Color[] 		colorPoint;
	public Color		color;
	public Face			face;
	public int[]		lines;

	bool colorPointOn;
	bool pause;
	bool LifeTimeOn;
	float VectorMeshLifeTime;

	public virtual void Awake ()
	{
		mf = gameObject.AddComponent<MeshFilter>();
		mesh = mf.mesh;
		mesh.Clear();
		color = Color.white;
		colorPointOn = false;
		LifeTimeOn = false;
		face = Face.xy;
	}

	public virtual void Update ()
	{
		if(pause || vertices!=null){return;}

		if(LifeTimeOn){
			VectorMeshLifeTime -= Time.deltaTime;
			if(VectorMeshLifeTime<0){Destroy(gameObject);}
		}
	}

	public void VectorMeshCreate ()
	{
	}

}
