using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class HeatMapManager : MonoBehaviour
{
    public List<GameObject> cubes = new List<GameObject>();
    public List<int> activity = new List<int>();
    public List<int> newActivity = new List<int>();
    public HeatMap heatMap;
    public bool updateTheMap = false;
    private int currentFile = 0;
    private string path;
    private string firstPath;

    void Start()
    {
        firstPath = Application.dataPath + "/ActivetyValues.txt";
    }

    void CreateActivetyValueFile()
    {
        path = CheckIfFileExists(firstPath);

        foreach(int i in activity)
                File.AppendAllText(path, i.ToString() + "\n");
    }

    string CheckIfFileExists(string path)
    {
        if (File.Exists(path))
        {
            currentFile += 1;
            path = Application.dataPath + "/ActivetyValues" + currentFile.ToString() + ".txt";
            CheckIfFileExists(path);
        }
        return path;
    }

    void PreFillActivetyList()
    {
        for(int i = 0; i < heatMap.GetCubes().Count; i++)
            newActivity.Add(0);
    }

    void ResetPath()
    {
        path = firstPath;
    }

    void AddAllSavedFiles()
    {
        int temp = 0;

        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            for(int i = 0; i < lines.Length; i++)
                newActivity[i] += Int32.Parse(lines[i]);

            temp += 1;
            path = Application.dataPath + "/ActivetyValues" + temp.ToString() + ".txt";
            AddAllSavedFiles();
        }
    }

    void LoadAllActivetyValuesFromFile(string path)
    {
        newActivity.Clear();
        string[] lines = File.ReadAllLines(path);
        for(int i = 0; i < lines.Length; i++)
            newActivity.Add(Int32.Parse(lines[i]));

        heatMap.SetActivety(newActivity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            PreFillActivetyList();
            ResetPath();
            AddAllSavedFiles();
            heatMap.SetActivety(newActivity);
        }
    }

    void OnApplicationQuit()
    {
        cubes = heatMap.GetCubes();
        for(int i = 0; i < cubes.Count; i++)
            activity.Add(cubes[i].GetComponent<HeatCubeInfo>().GetScore());
        CreateActivetyValueFile();
    }
}
