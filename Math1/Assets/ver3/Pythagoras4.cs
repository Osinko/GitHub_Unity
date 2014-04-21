﻿using UnityEngine;
using System.Collections;

public class Pythagoras4 : MonoBehaviour {
	

	DrawGraph2 dg;
	//DrawGraphAnimMesh animMesh;		//dg内とは別のライフスパンなどを持っているクラス

	void Start () {

		dg = new DrawGraph2();
		GameObject go = Instantiate(Resources.Load("VectorMesh")) as GameObject;
		VectorMesh vectorMesh = go.GetComponent<VectorMesh>();
		
		
		dg.AddLine("Line1" , Vector3.zero , new Vector3(2,2,2), Color.red);
		//dg.Set("Line1").lifeTime = 5;
		dg.AddLine("Line2" , new Vector3(3,4,2) , new Vector3(2,2,2), Color.green);
		
		dg.AddBox("Box1",new Rect(0,0,5,5), Color.yellow);
		//dg.AddDrawGraph("Graph1",animMesh,Color.white,Face.xy);
		//dg.Set("Graph1").lifeTime = 5;

	}

	void Update ()
	{
		if( Input.GetButton("Jump")){
			//dg.Remove("Line2");
		}
	}

}
