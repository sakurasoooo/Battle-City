using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : PowerUp
{
    protected override void Effect()
    {
        target.SendMessage("AddShield");
    }
}

