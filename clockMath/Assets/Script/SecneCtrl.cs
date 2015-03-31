using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SecneCtrl : MonoBehaviour
{
		//コンポーネント
		ClockCalc clockCalc; 
		Sheet sheet;

		//シート本体を操作するデータ
		float[] timeList;
		float time, deg, sheetWidth, sheetHeight;

		//更新検出用
		float preTime, preDeg, preSheetWidth, preSheetHeight;	
		bool preToggleDrawBox;

		//GUI用
		string degtex;
		bool toggleDrawBox;

		void Awake ()
		{
				clockCalc = GetComponent<ClockCalc> () as ClockCalc;
				sheet = GetComponent<Sheet> () as Sheet;
				time = 12;
				deg = 180;
				sheetWidth = 12;
				sheetHeight = 6;
				toggleDrawBox = true;
		}
		
		void OnGUI ()
		{
				time = GUI.HorizontalSlider (new Rect (20, 20, 600, 30), time, 1f, 12f);
				deg = GUI.HorizontalSlider (new Rect (20, 60, 600, 30), deg, 0f, 359f);
				degtex = GUI.TextField (new Rect (650, 60, 100, 20), deg.ToString (), 25);
				if (degtex == "") {
						deg = 0;
						degtex = "0";
				} else {
						deg = float.Parse (degtex);
				}
				toggleDrawBox = GUI.Toggle (new Rect (650, 140, 680, 30), toggleDrawBox, "描画領域表示");
				sheetWidth = GUI.HorizontalSlider (new Rect (20, 100, 600, 30), sheetWidth, 1f, 50f);
				sheetHeight = GUI.HorizontalSlider (new Rect (20, 140, 600, 30), sheetHeight, 1f, 50f);
		}

		void Update ()
		{
				if (preTime != time || preDeg != deg || preSheetHeight != sheetHeight || preSheetWidth != sheetWidth || preToggleDrawBox != toggleDrawBox) {
						timeList = clockCalc.ClockLine (Mathf.CeilToInt (time), deg).ToArray ();
						ClockContorl[] clockContorl = sheet.SetSheet (timeList.Length, sheetWidth, sheetHeight)
							.Select (x => x.GetComponent<ClockContorl> ()).ToArray ();
						for (int i = 0; i < clockContorl.Length; i++) {
								clockContorl [i].SetTime (timeList [i]);
						}

						if (toggleDrawBox) {
								sheet.DrawBoxVisible ();
						} else {
								sheet.DrawBoxInvisible ();
						}

						preTime = time;
						preDeg = deg;
						preSheetWidth = sheetWidth;
						preSheetHeight = sheetHeight;
						preToggleDrawBox = toggleDrawBox;
				}
		}
}
