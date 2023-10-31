using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Task4Manager : MonoBehaviour ,IDragHandler, IEndDragHandler
{
    [SerializeField]
    USBScreenManager uSBScreenManager;

    [SerializeField]
    GameObject folderContents;
    [SerializeField]
    GameObject folderContentsPanel;
    [SerializeField]
    GameObject Folder_6;
    [SerializeField]
    Text contentsText;
    [SerializeField]
    AudioSource audioSourse;
    [SerializeField]
    AudioClip errorSound;
    public GameObject uSBTask4Panel;

    private bool canDocumentDrug;
    private bool isFirstFolderClicked;
    public bool isTask4Clear;
    public bool isErrorSounding;
    private Vector2 firstPosition;

    void Awake()
    {   
        canDocumentDrug = false;
        isTask4Clear = false;
        isFirstFolderClicked = true;
        isErrorSounding = false;
        firstPosition = folderContentsPanel.transform.position;
    }

    public void OnButton4Clicked()
    {
        if (uSBScreenManager.canButtonPush && uSBScreenManager.taskProcess >= 3)
        {
            //初めてのボタンクリックでメッセージが表示
            if (uSBScreenManager.isFirstPushUSBButton4)
            {
                StartCoroutine(uSBScreenManager.ShowMessages(uSBScreenManager.buttonClickText4));
                uSBScreenManager.isFirstPushUSBButton4 = false;
            }

            uSBScreenManager.uSBMenuPanel.gameObject.SetActive(false);
            uSBTask4Panel.gameObject.SetActive(true);
        }
    }

    public void Task4FolderClicked()
    {
        if(Folder_6.GetComponent<Task4ButtonInspector>().isFolderClicked)
        {   
            folderContentsPanel.gameObject.SetActive(true);
            canDocumentDrug = true;
            if (isFirstFolderClicked)
            {
                audioSourse.PlayOneShot(errorSound);
                isErrorSounding = true;
                isFirstFolderClicked = false;
            }    
        }
        else
        {
            if(folderContentsPanel.activeSelf)
            {
                folderContentsPanel.gameObject.SetActive(false);
            }
        }
    }


    public void OnCloseButtonClick()
    {
        folderContents.gameObject.SetActive(false);
    }

    //ドラッグで書類を移動
    public void OnDrag(PointerEventData eventData)
    {   
        if(canDocumentDrug)
        {
            Vector3 TargetPos = Camera.main.ScreenToWorldPoint (eventData.position);
	        TargetPos.z = 0;
	        folderContentsPanel.transform.position = TargetPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //重なっている全てのUIをListに格納
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        //USBにフォルダーをドラッグしてクリア
        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("Folder"))
            {
                isTask4Clear = true;
            }
        }

        //初期位置に戻す
        folderContentsPanel.transform.position = firstPosition;
    }
}
