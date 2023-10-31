using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class USBScreenManager : MonoBehaviour
{
    [SerializeField]
    BossManager bossManager;
    [SerializeField]
    TimeManager timeManager;
    [SerializeField]
    Task1Manager task1Manager;
    [SerializeField]
    Task2Manager task2Manager;
    [SerializeField]
    Task3Manager task3Manager;
    [SerializeField]
    Task4Manager task4Manager;

    private IEnumerator uSBCoroutine;

    //UI・パネル
    public GameObject uSBMenuPanel;
    [SerializeField]
    GameObject uSBTextPanel;
    [SerializeField]
    Text uSBPanelText;
    [SerializeField]
    GameObject cannotInputObject;

    //シナリオテキスト関連
    [SerializeField]
    TextAsset uSBTextAsset1;
    [SerializeField]
    TextAsset uSBTextAsset2;
    [SerializeField]
    TextAsset uSBTextAsset3;
    [SerializeField]
    TextAsset buttonClickTextAsset1;
    [SerializeField]
    TextAsset buttonClickTextAsset2;
    [SerializeField]
    TextAsset buttonClickTextAsset3;
    [SerializeField]
    TextAsset buttonClickTextAsset4;
    [SerializeField]
    TextAsset task4ClearTextAsset;

    private string loadUSBText1;
    private string loadUSBText2;
    private string loadUSBText3;
    private string loadButtonClickText1;
    private string loadButtonClickText2;
    private string loadButtonClickText3;
    private string loadButtonClickText4;
    private string loadTask4ClearText;
    private string[] uSBText1;
    private string[] uSBText2;
    private string[] uSBText3;
    public string[] buttonClickText1 { get; set; }
    public string[] buttonClickText2 { get; set; }
    public string[] buttonClickText3 { get; set; }
    public string[] buttonClickText4 { get; set; }
    public string[] task4ClearText { get; set; }

    //BGMアセット
    [SerializeField]
    AudioSource audio_ComputerTextSound;
    [SerializeField]
    AudioClip computerTextSound;

    private float computerMessageSpeed = 0.1f;

    //各種フラグ
    public int taskProcess { get; set; }
    public bool canButtonPush { get; set; }
    private bool firstReadUSB;
    private bool firstReadUSBText1;
    private bool firstReadUSBText2;
    private bool firstReadUSBText3;
    public bool isFirstPushUSBButton1 { get; set; }
    public bool isFirstPushUSBButton2 { get; set; }
    public bool isFirstPushUSBButton3 { get; set; }
    public bool isFirstPushUSBButton4 { get; set; }

    void Awake()
    {
        //テキストアセットのロード
        loadUSBText1 = uSBTextAsset1.text;
        loadUSBText2 = uSBTextAsset2.text;
        loadUSBText3 = uSBTextAsset3.text;
        loadButtonClickText1 = buttonClickTextAsset1.text;
        loadButtonClickText2 = buttonClickTextAsset2.text;
        loadButtonClickText3 = buttonClickTextAsset3.text;
        loadButtonClickText4 = buttonClickTextAsset4.text;
        loadTask4ClearText = task4ClearTextAsset.text;

        //外部テキストを参照する際、一文ごとに'で区切っている
        uSBText1 = loadUSBText1.Split(',');
        uSBText2 = loadUSBText2.Split(',');
        uSBText3 = loadUSBText3.Split(',');
        buttonClickText1 = loadButtonClickText1.Split(',');
        buttonClickText2 = loadButtonClickText2.Split(',');
        buttonClickText3 = loadButtonClickText3.Split(',');
        buttonClickText4 = loadButtonClickText4.Split(',');
        task4ClearText = loadTask4ClearText.Split(',');

        //値の初期化
        taskProcess = 1;
        firstReadUSB = true;
        firstReadUSBText1 = true;
        firstReadUSBText2 = false;
        firstReadUSBText3 = false;
        isFirstPushUSBButton1 = true;
        isFirstPushUSBButton2 = true;
        isFirstPushUSBButton3 = true;
        isFirstPushUSBButton4 = true;
    }

    void OnEnable()
    {
        if (firstReadUSBText1)
        {
            uSBCoroutine = USBAction(uSBText1);
            StartCoroutine(uSBCoroutine);
            firstReadUSBText2 = true;
        }
        else if (firstReadUSBText2)
        {
            uSBCoroutine = USBAction(uSBText2);
            StartCoroutine(uSBCoroutine);
        }
        else if (firstReadUSBText3)
        {
            uSBCoroutine = USBAction(uSBText3);
            StartCoroutine(uSBCoroutine);
        }
    }

    void OnDisable()
    {
        BackToTitle();

        canButtonPush = true;
        timeManager.canTimePass = true;
        if (uSBTextPanel.activeSelf) { uSBTextPanel.gameObject.SetActive(false); }

        //コルーチンを停止して初期化
        if (uSBCoroutine != null)
        {
            StopCoroutine(uSBCoroutine);
            uSBCoroutine = null;
        }
    }

    void Update()
    {
        if (task2Manager.uSBTask2Panel.activeSelf) { task2Manager.Task2Process(); }
        if (task4Manager.isTask4Clear)
        {
            task4Manager.isTask4Clear = false;
            StartCoroutine(ShowMessages(task4ClearText));
            taskProcess++;
        }
    }

    private bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    protected IEnumerator USBAction(string[] uSBText)
    {
        //ボタンをクリックできなくする・時間を止める・入力できなくする
        canButtonPush = false;
        timeManager.canTimePass = false;
        cannotInputObject.gameObject.SetActive(true);

        //ShowMessagesコルーチンを開始
        yield return ShowMessages(uSBText);

        //一度表示したメッセージが再度表示されないようにする
        if (firstReadUSBText1) { firstReadUSBText1 = false; }
        else if (firstReadUSBText2) { firstReadUSBText2 = false; }
        else if (firstReadUSBText3) { firstReadUSBText3 = false; }

        //ボタンをクリックできるようにする・時間を再開する・入力可能にする
        canButtonPush = true;
        timeManager.canTimePass = true;
        cannotInputObject.gameObject.SetActive(false);

        yield break;
    }

    public IEnumerator ShowMessages(string[] message)
    {
        //テキストパネルを表示
        uSBTextPanel.gameObject.SetActive(true);

        //メッセージ送り
        for (int i = 0; i < message.Length; ++i)
        {
            float time = 0;
            int readingTextWordNumber = 1;

            while (true)
            {
                yield return null;

                time += Time.deltaTime;
                // クリックされると一気に表示
                if (IsClicked()) break;

                //0.1秒ごとにメッセージが一文字ずつ表示
                int len = Mathf.FloorToInt(time / computerMessageSpeed);
                if (len <= message[i].Length)
                {
                    //メッセージの表示
                    this.uSBPanelText.text = message[i].Substring(0, len);

                    //メッセージ送りの音声
                    if (readingTextWordNumber == len)
                    {
                        audio_ComputerTextSound.PlayOneShot(computerTextSound);
                        readingTextWordNumber++;
                    }
                }
                else
                {
                    break;
                }

            }

            //メッセージを全て表示
            this.uSBPanelText.text = message[i];
            yield return null;

            //次のクリックまで待機
            yield return new WaitUntil(() => IsClicked());
        }

        if (!firstReadUSB) { uSBTextPanel.gameObject.SetActive(false); }

        firstReadUSB = false;
    }

    protected IEnumerator GameClearAction()
    {
        //ボタンをクリックできなくする・時間を止める・入力できなくする
        canButtonPush = false;
        timeManager.canTimePass = false;
        cannotInputObject.gameObject.SetActive(true);

        //ShowMessagesコルーチンを開始
        yield return null;
    }

    //センドボタンのクリック
    public void OnSendButtonClicked()
    {
        if (canButtonPush && taskProcess >= 4)
        {
            StartCoroutine(GameClearAction());
            SceneManager.LoadScene("ClearScene");
        }
    }

    //リターンボタンのクリック
    public void OnReturnButtonClicked()
    {
        if (canButtonPush) { BackToTitle(); }
    }

    public void BackToTitle()
    {
        uSBMenuPanel.gameObject.SetActive(true);
        task1Manager.uSBTask1Panel.gameObject.SetActive(false);
        task2Manager.uSBTask2Panel.gameObject.SetActive(false);
        task3Manager.uSBTask3Panel.gameObject.SetActive(false);
        task3Manager.uSBTask3_1Panel.gameObject.SetActive(false);
        task3Manager.uSBTask3_2Panel.gameObject.SetActive(false);
        task3Manager.uSBTask3_345Panel.gameObject.SetActive(false);
        task4Manager.uSBTask4Panel.gameObject.SetActive(false);
    }
}