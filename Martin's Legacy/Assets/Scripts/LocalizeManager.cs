using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class LocalizeManager : MonoBehaviour {

	private string language;    //文本语言
	private string textPath;    //文本路径

	public GameObject[] textTiles;     //文本列表
	//public GameObject localize;        //Fungus Localization组件

	// Use this for initialization
	void Start () {
		//获取语言包路径
		textPath = "Texts/";

		//获取用户语言
		language = UserDataManager.instance.GetLanguage ();

		//调用Fungus
		//localize.GetComponent <Localization > ().SetActiveLanguage (language);

		//加载资源
		LoadText (language);
	}

	//每帧调用
	void Update () {
		string new_language = UserDataManager.instance.GetLanguage ();
		if (language != new_language) {
			LoadText (new_language);
		}
	}

	//本地化加载文本资源
	void LoadText(string this_language) {
		//根据用户语言选定Resource文件夹下的语言包路径
		textPath += this_language;
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

		//清空textPath保证下次调用
		textPath = "Texts/";
	}
}
