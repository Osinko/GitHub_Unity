using UnityEngine;
using System.Collections;

public class TestDictionary2 : MonoBehaviour {

	CatDictionary ctd;

	void Start () {
		ctd = new CatDictionary();

		ctd.Add("abc",new Cat());
	}


}
