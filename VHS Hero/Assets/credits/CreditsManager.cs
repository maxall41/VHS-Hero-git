using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreditsManager : MonoBehaviour
{

    public TextMeshProUGUI title;

    public string titleText;

    public string[] people;

    public TextMeshProUGUI[] peopleSlots;

    float timeRemaining = 0;

    public bool catalyst;

    public bool end;

    bool nextScreen = false;

    public CreditsManager next;




    private int offset = 155;

    // Start is called before the first frame update
    void Start()
    {
        if (catalyst == true)
        {
            DrawNames(0);
        }
    }

    void DrawNames(int startPoint)
    {
        title.text = titleText;

        Debug.Log("Drawing names...");


        for (int i = startPoint; i < peopleSlots.Length;)
        {
            if (timeRemaining == 0)
            {
                peopleSlots[i].text = people[i];
                Debug.Log(peopleSlots.Length);
                if (i == peopleSlots.Length - 1)
                {
                    Debug.Log("Done");
                    timeRemaining = 4;
                    nextScreen = true;
                    LeanTween.alphaCanvas(GameObject.Find("Canvas").GetComponent<CanvasGroup>(), 1, 0.5F);
                }
            }
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        } else if (nextScreen == true)
        {
            if (end == false)
            {
                Debug.Log("Next");
                LeanTween.alphaCanvas(GameObject.Find("Canvas").GetComponent<CanvasGroup>(), 0, 0.5F).setOnComplete(() => {
                    next.StartNext();
                    Destroy(this);
                });
            }

        }

    }

    IEnumerator StartDisplay()
    {
        yield return new WaitForSeconds(1.5F);
        if (catalyst == false)
        {
            Debug.Log("Started next");
            DrawNames(0);

        }
        else
        {
            Debug.Log("NOT NON-CATALYST");
        }

    }



    void StartNext()
    {
        StartCoroutine("StartDisplay");
    }

}
