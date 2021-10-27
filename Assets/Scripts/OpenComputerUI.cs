using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (computer.IsComputerOn && !EventSystem.current.IsPointerOverGameObject())
            ui.OpenComputerUI();
    }


}
