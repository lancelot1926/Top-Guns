using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Buff:ScriptableObject
{
    public string BuffName; //new weapon,attack speed,move speed,health,
    public float buffAmount;
    public int buffLevel;
}
