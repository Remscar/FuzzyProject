using UnityEngine;
using System.Collections;
using System;

public static class FuzzyNumber
{

  public static Func<float, float> Trapezoidal(float a, float b, float c, float d)
  {
    return (float x0) =>
    {
      if (x0 >= a && x0 < b)
        return (x0 - a) / (b - a);

      // if b <= x <= c
      else if (x0 >= b && x0 <= c)
        return 1.0f;

      // if c < x <= d
      else if (x0 > c && x0 <= d)
        return (d - x0) / (d - c);

      // 0 otherwise
      else
        return 0.0f;
    };
  }

  public static Func<float, float> Triangular(float a, float b, float c)
  {
    return Trapezoidal(a, b, b, c);
  }

  public static Func<float, float> LinearUp(float a, float b)
  {
    return (float x0) =>
    {
      if (x0 < a)
        return 0.0f;

      if (x0 >= b)
        return 1.0f;

      return (x0 - a) / (b - a);
    };
  }

  public static Func<float, float> LinearDown(float a, float b)
  {
    return (float x0) =>
    {
      if (x0 < a)
        return 1.0f;

      if (x0 >= b)
        return 0.0f;

      return (b - x0) / (b - a);
    };
  }


};
