using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BGFadeInOut : MonoBehaviour {


	public Image presentImage;
	public float currentTime;        //当前时间
	public float showTime = 3;       //出现时间
	public float fadeTime = 2;       //隐藏时间

	//设置图片的α通道为0
	void Start () {
		presentImage.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		//当前时间在出现时间段
		if (currentTime >= 0 && currentTime < showTime) 
		{
			presentImage.color = new Color (1, 1, 1, (currentTime / showTime));
		} 
		//当前时间在隐藏时间段
		else if (currentTime >= showTime && currentTime < showTime+fadeTime) 
		{
			presentImage.color = new Color (1, 1, 1, 1 - ((currentTime-showTime)/ fadeTime));
		}
		//否则跳转页面
		else {
			loadLoadingScene ();
		}

	}

	//加载Loading页面
	void loadLoadingScene() {
		SceneManager.LoadScene ("Loading");
	}
}
