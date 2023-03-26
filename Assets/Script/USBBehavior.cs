using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class USBBehavior :  MonoBehaviour, IDragHandler, IEndDragHandler
{   
    [SerializeField]
    GameObject unconnectedUSB;
    [SerializeField]
    GameObject connectedUSB;

    [SerializeField]
    GameObject uSBScreenCanvas;

    private Vector2 firstPosition;
    
    public void Awake() {
        firstPosition = transform.position;
    }

    //ドラッグでUSBを移動
    public void OnDrag(PointerEventData eventData)
    {   
        if(BossManager.canUSBDrug){
            Vector3 TargetPos = Camera.main.ScreenToWorldPoint (eventData.position);
	        TargetPos.z = 0;
	        transform.position = TargetPos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        BossManager.isUSBConnected = false;
        //重なっている全てのUIをListに格納
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);

        //USBとコンピューターが重なっていれば、USBを挿入
        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.CompareTag("Image")){
                BossManager.isUSBConnected = true;
            }
        }

        if(BossManager.isUSBConnected)
        {   
            //USBCanvasをアクティブ化
		    uSBScreenCanvas.gameObject.SetActive (true);
            unconnectedUSB.SetActive(false);
            connectedUSB.SetActive(true);
        }
        else
        {   
		    uSBScreenCanvas.gameObject.SetActive (false);
            unconnectedUSB.SetActive(true);
            connectedUSB.SetActive(false);
        }

        //初期位置に戻す
        transform.position = firstPosition;
    }
}
