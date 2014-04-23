using UnityEngine;
using System.Collections;


/// <summary>
/// Pythagoras4.
/// ベクター描画をテストするプログラム
/// </summary>
public class Pythagoras4 : MonoBehaviour {
	
	DrawGraph dg;
	AnimationVectorMesh animVecMesh;

	void Start () {

		GameObject go = Instantiate(Resources.Load("DrawGraph")) as GameObject;
		dg = go.GetComponent<DrawGraph>();

		GameObject vmGameObject = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vm = vmGameObject.GetComponent<VectorMesh>();

		//dg.AddAnimationDrawGraph();

		dg.AddCircle("Circle1",Vector3.zero,3,128,Color.grey);

		dg.AddLine("Line1",new Vector3( -5.0f , 3.0f , 0.0f ),new Vector3( 20.0f , 3.0f , 0.0f ),Color.cyan);
		dg.AddLine("Line2",new Vector3( -5.0f , 0.0f , 0.0f ),new Vector3( 20.0f , 0.0f , 0.0f ),Color.cyan);
		dg.AddLine("Line3",new Vector3( -5.0f , -3.0f , 0.0f ),new Vector3( 20.0f , -3.0f , 0.0f ),Color.cyan);

		dg.AddLine("Line4",new Vector3( -3.0f , 5.0f , 0.0f ),new Vector3( -3.0f , -20.0f , 0.0f ),Color.cyan);
		dg.AddLine("Line5",new Vector3( 0.0f , 5.0f , 0.0f ),new Vector3( 0.0f , -20.0f , 0.0f ),Color.cyan);
		dg.AddLine("Line6",new Vector3( 3.0f , 5.0f , 0.0f ),new Vector3( 3.0f , -20.0f , 0.0f ),Color.cyan);

		//TODO
		GridVectorMesh gvm = dg.AddGrid("Grid1",new Color(100,100,100,0.1f),1.0f/2.0f,2*6);
		gvm.transform.position = new Vector3(8,0,0);		//普通にゲームオブジェクトとして扱えます
	}

	void Update ()
	{

		if(Input.GetMouseButton(0)){
		}

		if( Input.GetButton("Jump")){
		}
	}

}
