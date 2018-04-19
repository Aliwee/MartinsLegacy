using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Xml;

//用于处理设置页面的类
public class SettingManager : MonoBehaviour {

	private string language;             //文本语言
	private string languageChoicePath;   //设置页面中的语言选项路径
	private string new_language;         //新的文本语言
	private int index;                   //语言索引下标
	private ArrayList languageChoices;   //文本选项列表
	private int numberOfLanguages;       //支持的语言总数

	public GameObject languageChoice;    //针对设置页面中的语言选项文本
	public Button backBtn;               //返回按钮
	public Button nextLanguageBtn;       //语言选项按钮
	public Button applyBtn;              //确定按钮

	// 初始化
	void Start () {
		//获取初始的用户设置数据
		language = UserDataManager.instance.GetLanguage ();
		new_language = UserDataManager.instance.GetLanguage ();

		//获取用户语言选项文本的路径
		languageChoicePath = "General/";

		//获取当前语言下标
		languageChoices = new ArrayList ();
		languageChoices.Add ("CN");
		languageChoices.Add ("EN");
		index = languageChoices.IndexOf (language);

		//获取当前语言选项列总数
		numberOfLanguages = languageChoices.Count;

		//加载对应语言选项文本
		LoadLanguageChoice (language);

		//绑定按钮点击事件
		AddListener ();

	}
	
	//每一帧调用
	void Update () {
		if (new_language != language) {
			//显示确认按钮
			applyBtn.gameObject.SetActive (true);
		} else {
			//隐藏确认按钮
			applyBtn.gameObject.SetActive (false);
		}
	}

	//本地化加载设置页面中的语言选项
	void LoadLanguageChoice(string language) {
		//由语言包路径加上文本名字便是我们真正的资源路径
		string imageSourePath = languageChoicePath + language;

		//取得索引下的资源
		Sprite imageSourceSprite = new Sprite();
		imageSourceSprite = Resources.Load (imageSourePath, imageSourceSprite.GetType ()) as Sprite;

		//绑定资源
		languageChoice.GetComponent <Image> ().sprite = imageSourceSprite;
	}

	//这个用来绑定点击事件
	void AddListener() {
		backBtn.onClick.AddListener (BackClick);
		nextLanguageBtn.onClick.AddListener (NextLanguageClick);
		applyBtn.onClick.AddListener (ApplyClick);
	}

	//返回按钮事件
	void BackClick() {
		Debug.Log("Log-SettingManager: back to Start scene.");
		SceneManager.LoadScene ("Start");
	}

	//语言按钮事件
	void NextLanguageClick() {
		Debug.Log("Log-SettingManager: next language.");
		//判断当前下标是否为最后一个
		if (index == numberOfLanguages - 1)
			index = 0;
		else
			index++;
		
		//取得下一个索引下的文本语言ID
		new_language = languageChoices[index].ToString();

		//更新贴图路径
		string imageSourePath = languageChoicePath + new_language;

		//取得索引下的资源
		Sprite imageSourceSprite = new Sprite();
		imageSourceSprite = Resources.Load (imageSourePath, imageSourceSprite.GetType ()) as Sprite;

		//绑定资源
		languageChoice.GetComponent <Image> ().sprite = imageSourceSprite;
	}

	//确定按钮事件
	void ApplyClick() {
		//如果发生了更改
		if (new_language != language) {
			Debug.Log("Log-SettingManager: new language:" + new_language);
			UserDataManager.instance.UpdateSettingData (new_language);
		}

		language = new_language;
	}
}
