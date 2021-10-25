using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class TaskManager : MonoBehaviour
{
    [SerializeField] Dropdown TaskDropdown;
    [SerializeField] Image TaskArea;
    [SerializeField] Text TaskNameText;
    [SerializeField] Text TaskDescriptionText;

    private List<string> TasksText = new List<string>();

    void Start()
    {
        TextAsset text = Resources.Load("Tasks") as TextAsset;
        if(text != null)
            TasksText = text.text.Split('\n').ToList();
    }


    public void ChangeTask(int index)
    {
        if (index == 0)
            TurnOffArea();
        else
        {
            TurnOnArea();
            TaskNameText.text = TaskDropdown.options[index].text;
            TaskDescriptionText.text = TasksText[index - 1];
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
