using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Task2Manager : MonoBehaviour
{
    [SerializeField]
    USBScreenManager uSBScreenManager;

    //タスク2のアセット
    public GameObject uSBTask2Panel;
    public GameObject task2NextButton;
    public Text task2BottomText;
    public Slider task2ProcessBar;
    public Text task2NextButtonText;
    public int task2Flag { get; set; }
    public bool task2Finished { get; set; }

    private string strPerfect = "完了";
    private string[] bottomTexts = new string[] {"次へ　をクリックしてください。",
    "インストールが完了しました",
    "キーボードのQを長押ししてください",
    "キーボードの A,C,V,Y,J,Space を長押ししてください",
    "右クリック連打!",
    "インストールが完了しました" };

    public void Awake()
    {
        task2Flag = 1;
        task2Finished = false;
    }

    public void Task2Process()
    {
        //Task2のProcessBar
        if (task2Flag == 2)
        {
            //ボタンを長押しでゲージを増加
            if (Input.GetKey(KeyCode.Q))
            {
                task2ProcessBar.value += Time.deltaTime;
                if (task2ProcessBar.value == 5f)
                {
                    task2NextButton.SetActive(true);
                }
            }
            //ボタンを離した際にバーを初期化
            if (Input.GetKeyUp(KeyCode.Q))
            {
                if (task2ProcessBar.value != 5f)
                {
                    task2ProcessBar.value = 0;
                }
            }
        }
        if (task2Flag == 3)
        {
            //ボタンを長押しでゲージを増加
            if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.V) && Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.J))
            {
                task2ProcessBar.value += Time.deltaTime;
                if (task2ProcessBar.value == 5f)
                {
                    task2NextButton.SetActive(true);
                }
            }
            //ボタンを離した際にバーを初期化
            if (Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.C) && Input.GetKeyUp(KeyCode.V) && Input.GetKeyUp(KeyCode.Y) && Input.GetKeyUp(KeyCode.J))
            {
                if (task2ProcessBar.value != 5f)
                {
                    task2ProcessBar.value = 0;
                }
            }
        }
        if (task2Flag == 4)
        {
            //右クリック連打でゲージを増加
            if (Input.GetMouseButtonUp(1))
            {
                task2ProcessBar.value += 0.05f;
            }

            if (task2ProcessBar.value == 5f)
            {
                task2NextButton.SetActive(true);
            }
        }
        
    }

    public void OnButton2Clicked()
    {
        if (uSBScreenManager.canButtonPush)
        {
            uSBScreenManager.uSBMenuPanel.gameObject.SetActive(false);
            uSBTask2Panel.gameObject.SetActive(true);

            //初めてのボタンクリックでメッセージが表示
            if (uSBScreenManager.isFirstPushUSBButton2)
            {
                //未クリア
                StartCoroutine(uSBScreenManager.ShowMessages(uSBScreenManager.buttonClickText2));
                uSBScreenManager.isFirstPushUSBButton2 = false;
            }

            if (task2Flag <= 4)
            {
                task2Flag = 1;
                if (!task2NextButton.activeSelf) { task2NextButton.SetActive(true); }
                if (task2ProcessBar.gameObject.activeSelf) { task2ProcessBar.gameObject.SetActive(false); }
                task2ProcessBar.value = 0;
                task2BottomText.text = bottomTexts[0];
            }
            else
            {
                //クリア時
                task2BottomText.text = bottomTexts[1];
            }
        }
    }

    public void OnTask2NextButtonClicked()
    {
        if (uSBScreenManager.canButtonPush)
        {
            task2Flag++;

            if (task2Flag == 2)
            {
                task2NextButton.SetActive(false);
                task2ProcessBar.value = 0;
                task2BottomText.text = bottomTexts[2];
                task2ProcessBar.gameObject.SetActive(true);
            }

            if (task2Flag == 3)
            {
                task2NextButton.SetActive(false);
                task2ProcessBar.value = 0;
                task2BottomText.text = bottomTexts[3];
            }

            if (task2Flag == 4)
            {
                task2NextButton.SetActive(false);
                task2ProcessBar.value = 0;
                task2BottomText.text = bottomTexts[4];
            }

            if (task2Flag == 5)
            {
                task2NextButtonText.text = strPerfect;
                task2BottomText.text = bottomTexts[5];

                if (!task2Finished)
                {
                    task2Finished = true;
                    uSBScreenManager.taskProcess++;
                }
            }

            if (task2Flag >= 6)
            {
                uSBScreenManager.uSBMenuPanel.gameObject.SetActive(true);
                uSBTask2Panel.gameObject.SetActive(false);
            }
        }
    }
}

