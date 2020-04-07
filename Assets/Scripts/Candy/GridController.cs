using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GridController : MonoBehaviour
{
    public int x;
    public int y;
    [FormerlySerializedAs("GridWidth")] public float gridWidth = 1f;
    [FormerlySerializedAs("GridHeighth")] public float gridHeighth = 1f;
    [FormerlySerializedAs("Prefabs")] public GameObject[] _prefabsList;
    private ItemCandy[,] _itemCandies;
    private ItemCandy _itemAtual;

    void Start()
    {
        getPrefabsInPhath();
        FillGrid();
        ItemCandy.clickItemHandler += clickItem;
    }

    private void OnDisable()
    {
        ItemCandy.clickItemHandler -= clickItem;
    }

    void FillGrid()
    {
        _itemCandies = new ItemCandy[this.x, this.y];
        for (int x = 0; x < this.x; x++)
        {
            for (int y = 0; y < this.y; y++)
            {
                _itemCandies[x, y] = gridPopulate(x, y);
            }
        }
    }

    ItemCandy gridPopulate(int x, int y)
    {
        GameObject randomItem = _prefabsList[Random.Range(0, _prefabsList.Length)];
        GameObject newItemGrid = (GameObject) Instantiate(randomItem, new Vector3(x * gridWidth, y * gridHeighth),
            Quaternion.identity);
        newItemGrid.GetComponent<ItemCandy>().ItemPosicao(x, y);
        return newItemGrid.GetComponent<ItemCandy>();
    }

    void clickItem(ItemCandy item)
    {
        if (item == _itemAtual)
        {
            return;
        }

        if (_itemAtual != null)
        {
            float xDiff = Mathf.Abs(item.x - _itemAtual.x);
            float yDiff = Mathf.Abs(item.y - _itemAtual.y);
            if (xDiff + yDiff == 1)
            {
                StartCoroutine(tryMactch(_itemAtual, item));
            }
            _itemAtual = null;
        }
        else
        {
            _itemAtual = item;
        }
    }

    IEnumerator TrocarItem(ItemCandy a, ItemCandy b)
    {
        ChagedRigidbodyStatus(false);
        StartCoroutine(a.transform.Move(b.transform.position, 0.1f));
        StartCoroutine(b.transform.Move(a.transform.position, 0.1f));
        yield return new WaitForSeconds(0.1f);
        TrocaIndices(a,b);
        ChagedRigidbodyStatus(true);
    }

    void TrocaIndices(ItemCandy a, ItemCandy b)
    {
        ItemCandy tempA = _itemCandies[a.x,a.y];
        _itemCandies[a.x, a.y] = b;
        _itemCandies[b.x, b.y] = tempA;
        int bTempIndiceX = b.x;
        int bTempIndiceY = b.y;
        b.ItemPosicao(a.x,a.y);
        a.ItemPosicao(bTempIndiceX,bTempIndiceY);
        
    }

    void ChagedRigidbodyStatus(bool status)
    {
        foreach (var itemCandy in _itemCandies)
        {
            itemCandy.GetComponent<Rigidbody2D>().isKinematic = !status;
        }
    }


    List<ItemCandy> machHorizontal(ItemCandy item)
    {
        List<ItemCandy> itensHorizontal = new List<ItemCandy>{item};
        int esquerda = item.x - 1;
        int direita = item.x + 1;
        while (esquerda>=0 && _itemCandies[esquerda,item.y].idetificacao == item.idetificacao)
        {
            itensHorizontal.Add(_itemCandies[esquerda,item.y]);
            esquerda--;
        }

        while (direita<x && _itemCandies[direita,item.y].idetificacao == item.idetificacao)
        {
            itensHorizontal.Add(_itemCandies[direita,item.y]);
            direita++;
        }

        return itensHorizontal;
    }

    List<ItemCandy> machVertical(ItemCandy itemCandy)
    {
        List<ItemCandy> itensVertical = new List<ItemCandy>{itemCandy};
        int baixo = itemCandy.y - 1;
        int cima = itemCandy.y + 1;
        while (baixo>=0 && _itemCandies[itemCandy.x,baixo].idetificacao == itemCandy.idetificacao)
        {
            itensVertical.Add(_itemCandies[itemCandy.x,baixo]);
            baixo--;
        }

        while (cima<y && _itemCandies[itemCandy.x,cima].idetificacao == itemCandy.idetificacao)
        {
            itensVertical.Add(_itemCandies[itemCandy.x,cima]);
            cima++;
        }

        return itensVertical;
    }

    MachInfo GetMatchInfo(ItemCandy itemCandy)
    {
        MachInfo machInfo = new MachInfo();
        machInfo.match = null;
        List<ItemCandy> horizontalMach = machHorizontal(itemCandy);
        List<ItemCandy> verticalMatch = machVertical(itemCandy);
        if (horizontalMach.Count > 3 && horizontalMach.Count > verticalMatch.Count )
        {
            //definir regras
            machInfo.matchInicioEixoX = GetMinimuX(horizontalMach);
            machInfo.matchFimEixoX = GetMaximoX(horizontalMach);
            machInfo.matchIncioEixoY = horizontalMach[0].y;
            machInfo.match = horizontalMach;
        }
        else if(verticalMatch.Count>3)
        {
            //regras e pa
            machInfo.matchIncioEixoY = GetMinimuY(verticalMatch);
            machInfo.matchFimEixoY = GetMaximoY(verticalMatch);
            machInfo.matchIncioEixoY = verticalMatch[0].x;
            machInfo.match = verticalMatch;
        }
        return machInfo;
    }

    int GetMinimuX(List<ItemCandy> itemCandies)
         {
             float[] indices = new float[itemCandies.Count];
             for (int i = 0; i < indices.Length; i++)
             {
                 indices[i] = itemCandies[i].x;
             }
     
             return (int) Mathf.Min(indices);
         }
    int GetMaximoX(List<ItemCandy> itemCandies)
    {
        float[] indices = new float[itemCandies.Count];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = itemCandies[i].x;
        }

        return (int) Mathf.Max(indices);
    }
    int GetMinimuY(List<ItemCandy> itemCandies)
    {
        float[] indices = new float[itemCandies.Count];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = itemCandies[i].y;
        }
     
        return (int) Mathf.Min(indices);
    }
    int GetMaximoY(List<ItemCandy> itemCandies)
    {
        float[] indices = new float[itemCandies.Count];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = itemCandies[i].y;
        }

        return (int) Mathf.Max(indices);
    }

    IEnumerator tryMactch(ItemCandy a,ItemCandy b)
    {
        yield return StartCoroutine(TrocarItem(a, b));
        MachInfo matchA = GetMatchInfo(a);
        MachInfo matchB = GetMatchInfo(b);
        if (!matchA.validMatch && !matchB.validMatch)
        {
            yield return StartCoroutine(TrocarItem(a, b));
            yield break;
        }

        if (matchA.validMatch)
        {
            yield return StartCoroutine(DestroyItems(matchA.match));
        }else if (matchB.validMatch)
        {
            yield return StartCoroutine(DestroyItems(matchB.match));
        }
    }

    IEnumerator DestroyItems(List<ItemCandy> itens)
    {
        foreach (var iten in itens)
        {
            yield return StartCoroutine(iten.transform.Scale(Vector3.zero, 0.05f));
            Destroy(iten.gameObject);
        }
    }

    void getPrefabsInPhath()
    {
        for (int i = 0; i < _prefabsList.Length; i++)
        {
            _prefabsList[i].GetComponent<ItemCandy>().idetificacao = i;
        }
    }
}