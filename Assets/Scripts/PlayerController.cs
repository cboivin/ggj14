using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public Camera m_MainCamera;
	public Transform m_CursorObject;

	public float m_Speed = 2f;
	public float m_DashSpeed = 10f;
	public float m_DashDuration = 0.3f;
	public float m_DashCooldown = 0.2f;

	private float m_CurrentDashDuration = 0;
	private float m_CurrentDashCooldown = 0;
	private Vector3 m_CurrentDashPosition;

	private Plane m_PlayerMovementPlane;
	private Transform m_Transform;

	void Start ()
	{
		m_Transform = transform;
		m_PlayerMovementPlane = new Plane(m_Transform.forward, m_Transform.position);
	}

	void Update ()
	{
		Vector3 cursorScreenPosition  = Input.mousePosition;
		Vector3 cursorWorldPosition = ScreenPointToWorldPointOnPlane (cursorScreenPosition, m_PlayerMovementPlane, m_MainCamera);

		if (m_CurrentDashDuration == 0)
		{
			/*m_CurrentDashCooldown = Mathf.Max(0, m_CurrentDashCooldown - Time.deltaTime);
			
			if (m_CurrentDashCooldown == 0 && Input.GetAxis("Fire1") > 0)
			{
				m_CurrentDashDuration = m_DashDuration;
				m_CurrentDashPosition = cursorWorldPosition;
				m_CurrentDashCooldown = m_DashCooldown;
			}*/
		}

		Vector3 targetPosition = cursorWorldPosition;
		float speed = m_Speed;
		if (m_CurrentDashDuration > 0)
		{
			speed = m_DashSpeed;
			targetPosition = m_CurrentDashPosition;
		}

		Vector3 distance = targetPosition - m_Transform.position;

		if (m_CursorObject != null)
		{
			Vector2 fromVector = new Vector2(1, 0);
			Vector2 toVector = cursorWorldPosition-m_Transform.position;

			float angle = Vector2.Angle(fromVector, toVector);
			Vector3 cross = Vector3.Cross(fromVector, toVector);

			if (cross.z < 0)
			{
				angle = 360 - angle;
			}

			m_CursorObject.localEulerAngles = new Vector3(0, 0, angle);
		}

		if (distance.magnitude > 0.1f)
		{
			m_Transform.position += distance.normalized * Mathf.Min(distance.magnitude, 5) * speed * Time.deltaTime;
		}
		else
		{
			m_CurrentDashDuration = 0;
		}


		m_CurrentDashDuration = Mathf.Max(0, m_CurrentDashDuration - Time.deltaTime);

		/*float halfWidth = Screen.width / 2.0f;
		float halfHeight = Screen.height / 2.0f;
		float maxHalf = Mathf.Max (halfWidth, halfHeight);

		// Acquire the relative screen position			
		Vector3 posRel = cursorScreenPosition - Vector3 (halfWidth, halfHeight, cursorScreenPosition.z);		
		posRel.x /= maxHalf;
		posRel.y /= maxHalf;

		cameraAdjustmentVector = posRel.x * screenMovementRight + posRel.y * screenMovementForward;
		cameraAdjustmentVector.y = 0.0;	

		// The facing direction is the direction from the character to the cursor world position
		motor.facingDirection = (cursorWorldPosition - character.position);
		motor.facingDirection.y = 0;

		// Draw the cursor nicely
		HandleCursorAlignment (cursorWorldPosition);*/
	}


	public static Vector3 PlaneRayIntersection (Plane plane, Ray ray)
	{
		float dist;
		plane.Raycast (ray, out dist);
		return ray.GetPoint (dist);
	}

	public static Vector3 ScreenPointToWorldPointOnPlane (Vector3 screenPoint, Plane plane, Camera camera)
	{
		Ray ray = camera.ScreenPointToRay (screenPoint);
		return PlaneRayIntersection (plane, ray);
	}

	/*void HandleCursorAlignment (Vector3 cursorWorldPosition)
	{
		if (!m_CursorObject)
			return;

		// HANDLE CURSOR POSITION

		// Set the position of the cursor object
		m_CursorObject.position = cursorWorldPosition;

		#if !UNITY_FLASH
		// Hide mouse cursor when within screen area, since we're showing game cursor instead
		Screen.showCursor = (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height);
		#endif


		// HANDLE CURSOR ROTATION

		Quaternion cursorWorldRotation = cursorObject.rotation;
		if (motor.facingDirection != Vector3.zero)
			cursorWorldRotation = Quaternion.LookRotation (motor.facingDirection);

		// Calculate cursor billboard rotation
		Vector3 cursorScreenspaceDirection = Input.mousePosition - mainCamera.WorldToScreenPoint (transform.position + character.up * cursorPlaneHeight);
		cursorScreenspaceDirection.z = 0;
		Quaternion cursorBillboardRotation = mainCameraTransform.rotation * Quaternion.LookRotation (cursorScreenspaceDirection, -Vector3.forward);

		// Set cursor rotation
		cursorObject.rotation = Quaternion.Slerp (cursorWorldRotation, cursorBillboardRotation, cursorFacingCamera);


		// HANDLE CURSOR SCALING

		// The cursor is placed in the world so it gets smaller with perspective.
		// Scale it by the inverse of the distance to the camera plane to compensate for that.
		float compensatedScale = 0.1 * Vector3.Dot (cursorWorldPosition - mainCameraTransform.position, mainCameraTransform.forward);

		// Make the cursor smaller when close to character
		float cursorScaleMultiplier = Mathf.Lerp (0.7, 1.0, Mathf.InverseLerp (0.5, 4.0, motor.facingDirection.magnitude));

		// Set the scale of the cursor
		cursorObject.localScale = Vector3.one * Mathf.Lerp (compensatedScale, 1, cursorSmallerWithDistance) * cursorScaleMultiplier;

		// DEBUG - REMOVE LATER
		if (Input.GetKey(KeyCode.O)) cursorFacingCamera += Time.deltaTime * 0.5;
		if (Input.GetKey(KeyCode.P)) cursorFacingCamera -= Time.deltaTime * 0.5;
		cursorFacingCamera = Mathf.Clamp01(cursorFacingCamera);

		if (Input.GetKey(KeyCode.K)) cursorSmallerWithDistance += Time.deltaTime * 0.5;
		if (Input.GetKey(KeyCode.L)) cursorSmallerWithDistance -= Time.deltaTime * 0.5;
		cursorSmallerWithDistance = Mathf.Clamp01(cursorSmallerWithDistance);
	}*/
}
