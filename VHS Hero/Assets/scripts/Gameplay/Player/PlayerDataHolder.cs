using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHolder : MonoBehaviour
{
    public bool holdingKey = false;

    //greaterKeys, bool might be more convenient for making UI for diffrent keys
    private bool firstGreaterKey = false;
    private bool secondGreaterKey = false;
    private bool thirdGreaterKey = false;

    // New abilities
    private bool doubleJump = false;
    private bool climbWall = false;
    private bool eternity = false; // player will not be automatically pulled back to current level


    public bool hasFirstGreaterKey { get => firstGreaterKey; set => firstGreaterKey = value; }
    public bool hasSecondGreaterKey { get => secondGreaterKey; set => secondGreaterKey = value; }
    public bool hasThirdGreaterKey { get => thirdGreaterKey; set => thirdGreaterKey = value; }
    public bool hasDoubleJump = false;
    public bool hasClimb { get => climbWall; set => climbWall = value; }
    
    public bool hasEternity { get => eternity; set => eternity = value; }
}
