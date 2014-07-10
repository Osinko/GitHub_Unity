using UnityEngine;
using System.Collections;

using System.Linq;
using System.Text.RegularExpressions;

public class ClockContorl : MonoBehaviour
{
		//時間をstringで指定すると時計がその時間を指すように作っている。逐次更新を監視
		public string timeText;
		public  float time; 
		public Transform _transform;
		public bool death;

		Transform hourHand;
		Transform minuteHand;
		TextMesh timeTextMesh;
		bool changeFlag;

		//更新監視用
		string timeTextPrev;
		float timePrev;

		float hour;
		float min;
		float sec;

		void Awake ()
		{
				death = false;
				_transform = transform;
				Transform[] child = GetComponentsInChildren<Transform> ();
				timeTextMesh = GetComponentInChildren<TextMesh> ();
				hourHand = child.Where (x => x.name == "HourHand").Single ();		//Linq：子から欲しいオブジェクトネームのTransformを得る
				minuteHand = child.Where (x => x.name == "MinuteHand").Single ();
				timeText = "0時0分0.00秒";
				timeTextPrev = timeText;
				time = 0;
				timePrev = time;
				changeFlag = false;
		}
	
		void Update ()
		{
				//自爆装置
				if (death) {
						Destroy (gameObject);
				}

				//更新監視
				TimeChangeCheck ();
				TimeTextChangeCheck ();

				//時計表示の更新
				if (changeFlag) {
						hourHand.eulerAngles = new Vector3 (0, 0, (-360f / 12f) * TotalTime / 3600f);
						minuteHand.eulerAngles = new Vector3 (0, 0, (-360f / 60f) * TotalTime / 60f);
						timeTextMesh.text = TotalTimeText;
						changeFlag = false;
				}
		}

		void TimeChangeCheck ()
		{
				if (time != timePrev) {
						timePrev = time;
						SetTime (time);
				}
		}

		void TimeTextChangeCheck ()
		{
				if (timeText != timeTextPrev) {
						timeTextPrev = timeText;
						SetTimeString (timeText);
				}
		}

		/// <summary>
		/// 小数点付のfloatで時間を受け取る
		/// </summary>
		/// <param name="time">Time.</param>
		public void SetTime (float time)
		{
				float hour = Mathf.Floor (time);
				time -= hour;
				//時間を分に
				float min = time * 60f;
				float minOut = Mathf.Floor (min);
		
				//秒を取り出す
				float sec = (min - minOut) * 60f;
				ChangeTime (hour, minOut, sec);
		}

		/// <summary>
		/// 文章指定による時間の受け取り
		/// </summary>
		/// <param name="time">Time.</param>
		public void SetTimeString (string time)
		{
				MatchCollection mc;
				mc = Regex.Matches (time, @"\d{1,}(?=時)|\d{1,}(?=分)|\d\d*\.\d{1,}(?=秒)");	//正規表現を使いスクリプトstringオブジェクトから値を翻訳
				if (mc.Count >= 3) {
						float hour = float.Parse (mc [0].Value);
						float min = float.Parse (mc [1].Value);
						float sec = float.Parse (mc [2].Value);	//秒は小数点込の値でないと反応しなくなっているので注意
						ChangeTime (hour, min, sec);
				}
		}

		void ChangeTime (float hour, float min, float sec)
		{
				this.hour = hour;
				this.min = min;
				this.sec = sec;

				changeFlag = true;
		}

		public float TotalTime {
				get{ return (60f * 60f * hour) + (60f * min) + sec;}
		}
	
		public string TotalTimeText {
				get{ return string.Format ("{0}時{1}分{2:f2}秒", hour, min, sec);}
		}
}
