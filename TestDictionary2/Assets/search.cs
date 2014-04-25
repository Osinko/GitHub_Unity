using UnityEngine;
using System.Collections;

public class search : MonoBehaviour {

	public Person go;
	public test1 tes;

	void Start () {
		go =  FindObjectOfType<Person>();
		go.Add();
		tes =  FindObjectOfType<test1>();
		tes.print();

		tes.gameObject.transform.position = new Vector3(5,1,1);

	}
	
	void Update () {
	
	}
}
