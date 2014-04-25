using UnityEngine;
using System.Collections;
using System;

public class ShortStringDictionary : DictionaryBase {
		
		public String this[ String key ]  {
			get  {
				return( (String) Dictionary[key] );
			}
			set  {
				Dictionary[key] = value;
			}
		}
		
		public ICollection Keys  {
			get  {
				return( Dictionary.Keys );
			}
		}
		
		public ICollection Values  {
			get  {
				return( Dictionary.Values );
			}
		}
		
		public void Add( String key, String value )  {
			Dictionary.Add( key, value );
		}
		
		public bool Contains( String key )  {
			return( Dictionary.Contains( key ) );
		}
		
		public void Remove( String key )  {
			Dictionary.Remove( key );
		}
		
		protected override void OnInsert( System.Object key, System.Object value )  {
			if ( key.GetType() != typeof(System.String) )
				throw new ArgumentException( "key must be of type String.", "key" );
			else  {
				String strKey = (String) key;
				if ( strKey.Length > 5 )
					throw new ArgumentException( "key must be no more than 5 characters in length.", "key" );
			}
			
			if ( value.GetType() != typeof(System.String) )
				throw new ArgumentException( "value must be of type String.", "value" );
			else  {
				String strValue = (String) value;
				if ( strValue.Length > 5 )
					throw new ArgumentException( "value must be no more than 5 characters in length.", "value" );
			}
		}
		
		protected override void OnRemove( System.Object key, System.Object value )  {
			if ( key.GetType() != typeof(System.String) )
				throw new ArgumentException( "key must be of type String.", "key" );
			else  {
				String strKey = (String) key;
				if ( strKey.Length > 5 )
					throw new ArgumentException( "key must be no more than 5 characters in length.", "key" );
			}
		}
		
		protected override void OnSet( System.Object key, System.Object oldValue, System.Object newValue )  {
			if ( key.GetType() != typeof(System.String) )
				throw new ArgumentException( "key must be of type String.", "key" );
			else  {
				String strKey = (String) key;
				if ( strKey.Length > 5 )
					throw new ArgumentException( "key must be no more than 5 characters in length.", "key" );
			}
			
			if ( newValue.GetType() != typeof(System.String) )
				throw new ArgumentException( "newValue must be of type String.", "newValue" );
			else  {
				String strValue = (String) newValue;
				if ( strValue.Length > 5 )
					throw new ArgumentException( "newValue must be no more than 5 characters in length.", "newValue" );
			}
		}
		
		protected override void OnValidate( System.Object key, System.Object value )  {
			if ( key.GetType() != typeof(System.String) )
				throw new ArgumentException( "key must be of type String.", "key" );
			else  {
				String strKey = (String) key;
				if ( strKey.Length > 5 )
					throw new ArgumentException( "key must be no more than 5 characters in length.", "key" );
			}
			
			if ( value.GetType() != typeof(System.String) )
				throw new ArgumentException( "value must be of type String.", "value" );
			else  {
				String strValue = (String) value;
				if ( strValue.Length > 5 )
					throw new ArgumentException( "value must be no more than 5 characters in length.", "value" );
			}
		}
}

