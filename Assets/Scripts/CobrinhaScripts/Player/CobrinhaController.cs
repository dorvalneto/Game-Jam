using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class CobrinhaController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject corpo;
    private MovimentacaoUtils _movimentacaoUtils;
    private KeyBoardUtils _boardUtils;
    private Rigidbody2D _corpoRigido;
    private List<GameObject> _listCorpo;
    public float timeMovieCorpo;
    public float repeateRate;
    private ColiderCorpoController _coliderCorpoController;
    private HUDcontroller _huDcontroller;

    void Start()
    {
        _corpoRigido = GetComponent<Rigidbody2D>();
        _movimentacaoUtils = new MovimentacaoUtils();
        _boardUtils = new KeyBoardUtils();
        _listCorpo = new List<GameObject>();
        InvokeRepeating("CorpoMoveLogic", timeMovieCorpo, repeateRate);
        _coliderCorpoController = GetComponentInChildren<ColiderCorpoController>();
        _huDcontroller = GameObject.Find("HUDController").GetComponent<HUDcontroller>();
        _huDcontroller.setHudState("Snake Bug", "0");
    }

    // Update is called once per frame
    void Update()
    {
        _boardUtils.getEventTouch();
        _movimentacaoUtils.movieUtils(_boardUtils.ActionKeyBoard(), _corpoRigido, speed);
        _coliderCorpoController.controleCasoCollider(_boardUtils.ActionKeyBoard());
        if (_huDcontroller.finishGame())
        {
            GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "comida":
                _listCorpo.Add(Instantiate(corpo, transform.position, Quaternion.identity));
                _huDcontroller.cont.text = _listCorpo.Count.ToString();
                Destroy(other.gameObject);
                Debug.Log(_listCorpo.Count);
                break;
            case "barreira":
                GameOver();
                break;
            case "corpo":
                GameOver();
                break;
        }

        Debug.Log("colider");
    }

    private void CorpoMoveLogic()
    {
        if (_listCorpo != null && _listCorpo.Count > 0)
        {
            _listCorpo.Last().transform.position = transform.position;
            _listCorpo.Insert(0, _listCorpo.Last());
            _listCorpo.RemoveAt(_listCorpo.Count - 1);
        }
    }

    private void GameOver()
    {
        foreach (var calda in _listCorpo)
        {
            Destroy(calda);
        }

        Destroy(gameObject);
        _huDcontroller.gameOverLabel.enabled = true;
    }
}