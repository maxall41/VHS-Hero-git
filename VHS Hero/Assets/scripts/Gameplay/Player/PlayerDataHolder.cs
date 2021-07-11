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

    //new abilities
    private bool doubleJump = false;
    private bool climbWall = false;
    private bool eternity = false; // player will not be automatically pulled back to current level

    private bool nigredo = false;
    private bool albedo = false;
    private bool citrinitas = false;


    public void SetInt(string KeyName, int Value)
    {
        PlayerPrefs.SetInt(KeyName, Value);
    }

    public int Getint(string KeyName)
    {
        return PlayerPrefs.GetInt(KeyName);
    }


    public bool FirstGreaterKey { get => firstGreaterKey; set => firstGreaterKey = value; }
    public bool SecondGreaterKey { get => secondGreaterKey; set => secondGreaterKey = value; }
    public bool ThirdGreaterKey { get => thirdGreaterKey; set => thirdGreaterKey = value; }
    public bool DoubleJump { get => doubleJump; set => doubleJump = value; }
    public bool ClimbWall { get => climbWall; set => climbWall = value; }
    
    public bool Eternity { get => eternity; set => eternity = value; }
    public bool Nigredo { get => nigredo; set => nigredo = value; }
    public bool Albedo { get => albedo; set => albedo = value; }
    public bool Citrinitas { get => citrinitas; set => citrinitas = value; }


}
