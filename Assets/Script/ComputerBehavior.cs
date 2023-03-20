using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerBehavior : MonoBehaviour {

	[SerializeField] InputField productNumber1;
	[SerializeField] InputField itemNumber1;
	[SerializeField] InputField price1;
	[SerializeField] InputField productNumber2;
	[SerializeField] InputField itemNumber2;
	[SerializeField] InputField price2;
	[SerializeField] InputField productNumber3;
	[SerializeField] InputField itemNumber3;
	[SerializeField] InputField price3;
	[SerializeField] InputField productNumber4;
	[SerializeField] InputField itemNumber4;
	[SerializeField] InputField price4;
	[SerializeField] InputField productNumber5;
	[SerializeField] InputField itemNumber5;
	[SerializeField] InputField price5;
	[SerializeField] InputField productNumber6;
	[SerializeField] InputField itemNumber6;
	[SerializeField] InputField price6;
	[SerializeField] InputField productNumber7;
	[SerializeField] InputField itemNumber7;
	[SerializeField] InputField price7;
	[SerializeField] InputField productNumber8;
	[SerializeField] InputField itemNumber8;
	[SerializeField] InputField price8;
	[SerializeField] InputField productNumber9;
	[SerializeField] InputField itemNumber9;
	[SerializeField] InputField price9;
	[SerializeField] InputField productNumber10;
	[SerializeField] InputField itemNumber10;
	[SerializeField] InputField price10;
	[SerializeField] InputField productNumber11;
	[SerializeField] InputField itemNumber11;
	[SerializeField] InputField price11;
	[SerializeField] InputField productNumber12;
	[SerializeField] InputField itemNumber12;
	[SerializeField] InputField price12;
	[SerializeField] InputField productNumber13;
	[SerializeField] InputField itemNumber13;
	[SerializeField] InputField price13;
	[SerializeField] InputField productNumber14;
	[SerializeField] InputField itemNumber14;
	[SerializeField] InputField price14;
	[SerializeField] InputField productNumber15;
	[SerializeField] InputField itemNumber15;
	[SerializeField] InputField price15;
	[SerializeField] InputField productNumber16;
	[SerializeField] InputField itemNumber16;
	[SerializeField] InputField price16;
	[SerializeField] InputField productNumber17;
	[SerializeField] InputField itemNumber17;
	[SerializeField] InputField price17;
	[SerializeField] InputField productNumber18;
	[SerializeField] InputField itemNumber18;
	[SerializeField] InputField price18;
	[SerializeField] InputField productNumber19;
	[SerializeField] InputField itemNumber19;
	[SerializeField] InputField price19;
	[SerializeField] InputField productNumber20;
	[SerializeField] InputField itemNumber20;
	[SerializeField] InputField price20;
	[SerializeField] InputField productNumber21;
	[SerializeField] InputField itemNumber21;
	[SerializeField] InputField price21;
	[SerializeField] InputField productNumber22;
	[SerializeField] InputField itemNumber22;
	[SerializeField] InputField price22;
	[SerializeField] InputField productNumber23;
	[SerializeField] InputField itemNumber23;
	[SerializeField] InputField price23;
	[SerializeField] InputField productNumber24;
	[SerializeField] InputField itemNumber24;
	[SerializeField] InputField price24;
	[SerializeField] InputField productNumber25;
	[SerializeField] InputField itemNumber25;
	[SerializeField] InputField price25;

	private List<InputField> inputFieldsList;

	private string[] correctProductNumber = new string[] {"X2458FF31","X2458FF32","X2458FF33","X2458FF34","X2458FF35",
									              "A9902CF78","A9902CF79","A9902CF80","X2458DF01","X2458DF02",
									              "X2458DF03","X2458DF04","X2458DF05","X2458DF06","X2458DF07",
									              "C6222FF03","C6222FF04","C6222FF05","C4451DF11","C4451DF12",
									              "C4451DF13","C4451DF14","X2458FF97","X2458FF98","X2458FF99"};
	private string[] correctItemNumber = new string[] {"5","5","2","4","3",
								               		   "100","150","150","70","75",
								               		   "80","85","80","80","70",
								                       "10","10","14","15","15",
								                       "10","10","30","30","30"};
	private string[] correctPrice = new string[] {"20980","20980","15980","15980","10800",
	                                              "690","790","790","690","890",
	                                              "150","170","120","160","190",
	                                              "8980","8480","8480","8980","10980",
	                                              "9980","5980","2980","3280","3880",};

	private IEnumerator uSBCoroutine;

	[SerializeField]
	Canvas uSBScreenCanvas;
	[SerializeField]
	GameObject uSBTextPanel;
	[SerializeField]
	GameObject uSBMenuPanel;

	[SerializeField]
    Text uSBPanelText;

	[SerializeField]
	TextAsset uSBTextAsset1;
	[SerializeField]
	TextAsset uSBTextAsset2;
	[SerializeField]
	TextAsset uSBTextAsset3;

	private string loadUSBText1;
    private string[] uSBText1;
	private string loadUSBText2;
    private string[] uSBText2;
	private string loadUSBText3;
    private string[] uSBText3;
	
	//BGMアセット
	[SerializeField]
    AudioSource audio_ComputerTextSound;

	[SerializeField]
    AudioClip computerTextSound;

	//タスク1のアセット
	[SerializeField]
	GameObject uSBTaskPanel1;
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
	/*[SerializeField]
	GameObject reCAPTCHAButton;*/
	private int task2Flag;

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
	InputField task3_345InputField;
	
	[SerializeField]
	Text task3TextMessage;
	[SerializeField]
	Text task3ErrorMessage;
	[SerializeField]
	Text task3ButtonText;

	private int task3Flag;

	public static int confidenceValue;
	private float computerMessageSpeed = 0.1f;
	private int uSBEventFlg;
	private string readingText;

	public static  float nextTimeBossComing;
	private float nextHourBossComing;
	private float nextMinuteBossComing;
	private string hourStr;
	private string minuteStr;
	private string confidenceValueStr;
	private bool isFirstReadUSBText2 = true;
	private bool canReadUSBText3 = false;

	void Awake(){

		inputFieldsList = new List<InputField>();
		//Componentを扱えるようにする
        inputFieldsList.Add(productNumber1);
		inputFieldsList.Add(itemNumber1);
		inputFieldsList.Add(price1);
        inputFieldsList.Add(productNumber2);
		inputFieldsList.Add(itemNumber2);
		inputFieldsList.Add(price2);
        inputFieldsList.Add(productNumber3);
		inputFieldsList.Add(itemNumber3);
		inputFieldsList.Add(price3);
        inputFieldsList.Add(productNumber4);
		inputFieldsList.Add(itemNumber4);
		inputFieldsList.Add(price4);
        inputFieldsList.Add(productNumber5);
		inputFieldsList.Add(itemNumber5);
		inputFieldsList.Add(price5);
        inputFieldsList.Add(productNumber6);
		inputFieldsList.Add(itemNumber6);
		inputFieldsList.Add(price6);
        inputFieldsList.Add(productNumber7);
		inputFieldsList.Add(itemNumber7);
		inputFieldsList.Add(price7);
        inputFieldsList.Add(productNumber8);
		inputFieldsList.Add(itemNumber8);
		inputFieldsList.Add(price8);
        inputFieldsList.Add(productNumber9);
		inputFieldsList.Add(itemNumber9);
		inputFieldsList.Add(price9);
        inputFieldsList.Add(productNumber10);
		inputFieldsList.Add(itemNumber10);
		inputFieldsList.Add(price10);
		inputFieldsList.Add(productNumber11);
		inputFieldsList.Add(itemNumber11);
		inputFieldsList.Add(price11);
        inputFieldsList.Add(productNumber12);
		inputFieldsList.Add(itemNumber12);
		inputFieldsList.Add(price12);
        inputFieldsList.Add(productNumber13);
		inputFieldsList.Add(itemNumber13);
		inputFieldsList.Add(price13);
        inputFieldsList.Add(productNumber14);
		inputFieldsList.Add(itemNumber14);
		inputFieldsList.Add(price14);
        inputFieldsList.Add(productNumber15);
		inputFieldsList.Add(itemNumber15);
		inputFieldsList.Add(price15);
        inputFieldsList.Add(productNumber16);
		inputFieldsList.Add(itemNumber16);
		inputFieldsList.Add(price16);
        inputFieldsList.Add(productNumber17);
		inputFieldsList.Add(itemNumber17);
		inputFieldsList.Add(price17);
        inputFieldsList.Add(productNumber18);
		inputFieldsList.Add(itemNumber18);
		inputFieldsList.Add(price18);
        inputFieldsList.Add(productNumber19);
		inputFieldsList.Add(itemNumber19);
		inputFieldsList.Add(price19);
        inputFieldsList.Add(productNumber20);
		inputFieldsList.Add(itemNumber20);
		inputFieldsList.Add(price20);
 		inputFieldsList.Add(productNumber21);
		inputFieldsList.Add(itemNumber21);
		inputFieldsList.Add(price21);
        inputFieldsList.Add(productNumber22);
		inputFieldsList.Add(itemNumber22);
		inputFieldsList.Add(price22);
        inputFieldsList.Add(productNumber23);
		inputFieldsList.Add(itemNumber23);
		inputFieldsList.Add(price23);
        inputFieldsList.Add(productNumber24);
		inputFieldsList.Add(itemNumber24);
		inputFieldsList.Add(price24);
        inputFieldsList.Add(productNumber25);
		inputFieldsList.Add(itemNumber25);
		inputFieldsList.Add(price25);

		//テキストアセットのロード
		loadUSBText1 = uSBTextAsset1.text;
        uSBText1 = loadUSBText1.Split(char.Parse("\n"));
		loadUSBText2 = uSBTextAsset2.text;
        uSBText2 = loadUSBText2.Split(char.Parse("\n"));
		loadUSBText3 = uSBTextAsset3.text;
        uSBText3 = loadUSBText3.Split(char.Parse("\n"));

		//値の初期化
	    uSBEventFlg = 1;
		confidenceValue = 0;
		task2Flag = 1;
		task3Flag = 1;
	}
	
	//tabキーを押すと次の入力欄へ移動するメソッド
	public void TabKeyPressed(){
		for(int i = 0; i < 74 ; i++)
		{
			if(inputFieldsList[i].isFocused)
			{	
				inputFieldsList[i+1].Select();
			}
		}
	}	

	public void InputText(){
		//信頼値の初期化
		confidenceValue = 0;
		
		//製品番号・個数・価格が正しければ信頼値が上昇
		for(int i = 0; i < 25; i++)
		{	
			if(inputFieldsList[3*i].text == correctProductNumber[i] && inputFieldsList[3*i + 1].text == correctItemNumber[i] && inputFieldsList[3*i + 2].text == correctPrice[i])
			{
				confidenceValue += 4;
			}
		}
	}

	void Update(){
		//tabキーを押すと次の入力欄へ
		if(Input.GetKeyUp(KeyCode.Tab) && !BossManager.isUSBConnected)
		{
			TabKeyPressed();
		}

		if (BossManager.isUSBConnected && uSBCoroutine == null) {
            //コルーチンの起動
			uSBCoroutine = CreateUSBCoroutine();
            StartCoroutine(uSBCoroutine);
        }
		
		if (!BossManager.isUSBConnected && uSBCoroutine != null) {
			if(uSBEventFlg >= 2){
				uSBMenuPanel.gameObject.SetActive(true);
			}	
			uSBTaskPanel1.gameObject.SetActive(false);
			uSBTask2Panel.gameObject.SetActive(false);
			uSBTask3Panel.gameObject.SetActive(false);
			uSBTask3_1Panel.gameObject.SetActive(false);
			uSBTask3_2Panel.gameObject.SetActive(false);
			uSBTask3_345Panel.gameObject.SetActive(false);
			task3TextMessage.text = "1,5,10,50,100,500,1000, ? ,\n5000,10000";
			task3ErrorMessage.text = "";
			uSBTextPanel.gameObject.SetActive(false);
			uSBScreenCanvas.gameObject.SetActive (false);

			//コルーチンを停止して初期化
			if(uSBCoroutine != null)
			{
        		StopCoroutine(uSBCoroutine);
        		uSBCoroutine = null;
			}	
		}

		//Task2のProcessBar
		if(uSBTask2Panel.activeSelf)
		{
			if(task2Flag == 2){
				//ボタンを長押しでゲージを増加
				if(Input.GetKey(KeyCode.Q)){
					task2ProcessBar.value += Time.deltaTime;
					if(task2ProcessBar.value == 5f){
						task2NextButton.SetActive(true);
					}
				}
				//ボタンを離した際にバーを初期化
				if(Input.GetKeyUp(KeyCode.Q)){
					if(task2ProcessBar.value != 5f){
						task2ProcessBar.value = 0;
					}
				}
			}
			if(task2Flag == 3){
				//ボタンを長押しでゲージを増加
				if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.V) && Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.J) && Input.GetKey(KeyCode.Space)){
					task2ProcessBar.value += Time.deltaTime;
					if(task2ProcessBar.value == 5f){
						task2NextButton.SetActive(true);
					}
				}
				//ボタンを離した際にバーを初期化
				if(Input.GetKeyUp(KeyCode.A) && Input.GetKeyUp(KeyCode.C) && Input.GetKeyUp(KeyCode.V) && Input.GetKeyUp(KeyCode.Y) && Input.GetKeyUp(KeyCode.J) && Input.GetKeyUp(KeyCode.Space)){
					if(task2ProcessBar.value != 5f){
						task2ProcessBar.value = 0;
					}
				}
			}
			if(task2Flag == 4){
				//右クリック連打でゲージを増加
				if(Input.GetMouseButtonUp(1)){
					task2ProcessBar.value += 0.05f;
				}
				
				if(task2ProcessBar.value == 5f){
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

	private IEnumerator CreateUSBCoroutine(){
		//canvasをアクティブ化
		uSBScreenCanvas.gameObject.SetActive (true);

		if(uSBEventFlg == 1){
			yield return USBAction1();
		} else if(uSBEventFlg == 2){
			yield return USBAction2();
			uSBMenuPanel.gameObject.SetActive(true);
			uSBTaskPanel1.gameObject.SetActive(false);
		} else if(uSBEventFlg == 3){
			yield return USBAction3();
		}
	}

	protected IEnumerator USBAction1(){
		//テキストパネルを表示
		uSBTextPanel.gameObject.SetActive (true);

		//時間を止める
		BossManager.canTimePass = false;

		//テキストを表示
		for (int i = 0; i < uSBText1.Length; ++i)
        {            
            StartCoroutine(ShowMessage(uSBText1[i]));

            //showMessageが終わるまで待機
            yield return ShowMessage(uSBText1[i]);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

		//時間を再開する
		BossManager.canTimePass = true;
		//次のイベントへ
		uSBEventFlg += 1;
	}	

	protected IEnumerator USBAction2(){
		
		//メニューパネルを表示
		uSBMenuPanel.gameObject.SetActive(true);
	
		//テキストを表示
		if(isFirstReadUSBText2){
			//テキストパネルを表示
			uSBTextPanel.gameObject.SetActive(true);

			//時間を止める
			BossManager.canTimePass = false;

			for (int i = 0; i < uSBText2.Length; ++i)
        	{            
        	    StartCoroutine(ShowMessage(uSBText2[i]));

        	    //showMessageが終わるまで待機
        	    yield return ShowMessage(uSBText2[i]);
        	    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        	}

			isFirstReadUSBText2 = false;
			uSBTextPanel.gameObject.SetActive(false);
			
			//時間を再開する
			BossManager.canTimePass = true;
			uSBEventFlg += 1;
		}
	}	

	protected IEnumerator USBAction3(){
		//テキストを表示
		if(canReadUSBText3){
			//テキストパネルを表示
			uSBTextPanel.gameObject.SetActive(true);

			//時間を止める
			BossManager.canTimePass = false;

			for (int i = 0; i < uSBText3.Length; ++i)
        	{            
        	    StartCoroutine(ShowMessage(uSBText3[i]));

        	    //showMessageが終わるまで待機
        	    yield return ShowMessage(uSBText3[i]);
        	    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        	}

			uSBTextPanel.gameObject.SetActive(false);
			
			//時間を再開する
			BossManager.canTimePass = true;
			uSBEventFlg += 1;
			canReadUSBText3 = false;
		}
	}

	//メッセージを表示する
    protected IEnumerator ShowMessage(string message){   
		float time = 0;
        int readingTextWordNumber = 1;

        while ( true )
        {   
            yield return null;
            
            time += Time.deltaTime;

            // クリックされると一気に表示
            if ( IsClicked() ) break;
            
            int len = Mathf.FloorToInt ( time / computerMessageSpeed);
            if (len <= message.Length) {    
                this.uSBPanelText.text = message.Substring(0, len);
                if(readingTextWordNumber == len){
                    audio_ComputerTextSound.PlayOneShot(computerTextSound);
                    readingTextWordNumber++;
                }
            } else {
                break;
            }            
        }
        
        this.uSBPanelText.text = message;
        yield return null;        
    }

	public void OnButton1Clicked(){
		uSBMenuPanel.gameObject.SetActive(false);
		uSBTaskPanel1.gameObject.SetActive(true);

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
		confidenceValueStr = confidenceValue.ToString();
		confidenceValueText.text = confidenceValueStr + " / 100";

	}

	public void OnReturnButtonClicked(){
		uSBMenuPanel.gameObject.SetActive(true);
		uSBTaskPanel1.gameObject.SetActive(false);
		uSBTask2Panel.gameObject.SetActive(false);
		uSBTask3Panel.gameObject.SetActive(false);
		uSBTask3_1Panel.gameObject.SetActive(false);
		uSBTask3_2Panel.gameObject.SetActive(false);
		uSBTask3_345Panel.gameObject.SetActive(false);
		task3TextMessage.text = "1,5,10,50,100,500,1000, ? ,\n5000,10000";
		task3ErrorMessage.text = "";

		if(uSBEventFlg == 3){
			StopCoroutine(uSBCoroutine);
			uSBCoroutine = CreateUSBCoroutine();
            StartCoroutine(uSBCoroutine);
		}
	}

	public void OnButton2Clicked(){
		uSBMenuPanel.gameObject.SetActive(false);
		uSBTask2Panel.gameObject.SetActive(true);

		if(task2Flag <= 4){
			task2Flag = 1;
			if(!task2NextButton.activeSelf){
				task2NextButton.SetActive(true);
			}

			if(task2ProcessBar.gameObject.activeSelf){
				task2ProcessBar.gameObject.SetActive(false);
			}

			task2ProcessBar.value = 0;
			task2BottomText.text = "次へ　をクリックしてください。";
		} else{
			task2BottomText.text = "インストールが完了しました";
		}
	}
	
	public void OnTask2NextButtonClicked(){
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
		}

		if(task2Flag >= 6){
			uSBMenuPanel.gameObject.SetActive(true);
			uSBTask2Panel.gameObject.SetActive(false);
		}
	}

	public void OnButton3Clicked(){
		if(task3Flag < 6){
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

	public void OnTask3ToggleChanged(){
		uSBTask3_1Panel.gameObject.SetActive(false);
		uSBTask3_2Panel.gameObject.SetActive(true);	
		task3Flag++;
	}

	public void OnreCAPTCHA(){
		/*GameObject clickedButton =
		GetComponent<Button>().image.color = new Color (95,169,255,255);*/
	}

	public void OnTask3BottonClicked(){
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
				task3TextMessage.text = "秘密の質問：/nあなたの出身中学校の名前は？";
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
			}
			else
			{
				task3ErrorMessage.text = "秘密の質問が正しくありません";
			}
		}
		else
		{
			uSBMenuPanel.gameObject.SetActive(true);
			uSBTask3_345Panel.gameObject.SetActive(false);
		}
	}
}