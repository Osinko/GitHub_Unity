using UnityEngine;
using System.Collections;

public class Obj : MonoBehaviour
{
	public Transform _transform;
	public bool death;

	void Awake ()
	{
		death = false;
		_transform = transform;
	}

	void Update ()
	{
		if (death) {
			Destroy (gameObject);
		}
	}

}
