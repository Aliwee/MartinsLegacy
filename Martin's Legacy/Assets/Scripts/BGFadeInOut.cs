using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Background fade in out.
/// </summary>
public class BGFadeInOut : MonoBehaviour {

	private string levelName;

	public Image presentImage;
	public float currentTime;    //current game time 当前时间
	public float showTime;       //time to show the image 出现时间
	public float fadeTime;       //time to hide the image 隐藏时间

	/// <summary>
	/// Set the alpha channel of the image to 0 设置图片的通道为0
	/// </summary>
	void Start () {
		presentImage.color = new Color(1, 1, 1, 0);
		//获取当前场景名
		levelName = SceneManager.GetActiveScene ().name;
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		//When current time is in image-showing time 当前时间在出现时间段
		if (currentTime >= 0 && currentTime < showTime) 
		{
			presentImage.color = new Color (1, 1, 1, (currentTime / showTime));
		} 
		//When current time is in image-fading time当前时间在隐藏时间段
		else if (currentTime >= showTime && currentTime < showTime+fadeTime) 
		{
			presentImage.color = new Color (1, 1, 1, 1 - ((currentTime-showTime)/ fadeTime));
		}
		//Load next scene 否则跳转页面
		else {
			ChooseSceneToLoad (levelName);
		}
	}

	/// <summary>
	/// Chooses the scene to load. 根据当前场景选择下一个场景
	/// </summary>
	/// <param name="levelName">Level name.</param>
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

	/// <summary>
	/// Loads the "Loading" scene. 加载Loading页面
	/// </summary>
	void loadLoadingScene() {
		SceneManager.LoadScene ("Loading");
	}

	/// <summary>
	/// Loads the "start" scene. 加载Start页面
	/// </summary>
	void LoadStartScene() {
		SceneManager.LoadScene ("Start");
	}
}
