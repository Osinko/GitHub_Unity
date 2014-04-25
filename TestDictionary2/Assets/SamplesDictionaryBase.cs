using UnityEngine;
using System.Collections;
using System;

public class SamplesDictionaryBase : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Creates and initializes a new DictionaryBase.
		ShortStringDictionary mySSC = new ShortStringDictionary();
		
		// Adds elements to the collection.
		mySSC.Add( "One", "a" );
		mySSC.Add( "Two", "ab" );
		mySSC.Add( "Three", "abc" );
		mySSC.Add( "Four", "abcd" );
		mySSC.Add( "Five", "abcde" );
		
		// Display the contents of the collection using foreach. This is the preferred method.
		Console.WriteLine( "Contents of the collection (using foreach):" );
		PrintKeysAndValues1( mySSC );
		
		// Display the contents of the collection using the enumerator.
		Console.WriteLine( "Contents of the collection (using enumerator):" );
		PrintKeysAndValues2( mySSC );
		
		// Display the contents of the collection using the Keys property and the Item property.
		Console.WriteLine( "Initial contents of the collection (using Keys and Item):" );
		PrintKeysAndValues3( mySSC );
		
		// Tries to add a value that is too long.
		try  {
			mySSC.Add( "Ten", "abcdefghij" );
		}
		catch ( ArgumentException e )  {
			Console.WriteLine( e.ToString() );
		}
		
		// Tries to add a key that is too long.
		try  {
			mySSC.Add( "Eleven", "ijk" );
		}
		catch ( ArgumentException e )  {
			Console.WriteLine( e.ToString() );
		}
		
		Console.WriteLine();
		
		// Searches the collection with Contains.
		Console.WriteLine( "Contains \"Three\": {0}", mySSC.Contains( "Three" ) );
		Console.WriteLine( "Contains \"Twelve\": {0}", mySSC.Contains( "Twelve" ) );
		Console.WriteLine();
		
		// Removes an element from the collection.
		mySSC.Remove( "Two" );
		
		// Displays the contents of the collection.
		Console.WriteLine( "After removing \"Two\":" );
		PrintKeysAndValues1( mySSC );
		
	}
	
	// Uses the foreach statement which hides the complexity of the enumerator.
	// NOTE: The foreach statement is the preferred way of enumerating the contents of a collection.
	public static void PrintKeysAndValues1( ShortStringDictionary myCol )  {
		foreach ( DictionaryEntry myDE in myCol )
			Console.WriteLine( "   {0,-5} : {1}", myDE.Key, myDE.Value );
		Console.WriteLine();
	}
	
	// Uses the enumerator. 
	// NOTE: The foreach statement is the preferred way of enumerating the contents of a collection.
	public static void PrintKeysAndValues2( ShortStringDictionary myCol )  {
		DictionaryEntry myDE;
		System.Collections.IEnumerator myEnumerator = myCol.GetEnumerator();
		while ( myEnumerator.MoveNext() )
		if ( myEnumerator.Current != null )  {
			myDE = (DictionaryEntry) myEnumerator.Current;
			Console.WriteLine( "   {0,-5} : {1}", myDE.Key, myDE.Value );
		}
		Console.WriteLine();
	}
	
	// Uses the Keys property and the Item property.
	public static void PrintKeysAndValues3( ShortStringDictionary myCol )  {
		ICollection myKeys = myCol.Keys;
		foreach ( String k in myKeys )
			Console.WriteLine( "   {0,-5} : {1}", k, myCol[k] );
		Console.WriteLine();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
