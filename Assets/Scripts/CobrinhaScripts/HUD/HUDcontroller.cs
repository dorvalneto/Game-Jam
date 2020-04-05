using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HUDcontroller : MonoBehaviour
{
    public Text title;

    public Text cont;

    [FormerlySerializedAs("GameOverLabel")] public Text gameOverLabel;
    
    public Image barTime;

    public float timeInGame;

    private void Start()
    {
        gameOverLabel.enabled = false;
    }

    private void Update()
    {
        timeGame();
    }
    public void setHudState(string title, string cont)
    {
        this.title.text = title;
        this.cont.text = cont;
    }

    private float timeGame()
    {
        this.barTime.fillAmount -= 1f / timeInGame * Time.deltaTime;
        return this.barTime.fillAmount;
    }

    public bool finishGame()
    {
        if (timeGame() <= 0)
        {
            return true;
        }
        return false;
    }
}