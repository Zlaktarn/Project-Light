using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //SAVE

    public static void SavePlayer(MovementScript player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.aml";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        Debug.LogError("Save file CREATED " + path);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void SaveInventory(Inventory inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/inventory.aml";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(inventory);
        Debug.LogError("Save file CREATED " + path);
        formatter.Serialize(stream, data);

        stream.Close();
    }
    public static void SaveSpawnedPlants(PlantSeed ps)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/plants.aml";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(ps);
        Debug.LogError("Save file CREATED " + path);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    //LOAD

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.aml";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            Debug.LogError("Save file FOUND " + path);
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }


    }

    public static PlayerData LoadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.aml";

        if (File.Exists(path))
        {

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            Debug.LogError("Save file FOUND " + path);
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }


    }
}
