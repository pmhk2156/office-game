using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneBehavior : MonoBehaviour
{
    [SerializeField]
    GameObject phone;
    [SerializeField]
    Image phoneBubble;
    [SerializeField]
    Text phoneBubbleText;

    [SerializeField]
	private TextAsset textAsset;
    private string loadText;
    private string[] splitText;

    private float nextTimePhoneRing;
    private IEnumerator coroutine;
    private bool isPhoneRing = false;
    private bool canMouseClick = true;


    // セリフ : Unityのインスペクタ(UI上)で会話文を定義する 
　　// （次項 : インスペクタでscriptを追加して、設定をする で説明）
    private string readingText;

    [SerializeField]
    Image  mySpeechBubble;
    [SerializeField]
    private List<string> choices;
    private int inputKeyTimes;
    private int currentCursorNum;

    void Awake()
    {
        nextTimePhoneRing = 5f;
        inputKeyTimes = 0;
        currentCursorNum = 0;

        loadText = textAsset.text;
        splitText = loadText.Split(char.Parse("\n"));
    }

    void Update()
    {
        //一定時間後に電話が鳴る
        nextTimePhoneRing -=Time.deltaTime;

        if (nextTimePhoneRing <= 0.0f && coroutine == null)
        {
            isPhoneRing = true;
            phone.GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (isPhoneRing == true && coroutine == null && Input.GetMouseButtonDown(0)) {
            coroutine = CreateCoroutine();
            //コルーチンの起動
            StartCoroutine(coroutine);
            //電話機の色変更
            phone.GetComponent<SpriteRenderer>().color = Color.white;
            isPhoneRing = false;
        }
    }

    private IEnumerator CreateCoroutine()
    {
        phoneBubble.gameObject.SetActive(true);

        yield return OnAction();

        //ふきだしを消す
        this.phoneBubbleText.text = "";
        this.phoneBubble.gameObject.SetActive(false);

        //コルーチンを停止して初期化
        StopCoroutine(coroutine);
        coroutine = null;
    }

    protected IEnumerator OnAction()
    {
        for (int i = 0; i < splitText.Length; ++i)
        {
            yield return null; 
            
            // 会話を表示
            StartCoroutine(showMessage(splitText[i]));
            //showMessageが終わるまで待機
            yield return showMessage(splitText[i]);
            //クリックするまでキー入力を待機
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));            
        }
        
        //電話が鳴るまでの時間をランダムに代入
        nextTimePhoneRing  = Random.Range (10f, 20f);  
        yield break;
    }
    
    //メッセージを表示する
    protected IEnumerator showMessage(string message)
    {
        //メッセージを一文字ずつ表示
        for(int i = 0; i <= message.Length; ++i)
        {   
            readingText = message.Substring(0, i);
            this.phoneBubbleText.text = readingText;

            //一文字ごとに0.1秒待機
            yield return new WaitForSeconds(0.1f);

            if(Input.GetMouseButton(0) && canMouseClick == true)
            {
                this.phoneBubbleText.text = message;
                canMouseClick = false;
                Debug.Log("bbb");
                yield break; 
            }          
            
        }
        yield break;        
    }
    
    private void choiceOption ()
    {
        
        mySpeechBubble.gameObject.SetActive(true);

        //下を押したらカーソルが下に
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            inputKeyTimes += 1;
        }
        //上を押したらカーソルが上に
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            inputKeyTimes -= 1;
        }

        //最下部で下矢印を押すと一番上に、最上部も同様
        currentCursorNum = inputKeyTimes % choices.Count;
    }
}