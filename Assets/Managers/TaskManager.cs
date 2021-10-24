using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class TaskManager : MonoBehaviour
{
    [SerializeField] Dropdown TaskDropdown;
    [SerializeField] Image TaskArea;
    [SerializeField] Text TaskNameText;
    [SerializeField] Text TaskDescriptionText;

    private List<string> TasksText = new List<string>();

    void Awake()
    {
        string path = Application.dataPath + "/TasksText/Tasks.txt";
        using (StreamReader reader = new StreamReader(path))
        {
            while (!reader.EndOfStream)
                TasksText.Add(reader.ReadLine());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
