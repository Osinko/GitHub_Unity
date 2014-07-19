using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NewSheet2 : MonoBehaviour
{
		public Element elemntPref;
		public GameObject drawBoxPref;

		DrawBox drawBox;
		Element element;
		GameObject sheetRoot;

		int order = 0;
		int sheetWidth = 0;
		int sheetHeight = 0;

		//値の変更を検出する用
		int preOrder = 0;
		int preSheetWidth = 0;
		int preSheetHeight = 0;

		Vector4 rectInfo;
		List<Element> elemntList;

		//外部のゲームオブジェクトからシートを作成および値の変更を行う際にこの関数を使って行う
		//変更は自動的に検知され更新が行なわれる
		public void SetSheet (int order, int sheetWidth, int sheetHeight)
		{
				this.order = order;
				this.sheetWidth = sheetWidth;
				this.sheetHeight = sheetHeight;
		}

		//エレメント要素変更時に利用
		public void SetElement (Element element)
		{
				elemntPref = element;
				this.element = element;
		}

		void Awake ()
		{
				sheetRoot = new GameObject ("sheetRoot");
				GameObject go = Instantiate (drawBoxPref) as GameObject;
				go.transform.parent = sheetRoot.transform;
				drawBox = go.GetComponent<DrawBox> ();
				element = elemntPref.GetComponent<Element> ();
				elemntList = new List<Element> ();
		}


		void CreateSheet ()
		{
				if (order > 0) {
						rectInfo = RectOrder2.BresenhamsRect (order, sheetWidth, sheetHeight);
						float elementResizeW;	
						float elementResizeH;
						//三個以降は要素自体の多少の変形(各軸1.5倍まで)を認める
						elementResizeW = (sheetWidth / rectInfo.x) / element.widthSize;
						elementResizeH = (sheetHeight / rectInfo.y) / element.heightSize;
						
						//仕様変更：最初の3個までは縦横比を変えない
						if (order < 4) {
								float temp;
								temp = elementResizeW > elementResizeH ? elementResizeH : elementResizeW;
								elementResizeW = temp;
								elementResizeH = temp;
						}
						
						float xShift = elementResizeW * element.widthSize;
						float yShift = elementResizeH * element.heightSize;
						float xCenter = xShift / 2;
						float yCenter = yShift / 2;

						if (elemntList.Count > 0) {
								for (int i = 0; i < elemntList.Count; i++) {
										DestroyImmediate (elemntList [i].gameObject);
								}
								elemntList.Clear ();
						}

						for (int i = 0; i < order; i++) {
								Element go = Instantiate (element) as Element;
								go.transform.parent = sheetRoot.transform;
								elemntList.Add (go.GetComponent<Element> ());
						}

						int j = 0, k = 0;
						for (int i = 0; i < elemntList.Count; i++) {
								if (j >= rectInfo.x) {
										j = 0;
										k++;
								}
								elemntList [i]._transform.localScale = new Vector3 (elementResizeW, elementResizeH, 1);
								elemntList [i]._transform.position = new Vector3 ((xShift * j) + xCenter, -(yShift * k) - yCenter, 0);
								j++;
						}

						drawBox.SetBox (new Rect (0, 0, sheetWidth, sheetHeight), Color.white);

						preOrder = order;	//更新を記録
						preSheetWidth = sheetWidth;
						preSheetHeight = sheetHeight;
				}
		}
	
		void Update ()
		{
				//更新チェック
				if (order != preOrder || preSheetHeight != sheetHeight || preSheetWidth != sheetWidth) {
						CreateSheet ();
				}
		}
}
