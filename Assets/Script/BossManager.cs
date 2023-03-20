using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    GameObject boss;
    [SerializeField]
    Image bossBubble;
    [SerializeField]
    Text bossBubbleText;
    [SerializeField]
    Canvas gameOverCanvas;

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
	TextAsset gameOverTextAsset;
    
    [SerializeField]
    AudioSource audio_TextSound;
    [SerializeField]
    AudioSource audio_Footsteps;

    [SerializeField]
    AudioClip textSound;
    [SerializeField]
    AudioClip footsteps;

    private string loadText1;
    private string[] splitText1;
    private string loadText2;
    private string[] splitText2;
    private string loadText3;
    private string[] splitText3;
    private string loadText4;
    private string[] splitText4;
    private string loadText5;
    private string[] splitText5;
    private string loadGameOverText;
    private string[] splitGameOverText;

    public float sEVolume; 

    private int confidenceValue; 
    public static float nextSecondBossComing;
    private IEnumerator coroutine;
    private bool isBossTalking;
    private bool isFirstTalk;
    public static bool canTimePass;
    public static bool canUSBDrug;
    public static bool isUSBConnected;

    private string readingText;

    private float messageSpeed = 0.1f;

    void Awake()
    {   
        confidenceValue = 0;
        nextSecondBossComing = 0f;
        coroutine = null;
        isBossTalking = false;
        isFirstTalk = true;
        canTimePass = true;
        canUSBDrug = true;
        isUSBConnected = false;

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
        loadGameOverText = gameOverTextAsset.text;
        splitGameOverText = loadGameOverText.Split(',');

        audio_TextSound = GetComponent<AudioSource>();
        audio_Footsteps = GetComponent<AudioSource>();

    }


    void Update()
    {   
        if(canTimePass){
            nextSecondBossComing  -= Time.deltaTime;
        }
        
        if (nextSecondBossComing <= 1.7f && coroutine == null)
        {   
            //会話中
            isBossTalking = true;
        }
        
        confidenceValue = ComputerBehavior.confidenceValue;

        if (isBossTalking && coroutine == null) {
            coroutine = CreateCoroutine();        
            //コルーチンの起動
            StartCoroutine(coroutine);
            //値を初期化
            isBossTalking = false;
        }
    }

    private bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }

    private IEnumerator CreateCoroutine()
    {
        boss.gameObject.SetActive(true);
        audio_Footsteps.PlayOneShot(footsteps);
        yield return new WaitForSeconds(1.7f);
        audio_Footsteps.Stop();

        //時間を止める・USBをドラッグできなくする
        canTimePass = false;
        canUSBDrug = false;

        yield return OnAction();

        //上司・ふきだしを消す
        this.bossBubbleText.text = "";
        this.bossBubble.gameObject.SetActive(false);
        this.boss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        this.boss.gameObject.SetActive(false);

        //次の上司の来る時間を、信頼値に応じて設定
        if(isFirstTalk){
            nextSecondBossComing  = 60f;
        }
        else if(confidenceValue <= 30){
            nextSecondBossComing  = 60f;
        } else if(confidenceValue <= 75){
            nextSecondBossComing  = 90f;
        } else if(confidenceValue < 100){
            nextSecondBossComing  = 120f;
        } else{
            nextSecondBossComing  = 150f;
        }
        isFirstTalk = false;

        //ComputerBehavior.nextTimeBossComingに値を代入
        ComputerBehavior.nextTimeBossComing = MinuteHandManager.elapsedTime + nextSecondBossComing;

        //コルーチンを停止して初期化
        StopCoroutine(coroutine);
        coroutine = null;
        
        //時間を動かす・USBをドラッグできる
        canTimePass = true;
        canUSBDrug = true;
    }

    protected IEnumerator OnAction()
    {   
        //ボスを徐々に鮮明に
        StartCoroutine(BossColorChange());
        yield return BossColorChange();

        //USBが繋がっていたらゲームオーバー
        if(isUSBConnected) {
            yield return GameOverAction();
        }

        //会話を表示
        if(isFirstTalk){
            for (int i = 0; i < splitText1.Length; ++i)
            {            
                //ふきだしを表示
                bossBubble.gameObject.SetActive(true);
                StartCoroutine(ShowMessage(splitText1[i]));

                //showMessageが終わるまで待機
                yield return ShowMessage(splitText1[i]);
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }    
        } else if(confidenceValue <= 30){
            for (int i = 0; i < splitText2.Length; ++i)
            {            
                //ふきだしを表示
                bossBubble.gameObject.SetActive(true);
                StartCoroutine(ShowMessage(splitText2[i]));
                
                //showMessageが終わるまで待機
                yield return ShowMessage(splitText2[i]);
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }  
        } else if(confidenceValue <= 75 ){
            for (int i = 0; i < splitText3.Length; ++i)
            {            
                //ふきだしを表示
                bossBubble.gameObject.SetActive(true);
                StartCoroutine(ShowMessage(splitText3[i]));
                
                //showMessageが終わるまで待機
                yield return ShowMessage(splitText3[i]);
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            } 
        } else if(confidenceValue < 100 ){
            for (int i = 0; i < splitText4.Length; ++i)
            {
                //ふきだしを表示
                bossBubble.gameObject.SetActive(true);
                StartCoroutine(ShowMessage(splitText4[i]));
                
                //showMessageが終わるまで待機
                yield return ShowMessage(splitText4[i]);
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }
        } else {
            for (int i = 0; i < splitText5.Length; ++i)
            {
                //ふきだしを表示
                bossBubble.gameObject.SetActive(true);
                StartCoroutine(ShowMessage(splitText5[i]));
                
                //showMessageが終わるまで待機
                yield return ShowMessage(splitText5[i]);
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            }
        }
        //クリックするまでキー入力を待機
        yield break;
    }
    
    //上司を色づける
    protected IEnumerator BossColorChange()
    {
        for(int i=0; i<=12; i++){
            yield return new WaitForSeconds(0.03f);
            boss.GetComponent<SpriteRenderer>().color += new Color(0f, 0f, 0f, 0.05f);
        }
        boss.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        yield break;
    }

    //メッセージを表示する
    protected IEnumerator ShowMessage(string message)
    {   
        float time = 0;
        int readingTextWordNumber = 1;

        while ( true )
        {   
            yield return null;
            
            time += Time.deltaTime;

            // クリックされると一気に表示
            if ( IsClicked() ) break;
            
            int len = Mathf.FloorToInt ( time / messageSpeed);
            if (len <= message.Length) {    
                this.bossBubbleText.text = message.Substring(0, len);
                if(readingTextWordNumber == len){
                    audio_TextSound.PlayOneShot(textSound);
                    readingTextWordNumber++;
                }
            } else {
                break;
            }            
        }
        
        this.bossBubbleText.text = message;
        yield return null;
    }
    
    //ゲームオーバー処理
    private IEnumerator GameOverAction()
    {
        StartCoroutine(BossColorChange());
        yield return BossColorChange();

        for (int i = 0; i < splitGameOverText.Length; ++i)
        {
            bossBubble.gameObject.SetActive(true);
            StartCoroutine(ShowMessage(splitGameOverText[i]));
                
            yield return ShowMessage(splitGameOverText[i]);
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }

        gameOverCanvas.gameObject.SetActive(true);
        StopCoroutine(coroutine);
    }
}
