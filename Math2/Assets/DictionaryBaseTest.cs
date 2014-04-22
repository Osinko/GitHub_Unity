using UnityEngine;
using System.Collections;

public class DictionaryBaseTest : MonoBehaviour {
	
	//資料：DictionaryBase クラス (System.Collections)
	//http://msdn.microsoft.com/ja-jp/library/system.collections.dictionarybase(v=vs.110).aspx
	
	public class TestDictionary : DictionaryBase{

		//ここでDictionaryを使えるようにしている
		public Vector3 this[ string key ]{
			get{return (Vector3) Dictionary[key];}
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
		
		public void Add(string key,Vector3 value){
			if(Dictionary.Contains(key)){//TODO
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
			print(string.Format("Add  key={0} value={1}",key,value));
			base.OnInsertComplete (key, value);
		}

		protected override void OnRemoveComplete (object key, object value)
		{
			print(string.Format("Remove  key={0} value={1}",key,value));
			base.OnRemoveComplete (key, value);
		}
		
	}
	
	void Start () {
		TestDictionary tes = new TestDictionary();
		tes.Add("TestAdd",Vector3.one);
		tes.Add("A",Vector3.zero);
		tes.Add("B",Vector3.one);
		tes.Add("C",Vector3.down);
		tes.Add("D",Vector3.up);
		tes.Add("E",Vector3.right);
		tes.Remove("B");
		tes.Add("E",Vector3.right);
		
		foreach (DictionaryEntry item in tes) {
			print( string.Format("{0,-10}:{1}",item.Key,item.Value) );
		}
	}
}
