using UnityEngine;

[AddComponentMenu("Camera-Control/Mouse drag Orbit with zoom")]
public class MouseOrbit : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 1.0f;

    [SerializeField] private float xSpeed = 250.0f;
    [SerializeField] private float ySpeed = 120.0f;

    [SerializeField] private int yMinLimit = -20;
    [SerializeField] private int yMaxLimit = 80;

    private float _x;
    private float _y;
    private Vector3 targetPos;


    private void Start()
    {
        targetPos = target.position;
        var angles = transform.eulerAngles;
        _x = angles.y;
        _y = angles.x;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(2))
        {
            targetPos -= transform.up * (Input.GetAxis("Mouse Y") * 0.05f);
            targetPos -= transform.right * (Input.GetAxis("Mouse X") * 0.05f);
        }

        if (Input.GetMouseButton(0))
        {
            _x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            _y = ClampAngle(_y, yMinLimit, yMaxLimit);
        }

        if (Input.mouseScrollDelta.y != 0) distance -= Input.mouseScrollDelta.y * 0.1f;

        var rotation = Quaternion.Euler(_y, _x, 0);
        var position = rotation * new Vector3(0.0f, 0.0f, -distance) + targetPos;
        transform.SetPositionAndRotation(position, rotation);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}