using UnityEngine;
using System.Collections;

using System.Collections.Generic;

/// <summary>
/// Vector mesh.
/// 全てのベクターメッシュの基底となるクラス
/// このクラスに登録された機能は共通機能となる
/// </summary>
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

	public bool visible;
	public bool cullBack;
	public bool colorPointOn;
	public bool pause;
	public bool LifeTimeOn;
	public float VectorMeshLifeTime;

	bool visiblePrev;
	bool colorPointOnPrev;
	bool cullBackPrev;
	Face facePrev;

	public virtual void Awake ()
	{
		mf = gameObject.AddComponent<MeshFilter>();
		mr = gameObject.AddComponent<MeshRenderer>();
		mesh = mf.mesh;
		mesh.Clear();
		
		visible = true;
		color = Color.white;
		cullBack = false;
		colorPointOn = false;
		LifeTimeOn = false;
		face = Face.xy;

		visiblePrev = visible;
		colorPointOnPrev = colorPointOn;
		cullBackPrev = cullBack;
		facePrev = face;

	}


	public virtual void Update ()
	{
		if(visible != visiblePrev){
			mr.enabled = visible;
			visiblePrev = visible;
		}

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
			for (int i = 0; i < vertices.Length; i++) {
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
			break;
		default:
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

	public void RefreshMesh ()
	{
		mesh.Clear();
		CullModeChange ();
		mesh.vertices = RotationVertices (vertices);
		mesh.uv = uvs;
		ColorModeChange ();
		mesh.SetIndices (lines, MeshTopology.Lines, 0);
	}

	//辞書クラスから適時呼び出される 
	public virtual void OnInsertComplete()
	{
		transform.parent = root.transform;		//呼び出し元のDrawGraphオブジェクトの子にする 
		//RefreshMesh ();
	}

	//辞書クラスから適時呼び出される 
	public virtual void OnRemoveComplete ()
	{
		Destroy(gameObject);
	}
}
