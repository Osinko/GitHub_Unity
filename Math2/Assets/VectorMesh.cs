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

	bool colorPointOnPrev;
	bool cullBackPrev;
	Face facePrev;

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

		colorPointOnPrev = colorPointOn;
		cullBackPrev = cullBack;
		facePrev = face;

	}

	public virtual void Update ()
	{
		if(pause || vertices == null){return;}

		if(colorPointOn!=colorPointOnPrev){
			ColorModeChange();
			mesh.SetIndices(lines,MeshTopology.Lines,0);
		}

		if(cullBack != cullBackPrev){
			CullModeChange();
			mesh.SetIndices(lines,MeshTopology.Lines,0);
		}

		if(face != facePrev){
			mesh.vertices = RotationVertices(vertices);
			mesh.SetIndices(lines,MeshTopology.Lines,0);
		}

		if(LifeTimeOn){
			VectorMeshLifeTime -= Time.deltaTime;
			if(VectorMeshLifeTime<0){Destroy(gameObject);}
		}

	}

	public virtual void OnInsertComplete(){

		//呼び出し元のDrawGraphオブジェクトの子にする
		transform.parent = root.transform;
		
		CullModeChange ();

		mesh.vertices = RotationVertices(vertices);
		mesh.uv = uvs;

		ColorModeChange ();

		mesh.SetIndices(lines,MeshTopology.Lines,0);
	}

	void CullModeChange ()
	{
		if (cullBack) {
			mr.material = new Material (Shader.Find ("Sprites/Default"));
		}
		else {
			mr.material = new Material (Shader.Find ("GUI/Text Shader"));
		}

		cullBackPrev = cullBack;
	}

	void ColorModeChange ()
	{
		Color[] setColor;
		setColor = new Color[vertices.Length];
		if (colorPointOn) {
			setColor = colorPoint;
		}
		else {
			for (int i = 0; i < setColor.Length; i++) {
				setColor [i] = color;
			}
		}
		mesh.colors = setColor;
		colorPointOnPrev = colorPointOn;
	}
	
	//頂点配列データーをすべて指定の方向へ回転移動させる
	Vector3[] RotationVertices(Vector3[] vertices){

		Vector3 rotDirection;

		switch (face) {
		case Face.xy:
			rotDirection = Vector3.forward;
			break;
		case Face.zx:
			rotDirection = Vector3.up;
			break;
		case Face.yz:
			rotDirection = Vector3.right;
		break;        default:
			rotDirection = Vector3.forward;
			break;
		}

		Vector3[] ret= new Vector3[vertices.Length];
		for (int i = 0; i < vertices.Length; i++) {
			ret[i] = Quaternion.LookRotation(rotDirection) * vertices[i];
		}

		facePrev = face;

		return ret;
	}

	public void OnRemoveComplete ()
	{
		Destroy(gameObject);
	}
}
