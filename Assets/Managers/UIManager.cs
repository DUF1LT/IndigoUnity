using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject GeneralUI;
    [SerializeField] GameObject ComputerUI;
    [SerializeField] GameObject ErrorPopup;
    [SerializeField] AudioClip ClickSound;
    [SerializeField] AudioClip ErrorSound;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void OpenComputerUI()
    {
        GeneralUI.gameObject.SetActive(false);
        ComputerUI.gameObject.SetActive(true);
        Camera.main.GetComponent<CameraRotate>().isInUI = true;
    }

    public void OpenGeneralUI()
    {
        GeneralUI.gameObject.SetActive(true);
        ComputerUI.gameObject.SetActive(false);
        source.PlayOneShot(ClickSound);
        Camera.main.GetComponent<CameraRotate>().isInUI = false;
    }

    public void PrintButton()
    {
        if (gameObject.GetComponent<TaskManager>().TasksConditions[2].TrueForAll(p => p))
        {
            source.PlayOneShot(ClickSound);
            gameObject.GetComponent<IndigoManager>().StartPrint();
            gameObject.GetComponent<TaskManager>().RefreshConditions();
            OpenGeneralUI();
        }
        else
        {
            source.PlayOneShot(ErrorSound);
            ErrorPopup.SetActive(true);
        }

    }
}
