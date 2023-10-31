using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Task4ButtonInspector : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{   
    [SerializeField]
    GameObject folderContents;

    private int clickCount;
    private GameObject onCursorFolderLight;
    public bool isFolderClicked;

    void Awake()
    {     
        onCursorFolderLight = transform.GetChild(1).gameObject;    
        isFolderClicked = false; 
    }

    void Start()
    {   
        clickCount = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onCursorFolderLight.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        onCursorFolderLight.gameObject.SetActive(false);
    }

    public void OnClickDown()
    {
        clickCount++;
        Invoke("OnDoubleClick", 0.3f);
    }

    private void OnDoubleClick()
    {
        if(clickCount != 2) {clickCount = 0; return;}
        else {clickCount = 0;}
        
        isFolderClicked = true;
        folderContents.gameObject.SetActive(true);
        folderContents.GetComponent<Task4Manager>().Task4FolderClicked();
        isFolderClicked = false;
    }
}