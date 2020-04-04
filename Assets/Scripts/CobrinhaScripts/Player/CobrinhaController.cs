
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CobrinhaController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject corpo;
    private MovimentacaoUtils _movimentacaoUtils;
    private KeyBoardUtils _boardUtils;
    private Rigidbody2D corpoRigido;
    private List<GameObject> _listCorpo;
    public float timeMovieCorpo;
    public float repeateRate;
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody2D>();
        _movimentacaoUtils = new MovimentacaoUtils();
        _boardUtils = new KeyBoardUtils();
        _listCorpo = new List<GameObject>();
        InvokeRepeating("movieLogic",timeMovieCorpo,repeateRate);
    }

    // Update is called once per frame
    void Update()
    {
        _movimentacaoUtils.movieUtils(_boardUtils.actionKeyBoard(),corpoRigido,speed);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "comida":
                _listCorpo.Add(Instantiate(corpo, transform.position, Quaternion.identity));
                Destroy(other.gameObject);
                Debug.Log(_listCorpo.Count);
                break;
            case "barreira":
                Debug.Log("barreira");
                break;
            case "corpo":
                Debug.Log("corpo");
                
                break;
        }
        Debug.Log("colider");
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
