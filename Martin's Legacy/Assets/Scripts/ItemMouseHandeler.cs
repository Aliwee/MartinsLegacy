using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemMouseHandeler :  MonoBehaviour,IPointerEnterHandler,IPointerExitHandler{

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
}

