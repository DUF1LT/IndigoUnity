using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnComputer : MonoBehaviour
{
    [SerializeField] Material TurnOffMaterial;
    [SerializeField] Material TurnOnMaterial;
    [SerializeField] GameObject Screen;
    [SerializeField] AudioClip TurnOnPC;
    [SerializeField] AudioClip TurnOffPC;

    [HideInInspector]
    public bool IsComputerOn;

    private AudioSource source;

    private void Start()
    {
        Screen.GetComponent<MeshRenderer>().material = TurnOffMaterial;
        source = GetComponent<AudioSource>();        
    }
    private void OnMouseDown()
    {
        if(!IsComputerOn)
        {
            Screen.GetComponent<MeshRenderer>().material = TurnOnMaterial;
            IsComputerOn = true;
            source.PlayOneShot(TurnOnPC);
            source.PlayDelayed(1);
        }
        else
        {
            Screen.GetComponent<MeshRenderer>().material = TurnOffMaterial;
            IsComputerOn = false;
            source.Stop();
            source.PlayOneShot(TurnOffPC);
        }
    }
}
