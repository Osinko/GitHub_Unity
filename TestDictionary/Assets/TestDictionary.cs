using UnityEngine;
using System.Collections;

public class TestDictionary : MonoBehaviour {

	PersonDictionary personList;

	void Start () {
		personList= new PersonDictionary();

		Person person = (Instantiate(Resources.Load("PersonPref")) as GameObject).GetComponent<Person>();
		person.name = "kaori";
		person.age =18;
		person.position = new Vector3[]{
			Vector3.zero
		};

		personList.Add("osaka",person);
	}

}
