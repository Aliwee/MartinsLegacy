using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemMouseHandeler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler{

	//cursorTexture：使用的2D图片 
	public Texture2D cursorTexture;

	//鼠标移入 设置鼠标样式
	public void OnPointerEnter (PointerEventData eventData) {
		Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);  
	}

	//鼠标移出 设置鼠标样式为默认
	public void OnPointerExit (PointerEventData eventData) {
		Cursor.SetCursor(null, Vector2.zero,CursorMode.Auto);
	}

	//鼠标点下
	public void OnPointerClick (PointerEventData eventData) {
		/*如果是可放入物品，放入物品栏
		 * 如果是有对话的物品，产生对话
        */
		if (this.tag == "PickableItem") {
			UserDataManager.instance.AddItemInPack (this.name, "use");
			this.gameObject.SetActive (false);
			Cursor.SetCursor(null, Vector2.zero,CursorMode.Auto);
		}
		else if (this.tag == "UnravelableItem") {
			UserDataManager.instance.AddItemInPack (this.name, "unravel");
			this.gameObject.SetActive (false);
			Cursor.SetCursor(null, Vector2.zero,CursorMode.Auto);
		}
		else if (this.tag == "CheckableItem") {
			//TODO:对话
		}
		else if (this.tag == "UseableItem") {
			//TODO:对话
			//设置当前选中的物品标志到ItemsInteractiveManager中去
			ItemsInteractiveManager.instance.SetWaitForConsumeItem (this.name);
			//取消选中当前的物品栏物品
			GameObject o = GameObject.Find("/Scripts/ItemsManager");
			GameObject lastPickedItem = o.GetComponent <ItemsManager> ().lastPickedItem;
			if (lastPickedItem != null)
				lastPickedItem.SetActive (false);
		}
	}
}
