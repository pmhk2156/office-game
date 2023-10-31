using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Task3Manager : MonoBehaviour
{
    [SerializeField]
    USBScreenManager uSBScreenManager;

    public GameObject uSBTask3Panel;
    public GameObject uSBTask3_1Panel;
    public GameObject uSBTask3_2Panel;
    public GameObject uSBTask3_345Panel;

    [SerializeField]
    Toggle task3Toggle;

    [SerializeField]
    InputField task3_34InputField;
    public InputField task3_5InputField;
    public string task3_5FixedCharacter { get; set; }

    [SerializeField]
    Text task3TextMessage;
    [SerializeField]
    Text task3ErrorMessage;
    [SerializeField]
    Text task3ButtonText;

    private int task3Flag;
    private bool task3Finished;

    private string[] strTask3MessegeTexts = new string[] {"1,5,10,50,100,500,1000, ? ,\n5000,10000",
    "パスワードを入力してください",
    "秘密の質問：\nあなたの出身中学校の名前は？\n(小文字ローマ字入力)",
    "ログインしました" };
    private string[] strErrorMessegeTexts = new string[] {"不正解です",
    "パスワードが正しくありません\n大文字入力されていますか？",
    "秘密の質問が正しくありません" };
    private string[] strAnswers = new string[] {"2000",
    "PASSWORD1",
    "上下市立東西南北",　"東西南北",　"じょうげしりつとうざいなんぼく",　"うえしたしりつとうざいなんぼく",　"とうざいなんぼく",
    "touzainannboku",　"touzainanboku",　"tozainannboku",　"tozainanboku",　"touzainannbokutyuu",
    "touzainanbokutyuu",　"tozainannbokutyuu",　"tozainanbokutyuu",　"touzainannbokutyuugakkou",　"touzainanbokutyuugakkou",
    "tozainannbokutyuugakkou",　"tozainanbokutyuugakkou",　"zyougesiritutouzainannboku",　"zyougesiritutouzainanboku",　"zyougesiritutozainannboku",
    "zyougesiritutozainanboku",　"zyougesiritutouzainannbokutyuu",　"zyougesiritutouzainanbokutyuu",　"zyougesiritutozainannbokutyuu",　"zyougesiritutozainanbokutyuu",
    "zyougesiritutouzainannbokutyuugakkou",　"zyougesiritutouzainanbokutyuugakkou",　"zyougesiritutozainannbokutyuugakkou",　"zyougesiritutozainanbokutyuugakkou",　"uesitasiritutouzainannboku",
    "uesitasiritutouzainanbokutyuu",　"uesitasiritutozainannbokutyuu",　"uesitasiritutouzainannbokutyuugakkou",　"uesitasiritutozainannbokutyuugakkou",　"uesitasiritutouzainanbokutyuugakkou",
    "uesitasiritutozainanbokutyuugakkou" };

    private string strPerfect = "完了";

    public void Awake()
    {
        task3Flag = 1;
        task3_5FixedCharacter = string.Empty;
        task3Finished = false;
    }

    public void OnButton3Clicked()
    {

        if (uSBScreenManager.canButtonPush && uSBScreenManager.taskProcess >= 2)
        {
            //初めてのボタンクリックでメッセージが表示
            if (uSBScreenManager.isFirstPushUSBButton3)
            {
                StartCoroutine(uSBScreenManager.ShowMessages(uSBScreenManager.buttonClickText3));
                uSBScreenManager.isFirstPushUSBButton3 = false;
            }

            uSBScreenManager.uSBMenuPanel.gameObject.SetActive(false);
            uSBTask3Panel.gameObject.SetActive(true);

            if (task3Flag < 5)
            {
                //未クリアなら初期化
                task3Flag = 1;
                task3Toggle.isOn = false;
                task3TextMessage.text = strTask3MessegeTexts[0];
                task3ErrorMessage.text = string.Empty;
                task3_5FixedCharacter = string.Empty;
                task3_34InputField.text = string.Empty;
                task3_5InputField.text = string.Empty;
                uSBTask3_1Panel.gameObject.SetActive(true);
                task3_34InputField.gameObject.SetActive(true);
                task3_5InputField.gameObject.SetActive(false);
            }
            else
            {
                //クリア
                uSBScreenManager.uSBMenuPanel.gameObject.SetActive(false);
                uSBTask3Panel.gameObject.SetActive(true);
                uSBTask3_345Panel.gameObject.SetActive(true);
            }
        }
    }

    //task3_1の処理
    public void OnTask3ToggleChanged()
    {
        if (uSBScreenManager.canButtonPush)
        {
            if (task3Toggle.isOn == true)
            {
                uSBTask3_1Panel.gameObject.SetActive(false);
                uSBTask3_2Panel.gameObject.SetActive(true);
                task3Flag++;
            }
        }
    }

    //task3_2以降の処理
    public void OnTask3BottonClicked()
    {
        if (uSBScreenManager.canButtonPush)
        {
            if (task3Flag == 2)
            {
                uSBTask3_2Panel.gameObject.SetActive(false);
                uSBTask3_345Panel.gameObject.SetActive(true);
                task3Flag++;
            }
            else if (task3Flag == 3)
            {
                if (task3_34InputField.text == strAnswers[0])
                {
                    task3TextMessage.text = strTask3MessegeTexts[1];
                    task3ErrorMessage.text = string.Empty;
                    task3_34InputField.text = string.Empty;
                    task3Flag++;
                }
                else
                {
                    task3ErrorMessage.text = strErrorMessegeTexts[0];
                }
            }
            else if (task3Flag == 4)
            {
                if (task3_34InputField.text == strAnswers[1])
                {
                    task3_34InputField.text = string.Empty;
                    task3_34InputField.gameObject.SetActive(false);
                    task3_5InputField.gameObject.SetActive(true);
                    task3TextMessage.text = strTask3MessegeTexts[2];
                    task3ErrorMessage.text = string.Empty;
                    task3Flag++;
                }
                else
                {
                    task3ErrorMessage.text = strErrorMessegeTexts[1];
                }
            }
            else if (task3Flag == 5)
            {
                for (int i = 2; i < strAnswers.Length; i++)
                {
                    if (task3_5FixedCharacter == strAnswers[i])
                    {
                        task3TextMessage.text = strTask3MessegeTexts[3];
                        task3ErrorMessage.text = string.Empty;
                        task3_5InputField.gameObject.SetActive(false);
                        task3ButtonText.text = strPerfect;
                        task3Flag++;
                        uSBScreenManager.taskProcess++;
                        if (!task3Finished)
                        {
                            task3Finished = true;
                        }
                    }
                    else
                    {
                        task3ErrorMessage.text = strErrorMessegeTexts[2];
                    }
                }
            }
            else
            {
                uSBScreenManager.uSBMenuPanel.gameObject.SetActive(true);
                uSBTask3Panel.gameObject.SetActive(false);
                uSBTask3_345Panel.gameObject.SetActive(false);
            }
        }
    }

    public void FixedSmallCharacter()
    {
        task3_5FixedCharacter = task3_5InputField.text.ToLower();
    }
}
