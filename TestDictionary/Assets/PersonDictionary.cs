using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PersonDictionary : DictionaryBase {

	//ここでDictionaryを使えるようにしている
	public Person this[ string key ]{
		get{return (Person) Dictionary[key];}
		set{Dictionary[key] = value;}
	}
	
	public ICollection Keys{
		get {
			return Dictionary.Keys;
		}
	}
	
	public ICollection Values{
		get {
			return Dictionary.Values;
		}
	}
	
	public void Add(string key,Person value){
		if(Dictionary.Contains(key)){
			Dictionary.Remove(key);
		}
		Dictionary.Add(key,value);
	}
	
	public bool Contains(string key){
		return Dictionary.Contains(key);
	}
	
	public void Remove(string key ){
		Dictionary.Remove(key);
	}

	protected override void OnInsertComplete (object key, object value)
	{
		Person vm = (Person) value;
		vm.OnInsertComplete();    //VectorMeshクラスの関数を呼び出す
		base.OnInsertComplete (key, value);
	}
	
	protected override void OnRemoveComplete (object key, object value)
	{
		Person vm = (Person) value;
		vm.OnRemoveComplete();    //VectorMeshクラスの関数を呼び出す
		base.OnRemoveComplete (key, value);
	}


}
