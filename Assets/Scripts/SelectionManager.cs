using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Images;

    private GameObject CurrentSelection;

    void Start()
    {
        CurrentSelection = null;
        gameObject.SetActive(false);
    }

    public void ChangeSelectionTo(GameObject GameObject)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<RectTransform>().transform.position = GameObject.transform.GetComponent<RectTransform>().position;
        CurrentSelection = gameObject;
    }

    public void ClearSelection()
    {
        CurrentSelection = null;
        gameObject.SetActive(false);
    }
}
