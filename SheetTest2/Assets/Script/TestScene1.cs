using UnityEngine;
using System.Collections;

public class TestScene1 : MonoBehaviour
{
		public NewSheet2 sheetPref;
		NewSheet2 newSheet;

		int order = 14;
		int sheetWidth = 15;
		int sheetHeight = 10;
		int preOrder = 0;
		int preSheetWidth = 0;
		int preSheetHeight = 0;

		void Awake ()
		{
				newSheet = Instantiate (sheetPref) as NewSheet2;
		}

		void OnGUI ()
		{
				order = (int)GUI.HorizontalSlider (new Rect (20, 20, 600, 30), order, 1f, 300f);
				sheetWidth = (int)GUI.HorizontalSlider (new Rect (20, 60, 600, 30), sheetWidth, 1f, 50);
				sheetHeight = (int)GUI.HorizontalSlider (new Rect (20, 100, 600, 30), sheetHeight, 1f, 50f);
		}
	
		void Update ()
		{
				if (order != preOrder || preSheetHeight != sheetHeight || preSheetWidth != sheetWidth) {
						newSheet.SetSheet (order, sheetWidth, sheetHeight);
				}
		}
}