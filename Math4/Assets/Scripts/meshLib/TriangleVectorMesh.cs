using UnityEngine;
using System.Collections;

public class TriangleVectorMesh : VectorMesh {

	public Vector3 a;
	public Vector3 b;
	public Vector3 c;
	
	Vector3 aPrev;
	Vector3 bPrev;
	Vector3 cPrev;

	public override void Awake ()
	{
		a = Vector3.zero;
		b = Vector3.zero;
		c = Vector3.zero;
		
		aPrev = a;
		bPrev = b;
		cPrev = c;

		base.Awake ();
	}
	
	public override void Update ()
	{
		if( a!=aPrev || b!=bPrev || c!=cPrev){
			Refresh();
		}
		base.Update ();
	}
	
	public void SetUpMesh (string name,Vector3 a, Vector3 b, Vector3 c , Color color){
		gameObject.name = name;
		this.vertices = new Vector3[]{a,b,c,};
		this.uvs = new Vector2[]{Vector2.zero,Vector2.zero,Vector2.zero,};
		this.lines = new int[]{0,1,1,2,2,0,};
		this.color = color;
		
		this.RefreshMesh();
	}
	
	void Refresh ()
	{
		this.vertices = new Vector3[]{a,b,c,};
		this.RefreshMesh();
	}
}