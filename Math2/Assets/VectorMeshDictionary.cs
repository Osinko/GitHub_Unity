using UnityEngine;
using System.Collections;

public class VectorMeshDictionary : DictionaryBase {
	
	//ここでDictionaryを使えるようにしている
	public VectorMesh this[ string key ]{
		get{return (VectorMesh) Dictionary[key];}
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
	
	public void Add(string key,VectorMesh value){
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
		VectorMesh vm = (VectorMesh) value;
		vm.OnInsertComplete();
		base.OnInsertComplete (key, value);
	}

	protected override void OnRemoveComplete (object key, object value)
	{
		VectorMesh vm = (VectorMesh) value;
		vm.OnRemoveComplete();
		base.OnRemoveComplete (key, value);
	}

}
