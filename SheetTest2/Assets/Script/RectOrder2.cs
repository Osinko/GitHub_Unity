using UnityEngine;
using System.Collections;

public class RectOrder2 : MonoBehaviour
{
		public static Vector4 BresenhamsRect (int order, int width, int height)
		{
				width = Mathf.Abs (width);
				height = Mathf.Abs (height);
				//計算距離を２倍延長
				//基本的に配置するエレメント要素の数より面積の方が爆発的に増えてゆくので常識的にこれで充分
				do {
						width *= 2;
						height *= 2;
				} while (order > width * height);

				int x0 = 0, y0 = 0;
				int x1 = width;
				int y1 = height;

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
		
				int a2 = a << 1;
				int b2 = b << 1;
				int D = -b;

				Vector4 ret = Vector4.zero;
				int w = 0, h = 0, z = 0;
				for (int x = 0,y = 0; x <= b; x++) {
						if (D > 0) {
								y = y + 1;
								D -= b2;
						}
						D += a2;
						if (changeAB) {
								w = y0 + (incrementA ? y : -y);
								h = x0 + (incrementB ? x : -x);
								z = w * h;
						} else {
								w = x0 + (incrementB ? x : -x);
								h = y0 + (incrementA ? y : -y);
								z = w * h;
						}
						if (z > order) {
								ret = new Vector4 (w, h, 0, 0);	//計算途中でオーダーを満たす面積を発見したらそこで終了する
								break;
						}
				}
				ret = new Vector4 (w, h, 0, 0);	//ありえないが一応
				return ret;
		}
}
