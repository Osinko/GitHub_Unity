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
		
		dg.AddLine("Line1" , Vector3.zero , new Vector3(2,2,2), Color.red);
		dg.Set("Line1").VectorMeshLifeTime = 5.0f;
		dg.Set("Line1").LifeTimeOn = false;
		dg.Set("Line1").colorPoint = new Color[]{Color.red,Color.blue};
		dg.Set("Line1").colorPointOn = true;
		dg.AddGrid("Grid1",Color.white);

		dg.AddTriangle("Triangle1" , Vector3.zero,new Vector3(0,1,0),new Vector3(1,1,0),Color.grey);
		dg.AddLine("Line2" , new Vector3(3,4,2) , new Vector3(2,2,2), Color.green);
		
		dg.AddBox("Box1",new Rect(0,0,5,5), Color.yellow);

		dg.AddCircle("Circle1",Vector3.zero,1.5f,128,Color.white,true);

		//dg.AddMesh();
		//dg.AddDrawGraph("Graph1",animMesh,Color.white,Face.xy);

	}

	void Update ()
	{

		if(Input.GetMouseButton(0)){
			dg.AddLine("Line2" , new Vector3(2,2,0) , new Vector3(8,6,4), Color.cyan);
			dg.Set("Line1").colorPointOn = false;
			dg.Set("Grid1").visible = !dg.Set("Grid1").visible;
			dg.AddCircle("Circle1",Vector3.zero,3.0f,128,Color.white,true);
			
		}

		if( Input.GetButton("Jump")){
			dg.Remove("Line2");
			dg.Remove("Box1");
			dg.Remove("Grid1");
			dg.Clear();
		}
	}

}
