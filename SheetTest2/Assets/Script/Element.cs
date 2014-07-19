using UnityEngine;
using System.Collections;

public class Element : MonoBehaviour
{
		//その要素オブジェクトのシート上での扱いの大きさをここで設定しています
		public float widthSize = 1;
		public float heightSize = 1;
	
		public Transform _transform;
	
		void Awake ()
		{
				_transform = transform;	
		}
}
