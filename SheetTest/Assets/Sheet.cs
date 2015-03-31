using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Sheet : MonoBehaviour
{
	public GameObject objPref;	//並べて表示する要素となるスプライト
	public GameObject drawBoxPref;
	float objPref_Pixel2Units;	//ピクセル基準用
	Rect objSize;

	//コントロール用
	GameObject objRoot;
	List<Obj> objList;
	DrawBox drawbox;

	//オーダー数
	public int order = 40;
	public int sheetWidth = 960;
	public int sheetHeight = 400;

	//値の変更を検出する用
	int preOrder;
	int preSheetWidth;
	int preSheetHeight;

	void Awake ()
	{
		objRoot = new GameObject ("objRoot");

		//スプライトをunityのユニット単位で扱うには、そのテクスチャの1ピクセルがunityの１グリッドに対してどれぐらいの比なのか知る必要がある
		//良い資料：テラシュールウェア Unityでドット絵の1ドット単位移動
		//http://terasur.blog.fc2.com/blog-entry-846.html
		Bounds spBounds = objPref.GetComponent<SpriteRenderer> ().sprite.bounds;
		Rect spRect = objPref.GetComponent<SpriteRenderer> ().sprite.textureRect;
		objPref_Pixel2Units = spRect.width / spBounds.size.x;	//UnityのImportSettingのPixel2Unitsの値は直接拾えないので計算して拾う

		//テクスチャの縦横ピクセル数を記憶しておく
		objSize.width = spRect.width;
		objSize.height = spRect.height;

		GameObject go = Instantiate (drawBoxPref) as GameObject;
		drawbox = go.GetComponent<DrawBox> ();
		
	}

	void Start ()
	{
		SheetSpawn (order);
		drawbox.SetBox (new Rect (0, 0, Pixel2UnitFloat (sheetWidth), Pixel2UnitFloat (sheetHeight)), Color.white);
	}

	void Update ()
	{
		//更新チェック
		if (order != preOrder || preSheetHeight != sheetHeight || preSheetWidth != sheetWidth) {
			DestroyObj ();
			SheetSpawn (order);
		}
	}

	void OnGUI ()
	{
		order = (int)GUI.HorizontalSlider (new Rect (20, 20, 600, 30), order, 1f, 200f);
		sheetWidth = (int)GUI.HorizontalSlider (new Rect (20, 60, 600, 30), sheetWidth, 64f, 1200f);
		sheetHeight = (int)GUI.HorizontalSlider (new Rect (20, 100, 600, 30), sheetHeight, 64f, 600f);
	}

	//オブジェクトを削除
	void DestroyObj ()
	{
		for (int i = 0; i < objList.Count; i++) {
			objList [i].death = true;
		}
		objList.Clear ();
	}

	void SheetSpawn (int order)
	{
		objList = new List<Obj> ();

		for (int i = 0; i < order; i++) {
			GameObject go = Instantiate (objPref) as GameObject;
			go.transform.parent = objRoot.transform;
			objList.Add (go.GetComponent<Obj> ());
		}

		//最小公約数を求め
		float gcd = Gcd (sheetWidth, sheetHeight);

		//公約数から比率を決める
		float xRate = sheetWidth / gcd;
		float yRate = sheetHeight / gcd;

		//オーダー数を１としてその平方比を利用する
		float rate = xRate * yRate;
		float size = Mathf.Sqrt (order / rate);

		//縦横比
		float xx = xRate * size;
		float yy = yRate * size;

		//オブジェクトのサイズを狭いサイズに合わせる
		float reSize;
		if (xx <= yy) {
			reSize = (sheetWidth / xx) / objSize.width;
		} else {
			reSize = (sheetHeight / yy) / objSize.height;
		}
		
		print (string.Format ("矩形範囲{0}×{1} : オブジェクト矩形{2}×{3} : ピクセル/ユニット{4}", sheetWidth, sheetHeight,
		                      objSize.width, objSize.height, objPref_Pixel2Units));
		print (string.Format ("並べたい個数{0} ： オブジェクトのサイズ{1}：敷き詰める個数 {2}×{3}", order, reSize, xx, yy));

		Vector3[] test = GetSpawPosition (reSize, xx, yy).ToArray ();
		for (int i = 0; i < order; i++) {
			objList [i]._transform.localScale = new Vector3 (reSize, reSize, 1);
			objList [i]._transform.position = test [i];
		}

		drawbox.SetBox (new Rect (0, 0, Pixel2UnitFloat (sheetWidth), Pixel2UnitFloat (sheetHeight)), Color.white);


		preOrder = order;	//更新を記録
		preSheetWidth = sheetWidth;
		preSheetHeight = sheetHeight;
	}

	//オブジェクトの生成位置を作成する関数
	IEnumerable<Vector3> GetSpawPosition (float reSize, float x, float y)
	{
		float xShift = sheetWidth / x;
		float yShift = sheetHeight / y;
		
		float xCenter = (objSize.width * reSize) / 2;
		float yCenter = (objSize.height * reSize) / 2;

		float posX = 0;
		float posY = 0;
		for (int i = 0; i < order; i++) {
			yield return new Vector3 (Pixel2UnitFloat (posX + xCenter), Pixel2UnitFloat (posY - yCenter), 0);

			posX += xShift;
			if (posX > sheetWidth - xCenter) {
				posY -= yShift;
				posX = 0;
			}
		}
	}

	//ユークリッドの互除法
	public static float Gcd (float x, float y)
	{
		while (y!=0) {
			float z = x % y;
			x = y;
			y = z;
		}
		return x;
	}

	float Pixel2UnitFloat (float f)
	{
		return f / objPref_Pixel2Units;
	}

}
