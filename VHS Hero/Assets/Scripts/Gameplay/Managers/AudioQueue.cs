using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueue : MonoBehaviour
{

    public List<VoicelineTrigger> queuedPlayers = new List<VoicelineTrigger>();

    public bool first = true;



    public void AddToQueue(VoicelineTrigger add)
    {
        if (first == true)
        {
            Debug.Log("Playing current");
            add.play();
            first = false;
        } else
        {
            queuedPlayers.Add(add);
        }
        Debug.Log("Count: ");
        Debug.Log(queuedPlayers.Count);
        if (queuedPlayers.Count < 2)
        {
            queuedPlayers[queuedPlayers.Count - 1].play();
            //queuedPlayers.RemoveAt(queuedPlayers.Count - 1);
        }
    } 

    public void Finished()
    {
        queuedPlayers[queuedPlayers.Count - 1].play();
        queuedPlayers.RemoveAt(queuedPlayers.Count - 1);
        Debug.Log("Finished");
    }
}
