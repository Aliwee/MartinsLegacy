using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizeManager : MonoBehaviour {

	private string language;    //文本语言
	private string textPath;    //文本路径
	private string languageChoicePath;   //设置页面中的语言选项路径

	public GameObject[] textTiles;     //文本列表
	public GameObject languageChoice;  //针对设置页面中的语言选项文本

	// Use this for initialization
	void Start () {
		//获取语言包路径
		textPath = "Texts/";
		languageChoicePath = "Texts/General/";

		//获取用户语言
		language = UserDataManager.instance.GetLanguage ();

		//加载资源
		LoadText (language);

		//设置页面的单独的语言选项文本
		if (languageChoice != null)
			LoadLanguageChoice (language);
	}

	//本地化加载文本资源
	void LoadText(string language) {
		//根据用户语言选定Resource文件夹下的语言包路径
		textPath += language;
		textPath += "/";

		//为当前场景下的所有文本匹配对应的sprite资源
		foreach (GameObject image in textTiles ) {
			//取得文本索引名字
			string imageName = image.name;
			//由语言包路径加上文本名字便是我们真正的资源路径
			string imageSourePath = textPath + imageName;

			//取得索引下的资源
			Sprite imageSourceSprite = new Sprite();
			imageSourceSprite = Resources.Load (imageSourePath, imageSourceSprite.GetType ()) as Sprite;

			//绑定资源
			image.GetComponent <Image> ().sprite = imageSourceSprite;
		}
	}

	//本地化加载设置页面中的语言选项
	void LoadLanguageChoice(string language) {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
