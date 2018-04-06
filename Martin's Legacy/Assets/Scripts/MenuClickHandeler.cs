using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//用于处理开始界面跳转的类
public class MenuClickHandeler : MonoBehaviour {

	public Button startBtn;
	public Button settingBtn;
	public Button exitBtn;

	//页面载入时就调用它
	void Start() {
		AddListener ();
	}

	//这个用来绑定点击事件
	void AddListener() {
		startBtn.onClick.AddListener (StartClick);
		settingBtn.onClick.AddListener (SettingClick);
		exitBtn.onClick.AddListener (ExitClick);
	}

	//开始按钮
	private void StartClick() {
		//找到存档中的章节和关卡号
		string chapterNum = UserDataManager.instance.GetChapterNum ();
		string levelNum = UserDataManager.instance.GetLevelNum ();

		//建立场景索引
		string sceneName = "Chapter-" + chapterNum + 
		                   "-Level-" + levelNum;

		//加载场景
		SceneManager.LoadScene (sceneName);
	}

	//设置按钮
	private void SettingClick() {
		SceneManager.LoadScene ("Setting");
	}

	//退出按钮
	private void ExitClick() {
		Application.Quit ();
	}
}
