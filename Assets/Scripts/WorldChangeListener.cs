using UnityEngine;
using System.Collections.Generic;

public class WorldChangeListener : MonoBehaviour
{
    [SerializeField]
    List<GameObject> normalWorldObjects;

    [SerializeField]
    List<GameObject> disturbedWorldObjects;


    private void OnEnable()
    {
        GameManager.OnWorldChanged += OnWorldChanged;  
    }

    private void OnDisable()
    {
        GameManager.OnWorldChanged -= OnWorldChanged;
    }

    void OnWorldChanged(bool worldIsDisturbed)
    {
        if (worldIsDisturbed)
        {
            foreach (GameObject obj in normalWorldObjects)
            {
                obj.SetActive(false);
            }

            foreach (GameObject obj in disturbedWorldObjects)
            {
                obj.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject obj in normalWorldObjects)
            {
                obj.SetActive(true);
            }

            foreach (GameObject obj in disturbedWorldObjects)
            {
                obj.SetActive(false);
            }
        }
    }
}
