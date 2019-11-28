using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownActivity : MonoBehaviour
{
    List<string> paths = new List<string>();
    public Dropdown dropdown;
    public HeatMapManager hm;
    public string selectedPath;
    private bool enable = false;


    public void Dropdown_IndexChanged(int index)
    {
        selectedPath = paths[index];
    }

    void Update()
    {
        if (!enable)
        {
            PopulateList();
            enable = true;
        }
    }

    void PopulateList()
    {
        paths.AddRange(hm.FindAllPaths());
        dropdown.AddOptions(paths);
    }
}
