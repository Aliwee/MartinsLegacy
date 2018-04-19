using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeItem : MonoBehaviour {

	string judgeCheese;
	int judgeItem;

	void Update () {
		
		//用于实时获取当前用户选中的物品
		judgeCheese = ItemsInteractiveManager.instance.GetPickedItem();
		GameObject.Find("JudgeCheese").GetComponent<Text>().text = judgeCheese;

		//用于判断用户是否已经使用item003
		judgeItem = UserDataManager.instance.findItemInConsume();
		if(judgeItem > 0)
			GameObject.Find("IfCheeseUsed").GetComponent<Text>().text  = "true";
		else
			GameObject.Find("IfCheeseUsed").GetComponent<Text>().text  = "false";
	}
}
