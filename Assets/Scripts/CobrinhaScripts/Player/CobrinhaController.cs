using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CobrinhaController : MonoBehaviour
{
    public float speed = 5f;
    private MovimentacaoUtils _movimentacaoUtils;
    private KeyBoardUtils _boardUtils;
    private Rigidbody2D corpoRigido;
    private List<Transform> _listCorpo;
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody2D>();
        _movimentacaoUtils = new MovimentacaoUtils();
        _boardUtils = new KeyBoardUtils();
    }

    // Update is called once per frame
    void Update()
    {
        _movimentacaoUtils.movieUtils(_boardUtils.actionKeyBoard(),corpoRigido,speed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("comida"))
        {
            
        }
        
    }

    private void movieLogic()
    {
        if (_listCorpo != null && _listCorpo.Count > 0)
        {
            _listCorpo.Last().transform.position = transform.position;
            _listCorpo.Insert(0,_listCorpo.Last());
            _listCorpo.RemoveAt(_listCorpo.Count - 1);
        }
        
    }
}
