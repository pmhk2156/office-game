using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	private string[] buttonClickText1;
    private string[] buttonClickText2;
    private string[] buttonClickText3;
    private string[] buttonClickText4;
    private string[] task4ClearText;

	//BGMアセット
	[SerializeField]
    AudioSource audio_ComputerTextSound;
    [SerializeField]
    AudioClip computerTextSound;

	//タスク1のアセット
	[SerializeField]
	GameObject uSBTask1Panel;
    [SerializeField]
    Text task1BossText;
    [SerializeField]
    Text task1BossTextDetail;
    [SerializeField]
	Slider task1ConfidenceSlider;

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
	InputField task3_34InputField;
    [SerializeField]
	InputField task3_5InputField;
    private string task3_5FixedCharacter;
	
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

    private int taskProcess;
    public static bool canButtonPush;
	private float nextHourBossComing;
	private float nextMinuteBossComing;
    private bool firstReadUSB;
	private bool firstReadUSBText1;
	private bool firstReadUSBText2;
	private bool firstReadUSBText3;
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
        loadTask4ClearText = task4ClearTextAsset.text;
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
		task2Flag = 1;
		task3Flag = 1;
        task3_5FixedCharacter = "";
		task2Finished = false;
		task3Finished = false;
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
        if(firstReadUSBText1)
		{   
            uSBCoroutine = USBAction(uSBText1);
			StartCoroutine(uSBCoroutine);
            firstReadUSBText2 = true;
		} 
		else if(firstReadUSBText2)
		{
			uSBCoroutine = USBAction(uSBText2);
			StartCoroutine(uSBCoroutine);
		} 
		else if(firstReadUSBText3)
		{
			uSBCoroutine = USBAction(uSBText3);
			StartCoroutine(uSBCoroutine);
		}
    }

    void OnDisable()
    {   
        //次回起動した際にMenu画面を表示させる
        if(!firstReadUSBText1)
        {
		    uSBMenuPanel.gameObject.SetActive(true);
        }

        if(task3Flag < 6)
        {
            task3Flag = 1;
            task3Toggle.isOn = false;
            task3TextMessage.text = "1,5,10,50,100,500,1000, ? ,\n5000,10000";
            task3ErrorMessage.text = "";
            task3_34InputField.text = "";
            task3_5InputField.text = "";
            task3_5FixedCharacter = "";
            task3_34InputField.gameObject.SetActive(true);
            task3_5InputField.gameObject.SetActive(false);

        }

		uSBTask1Panel.gameObject.SetActive(false);
		uSBTask2Panel.gameObject.SetActive(false);
		uSBTask3Panel.gameObject.SetActive(false);
		uSBTask3_1Panel.gameObject.SetActive(false);
	    uSBTask3_2Panel.gameObject.SetActive(false);
		uSBTask3_345Panel.gameObject.SetActive(false);
        uSBTask4Panel.gameObject.SetActive(false);
        

        canButtonPush = true;
        BossManager.canTimePass = true;
        if(uSBTextPanel.activeSelf)
        {
		    uSBTextPanel.gameObject.SetActive(false);
        }

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

        if(Task4Manager.isTask4Clear)
        {   
            Task4Manager.isTask4Clear = false;
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
		BossManager.canTimePass = false;
        cannotInputObject.gameObject.SetActive(true);

		//ShowMessagesコルーチンを開始
		yield return ShowMessages(uSBText);
		
		//一度表示したメッセージが再度表示されないようにする
		if(firstReadUSBText1)
        {
            firstReadUSBText1 = false;
        }
        else if(firstReadUSBText2)
        {
            firstReadUSBText2 = false;
        }
        else if(firstReadUSBText3)
        {
            firstReadUSBText3 = false;
        }

        //ボタンをクリックできるようにする・時間を再開する・入力可能にする
        canButtonPush = true;
		BossManager.canTimePass = true;
        cannotInputObject.gameObject.SetActive(false);      

        yield break;
	}	

	protected IEnumerator ShowMessages(string[] message)
	{
		//テキストパネルを表示
		uSBTextPanel.gameObject.SetActive(true);

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
        	yield return new WaitUntil(() => IsClicked());
		}
        
        if(!firstReadUSB)
        {
		    uSBTextPanel.gameObject.SetActive(false);
        }

        firstReadUSB = false;
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

            if(ComputerBehavior.confidenceValue < 27 && !BossManager.isSecondTalk)
            {
                task1BossText.text = "評価 ✕ ";
                task1BossTextDetail.text = "全く仕事をこなしていない";
            }
            else if(ComputerBehavior.confidenceValue < 48 && !BossManager.isSecondTalk)
            {
                task1BossText.text = "評価 △ ";
                task1BossTextDetail.text = "仕事に取り掛かり始めた段階";
            } 
            else if(ComputerBehavior.confidenceValue < 105 && !BossManager.isSecondTalk)
            {
                task1BossText.text = "評価 〇 ";
                task1BossTextDetail.text = "順調に仕事をこなしている\nパスワードは「PASSWORD1」";
            }
            else if(ComputerBehavior.confidenceValue == 105 && !BossManager.isSecondTalk)
            {
                task1BossText.text = "評価 ◎ ";
                task1BossTextDetail.text = "本日の業務を完了\n優秀な人材\nパスワードは「PASSWORD1」";
            }            

            //信頼値ゲージ
            task1ConfidenceSlider.value = ComputerBehavior.confidenceValue;
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

            if(task2Flag == 2)
            {
                task2NextButton.SetActive(false);
                task2ProcessBar.value = 0;
                task2BottomText.text = "キーボードのQを長押ししてください";
                task2ProcessBar.gameObject.SetActive(true);
            }

            if(task2Flag == 3)
            {
                task2NextButton.SetActive(false);
                task2ProcessBar.value = 0;
                task2BottomText.text = "キーボードの A,C,V,Y,J,Space を長押ししてください";
            }

            if(task2Flag == 4)
            {
                task2NextButton.SetActive(false);
                task2ProcessBar.value = 0;
                task2BottomText.text = "右クリック連打!";
            }

            if(task2Flag == 5)
            {
                task2NextButtonText.text = "完了";
                task2BottomText.text = "インストールが完了しました";
                firstReadUSBText3 = true;

                if(!task2Finished){
                    task2Finished = true;
                    taskProcess++;
                }
            }

            if(task2Flag >= 6)
            {
                uSBMenuPanel.gameObject.SetActive(true);
                uSBTask2Panel.gameObject.SetActive(false);
            }
        }
	}

	public void OnButton3Clicked()
	{
        if(canButtonPush && taskProcess >= 2)
        {   
            //初めてのボタンクリックでメッセージが表示
            if(isFirstPushUSBButton3)
            {
                StartCoroutine(ShowMessages(buttonClickText3));
                isFirstPushUSBButton3 = false;
            }

            if(task3Flag < 5)
            {            
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
                if(task3_34InputField.text == "2000")
                {	
                    task3TextMessage.text = "パスワードを入力してください";
                    task3ErrorMessage.text = "";
                    task3_34InputField.text = "";
                    task3Flag++;
                }
                else
                {
                    task3ErrorMessage.text = "不正解です";
                }
            }
            else if (task3Flag == 4)
            {
                if(task3_34InputField.text == "PASSWORD1")
                {	
                    task3_34InputField.text = "";
                    task3_34InputField.gameObject.SetActive(false);
                    task3_5InputField.gameObject.SetActive(true);
                    task3TextMessage.text = "秘密の質問：\nあなたの出身中学校の名前は？\n(小文字ローマ字入力)";
                    task3ErrorMessage.text = "";
                    task3Flag++;
                }
                else
                {
                    task3ErrorMessage.text = "パスワードが正しくありません\n大文字入力されていますか？";
                }
            }
            else if (task3Flag == 5)
            {
                if(task3_5FixedCharacter == "上下市立東西南北" || task3_5FixedCharacter == "東西南北" || task3_5FixedCharacter == "じょうげしりつとうざいなんぼく" || task3_5FixedCharacter == "うえしたしりつとうざいなんぼく" || task3_5FixedCharacter == "とうざいなんぼく" || task3_5FixedCharacter == "touzainannboku" || task3_5FixedCharacter == "touzainanboku" || task3_5FixedCharacter == "tozainannboku" || task3_5FixedCharacter == "tozainanboku" || task3_5FixedCharacter == "touzainannbokutyuu" || task3_5FixedCharacter == "touzainanbokutyuu" || task3_5FixedCharacter == "tozainannbokutyuu" || task3_5FixedCharacter == "tozainanbokutyuu"|| task3_5FixedCharacter == "touzainannbokutyuugakkou" || task3_5FixedCharacter == "touzainanbokutyuugakkou" || task3_5FixedCharacter == "tozainannbokutyuugakkou" || task3_5FixedCharacter == "tozainanbokutyuugakkou" || task3_5FixedCharacter == "zyougesiritutouzainannboku" || task3_5FixedCharacter == "zyougesiritutouzainanboku" || task3_5FixedCharacter == "zyougesiritutozainannboku" || task3_5FixedCharacter == "zyougesiritutozainanboku" || task3_5FixedCharacter == "zyougesiritutouzainannbokutyuu" || task3_5FixedCharacter == "zyougesiritutouzainanbokutyuu" || task3_5FixedCharacter == "zyougesiritutozainannbokutyuu" || task3_5FixedCharacter == "zyougesiritutozainanbokutyuu"|| task3_5FixedCharacter == "zyougesiritutouzainannbokutyuugakkou" || task3_5FixedCharacter == "zyougesiritutouzainanbokutyuugakkou" || task3_5FixedCharacter == "zyougesiritutozainannbokutyuugakkou" || task3_5FixedCharacter == "zyougesiritutozainanbokutyuugakkou" || task3_5FixedCharacter == "uesitasiritutouzainannboku" || task3_5FixedCharacter == "uesitasiritutouzainanboku" || task3_5FixedCharacter == "uesitasiritutozainannboku" || task3_5FixedCharacter == "uesitasiritutozainanboku" || task3_5FixedCharacter == "uesitasiritutouzainannbokutyuu" || task3_5FixedCharacter == "uesitasiritutouzainanbokutyuu" || task3_5FixedCharacter == "uesitasiritutozainannbokutyuu"  || task3_5FixedCharacter == "uesitasiritutouzainannbokutyuugakkou" || task3_5FixedCharacter == "uesitasiritutozainannbokutyuugakkou"|| task3_5FixedCharacter == "uesitasiritutouzainanbokutyuugakkou"|| task3_5FixedCharacter == "uesitasiritutozainanbokutyuugakkou")
                {	
                    task3TextMessage.text = "ログインしました";
                    task3ErrorMessage.text = "";
                    task3_5InputField.gameObject.SetActive(false);
                    task3ButtonText.text ="完了";
                    task3Flag++;
                    taskProcess++;
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

    public void FixedSmallCharacter()
    {
        task3_5FixedCharacter = task3_5InputField.text.ToLower();
    }

	public void OnButton4Clicked()
	{
        if(canButtonPush && taskProcess >= 3)
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
    
    protected IEnumerator GameClearAction()
    {
        //ボタンをクリックできなくする・時間を止める・入力できなくする
        canButtonPush = false;
		BossManager.canTimePass = false;
        cannotInputObject.gameObject.SetActive(true);

		//ShowMessagesコルーチンを開始
		//yield return ShowMessages(sendButtonText1);
        yield return null;
        
    }

    //センドボタンのクリック
    public void OnSendButtonClicked()
    {   
        if(canButtonPush && taskProcess >= 4)
        {   
            StartCoroutine(GameClearAction());
            SceneManager.LoadScene("ClearScene");
        }
    }

	//リターンボタンのクリック
	public void OnReturnButtonClicked()
	{
        if(canButtonPush)
        {
            uSBMenuPanel.gameObject.SetActive(true);

            if(task3Flag < 6)
            {
                task3Flag = 1;
                task3Toggle.isOn = false; 
                task3TextMessage.text = "1,5,10,50,100,500,1000, ? ,\n5000,10000";
                task3ErrorMessage.text = "";
                task3_5FixedCharacter = "";
                task3_34InputField.text = "";
                task3_5InputField.text = "";
                task3_34InputField.gameObject.SetActive(true);
                task3_5InputField.gameObject.SetActive(false);
            }

            uSBTask1Panel.gameObject.SetActive(false);
            uSBTask2Panel.gameObject.SetActive(false);
            uSBTask3Panel.gameObject.SetActive(false);
            uSBTask3_1Panel.gameObject.SetActive(false);
            uSBTask3_2Panel.gameObject.SetActive(false);
            uSBTask3_345Panel.gameObject.SetActive(false);
            uSBTask4Panel.gameObject.SetActive(false);            
        }
	}    
}

