using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera Camera;
    [SerializeField] List<Transform> CameraCheckpoints;
    [SerializeField] float MovementSpeed;

    private float offset = 0.5f;
    private bool isMoving;
    public void MoveCameraTo(int index)
    {
        if(index == 0)
            return;

        StartCoroutine(Move(CameraCheckpoints[index - 1]));
    }

    IEnumerator Move(Transform checkpoint)
    {
        if(isMoving)
            yield break;

        isMoving = true;

        while (Vector3.Distance(Camera.transform.position, checkpoint.position) > offset)
        {
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, checkpoint.position,
                Time.deltaTime * MovementSpeed);
            Camera.transform.rotation = Quaternion.Slerp(Camera.transform.rotation, checkpoint.transform.rotation, Time.deltaTime*MovementSpeed);

            yield return null;
        }

        isMoving = false;
    }
}
