using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class InsertableObject : MonoBehaviour
{
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
        }
        else
        {
            mesh.material = NotInsertedMaterial;
            IsInserted = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
