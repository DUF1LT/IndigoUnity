using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnUIHighlightObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] List<string> uiText;
    [SerializeField] List<GameObject> gameObjectToHighlight;


    private Outline outline;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uiText.Contains(text.text) && EventSystem.current.IsPointerOverGameObject())
        {
            outline = gameObjectToHighlight[uiText.IndexOf(text.text)].AddComponent<Outline>();
            outline.OutlineMode = Outline.Mode.OutlineAndSilhouette;
            outline.OutlineColor = Color.green;
            outline.OutlineWidth = 10f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (outline != null)
            Destroy(outline);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (outline != null)
            Destroy(outline);
    }

    void OnDestroy()
    {
        Destroy(outline);
    }
}
