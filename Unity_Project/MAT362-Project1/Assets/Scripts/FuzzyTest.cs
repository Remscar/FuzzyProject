using UnityEngine;
using System.Collections;
using System;

public class FuzzyTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate()
  {
    var triTest = FuzzyNumber.Triangular(0, 10, 20);
    var trapTest = FuzzyNumber.Trapezoidal(-20, -10, 0, 10);

    if (Input.GetKeyDown(KeyCode.Space))
    {
      DrawFuzzyNumber(trapTest, -20, 10, new Color(0, 1, 0));
    }

    //DrawFuzzyNumber(triTest, 0, 20, new Color(1, 0, 0));



  }

  void DrawFuzzyNumber(Func<float, float> num, float min, float max, Color c)
  {
    Vector3 position = gameObject.transform.position;
    Vector3 scale = gameObject.transform.localScale;

    float step = 1;

    float scaleX = scale.x / (max - min);
    float scaleY = scale.y;

    float y = 0.0f;
    float x0 = min;
    float lastY = num(x0);

    x0 += step;

    for (; x0 <= max; x0 += step)
    {
      y = num(x0);

      Debug.Log(x0.ToString() + "=" + y.ToString());

      Debug.DrawLine(position + new Vector3((x0 - step) * scaleX, lastY * scaleY, 0),
        position + new Vector3(x0 * scaleX, y * scaleY,0), c);

      lastY = y;
    }


  }
}
