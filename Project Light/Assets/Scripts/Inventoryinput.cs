using UnityEngine;

public class Inventoryinput : MonoBehaviour
{
    [SerializeField] public GameObject InventoryObject;
    [SerializeField] GameObject EquipPanelObject;

    [SerializeField] KeyCode[] toggleInventory;

    private void Start()
    {
        InventoryObject.SetActive(!InventoryObject.activeSelf);
    }
    void Update()
    {
        for (int i = 0; i < toggleInventory.Length; i++)
        {
            if(Input.GetKeyDown(toggleInventory[i]))
            {
                InventoryObject.SetActive(!InventoryObject.activeSelf);

                if(InventoryObject.activeSelf || PauseMenu.GameIsPaused)
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
        if(!PauseMenu.GameIsPaused && !InventoryObject.activeSelf)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
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
