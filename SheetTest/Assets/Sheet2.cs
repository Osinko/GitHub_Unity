using UnityEngine;
using System.Collections;

public class Sheet2 : MonoBehaviour
{

	void Start ()
	{
		SetSheet (14, 8, 6, 1.2f, 1.4f);
	}

	void SetSheet (float order, float width, float height, float elemntWidth, float elemntHeight)
	{
		bool widthRate;
		bool elemntWidthRate;
		float rate, elementRate;
		float size;
		float resizeElementX, resizeElementY;

		//シート：大きい値の方を基準にする
		if (width <= height) {
			widthRate = false;
			rate = 1.0f / (height / width);
		} else {
			widthRate = true;
			rate = 1.0f / (width / height);
		}

		//エレメント：大きい値の方を基準にする
		if (elemntWidth <= elemntHeight) {
			elemntWidthRate = false;
			elementRate = 1.0f / (elemntHeight / elemntWidth);
			
		} else {
			elemntWidthRate = true;
			elementRate = 1.0f / (elemntWidth / elemntHeight);
		}

		float square = order * rate;


		print (string.Format ("widthRate {0}: rate= {1}   elemntWidthRate {2}: elemntRate= {3}", widthRate, rate, elemntWidthRate, elementRate));
		print (string.Format ("square {0}", square));

	}

	
	void Update ()
	{
	
	}
}
