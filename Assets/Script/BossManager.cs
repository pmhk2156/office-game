using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    Image bossBubble;
    [SerializeField]
    Text bossBubbleText;
    [SerializeField]
    GameObject cannotInputObject;
    
    [SerializeField]
    AudioSource audio_BGM;
    [SerializeField]
    AudioSource audio_TextSound;
    [SerializeField]
    AudioSource audio_Footsteps;
    [SerializeField]
    AudioSource audio_ErrorSound;

    AudioSource[] sEs;

    [SerializeField]
    AudioClip textSound;
    [SerializeField]
    AudioClip footsteps;
    [SerializeField]
    AudioClip errorSound;

    [SerializeField]
	TextAsset textAsset1;
    [SerializeField]
	TextAsset textAsset2;
    [SerializeField]
	TextAsset textAsset3;
    [SerializeField]
	TextAsset textAsset4;
    [SerializeField]
	TextAsset textAsset5;
    [SerializeField]
	TextAsset textAsset6;
    [SerializeField]
	TextAsset textAsset7;
    [SerializeField]
	TextAsset errorSoundingTextAsset;
    [SerializeField]
	TextAsset errorGameOverTextAsset;
    [SerializeField]
	TextAsset gameOverTextAsset;
    [SerializeField]
    TextAsset gameEndText1Asset;
    [SerializeField]
    TextAsset gameEndText2Asset;

    private string loadText1;
    private string loadText2;
    private string loadText3;
    private string loadText4; 
    private string loadText5;
    private string loadText6;
    private string loadText7;
    private string loadErrorSoundingText;
    private string loadErrorGameOverText;
    private string loadGameOverText;
    private string loadGameEndText1;
    private string loadGameEndText2;
    private string[] splitText1;
    private string[] splitText2;
    private string[] splitText3;
    private string[] splitText4;
    private string[] splitText5;
    private string[] splitText6;
    private string[] splitText7;
    private string[] splitErrorSoundingText;
    private string[] splitErrorGameOverText;
    private string[] splitGameOverText;
    private string[] splitGameEndText1;
    private string[] splitGameEndText2;
    

    public float bGMVolume; 
    public float sEVolume; 

    public static float nextSecondBossComing;
    public static float nextBossTime;
    private float randomChangeBossComingTime;
    private float randomFootstepsTime;
    private IEnumerator coroutine;
    private bool isFirstTalk;
    public static bool isSecondTalk;
    public static bool canTimePass;
    public static bool canUSBDrug;
    public static bool isUSBConnected;
    public static bool isErrorSounding;
    private bool isFirstShowText4;

    private string readingText;

    private float messageSpeed = 0.1f;

    void Awake()
    {   
        nextSecondBossComing = 0f;
        nextBossTime = 0f;
        coroutine = null;
        isFirstTalk = true;
        isSecondTalk = true;
        canTimePass = true;
        canUSBDrug = true;
        isUSBConnected = false;
        isErrorSounding = false;
        isFirstShowText4 = true;

        //テキスト読み込み
        loadText1 = textAsset1.text;
        splitText1 = loadText1.Split(',');
        loadText2 = textAsset2.text;
        splitText2 = loadText2.Split(',');
        loadText3 = textAsset3.text;
        splitText3 = loadText3.Split(',');
        loadText4 = textAsset4.text;
        splitText4 = loadText4.Split(',');
        loadText5 = textAsset5.text;
        splitText5 = loadText5.Split(',');
        loadText6 = textAsset6.text;
        splitText6 = loadText6.Split(',');
        loadText7 = textAsset7.text;
        splitText7 = loadText7.Split(',');
        loadErrorSoundingText = errorSoundingTextAsset.text;
        splitErrorSoundingText = loadErrorSoundingText.Split(',');
        loadErrorGameOverText = errorGameOverTextAsset.text;
        splitErrorGameOverText = loadErrorGameOverText.Split(',');
        loadGameOverText = gameOverTextAsset.text;
        splitGameOverText = loadGameOverText.Split(',');
        loadGameEndText1 = gameEndText1Asset.text;
        splitGameEndText1 = loadGameEndText1.Split(',');
        loadGameEndText2 = gameEndText2Asset.text;
        splitGameEndText2 = loadGameEndText2.Split(',');

        audio_BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        sEs = GetComponents<AudioSource>();
        audio_TextSound = sEs[0];
        audio_Footsteps = sEs[1];
        audio_ErrorSound = sEs[2];
    }


    void Update()
    {   
        if(MinuteHandManager.elapsedTime >= nextBossTime && coroutine == null)
        {
            coroutine = CreateCoroutine();        
            //コルーチンの起動
            StartCoroutine(coroutine);
        }

        //エラー音が鳴り響いた時
        if(isErrorSounding && coroutine == null)
        {
            coroutine = ErrorSounding(); 
            //コルーチンの起動
            StartCoroutine(coroutine);
            isErrorSounding = false;
        }
    }

    private bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    private IEnumerator CreateCoroutine()
    {

        if(!isFirstTalk)
        {
            yield return BossComingAudioSet(); 
        }

    
        boss.gameObject.SetActive(true);
        //時間を止める・USBをドラッグできなくする
        canTimePass = false;
        canUSBDrug = false;
        cannotInputObject.gameObject.SetActive(true);

        yield return BossAction();

        //上司・ふきだしを消す
        this.bossBubbleText.text = "";
        this.bossBubble.gameObject.SetActive(false);
        this.boss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        this.boss.gameObject.SetActive(false);

        
        //次の上司の来る時間を、信頼値に応じて設定
        if(isFirstTalk)
        {
            nextSecondBossComing  = 60f;
        }
        else if(ComputerBehavior.confidenceValue < 13)
        {
            nextSecondBossComing  = 30f;
        }
        else if(ComputerBehavior.confidenceValue < 27)
        {
            nextSecondBossComing  = 90f;
        }
        else if(ComputerBehavior.confidenceValue < 69)
        {
            nextSecondBossComing  = 120f;
        }
        else if(ComputerBehavior.confidenceValue < 105)
        {
            nextSecondBossComing  = 150f;
        }
        else
        {
            nextSecondBossComing  = 180f;
        }

        //ランダム時間ずれた分を調整
        if(!isFirstTalk)
        {
            nextSecondBossComing = nextSecondBossComing - randomChangeBossComingTime;
        }

        //上司の来る時間がランダムに変わる
        randomChangeBossComingTime = Random.Range(0f,10f) - 5f;
        nextBossTime = MinuteHandManager.elapsedTime + nextSecondBossComing + randomChangeBossComingTime;
        //次の上司の来る時間が17時を超える際、17時に来るように設定
        if(nextBossTime >= 520)
        {
            nextBossTime = 540;
        }

        //上司の足音が鳴る時間を設定
        randomFootstepsTime = Random.Range(2f,8.5f);
        nextBossTime = nextBossTime - randomFootstepsTime;

        isFirstTalk = false;

        //BGMの音量を元に戻す
        audio_BGM.volume = bGMVolume;

        //時間を動かす・USBをドラッグできる・入力可能にする
        canTimePass = true;
        canUSBDrug = true;
        cannotInputObject.gameObject.SetActive(false);

        //信頼度が高い時にパスワードを教える
        if(isFirstShowText4 && ComputerBehavior.confidenceValue > 48)
        {   
            yield return new WaitForSeconds(4f);
            //足音の設定
            audio_Footsteps.PlayOneShot(footsteps);
            for(int i = 0; i < 4; i++)
            {
                audio_BGM.volume -= 0.2f * bGMVolume;
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(0.2f);
            audio_Footsteps.Stop();

            boss.gameObject.SetActive(true);
            //時間を止める・USBをドラッグできなくする
            canTimePass = false;
            canUSBDrug = false;
            cannotInputObject.gameObject.SetActive(true);

            if(isUSBConnected) 
            {
                yield return GameOverAction();
            }
            
            yield return BossColorChange();
            yield return ShowMessages(splitText6);

            isFirstShowText4 = false;
            this.boss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            boss.gameObject.SetActive(false);

            //時間を動かす・USBをドラッグできる・入力可能にする
            canTimePass = true;
            canUSBDrug = true;
            cannotInputObject.gameObject.SetActive(false);
        }

        //コルーチンを停止して初期化
        StopCoroutine(coroutine);
        coroutine = null;        
    }

    protected IEnumerator BossAction()
    {   
        //ボスを徐々に鮮明に
        yield return BossColorChange();

        //USBが繋がっていたらゲームオーバー
        if(isUSBConnected) 
        {
            yield return GameOverAction();
        }
        else if(MinuteHandManager.elapsedTime >= 535)
        {
            yield return GameEndAction();
        }
        else 
        {
            //会話を表示
            if(isFirstTalk)
            {         
                yield return ShowMessages(splitText1);
            } 
            else if(isSecondTalk)
            {       
                yield return ShowMessages(splitText7);
                isSecondTalk = false;
            }
            else if(ComputerBehavior.confidenceValue < 27 )
            {
                yield return ShowMessages(splitText2);
            }
            else if(ComputerBehavior.confidenceValue < 48 )
            {
                yield return ShowMessages(splitText3);
            } 
            else if(ComputerBehavior.confidenceValue < 105 )
            {
                yield return ShowMessages(splitText4);
            }

            else 
            {
                yield return ShowMessages(splitText5);
            }

            
            yield break;
        }
    }

    protected IEnumerator BossComingAudioSet()
    {       
        audio_Footsteps.PlayOneShot(footsteps);
        for(int i = 0; i < 4; i++)
        {
            audio_BGM.volume -= 0.2f * bGMVolume;
            yield return new WaitForSeconds(0.5f);
        }  
        yield return new WaitForSeconds(randomFootstepsTime - 2f);
        audio_Footsteps.Stop();
    }
    
    //上司を色づける
    protected IEnumerator BossColorChange()
    {
        for(int i=0; i<=12; i++)
        {
            yield return new WaitForSeconds(0.03f);
            boss.GetComponent<SpriteRenderer>().color += new Color(0f, 0f, 0f, 0.05f);
        }
        boss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield break;
    }
    
    protected IEnumerator ShowMessages(string[] message)
	{   
        //ふきだしを表示
        bossBubble.gameObject.SetActive(true);

        //ボタンをクリックできなくする
        USBScreenManager.canButtonPush = false;

		//時間を止める
		canTimePass = false;

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
            	int len = Mathf.FloorToInt ( time / messageSpeed);
            	if (len <= message[i].Length) 
				{    
					//メッセージの表示
                	this.bossBubbleText.text = message[i].Substring(0, len);
					
					//メッセージ送りの音声
                	if(readingTextWordNumber == len){
                    	audio_TextSound.PlayOneShot(textSound);
           	        	readingTextWordNumber++;
           	    	}
				}
            	else 
				{
                	break;
            	}            

			}

			//メッセージを全て表示
        	this.bossBubbleText.text = message[i];
        	yield return null; 
		
        	//次のクリックまで待機
        	yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
		}
        
		bossBubble.gameObject.SetActive(false);
			
		//時間を再開する
		canTimePass = true;
        //ボタンをクリックできるようにする
        USBScreenManager.canButtonPush = true;
	}

    private IEnumerator ErrorSounding()
    {   
        audio_Footsteps.PlayOneShot(errorSound);
        yield return new WaitForSeconds(1f);
        audio_Footsteps.PlayOneShot(footsteps);
        for(int i = 0; i < 4; i++)
        {
            audio_BGM.volume -= 0.2f * bGMVolume;
            yield return new WaitForSeconds(0.5f);
        }  
        audio_Footsteps.Stop();

        boss.gameObject.SetActive(true);
        //時間を止める・USBをドラッグできなくする
        canTimePass = false;
        canUSBDrug = false;

        yield return BossColorChange();

        if(!isUSBConnected)
        {
                yield return ShowMessages(splitErrorSoundingText);
        }
        else
        {         
            yield return ShowMessages(splitErrorGameOverText);
            SceneManager.LoadScene("GameOverScene");
        }

        //上司・ふきだしを消す
        this.bossBubbleText.text = "";
        this.bossBubble.gameObject.SetActive(false);
        this.boss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        this.boss.gameObject.SetActive(false);

        //BGMの音量を元に戻す
        audio_BGM.volume = bGMVolume;

        //時間を動かす・USBをドラッグできる
        canTimePass = true;
        canUSBDrug = true;

        //コルーチンを停止して初期化
        StopCoroutine(coroutine);
        coroutine = null;     
    }
    
    //ゲームオーバー処理
    private IEnumerator GameOverAction()
    {
        yield return BossColorChange();
        yield return ShowMessages(splitGameOverText);
        
        StopCoroutine(coroutine);
        coroutine = null;
        SceneManager.LoadScene("GameOverScene");
    }

    protected IEnumerator GameEndAction()
    {
        yield return BossColorChange();
        
        if(ComputerBehavior.confidenceValue >= 105)
        {
            yield return ShowMessages(splitGameEndText1);
            StopCoroutine(coroutine);
            coroutine = null;
            SceneManager.LoadScene("ClearScene");
        }
        else
        {
            yield return ShowMessages(splitGameEndText2);
            StopCoroutine(coroutine);
            coroutine = null;
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
