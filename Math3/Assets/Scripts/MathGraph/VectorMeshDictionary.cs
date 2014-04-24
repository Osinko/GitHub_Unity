using UnityEngine;
using System.Collections;

/// <summary>
/// ベクターメッシュを管理するためのカスタム化された専用辞書クラス
/// 適切なタイミングでVectorMeshクラスのOn～機能などを呼んでいる点に特徴がある
/// </summary>
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
		VectorMesh vm = (VectorMesh) value;
		vm.OnInsertComplete();	//VectorMeshクラスの関数を呼び出す
		base.OnInsertComplete (key, value);
	}

	protected override void OnRemoveComplete (object key, object value)
	{
		VectorMesh vm = (VectorMesh) value;
		vm.OnRemoveComplete();	//VectorMeshクラスの関数を呼び出す
		base.OnRemoveComplete (key, value);
	}

}
