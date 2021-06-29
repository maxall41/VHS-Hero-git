using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections;

// Written by Steve Streeting 2017
// License: CC0 Public Domain http://creativecommons.org/publicdomain/zero/1.0/

/// <summary>
/// Component which will flicker a linked light while active by changing its
/// intensity between the min and max values given. The flickering can be
/// sharp or smoothed depending on the value of the smoothing parameter.
///
/// Just activate / deactivate this component as usual to pause / resume flicker
/// </summary>
public class Flicker : MonoBehaviour
{
    private new Light2D light;

    private bool lastWasBright = false;

    public float min;

    public float max;


    void Start()
    {
        if (light == null)
        {
            light = GetComponent<Light2D>();
        }
        StartCoroutine(FlickerLight());
    }


    public IEnumerator FlickerLight()
    {
        while (true)
        {
            light.intensity = Random.Range(0.2F,1);
            yield return new WaitForSeconds(Random.Range(min, max));
        }
    }
 

}