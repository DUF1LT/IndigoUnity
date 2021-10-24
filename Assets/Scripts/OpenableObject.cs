using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Newtonsoft.Json.Converters;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.EventSystems;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class OpenableObject : MonoBehaviour
{
    [SerializeField] private Axis Axis;
    [SerializeField] private float Angle;
    [SerializeField] private float InteractSpeed;


    Vector3 axis = Vector3.zero;
    private bool isOpen;
    private bool isMoving;
    private float currentAngle = 0;


    void Start()
    {
        isOpen = false;

        switch (Axis)
        {
            case Axis.Forward:
                axis = Vector3.forward;
                break;
            case Axis.Right:
                axis = Vector3.right;
                break;
            case Axis.Up:
                axis = Vector3.up;
                break;
        }

    }

    void OnMouseDown()
    {
        Interact();
    }

    private void Interact()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (isOpen)
                StartCoroutine(Close());
            else
                StartCoroutine(Open());
        }
    }

    private IEnumerator Open()
    {
        if(isMoving)
            yield break;

        isMoving = true;

        while (Angle - currentAngle > 1f)
        {
            transform.Rotate(axis, Time.deltaTime * InteractSpeed);
            currentAngle += Time.deltaTime * InteractSpeed;

            yield return null;
        }

        isMoving = false;
        isOpen = true;

    }

    private IEnumerator Close()
    {
        if (isMoving)
            yield break;

        isMoving = true;

        while (currentAngle - 0 > 1f)
        {
            transform.Rotate(axis, Time.deltaTime * (-InteractSpeed));
            currentAngle -= Time.deltaTime * InteractSpeed;

            yield return null;
        }

        isMoving = false;
        isOpen = false;
    }
}

public enum Axis
{
    Forward,
    Up,
    Right
}

