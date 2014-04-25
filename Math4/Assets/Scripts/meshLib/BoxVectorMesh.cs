using UnityEngine;
using System.Collections;

public class BoxVectorMesh : VectorMesh {
	
	public Rect rect;
	
	Rect rectPrev;
	
	void Start () {
		rect = new Rect(0,0,0,0);
		rectPrev = rect;
	}
	
	public override void Awake ()
	{
		if(rect != rectPrev){
			Refresh();
		}
		base.Awake ();
	}
	
	public void SetUpMesh (string name, Rect rect, Color color){
		gameObject.name = name;
		
		this.vertices = new Vector3[]{
			new Vector3(rect.xMin,rect.yMin,0),
			new Vector3(rect.xMax,rect.yMin,0),
			new Vector3(rect.xMax,rect.yMax,0),
			new Vector3(rect.xMin,rect.yMax,0),
		};

		this.uvs = new Vector2[]{Vector2.zero,Vector2.zero,Vector2.zero,Vector2.zero,};
		this.lines = new int[]{0,1,1,2,2,3,3,0,};
		this.color = color;
		
		this.RefreshMesh();
	}
	
	void Refresh ()
	{
		this.vertices = new Vector3[]{
			new Vector3(rect.xMin,rect.yMin,0),
			new Vector3(rect.xMax,rect.yMin,0),
			new Vector3(rect.xMax,rect.yMax,0),
			new Vector3(rect.xMin,rect.yMax,0),
		};
		this.RefreshMesh();
	}
}