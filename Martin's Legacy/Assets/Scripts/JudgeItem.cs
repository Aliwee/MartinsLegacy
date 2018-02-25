using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeItem : MonoBehaviour {

	string item;
	//用于实时获取当前用户选中的物品
	void Update () {
		item = ItemsInteractiveManager.instance.GetPickedItem();
		GameObject.Find("JudgeCheese").GetComponent<Text>().text = item;
	}
}
