using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move 
{
    public MoveBase pBase { get; set; }

    public int PP { get; set; }

    public Move(MoveBase Base)
    {
        pBase = Base;
        PP = Base.PP;
    }
}
