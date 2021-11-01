using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.EventSystems;
using System;

public class TaskManager : MonoBehaviour
{
    [SerializeField] Dropdown TaskDropdown;
    [SerializeField] Image TaskArea;
    [SerializeField] Text TaskNameText;
    [SerializeField] Text TaskDescriptionText;
    [SerializeField] Image IsDoneImage;

    private List<string> TasksText = new List<string>();
    private IndigoManager indigoManager;
    private int CurrentTaskIndex;

    public List<List<bool>> TasksConditions;


    void Start()
    {
        TextAsset text = Resources.Load("Tasks") as TextAsset;
        if (text != null)
            TasksText = text.text.Split('\n').ToList();

        indigoManager = GetComponent<IndigoManager>();

        TasksConditions = new List<List<bool>>();
        RefreshConditions();
    }

    IEnumerator AcceptTask()
    {
        yield return new WaitForSeconds(2f);

        Debug.Log(CurrentTaskIndex);

        TaskDropdown.value = ++CurrentTaskIndex;
        RefreshConditions();
    }

    public void RefreshConditions()
    {
        TasksConditions.Clear();

        TasksConditions.Add(new List<bool> { true, indigoManager.ComputerButton.IsComputerOn });

        var tempList = new List<bool>();
        tempList.Add(TasksConditions[0].TrueForAll(p => p));

        foreach (var ink in indigoManager.Inks)
            tempList.Add(ink.IsInserted);

        TasksConditions.Add(tempList);
        TasksConditions.Add(new List<bool> { TasksConditions[1].TrueForAll(p => p), indigoManager.Papers.IsInserted });
        TasksConditions.Add(new List<bool> { TasksConditions[2].TrueForAll(p => p), indigoManager.IsPrintStarted });
        TasksConditions.Add(new List<bool> { TasksConditions[3].TrueForAll(p => p), indigoManager.IsPrintFinished });
        TasksConditions.Add(new List<bool> { TasksConditions[4].TrueForAll(p => p), false });

        if (CurrentTaskIndex >= 1)
        {
            if (TasksConditions[CurrentTaskIndex - 1].TrueForAll(p => p))
            {
                IsDoneImage.gameObject.SetActive(true);
                Debug.Log("Current task");

                StartCoroutine(AcceptTask());
            }
            else
                IsDoneImage.gameObject.SetActive(false);

            while (!TasksConditions[CurrentTaskIndex - 1][0])
            {
                TaskDropdown.value = CurrentTaskIndex - 1;
                if (indigoManager.IsPrintStarted)
                {
                    indigoManager.StopPrint();
                }
            }
        }
    }

    public void ChangeTask(int index)
    {
        if (index == 0)
        {
            CurrentTaskIndex = 0;
            TurnOffArea();
        }
        else
        {
            CurrentTaskIndex = index;
            TurnOnArea();
            TaskNameText.text = TaskDropdown.options[index].text;
            TaskDescriptionText.text = TasksText[index - 1];
            IsDoneImage.gameObject.SetActive(false);
        }
    }

    private void TurnOffArea()
    {
        TaskArea.gameObject.SetActive(false);
    }

    private void TurnOnArea()
    {
        TaskArea.gameObject.SetActive(true);
    }
}
