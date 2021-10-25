using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableImage : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SelectionManager manager;

    public void OnPointerClick(PointerEventData eventData)
    {
        manager.ChangeSelectionTo(gameObject);
    }
}
