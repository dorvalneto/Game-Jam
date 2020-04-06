﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridController : MonoBehaviour
{
    public int x;
    public int y;
    [FormerlySerializedAs("GridWidth")] public float gridWidth = 1f;
    [FormerlySerializedAs("GridHeighth")] public float gridHeighth = 1f;
        [FormerlySerializedAs("Prefabs")] public GameObject[] _prefabsList;
    private ItemCandy[,] _itemCandies;
    void Start()
    {
        FillGrid();
    }

    void FillGrid()
    {
        _itemCandies = new ItemCandy[this.x,this.y];
        for (int x = 0; x < this.x; x++)
        {
            for (int y = 0; y < this.y; y++)
            {
               _itemCandies[x,y] = gridPopulate(x,y);
            }
        }
    }

    ItemCandy gridPopulate(int x, int y)
    {
        GameObject randomItem = _prefabsList[Random.Range(0, _prefabsList.Length)];
       GameObject newItemGrid = (GameObject) Instantiate(randomItem, new Vector3(x * gridWidth, y * gridHeighth), Quaternion.identity);
       newItemGrid.GetComponent<ItemCandy>().ItemPosicao(x,y);
       return newItemGrid.GetComponent<ItemCandy>();
    }

    // void getPrefabsInPhath()
    // {
    //     _itensGrid = Resources.LoadAll<GameObject>("CandyPrefabs");
    //     for (int i = 0; i < _itensGrid.Length; i++)
    //     {
    //         _itensGrid[i].GetComponent<ItemCandy>().idetificacao = i;
    //     }
    // }
    
}
