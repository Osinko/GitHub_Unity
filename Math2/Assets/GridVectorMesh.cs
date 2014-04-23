using UnityEngine;
using System.Collections;

//TODO
public class GridVectorMesh : VectorMesh {

	public float	gridSize;
	public int		totalSize;

	public int repeatX;
	public int repeatY;

	//値変化検出用
	float	gridSizePrev;
	int 	totalSizePrev;
	int 	repeatXprev;
	int 	repeatYprev;

	public override void Awake ()
	{
		gridSize=1;
		totalSize=8;

		repeatX=1;
		repeatY=1;

		gridSizePrev = gridSize;
		totalSizePrev = totalSize;
		repeatXprev = repeatX;
		repeatYprev = repeatY;

		base.Awake ();
	}

	void Start () {
	}

	public override void Update ()
	{
		if(gridSize!=gridSizePrev || totalSize!=totalSizePrev || repeatX!=repeatXprev || repeatY!=repeatYprev){
			ReGridVectorMesh();
		}
		base.Update ();
	}

	public void CreateGridVectorMesh(float gridSize,int totalSize,Color color,int repeatX = 1 , int repeatY = 1){
		this.gridSize = gridSize;
		this.totalSize = totalSize;
		this.totalSize = totalSize;

		this.repeatX = repeatX;
		this.repeatY = repeatY;
		this.color = color;
		
		ReGridVectorMesh();
	}

	public void ReGridVectorMesh(){

		int		drawSize;
		float	width;
		int		resolution;
		float	diff;
		
		drawSize = totalSize * 2;
		width = gridSize * drawSize / 4.0f;
		Vector2 startPosition = new Vector2 (-width, -width);
		Vector2 endPosition = new Vector2 (width, width);
		diff = width / drawSize;
		resolution = (drawSize + 2) * 2;	//最期の２辺を追加している
		
		vertices = new Vector3[resolution];
		uvs = new Vector2[resolution];
		lines = new int[resolution];
		
		for (int i = 0; i < vertices.Length; i += 4) {
			vertices [i] = new Vector3 (startPosition.x + (diff * (float)i), startPosition.y, 0);
			vertices [i + 1] = new Vector3 (startPosition.x + (diff * (float)i), endPosition.y, 0);
			vertices [i + 2] = new Vector3 (startPosition.x, endPosition.y - (diff * (float)i), 0);
			vertices [i + 3] = new Vector3 (endPosition.x, endPosition.y - (diff * (float)i), 0);
		}
		
		for (int i = 0; i < resolution; i++) {
			lines [i] = i;
		}
		
		vertices = vertices;
		uvs = uvs;
		lines = lines;
		color = color;
	}

}
