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

	private List<InputField> inputFieldsList;
	private string[] largeInputFieldsList = new string[15];

	private string[] correctProductNumber = new string[] {"X2458FF31","X2458FF32","X2458FF33","X2458FF34","X2458FF35",
									              "A9902CF78","A9902CF79","A9902CF80","X2458DF01","X2458DF02",
									              "X2458DF03","X2458DF04","X2458DF05","X2458DF06","X2458DF07"};
	private string[] correctItemNumber = new string[] {"5","5","2","4","3",
								               		   "100","150","150","70","75",
								               		   "80","85","80","80","70"};
	private string[] correctPrice = new string[] {"20980","20980","15980","15980","10800",
	                                              "690","790","790","690","890",
	                                              "150","170","120","160","190"};
	

	public static int confidenceValue;

	void Awake()
	{
		inputFieldsList = new List<InputField>();
		//Componentを扱えるようにする
        inputFieldsList.Add(productNumber1);
		inputFieldsList.Add(itemNumber1);
		inputFieldsList.Add(price1);
        inputFieldsList.Add(productNumber2);
		inputFieldsList.Add(itemNumber2);
		inputFieldsList.Add(price2);
        inputFieldsList.Add(productNumber3);
		inputFieldsList.Add(itemNumber3);
		inputFieldsList.Add(price3);
        inputFieldsList.Add(productNumber4);
		inputFieldsList.Add(itemNumber4);
		inputFieldsList.Add(price4);
        inputFieldsList.Add(productNumber5);
		inputFieldsList.Add(itemNumber5);
		inputFieldsList.Add(price5);
        inputFieldsList.Add(productNumber6);
		inputFieldsList.Add(itemNumber6);
		inputFieldsList.Add(price6);
        inputFieldsList.Add(productNumber7);
		inputFieldsList.Add(itemNumber7);
		inputFieldsList.Add(price7);
        inputFieldsList.Add(productNumber8);
		inputFieldsList.Add(itemNumber8);
		inputFieldsList.Add(price8);
        inputFieldsList.Add(productNumber9);
		inputFieldsList.Add(itemNumber9);
		inputFieldsList.Add(price9);
        inputFieldsList.Add(productNumber10);
		inputFieldsList.Add(itemNumber10);
		inputFieldsList.Add(price10);
		inputFieldsList.Add(productNumber11);
		inputFieldsList.Add(itemNumber11);
		inputFieldsList.Add(price11);
        inputFieldsList.Add(productNumber12);
		inputFieldsList.Add(itemNumber12);
		inputFieldsList.Add(price12);
        inputFieldsList.Add(productNumber13);
		inputFieldsList.Add(itemNumber13);
		inputFieldsList.Add(price13);
        inputFieldsList.Add(productNumber14);
		inputFieldsList.Add(itemNumber14);
		inputFieldsList.Add(price14);
        inputFieldsList.Add(productNumber15);
		inputFieldsList.Add(itemNumber15);
		inputFieldsList.Add(price15);

		confidenceValue = 0;
	}
	
	//tabキーを押すと次の入力欄へ移動するメソッド
	public void TabKeyPressed()
	{
		for(int i = 0; i < 44 ; i++)
		{
			if(inputFieldsList[i].isFocused)
			{	
				inputFieldsList[i+1].Select();
			}
		}
	}	
	
	public void InputText()
	{
		//信頼値の初期化
		confidenceValue = 0;

		//製品番号・個数・価格が正しければ信頼値が上昇
		for(int i = 0; i < 15; i++)
		{	
			//小文字入力を大文字に
			largeInputFieldsList[i] = inputFieldsList[3*i].text.ToUpper();

			if(largeInputFieldsList[i] == correctProductNumber[i] && inputFieldsList[3*i + 1].text == correctItemNumber[i] && inputFieldsList[3*i + 2].text == correctPrice[i])
			{
				confidenceValue += 7;
			}
		}
	}

	void Update()
	{
		//tabキーを押すと次の入力欄へ
		if(Input.GetKeyUp(KeyCode.Tab) && !BossManager.isUSBConnected)
		{
			TabKeyPressed();
    	}
	}
}