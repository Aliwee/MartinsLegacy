using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
