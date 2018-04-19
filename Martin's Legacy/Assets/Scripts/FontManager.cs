using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Font manager. 中文字体与英文字体选择
/// </summary>
public class FontManager : MonoBehaviour {

	public Font font_EN,font_CN;      //Different text font files for English,Chinese 中文字体,英文字体

	private Text story_text;          //Text component to be fetched 将要获取到的Text脚本
	private string language;          //Current language 当前语言

	// Use this for initialization
	void Start () {
		//Fetch the Text component from the GameObject  获取到Text脚本
		story_text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		//Get current language
		language = UserDataManager.instance.GetLanguage ();
		//Set font for different languages
		setFont (language);
	}

	/// <summary>
	/// Set the font. 设置字体
	/// </summary>
	/// <param name="language">Current language in game. 当前语言</param>
	void setFont(string language) {
		switch (language) {
		case "CN":
			story_text.font = font_CN;
			break;
		case "EN":
			story_text.font = font_EN;
			break;
		default:
			break;
		}
	}
}
