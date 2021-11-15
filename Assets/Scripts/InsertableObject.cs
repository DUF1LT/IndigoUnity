using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Cursor = UnityEngine.Cursor;

public class InsertableObject : MonoBehaviour
{
    [SerializeField] private IndigoManager indigo;
    [SerializeField] private Material InsertedMaterial;
    [SerializeField] private Material NotInsertedMaterial;
    [SerializeField] private AudioClip InputSound;
    [SerializeField] private AudioClip PickUpSound;

    [HideInInspector] public bool IsInserted = false;

    private MeshRenderer mesh;

    void Start()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        gameObject.AddComponent<AudioSource>();
        mesh.material = NotInsertedMaterial;
    }

    void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Cursor.lockState != CursorLockMode.Locked)
        {
            if (!IsInserted)
            {
                mesh.material = InsertedMaterial;
                IsInserted = true;
                GetComponent<AudioSource>().PlayOneShot(InputSound);
                indigo.ConditionsChanged();
            }
            else
            {
                mesh.material = NotInsertedMaterial;
                IsInserted = false;
                GetComponent<AudioSource>().PlayOneShot(PickUpSound);
                indigo.ConditionsChanged();
            }
        }
    }
}
