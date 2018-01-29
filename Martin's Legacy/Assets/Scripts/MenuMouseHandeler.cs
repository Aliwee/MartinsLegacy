using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
