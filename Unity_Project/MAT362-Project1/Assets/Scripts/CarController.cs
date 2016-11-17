using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

  private RaycastHit? leftCast
  {
    get
    {
      return m_leftHit ? m_left : (RaycastHit?)null;
    }
  }
  private RaycastHit? forwardCast
  {
    get
    {
      return m_forwardHit ? m_forward : (RaycastHit?)null;
    }
  }
  private RaycastHit? rightCast
  {
    get
    {
      return m_rightHit ? m_right : (RaycastHit?)null;
    }
  }

  RaycastHit m_left = new RaycastHit();
  bool m_leftHit = false;
  RaycastHit m_forward = new RaycastHit();
  bool m_forwardHit = false;
  RaycastHit m_right = new RaycastHit();
  bool m_rightHit = false;

  public float m_angleCastLength;
  public float m_forwardCastLength;

  public float m_sensorAngle;

  public float m_maxForwardSpeed;

  public float m_maxTurnAngle;

  private float m_curSpeed = 0;
  private float m_curTurnAngle = 0;


  // Use this for initialization
  void Start()
  {
    m_curSpeed = m_maxForwardSpeed;
	}
	

	void FixedUpdate()
  {
    CastRays();

    Decide();

    Turn();
    Move();
  }

  void CastRays()
  {
    Vector3 scale = gameObject.transform.localScale;
    Vector3 position = gameObject.transform.position;
    Vector3 forward = gameObject.transform.forward.normalized;
    Vector3 up = gameObject.transform.up.normalized;
    Vector3 right = gameObject.transform.right.normalized;

    Vector3 forwardPosition = position + forward * (scale.z / 2.0f);
    Vector3 leftPosition = forwardPosition - right * (scale.x / 2.0f);
    Vector3 rightPosition = forwardPosition + right * (scale.x / 2.0f);

    // Left
    m_leftHit = Physics.Raycast(new Ray(leftPosition, Quaternion.AngleAxis(-m_sensorAngle, up) * forward), out m_left, m_angleCastLength);
    if (m_leftHit)
    {
      Debug.DrawLine(leftPosition, m_left.point, new Color(1, 0, 0));
    }
    else
    {
      Debug.DrawLine(leftPosition, leftPosition + Quaternion.AngleAxis(-m_sensorAngle, up) * forward * m_angleCastLength, new Color(0, 1, 0));
    }


    // Forward
    m_forwardHit = Physics.Raycast(new Ray(forwardPosition, forward), out m_forward, m_forwardCastLength);
    if (m_forwardHit)
    {
      Debug.DrawLine(forwardPosition, m_forward.point, new Color(1, 0, 0));
    }
    else
    {
      Debug.DrawLine(forwardPosition, forwardPosition + forward * m_forwardCastLength, new Color(0, 1, 0));
    }


    // Right
    m_rightHit = Physics.Raycast(new Ray(rightPosition, Quaternion.AngleAxis(m_sensorAngle, up) * forward), out m_right, m_angleCastLength);
    if (m_rightHit)
    {
      Debug.DrawLine(rightPosition, m_right.point, new Color(1, 0, 0));
    }
    else
    {
      Debug.DrawLine(rightPosition, rightPosition + Quaternion.AngleAxis(m_sensorAngle, up) * forward * m_angleCastLength, new Color(0, 1, 0));
    }


  }

  void Decide()
  {
    bool turnSet = false;

    if (forwardCast != null)
    {
      m_curSpeed = m_maxForwardSpeed / 4;
      m_curTurnAngle = -m_maxTurnAngle;
      turnSet = true;
      //Debug.Log("Front Hit Distance:" + forwardCast.Value.distance.ToString());
    }
    else
    {
      m_curSpeed = m_maxForwardSpeed;
    }

    if (leftCast != null)
    {
      //Debug.Log("Hit Left:" + leftCast.Value.distance.ToString());
      m_curTurnAngle = m_maxTurnAngle;
      turnSet = true;
    }

    if (rightCast != null)
    {
      //Debug.Log("Hit Right:" + rightCast.Value.distance.ToString());
      m_curTurnAngle = -m_maxTurnAngle;
      turnSet = true;
    }

    if (!turnSet)
    {
      m_curTurnAngle = 0;
    }

  }

  void Turn()
  {
    Vector3 scale = gameObject.transform.localScale;
    Vector3 position = gameObject.transform.position;
    Vector3 forward = gameObject.transform.forward.normalized;
    Vector3 up = gameObject.transform.up.normalized;

    Vector3 pivot = position - forward * (scale.z / 4.0f);

    GetComponent<Transform>().RotateAround(pivot, up, m_curTurnAngle * Time.deltaTime);
  }

  void Move()
  {
    float speedScaling = 0.1f;

    Vector3 forward = gameObject.transform.forward.normalized;
    
    GetComponent<Rigidbody>().velocity = forward * m_curSpeed * speedScaling;
  }
}
