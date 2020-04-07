//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Serialization;
//using Random = UnityEngine.Random;

//public class GridController : MonoBehaviour
//{
//    public int x;
//    public int y;
//    [FormerlySerializedAs("GridWidth")] public float gridWidth = 1f;
//    [FormerlySerializedAs("GridHeighth")] public float gridHeighth = 1f;
//    [FormerlySerializedAs("Prefabs")] public GameObject[] _prefabsList;
//    private ItemCandy[,] _itemCandies;
//    private ItemCandy _itemAtual;

//    void Start()
//    {
//        FillGrid();
//        ItemCandy.clickItemHandler += clickItem;
//    }

//    private void OnDisable()
//    {
//        ItemCandy.clickItemHandler -= clickItem;
//    }

//    void FillGrid()
//    {
//        _itemCandies = new ItemCandy[this.x, this.y];
//        for (int x = 0; x < this.x; x++)
//        {
//            for (int y = 0; y < this.y; y++)
//            {
//                _itemCandies[x, y] = gridPopulate(x, y);
//            }
//        }
//    }

//    ItemCandy gridPopulate(int x, int y)
//    {
//        GameObject randomItem = _prefabsList[Random.Range(0, _prefabsList.Length)];
//        GameObject newItemGrid = (GameObject) Instantiate(randomItem, new Vector3(x * gridWidth, y * gridHeighth),
//            Quaternion.identity);
//        newItemGrid.GetComponent<ItemCandy>().ItemPosicao(x, y);
//        return newItemGrid.GetComponent<ItemCandy>();
//    }

//    void clickItem(ItemCandy item)
//    {
//        if (item == _itemAtual)
//        {
//            return;
//        }

//        if (_itemAtual != null)
//        {
//            float xDiff = Mathf.Abs(item.x - _itemAtual.x);
//            float yDiff = Mathf.Abs(item.y - _itemAtual.y);
//            if (xDiff + yDiff == 1)
//            {
//                StartCoroutine(TrocarItem(_itemAtual, item));
//            }
//            _itemAtual = null;
//        }
//        else
//        {
//            _itemAtual = item;
//        }
//    }

//    //IEnumerator TrocarItem(ItemCandy a, ItemCandy b)
//    //{
//    //    ChagedRigidbodyStatus(false);
//    //    StartCoroutine(a.transform.Move(b.transform.position, 0.1f));
//    //    StartCoroutine(b.transform.Move(a.transform.position, 0.1f));
//    //    yield return new WaitForSeconds(0.1f);
//    //    TrocaIndices(a,b);
//    //    ChagedRigidbodyStatus(true);
//    //}

//    void TrocaIndices(ItemCandy a, ItemCandy b)
//    {
//        ItemCandy tempA = _itemCandies[a.x,a.y];
//        _itemCandies[a.x, a.y] = b;
//        _itemCandies[b.x, b.y] = tempA;
//        int bTempIndiceX = b.x;
//        int bTempIndiceY = b.y;
//        b.ItemPosicao(a.x,a.y);
//        a.ItemPosicao(bTempIndiceX,bTempIndiceY);
        
//    }

//    void ChagedRigidbodyStatus(bool status)
//    {
//        foreach (var itemCandy in _itemCandies)
//        {
//            itemCandy.GetComponent<Rigidbody2D>().isKinematic = !status;
//        }
//    }

//    // void getPrefabsInPhath()
//    // {
//    //     _itensGrid = Resources.LoadAll<GameObject>("CandyPrefabs");
//    //     for (int i = 0; i < _itensGrid.Length; i++)
//    //     {
//    //         _itensGrid[i].GetComponent<ItemCandy>().idetificacao = i;
//    //     }
//    // }
//}