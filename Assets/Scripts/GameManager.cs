using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    GameObject infoCanvas;

    [SerializeField]
    Image infoImage;

    [SerializeField]
    TextMeshProUGUI infoText;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
            infoCanvas.SetActive(false);
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
}
