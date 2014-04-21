using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class DrawGraph : MonoBehaviour {

	//表示面の方向
	public enum Face{
		xy,
		zx,
		yz,
	};

	Mesh mesh;

	//Start()では間に合わないので注意
	void Awake () {
		GetComponent<MeshFilter>().mesh = mesh = new Mesh();
		mesh.Clear();
	}

	Vector2[] uvs;
	int[] lines;
	Color[] pointColor;

	public void MeshWireDraw(Mesh baseMesh,Color color,bool back = true){
		mesh.Clear();
		mesh.name ="MeshWire";

		if(back){
			GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
		}else{
			GetComponent<MeshRenderer>().material = new Material(Shader.Find("GUI/Text Shader"));
		}

		Vector3[] vertices = baseMesh.vertices;
		int[] triangles = baseMesh.triangles; 
		int[] lines;
		Vector2[] uvs;
		Color[] pointColor;

		uvs = new Vector2[vertices.Length];
		pointColor = new Color[vertices.Length];

		lines = MakeIndices(triangles);
		for (int i = 0; i < vertices.Length; i++) {
			uvs[i]=Vector2.zero;
			pointColor[i] = color;
		}

		mesh.vertices = vertices;
		mesh.uv = uvs;
		mesh.colors = pointColor;
		mesh.SetIndices (lines, MeshTopology.Lines, 0);
	}

	/// <summary>
	/// グラフを描く関数です
	/// </summary>
	/// <param name="vertices">頂点データ</param>
	/// <param name="color">カラー</param>
	/// <param name="face">表示面</param>
	/// <param name="pointLink">If set to <c>true</c> falseにすると形を細かく刻んだ頂点配列の場合、破線風になる</param>
	/// <param name="closeLink">If set to <c>true</c> 線を閉じる指定</param>
	public void draw (Vector3[] vertices, Color color, Face face,bool back = true, bool pointLink = true, bool closeLink = false)
		{
		mesh.Clear();
		mesh.name ="GraphMesh";
		
		int closeLinkPoly = 0;
		if(closeLink){closeLinkPoly = 1;}

		uvs = new Vector2[vertices.Length];
		pointColor = new Color[vertices.Length];

		if(back){
			GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
		}else{
			GetComponent<MeshRenderer>().material = new Material(Shader.Find("GUI/Text Shader"));
		}

		if(pointLink){
			//連続する線のトポロジを作成
			lines = new int[(vertices.Length - 1 + closeLinkPoly)*2];
			for (int i = 0, j = 0 ; i < (lines.Length - (closeLinkPoly*2)); i+=2,j++) {
				lines[i]=j;
				lines[i+1]=j+1;
			}
			if(closeLink){
				//終端と始点をつなぐポリゴンのトポロジを作成
				lines[lines.Length-2]=(lines.Length/2)-1;
				lines[lines.Length-1]=0;
			}
		}else{
			lines = new int[vertices.Length + closeLinkPoly*2];
			for (int i = 0; i < (lines.Length - closeLinkPoly*2); i++) {
				lines[i]=i;
			}
			if(closeLink){
				//終端と始点をつなぐポリゴンのトポロジを作成
				lines[lines.Length-2]=lines.Length - 3;
				lines[lines.Length-1]=0;
			}
		}

		for (int i = 0; i < vertices.Length; i++) {
			uvs[i]=Vector2.zero;
			pointColor[i]=color;
		}

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
		break;		default:
			rotDirection = Vector3.forward;
			break;
		}
		
		mesh.vertices = RotationVertices(vertices,rotDirection);
		mesh.uv = uvs;
		mesh.colors = pointColor;
		mesh.SetIndices (lines, MeshTopology.Lines, 0);

	}

	//頂点配列データーをすべて指定の方向へ回転移動させる
	Vector3[] RotationVertices(Vector3[] vertices,Vector3 rotDirection){
		Vector3[] ret= new Vector3[vertices.Length];
		for (int i = 0; i < vertices.Length; i++) {
			ret[i] = Quaternion.LookRotation(rotDirection) * vertices[i];
		}
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

}
