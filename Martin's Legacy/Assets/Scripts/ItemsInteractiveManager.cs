using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//全局处理点击物品栏物品之后与场景交互的类
public class ItemsInteractiveManager : MonoBehaviour {
	private string pickedItem;                //选中的物品栏物品
	private List<string> consumableItems;     //待互动物品列表
	private string waitForConsumeItem;        //选中的场景中待交互物品
	private string isRightItem;               //物品栏物品与场景物品是否交互成功

	public static ItemsInteractiveManager instance = null;     //一个ItemsInteractiveManager实例

	//Awake总是在任何Start方法之前调用
	void Awake() {
		//判断是否已有一个实例
		if (instance == null)
			instance = this;   //如果没有，那么设置实例为该实例
		else if (instance != this)
			Destroy (gameObject);    //保证只能有一个实例

		//设置为重新加载场景时不被销毁
		DontDestroyOnLoad(gameObject);

		//初始化
		pickedItem = "null_picked";
		waitForConsumeItem = "null_consumed";
		isRightItem = "false";
		consumableItems = new List<string> ();

		//获得待互动物品列表
		GetConsumableItems ();
	}
	
	// Update is called once per frame
	void Update () {
		//判断是否是有物品交互成功
		if(pickedItem == waitForConsumeItem) {
			isRightItem = "true";
		}
		else {
			isRightItem = "false";
		}
	}
		
	//设置当前选中的物品栏物品
	public void SetPickedItem(string itemName) {
		Debug.Log("Log-ItemsInteractiveManager: set pickedItem:" + itemName);
		this.pickedItem = itemName;
	}

	//获取当前选中的物品栏物品
	public string GetPickedItem() {
		return this.pickedItem;
	}

	//设置当前选中的场景中物品
	public void SetWaitForConsumeItem(string itemName) {
		Debug.Log("Log-ItemsInteractiveManager: set waitForConsumeItem:" + itemName);
		this.waitForConsumeItem = itemName;
	}

	//获取当前选中的物品栏物品
	public string GetWaitForConsumeItem() {
		return this.waitForConsumeItem;
	}

	//获取是否交互成功
	public string GetItemInteractionResult() {
		return this.isRightItem;
	}

	//获取当前待交互物品
	void GetConsumableItems() {
		string path = "/Canvas/background/consumableItems";
		GameObject root = GameObject.Find (path);
		if (root != null) {
			foreach (Transform child in root.transform) {
				consumableItems.Add (child.gameObject.name);
			}
		}
	}

	//销毁场景中的物品
	public void DestoryConsumedItem() {
		string path = "/Canvas/background/consumableItems/" + waitForConsumeItem;
		GameObject o = GameObject.Find (path);
		if (o != null) {
			Destroy (o);
			consumableItems.Remove (waitForConsumeItem);
			waitForConsumeItem = "null_consumed";
			pickedItem = "null_picked";
		}
		AfterSuccessfulInteraction (pickedItem);
	}

	//根据物品进行交互成功之后的处理
	void AfterSuccessfulInteraction(string changeId) {
		switch (changeId) {
		default :
			break;
		}
	}
}
