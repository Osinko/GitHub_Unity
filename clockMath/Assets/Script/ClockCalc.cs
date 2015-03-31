using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ClockCalc : MonoBehaviour
{
	
		/// <summary>
		///	指定した時刻の範囲で長針と短針が指定した角度関係になる時刻を出力する
		/// </summary>
		/// <returns>単位：時 hour　（分や秒は小数点に含まれている）</returns>
		/// <param name="time">時刻</param>
		/// <param name="deg">角度</param>
		public IEnumerable<float> ClockLine (int time, float deg)
		{
				for (int i = 0; i < time; i++) {
						IEnumerable<float> temp = ClockCalculate (i, deg);
						foreach (float item in temp) {
								yield return item + i;
						}
				}
		}
	
		//時単位の値を「○時○分○秒」のstring書式へと変換する。秒は小数点floatで出力
		string ClockTextConv (float time)
		{
				float hour = Mathf.Floor (time);
				time -= hour;
				//時間を分に
				float min = time * 60f;
				float minOut = Mathf.Floor (min);
		
				//秒を取り出す
				float sec = (min - minOut) * 60f;
		
				return string.Format ("{0}時{1}分{2:f6}秒", hour, minOut, sec);
		}
	
		//時計算する関数
		IEnumerable<float> ClockCalculate (int hour, float deg)
		{
				//午前、午後の補正
				float flipHour = hour >= 12 ? hour % 12 : hour;
		
				//時計の長針は１時間に360°
				//短針は30°進む。その差は330°でこれを使い旅人算する
				//逆角度も計算
				float time = (((30f * flipHour) + deg) % 360) / 330f;
				float reverseTime = (((30f * flipHour) + -deg) % 360) / 330f;
		
				//分の単位が60分を超え時を更新する必要がある場合は出力しない
				//又、マイナス分も時を更新する必要がある為、出力しない
				if (time >= 0 && time < 1) {
						yield return time;
				}
				//角度を反転した計算結果の値が重複しない有効な値な場合出力
				if (reverseTime >= 0 && reverseTime < 1 && time != reverseTime) {
						yield return reverseTime;
				}
		}
}
