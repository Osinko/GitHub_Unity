using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour {


	public string personName;
	public int age;
	public Vector3[] position;

	void Start ()
	{
		print("PersonCloneStart.");
	}

	public void OnInsertComplete(){
		print("!");
	}

	public void OnRemoveComplete ()
	{
		print("!!");
		
	}
}
