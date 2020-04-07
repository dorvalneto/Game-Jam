using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GridController : MonoBehaviour
{
    [FormerlySerializedAs("x")] public int xSize;
    [FormerlySerializedAs("y")] public int ySize;
    [FormerlySerializedAs("GridWidth")] public float gridWidth = 1f;
    [FormerlySerializedAs("GridHeighth")] public float gridHeighth = 1f;
    [FormerlySerializedAs("Prefabs")] public GameObject[] _prefabsList;
    private ItemCandy[,] _itemCandies;
    private ItemCandy _itemAtual;
    private bool canPlay;
    private float delayBetweenMatch = 0.2f;

    void Start()
    {
        canPlay = true;
        GenerateIdetificacoPrefabs();
        FillGrid();
        ClearGrid();
        ItemCandy.clickItemHandler += clickItem;
    }

    private void OnDisable()
    {
        ItemCandy.clickItemHandler -= clickItem;
    }

    void FillGrid()
    {
        _itemCandies = new ItemCandy[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                _itemCandies[x, y] = gridPopulate(x, y);
                Debug.Log("FillGrid populate");
            }
        }
    }

    void ClearGrid()
    {
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                MachInfo machInfo = GetMatchInfo(_itemCandies[x, y]);
                if (machInfo.validMatch)
                {
                    Destroy(_itemCandies[x, y].gameObject);
                    _itemCandies[x, y] = gridPopulate(x, y);
                    Debug.Log("ClearGrid populate");
                    y--;
                }
            }
        }
    }

    ItemCandy gridPopulate(int x, int y)
    {
        GameObject randomItem = _prefabsList[Random.Range(0, _prefabsList.Length)];
        ItemCandy newItemGrid =
            ((GameObject) Instantiate(randomItem, new Vector3(x * gridWidth, y * gridHeighth), Quaternion.identity))
            .GetComponent<ItemCandy>();
        newItemGrid.ItemPosicao(x, y);
        return newItemGrid;
    }

    void clickItem(ItemCandy item)
    {
        if (item == _itemAtual || !canPlay)
        {
            return;
        }

        if (_itemAtual == null)
        {
            _itemAtual = item;
        }
        else
        {
            float xDiff = Mathf.Abs(item.x - _itemAtual.x);
            float yDiff = Mathf.Abs(item.y - _itemAtual.y);
            if (xDiff + yDiff == 1)
            {
                StartCoroutine(tryMactch(_itemAtual, item));
            }
            else
            {
                Debug.Log("intes não podem ser trocado");
            }

            _itemAtual = null;
        }
    }

    IEnumerator TrocarItem(ItemCandy a, ItemCandy b)
    {
        ChagedRigidbodyStatus(false);
        StartCoroutine(a.transform.Move(b.transform.position, 0.1f));
        StartCoroutine(b.transform.Move(a.transform.position, 0.1f));
        yield return new WaitForSeconds(delayBetweenMatch);
        TrocaIndices(a, b);
        ChagedRigidbodyStatus(true);
    }

    void TrocaIndices(ItemCandy a, ItemCandy b)
    {
        ItemCandy tempA = _itemCandies[a.x, a.y];
        _itemCandies[a.x, a.y] = b;
        _itemCandies[b.x, b.y] = tempA;
        int bTempIndiceX = b.x;
        int bTempIndiceY = b.y;
        b.ItemPosicao(a.x, a.y);
        a.ItemPosicao(bTempIndiceX, bTempIndiceY);
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
        List<ItemCandy> itensHorizontal = new List<ItemCandy> {item};
        int esquerda = item.x - 1;
        int direita = item.x + 1;
        while (esquerda >= 0 && _itemCandies[esquerda, item.y].idetificacao == item.idetificacao)
        {
            itensHorizontal.Add(_itemCandies[esquerda, item.y]);
            esquerda--;
        }

        while (direita < xSize && _itemCandies[direita, item.y].idetificacao == item.idetificacao)
        {
            itensHorizontal.Add(_itemCandies[direita, item.y]);
            direita++;
        }

        return itensHorizontal;
    }

    List<ItemCandy> machVertical(ItemCandy itemCandy)
    {
        List<ItemCandy> itensVertical = new List<ItemCandy> {itemCandy};
        int baixo = itemCandy.y - 1;
        int cima = itemCandy.y + 1;
        while (baixo >= 0 && _itemCandies[itemCandy.x, baixo].idetificacao == itemCandy.idetificacao)
        {
            itensVertical.Add(_itemCandies[itemCandy.x, baixo]);
            baixo--;
        }

        while (cima < ySize && _itemCandies[itemCandy.x, cima].idetificacao == itemCandy.idetificacao)
        {
            itensVertical.Add(_itemCandies[itemCandy.x, cima]);
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
        if (horizontalMach.Count >= 3 && horizontalMach.Count > verticalMatch.Count)
        {
            machInfo.matchInicioEixoX = GetMinimuX(horizontalMach);
            machInfo.matchFimEixoX = GetMaximoX(horizontalMach);
            machInfo.matchIncioEixoY = horizontalMach[0].y;
            machInfo.match = horizontalMach;
        }
        else if (verticalMatch.Count >= 3)
        {
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

    IEnumerator tryMactch(ItemCandy a, ItemCandy b)
    {
        canPlay = false;
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
            Debug.Log("Destroy MatchA");
            yield return new WaitForSeconds(delayBetweenMatch);
            yield return StartCoroutine(UpdateGrideAfterMatch(matchA));
        }
        else if (matchB.validMatch)
        {
            yield return StartCoroutine(DestroyItems(matchB.match));
            Debug.Log("Destroy MatchB");
            yield return new WaitForSeconds(delayBetweenMatch);
            yield return StartCoroutine(UpdateGrideAfterMatch(matchB));
        }

        canPlay = true;
    }

    IEnumerator DestroyItems(List<ItemCandy> itens)
    {
        foreach (var iten in itens)
        {
            yield return StartCoroutine(iten.transform.Scale(Vector3.zero, 0.1f));
                Destroy(iten.gameObject);
        }
    }

    void GenerateIdetificacoPrefabs()
    {
        for (int i = 0; i < _prefabsList.Length; i++)
        {
            _prefabsList[i].GetComponent<ItemCandy>().idetificacao = i;
        }
    }

    IEnumerator UpdateGrideAfterMatch(MachInfo match)
    {
        canPlay = false;
        if (match.matchIncioEixoY == match.matchFimEixoY)
        {
            //match horizontal
            for (int x = match.matchInicioEixoX; x <= match.matchFimEixoX; x++)
            {
                for (int y = match.matchIncioEixoY; y < ySize - 1; y++)
                {
                    ItemCandy upperIndex = _itemCandies[x, y + 1];
                    ItemCandy current = _itemCandies[x, y];
                    _itemCandies[x, y] = upperIndex;
                    _itemCandies[x, y + 1] = current;
                    _itemCandies[x, y].ItemPosicao(_itemCandies[x, y].x, _itemCandies[x, y].y - 1);
                }

                _itemCandies[x, ySize - 1] = gridPopulate(x, ySize - 1);
                Debug.Log("Update horizontal populate");
            }
        }
        else if (match.matchInicioEixoX == match.matchFimEixoX)
        {
            //match vertical
            int matchHeight = 1+ (match.matchFimEixoY - match.matchIncioEixoY);
            for (int y = match.matchIncioEixoY + matchHeight; y <= ySize - 1; y++)
            {
                ItemCandy indiceBaixo = _itemCandies[match.matchInicioEixoX, y - matchHeight];
                ItemCandy atual = _itemCandies[match.matchInicioEixoX, y];
                _itemCandies[match.matchInicioEixoX, y - matchHeight] = atual;
                _itemCandies[match.matchInicioEixoX, y] = indiceBaixo;
            }

            for (int y = 0; y < ySize - matchHeight; y++)
            {
                _itemCandies[match.matchInicioEixoX, y].ItemPosicao(match.matchInicioEixoX, y);
            }

            for (int i = 0; i < match.match.Count; i++)
            {
                _itemCandies[match.matchInicioEixoX, (ySize - 1) - i] =
                    gridPopulate(match.matchInicioEixoX, (ySize - 1) - i);
                Debug.Log("Update vertical populate");
            }
        }

        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                MachInfo machInfo = GetMatchInfo(_itemCandies[x, y]);
                if (machInfo.validMatch)
                {
                    // yield return new WaitForSeconds(0.1f);
                    yield return StartCoroutine(DestroyItems(machInfo.match));
                    Debug.Log("Destroy Update");
                    yield return new WaitForSeconds(delayBetweenMatch);
                    yield return StartCoroutine(UpdateGrideAfterMatch(machInfo));
                }
            }
        }

        canPlay = true;
    }
}