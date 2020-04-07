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
    public AudioClip coletandoComida;
    private AudioSource soundEmmiter;

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
        soundEmmiter = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _movimentacaoUtils.movieUtils(_boardUtils.ActionKeyBoard(), _corpoRigido, speed);
        _boardUtils.getEventTouch();
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
               _listCorpo.Add(Instantiate(corpo, new Vector3(transform.position.x,transform.position.y,2f), Quaternion.identity));
                _huDcontroller.cont.text = _listCorpo.Count.ToString();
               Destroy(other.gameObject);
               soundEmmiter.clip = coletandoComida;
               soundEmmiter.Play();
               Debug.Log(_listCorpo.Count);
               break;
            case "barreira":
                _huDcontroller.gameOverSound();
                GameOver();
                break;
            case "corpo":
                _huDcontroller.gameOverSound();
                GameOver();
                break;
        }

        Debug.Log("colider");
    }

    private void CorpoMoveLogic()
    {
        if (_listCorpo != null && _listCorpo.Count > 0)
        {
            _listCorpo.Last().transform.position = new Vector3(transform.position.x,transform.position.y,2f);
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