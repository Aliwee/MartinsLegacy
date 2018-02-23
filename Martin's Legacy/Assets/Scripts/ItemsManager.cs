using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ItemsManager : MonoBehaviour {

	private List<Item> itemsInPack;              //用户存档中物品栏
	private List<Item> consumedItems;            //用户存档中已消耗物品
	private string itemsPath;                    //物品资源路径
	private int levelItemsNum;                   //用户存档物品数
	private string interactionSuccess;           //交互成功

	public Button[] itemTiles;                   //物品栏中的物品窗格
	public Button[] clickTiles;                  //拆解互动按钮窗格
	public Button[] consumeTiles;                //待互动物品列表
	public Button exitBtn;                       //退出按钮
	public GameObject letter;                    //显示纸质内容的面板
	public GameObject shadow;                    //显示纸质内容的阴影
	public Button backBtn;                       //取消显示纸质内容按钮

	//Awake总是在任何Start方法之前调用
	void Start() {
		//初始化
		itemsInPack = UserDataManager.instance.GetItemsInPack ();
		consumedItems = UserDataManager.instance.GetConsumedItems ();
		itemsPath = "LevelItems/";
		int item_length = itemTiles.Length;
		int consume_length = consumeTiles.Length;
		for (int i = 0; i < item_length; i++) {
			itemTiles [i].onClick.AddListener (ItemClick);
			clickTiles [i].onClick.AddListener (UseItemClick);
		}
		for (int i = 0; i< consume_length; i++) {
			consumeTiles [i].onClick.AddListener (ConsumeItemClick);
		}
		exitBtn.onClick.AddListener (ExitClick);
		backBtn.onClick.AddListener (BackClick);

		//隐藏场景中已经获得的物品
		HideItemsInScene();

		//加载物品栏
		LoadItemsInPack ();

	}

	//每帧调用
	void Update() {
		//加载物品栏
		itemsInPack = UserDataManager.instance.GetItemsInPack ();
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

				//取得物品的属性
				string tag = itemsInPack[i].tag;
				//如果是可拆解物品还需要拆解按钮显示
				if (tag == "unravel") {
					clickTiles [i].gameObject.SetActive (true);
				}
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
		for (int i = 0; i < consumedItems.Count; i++) {
			//取得物品索引名字
			string name = consumedItems[i].name;
			string path1 = "/Canvas/interactableItems/" + name;
			string path2 = "/Canvas/consumableItems/" + name;
			//隐藏已获得在物品栏中的图标
			GameObject o = GameObject.Find (path1);
			if (o != null) {
				o.SetActive (false);
			}
			//隐藏在场景中的物品
			o = GameObject.Find (path2);
			if (o != null) {
				o.SetActive (false);
			}
		}
		for (int i = 0; i < itemsInPack.Count; i++) {
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

	//退出按钮
	void ExitClick() {
		//保存用户当前物品栏中的物品
		UserDataManager.instance.UpdateItemsAndLevelData ();
		//跳转至开始界面
		SceneManager.LoadScene ("Start");
	}

	//物品点击按钮
	void ItemClick() {
		//获取该物品的名称
		GameObject btn = EventSystem.current.currentSelectedGameObject;
		string itemName = btn.GetComponent <Image> ().sprite.name;

		//设置当前选中的物品标志到ItemsInteractiveManager中去
		ItemsInteractiveManager.instance.SetPickedItem (itemName);

		//读信件
		if (itemName == "item003") {
			//取得路径
			string path = itemsPath + itemName;
			//取得索引下的资源
			Sprite imageSourceSprite = new Sprite();
			imageSourceSprite = Resources.Load (path, imageSourceSprite.GetType ()) as Sprite;
			//绑定资源
			letter.GetComponent<Image >().sprite = imageSourceSprite;
			//显示
			letter.SetActive (true);
			shadow.SetActive (true);
			backBtn.gameObject.SetActive (true);
		}		
	}

	//取消显示纸质内容按钮
	void BackClick() {
		letter.SetActive (false);
		shadow.SetActive (false);
		backBtn.gameObject.SetActive (false);
	}

	//拆解物品按钮
	void UseItemClick() {
		//获取物品的名称
		GameObject btn = EventSystem.current.currentSelectedGameObject;
		GameObject itemImage = btn.transform.parent.GetChild (0).gameObject;
		string itemName = itemImage.GetComponent <Image> ().sprite.name;
		//在itemsInPack中找到它并删除它
		for (int i = 0; i < itemsInPack.Count; i++) {
			Item item = itemsInPack [i];
			if (item.name == itemName) {
				UserDataManager.instance.RemoveItemInPack (item);
				UserDataManager.instance.AddItemInConsume (itemName, "ravel");
				break;
			}
		}
		//得到分割物品名之后的多个子物品名
		string[] nameArray = itemName.Split (new char[] { '-' });
		for (int i = 0; i < nameArray.Length; i++) {
			string childItemName = nameArray [i];
			UserDataManager.instance.AddItemInPack (childItemName, "use");
		}
		//交互按钮重设为设为不可见
		btn.SetActive (false);
	}

	//消耗物品按钮
	void ConsumeItemClick() {
		//获取物品的名称
		GameObject btn = EventSystem.current.currentSelectedGameObject;
		string consumeItemName = btn.name;
		//设置当前选中的物品标志到ItemsInteractiveManager中去
		ItemsInteractiveManager.instance.SetWaitForConsumeItem (consumeItemName);
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
		Cursor.SetCursor(null, Vector2.zero,CursorMode.Auto);
	}
}
