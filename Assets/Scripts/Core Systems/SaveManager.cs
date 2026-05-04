using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;

public class SaveManager : MonoBehaviour
{
    static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newInstance = new GameObject();
                newInstance.name = "SaveManager";
                instance = newInstance.AddComponent<SaveManager>();
                DontDestroyOnLoad(newInstance);
            }

            return instance;
        }
    }

    string filename = "data.ddg";


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SaveData(Dictionary<string, int> inventory)
    {
        string saveString = "";
        foreach (string i in inventory.Keys)
        {
            saveString += $"{i}-{inventory[i]}/";
        }
        Debug.Log(saveString);

        try
        {
            string path = Application.persistentDataPath + "/" + filename;
            using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.Unicode))
            {
                Debug.LogFormat("Path: {0}", path);
                writer.WriteLine(saveString);
                writer.Flush();
                writer.Close();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogErrorFormat("Couldn't save string {0}. {1}", filename, e.Message);
        }
    }

    public Dictionary<string, int> LoadInventory()
    {
        Dictionary<string, int> loadedInventory = new Dictionary<string, int>();

        string path = Application.persistentDataPath + "/" + filename;
        string inventoryString = "";

        if (FileExists(path))
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    inventoryString = sr.ReadToEnd();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogErrorFormat("Couldn't load string {0}. {1}", filename, e.Message);
            }
        }
        else
        {
            Debug.LogWarning("File does not exist");
        }

        if (inventoryString.Length > 0)
        {
            List<string> itemsStrings = new List<string>();
            itemsStrings.AddRange(inventoryString.Split('/'));

            foreach (string item in itemsStrings)
            {
                List<string> loadedItem = new List<string>();
                loadedItem.AddRange(item.Split("-"));

                if (loadedItem.Count < 2) break;

                loadedInventory.Add(loadedItem[0], Int32.Parse(loadedItem[1]));
            }
        }
        return loadedInventory;
    }

    bool FileExists(string path)
    {
        FileInfo info = new FileInfo(path);
        return info.Exists;
    }
}
