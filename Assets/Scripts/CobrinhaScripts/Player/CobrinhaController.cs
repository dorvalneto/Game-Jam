
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using Debug = UnityEngine.Debug;

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
    private float _distanceSpawX;
    private float _distanceSpaey;
    private ColiderCorpoController _coliderCorpoController;
    
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody2D>();
        _movimentacaoUtils = new MovimentacaoUtils();
        _boardUtils = new KeyBoardUtils();
        _listCorpo = new List<GameObject>();
        _distanceSpawX = GetComponent<BoxCollider2D>().size.x/2;
        _distanceSpaey = GetComponent<BoxCollider2D>().size.y/2;
        InvokeRepeating("movieLogic",timeMovieCorpo,repeateRate);
        _coliderCorpoController = GetComponentInChildren<ColiderCorpoController>();
    }

    // Update is called once per frame
    void Update()
    {
        _movimentacaoUtils.movieUtils(_boardUtils.actionKeyBoard(),corpoRigido,speed);
        _coliderCorpoController.controleCasoCollider(_boardUtils.actionKeyBoard());
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
                //game game
                gameOver();
                break;
        }
        Debug.Log("colider");
    }

    private Vector3 distaceLogic()
    {
        switch(_boardUtils.actionKeyBoard())
        {
            case MOVIE.Baixo:
               return new Vector3(transform.position.x, transform.position.y + _distanceSpaey, transform.position.z);
            case  MOVIE.Cima:
                return new Vector3(transform.position.x, transform.position.y - _distanceSpaey, transform.position.z);
            case MOVIE.Esquerda:
                return new Vector3(transform.position.x + _distanceSpawX, transform.position.y, transform.position.z);
            default:
                return new Vector3(transform.position.x - _distanceSpawX, transform.position.y, transform.position.z);
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

    private void gameOver()
    {
        foreach (var calda in _listCorpo)
        {
            Destroy(calda);
        }
        Destroy(this.gameObject);
    }
}
