using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BGFadeInOut : MonoBehaviour {

	public Image presentImage;
	public float currentTime;
	public float showTime = 3;
	public float fadeTime = 2;
	//private bool show = true ;

	// Use this for initialization
	void Start () {
		presentImage.color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		if (currentTime >= 0 && currentTime < showTime) 
		{
			presentImage.color = new Color (1, 1, 1, (currentTime / showTime));
		} 
		else if (currentTime >= showTime && currentTime < showTime+fadeTime) 
		{
			presentImage.color = new Color (1, 1, 1, 1 - ((currentTime-showTime)/ fadeTime));
		}
		else {
			loadLoadingScene ();
		}
//		if (ShowTimeTrigger > showTime)
//		{
//			if (fadeTimeTrigger >= 0 && fadeTimeTrigger < fadeTime)
//			{
//				fadeTimeTrigger += Time.deltaTime;
//				if(show)
//				{
//					bgimages.color = new Color(1, 1, 1, 1 - (fadeTimeTrigger / fadeTime));
//
//				}
//				else
//				{
//					bgimages.color = new Color(1, 1, 1, (fadeTimeTrigger / fadeTime));
//
//				}
//			}

	}
	void loadLoadingScene() {
		SceneManager.LoadScene ("Loading");
	}
}
