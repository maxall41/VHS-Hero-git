using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectOnPick : MonoBehaviour
{
    public ParticleSystem playerParticles;

    public AudioSource sfx;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("NAME: " + collision.gameObject.name);
        if (collision.gameObject.name == "Player")
        {
            if (activated == false)
            {
                // Screen shake
                GameObject.Find("Main Camera").GetComponent<CameraShake>().Shake(0.6F, 0.8F);
                OnComplete();
                // Zoom out
                //StartCoroutine(ZoomOutCam(8.315489F, GameObject.Find("Main Camera").GetComponent<Camera>(), 0.5F, true));
                // Big bang sound
                sfx.Play();
                activated = true;
            }
        }
    }

    private IEnumerator ZoomOutCam(float toValue,Camera cam,float duration, bool trigger)
    {
        float currentTime = 0;
        float currentVal;
        currentVal = cam.orthographicSize;
        float targetValue = Mathf.Clamp(toValue, 0.0001f, 100);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVal, targetValue, currentTime / duration);
            cam.orthographicSize = newVol;
            yield return null;
        }
        if (trigger == true)
        {
            OnComplete();
        }
        yield break;
    }

    private void OnComplete ()
    {
        // Particle effects
        playerParticles.Play();
        // Zoom back in
        // StartCoroutine(ZoomInAfterDelay());
    }

    private IEnumerator ZoomInAfterDelay()
    {
        yield return new WaitForSeconds(2.5F);
        StartCoroutine(ZoomOutCam(7.815489F, GameObject.Find("Main Camera").GetComponent<Camera>(), 0.55F,false));
    }





}
