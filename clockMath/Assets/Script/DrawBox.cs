using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class DrawBox : MonoBehaviour
{
		Rect boxRect;
		Color BoxColor;

		bool death;

		Mesh mesh;
		Vector3[]	vertices;
		Vector2[]	uv;
		Color[]	colors;
		int[]	lines;

		/// <summary>
		/// 他のゲームオブジェクトから利用する際
		/// この関数を使って矩形を書き換える
		/// </summary>
		/// <param name="rect">Rect.</param>
		/// <param name="color">Color.</param>
		public void SetBox (Rect rect, Color color)
		{
				boxRect = rect;
				BoxColor = color;
				RefreshPolyBox (boxRect, BoxColor);
		}
		public void VisibleOn ()
		{
				MeshRenderer meshR = GetComponent<MeshRenderer> ();
				meshR.enabled = true;
		}

		public void VisibleOff ()
		{
				MeshRenderer meshR = GetComponent<MeshRenderer> ();
				meshR.enabled = false;
		}

		void Awake ()
		{
				GetComponent<MeshRenderer> ().material = new Material (Shader.Find ("GUI/Text Shader"));
				death = false;
		}

		void Update ()
		{
				if (death) {
						Destroy (gameObject);
				}
		}	

		/// <summary>
		/// 矩形ワイヤーメッシュを指定した矩形範囲に書き換える
		/// </summary>
		/// <param name="rect">矩形</param>
		/// <param name="col">色</param>
		void RefreshPolyBox (Rect rect, Color col)
		{
				Mesh mesh = GetComponent<MeshFilter> ().mesh;
				mesh.Clear ();

				vertices = new Vector3[]{
			new Vector3 (rect.xMin, -rect.yMin, 0.0f),
			new Vector3 (rect.xMax, -rect.yMin, 0.0f),
			new Vector3 (rect.xMax, -rect.yMax, 0.0f),
			new Vector3 (rect.xMin, -rect.yMax, 0.0f)};
				uv = new Vector2[]{Vector2.zero,Vector2.zero,Vector2.zero,Vector2.zero,};
				colors = new Color[]{col,col,col,col,};
				lines = new int[]{0,1,1,2,2,3,3,0};
		
				mesh.vertices = vertices;
				mesh.uv = uv;
				mesh.colors = colors;
				mesh.SetIndices (lines, MeshTopology.Lines, 0);
				transform.position = Vector3.zero;	//補正された位置が自分の考えていた位置では無い場合、自分で位置を変える必要がある
		}

}
