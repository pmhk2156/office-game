using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Task4Manager : MonoBehaviour ,IDragHandler, IEndDragHandler
{   
    [SerializeField]
    GameObject folderContents;
    [SerializeField]
    GameObject folderContentsPanel;
    [SerializeField]
    GameObject Folder_6;
    [SerializeField]
    Text contentsText;
    [SerializeField]
    string contentsTextStr;
    private bool canDocumentDrug;
    private bool isTask4Clear;
    private Vector2 firstPosition;

    void Awake()
    {   
        canDocumentDrug = false;
        isTask4Clear = false;
        firstPosition = folderContentsPanel.transform.position;
    }
    

    public void Task4FolderClicked()
    {
        if(Folder_6.GetComponent<Task4ButtonInspector>().isFolderClicked)
        {   
            contentsTextStr = contentsText.text;
            canDocumentDrug = true;
        }
    }


    public void OnCloseButtonClick()
    {
        folderContents.gameObject.SetActive(false);
    }

    //ドラッグで書類を移動
    public void OnDrag(PointerEventData eventData)
    {   
        if(canDocumentDrug){
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

        //USBとコンピューターが重なっていれば、USBを挿入
        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("Folder")){
                isTask4Clear = true;
            }
        }

        //初期位置に戻す
        folderContentsPanel.transform.position = firstPosition;
    }
}
