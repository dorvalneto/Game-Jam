using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TetrisManager : MonoBehaviour
{
    public static int totalLines = 6;
    public static int lines = 0;
    public Text textUI;
    public Image[] images;
    public GameObject gameOverCanvas;

    // Start is called before the first frame update
    void Start(){
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = string.Concat("Lines: " , lines.ToString() , " / " , totalLines.ToString());

        if (lines == totalLines) {
            GameOver();
        }

    }

    private void GameOver() {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadScene(string scene){
        SceneManager.LoadScene(scene);
    }

    public void AddLine(int linecount){
        lines = linecount;

        if (lines > 0){
            AddImage();
        }
    }

    private void AddImage()
    {
        for (int i = 0; i < lines; i++){
            images[i].gameObject.SetActive(true);
        }
    }
}
