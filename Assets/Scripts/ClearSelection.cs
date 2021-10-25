using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClearSelection : MonoBehaviour
{
    [SerializeField] private SelectionManager manager;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clear");
        manager.ClearSelection();
    }
}
