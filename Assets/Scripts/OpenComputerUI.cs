using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OpenComputerUI : MonoBehaviour
{
    [SerializeField] GameObject SimulatorManagers;
    [SerializeField] TurnOnComputer computer;

    private UIManager ui;

    private void Start()
    {
        ui = SimulatorManagers.GetComponent<UIManager>();
    }
    private void OnMouseDown()
    {
        if (computer.IsComputerOn)
            ui.OpenComputerUI();
    }


}
