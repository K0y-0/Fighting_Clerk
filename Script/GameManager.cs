using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public  class GameManager : MonoBehaviour
{
    // シングルトン？あっているのか？
    public static GameManager ManagerInstance;

    [SerializeField]private GameObject startObject;
    
    // 各種テキスト
    [SerializeField]private TextMeshProUGUI scoreText;
    [SerializeField]private TextMeshProUGUI timeText;
    [SerializeField]private TextMeshProUGUI wrongMessageText;
    [SerializeField]private TextMeshProUGUI resultScoreText;

    //各種オブジェクト(UI)
    [SerializeField]private GameObject titleUiObjects;
    [SerializeField]private GameObject prevStartUiObjects;
    [SerializeField]private GameObject inGameObjects;
    [SerializeField]private GameObject resultUI;
    
    // ゲーム進行管理フラグ
    private bool isTitle;
    private bool isGameStart;

    [SerializeField]private float timeLimit;
    [SerializeField]private float stopText;
    private AudioSource audioSource;
    private int score;
    private float count;


    void Awake()
    {
        Debug.Log("Awake");

        // シングルトン
        if(ManagerInstance == null)
        {
            ManagerInstance = this;
            //DontDestroyOnLoad(this.gameObject);
            Debug.Log("ManagerInstance Complete");

            if(ManagerInstance == null)
            {
                Debug.Log("GameManager ManagerInstance Error!");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        StartCoroutine("Title");
        // 初期化処理
        audioSource = this.GetComponent<AudioSource>();
        wrongMessageText.text = null;
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(JoyconInput.instance.joyconL.GetButtonDown(Joycon.Button.PLUS)) { Application.Quit(); }
        
        if(isTitle) return;

        if(timeLimit < 0)
        {
            Debug.Log("FINISH!");
            resultUI.SetActive(true);
            resultScoreText.text = $"記録：{score} ジンギスカン";
            isGameStart = false;

            if(JoyconInput.instance.joyconR.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
                SceneManager.LoadScene(0);
            }
        }

        if(!isGameStart) return;

        timeLimit -= Time.deltaTime; // 制限時間を減らす
        SetTimeText();
        SetScoreText();
        

        if(wrongMessageText.text != null)
        {
            wrongMessageText.gameObject.SetActive(true);
            count += Time.deltaTime;
            if(count > stopText) 
            { 
                wrongMessageText.gameObject.SetActive(false);
                count = 0;
                wrongMessageText.text = null;
            }
        }
    }

    // 初期化関数
    public void Init()
    {
        isTitle = true;

        titleUiObjects.SetActive(true);
        prevStartUiObjects.SetActive(false);
        inGameObjects.SetActive(false);

        SetTimeText();
    }

    // 時間をテキストに設定
    void SetTimeText()
    {
        timeText.text = $"残り {timeLimit.ToString("F0")} 秒";
    }

    // スコアをテキストに設定
    void SetScoreText()
    {
        scoreText.text = $"{score} ジンギスカン";
    }

    public void SetWrongText(string name)
    {
        wrongMessageText.text = $"それは{name}だべさ！";
    }

    // 押されるまでゲームスタートしない
    IEnumerator Title()
    {
        // タイトルシーンでAボタンが押されるまで
        yield return new WaitWhile(() => !JoyconInput.instance.m_joyconR.GetButtonDown(Joycon.Button.DPAD_RIGHT));

        StartCoroutine("PrevStart");
        isTitle = false;
        startObject.SetActive(false);
        titleUiObjects.SetActive(false);
        prevStartUiObjects.SetActive(true);
        Debug.Log("ゲーム開始前のシーンに移行しました。");

        yield return new WaitForSeconds(1f);
        StopCoroutine("Title");
    }

    IEnumerator PrevStart()
    {
        Debug.Log("PrevStart");
        // ゲーム開始前にAボタンが押されるまで
        yield return new WaitWhile(() => !JoyconInput.instance.m_joyconR.GetButtonDown(Joycon.Button.SHOULDER_2));

        prevStartUiObjects.SetActive(false);
        inGameObjects.SetActive(true);
        audioSource.Play();
        isGameStart = true;
        Debug.Log("ゲームスタートしました。");
        StopCoroutine("PrevStart");
    }

    // スタートフラグ参照
    public bool IsStart
    {
        get
        {
            return isGameStart;
        }
    }

    // スコアポイント参照用
    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }
}
