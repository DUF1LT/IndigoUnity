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
    [SerializeField] float KeyboardSensitivity = 0.5f;
    [SerializeField] float maxX;
    [SerializeField] float minX;
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] float maxZ;
    [SerializeField] float minZ;

    private float minDistance = 3f;
    private float mouseY = 0;
    private bool isCursorLocked = false;
    [HideInInspector]
    public bool isInUI = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isCursorLocked = !isCursorLocked;
            if (isCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

        }
    }
    void FixedUpdate()
    {

        if (!isInUI)
        {
            
            if (isCursorLocked)
            {
                mouseY -= Input.GetAxis("Mouse Y");
                ChangeDropdown();

                transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * MouseSensitivity, Space.World);
                transform.localEulerAngles = new Vector3(Mathf.Clamp(mouseY, -5, 20), transform.localEulerAngles.y,
                    transform.localEulerAngles.z);
            }

            float w = Convert.ToInt32(Input.GetKey(KeyCode.W));
            float a = -Convert.ToInt32(Input.GetKey(KeyCode.A));
            float s = -Convert.ToInt32(Input.GetKey(KeyCode.S));
            float d = Convert.ToInt32(Input.GetKey(KeyCode.D));
            float ctrl = -Convert.ToInt32(Input.GetKey(KeyCode.LeftControl));
            float space = Convert.ToInt32(Input.GetKey(KeyCode.Space));

            if (ctrl != 0 || space != 0)
            {
                Vector3 newPos = transform.position +
                                 (transform.TransformDirection(Vector3.up * (ctrl + space) * Time.deltaTime * KeyboardSensitivity));

                if (ControlDistance(Vector3.Distance(newPos, TargetPosition.position)))
                {
                    ChangeDropdown();

                    transform.position = newPos;
                }
            }

            if (a != 0 || d != 0)
            {
                Vector3 newPos = transform.position +
                                 (transform.TransformDirection(Vector3.right * (a + d) * Time.deltaTime * KeyboardSensitivity));

                if (ControlDistance(Vector3.Distance(newPos, TargetPosition.position)))
                {
                    ChangeDropdown();

                    transform.position = newPos;
                }
            }

            if (w != 0 || s != 0)
            {
                Vector3 newPos = transform.position +
                                 (transform.TransformDirection(Vector3.forward * (w + s) * Time.deltaTime * KeyboardSensitivity));

                if (ControlDistance(Vector3.Distance(newPos, TargetPosition.position)))
                {
                    ChangeDropdown();

                    transform.position = newPos;

                }
            }

            if (transform.position.x > maxX)
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);

            if (transform.position.x < minX)
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);

            if (transform.position.y < minY)
                transform.position = new Vector3(transform.position.x, minY, transform.position.z);

            if (transform.position.y > maxY)
                transform.position = new Vector3(transform.position.x, maxY, transform.position.z);

            if (transform.position.z > maxZ)
                transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);

            if (transform.position.z < minZ)
                transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
        }
    }

    bool ControlDistance(float distance)
    {
        if (distance > minDistance)
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
