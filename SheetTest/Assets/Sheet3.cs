using UnityEngine;
using System.Collections;

public class Sheet3 : MonoBehaviour
{


	void Start ()
	{
		SetSheet (14, 600, 500, 32, 48);
	}

	void SetSheet (int order, float width, float height, float elementWidth, float elementHeight)
	{
		float rateX;
		float rateY;
		float elementRateX;
		float elementRateY;

		//小さい値を基準に１として比をきめる
		if (width < height) {
			rateX = width / width;
			rateY = height / width;
		} else {
			rateY = height / height;
			rateX = width / height;
		}

		if (elementWidth < elementHeight) {
			elementRateX = elementWidth / elementWidth;
			elementRateY = elementHeight / elementWidth;
		} else {
			elementRateY = elementHeight / elementHeight;
			elementRateX = elementWidth / elementHeight;
		}

		float tempRateX = rateX / elementRateX;
		float tempRateY = rateY / elementRateY;


		float size = Mathf.CeilToInt (order / (rateX * rateY));
		float sizeX = Mathf.CeilToInt (size * rateX);
		float sizeY = Mathf.CeilToInt (size * rateY);

		float gcd = Gcd (sizeX, sizeY);
		sizeX /= gcd;
		sizeY /= gcd;

		//
		print (string.Format ("order={0} : {1}:{2}", size, sizeX, sizeY));
	
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
	
	public static float Lcm (float x, float y)
	{
		return x * y / Gcd (x, y);
	}
}
