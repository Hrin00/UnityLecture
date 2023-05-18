using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneController : MonoBehaviour
{
    private TextMeshProUGUI hintText;
    private GameObject hintPanel;
    private GameObject easyButton;
    private GameObject hardButton;
    private GameObject root;

    public static int gameMode;
    // Start is called before the first frame update
    void Start()
    {
        root = GameObject.Find("Canvas");
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("TitleScene"))
        {
            hintPanel = root.transform.Find("HintPanel").gameObject;
            hintText = hintPanel.transform.Find("HintText").gameObject.GetComponent<TextMeshProUGUI>();
            easyButton = root.transform.Find("Easy").gameObject;
            hardButton = root.transform.Find("Hard").gameObject;
        }
        else if (scene.name.Equals("MainScene"))
            Debug.Log(gameMode);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickGameLevelButton(string level)
    {
        switch (level)
        {
            case "Easy":
                hintText.text = "This is Easy Mode.You can touch the Wall.Try to touch all cube!";
                gameMode = 0;
                break;
            case "Hard":
                hintText.text = "This is Hard Mode.\nIf you touch the wall, Game Over.\nTry to touch all cube!";
                gameMode = 1;
                break;
        }
        hintPanel.SetActive(true);
        easyButton.SetActive(false);
        hardButton.SetActive(false);
    }

    public void OnClickGameStartButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

}
