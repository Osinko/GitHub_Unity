using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class VectorMesh : MonoBehaviour {

	MeshFilter mf;
	MeshRenderer mr;
	Mesh mesh;
	
	//表示面の方向　
	public enum Face{
		xy,
		zx,
		yz,
	};

	public GameObject	root;
	public Vector3[]	vertices;
	public Vector2[]	uvs;
	public Color[] 		colorPoint;
	public Color		color;
	public Face			face;
	public int[]		lines;

	public bool cullBack;
	public bool colorPointOn;
	public bool pause;
	public bool LifeTimeOn;
	public float VectorMeshLifeTime;

	public virtual void Awake ()
	{
		mf = gameObject.AddComponent<MeshFilter>();
		mr = gameObject.AddComponent<MeshRenderer>();
		mesh = mf.mesh;
		mesh.Clear();

		color = Color.white;
		cullBack = false;
		colorPointOn = false;
		LifeTimeOn = false;
		face = Face.xy;
	}

	public virtual void Update ()
	{
		if(pause || vertices == null){return;}

		if(LifeTimeOn){
			VectorMeshLifeTime -= Time.deltaTime;
			if(VectorMeshLifeTime<0){Destroy(gameObject);}
		}
	}

	public virtual void OnInsertComplete(){

		//呼び出し元のDrawGraphオブジェクトの子にする
		transform.parent = root.transform;
		
		if(cullBack){
			mr.material = new Material(Shader.Find("Sprites/Default"));
		}else{
			mr.material = new Material(Shader.Find("GUI/Text Shader"));
		}

		mesh.vertices = vertices;
		mesh.uv = uvs;

		Color[] setColor;
		setColor = new Color[vertices.Length];
		if(colorPointOn){
			setColor = colorPoint;
		}else{
			for (int i = 0; i < setColor.Length; i++) {
				setColor[i] = color;
			}
		}
		mesh.colors =setColor;

		mesh.SetIndices(lines,MeshTopology.Lines,0);
	}


	public void OnRemoveComplete ()
	{
		Destroy(gameObject);
	}
}
