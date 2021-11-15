using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IndigoManager : MonoBehaviour
{
    [Header("Working Elements")]
    [SerializeField] public List<InsertableObject> Inks;
    [SerializeField] public InsertableObject Papers;
    [SerializeField] public TurnOnComputer ComputerButton;
    [SerializeField] public OpenableObject TopCap;
    [SerializeField] public OpenableObject BottomCap;
    [SerializeField] public SelectionManager ImageSelectionManager;

    [Header("Cylinders")]
    [SerializeField] GameObject DevelopingCylinder;
    [SerializeField] GameObject FormingCylinder;
    [SerializeField] GameObject OffsetCylinder;
    [SerializeField] GameObject PrintingCylinder;
    [SerializeField] GameObject UpperFeeder;
    [SerializeField] GameObject LowerFeeder;

    [Header("Paper Elements")]
    [SerializeField] GameObject Paper;
    [SerializeField] List<Transform> PaperCheckpoints;

    [Header("Particles")]
    [SerializeField] ParticleSystem InksParticles;
    [SerializeField] ParticleSystem Lasers;
    [SerializeField] Material Cyan;
    [SerializeField] Material Magenta;
    [SerializeField] Material Yellow;
    [SerializeField] Material Key;

    [Header("Other")]
    [SerializeField] SelectionManager SelectionManager;
    [SerializeField] AudioClip WorkingSound;

    [HideInInspector]
    public bool IsPrintFinished;
    [HideInInspector]
    public bool IsPrintStarted;

    private float RotationSpeed = 40f;
    private Texture SelectedImage;
    private float PaperMoveSpeed = 0.5f;

    public void ConditionsChanged()
    {
        gameObject.GetComponent<TaskManager>().RefreshConditions();
    }

    public void StartPrint()
    {
        IsPrintStarted = true;
        GetComponent<AudioSource>().clip = WorkingSound;
        GetComponent<AudioSource>().Play();
        SelectedImage = SelectionManager.CurrentSelection;
        StartCoroutine(Rotate(DevelopingCylinder, Vector3.forward));
        StartCoroutine(Rotate(FormingCylinder, Vector3.back));
        StartCoroutine(Rotate(OffsetCylinder, Vector3.forward));
        StartCoroutine(Rotate(PrintingCylinder, Vector3.back));
        StartCoroutine(Rotate(UpperFeeder, Vector3.forward));
        StartCoroutine(Rotate(LowerFeeder, Vector3.forward));
        StartCoroutine(InksInject());
    }

    public void StopPrint()
    {
        StopAllCoroutines();
        IsPrintStarted = false;
        GetComponent<AudioSource>().Stop();
    }

    IEnumerator Rotate(GameObject obj, Vector3 axis)
    {
        while (!IsPrintFinished)
        {
            obj.transform.Rotate(axis, Time.deltaTime * RotationSpeed);

            if (IsPrintFinished)
                break;

            yield return null;
        }
    }

    IEnumerator InksInject()
    {
        yield return new WaitForSeconds(8f);

        Lasers.Play();
        yield return new WaitForSeconds(2f);

        InksParticles.GetComponent<Renderer>().material = Cyan;
        InksParticles.Play();
        yield return new WaitForSeconds(4f);
        InksParticles.Stop();
        yield return new WaitForSeconds(1f);
        InksParticles.Play();

        InksParticles.GetComponent<Renderer>().material = Magenta;
        yield return new WaitForSeconds(4f);
        InksParticles.Stop();
        yield return new WaitForSeconds(1f);
        InksParticles.Play();

        InksParticles.GetComponent<Renderer>().material = Yellow;
        yield return new WaitForSeconds(4f);
        InksParticles.Stop();
        yield return new WaitForSeconds(1f);
        InksParticles.Play();

        InksParticles.GetComponent<Renderer>().material = Key;
        yield return new WaitForSeconds(4f);
        InksParticles.Stop();
        Lasers.Stop();

        StartCoroutine(OffsetCylinderColoring());
        StartCoroutine(PaperMove());
    }

    IEnumerator OffsetCylinderColoring()
    {
        OffsetCylinder.GetComponent<MeshRenderer>().material.mainTexture = SelectedImage;
        yield return new WaitForSeconds(10f);
        OffsetCylinder.GetComponent<MeshRenderer>().material.mainTexture = null;

    }

    IEnumerator PaperMove()
    {
        GameObject paper = Instantiate(Paper, PaperCheckpoints[0].position, PaperCheckpoints[0].rotation);
        for(int i = 1; i < PaperCheckpoints.Count; i++)
        {
            while (Vector3.Distance(paper.transform.position, PaperCheckpoints[i].position) > 0.3f)
            {
                paper.transform.position = Vector3.Lerp(paper.transform.position, PaperCheckpoints[i].position,
                    Time.deltaTime * PaperMoveSpeed);
                paper.transform.rotation = Quaternion.Slerp(paper.transform.rotation, PaperCheckpoints[i].transform.rotation, 
                    Time.deltaTime * PaperMoveSpeed);
                yield return null;
            }

            if (i == 3)
            {
                paper.GetComponent<MeshRenderer>().material.mainTexture = SelectedImage;
            }
        }

        paper.AddComponent<HighlightedObject>();
        paper.AddComponent<FinalImage>();
        paper.GetComponent<FinalImage>().indigo = this;
        paper.GetComponent<FinalImage>().taskManager = GetComponent<TaskManager>();

        IsPrintFinished = true;

        GetComponent<TableManager>().AddRecordToTable();
        GetComponent<AudioSource>().Stop();
        GetComponent<TaskManager>().RefreshConditions();
    }

}
