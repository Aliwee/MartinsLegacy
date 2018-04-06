using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//用于管理物品栏的类
public class ItemsManager : MonoBehaviour {

	private List<Item> itemsInPack;              //用户存档中物品栏
	private List<Item> consumedItems;            //用户存档中已消耗物品
	private string itemsPath;                    //物品资源路径
	private int levelItemsNum;                   //用户存档物品数
	private string interactionSuccess;           //交互成功
	private const string magnifierName = "item000";

	public Button[] itemTiles;                   //物品栏中的物品窗格
	public GameObject lastPickedItem;            //上一次选中的物品

	//Awake总是在任何Start方法之前调用
	void Start() {
		//初始化
		itemsInPack = UserDataManager.instance.GetItemsInPack ();
		consumedItems = UserDataManager.instance.GetConsumedItems ();
		itemsPath = "LevelItems/";
		int item_length = itemTiles.Length;
		for (int i = 0; i < item_length; i++) {
			itemTiles [i].onClick.AddListener (ItemClick);
		}

		//隐藏场景中已经获得的物品
		HideItemsInScene();

		//加载物品栏
		LoadItemsInPack ();

	}

	//每帧调用
	void Update() {
		//加载物品栏
		itemsInPack = UserDataManager.instance.GetItemsInPack ();
		consumedItems = UserDataManager.instance.GetConsumedItems ();
		LoadItemsInPack ();
		//交互成功事件是否触发
		interactionSuccess = ItemsInteractiveManager.instance.GetItemInteractionResult ();
		if (interactionSuccess == "true") {
			DestoryItems ();
			interactionSuccess = "false";
		}
	}

	//加载物品栏
	void LoadItemsInPack() {
		int item_length = itemsInPack.Count;
		for (int i = 0; i < itemTiles.Length; i++) {
			if (i < item_length) {
				//取得物品索引名字
				string name = itemsInPack[i].name;
				//取得路径
				string path = itemsPath + name;
				//取得索引下的资源
				Sprite imageSourceSprite = new Sprite();
				imageSourceSprite = Resources.Load (path, imageSourceSprite.GetType ()) as Sprite;
				//绑定资源
				itemTiles[i].GetComponent <Image> ().sprite = imageSourceSprite;
			} else {
				//取得路径
				string path = "General/transparent";
				//取得索引下的资源
				Sprite imageSourceSprite = new Sprite();
				imageSourceSprite = Resources.Load (path, imageSourceSprite.GetType ()) as Sprite;
				//绑定资源
				itemTiles[i].GetComponent <Image> ().sprite = imageSourceSprite;
			}
		}
	}

	//隐藏场景中的物品
	void HideItemsInScene() {
		for (int i = consumedItems.Count-1; i >= 0; i--) {
			//取得物品索引名字
			string name = consumedItems[i].name;
			string path1 = "/Canvas/interactableItems/" + name;
			string path2 = "/Canvas/consumableItems/" + name;
			//隐藏已获得在物品栏中的图标
			GameObject o = GameObject.Find (path1);
			if (o != null) {
				Destroy (o);
			}
			//隐藏在场景中的物品
			o = GameObject.Find (path2);
			if (o != null) {
				Destroy (o);
			}
		}
		for (int i = itemsInPack.Count-1; i >= 0; i--) {
			//取得物品索引名字
			string name = itemsInPack[i].name;
			string path = "/Canvas/interactableItems/" + name;
			//隐藏已获得在物品栏中的图标
			GameObject o = GameObject.Find (path);
			if (o != null) {
				o.SetActive (false);
			}
		}

	}

	//物品点击按钮
	void ItemClick() {
		//获取该物品的名称
		GameObject btn = EventSystem.current.currentSelectedGameObject;
		string itemName = btn.GetComponent <Image> ().sprite.name;

		//显示选框
		if(itemName != "transparent" && itemName != magnifierName) {
			if (lastPickedItem != null)
				lastPickedItem.SetActive (false);
			GameObject itemBack = btn.transform.GetChild (0).gameObject;
			itemBack.SetActive (true);
			lastPickedItem = itemBack;
		}

		//设置当前选中的物品标志到ItemsInteractiveManager中去
		ItemsInteractiveManager.instance.SetWaitForConsumeItem ("null_consumed");
		ItemsInteractiveManager.instance.SetPickedItem (itemName);
	}
		
	//销毁交互成功的物品
	void DestoryItems() {
		//获取交互成功的物品栏物品和场景中物品
		string pickedItemName = ItemsInteractiveManager.instance.GetPickedItem ();
		//销毁物品栏物品
		for (int i = 0; i < itemsInPack.Count; i++) {
			if (itemsInPack [i].name == pickedItemName)
				UserDataManager.instance.RemoveItemInPack (itemsInPack [i]);
		}
		UserDataManager.instance.AddItemInConsume (pickedItemName, "consumed");
		//销毁场景物品
		ItemsInteractiveManager.instance.DestoryConsumedItem ();
		//鼠标重设为默认
		Cursor.SetCursor(null, Vector2.zero,CursorMode.Auto);
	}

	//仅销毁物品栏中的物品
	public void DestoryPickedItems() {
		//获取交互成功的物品栏物品和场景中物品
		string pickedItemName = ItemsInteractiveManager.instance.GetPickedItem ();
		//销毁物品栏物品
		for (int i = 0; i < itemsInPack.Count; i++) {
			if (itemsInPack [i].name == pickedItemName)
				UserDataManager.instance.RemoveItemInPack (itemsInPack [i]);
		}
		UserDataManager.instance.AddItemInConsume (pickedItemName, "consumed");
		//鼠标重设为默认
		Cursor.SetCursor(null, Vector2.zero,CursorMode.Auto);
	}
}
