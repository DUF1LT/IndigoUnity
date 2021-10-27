using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> Images;
    [SerializeField] private List<Texture> Textures;

    [HideInInspector]
    public Texture CurrentSelection;

    void Start()
    {
        gameObject.GetComponent<RectTransform>().transform.position = Images[0].transform.GetComponent<RectTransform>().position;
    }

    public void ChangeSelectionTo(GameObject GameObject)
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<RectTransform>().transform.position = GameObject.transform.GetComponent<RectTransform>().position;
        CurrentSelection = Textures[Images.IndexOf(Images.Find(p => p == GameObject))];
    }

    public void ClearSelection()
    {
        gameObject.SetActive(false);
    }

}
