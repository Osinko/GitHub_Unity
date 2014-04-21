using UnityEngine;
using System.Collections;

public class Pythagoras4 : MonoBehaviour {
	

	DrawGraph2 dg;
	DrawGraphAnimMesh animMesh;		//dg内とは別のライフスパンなどを持っているクラス

	void Start () {

		dg = new DrawGraph2();

		dg.AddLine("Line1",24.5f,12.3f,Color.red,DrawGraph2.Face.xy);
		dg.Set("Line1").lifeTime = 5;
		dg.AddLine("Line2",53.6f,-12.5f);
		dg.AddBox("Box1",new Rect(0,0,5,5),DrawGraph2.Face.xy);
		dg.AddDrawGraph("Graph1",animMesh,Color.white,Face.xy);
		dg.Set("Graph1").lifeTime = 5;

	}

	void Update ()
	{
		if( Input.GetButton("Jump")){
			dg.AddLine("Line1",50.0f,50.0f,Color.red,face);
			dg.Remove("Line2");
		}
	}

}
