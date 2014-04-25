using UnityEngine;
using System.Collections;

public class LineVectorMesh : VectorMesh {

	public Vector3 start;
	public Vector3 end;

	Vector3 startPrev;
	Vector3 endPrev;

	public override void Awake ()
	{
		start = Vector3.zero;
		end = Vector3.zero;
		
		startPrev = start;
		endPrev = end;
		base.Awake ();
	}

	public override void Update ()
	{
		if( start!=startPrev || end!=endPrev){
			Refresh();
		}
		base.Update ();
	}

	public void SetUpMesh (string name, Vector3 start, Vector3 end, Color color){
		gameObject.name = name;
		this.vertices = new Vector3[]{start,end,};
		this.uvs = new Vector2[]{Vector2.zero,Vector2.zero,};
		this.lines = new int[]{0,1,};
		this.color = color;
		
		this.RefreshMesh();
	}

	void Refresh ()
	{
		this.vertices = new Vector3[]{start,end,};
		this.RefreshMesh();
	}
}
