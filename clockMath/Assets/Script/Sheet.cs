using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Sheet : MonoBehaviour
{
		public GameObject objPref;	//並べて表示する要素となるゲームオブジェクト
		public GameObject drawBoxPref;

		//コントロール用
		Element element;
		GameObject objRoot;
		List<GameObject> GameObjectList;
		DrawBox drawbox;

		//値の変更を検出する用
		int preOrder;
		int preSheetWidth;
		int preSheetHeight;

		void Awake ()
		{
				objRoot = new GameObject ("objRoot");
				element = objPref.GetComponent<Element> () as Element;
				GameObject go = Instantiate (drawBoxPref) as GameObject;
				drawbox = go.GetComponent<DrawBox> ();

				GameObjectList = new  List<GameObject> ();
		}

		public void  DrawBoxVisible ()
		{
				drawbox.VisibleOn ();
		}

		public void  DrawBoxInvisible ()
		{
				drawbox.VisibleOff ();
		}

		public List<GameObject> SetSheet (int order, float sheetWidth, float sheetHeight)
		{
				SheetSpawn (order, sheetWidth, sheetHeight);
				drawbox.SetBox (new Rect (0, 0, sheetWidth, sheetHeight), Color.cyan);
				return GameObjectList;
		}

		//オブジェクトを削除
		void DestroyObj ()
		{
				for (int i = 0; i < GameObjectList.Count; i++) {
						Destroy (GameObjectList [i]);
				}
				GameObjectList.Clear ();
		}

		void SheetSpawn (int order, float sheetWidth, float sheetHeight)
		{
				if (GameObjectList.Count >= 1) {
						DestroyObj ();
				}

				for (int i = 0; i < order; i++) {
						GameObject go = Instantiate (objPref) as GameObject;
						go.transform.parent = objRoot.transform;
						GameObjectList.Add (go);
				}

				//最小公約数を求め
				float gcd = Gcd (sheetWidth * element.unitsWidth, sheetHeight * element.unitsHeight);

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
						reSize = (sheetWidth / xx) / element.unitsWidth;
				} else {
						reSize = (sheetHeight / yy) / element.unitsHeight;
				}
		
				print (string.Format ("矩形範囲{0}×{1} : オブジェクト矩形{2}×{3}", sheetWidth, sheetHeight,
		                      element.unitsWidth, element.unitsHeight));
				print (string.Format ("並べたい個数{0} ： オブジェクトのサイズ{1}：敷き詰める個数 {2}×{3}", order, reSize, xx, yy));

				Vector3[] test = GetSpawPosition (reSize, xx, yy, order, sheetWidth, sheetHeight).ToArray ();
				for (int i = 0; i < order; i++) {
						GameObjectList [i].transform.localScale = new Vector3 (reSize, reSize, 1);
						GameObjectList [i].transform.position = test [i];
				}

				//矩形描画
				drawbox.SetBox (new Rect (0, 0, sheetWidth, sheetHeight), Color.white);
		}

		//オブジェクトの生成位置を作成する関数
		IEnumerable<Vector3> GetSpawPosition (float reSize, float x, float y, int order, float sheetWidth, float sheetHeight)
		{
				float xShift = sheetWidth / x;
				float yShift = sheetHeight / y;
		
				float xCenter = element.unitsCenterX * reSize;
				float yCenter = element.unitsCenterY * reSize;

				float posX = 0;
				float posY = 0;
				for (int i = 0; i < order; i++) {
						yield return new Vector3 (posX + xCenter, posY - yCenter, 0);

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

}
