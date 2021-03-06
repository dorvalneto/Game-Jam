﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlappyBirdManager : MonoBehaviour
{
    public GameObject gameOverCanvas;

    private void Start()    {
        Time.timeScale = 1;
    }

    public void GameOver(){
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void Replay(){
        SceneManager.LoadScene("FlappyBirdScene");
    }

    public void LoadScene(string scene){
        SceneManager.LoadScene(scene);
    }
}
