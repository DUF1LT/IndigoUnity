using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class InsertableObject : MonoBehaviour
{
    [SerializeField] private IndigoManager indigo;
    [SerializeField] private Material InsertedMaterial;
    [SerializeField] private Material NotInsertedMaterial;
    [HideInInspector] public bool IsInserted = false;

    private MeshRenderer mesh;

    void Start()
    {

        mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.material = NotInsertedMaterial;
    }

    void OnMouseDown()
    {
        if (!IsInserted)
        {
            mesh.material = InsertedMaterial;
            IsInserted = true;
            indigo.ConditionsChanged();
        }
        else
        {
            mesh.material = NotInsertedMaterial;
            IsInserted = false;
            indigo.ConditionsChanged();
        }
    }

}
