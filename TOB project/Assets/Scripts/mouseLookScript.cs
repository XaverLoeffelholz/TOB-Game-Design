using UnityEngine;
using System.Collections;

public class mouseLookScript : MonoBehaviour {
	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public bool clampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;
	public bool smooth;
	public float smoothTime = 5f;

	private Quaternion m_CharacterTargetRot;
	private Quaternion m_CameraTargetRot;
	private GameObject player;
	private GameObject mainCamera;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		m_CharacterTargetRot = player.transform.localRotation;
		m_CameraTargetRot = mainCamera.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		// get the mouse location
		float yRot = Input.GetAxis("Mouse X") * XSensitivity;
		float xRot = Input.GetAxis("Mouse Y") * YSensitivity;
		
		m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
		m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);
		
		if(clampVerticalRotation)
			m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);
		
		if(smooth)
		{
			player.transform.localRotation = Quaternion.Slerp (player.transform.localRotation, m_CharacterTargetRot,
			                                            smoothTime * Time.deltaTime);
			mainCamera.transform.localRotation = Quaternion.Slerp (mainCamera.transform.localRotation, m_CameraTargetRot,
			                                         smoothTime * Time.deltaTime);
		}
		else
		{
			player.transform.localRotation = m_CharacterTargetRot;
			mainCamera.transform.localRotation = m_CameraTargetRot;
		}
	}

	Quaternion ClampRotationAroundXAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;
		
		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);
		
		angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);
		
		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);
		
		return q;
	}
}
