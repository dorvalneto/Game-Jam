using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisManager : MonoBehaviour
{
    public static int totalLines = 6;
    public static int lines = 0;
    public Text textUI;
    public Image[] images;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textUI.text = string.Concat("Lines: " , lines.ToString() , " / " , totalLines.ToString());
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
