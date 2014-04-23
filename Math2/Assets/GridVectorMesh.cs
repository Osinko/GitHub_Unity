using UnityEngine;
using System.Collections;

public class GridVectorMesh : VectorMesh {

	public int		column;
	public int		row;
	public float	girdSizeX;
	public float	girdSizeY;


	//値変化検出用 
	int		columnPrev;
	int 	rowPrev;
	float 	girdSizeXPrev;
	float 	girdSizeYPrev;

	public override void Awake ()
	{
		column=5;
		row =5;
		girdSizeX=1.0f;
		girdSizeY=1.0f;

		base.Awake ();
	}

	public override void Update ()
	{

		base.Update ();
	}

	public void CreateGridVectorMesh(int column,int row,Color color,float girdSizeX = 1,float girdSizeY = 1){

		this.column = column;
		this.row = row;
		this.girdSizeX = girdSizeX;
		this.girdSizeY = girdSizeY;
		this.color = color;

		ReGridVectorMesh();
	}

	public void ReGridVectorMesh(){

		float totalX = (float)column * girdSizeX/2;
		float totalY = (float)row * girdSizeY/2;

		Vector2 startPosition = new Vector2 (-(totalX), -(totalY));
		Vector2 endPosition = new Vector2 ((totalX) , (totalY));
		int resolution = ((column+1)*2) + ((row+1)*2);

		vertices = new Vector3[resolution];
		uvs = new Vector2[resolution];
		lines = new int[resolution];

		float diffx = girdSizeX/2;
		float diffy = girdSizeY/2;

		for (int x = 0; x < ((column+1)*2); x+=2) {
			vertices [x] = new Vector3 (startPosition.x + (diffx * (float)x), startPosition.y, 0);
			vertices [x+1] = new Vector3 (startPosition.x + (diffx * (float)x), endPosition.y, 0);
		}
		for (int y = 0; y < ((row+1)*2); y+=2) {
			vertices [(column*2)+2 + y] = new Vector3 (startPosition.x, endPosition.y - (diffy * (float)y), 0);
			vertices [(column*2)+2 + y+1] = new Vector3 (endPosition.x, endPosition.y - (diffy * (float)y), 0);
		}


//		float diffx = girdSizeX/4;
//		float diffy = girdSizeY/4;
//		for (int i = 0; i < (vertices.Length/2); i += 4) {
//			vertices [i] = new Vector3 (startPosition.x + (diffx * (float)i), startPosition.y, 0);
//			vertices [i + 1] = new Vector3 (startPosition.x + (diffx * (float)i), endPosition.y, 0);
//			vertices [i + 2] = new Vector3 (startPosition.x, endPosition.y - (diffy * (float)i), 0);
//			vertices [i + 3] = new Vector3 (endPosition.x, endPosition.y - (diffy * (float)i), 0);
//		}

		
		for (int i = 0; i < resolution; i++) {
			lines [i] = i;
		}
		
		vertices = vertices;
		uvs = uvs;
		lines = lines;
	}

}
