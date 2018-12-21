﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class read_Score : MonoBehaviour
{
    StreamReader readScore;
    string scorePath;
    float minTime, maxProfit;

    private void Awake()
    {
        scorePath = Application.dataPath + "/Database/Input/score_info_fromMatlab.txt";
        
        reset_score();
    }

    //reset score file so that matlab can rewrite it
    private void reset_score()
    {
        if(!File.Exists(scorePath))
        {
            File.CreateText(scorePath).Dispose();
        }
        
        //reset/init the file to waiting..
        File.WriteAllText(scorePath,"waiting...");

        //reset/init variables
        minTime = 0; maxProfit = 0;
    }

    //read the score file written by matlab algo server thingy
    private bool read_score()
    {
        string initRead;

        try
        {
            initRead = File.ReadAllText(scorePath);            
        }
        catch (Exception e)
        {
            Debug.Log("the file couldnt be read - " + e.Message);
            return false;
        }

        if (initRead == "waiting...")
        {
            return false;
        }

        string[] scoreInfo = new string[2];

        scoreInfo = initRead.Split(',');

        maxProfit = float.Parse(scoreInfo[0]);
        minTime = float.Parse(scoreInfo[1]);
        return true;
    }

    //keep reading the score file while matlab is calculating and done writing the score file
    public void reading()
    {
        //reset score file so it be waiting for matlab new scores
        reset_score();
        Debug.Log("waiting on score...");

        while (!read_score()) ;
        //read_score();
        Debug.Log(maxProfit);
        Debug.Log(minTime);

        reset_score();
    }
}
