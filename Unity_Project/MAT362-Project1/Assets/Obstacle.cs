using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour
{

  public float m_moveSpeed = 1.0f;
  public float m_moveTime = 3.0f;
  public Vector3 m_moveDir;

  //private Vector3 m_startPos;
  private Vector3 m_curMoveDir;

  private float m_timer;
  // Use this for initialization

  void Start()
  {
    //m_startPos = GetComponent<Transform>().position;
    m_curMoveDir = -m_moveDir.normalized;
    ChangeDirection();
  }

  void ChangeDirection()
  {
    m_timer = m_moveTime;
    m_curMoveDir = -m_curMoveDir;
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    var curPos = GetComponent<Transform>().position;
    GetComponent<Transform>().position = curPos + m_curMoveDir * m_moveSpeed * Time.deltaTime;

    m_timer -= Time.deltaTime;
    if (m_timer <= 0)
      ChangeDirection();
  }
}
