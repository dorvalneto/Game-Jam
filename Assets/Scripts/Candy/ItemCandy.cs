using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCandy : MonoBehaviour
{
    public int x
    {
        get;
        private set;
    }
    public int y
    {
        get;
        private set;
    }

    public int idetificacao;
    
    public delegate void clickItem(ItemCandy item);

    public static event clickItem clickItemHandler;
    public void ItemPosicao(int x,int y)
    {
        this.x = x;
        this.y = y;
        gameObject.name = string.Format("posicao: [{0}], [{1}]", x, y);
    }

    private void OnMouseDown()
    {
        if (clickItemHandler != null)
        {
            clickItemHandler(this);
        }
    }
}
