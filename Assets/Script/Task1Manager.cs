using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Task1Manager : MonoBehaviour
{
    [SerializeField]
    BossManager bossManager;
    [SerializeField]
    USBScreenManager uSBScreenManager;

    [SerializeField]
    GameObject task11;
    [SerializeField]
    GameObject task12;
    public GameObject uSBTask1Panel;
    [SerializeField]
    Text task1BossText;
    [SerializeField]
    Text task1BossTextDetail;
    [SerializeField]
    Slider task1ConfidenceSlider;

    private string[] assessmentTexts = new string[] { "評価 - ", "評価 ✕ ", "評価 △ ", "評価 〇 ", "評価 ◎ " };
    private string[] assessmentDetailTexts = new string[] {
    "",
    "全く仕事をこなしていない",
    "仕事に取り掛かり始めた段階",
    "順調に仕事をこなしている\nパスワードは「PASSWORD1」",
    "本日の業務を完了\n優秀な人材\nパスワードは「PASSWORD1」" };

    public void OnButton1Clicked()
    {
        if (task12.activeSelf)
        {
            task12.gameObject.SetActive(false);
            task11.gameObject.SetActive(true);
        }
        else if (task11.activeSelf)
        {
            task11.gameObject.SetActive(false);
            task12.gameObject.SetActive(true);
        }

        if (uSBScreenManager.canButtonPush)
        {
            //初めてのボタンクリックでメッセージが表示
            if (uSBScreenManager.isFirstPushUSBButton1)
            {
                StartCoroutine(uSBScreenManager.ShowMessages(uSBScreenManager.buttonClickText1));
                uSBScreenManager.isFirstPushUSBButton1 = false;
            }

            uSBScreenManager.uSBMenuPanel.gameObject.SetActive(false);
            uSBTask1Panel.gameObject.SetActive(true);

            if (bossManager.isSecondTalk)
            {
                task1BossText.text = assessmentTexts[0];
                task1BossTextDetail.text = assessmentDetailTexts[0];
            }
            else if (bossManager.confidenceValue < 4)
            {
                task1BossText.text = assessmentTexts[1];
                task1BossTextDetail.text = assessmentDetailTexts[1];
            }
            else if (bossManager.confidenceValue < 7)
            {
                task1BossText.text = assessmentTexts[2];
                task1BossTextDetail.text = assessmentDetailTexts[2];
            }
            else if (bossManager.confidenceValue < 15)
            {
                task1BossText.text = assessmentTexts[3];
                task1BossTextDetail.text = assessmentDetailTexts[3];
            }
            else if (bossManager.confidenceValue == 15)
            {
                task1BossText.text = assessmentTexts[4];
                task1BossTextDetail.text = assessmentDetailTexts[4];
            }

            //信頼値ゲージ
            task1ConfidenceSlider.value = bossManager.confidenceValue;
        }
    }
}
