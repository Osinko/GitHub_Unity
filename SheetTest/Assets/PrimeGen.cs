using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using System.Linq;

public class PrimeGen : MonoBehaviour
{

	IEnumerable<int>    numbers, primeNum;
	
	void Start ()
	{
		numbers = GetRange (100);
		//エラトステネスの篩のアルゴリズムを利用する
		primeNum = numbers.Where (p => p != 1 && p % 2 != 0 && p % 3 != 0 && p % 5 != 0 && p % 7 != 0
			|| p == 2 || p == 3 || p == 5 || p == 7);
		
		foreach (var item in primeNum) {
			print (item);
		}
	}
	
	public static IEnumerable <int> GetRange (int num)
	{
		for (int i = 0; i < num; i++) {
			yield return i;
		}
	}
}
