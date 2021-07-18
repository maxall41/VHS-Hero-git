using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Hint Settings")]

public class HintsReference : ScriptableObject
{
    public SerializableDictionary<string, string> hints;
}
