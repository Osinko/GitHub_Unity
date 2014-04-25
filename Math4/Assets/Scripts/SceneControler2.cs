using UnityEngine;
using System.Collections;

public class SceneControler2 : MonoBehaviour {

	DrawGraph dg;
	AnimationSinVectorMesh animSin;
	
	void Awake () {
		
		GameObject go = Instantiate(Resources.Load("DrawGraph")) as GameObject;
		dg = go.GetComponent<DrawGraph>();
		
//		dg.AddCircle("circle1",Vector3.zero,3,32,Color.grey);
		
		dg.AddLine("Line1",new Vector3( -5.0f , 3.0f , 0.0f ),new Vector3( 20.0f , 3.0f , 0.0f ),Color.cyan);
		dg.AddLine("Line2",new Vector3( -5.0f , 0.0f , 0.0f ),new Vector3( 20.0f , 0.0f , 0.0f ),Color.cyan);
		dg.AddLine("Line3",new Vector3( -5.0f , -3.0f , 0.0f ),new Vector3( 20.0f , -3.0f , 0.0f ),Color.cyan);
		
		dg.AddLine("Line4",new Vector3( -3.0f , 5.0f , 0.0f ),new Vector3( -3.0f , -20.0f , 0.0f ),Color.cyan);
		dg.AddLine("Line5",new Vector3( 0.0f , 5.0f , 0.0f ),new Vector3( 0.0f , -20.0f , 0.0f ),Color.cyan);
		dg.AddLine("Line6",new Vector3( 3.0f , 5.0f , 0.0f ),new Vector3( 3.0f , -20.0f , 0.0f ),Color.cyan);
		
//		GameObject grid1 = dg.AddGrid("grid1",14*2,6*2,new Color(100,100,100,0.2f),0.5f,0.5f);
//		grid1.transform.position = new Vector3(12,0,0);			//普通にゲームオブジェクトとして扱えます
		
//		GridVectorMesh grid2 = dg.AddGrid("grid2",6*2,14*2,new Color(100,100,100,0.2f),0.5f,0.5f);
//		grid2.transform.position = new Vector3(0,-12,0);
		
		//VectorMeshを継承した独自オブジェクトをDrawGraphクラスの辞書に登録
		//この独自オブジェクトは独立したゲームオブジェクトとしても使える
		//辞書に登録すればRemoveやカラー変更等の機能も一括管理で利用できる（登録するか、しないかは自由）
//		GameObject go2 = Instantiate(Resources.Load("AnimationSinVectorMesh")) as GameObject;
//		dg.AddVectorMeshObjcect("AnimationSinVectorMesh1" , go2.GetComponent<VectorMesh>() );
//		
//		GameObject go3 = Instantiate(Resources.Load("AnimationSinVectorMesh")) as GameObject;
//		dg.AddVectorMeshObjcect("AnimationSinVectorMesh2" , go3.GetComponent<VectorMesh>() );
		
		//dg.DebugPrint();
		
	}
	
	void Update ()
	{
		
		if(Input.GetMouseButton(0)){
		}
		
		if( Input.GetButton("Jump")){
		}
	}
}
