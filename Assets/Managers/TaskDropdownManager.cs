using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskDropdownManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TaskManager TaskManager;
    private GameObject DropdownList;
    private List<Toggle> DropdownItems;
    

    public void OnPointerClick(PointerEventData eventData)
    {
        DropdownList = gameObject.transform.Find("Dropdown List").gameObject;
        DropdownItems = DropdownList.GetComponentsInChildren<Toggle>().ToList();
        TaskManager.RefreshConditions();
        SetDropdownOptionsEnabled();
    }

    private void SetDropdownOptionsEnabled()
    {
        for (int i = 2; i < DropdownItems.Count; i++)
        {
            DropdownItems[i].interactable = TaskManager.TasksConditions[i - 2].TrueForAll(p => p);
        }

    }
}
