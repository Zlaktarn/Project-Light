using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPath : MonoBehaviour
{
    public Button button;
    public DropdownActivity dropdown;
    public HeatMapManager hm;

    private int activityType = 0;
    private int deathsType = 1;

    void Start()
    {
        button.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        if(dropdown.selectedPath.Contains(hm.activetyValue))
            hm.LoadSpecificFile(dropdown.selectedPath, activityType);
        else if(dropdown.selectedPath.Contains(hm.deathValue))
            hm.LoadSpecificFile(dropdown.selectedPath, deathsType);
    }
}
