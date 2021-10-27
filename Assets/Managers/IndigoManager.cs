using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndigoManager : MonoBehaviour
{
    [SerializeField] public List<InsertableObject> Inks;
    [SerializeField] public InsertableObject Papers;
    [SerializeField] public TurnOnComputer ComputerButton;
    [SerializeField] public OpenableObject TopCap;
    [SerializeField] public OpenableObject BottomCap;
    [SerializeField] public SelectionManager ImageSelectionManager;

    [Header("Cylinders")]
    [SerializeField] public GameObject Cylinders;

    public bool IsPrintFinished;

    public void ConditionsChanged()
    {
        gameObject.GetComponent<TaskManager>().RefreshConditions();
    }
}
