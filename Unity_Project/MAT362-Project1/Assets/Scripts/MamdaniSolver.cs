using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MamdaniSolver
{
  private float mMinimum;
  private float mMaximum;

  private List<Func<float, float>> mConsequences = new List<Func<float, float>>();
  private List<Func<float, float>> mAntecedents = new List<Func<float, float>>();

  public MamdaniSolver()
  {
    mMinimum = float.MaxValue;
    mMaximum = -float.MaxValue;
  }

  public float Solve(float x0)
  {
    List<float> firingLevels = new List<float>();

    foreach (var antecedent in mAntecedents)
    {
      firingLevels.Add(antecedent(x0));
    }
    float delta = (mMaximum - mMinimum) / 100.0f;
    float acc = 0.0f;

    float sumTop = 0.0f;
    float sumBot = 0.0f;

    for (int i = 0; i < 100; ++i)
    {
      float max = GetMax(firingLevels, mMinimum + acc);
      acc += delta;

      sumTop += max * acc * delta;
      sumBot += max * delta;
    }

    return sumTop / sumBot;
  }

  public float GetMax(List<float> firingLevels, float x)
  {
    float result = 0.0f;

    for (int i = 0; i < mConsequences.Count; ++i)
    {
      result = Math.Max(Math.Min(mConsequences[i](x), firingLevels[i]), result);
    }

    return result;
  }

  public void AddConsequence(Func<float,float> consequence, float min, float max)
  {
    mConsequences.Add(consequence);
    mMinimum = Math.Min(min, mMinimum);
    mMaximum = Math.Max(max, mMaximum);
  }

  public void AddAntecedent(Func<float, float> antecedent)
  {
    mAntecedents.Add(antecedent);
  }
}
