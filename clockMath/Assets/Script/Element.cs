using UnityEngine;
using System.Collections;

//そのオブジェクトの矩形サイズをunits単位で記録しておくクラス
//この矩形のサイズでシートに並べる事が出来る
public class Element : MonoBehaviour
{
		public float unitsWidth;
		public float unitsHeight;

		public float unitsCenterX {
				get{ return unitsWidth / 2;}
		}
		public float unitsCenterY {
				get{ return unitsHeight / 2;}
		}
}