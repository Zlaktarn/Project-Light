using UnityEngine;

public class Inventoryinput : MonoBehaviour
{
    [SerializeField] GameObject InventoryObject;
    [SerializeField] GameObject EquipPanelObject;

    [SerializeField] KeyCode[] toggleInventory;
    void Update()
    {
        for (int i = 0; i < toggleInventory.Length; i++)
        {
            if(Input.GetKeyDown(toggleInventory[i]))
            {
                InventoryObject.SetActive(!InventoryObject.activeSelf);

                if(InventoryObject.activeSelf)
                {
                    ShowMouse();
                }
                else
                {
                    HideMouse();
                }
                break;
            }
        }
    }
    public void HideMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ShowMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ToggelEquipPanel()
    {
        EquipPanelObject.SetActive(!EquipPanelObject.activeSelf);
    }
}
