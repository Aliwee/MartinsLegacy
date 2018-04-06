using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Fungus;

/// <summary>
/// Check item manager. 处理点击放大镜之后的交互事件
/// </summary>
public class CheckItemManager : MonoBehaviour {

	private bool inChecking = false;             //if player click the magnifier
	private const string magnifierName = "item000";  
	private const string letterName = "item002";

	public Texture2D cursorTexture;              //texture for game cursor 鼠标指针
	public Fungus.Flowchart flowchart;           //Fungus Flowchart
	public GameObject pausePanel;                //pause panel 暂停菜单
	
	// Update is called once per frame
	void Update () {
		//if player click the magnifier in inventory 如果点击了物品栏中的放大镜
		if (ItemsInteractiveManager.instance.GetPickedItem() == magnifierName && !inChecking){
			Debug.Log("Log-CheckItemManager: set cursor.");
			inChecking = true;
		}

		if (inChecking == true)
			Cursor.SetCursor (cursorTexture, Vector2.zero, CursorMode.Auto);

		//if left click anything on Canvas when the game isn's paused 如果有鼠标左键事件
		if (Input.GetMouseButtonDown(0) && !pausePanel.activeSelf) {
			
			//hide the current selected item block in inventory取消选中当前的物品栏物品
			GameObject o = GameObject.Find ("/Scripts/ItemsManager");
			if (o != null){
				GameObject lastPickedItem = o.GetComponent <ItemsManager> ().lastPickedItem;
				if (lastPickedItem != null) {
					lastPickedItem.SetActive (false);
				}
			}

			//get current selected gameobject, determine whether to clear it or not 获取鼠标点击的物品,判断是否需要清空当前选中物品和待交互物品
			GameObject btn = EventSystem.current.currentSelectedGameObject;
			if (btn == null) {
				Debug.Log("Log-CheckItemManager: clear pickedItem & waitForConsumeItem.");
				clear ();
			}
			else {
				if (btn.tag != "UseableItem") {
					Debug.Log("Log-CheckItemManager: clear pickedItem & waitForConsumeItem.");
					clear ();
				}
			}

			//if player's choosen magnifier 处理点击放大镜事件
			if (inChecking == true){
				//set cursor's texture to null 取消鼠标指针样式
				Debug.Log("Log-CheckItemManager: clear cursor.");
				inChecking = false;
				Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
				clear ();

				//determine whether hit any item in inventory 判断是否点击到物品栏中的物品
				if (btn != null && btn.GetComponent <Image> () != null) {
					string itemName = btn.GetComponent <Image> ().sprite.name;
					string language = UserDataManager.instance.GetLanguage ();
					string text = LoadCSV.instance.getText (itemName, language);
					if (itemName.StartsWith("item")){
						lockCursor ();
						if (itemName == letterName) {
							showDialog2 (text);
						}
						else
							showDialog (text);
					}
				}
			}
		}
	}

	/// <summary>
	/// Clear selected item data. 清除数据
	/// </summary>
	public void clear() {
		ItemsInteractiveManager.instance.SetPickedItem ("null_picked");
		ItemsInteractiveManager.instance.SetWaitForConsumeItem ("null_consumed");
	}

	/// <summary>
	/// Get inChecking variable 获得是否点击了放大镜
	/// </summary>
	/// <returns><c>true</c>, 如果点击了放大镜, <c>false</c> 没有点击放大镜.</returns>
	public bool getInChecking() {
		return this.inChecking;
	}

	/// <summary>
	/// Set inChecking variable to false 设置是否电击了放大镜
	/// </summary>
	public void falseInChecking() {
		this.inChecking = false;
	}

	/// <summary>
	/// Show custom Fungus conversation dialog. 调用Fungus对话框之一
	/// </summary>
	/// <param name="storyText">需要加载进Say中的游戏文本.</param>
	void showDialog(string storyText) {
		Debug.Log("Log-CheckItemManager: show dailog.");
		Block block = flowchart.FindBlock ("CheckItems");
		List<Command> commands = block.CommandList;
		Say s = (Say)commands[0];
		s.SetStandardText (storyText);
		flowchart.ExecuteBlock ("CheckItems");
	}

	/// <summary>
	/// Show custom Fungus letter dialog. 调用Fungus对话框之二
	/// </summary>
	/// <param name="storyText">需要加载进Say中的游戏文本.</param>
	void showDialog2(string storyText) {
		Debug.Log("Log-CheckItemManager: show dailog.");
		Block block = flowchart.FindBlock ("CheckItems2");
		List<Command> commands = block.CommandList;
		Say s = (Say)commands[0];
		s.SetStandardText (storyText);
		flowchart.ExecuteBlock ("CheckItems2");
	}

	/// <summary>
	/// Lock cursor. 锁定鼠标指针
	/// </summary>
	 void lockCursor ()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	/// <summary>
	/// Unlock cursor. 解锁锁定的鼠标指针.
	/// </summary>
	public void unlockCheckCursor ()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
