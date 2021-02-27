using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Camera _camera;
    private Transform _parent;

    [SerializeField]
    private float _moveScale, _zoomScale, _rotateScale;

    [SerializeField]
    private float _movementBorder;

    [SerializeField]
    private float _minZoom, _maxZoom;

    [SerializeField]
    private float _maxAngle, _minAngle;

    private float _cameraDistance = 200;
    private Vector3 _localRotation;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _parent = transform.parent;
        RotateCamera();
    }

    void LateUpdate()
    {
        float wheelDelta = Input.GetAxis("Mouse ScrollWheel");
        if (wheelDelta != 0)
        {
            ZoomCamera(wheelDelta);
        }

        if (Input.GetMouseButton(1))
        {
            RotateCamera();
            return;
        }

        if (Input.GetMouseButton(2))
        {
            MoveCamera();
        }
    }

    private void RotateCamera()
    {
        _localRotation.x += Input.GetAxis("Mouse X") * _rotateScale;
        _localRotation.y -= Input.GetAxis("Mouse Y") * _rotateScale;

        _localRotation.y = Mathf.Clamp(_localRotation.y, _minAngle, _maxAngle);

        _parent.rotation = Quaternion.Euler(_localRotation.y, _localRotation.x, 0);
    }

    private void ZoomCamera(float delta)
    {
        _cameraDistance += delta * _zoomScale * -1f;

        _cameraDistance = Mathf.Clamp(_cameraDistance, _minZoom, _maxZoom);

        transform.localPosition = new Vector3(0, 0, -_cameraDistance);
    }

    private void MoveCamera()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        Quaternion rotation = _camera.transform.rotation;
        rotation.x = 0;
        rotation.z = 0;

        float distanceScale = Mathf.Sqrt(_cameraDistance / 100);
        Vector3 pos = rotation * new Vector3(-x * _moveScale * distanceScale, 0, -y * _moveScale * distanceScale);

        _parent.transform.Translate(pos, Space.World);
    }
}
