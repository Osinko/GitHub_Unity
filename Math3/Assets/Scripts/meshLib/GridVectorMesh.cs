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

		columnPrev = column;
		rowPrev = row;
		girdSizeXPrev = girdSizeX;
		girdSizeYPrev = girdSizeY;

		base.Awake ();
	}

	public override void Update ()
	{
		if(column!=columnPrev || row!=rowPrev || girdSizeX!=girdSizeXPrev || girdSizeY!=girdSizeYPrev){
			//最小値のクランプ
			if(column < 1){column = 1;}
			if(row < 1){row = 1;}
			if(girdSizeX < 0.00000001f){girdSizeX=0.00000001f;}
			if(girdSizeY < 0.00000001f){girdSizeY=0.00000001f;}
			Refresh();
		}
		base.Update ();
	}

	public void SetUpMesh(string name, int column,int row,Color color,float girdSizeX = 1,float girdSizeY = 1){
		this.column = column;
		this.row = row;
		this.girdSizeX = girdSizeX;
		this.girdSizeY = girdSizeY;

		//テンプレ部分
		gameObject.name = name;	//オブジェクトネーム更新
		this.color = color;		//継承元のカラーへ代入
		transform.parent = controler.transform;		//呼び出し元のDrawGraphオブジェクトの子にする 

		Refresh();
	}

	//メッシュ構造の再構築
	public void Refresh(){

		float totalX = (float)column * girdSizeX/2;
		float totalY = (float)row * girdSizeY/2;

		Vector2 startPosition = new Vector2 (-(totalX), -(totalY));
		Vector2 endPosition = new Vector2 ((totalX) , (totalY));
		int resolution = ((column+1)*2) + ((row+1)*2);

		Vector3[] vertices = new Vector3[resolution];
		Vector2[] uvs = new Vector2[resolution];
		int[] lines = new int[resolution];

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
		
		for (int i = 0; i < resolution; i++) {
			lines [i] = i;
		}
		
		this.vertices = vertices;
		this.uvs = uvs;
		this.lines = lines;

		RefreshMesh();
	}

}
