using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// ベクターメッシュを管理するためのカスタム化された専用辞書クラス
/// </summary>
public class VectorMeshDictionary : MonoBehaviour {

	Dictionary<string,VectorMesh> vmDictionary;

	void Awake (){
		vmDictionary = new Dictionary<string, VectorMesh>();
	}

	public VectorMesh Add(string key, GameObject go){
		VectorMesh vm = go.GetComponent<VectorMesh>() as VectorMesh;

		if(!vm){
			print("not VectorMesh GameObject");
			return null;
		}

		if(vmDictionary.ContainsKey(key)){
			vmDictionary.Remove(key);
		}
		vmDictionary.Add(key,vm);
		vm.OnInsertComplete();

		return vm;
	}

	public bool Remove(string key){
		if(!key){
			print("key is ArgumentNullException");
			return false;
		}

		if(!vmDictionary.ContainsKey(key)){
			print("not find key");
			return false;
		}

		VectorMesh vm = vmDictionary[key];
		vmDictionary.Remove(key);
		vm.OnRemoveComplete();

		return true;
	}

	public void Clear(){
		vmDictionary.Clear();
	}

	public VectorMesh this[string key]{
		get{
			return (VectorMesh) vmDictionary[key];
		}
	}

}
