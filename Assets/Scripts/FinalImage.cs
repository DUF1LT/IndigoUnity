using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalImage : MonoBehaviour
{
    [HideInInspector]
    public IndigoManager indigo;
    [HideInInspector] 
    public TaskManager taskManager;
    private bool IsShown = false;

    void OnMouseDown()
    {
        if (!IsShown)
        {
            StartCoroutine(MoveToCamera());
            IsShown = true;
        }
        else
        {
            indigo.IsPrintStarted = false;
            indigo.IsPrintFinished = false;
            taskManager.RefreshConditions();

            Destroy(gameObject);
        }
    }

    IEnumerator MoveToCamera()
    {
        while (Vector3.Distance(transform.position, Camera.main.transform.GetChild(0).transform.position) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, Camera.main.transform.GetChild(0).transform.position, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.GetChild(0).transform.rotation, Time.deltaTime * 5f);
            yield return null;
        }

        gameObject.transform.SetParent(Camera.main.transform);
    }
}
