using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

    RaycastHit m_left = new RaycastHit();
    RaycastHit m_forward = new RaycastHit();
    RaycastHit m_right = new RaycastHit();

    public float m_sensorAngle;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 scale = gameObject.transform.localScale;
        Vector3 position = gameObject.transform.position;
        Vector3 forward = gameObject.transform.forward.normalized;
        Vector3 up = gameObject.transform.up.normalized;
        Vector3 right = gameObject.transform.right.normalized;

        Vector3 forwardPosition = position + forward * (scale.z / 2.0f);
        Vector3 leftPosition = forwardPosition - right * (scale.x / 2.0f);
        Vector3 rightPosition = forwardPosition + right * (scale.x / 2.0f);

        // Left
        Physics.Raycast(new Ray(leftPosition, Quaternion.AngleAxis(-m_sensorAngle, up) * forward), out m_left);
        Debug.DrawLine(leftPosition, m_left.point, new Color(1,0,0));

        // Forward
        Physics.Raycast(new Ray(forwardPosition, forward), out m_forward);
        Debug.DrawLine(forwardPosition, m_forward.point, new Color(1, 0, 0));

        // Right
        Physics.Raycast(new Ray(rightPosition, Quaternion.AngleAxis(m_sensorAngle, up) * forward), out m_right);
        Debug.DrawLine(rightPosition, m_right.point, new Color(1, 0, 0));
    }
}
