using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableManager : MonoBehaviour
{
    [SerializeField] Text MountainsAndForestCount;
    [SerializeField] Text GreenHillCount;
    [SerializeField] Text PlanesCount;
    [SerializeField] Text IceLandCount;
    [SerializeField] SelectionManager SelectionManager;

    public void AddRecordToTable()
    {
        switch(SelectionManager.CurrentSelection.name)
        {
            case "landscapes1":
                MountainsAndForestCount.text = Convert.ToString(Convert.ToInt32(MountainsAndForestCount.text) + 1);
                break;
            case "landscapes2":
                IceLandCount.text = Convert.ToString(Convert.ToInt32(IceLandCount.text) + 1);
                break;
            case "landscapes3":
                GreenHillCount.text = Convert.ToString(Convert.ToInt32(GreenHillCount.text) + 1);
                break;
            case "landscapes4":
                PlanesCount.text = Convert.ToString(Convert.ToInt32(PlanesCount.text) + 1);
                break;
            default:
                break;
        }

    }
}
