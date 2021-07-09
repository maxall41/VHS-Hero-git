using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class hintTrigger : MonoBehaviour
{
    public string hint;

    public TextMeshProUGUI hintText;

    public string id;

    public List<string> load = new List<string>();

    public List<string> empty = new List<string>();

    private void Start()
    {
        load = ES3.Load("gameplayParams",empty);
        load.Add(id);
        ES3.Save("gameplayParams", load);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger: " + collision.gameObject.name);
        if (PlayerPrefs.GetInt(id) != 0 && collision.gameObject.name == "Player")
        {
            PlayerPrefs.SetInt(id, 0);
            StartCoroutine(Type(hintText, hint, 0.02F));
        }
    }

    IEnumerator Type(TextMeshProUGUI text, string textToType, float typingSpeed)
    {
        foreach (char letter in textToType.ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        yield return new WaitForSeconds(1.5F);
        foreach (char letter in text.text.ToCharArray())
        {
            text.text = text.text.Remove(text.text.Length - 1); ;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
