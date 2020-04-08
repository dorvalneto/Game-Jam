using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GAMEMANAGER : MonoBehaviour
{

    public static GAMEMANAGER instance;
    public GameObject[] passaro;
    public int passarosNum;
    public int passarosEmCena = 0;
    public Transform pos;
    public bool win;
    public bool jogoComecou;
    //public string nomePassaro;

    //public bool passaroLancado = false;
    //public Transform objE, objD;

    //public int numPorcosCena;
    //private bool tocaWin = false, tocaLose = false;
    //public bool lose;

    //public bool estrela1Fim, estrela2Fim;
    //public int aux;

    //public int estrelasNum;
    //public bool trava = false;

    //public int pontosGame, bestPontoGame;
    //public int moedasGame;

    //public bool pausado = false;



    void Awake()
    {

        //ZPlayerPrefs.Initialize("12345678", "pombobravogame");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


        SceneManager.sceneLoaded += Carrega;



    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        //    if (ONDEESTOU.instance.fase != 0 && ONDEESTOU.instance.fase != 5 && ONDEESTOU.instance.fase != 6 && ONDEESTOU.instance.fase != 7 && ONDEESTOU.instance.fase != 8 && ONDEESTOU.instance.fase != 9)
        //    {

        //        pos = GameObject.FindWithTag("pos").GetComponent<Transform>();
        //        objE = GameObject.FindWithTag("PE").GetComponent<Transform>();
        //        objD = GameObject.FindWithTag("PD").GetComponent<Transform>();

        //        StartGame();
        //        //Passaro Pos

        //        passarosNum = GameObject.FindGameObjectsWithTag("Player").Length;
        //        passaro = new GameObject[passarosNum];

        //        for (int x = 0; x < GameObject.FindGameObjectsWithTag("Player").Length; x++)
        //        {
        //            passaro[x] = GameObject.Find("Bird" + x);
        //        }

        //        //

        //        numPorcosCena = GameObject.FindGameObjectsWithTag("porco").Length;
        //        aux = passarosNum;
        //    }
    }

    //void NascPassaro()
    //{
    //    if (passarosEmCena == 0 && passarosNum > 0)
    //    {
    //        for (int x = 0; x < passaro.Length; x++)
    //        {
    //            if (passaro[x] != null)
    //            {
    //                if (passaro[x].transform.position != pos.position && passarosEmCena == 0)
    //                {
    //                    nomePassaro = passaro[x].name;
    //                    passaro[x].transform.position = pos.position;
    //                    passarosEmCena = 1;
    //                }
    //            }
    //        }
    //    }
    //}


    //void GameOver()
    //{
    //    if (ONDEESTOU.instance.fase != 0 && ONDEESTOU.instance.fase != 5 && ONDEESTOU.instance.fase != 6 && ONDEESTOU.instance.fase != 7 && ONDEESTOU.instance.fase != 8 && ONDEESTOU.instance.fase != 9)
    //    {

    //        jogoComecou = false;

    //        UIMANAGER.instance.painelGameOver.Play("MenuLoseAnimado");
    //        if (!UIMANAGER.instance.loseSom.isPlaying && tocaLose == false)
    //        {
    //            UIMANAGER.instance.loseSom.Play();
    //            tocaLose = true;
    //        }
    //    }
    //}


    //void WinGame()
    //{
    //    if (ONDEESTOU.instance.fase != 0 && ONDEESTOU.instance.fase != 5 && ONDEESTOU.instance.fase != 6 && ONDEESTOU.instance.fase != 7 && ONDEESTOU.instance.fase != 8 && ONDEESTOU.instance.fase != 9)
    //    {

    //        if (jogoComecou != false)
    //        {

    //            SCOREMANAGER.instance.SalvarDados(moedasGame);

    //            int tempOnde = ONDEESTOU.instance.fase + 1;
    //            ZPlayerPrefs.SetInt("Level" + tempOnde + "_" + ONDEESTOU.instance.faseMestra, 1);

    //            jogoComecou = false;
    //            UIMANAGER.instance.painelWin.Play("MenuWinAnimado");

    //            if (!UIMANAGER.instance.winSom.isPlaying && tocaWin == false)
    //            {
    //                UIMANAGER.instance.winSom.Play();
    //                tocaWin = true;
    //            }

    //            //Pontos

    //            POINTMANAGER.instance.MelhorPontuacaoSave(ONDEESTOU.instance.faseN, pontosGame);

    //            //
    //        }



    //        if (tocaWin && !UIMANAGER.instance.winSom.isPlaying && trava == false)
    //        {
    //            if (passarosNum == aux - 1)
    //            {
    //                UIMANAGER.instance.estrela1.Play("Estrela1_animada");

    //                if (estrela1Fim)
    //                {
    //                    UIMANAGER.instance.estrela2.Play("Estrela2_animada");

    //                    if (estrela2Fim)
    //                    {
    //                        UIMANAGER.instance.estrela3.Play("Estrela3_animada");
    //                        trava = true;

    //                        UIMANAGER.instance.winBtnMenu.interactable = true;
    //                        UIMANAGER.instance.winBtnNovamente.interactable = true;
    //                        UIMANAGER.instance.winBtnProximo.interactable = true;
    //                    }
    //                }

    //                estrelasNum = 3;

    //            }
    //            else if (passarosNum == aux - 2)
    //            {
    //                UIMANAGER.instance.estrela1.Play("Estrela1_animada");

    //                if (estrela1Fim)
    //                {
    //                    UIMANAGER.instance.estrela2.Play("Estrela2_animada");
    //                    trava = true;

    //                    UIMANAGER.instance.winBtnMenu.interactable = true;
    //                    UIMANAGER.instance.winBtnNovamente.interactable = true;
    //                    UIMANAGER.instance.winBtnProximo.interactable = true;
    //                }

    //                estrelasNum = 2;
    //            }
    //            else if (passarosNum <= aux - 3)
    //            {
    //                UIMANAGER.instance.estrela1.Play("Estrela1_animada");
    //                estrelasNum = 1;
    //                trava = true;

    //                UIMANAGER.instance.winBtnMenu.interactable = true;
    //                UIMANAGER.instance.winBtnNovamente.interactable = true;
    //                UIMANAGER.instance.winBtnProximo.interactable = true;
    //            }
    //            else
    //            {

    //                estrelasNum = 0;
    //                trava = true;
    //            }

    //            if (!ZPlayerPrefs.HasKey(ONDEESTOU.instance.faseN + "estrelas"))
    //            {
    //                ZPlayerPrefs.SetInt(ONDEESTOU.instance.faseN + "estrelas", estrelasNum);
    //            }
    //            else
    //            {

    //                if (ZPlayerPrefs.GetInt(ONDEESTOU.instance.faseN + "estrelas") < estrelasNum)
    //                {
    //                    ZPlayerPrefs.SetInt(ONDEESTOU.instance.faseN + "estrelas", estrelasNum);
    //                }
    //            }


    //        }
    //    }
    //}

    //public void StartGame()
    //{
    //    jogoComecou = true;
    //    passarosEmCena = 0;
    //    win = false;
    //    lose = false;
    //    trava = false;
    //    passaroLancado = false;
    //    tocaLose = false;
    //    tocaWin = false;
    //    estrela1Fim = false;
    //    estrela2Fim = false;

    //    pontosGame = 0;
    //    bestPontoGame = POINTMANAGER.instance.MelhorPontuacaoLoad(ONDEESTOU.instance.faseN);

    //    UIMANAGER.instance.pontosTxt.text = pontosGame.ToString();
    //    UIMANAGER.instance.bestPontoTxt.text = bestPontoGame.ToString();

    //    moedasGame = SCOREMANAGER.instance.LoadDados();
    //    UIMANAGER.instance.moedasTxt.text = SCOREMANAGER.instance.LoadDados().ToString();

    //    UIMANAGER.instance.winBtnMenu.interactable = false;
    //    UIMANAGER.instance.winBtnNovamente.interactable = false;
    //    UIMANAGER.instance.winBtnProximo.interactable = false;

    //}

    //// Use this for initialization
    //void Start()
    //{

    //    StartGame();

    //    //ZPlayerPrefs.DeleteAll ();
    //}

    //// Update is called once per frame
    //void Update()
    //{




    //    if (numPorcosCena <= 0 && passarosNum > 0)
    //    {
    //        win = true;
    //    }
    //    else if (numPorcosCena > 0 && passarosNum <= 0)
    //    {
    //        lose = true;
    //    }

    //    if (win)
    //    {
    //        WinGame();

    //    }
    //    else if (lose)
    //    {
    //        GameOver();
    //    }


    //    if (jogoComecou)
    //    {
    //        NascPassaro();
    //    }

    //}
}
