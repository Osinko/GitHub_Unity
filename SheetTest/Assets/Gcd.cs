using UnityEngine;
using System.Collections;

public class Gcd : MonoBehaviour
{

	void Start ()
	{
		print ("最大公約数:" + Gcd1 (100f, 86f));
		//print ("最小公倍数:" + Lcm (12, 18));
	}
	
	//ユークリッドの互除法
	public static float Gcd1 (float x, float y)
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
		return x * y / Gcd1 (x, y);
	}
}
