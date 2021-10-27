using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] Dropdown CameraDropdown;

    [SerializeField] Transform TargetPosition;
    [SerializeField] float MouseSensitivity = 0.5f;
    [SerializeField] float KeyboardSensitivity = 5f;
    [SerializeField] float ScrollSpeed = 0.4f;
    [SerializeField] int maxDistance = 20;
    [SerializeField] float minX;
    [SerializeField] float maxY;
    [SerializeField] float minY;

    private float mouseY = 0;
    private float minDistance = 3f;

    [HideInInspector]
    public bool isInUI = false;

    void FixedUpdate()
    {

        if (!isInUI)
        {
            if (Input.GetMouseButton(1))
            {
                mouseY -= Input.GetAxis("Mouse Y");

                ChangeDropdown();

                transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * MouseSensitivity, Space.World);
                transform.localEulerAngles = new Vector3(Mathf.Clamp(mouseY, -5, 20), transform.localEulerAngles.y,
                    transform.localEulerAngles.z);
            }

            if (transform.position.x < minX)
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);

            if (transform.position.y < minY)
                transform.position = new Vector3(transform.position.x, minY, transform.position.z);

            if (transform.position.y > maxY)
                transform.position = new Vector3(transform.position.x, maxY, transform.position.z);

            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0 || y != 0)
            {
                Vector3 newPos = transform.position +
                                 (transform.TransformDirection(new Vector3(x, 0, 0)) + Vector3.up * y) /
                                 KeyboardSensitivity;

                if (ControlDistance(Vector3.Distance(newPos, TargetPosition.position)))
                {
                    ChangeDropdown();

                    transform.position = newPos;
                }
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                Vector3 newPos = transform.position +
                                 (transform.TransformDirection(Vector3.forward * scroll * ScrollSpeed));

                if (ControlDistance(Vector3.Distance(newPos, TargetPosition.position)))
                {
                    ChangeDropdown();

                    transform.position = newPos;

                }
            }
        }
    }

    bool ControlDistance(float distance)
    {
        if (distance > minDistance && distance < maxDistance)
            return true;
        return false;
    }

    void ChangeDropdown()
    {
        if (CameraDropdown.value != 0)
            CameraDropdown.value = 0;
    }

    //IEnumerator SmoothLookAt(Quaternion lookAtRotation)
    //{
    //    if(isRotating)
    //        yield break;

    //    isRotating = true;

    //    while (Quaternion.Angle(transform.rotation, lookAtRotation) > angleOffset)
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime * rotationSpeed);

    //        yield return null;
    //    }

    //    isRotating = false;
    //}
}
