using UnityEngine;

public class Regulator : MonoBehaviour {

	public Transform model;

	public float minAngle = -180.0f;
	public float maxAngle = 180.0f;

	public float minOutput = 0.0f;
	public float maxOutput = 1.0f;

	public float value = 0.5f;

	private Transform _transform;

	private void Awake() {
		_transform = transform;
	}

	public float GetOutput() {
		return Mathf.Lerp(minOutput, maxOutput, value);
	}

	private void OnMouseDrag() {
		Vector3 mousePosition = MouseToWorld(Input.mousePosition);
		model.LookAt(mousePosition, _transform.up);
		float angle = Vector3.Angle(model.right, _transform.right);
		Vector3 cross = Vector3.Cross(model.right, _transform.right);
		if (_transform.TransformDirection(cross).z < 0)
			angle *= -1;
		if (angle < minAngle) {
			model.rotation = Quaternion.AngleAxis(minAngle - angle, model.up) * model.rotation;
			angle = minAngle;
		}
		else if (angle > maxAngle) {
			model.rotation = Quaternion.AngleAxis(maxAngle - angle, model.up) * model.rotation;
			angle = maxAngle;
		}
		value = Mathf.InverseLerp(minAngle, maxAngle, angle);
	}

	private Vector3 MouseToWorld(Vector2 mouse) {
		Plane regulatorPlane = new Plane(_transform.up, _transform.position);
		float rayDistance;
		Ray mouseRay = Camera.main.ScreenPointToRay(mouse);
		if (regulatorPlane.Raycast(mouseRay, out rayDistance))
			return mouseRay.GetPoint(rayDistance);
		return Vector3.zero;
	}
}