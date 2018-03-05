using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BGFadeInOut : MonoBehaviour {

	private string levelName;

	public Image presentImage;
	public float currentTime;    //当前时间
	public float showTime;       //出现时间
	public float fadeTime;       //隐藏时间

	//设置图片的α通道为0
	void Start () {
		presentImage.color = new Color(1, 1, 1, 0);
		//获取当前场景名
		levelName = SceneManager.GetActiveScene ().name;
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
			ChooseSceneToLoad (levelName);
		}
	}

	//根据当前场景选择下一个场景
	void ChooseSceneToLoad(string levelName) {
		switch (levelName) {
		case "Presents":
			loadLoadingScene ();
			break;
		case "Thanks":
			LoadStartScene ();
			break;
		default :
			break;
		}
	}
	//加载Loading页面
	void loadLoadingScene() {
		SceneManager.LoadScene ("Loading");
	}

	//加载Thanks页面
	void LoadStartScene() {
		SceneManager.LoadScene ("Start");
	}
}
