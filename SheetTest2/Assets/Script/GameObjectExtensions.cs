using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public  class GameObjectExtensions : MonoBehaviour
{

}

/// <summary>
/// GameObject 型の拡張メソッドを管理するクラス
/// </summary>
public static class GameObjectExtension
{

		/// <summary>
		/// コンポーネントを取得します
		/// コンポーネントが存在しなければ追加してから取得します
		/// </summary>
		/// <typeparam name="T">取得するコンポーネントの型</typeparam>
		/// <param name="gameObject">GameObject型のインスタンス</param>
		/// <returns>コンポーネント</returns>
		public static T SafeGetComponent<T> (this GameObject gameObject) where T : Component
		{
				return 
			gameObject.GetComponent<T> () ?? 
						gameObject.AddComponent<T> ();
		}
	
		/// <summary>
		/// 指定されたゲームオブジェクトが null または非アクティブであるかどうかを示します
		/// </summary>
		public static bool IsNullOrInactive (this GameObject self)
		{
				return self == null || !self.activeInHierarchy || !self.activeSelf;
		}
	
		/// <summary>
		/// 指定されたコンポーネントがアタッチされているかどうかを返します
		/// </summary>
		public static bool HasComponent<T> (this GameObject self) where T : Component
		{
				return self.GetComponent<T> () != null;
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, GameObject target)
		{
				self.transform.LookAt (target.transform);
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, Transform target)
		{
				self.transform.LookAt (target);
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, Vector3 worldPosition)
		{
				self.transform.LookAt (worldPosition);
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, GameObject target, Vector3 worldUp)
		{
				self.transform.LookAt (target.transform, worldUp);
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, Transform target, Vector3 worldUp)
		{
				self.transform.LookAt (target, worldUp);
		}
	
		/// <summary>
		/// 向きを変更します
		/// </summary>
		public static void LookAt (this GameObject self, Vector3 worldPosition, Vector3 worldUp)
		{
				self.transform.LookAt (worldPosition, worldUp);
		}
	
		public static bool HasChild (this GameObject gameObject)
		{
				return 0 < gameObject.transform.childCount;
		}
}

public static partial class TransformExtensions
{
		public static bool HasChild (this Transform transform)
		{
				return 0 < transform.childCount;
		}
}




/// <summary>
/// Mathf クラスに関する汎用関数を管理するクラス
/// </summary>
public static class MathfUtils
{
		//ユークリッドの互除法
		public static float Gcd (float x, float y)
		{
				while (y!=0) {
						float z = x % y;
						x = y;
						y = z;
				}
				return x;
		}
	

		/// <summary>
		/// 値を入れ替えるジェネリック関数
		/// </summary>
		/// <param name="lhs">ref必要　左項</param>
		/// <param name="rhs">ref必要　右項</param>
		/// <typeparam name="T">ジェネリック</typeparam>
		public static void Swap<T> (ref T lhs, ref T rhs)
		{
				T temp;
				temp = lhs;
				lhs = rhs;
				rhs = temp;
		}


		///ブレゼンハムのアルゴリズム（４象限対応）
		///始点(x0, y0),終点(x1, y1);
		public static IEnumerable<Vector2>  BresenhamsLine (int x0, int y0, int x1, int y1)
		{
				//正=true,負＝false
				int a = ((y0 * y1) > 0) ? Mathf.Abs (Mathf.Abs (y0) - Mathf.Abs (y1)) : Mathf.Abs (y0) + Mathf.Abs (y1);
				bool incrementA = ((y0 - y1) > 0) ? incrementA = false : incrementA = true;
		
				int b = ((x0 * x1) > 0) ? Mathf.Abs (Mathf.Abs (x0) - Mathf.Abs (x1)) : Mathf.Abs (x0) + Mathf.Abs (x1);
				bool incrementB = ((x0 - x1) > 0) ? incrementB = false : incrementB = true;
		
				bool changeAB = false;
				if (a > b) {
						MathfUtils.Swap (ref a, ref b);
						MathfUtils.Swap (ref x0, ref y0);
						MathfUtils.Swap (ref incrementA, ref incrementB);
						changeAB = true;
				}
		
				int a2 = a << 2;
				int b2 = b << 2;
				int D = -b;
		
				for (int x = 0,y = 0; x <= b; x++) {
						if (D > 0) {
								y = y + 1;
								D -= b2;
						}
						D += a2;
						if (changeAB) {
								yield return new Vector2 (y0 + (incrementA ? y : -y), x0 + (incrementB ? x : -x));
						} else {
								yield return new Vector2 (x0 + (incrementB ? x : -x), y0 + (incrementA ? y : -y));
						}
				}
		}


		/// <summary>
		/// ベジェ曲線における X 座標を返します
		/// </summary>
		/// <remarks>http://opentype.jp/fontguide_doc3.htm</remarks>
		/// <param name="x1">開始点の X 座標</param>
		/// <param name="x2">制御点 1 の X 座標</param>
		/// <param name="x3">制御点 2 の X 座標</param>
		/// <param name="x4">終点の X 座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>ベジェ曲線における X 座標</returns>
		public static float BezierCurveX (float x1, float x2, float x3, float x4, float t)
		{
				return Mathf.Pow (1 - t, 3) * x1 + 3 * Mathf.Pow (1 - t, 2) * t * x2 + 3 * (1 - t) * Mathf.Pow (t, 2) * x3 + Mathf.Pow (t, 3) * x4;
		}
	
		/// <summary>
		/// ベジェ曲線における Y 座標を返します
		/// </summary>
		/// <remarks>http://opentype.jp/fontguide_doc3.htm</remarks>
		/// <param name="y1">開始点の Y 座標</param>
		/// <param name="y2">制御点 1 の Y 座標</param>
		/// <param name="y3">制御点 2 の Y 座標</param>
		/// <param name="y4">終点の Y 座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>ベジェ曲線における Y 座標</returns>
		public static float BezierCurveY (float y1, float y2, float y3, float y4, float t)
		{
				return Mathf.Pow (1 - t, 3) * y1 + 3 * Mathf.Pow (1 - t, 2) * t * y2 + 3 * (1 - t) * Mathf.Pow (t, 2) * y3 + Mathf.Pow (t, 3) * y4;
		}
	
		/// <summary>
		/// ベジェ曲線における 2 次元座標を返します
		/// </summary>
		/// <param name="p1">開始点の座標</param>
		/// <param name="p2">制御点 1 の座標</param>
		/// <param name="p3">制御点 2 の座標</param>
		/// <param name="p4">終点の座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>B-スプライン曲線における 2 次元座標</returns>
		public static Vector2 BezierCurve (Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float t)
		{
				return new Vector2 (
			BezierCurveX (p1.x, p2.x, p3.x, p4.x, t), 
			BezierCurveY (p1.y, p2.y, p3.y, p4.y, t));
		}
	
	
		/// <summary>
		/// B-スプライン曲線における X 座標を返します
		/// </summary>
		/// <remarks>http://opentype.jp/fontguide_doc2.htm</remarks>
		/// <param name="x1">開始点の X 座標</param>
		/// <param name="x2">制御点の X 座標</param>
		/// <param name="x3">終点の X 座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>B-スプライン曲線における X 座標</returns>
		public static float B_SplineCurveX (float x1, float x2, float x3, float t)
		{
				return (1 - t) * (1 - t) * x1 + 2 * t * (1 - t) * x2 + t * t * x3;
		}
	
		/// <summary>
		/// B-スプライン曲線における Y 座標を返します
		/// </summary>
		/// <remarks>http://opentype.jp/fontguide_doc2.htm</remarks>
		/// <param name="y1">開始点の Y 座標</param>
		/// <param name="y2">制御点の Y 座標</param>
		/// <param name="y3">終点の Y 座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>B-スプライン曲線における Y 座標</returns>
		public static float B_SplineCurveY (float y1, float y2, float y3, float t)
		{
				return (1 - t) * (1 - t) * y1 + 2 * t * (1 - t) * y2 + t * t * y3;
		}
	
		/// <summary>
		/// B-スプライン曲線における 2 次元座標を返します
		/// </summary>
		/// <param name="p1">開始点の座標</param>
		/// <param name="p2">制御点の座標</param>
		/// <param name="p3">終点の座標</param>
		/// <param name="t">重み(0 から 1)</param>
		/// <returns>B-スプライン曲線における 2 次元座標</returns>
		public static Vector2 B_SplineCurveY (Vector2 p1, Vector2 p2, Vector2 p3, float t)
		{
				return new Vector2 (
			B_SplineCurveX (p1.x, p2.x, p3.x, t), 
			B_SplineCurveY (p1.y, p2.y, p3.y, t));
		}
}


/// <summary>
/// IList 型の拡張メソッドを管理するクラス
/// </summary>
public static partial class IListExtensions
{
		/// <summary>
		/// ランダムにリスト内の要素を返します
		/// </summary>
		/// <typeparam name="T">リスト要素の型</typeparam>
		/// <param name="self">リスト</param>
		/// <returns>リスト内の要素</returns>
		public static T GetRandom<T> (this IList<T> self)
		{
				return self [UnityEngine.Random.Range (0, self.Count)];
		}
}



/// <summary>
/// string 型の拡張メソッドを管理するクラス
/// </summary>
public static partial class StringExtensions
{
		/// <summary>
		/// 指定された文字列で区切られた部分文字列を格納する文字列配列を返します
		/// </summary>
		public static string[] Split (this string self, string separator)
		{
				return self.Split (new []{ separator }, StringSplitOptions.None);
		}
	
		/// <summary>
		/// 指定された文字列で区切られた部分文字列を格納する文字列配列を返します
		/// </summary>
		public static string[] Split (this string self, string separator, StringSplitOptions options)
		{
				return self.Split (new []{ separator }, options);
		}
	
		/// <summary>
		/// 指定された文字列の配列の要素で区切られた部分文字列を格納する文字列配列を返します
		/// </summary>
		public static string[] Split (this string self, params string[] separator)
		{
				return self.Split (separator, StringSplitOptions.None);
		}
}

