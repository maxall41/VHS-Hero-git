using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueue : MonoBehaviour
{

    public List<string> queuedPlayers = new List<string>();

    public List<string> playedBefore = new List<string>();

    public bool first = true;

    public bool stop = false;



    public void AddToQueue(VoicelineTrigger add)
    {
        if (stop == false)
        {
            if (playedBefore.Contains(add.id) == false)
            {
                //if (first == true)
                //{
                //    add.play();
                //    playedBefore.Add(add.id);
                //    first = false;
                //}
                if (queuedPlayers.Count < 2)
                {
                    //Debug.Log(queuedPlayers.Count - 1);
                    //add.play();
                    queuedPlayers.Add(add.id);
                    playedBefore.Add(add.id);
                    if (queuedPlayers.Count == 1)
                    {
                        GameObject.Find(queuedPlayers[0]).GetComponent<VoicelineTrigger>().play();

                    }
                }
                else
                {
                    queuedPlayers.Add(add.id);
                    playedBefore.Add(add.id);
                }
            }
        }
    } 

    public void Finished()
    {
        GameObject.Find(queuedPlayers[queuedPlayers.Count - 1]).GetComponent<VoicelineTrigger>().play();
        queuedPlayers.RemoveAt(queuedPlayers.Count - 1);
        Debug.Log("Finished");
    }
}
