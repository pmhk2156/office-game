using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class USBScreenManager : MonoBehaviour
{   
    private IEnumerator uSBCoroutine;

    //UI・パネル
	[SerializeField]
	GameObject uSBTextPanel;
	[SerializeField]
	GameObject uSBMenuPanel;
	[SerializeField]
    Text uSBPanelText;

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

	private string loadUSBText1;
	private string loadUSBText2;
	private string loadUSBText3;
    private string loadButtonClickText1;
    private string loadButtonClickText2;
    private string loadButtonClickText3;
    private string loadButtonClickText4;
    private string[] uSBText1;
    private string[] uSBText2;
    private string[] uSBText3;
	private string[] buttonClickText1;
    private string[] buttonClickText2;
    private string[] buttonClickText3;
    private string[] buttonClickText4;

	//BGMアセット
	[SerializeField]
    AudioSource audio_ComputerTextSound;
    [SerializeField]
    AudioClip computerTextSound;

	//タスク1のアセット
	[SerializeField]
	GameObject uSBTask1Panel;
	[SerializeField]
    Text timeText;
	[SerializeField]
    Text confidenceValueText;

	//タスク2のアセット
	[SerializeField]
	GameObject uSBTask2Panel;
	[SerializeField]
	GameObject task2NextButton;
	[SerializeField]
	Text task2BottomText;
	[SerializeField]
	Slider task2ProcessBar;
	[SerializeField]
	Text task2NextButtonText;
	private int task2Flag;
	private bool task2Finished;

	//タスク3のアセット
	[SerializeField]
	GameObject uSBTask3Panel;
	[SerializeField]
	GameObject uSBTask3_1Panel;
	[SerializeField]
	GameObject uSBTask3_2Panel;
	[SerializeField]
	GameObject uSBTask3_345Panel;

    [SerializeField]
    Toggle task3Toggle;
	
	[SerializeField]
	InputField task3_345InputField;
	
	[SerializeField]
	Text task3TextMessage;
	[SerializeField]
	Text task3ErrorMessage;
	[SerializeField]
	Text task3ButtonText;

	private int task3Flag;
	private bool task3Finished;

	//タスク4のアセット
	[SerializeField]
	GameObject uSBTask4Panel;

	private float computerMessageSpeed = 0.1f;
	private string readingText;

    private bool canButtonPush;
	public static  float nextTimeBossComing;
	private float nextHourBossComing;
	private float nextMinuteBossComing;
	private string hourStr;
	private string minuteStr;
	private string confidenceValueStr;
	private bool canFirstReadUSBText1;
	private bool canFirstReadUSBText2;
	private bool canFirstReadUSBText3;
	private bool isFirstPushUSBButton1;
	private bool isFirstPushUSBButton2;
	private bool isFirstPushUSBButton3;
	private bool isFirstPushUSBButton4;

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
        uSBText1 = loadUSBText1.Split(',');
        uSBText2 = loadUSBText2.Split(',');
        uSBText3 = loadUSBText3.Split(',');
        buttonClickText1 = loadButtonClickText1.Split(',');
        buttonClickText2 = loadButtonClickText2.Split(',');
        buttonClickText3 = loadButtonClickText3.Split(',');
        buttonClickText4 = loadButtonClickText4.Split(',');

		//値の初期化
		task2Flag = 1;
		task3Flag = 1;
		task2Finished = false;
		task3Finished = false;
        canFirstReadUSBText1 = true;
	    canFirstReadUSBText2 = false;
        canFirstReadUSBText3 = false;
        isFirstPushUSBButton1 = true;
        isFirstPushUSBButton2 = true;
        isFirstPushUSBButton3 = true;
        isFirstPushUSBButton4 = true;
    }

    void OnEnable()
    {   
        if(canFirstReadUSBText1)
		{   
            uSBCoroutine = USBAction(uSBText1 ,canFirstReadUSBText1);
			StartCoroutine(uSBCoroutine);
            canFirstReadUSBText1 = false;
            canFirstReadUSBText2 = true;
		} 
		else if(canFirstReadUSBText2)
		{
			uSBCoroutine = USBAction(uSBText2 ,canFirstReadUSBText2);
			StartCoroutine(uSBCoroutine);
            canFirstReadUSBText2 = false;
		} 
		else if(canFirstReadUSBText3)
		{
			uSBCoroutine = USBAction(uSBText3 ,canFirstReadUSBText3);
			StartCoroutine(uSBCoroutine);
            canFirstReadUSBText3 = false;	
		}
    }

    void OnDisable()
    {   
        //次回起動した際にMenu画面を表示させる
        if(!canFirstReadUSBText1)
        {
		    uSBMenuPanel.gameObject.SetActive(true);
        }
		uSBTask1Panel.gameObject.SetActive(false);
		uSBTask2Panel.gameObject.SetActive(false);
		uSBTask3Panel.gameObject.SetActive(false);
		uSBTask3_1Panel.gameObject.SetActive(false);
	    uSBTask3_2Panel.gameObject.SetActive(false);
		uSBTask3_345Panel.gameObject.SetActive(false);
        task3Toggle.isOn = false; 
		task3TextMessage.text = "1,5,10,50,100,500,1000, ? ,\n5000,10000";
		task3ErrorMessage.text = "";
		uSBTextPanel.gameObject.SetActive(false);

		//コルーチンを停止して初期化
		if(uSBCoroutine != null)
    	{
        	StopCoroutine(uSBCoroutine);
    		uSBCoroutine = null;
		}	
    }

    void Update()
    {
        //Task2のProcessBar
        if(uSBTask2Panel.activeSelf)
        {
            if(task2Flag == 2)
            {
                //ボタンを長押しでゲージを増加
                if(Input.GetKey(KeyCode.Q))
                {
                    task2ProcessBar.value += Time.deltaTime;
                    if(task2ProcessBar.value == 5f)
                    {
                        task2NextButton.SetActive(true);
                    }
                }
                //ボタンを離した際にバーを初期化
                if(Input.GetKeyUp(KeyCode.Q))
                {
                    if(task2ProcessBar.value != 5f)
                    {
                        task2ProcessBar.value = 0;
                    }
                }
            }
            if(task2Flag == 3)
            {
                //ボタンを長押しでゲージを増加
                if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.V) && Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.Space))
                {
                    task2ProcessBar.value += Time.deltaTime;
                    if(task2ProcessBar.value == 5f)
                    {
                        task2NextButton.SetActive(true);
                    }
                }
                //ボタンを離した際にバーを初期化
                if(Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.C) && Input.GetKeyUp(KeyCode.V) && Input.GetKeyUp(KeyCode.Y) && Input.GetKeyUp(KeyCode.J) && Input.GetKeyUp(KeyCode.Space))
                {
                    if(task2ProcessBar.value != 5f)
                    {
                        task2ProcessBar.value = 0;
                    }
                }
            }
            if(task2Flag == 4)
            {
                //右クリック連打でゲージを増加
                if(Input.GetMouseButtonUp(1))
                {
                    task2ProcessBar.value += 0.05f;
                }
                    
                if(task2ProcessBar.value == 5f)
                {
                    task2NextButton.SetActive(true);
                }
            }				
        }
    }
 
    private bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

	protected IEnumerator USBAction(string[] uSBText ,bool uSBFlag)
	{	
		//ShowMessagesコルーチンを開始
		if(uSBFlag)
		{
			yield return ShowMessages(uSBText);
		}

		//一度表示したメッセージが再度表示されないようにする
		uSBFlag = false;
        yield break;
	}	

	protected IEnumerator ShowMessages(string[] message)
	{
		//テキストパネルを表示
		uSBTextPanel.gameObject.SetActive(true);
        
        //ボタンをクリックできなくする
        canButtonPush = false;

		//時間を止める
		BossManager.canTimePass = false;

		//メッセージ送り
		for (int i = 0; i < message.Length; ++i)
        {            
			float time = 0;
        	int readingTextWordNumber = 1;

        	while ( true )
        	{
            	yield return null;
            
            	time += Time.deltaTime;
            	// クリックされると一気に表示
            	if ( IsClicked() ) break;

            	//0.1秒ごとにメッセージが一文字ずつ表示
            	int len = Mathf.FloorToInt ( time / computerMessageSpeed);
            	if (len <= message[i].Length) 
				{    
					//メッセージの表示
                	this.uSBPanelText.text = message[i].Substring(0, len);
					
					//メッセージ送りの音声
                	if(readingTextWordNumber == len){
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
        	yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
		}
        
		uSBTextPanel.gameObject.SetActive(false);
			
		//時間を再開する
		BossManager.canTimePass = true;
        //ボタンをクリックできるようにする
        canButtonPush = true;
	}

	public void OnButton1Clicked()
	{   
        if(canButtonPush)
        {   
            //初めてのボタンクリックでメッセージが表示
            if(isFirstPushUSBButton1)
            {
                StartCoroutine(ShowMessages(buttonClickText1));
                isFirstPushUSBButton1 = false;
            }

            uSBMenuPanel.gameObject.SetActive(false);
            uSBTask1Panel.gameObject.SetActive(true);

            //時間表示の変更
            nextHourBossComing = (float) Math.Truncate(8f + nextTimeBossComing / 60.0f);
            nextMinuteBossComing = (float) Math.Truncate(nextTimeBossComing - 60f * (float) Math.Truncate(nextTimeBossComing / 60.0f));
            hourStr = nextHourBossComing.ToString();
            if(nextMinuteBossComing > 15 && nextMinuteBossComing <= 45){
                minuteStr = "30";
            } else {
                minuteStr = "00";
            }	
            
            timeText.text = hourStr + ":" + minuteStr;

            //信頼値の変更
            confidenceValueStr = ComputerBehavior.confidenceValue.ToString();
            confidenceValueText.text = confidenceValueStr + " / 100";
        }
	}

	public void OnButton2Clicked()
	{
        if(canButtonPush)
        {
            uSBMenuPanel.gameObject.SetActive(false);
            uSBTask2Panel.gameObject.SetActive(true);

            //初めてのボタンクリックでメッセージが表示
            if(isFirstPushUSBButton2)
            {
                StartCoroutine(ShowMessages(buttonClickText2));
                isFirstPushUSBButton2 = false;
            }

            if(task2Flag <= 4)
            {
                task2Flag = 1;
                if(!task2NextButton.activeSelf){
                    task2NextButton.SetActive(true);
                }

                if(task2ProcessBar.gameObject.activeSelf){
                    task2ProcessBar.gameObject.SetActive(false);
                }

                task2ProcessBar.value = 0;
                task2BottomText.text = "次へ　をクリックしてください。";
            }
            else
            {
                task2BottomText.text = "インストールが完了しました";
            }
        }
	}
	
	public void OnTask2NextButtonClicked()
	{
        if(canButtonPush)
        {
            task2Flag ++;

            if(task2Flag == 2){
                task2NextButton.SetActive(false);
                task2ProcessBar.value = 0;
                task2BottomText.text = "キーボードのQを長押ししてください";
                task2ProcessBar.gameObject.SetActive(true);
            }

            if(task2Flag == 3){
                task2NextButton.SetActive(false);
                task2ProcessBar.value = 0;
                task2BottomText.text = "キーボードの A,C,V,Y,J,Space を長押ししてください";
            }

            if(task2Flag == 4){
                task2NextButton.SetActive(false);
                task2ProcessBar.value = 0;
                task2BottomText.text = "右クリック連打!";
            }

            if(task2Flag == 5){
                task2NextButtonText.text = "完了";
                task2BottomText.text = "インストールが完了しました";
                if(!task2Finished){
                    task2Finished = true;
                }
            }

            if(task2Flag >= 6){
                uSBMenuPanel.gameObject.SetActive(true);
                uSBTask2Panel.gameObject.SetActive(false);
            }
        }
	}

	public void OnButton3Clicked()
	{
        if(canButtonPush)
        {   
            //初めてのボタンクリックでメッセージが表示
            if(isFirstPushUSBButton3)
            {
                StartCoroutine(ShowMessages(buttonClickText3));
                isFirstPushUSBButton3 = false;
            }

            if(task3Flag < 5){
                task3Flag = 1;
                uSBMenuPanel.gameObject.SetActive(false);
                uSBTask3Panel.gameObject.SetActive(true);	
                uSBTask3_1Panel.gameObject.SetActive(true);	
            }
            else 
            {
                uSBMenuPanel.gameObject.SetActive(false);
                uSBTask3Panel.gameObject.SetActive(true);
                uSBTask3_345Panel.gameObject.SetActive(true);
            }
        }
	}

	public void OnTask3ToggleChanged()
	{
        if(canButtonPush)
        {   
            if(task3Toggle.isOn == true)
            {
                uSBTask3_1Panel.gameObject.SetActive(false);
                uSBTask3_2Panel.gameObject.SetActive(true);	
                task3Flag++;
            }
        }
	}

	public void OnTask3BottonClicked()
	{
        if(canButtonPush)
        {
            if(task3Flag == 2)
            {
                uSBTask3_2Panel.gameObject.SetActive(false);	
                uSBTask3_345Panel.gameObject.SetActive(true);	
                task3Flag++;
            }
            else if (task3Flag == 3)
            {
                if(task3_345InputField.text == "2000")
                {	
                    task3TextMessage.text = "パスワードを入力してください";
                    task3ErrorMessage.text = "";
                    task3Flag++;
                }
                else
                {
                    task3ErrorMessage.text = "不正解です";
                }
            }
            else if (task3Flag == 4)
            {
                if(task3_345InputField.text == "PASSWORD1")
                {	
                    task3TextMessage.text = "秘密の質問：\nあなたの出身中学校の名前は？";
                    task3ErrorMessage.text = "";
                    task3Flag++;
                }
                else
                {
                    task3ErrorMessage.text = "パスワードが正しくありません";
                }
            }
            else if (task3Flag == 5)
            {
                if(task3_345InputField.text == "東西南北中学校")
                {	
                    task3TextMessage.text = "ログインしました";
                    task3ErrorMessage.text = "";
                    task3_345InputField.gameObject.SetActive(false);
                    task3ButtonText.text ="完了";
                    task3Flag++;
                    if(!task3Finished)
                    {
                        task3Finished = true;
                    }
                }
                else
                {
                    task3ErrorMessage.text = "秘密の質問が正しくありません";
                }
            }
            else
            {
                uSBMenuPanel.gameObject.SetActive(true);
                uSBTask3Panel.gameObject.SetActive(false);
                uSBTask3_345Panel.gameObject.SetActive(false);
            }
        }    
	}

	public void OnButton4Clicked()
	{
        if(canButtonPush)
        {   
            //初めてのボタンクリックでメッセージが表示
            if(isFirstPushUSBButton4)
            {
                StartCoroutine(ShowMessages(buttonClickText4));
                isFirstPushUSBButton4 = false;
            }

		    uSBMenuPanel.gameObject.SetActive(false);
		    uSBTask4Panel.gameObject.SetActive(true);
        }
	}

	//リターンボタンのクリック
	public void OnReturnButtonClicked()
	{
        if(canButtonPush)
        {
            uSBMenuPanel.gameObject.SetActive(true);
            uSBTask1Panel.gameObject.SetActive(false);
            uSBTask2Panel.gameObject.SetActive(false);
            uSBTask3Panel.gameObject.SetActive(false);
            uSBTask3_1Panel.gameObject.SetActive(false);
            uSBTask3_2Panel.gameObject.SetActive(false);
            uSBTask3_345Panel.gameObject.SetActive(false);
            task3Toggle.isOn = false; 
            task3TextMessage.text = "1,5,10,50,100,500,1000, ? ,\n5000,10000";
            task3ErrorMessage.text = "";
        }
	}    
}

