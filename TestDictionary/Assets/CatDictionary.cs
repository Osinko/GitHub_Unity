using UnityEngine;
using System.Collections;

public class CatDictionary : DictionaryBase {

	//ここでDictionaryを使えるようにしている
	public Cat this[ string key ]{
		get{return (Cat) Dictionary[key];}
		set{Dictionary[key] = value;}
	}

	public void Add(string key,Cat value){
		if(Dictionary.Contains(key)){
			Dictionary.Remove(key);
		}
		Dictionary.Add(key,value);
	}
	protected override void OnInsertComplete (object key, object value)
	{
		Cat vm = (Cat) value;
		vm.OnInsertComplete();    //VectorMeshクラスの関数を呼び出す
		base.OnInsertComplete (key, value);
	}
	
	protected override void OnRemoveComplete (object key, object value)
	{
		Cat vm = (Cat) value;
		vm.OnRemoveComplete();    //VectorMeshクラスの関数を呼び出す
		base.OnRemoveComplete (key, value);
	}
}
