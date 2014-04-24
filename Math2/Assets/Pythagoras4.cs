﻿using UnityEngine;
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

		GridVectorMesh gvm = dg.AddGrid("Grid1",14*2,6*2,new Color(100,100,100,0.2f),0.5f,0.5f);
		gvm.transform.position = new Vector3(12,0,0);		//普通にゲームオブジェクトとして扱えます

		GridVectorMesh gvm2 = dg.AddGrid("Grid2",6*2,14*2,new Color(100,100,100,0.2f),0.5f,0.5f);
		gvm2.transform.position = new Vector3(0,-12,0);		//普通にゲームオブジェクトとして扱えます
	}

	void Update ()
	{

		if(Input.GetMouseButton(0)){
		}

		if( Input.GetButton("Jump")){
		}
	}

}
