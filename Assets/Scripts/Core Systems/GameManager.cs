using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using System;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = Instantiate(new GameObject("GameManager"), Vector3.zero, Quaternion.identity).AddComponent<GameManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    Tilemap groundTilemap;
    public Tilemap GroundTilemap { get { return groundTilemap; } }
    
    [SerializeField]
    Tilemap obstacleTilemap;
    public Tilemap ObstacleTilemap { get { return obstacleTilemap; } }

    [SerializeField]
    GameObject infoCanvas;

    [SerializeField]
    Image infoImage;

    [SerializeField]
    TextMeshProUGUI infoText;

    Dictionary<string, int> inventory = new Dictionary<string, int>();

    public static event Action<bool> OnWorldChanged;

    bool worldIsDisturbed = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            infoCanvas.SetActive(false);
            LoadInventory();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowInfoOnScreen(Sprite backgroundImage, string info)
    {
        infoCanvas.SetActive(true);
        infoImage.sprite = backgroundImage;
        infoText.text = info;
    }

    public void HideInfoScreen()
    {
        infoCanvas.SetActive(false);
        infoText.text = "";
    }

    public void AddItemToInventory(string itemName, int quantity)
    {
        if (inventory.ContainsKey(itemName) == false)
        {
            inventory.Add(itemName, quantity);
        }
        else
        {
            inventory[itemName] += quantity;
        }
        PrintInventory();
    }

    void PrintInventory()
    {
        foreach (string item in inventory.Keys)
        {
            Debug.LogFormat("{0}: {1}", item, inventory[item]);
        }
    }

    public void PillWasTaken()
    {
        worldIsDisturbed = true;
        if (worldIsDisturbed)
        {
            Debug.Log("Firing World Changed Event");
            OnWorldChanged?.Invoke(worldIsDisturbed);
        }
    }

    public void LoadInventory()
    {
        inventory = SaveManager.Instance.LoadInventory();
        Debug.Log("Inventory loaded. Inventory: ");
        PrintInventory();
    }

    public void SaveInventory()
    {
        SaveManager.Instance.SaveData(inventory);
    }

    public void DebugAddItemToInventory(string itemToAdd)
    {
        AddItemToInventory(itemToAdd, 1);
    }
}
