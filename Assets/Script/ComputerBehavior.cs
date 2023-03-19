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

	[SerializeField]
	Canvas uSBScreenCanvas;
	[SerializeField]
	GameObject uSBTextPanel;
	[SerializeField]
	GameObject uSBMenuPanel;
	[SerializeField]
	GameObject uSBTaskPanel1;
	
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

	[SerializeField]
    AudioSource audio_ComputerTextSound;

	[SerializeField]
    AudioClip computerTextSound;

	[SerializeField]
	GameObject uSBTaskPanel2;
	[SerializeField]
	GameObject task2NextButton;
	[SerializeField]
	Text task2BottomText;
	[SerializeField]
	Slider task2ProcessBar;
	[SerializeField]
	Text task2NextButtonText;


	private IEnumerator uSBCoroutine;

	private int task2Flag;

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
	
	public static int confidenceValue;
	private int confidenceValueDifference;
	private float computerMessageSpeed = 0.1f;

	private int uSBEventFlg;
	
	private string readingText;

	[SerializeField]
    Text timeText;
	[SerializeField]
    Text confidenceValueText;

	public static  float nextTimeBossComing;
	private float nextHourBossComing;
	private float nextMinuteBossComing;
	private string hourStr;
	private string minuteStr;
	private string confidenceValueStr;
	private bool isFirstReadUSBText2 = true;
	private bool canReadUSBText3 = false;

	void Awake(){
		//Componentを扱えるようにする
        productNumber1 = productNumber1.GetComponent<InputField> ();
		itemNumber1 = itemNumber1.GetComponent<InputField> ();
		price1 = price1.GetComponent<InputField> ();
        productNumber2 = productNumber2.GetComponent<InputField> ();
		itemNumber2 = itemNumber2.GetComponent<InputField> ();
		price2 = price2.GetComponent<InputField> ();
        productNumber3 = productNumber3.GetComponent<InputField> ();
		itemNumber3 = itemNumber3.GetComponent<InputField> ();
		price3 = price3.GetComponent<InputField> ();
        productNumber4 = productNumber4.GetComponent<InputField> ();
		itemNumber4 = itemNumber4.GetComponent<InputField> ();
		price4 = price4.GetComponent<InputField> ();
        productNumber5 = productNumber5.GetComponent<InputField> ();
		itemNumber5 = itemNumber5.GetComponent<InputField> ();
		price5 = price5.GetComponent<InputField> ();
		productNumber6 = productNumber6.GetComponent<InputField> ();
		itemNumber6 = itemNumber6.GetComponent<InputField> ();
		price6 = price6.GetComponent<InputField> ();
        productNumber7 = productNumber7.GetComponent<InputField> ();
		itemNumber7 = itemNumber7.GetComponent<InputField> ();
		price7 = price7.GetComponent<InputField> ();
        productNumber8 = productNumber8.GetComponent<InputField> ();
		itemNumber8 = itemNumber8.GetComponent<InputField> ();
		price8 = price8.GetComponent<InputField> ();
        productNumber9 = productNumber9.GetComponent<InputField> ();
		itemNumber9 = itemNumber9.GetComponent<InputField> ();
		price9 = price9.GetComponent<InputField> ();
        productNumber10 = productNumber10.GetComponent<InputField> ();
		itemNumber10 = itemNumber10.GetComponent<InputField> ();
		price10 = price10.GetComponent<InputField> ();
		productNumber11 = productNumber11.GetComponent<InputField> ();
		itemNumber11 = itemNumber11.GetComponent<InputField> ();
		price11 = price11.GetComponent<InputField> ();
        productNumber12 = productNumber12.GetComponent<InputField> ();
		itemNumber12 = itemNumber12.GetComponent<InputField> ();
		price12 = price12.GetComponent<InputField> ();
        productNumber13 = productNumber13.GetComponent<InputField> ();
		itemNumber13 = itemNumber13.GetComponent<InputField> ();
		price13 = price13.GetComponent<InputField> ();
        productNumber14 = productNumber14.GetComponent<InputField> ();
		itemNumber14 = itemNumber14.GetComponent<InputField> ();
		price14 = price14.GetComponent<InputField> ();
        productNumber5 = productNumber5.GetComponent<InputField> ();
		itemNumber5 = itemNumber5.GetComponent<InputField> ();
		price15 = price15.GetComponent<InputField> ();
		productNumber16 = productNumber16.GetComponent<InputField> ();
		itemNumber16 = itemNumber16.GetComponent<InputField> ();
		price16 = price16.GetComponent<InputField> ();
        productNumber17 = productNumber17.GetComponent<InputField> ();
		itemNumber17 = itemNumber17.GetComponent<InputField> ();
		price17 = price17.GetComponent<InputField> ();
        productNumber18 = productNumber18.GetComponent<InputField> ();
		itemNumber18 = itemNumber18.GetComponent<InputField> ();
		price18 = price18.GetComponent<InputField> ();
        productNumber19 = productNumber19.GetComponent<InputField> ();
		itemNumber19 = itemNumber19.GetComponent<InputField> ();
		price19 = price19.GetComponent<InputField> ();
        productNumber20 = productNumber20.GetComponent<InputField> ();
		itemNumber20 = itemNumber20.GetComponent<InputField> ();
		price20 = price20.GetComponent<InputField> ();
 		productNumber21 = productNumber21.GetComponent<InputField> ();
		itemNumber21 = itemNumber21.GetComponent<InputField> ();
		price21 = price21.GetComponent<InputField> ();
        productNumber22 = productNumber22.GetComponent<InputField> ();
		itemNumber22 = itemNumber22.GetComponent<InputField> ();
		price22 = price22.GetComponent<InputField> ();
        productNumber23 = productNumber23.GetComponent<InputField> ();
		itemNumber23 = itemNumber23.GetComponent<InputField> ();
		price23 = price23.GetComponent<InputField> ();
        productNumber24 = productNumber24.GetComponent<InputField> ();
		itemNumber24 = itemNumber24.GetComponent<InputField> ();
		price24 = price24.GetComponent<InputField> ();
        productNumber25 = productNumber25.GetComponent<InputField> ();
		itemNumber25 = itemNumber25.GetComponent<InputField> ();
		price25 = price25.GetComponent<InputField> ();

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
	}

	public void InputText(){
		//信頼値、信頼値差異の初期化
		confidenceValueDifference = 0;
		
		//配列にinputFieldの内容を反映
		string[] productNumberStrings = new string[] {productNumber1.text,productNumber2.text,productNumber3.text,productNumber4.text,productNumber5.text,
								                      productNumber6.text,productNumber7.text,productNumber8.text,productNumber9.text,productNumber10.text,
								                      productNumber11.text,productNumber12.text,productNumber13.text,productNumber14.text,productNumber15.text,
								                      productNumber16.text,productNumber17.text,productNumber18.text,productNumber19.text,productNumber20.text,
								                      productNumber21.text,productNumber22.text,productNumber23.text,productNumber24.text,productNumber25.text,};

		string[] itemNumberStrings = new string[] {itemNumber1.text,itemNumber2.text,itemNumber3.text,itemNumber4.text,itemNumber5.text,
							                       itemNumber6.text,itemNumber7.text,itemNumber8.text,itemNumber9.text,itemNumber10.text,
							                       itemNumber11.text,itemNumber12.text,itemNumber13.text,itemNumber14.text,itemNumber15.text,
							                       itemNumber16.text,itemNumber17.text,itemNumber18.text,itemNumber19.text,itemNumber20.text,
							                       itemNumber21.text,itemNumber22.text,itemNumber23.text,itemNumber24.text,itemNumber25.text,};

		string[] priceStrings = new string[] {price1.text,price2.text,price3.text,price4.text,price5.text,
						                      price6.text,price7.text,price8.text,price9.text,price10.text,
						                      price11.text,price12.text,price13.text,price14.text,price15.text,
						                      price16.text,price17.text,price18.text,price19.text,price20.text,
						                      price21.text,price22.text,price23.text,price24.text,price25.text,};
		
		for(int i = 0; i < 25; i++){
			//製品番号・個数・価格が正しければ信頼値が上昇
			if(productNumberStrings[i] == correctProductNumber[i] && itemNumberStrings[i] == correctItemNumber[i] && priceStrings[i] == correctPrice[i]){
				confidenceValueDifference += 4;
			}
		}

		confidenceValue += confidenceValueDifference;
	}

	void Update(){
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
			uSBTaskPanel2.gameObject.SetActive(false);
			uSBTextPanel.gameObject.SetActive(false);
			uSBScreenCanvas.gameObject.SetActive (false);

			//コルーチンを停止して初期化
        	StopCoroutine(uSBCoroutine);
        	uSBCoroutine = null;
			
		}

		//Task2のProcessBar
		if(uSBTaskPanel2.activeSelf)
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
		uSBTaskPanel2.gameObject.SetActive(false);

		if(uSBEventFlg == 3){
			StopCoroutine(uSBCoroutine);
			uSBCoroutine = CreateUSBCoroutine();
            StartCoroutine(uSBCoroutine);
		}
	}

	public void OnButton2Clicked(){
		if(task2Flag <= 4){
			task2Flag = 1;
			uSBMenuPanel.gameObject.SetActive(false);
			uSBTaskPanel2.gameObject.SetActive(true);
			if(!task2NextButton.activeSelf){
				task2NextButton.SetActive(true);
			}

			if(task2ProcessBar.gameObject.activeSelf){
				task2ProcessBar.gameObject.SetActive(false);
			}

			task2ProcessBar.value = 0;
			task2BottomText.text = "次へ　をクリックしてください。";
		} else{
			task2BottomText.text = "インストールは完了しています。";
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
			uSBTaskPanel2.gameObject.SetActive(false);
		}
	}
}