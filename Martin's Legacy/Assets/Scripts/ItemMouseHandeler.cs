﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AssemblyCSharp;

/// <summary>
/// 用于控制鼠标移入时候的鼠标指针变化
/// </summary>
public class ItemMouseHandeler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{

	//是否锁定鼠标
	private bool wasLocked = false;

	//cursorTexture：使用的2D图片
	public Texture2D[] cursorTextures;

	//鼠标移入 设置鼠标样式
	public void OnPointerEnter (PointerEventData eventData)
	{
		switch (this.tag) {
		case "PickableItem":
			Cursor.SetCursor (cursorTextures [(int)CursorState.Pick], Vector2.zero, CursorMode.Auto);
			break;
		case "CheckableItem":
			Cursor.SetCursor (cursorTextures [(int)CursorState.Check], Vector2.zero, CursorMode.Auto);
			break;
		case "UseableItem":
			if (ItemsInteractiveManager.instance.GetPickedItem() != "null_picked")
				Cursor.SetCursor (cursorTextures [(int)CursorState.Use], Vector2.zero, CursorMode.Auto);
			else
				Cursor.SetCursor (cursorTextures [(int)CursorState.Question], Vector2.zero, CursorMode.Auto);
			break;
		case "CtoPItem":
			Cursor.SetCursor (cursorTextures [(int)CursorState.Check], Vector2.zero, CursorMode.Auto);
			break;
		case "WalkabelItem":
			Cursor.SetCursor (cursorTextures [(int)CursorState.Walk], Vector2.zero, CursorMode.Auto);
			break;
		default:
			break;
		}
	}

	/// <summary>
	/// 鼠标移出 设置鼠标样式为默认
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerExit (PointerEventData eventData)
	{
		Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
	}

	/// <summary>
	/// 鼠标点下之后的事件处理
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnPointerClick (PointerEventData eventData)
	{
		/*如果是可放入物品，放入物品栏
		 * 如果是有对话的物品，产生对话
        */
		if (this.tag == "PickableItem") {
			UserDataManager.instance.AddItemInPack (this.name, "use");
			Destroy (this.gameObject);
			Cursor.SetCursor (null, Vector2.zero, CursorMode.Auto);
		} else if (this.tag == "CheckableItem") {
			lockCursor ();
		} else if (this.tag == "UseableItem") {
			//设置当前选中的物品标志到ItemsInteractiveManager中去
			ItemsInteractiveManager.instance.SetWaitForConsumeItem (this.name);
			//取消选中当前的物品栏物品
			GameObject o = GameObject.Find ("/Scripts/ItemsManager");
			GameObject lastPickedItem = o.GetComponent <ItemsManager> ().lastPickedItem;
			if (lastPickedItem != null)
				lastPickedItem.SetActive (false);
		} else if (this.tag == "CtoPItem") {
			lockCursor ();
		}
	}

	/// <summary>
	/// 锁定鼠标
	/// </summary>
	void lockCursor ()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
		wasLocked = true;
	}

	/// <summary>
	/// 解锁鼠标
	/// </summary>
	public void unlockCursor ()
	{
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
		wasLocked = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape) && wasLocked) {
			unlockCursor ();
		}
	}

	/// <summary>
	/// 改变CtoPItem的tag
	/// </summary>
	public void changeTag ()
	{
		this.tag = "PickableItem";
	}

	/// <summary>
	/// 获得点击的CtoPItem的tag
	/// </summary>
	/// <returns>The tag.</returns>
	public string getTag ()
	{
		return this.tag;
	}
}
