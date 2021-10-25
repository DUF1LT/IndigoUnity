using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject GeneralUI;
    [SerializeField] GameObject ComputerUI;
    [SerializeField] AudioClip ClickSound;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void OpenComputerUI()
    {
        GeneralUI.gameObject.SetActive(false);
        ComputerUI.gameObject.SetActive(true);
    }

    public void OpenGeneralUI()
    {
        GeneralUI.gameObject.SetActive(true);
        ComputerUI.gameObject.SetActive(false);
        source.PlayOneShot(ClickSound);

    }
}
