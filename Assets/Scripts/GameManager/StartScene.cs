using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    private Button startBtn;
    // Use this for initialization
    void Start()
    {
        startBtn = GameObject.Find("Canvas").transform.Find("StartButton").GetComponent<Button>();
        startBtn.onClick.AddListener(StartGame);
    }

    // Update is called once per frame
    void Update()
    {

    }




    public void StartGame()       //startbutton调用
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
