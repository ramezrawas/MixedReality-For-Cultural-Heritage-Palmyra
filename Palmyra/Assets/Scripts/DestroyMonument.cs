﻿using MRTK.Tutorials.GettingStarted;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class DestroyMonument : MonoBehaviour
{
    public Animator monumentanim;
    public GameObject ornaments;
    public GameObject[] stoneglitch;
    bool isBeignDestroyed = false;
    bool isBeignRebuilt = false;
    public UnityEvent OnMonumentDestroyed;
    public GameObject[] stoneToSnap;
    public GameObject[] stoneOfAnim;
    public GameObject[] stoneMainLocation;
    PartAssemblyController partAssembly;
    //public GameObject interact1;

    //public BoxCollider destroyButton;
    //public BoxCollider buildButton;
    private GameObject destroyButton;
    private GameObject rebuildButton;
    



    void Start()
    {
        destroyButton = GameObject.FindGameObjectWithTag("Destroy");
        rebuildButton = GameObject.FindGameObjectWithTag("Rebuild");
    }

    public void OnDestroyFinished()
    {
        Debug.Log("this is a destroy test");
        rebuildButton.GetComponent<Interactable>().enabled = true;
        rebuildButton.GetComponent<PressableButtonHoloLens2>().enabled = true;
    }

    public void OnRebuildFinished()
    {
        Debug.Log("this is a rebuilt test");
        destroyButton.GetComponent<Interactable>().enabled = true;
        destroyButton.GetComponent<PressableButtonHoloLens2>().enabled = true;

    }

    public void Destroy()
    {
        //interact1.In.enabled = false;
        //destroyButton.enabled = false;
        
        //OnRebuildFinished();
        monumentanim.SetTrigger("destroy");
        ornaments.SetActive(false);
        destroyButton.GetComponent<Interactable>().enabled = false;
        destroyButton.GetComponent<PressableButtonHoloLens2>().enabled = false;
        StartCoroutine(StoneToSnap());
        StartCoroutine(TrackDestroyAnim());

        //OnRebuildFinished();
        Debug.Log("Botton Off");


    }

    public void Rebuild()
    {

        //OnDestroyFinished();
        //buildButton.enabled = false;
        rebuildButton.GetComponent<Interactable>().enabled = false;
        rebuildButton.GetComponent<PressableButtonHoloLens2>().enabled = false;
        StartCoroutine(Waitrebuild());

        StartCoroutine(WaittoActive());

        Debug.Log("Botton Off"); 

    }
    IEnumerator Waitrebuild()
    {
        //yield return new WaitForSeconds(6.208f);
        monumentanim.SetTrigger("rebuild");

        yield return new WaitForSeconds(6.210f);
        ornaments.SetActive(true);
        //destroyButton.enabled = true;
        //stoneOfAnim.SetActive(true);
        foreach (GameObject stones in stoneOfAnim )
        {
            stones.SetActive(true);
        }
        //stoneToSnap.SetActive(false);
        foreach (GameObject stones in stoneToSnap)
        {
            stones.SetActive(false);
        }
        //stoneMainLocation.SetActive(false);
        foreach (GameObject stones in stoneMainLocation)
        {
            stones.SetActive(false);
        }
        //yield return new WaitForSeconds(6.13f);

    }
    IEnumerator TrackDestroyAnim()
    {
        yield return new WaitForSeconds(6.210f);
        //buildButton.enabled = true;
        //Monument already destroyed
        //OnMonumentDestroyed.Invoke();
    }

    IEnumerator WaittoActive()
    {
        yield return new WaitForSeconds(5.64f);
        foreach (GameObject target in stoneglitch)
        {
            target.SetActive(true);
        }
    }

    IEnumerator StoneToSnap()
    {
        yield return new WaitForSeconds(6.1f);
        //stoneToSnap.SetActive(true);
        //stoneOfAnim.SetActive(false);
        //stoneMainLocation.SetActive(true);

        foreach (GameObject stones in stoneOfAnim)
        {
            stones.SetActive(false);
        }
        //stoneToSnap.SetActive(false);
        foreach (GameObject stones in stoneToSnap)
        {
            stones.SetActive(true);
            stones.GetComponent<PartAssemblyController>().ResetPlacement();
        }
        //stoneMainLocation.SetActive(false);
        foreach (GameObject stones in stoneMainLocation)
        {
            stones.SetActive(true);
        }
        //stoneToSnap.GetComponent<PartAssemblyController>().ResetPlacement();
    }
}