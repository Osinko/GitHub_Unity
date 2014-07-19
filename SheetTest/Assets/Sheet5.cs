using UnityEngine;
using System.Collections;

public class Sheet5 : MonoBehaviour
{
	
	void Start ()
	{
		Line3 (0, 0, -5, -3);
	}
	
	void Line3 (int x0, int y0, int x1, int y1)
	{
		int dx = x1 - x0;
		int dy = y1 - y0;
		int dxQuadrant = 1;
		int dyQuadrant = 1;
	
		int dx2 = 2 * dx;
		int dy2 = 2 * dy;
		
		int D = -dx;
		int y = y0;
		
		for (int x = x0; x <= x1; x++) {
			if (D > 0) {
				y = y + 1;
				D -= dx2;
			}
			D += dy2;
			print (string.Format ("({0},{1}) :D={2}", (x * dxQuadrant), (y * dyQuadrant), D));
		}
	}
}
