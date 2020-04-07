using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int gridSizeX, gridSizeY;
    public float cellWidth = 1.1f;
    public int matchMinimun = 3;
    public float delayBetweenMatches = 0.2f;

    private bool _canPlay;

    private GameObject[] _fruits;
    private GridItem[,] _items;
    private GridItem _selectedItem;

	void Start ()
    {
        _canPlay = true;
        GetFruits();
        CreateGrid();
        ClearGrid();
        GridItem.OnMouseOverItemEventHandler += OnMouseOverItem;
	}

    private void OnDisable()
    {
        GridItem.OnMouseOverItemEventHandler -= OnMouseOverItem;
    }

    void CreateGrid()
    {
        _items = new GridItem[gridSizeX, gridSizeY];

        for(int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                _items[x,y] = InstantiateFruit(x, y);
            }
        }
    }

    void ClearGrid()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                MatchInfo matchInfo = GetMatchInfo(_items[x, y]);
                if(matchInfo.IsMatchValid)
                {
                    Destroy(_items[x, y].gameObject);
                    _items[x,y] =  InstantiateFruit(x, y);
                    y--;
                }
            }
        }
    }

    GridItem InstantiateFruit(int xPos, int yPos)
    {
        GameObject randomFruit = _fruits[Random.Range(0, _fruits.Length)];
        GridItem newFruit = ((GameObject)Instantiate(randomFruit, new Vector2(xPos * cellWidth, yPos), Quaternion.identity)).GetComponent<GridItem>();
        newFruit.OnItemPositionChanged(xPos, yPos);
        return newFruit;
    }

    void OnMouseOverItem(GridItem item)
    {
        if (_selectedItem == item || _canPlay == false)
        {
            return;
        }

        if (_selectedItem == null)
        {
            _selectedItem = item;
            _selectedItem.GetComponent<Animator>().SetTrigger("Select");
        }
        else
        {
            int xResult = Mathf.Abs(item.x - _selectedItem.x);
            int yResult = Mathf.Abs(item.y - _selectedItem.y);

            if (xResult + yResult == 1)
            {
                StartCoroutine(TryMatch(_selectedItem, item, 0.1f));
            }

            _selectedItem.GetComponent<Animator>().SetTrigger("Deselect");
            _selectedItem = null;
        }
    }

    IEnumerator TryMatch(GridItem a, GridItem b, float duration)
    {
        _canPlay = false;
        yield return StartCoroutine(Swap(a, b, duration));

        MatchInfo matchInfoA = GetMatchInfo(a);
        MatchInfo matchInfoB = GetMatchInfo(b);

        if (!matchInfoA.IsMatchValid && !matchInfoB.IsMatchValid)
        {
            yield return StartCoroutine(Swap(a, b, duration));
            _canPlay = true;
            yield break;
        }

        if (matchInfoA.IsMatchValid)
        {
            yield return StartCoroutine(DestroyItems(matchInfoA.match));
            yield return new WaitForSeconds(delayBetweenMatches);
            yield return StartCoroutine(UpdateGrid(matchInfoA));
        }

        if (matchInfoB.IsMatchValid)
        {
            yield return StartCoroutine(DestroyItems(matchInfoB.match));
            yield return new WaitForSeconds(delayBetweenMatches);
            yield return StartCoroutine(UpdateGrid(matchInfoB));
        }
        _canPlay = true;
    }

    IEnumerator UpdateGrid(MatchInfo match)
    {
        if(match.verticalMatchStart == match.verticalMatchEnd)
        {
            for(int x = match.horizontalMatchStart; x <= match.horizontalMatchEnd; x++)
            {
                for(int y = match.verticalMatchStart; y < gridSizeY -1; y++)
                {
                    GridItem aboveItem = _items[x, y + 1];
                    GridItem currentItem = _items[x, y];
                    _items[x, y] = aboveItem;
                    _items[x, y + 1] = currentItem;
                    _items[x, y].OnItemPositionChanged(_items[x, y].x, _items[x, y].y - 1);
                }
                _items[x, gridSizeY - 1] = InstantiateFruit(x, gridSizeY - 1);
            }
        }
        else if(match.horizontalMatchStart == match.horizontalMatchEnd)
        {
            int height = 1 + (match.verticalMatchEnd - match.verticalMatchStart);

            for(int y = match.verticalMatchStart + height; y <= gridSizeY -1; y++)
            {
                GridItem belowItem = _items[match.horizontalMatchStart, y - height];
                GridItem current = _items[match.horizontalMatchStart, y];
                _items[match.horizontalMatchStart, y - height] = current;
                _items[match.horizontalMatchStart, y] = belowItem;
            }

            for(int y = 0; y < gridSizeY - height; y++)
            {
                _items[match.horizontalMatchStart, y].OnItemPositionChanged(match.horizontalMatchStart, y);
            }

            for(int i = 0; i < match.match.Count;  i++)
            {
                _items[match.horizontalMatchStart, (gridSizeY - 1) - i] = InstantiateFruit(match.horizontalMatchStart, (gridSizeY - 1) - i);
            }
        }

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                MatchInfo matchInfo = GetMatchInfo(_items[x, y]);
                if (matchInfo.IsMatchValid)
                {
                    yield return StartCoroutine(DestroyItems(matchInfo.match));
                    yield return new WaitForSeconds(delayBetweenMatches);
                    yield return StartCoroutine(UpdateGrid(matchInfo));
                }
            }
        }
    }

    IEnumerator DestroyItems(List<GridItem> items)
    {
        foreach (GridItem i in items)
        {
            yield return StartCoroutine(i.transform.Scale(Vector3.zero, 0.05f));
            Destroy(i.gameObject);
        }
    }

    IEnumerator Swap(GridItem a, GridItem b, float duration)
    {
        ManagePhysics(false);

        Vector3 posA = a.transform.position;
        Vector3 posB = b.transform.position;

        StartCoroutine(a.transform.Move(posB, duration));
        StartCoroutine(b.transform.Move(posA, duration));

        SwapGridPos(a, b);

        yield return new WaitForSeconds(duration);

        ManagePhysics(true);

    }

    void SwapGridPos(GridItem a, GridItem b)
    {
        GridItem tempA = _items[a.x, a.y];
        _items[a.x, a.y] = b;
        _items[b.x, b.y] = tempA;

        int aPosX = a.x, aPosY = a.y;

        a.OnItemPositionChanged(b.x, b.y);
        b.OnItemPositionChanged(aPosX, aPosY);
    }

    List<GridItem> CheckHorizontalMatches(GridItem item)
    {
        List<GridItem> horizontalMatches = new List<GridItem> { item };
        int left = item.x - 1, right = item.x + 1;

        while(left >= 0 && _items[left,item.y].id == item.id)
        {
            horizontalMatches.Add(_items[left, item.y]);
            left --;
        }

        while (right < gridSizeX && _items[right, item.y].id == item.id)
        {
            horizontalMatches.Add(_items[right, item.y]);
            right++;
        }

        return horizontalMatches;
    }

    List<GridItem> CheckVerticalMatches(GridItem item)
    {
        List<GridItem> verticalMatches = new List<GridItem> { item };
        int down = item.y - 1, up = item.y + 1;

        while (down >= 0 && _items[item.x, down].id == item.id)
        {
            verticalMatches.Add(_items[item.x, down]);
            down--;
        }

        while (up < gridSizeY && _items[item.x, up].id == item.id)
        {
            verticalMatches.Add(_items[item.x, up]);
            up++;
        }

        return verticalMatches;
    }

    MatchInfo GetMatchInfo(GridItem item)
    {
        MatchInfo mInfo = new MatchInfo();
        mInfo.match = null;

        List<GridItem> horizontalMatch = CheckHorizontalMatches(item);
        List<GridItem> verticalMatch = CheckVerticalMatches(item);

        if(horizontalMatch.Count > verticalMatch.Count && horizontalMatch.Count >= matchMinimun)
        {
            mInfo.horizontalMatchStart = GetHorizontalStart(horizontalMatch);
            mInfo.horizontalMatchEnd = GetHorizontalEnd(horizontalMatch);

            mInfo.verticalMatchStart = mInfo.verticalMatchEnd = horizontalMatch[0].y;

            mInfo.match = horizontalMatch;
        }
        else if(verticalMatch.Count >= matchMinimun)
        {
            mInfo.verticalMatchStart = GetVerticalStart(verticalMatch);
            mInfo.verticalMatchEnd = GetVerticalEnd(verticalMatch);

            mInfo.horizontalMatchStart = mInfo.horizontalMatchEnd = verticalMatch[0].x;

            mInfo.match = verticalMatch;
        }
        return mInfo;
    }

    int GetHorizontalStart(List<GridItem> items)
    {
        float[] indexes = new float[items.Count];
        for(int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = items[i].x;
        }
        return (int) Mathf.Min(indexes);
    }

    int GetHorizontalEnd(List<GridItem> items)
    {
        float[] indexes = new float[items.Count];
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = items[i].x;
        }
        return (int)Mathf.Max(indexes);
    }

    int GetVerticalStart(List<GridItem> items)
    {
        float[] indexes = new float[items.Count];
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = items[i].y;
        }
        return (int)Mathf.Min(indexes);
    }

    int GetVerticalEnd(List<GridItem> items)
    {
        float[] indexes = new float[items.Count];
        for (int i = 0; i < indexes.Length; i++)
        {
            indexes[i] = items[i].y;
        }
        return (int)Mathf.Max(indexes);
    }

    void GetFruits()
    {
        _fruits = Resources.LoadAll<GameObject>("Fruits");
    }

    void ManagePhysics(bool state)
    {
        foreach(GridItem i in _items)
        {
            i.GetComponent<Rigidbody2D>().isKinematic = !state;
        }
    }
}
