﻿using UnityEngine;
using System.Collections;

public class Pythagoras4 : MonoBehaviour {
	

	DrawGraph dg;
	//DrawGraphAnimMesh animMesh;		//dg内とは別のライフスパンなどを持っているクラス

	void Start () {

		GameObject go = Instantiate(Resources.Load("DrawGraph")) as GameObject;
		dg = go.GetComponent<DrawGraph>();
		
		
		dg.AddLine("Line1" , Vector3.zero , new Vector3(2,2,2), Color.red);
		dg.Set("Line1").VectorMeshLifeTime = 5.0f;
		dg.Set("Line1").LifeTimeOn = true;
		dg.AddLine("Line2" , new Vector3(3,4,2) , new Vector3(2,2,2), Color.green);
		
		dg.AddBox("Box1",new Rect(0,0,5,5), Color.yellow);
		//dg.AddDrawGraph("Graph1",animMesh,Color.white,Face.xy);

	}

	void Update ()
	{

		if(Input.GetMouseButton(0)){
			dg.AddLine("Line2" , new Vector3(2,2,0) , new Vector3(8,6,4), Color.cyan);
		}

		if( Input.GetButton("Jump")){
			dg.Remove("Line2");
			dg.Remove("Box1");
		}
	}

}