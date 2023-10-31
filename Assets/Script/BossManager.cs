using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    USBBehavior uSBBehavior;
    [SerializeField]
    TimeManager timeManager;
    [SerializeField]
    Task4Manager task4Manager;
    [SerializeField]
    USBScreenManager uSBScreenManager;

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
    
    AudioSource[] sEs;

    [SerializeField]
    AudioClip textSound;
    [SerializeField]
    AudioClip footsteps;

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
    private string[] splitGameOverText;
    private string[] splitGameEndText1;
    private string[] splitGameEndText2;
    

    public float bGMVolume; 
    public float sEVolume;

    private float timeToBeginFootsteps;
    private float secondToShiftBossComing;
    private float footstepsTime;

    private IEnumerator coroutine;
    private bool isFirstTalk;
    public bool isSecondTalk { get; set; }
    public int confidenceValue { get; set; }

    //0はまだイベントを見ていない状態、1はフラグが立った状態、2以上は既にイベントを見た状態。
    private int text4Flag;

    private float messageSpeed = 0.1f;

    void Awake()
    {   
        timeToBeginFootsteps = 0f;
        coroutine = null;
        isFirstTalk = true;
        isSecondTalk = true;
        confidenceValue = 0;
        text4Flag = 0;

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
        splitGameOverText = loadGameOverText.Split(',');
        loadGameEndText1 = gameEndText1Asset.text;
        splitGameEndText1 = loadGameEndText1.Split(',');
        loadGameEndText2 = gameEndText2Asset.text;
        splitGameEndText2 = loadGameEndText2.Split(',');

        audio_BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        sEs = GetComponents<AudioSource>();
        audio_TextSound = sEs[0];
        audio_Footsteps = sEs[1];
    }


    void Update()
    {   
        if(timeManager.elapsedTime >= timeToBeginFootsteps && coroutine == null )
        {
            coroutine = BossCoroutine();
            //コルーチンの起動
            StartCoroutine(coroutine);
        }

        //エラー音が鳴り響いた時
        if(task4Manager.isErrorSounding && coroutine == null)
        {
            coroutine = BossCoroutine();
            //コルーチンの起動
            StartCoroutine(coroutine);
        }
    }

    private bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    //時間を止める・USBをドラッグできなくする
    private void StopGameWhenTalking()
    {
        timeManager.canTimePass = false;
        uSBBehavior.canUSBDrug = false;
        cannotInputObject.gameObject.SetActive(true);
        uSBScreenManager.canButtonPush = false;
    }

    
    private void StartGameAfterTalking()
    {
        task4Manager.isErrorSounding = false;

        //BGMの音量を元に戻す
        audio_BGM.volume = bGMVolume;

        //時間を動かす・USBをドラッグできる・入力可能にする
        timeManager.canTimePass = true;
        uSBBehavior.canUSBDrug = true;
        cannotInputObject.gameObject.SetActive(false);
        uSBScreenManager.canButtonPush = true;

        //上司とふきだしを初期化・非アクティブに
        this.bossBubbleText.text = "";
        this.boss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        this.bossBubble.gameObject.SetActive(false);
        this.boss.gameObject.SetActive(false);
    }

    private IEnumerator BossCoroutine()
    {
        if (task4Manager.isErrorSounding)
        {
            yield return new WaitForSeconds(1f);
            yield return SetFootstepsAudio(2f);
        }
        else if (!isFirstTalk) { yield return SetFootstepsAudio(footstepsTime); }

        StopGameWhenTalking();

        yield return ChangeBossColor();

        yield return Talk();

        if (!task4Manager.isErrorSounding) { timeToBeginFootsteps = SetVariable_TimeToBeginFootsteps(confidenceValue); }

        StartGameAfterTalking();

        //コルーチンを停止して初期化
        StopCoroutine(coroutine);
        coroutine = null;        
    }

    public float SetVariable_TimeToBeginFootsteps(int value)
    {
        float second;       

        //次の上司の来る時間を、信頼値に応じて設定
        if (isSecondTalk) { second = 60f; }
        else if (text4Flag == 1 && value > 7) { second = 0f; }
        else if (value < 2) { second = 30f; }
        else if (value < 4) { second = 90f; }
        else if (value < 10) { second = 120f; }
        else if (value < 14) { second = 150f; }
        else { second = 180f; }

        //0または30が常に基点になるよう、前の値を引く
        if (!isFirstTalk) { second -= secondToShiftBossComing; }

        //上司の来る時間が±5秒ランダムに変わる
        if (text4Flag != 1) { secondToShiftBossComing = Random.Range(0f, 10f) - 5f; }
        //パスワードを伝えに来る時だけ、4秒で固定
        else { secondToShiftBossComing = 4f; }

        //足音の鳴る時間がランダムに変わる
        if (text4Flag != 1) { footstepsTime = Random.Range(2f, 8.5f); }
        //パスワードを伝えに来る時だけ2秒で固定
        else { footstepsTime = 2; }

        //次の上司の来る時間が17時を超える際、17時に来るように設定
        if (timeToBeginFootsteps >= 530) { return 540; }
        else { return timeManager.elapsedTime + second + secondToShiftBossComing - footstepsTime; }
    }

    protected IEnumerator Talk()
    {
        bossBubble.gameObject.SetActive(true);

        //ゲームオーバー
        if (uSBBehavior.isUSBConnected) { yield return GameOverCoroutine(); }
        //ノーマルエンド(時間切れ)
        if (timeManager.elapsedTime >= 535) { yield return NomalEndCoroutine(); }
        
        //会話を表示
        if (isFirstTalk)
        {
            yield return ShowMessages(splitText1);
            isFirstTalk = false;
        }
        else if (isSecondTalk)
        {
            yield return ShowMessages(splitText7);
            isSecondTalk = false;
        }
        else if (text4Flag == 1)
        {
            yield return ShowMessages(splitText6);
            text4Flag++;
        }
        else if (confidenceValue < 4) { yield return ShowMessages(splitText2); }
        else if (confidenceValue < 7) { yield return ShowMessages(splitText3); }
        else if (confidenceValue < 14) { yield return ShowMessages(splitText4); }
        else { yield return ShowMessages(splitText5); }

        bossBubble.gameObject.SetActive(false);

        yield break;
    }

    protected IEnumerator SetFootstepsAudio(float time)
    {       
        audio_Footsteps.PlayOneShot(footsteps);
        for(int i = 0; i < 4; i++)
        {
            audio_BGM.volume -= 0.2f * bGMVolume;
            yield return new WaitForSeconds(0.5f);
        }  
        yield return new WaitForSeconds(time - 2f);
        audio_Footsteps.Stop();
    }
    
    protected IEnumerator ChangeBossColor()
    {
        for (int i = 0; i <= 12; i++)
        {
            yield return new WaitForSeconds(0.03f);
            boss.GetComponent<SpriteRenderer>().color += new Color(0f, 0f, 0f, 0.05f);
        }
        boss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield break;
    }
    
    protected IEnumerator ShowMessages(string[] message)
    {   
		//メッセージ送り処理
		for (int i = 0; i < message.Length; ++i)
        {            
			float time = 0;
        	int readingTextWordNumber = 1;

        	while ( true )
            {
            	time += Time.deltaTime;
                // クリックされると一気に表示
                if (IsClicked()) { break; }

            	//0.1秒ごとにメッセージが一文字ずつ表示
            	int len = Mathf.FloorToInt ( time / messageSpeed );
            	if (len <= message[i].Length)
                {    
					//メッセージの表示
                	this.bossBubbleText.text = message[i].Substring(0, len);
					
					//一文字表示させるごとに音声が鳴る
                	if(readingTextWordNumber == len)
                    {
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
		
        	//次のクリックまで待機
        	yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
		}  
	}
    
    //ゲームオーバー処理
    private IEnumerator GameOverCoroutine()
    {
        yield return ChangeBossColor();
        yield return ShowMessages(splitGameOverText);
        
        StopCoroutine(coroutine);
        coroutine = null;
        SceneManager.LoadScene("GameOverScene");
    }

    protected IEnumerator NomalEndCoroutine()
    {
        yield return ChangeBossColor();
        
        if(confidenceValue >= 105)
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
