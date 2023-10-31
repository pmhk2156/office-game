using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComputerBehavior : MonoBehaviour {
    [SerializeField]
    BossManager bossManager;
    [SerializeField]
	USBBehavior uSBBehavior;

	[SerializeField]
	InputField[] productNumberFields;
    [SerializeField]
    InputField[] itemNumberFields;
    [SerializeField]
    InputField[] priceFields;

	private string[] correctProductNumber = new string[] {"X2458FF31","X2458FF32","X2458FF33","X2458FF34","X2458FF35",
									              "A9902CF78","A9902CF79","A9902CF80","X2458DF01","X2458DF02",
									              "X2458DF03","X2458DF04","X2458DF05","X2458DF06","X2458DF07"};
	private string[] correctItemNumber = new string[] {"5","5","2","4","3",
								               		   "100","150","150","70","75",
								               		   "80","85","80","80","70"};
	private string[] correctPrice = new string[] {"20980","20980","15980","15980","10800",
	                                              "690","790","790","690","890",
	                                              "150","170","120","160","190"};

    void Update()
    {
        //tabキーを押すと次の入力欄へ
        if (Input.GetKeyUp(KeyCode.Tab) && !uSBBehavior.isUSBConnected)
        {
            TabKeyPressed();
        }
    }

    //tabキーを押すと次の入力欄へ移動するメソッド
    public void TabKeyPressed()
	{
		for　(int i = 0 ; i < productNumberFields.Length ; i++)
		{
			if　(productNumberFields[i].isFocused) { itemNumberFields[i].Select(); }
            else if (itemNumberFields[i].isFocused) { priceFields[i].Select(); }
            else if (priceFields[i].isFocused)
            {
				//OutOfBoundsを防ぐ
				if (i == productNumberFields.Length) { break; }
                productNumberFields[i+1].Select();
            }
        }
	}	
	
	public void InputText()
	{
		//信頼値の初期化
		bossManager.confidenceValue = 0;

		//製品番号・個数・価格が正しければ信頼値が上昇
		for(int i = 0 ; i < productNumberFields.Length ; i++)
		{
			//小文字入力を大文字に
			if (productNumberFields[i].isFocused) { productNumberFields[i].text.ToUpper(); }
            else if (itemNumberFields[i].isFocused) { itemNumberFields[i].text.ToUpper(); }
            else if (priceFields[i].isFocused) { priceFields[i].text.ToUpper(); }

            if (productNumberFields[i].text == correctProductNumber[i] && itemNumberFields[i].text == correctItemNumber[i] && priceFields[i].text == correctPrice[i])
			{
                bossManager.confidenceValue += 1;
			}
		}
	}
}