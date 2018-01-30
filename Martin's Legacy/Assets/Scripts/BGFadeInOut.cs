
//苑靖怡 2018-1-30 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BGFadeInOut : MonoBehaviour {

	//currentTime记录当前时间、showTime记录出现时间、fadeTime记录隐藏时间
	public Image presentImage;
	public float currentTime;
	public float showTime = 3;
	public float fadeTime = 2;
	//private bool show = true ;

	//初始化设置界面α通道为0
	void Start () {
		presentImage.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		//若当前时间在出现时间段
		if (currentTime >= 0 && currentTime < showTime) 
		{
			presentImage.color = new Color (1, 1, 1, (currentTime / showTime));
		} 
		//若当前时间在隐藏时间段
		else if (currentTime >= showTime && currentTime < showTime+fadeTime) 
		{
			presentImage.color = new Color (1, 1, 1, 1 - ((currentTime-showTime)/ fadeTime));
		}
		//否则跳转页面
		else {
			loadLoadingScene ();
		}
	}

	//加载loading页面
	void loadLoadingScene() {
		SceneManager.LoadScene ("Loading");
	}
}
