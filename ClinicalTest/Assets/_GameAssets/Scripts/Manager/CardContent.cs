﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardContent
{
    public int id;

    public string name;
    public string situation;

    public CardEffect yes;
    public CardEffect no;

    public string turns;
    public string image;
    public bool popout;

    public bool bonus;
}

public struct CardEffect
{
    public string choice;
    public string debrief;

    public int implication;
    public int rigueur;
    public int patients;
    public int argent;

    public int cost;
}