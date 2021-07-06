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
    private bool breakAbility = false;

    public bool FirstGreaterKey { get => firstGreaterKey; set => firstGreaterKey = value; }
    public bool SecondGreaterKey { get => secondGreaterKey; set => secondGreaterKey = value; }
    public bool ThirdGreaterKey { get => thirdGreaterKey; set => thirdGreaterKey = value; }
    public bool DoubleJump { get => doubleJump; set => doubleJump = value; }
    public bool ClimbWall { get => climbWall; set => climbWall = value; }
    public bool BreakAbility { get => breakAbility; set => breakAbility = value; }
}
