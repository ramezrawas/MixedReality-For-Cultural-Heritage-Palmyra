﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn: MonoBehaviour
{
    [SerializeField] float delayToFade=0.05f;
    [SerializeField] float fadeStep = 0.05f;
    [SerializeField] float fadeLimit = 1.0f;
    [SerializeField] Material material;
    [SerializeField] GameObject activateGameObject;
    bool fadeIn = false;
    
    void Start()
    {
        Color c = material.color;
        c.a = 0f;
        material.color = c;
        if(activateGameObject != null)
        {
            activateGameObject.SetActive(false);
        }   
    }

    void Update()
    {
        if(fadeIn)
        {
            FadeInAnimation();
        }
    }

    public void StartFadeInSequence()
    {
        fadeIn = true;
    }

    public void FadeInAnimation()
    {
        fadeIn = false;
        StartCoroutine(FadeInAnim());
    }

    IEnumerator FadeInAnim()
    {
        for(float f = fadeStep; f<=fadeLimit; f+=fadeStep)
        {
            Color c = material.color;
            c.a = f;
            material.color = c;
            yield return new WaitForSeconds(delayToFade);
        }

        if(activateGameObject != null)
        {
            activateGameObject.SetActive(true);
        }        
        
    }

}