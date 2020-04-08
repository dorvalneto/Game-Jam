 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject losePainel;
    //    public static UIMANAGER instance;

    //    public Animator painelGameOver, painelWin, painelPause;

    //    public Button winBtnMenu, winBtnNovamente, winBtnProximo;
    //    public Animator estrela1, estrela2, estrela3;
    //    private Button loseBtnMenu, loseBtnNovamente;
    //    [SerializeField]
    //    private Button pauseBtn, pauseBtnPlay, pauseBtnNovamente, pauseBtnMenu, pauseBtnLoja;
    //    public AudioSource winSom;
    //    public AudioSource loseSom;
    //    public Text pontosTxt, bestPontoTxt;
    //    public Text moedasTxt;
    //    private Image fundoPreto;

    void Awake()
    {
        losePainel = GameObject.Find("LoserPainel ");
        ligaDesligaPainel();
        //        if (instance == null)
        //        {
        //            instance = this;
        //            DontDestroyOnLoad(this.gameObject);
        //        }
        //        else
        //        {
        //            Destroy(gameObject);
        //        }

        //        SceneManager.sceneLoaded += Carrega;

        //        DadosParaCarregamento();
    }
    void ligaDesligaPainel()
    {
        StartCoroutine(tempo());
    }

    public void GameOver()
    {
        losePainel.SetActive(true);
    }
    IEnumerator tempo()
    {
        yield return new WaitForSeconds (0.001f);
        losePainel.SetActive(false);
    }

    //    void Carrega(Scene cena, LoadSceneMode modo)
    //    {
    //        DadosParaCarregamento();
    //    }

    //    void DadosParaCarregamento()
    //    {
    //        if (ONDEESTOU.instance.fase != 0 && ONDEESTOU.instance.fase != 5 && ONDEESTOU.instance.fase != 6 && ONDEESTOU.instance.fase != 7 && ONDEESTOU.instance.fase != 8 && ONDEESTOU.instance.fase != 9)
    //        {
    //            //Painel
    //            painelGameOver = GameObject.Find("Menu_Lose").GetComponent<Animator>();
    //            painelWin = GameObject.Find("Menu_Win").GetComponent<Animator>();
    //            painelPause = GameObject.Find("Painel_Pause").GetComponent<Animator>();
    //            //Btn Win
    //            winBtnMenu = GameObject.Find("Button_Menu").GetComponent<Button>();
    //            winBtnNovamente = GameObject.Find("Button_Novamente").GetComponent<Button>();
    //            winBtnProximo = GameObject.Find("Button_Avancar").GetComponent<Button>();
    //            //Estrelas
    //            estrela1 = GameObject.Find("Estrela1_win").GetComponent<Animator>();
    //            estrela2 = GameObject.Find("Estrela2_win").GetComponent<Animator>();
    //            estrela3 = GameObject.Find("Estrela3_win").GetComponent<Animator>();
    //            //Btn Lose
    //            loseBtnMenu = GameObject.Find("Button_Menul").GetComponent<Button>();
    //            loseBtnNovamente = GameObject.Find("Button_Novamentel").GetComponent<Button>();
    //            //Btn Pause
    //            pauseBtn = GameObject.Find("Pause").GetComponent<Button>();
    //            pauseBtnPlay = GameObject.Find("play").GetComponent<Button>();
    //            pauseBtnNovamente = GameObject.Find("again").GetComponent<Button>();
    //            pauseBtnMenu = GameObject.Find("scene").GetComponent<Button>();
    //            pauseBtnLoja = GameObject.Find("shop").GetComponent<Button>();
    //            //audio
    //            winSom = painelWin.GetComponent<AudioSource>();
    //            loseSom = painelGameOver.GetComponent<AudioSource>();
    //            //Pontos
    //            pontosTxt = GameObject.FindWithTag("pointVal").GetComponent<Text>();
    //            bestPontoTxt = GameObject.FindWithTag("ptBest").GetComponent<Text>();
    //            //Text Score
    //            moedasTxt = GameObject.FindWithTag("moedatxt").GetComponent<Text>();
    //            //Imagem fundo preto
    //            fundoPreto = GameObject.FindWithTag("fundoPreto").GetComponent<Image>();

    //            //Eventos

    //            //pause
    //            pauseBtn.onClick.AddListener(Pausar);
    //            pauseBtnPlay.onClick.AddListener(PausarInvers);
    //            pauseBtnNovamente.onClick.AddListener(Again);
    //            pauseBtnMenu.onClick.AddListener(GoMenu);
    //            pauseBtnLoja.onClick.AddListener(GoLoja);
    //            //lose
    //            loseBtnMenu.onClick.AddListener(GoMenu);
    //            loseBtnNovamente.onClick.AddListener(Again);

    //            //win

    //            winBtnMenu.onClick.AddListener(GoMenu);
    //            winBtnNovamente.onClick.AddListener(Again);
    //            winBtnProximo.onClick.AddListener(ProximaFase);

    //        }
    //    }

    //    //Metodo Pause

    //    void Pausar()
    //    {
    //        GAMEMANAGER.instance.pausado = true;
    //        Time.timeScale = 0;
    //        fundoPreto.enabled = true;
    //        painelPause.Play("MenuPauseAnim");
    //    }

    //    void PausarInvers()
    //    {
    //        GAMEMANAGER.instance.pausado = false;
    //        Time.timeScale = 1;
    //        fundoPreto.enabled = false;
    //        painelPause.Play("MenuPauseAnimInvers");
    //    }

    //    //Metodo Pause Joga Novamente

    //    void Again()
    //    {
    //        SceneManager.LoadScene(ONDEESTOU.instance.fase);
    //        Time.timeScale = 1;
    //        GAMEMANAGER.instance.pausado = false;
    //    }

    //    //Metodo Pause Menu

    //    void GoMenu()
    //    {

    //        if (ONDEESTOU.instance.faseMestra == "Mestra1")
    //        {
    //            SceneManager.LoadScene("Mestra1");
    //        }
    //        else if (ONDEESTOU.instance.faseMestra == "Mestra2")
    //        {
    //            SceneManager.LoadScene("Mestra2");
    //        }


    //        Time.timeScale = 1;
    //        GAMEMANAGER.instance.pausado = false;

    //        AUDIOMANAGER.instance.GetSom(1);
    //    }

    //    //Metodo Pause Loja

    //    void GoLoja()
    //    {

    //        AUDIOMANAGER.instance.GetSom(1);
    //        SceneManager.LoadScene("Loja");
    //        Time.timeScale = 1;
    //        GAMEMANAGER.instance.pausado = false;
    //    }

    //    //Metodo Avancar

    //    void ProximaFase()
    //    {
    //        if (ONDEESTOU.instance.faseN == "Level2_Mestra1" || ONDEESTOU.instance.faseN == "Level4_Mestra2")
    //        {
    //            SceneManager.LoadScene("MenuFasesPai");
    //            AUDIOMANAGER.instance.GetSom(1);
    //        }
    //        else
    //        {
    //            SceneManager.LoadScene(ONDEESTOU.instance.fase + 1);
    //        }

    //    }


    //    // Use this for initialization
    //    void Start()
    //    {


    //    }

}
