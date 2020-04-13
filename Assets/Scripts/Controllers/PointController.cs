﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    [SerializeField] private UIController uiCon;

    private int pointsNowGame = 0;

    public void AddPoint(int points)
    {
        pointsNowGame += points;

        uiCon.ChangePointsText(pointsNowGame);
    }
}
