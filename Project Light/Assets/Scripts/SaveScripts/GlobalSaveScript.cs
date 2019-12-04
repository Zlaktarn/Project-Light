using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GlobalSaveScript
{
    public MovementScript player;
    public PlantSeed[] plants;
    public Inventory inventory;
    private int[] inv;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SavePlayer();
            SaveInventory();

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            LoadPlayer();
            LoadInventory();
        }

    }

    public void SavePlayer()
    {

            SaveSystem.SavePlayer(player);
        SaveSystem.SaveInventory(inventory);
    }

    public void LoadPlayer()
    {
            SceneManager.LoadScene("FINALSCENE");
            PlayerData pData = SaveSystem.LoadPlayer();
            player.health = pData.health;

            Vector3 position;
            position.x = pData.position[0];
            position.y = pData.position[1];
            position.z = pData.position[2];
            player.transform.position = position;
        
    }

    public void SaveInventory()
    {
    }

    public void LoadInventory()
    {
        PlayerData invData = SaveSystem.LoadInventory();

        inventory.gAmmoTotal = invData.inv[0];
        inventory.rAmmoTotal = invData.inv[1];
        inventory.cbAmmoTotal = invData.inv[2];
        inventory.gun = invData.inv[3];
        inventory.rifle = invData.inv[4];
        inventory.crossbow = invData.inv[5];
        Inventory.water = invData.inv[6];
    }
}
