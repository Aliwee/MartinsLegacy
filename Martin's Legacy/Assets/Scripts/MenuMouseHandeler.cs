using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//用于处理开始界面鼠标移入移出效果
public class MenuMouseHandeler : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler {

	public GameObject mouseEnterBtn;    //鼠标移入按钮
	
	// 鼠标移入
	public void OnPointerEnter (PointerEventData eventData) {
		mouseEnterBtn.SetActive (true);
	}

	//鼠标移出
	public void OnPointerExit (PointerEventData eventData) {
		mouseEnterBtn.SetActive (false);
	}
}
