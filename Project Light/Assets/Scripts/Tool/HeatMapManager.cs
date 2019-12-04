﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class HeatMapManager : MonoBehaviour
{
    private List<string> paths = new List<string>();
    public List<GameObject> cubes = new List<GameObject>();
    private List<HeatCubeInfo> infos = new List<HeatCubeInfo>();
    public List<int> activity = new List<int>();
    public List<int> newActivity = new List<int>();
    public HeatMap heatMap;
    public bool updateTheMap = false;
    private int currentFile = 0;
    private string path;
    private string firstPath, secondaryPath;
    public bool on = false;

    private string activityString, deathstring;
    private int activityType = 0;
    private int deathsType = 1;
    public string activetyValue = "/ActivetyValues";
    public string deathValue = "/DeathValues";

    void Start()
    {
        if(on)
            Cursor.lockState = CursorLockMode.Confined;
        firstPath = Application.dataPath + "/ActivetyValues.txt";
        path = Application.dataPath + "/ActivetyValues.txt";
        secondaryPath = Application.dataPath + "/DeathValues.txt";
    }

    void CreateFile(string type)
    {
        path = CheckIfFileExists(type);
        if(type == activetyValue)
            File.WriteAllText(path, activityString);
        else if(type == deathValue)
            File.WriteAllText(path, deathstring);
    }

    string CheckIfFileExists(string type)
    {
        if (File.Exists(path))
        {
            currentFile += 1;
            path = Application.dataPath + type + currentFile.ToString() + ".txt";
            CheckIfFileExists(type);
        }
        return path;
    }

    public List<string> FindAllPaths()
    {
        paths.Clear();
        FindActivityPaths();
        ResetCurrentFile();
        FindDeathsPaths();
        return paths;
    }

    private void FindDeathsPaths()
    {
        if (File.Exists(secondaryPath))
        {
            paths.Add(secondaryPath);
            currentFile += 1;
            secondaryPath = Application.dataPath + "/DeathValues" + currentFile.ToString() + ".txt";
            FindDeathsPaths();
        }
    }

    private void FindActivityPaths()
    {
        if (File.Exists(path))
        {
            paths.Add(path);
            currentFile += 1;
            path = Application.dataPath + "/ActivetyValues" + currentFile.ToString() + ".txt";
            FindActivityPaths();
        }
    }

    void PreFillActivetyList()
    {
        for(int i = 0; i < heatMap.GetCubes().Count; i++)
            newActivity.Add(0);
    }

    private void ResetPath(string type)
    {
        path = Application.dataPath + type + ".txt";
    }

    private void ResetCurrentFile()
    {
        currentFile = 0;
    }

    void AddAllSavedFiles(string type)
    {
        if (File.Exists(path))
        {
            activity = GetAllActivityValuesFromFile(path);
            var sum = newActivity.Zip(activity, (x,y) => x + y).ToList();
            newActivity = sum;
            currentFile += 1;
            path = Application.dataPath + type + currentFile.ToString() + ".txt";
            AddAllSavedFiles(type);
        }
        heatMap.SetActivety(newActivity);
    }

    void LoadAllActivetyValuesFromFile(string path, int typeOfValue)
    {
        List<int> temp = new List<int>();
        string tempString = File.ReadAllText(path);

        string[] lines = Regex.Split(tempString, ",");
        for(int i = 0; i < lines.Length; i++)
            temp.Add(Int32.Parse(lines[i]));

        if(typeOfValue == activityType)
            heatMap.SetActivety(temp);
        if(typeOfValue == deathsType)
            heatMap.SetDeaths(temp);
    }
    
    List<int> GetAllActivityValuesFromFile(string path)
    {
        List<int> temp = new List<int>();
        string tempString = File.ReadAllText(path);

        string[] lines = Regex.Split(tempString, ",");
        for(int i = 0; i < lines.Length; i++)
            temp.Add(Int32.Parse(lines[i]));

        return temp;
    }

    public void LoadAllFiles(string type)
    {
        heatMap.ResetActivety();
        heatMap.ResetDeaths();
        ResetCurrentFile();
        newActivity.Clear();
        PreFillActivetyList();
        ResetPath(type);
        AddAllSavedFiles(type);
    }

    public void LoadSpecificFile(string path, int typeOfValue)
    {
        heatMap.ResetActivety();
        heatMap.ResetDeaths();
        ResetCurrentFile();
        LoadAllActivetyValuesFromFile(path, typeOfValue);
    }

    private void CreateDeathsFile()
    {
        activity.Clear();
        ResetPath(deathValue);
        ResetCurrentFile();
        activity = heatMap.GetDeaths();
        deathstring = String.Join(",", activity.Select(p=>p.ToString()).ToArray());
        CreateFile(deathValue);  
    }

    private void CreateActivityFile()
    {
        activity.Clear();
        ResetPath(activetyValue);
        ResetCurrentFile();
        activity = heatMap.GetScores();
        activityString = String.Join(",", activity.Select(p=>p.ToString()).ToArray());
        CreateFile(activetyValue);
    }

    public void CreateFiles()
    {
        CreateDeathsFile();
        CreateActivityFile();
    }

    //void OnApplicationQuit()
    //{
    //    cubes = heatMap.GetCubes();
    //    for(int i = 0; i < cubes.Count; i++)
    //        activity.Add(cubes[i].GetComponent<HeatCubeInfo>().GetScore());
    //    CreateFile(activetyValue);
    //}

    //void OnApplicationPause()
    //{
    //    cubes = heatMap.GetCubes();
    //    for(int i = 0; i < cubes.Count; i++)
    //        activity.Add(cubes[i].GetComponent<HeatCubeInfo>().GetScore());
    //    CreateFile(activetyValue);
    //}
}
