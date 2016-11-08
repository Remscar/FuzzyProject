using UnityEngine;
using System.Collections;

public class FuzzyNumber {
    public float m_a, m_b, m_c, m_d;

    public FuzzyNumber(float a, float b, float c, float d)
    {
        m_a = a;
        m_b = b;
        m_c = c;
        m_d = d;
    }

    public FuzzyNumber(float a, float b, float c) : this(a, b, b, c) { }

    public float FiringLevel(float x0)
    {
        // if a <= x < b
        if (x0 >= m_a && x0 < m_b)
            return (x0 - m_a) / (m_b - m_a);

        // if b <= x <= c
        else if (x0 >= m_b && x0 <= m_c)
            return 1.0f;

        // if c < x <= d
        else if (x0 > m_c && x0 <= m_d)
            return (m_d - x0) / (m_d - m_c);

        // 0 otherwise
        else
            return 0.0f;
    }
}
