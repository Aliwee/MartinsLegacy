using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemsInteractiveManager : MonoBehaviour {
	private List<Item> itemsInPack;           //用户存档中物品栏
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

	//Awake总是在任何Start方法之前调用
	void Start() {
		itemsInPack = UserDataManager.instance.GetItemsInPack ();
	}
	
	// Update is called once per frame
	void Update () {
		//加载物品栏
		itemsInPack = UserDataManager.instance.GetItemsInPack ();
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
		this.pickedItem = itemName;
	}

	//获取当前选中的物品栏物品
	public string GetPickedItem() {
		return this.pickedItem;
	}

	//设置当前选中的场景中物品
	public void SetWaitForConsumeItem(string itemName) {
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
		string path = "/Canvas/consumableItems";
		GameObject root = GameObject.Find (path);
		if (root != null) {
			foreach (Transform child in root.transform) {
				consumableItems.Add (child.gameObject.name);
			}
		}
	}

	//销毁场景中的物品
	public void DestoryConsumedItem() {
		string path = "/Canvas/consumableItems/" + waitForConsumeItem;
		GameObject o = GameObject.Find (path);
		if (o != null) {
			Destroy (o);
			consumableItems.Remove (waitForConsumeItem);
			waitForConsumeItem = "null_consumed";
		}
		AfterSuccessfulInteraction (pickedItem);
	}

	//根据物品进行交互成功之后的处理
	void AfterSuccessfulInteraction(string changeId) {
		switch (changeId) {
		case "item002":
			//跳转至开始界面
			SceneManager.LoadScene ("Chapter-1-Level-5");
			break;
		default :
			break;
		}
	}

	//仅销毁物品栏中的物品
	public void DestoryPickedItems() {
		//销毁物品栏物品
		for (int i = 0; i < itemsInPack.Count; i++) {
			Debug.Log (pickedItem);
			if (itemsInPack [i].name == pickedItem)
				UserDataManager.instance.RemoveItemInPack (itemsInPack [i]);
		}
		UserDataManager.instance.AddItemInConsume (pickedItem, "consumed");
	}
}
