using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//场景跳转
public class ChangeScene : MonoBehaviour {

	public Button nextBtn;//跳转到下一个场景的触发按钮
	string SceneName;//当前场景的名称

	// Use this for initialization
	void Start() {
		//获取当前场景的名字
		SceneName = SceneManager.GetActiveScene().name;
		AddListener ();
	}

	void AddListener() {
		nextBtn.onClick.AddListener (NextClick);
	}

	//开始按钮
	private void NextClick() {
		if (SceneName == "Chapter-1-Level-1") {
			SceneManager.LoadScene ("Chapter-1-Level-2");
		}
		if (SceneName == "Chapter-1-Level-2") {
			SceneManager.LoadScene ("Chapter-1-Level-3");
		}
		if (SceneName == "Chapter-1-Level-3") {
			SceneManager.LoadScene ("Chapter-1-Level-4");
		}
		if (SceneName == "Chapter-1-Level-5") {
			SceneManager.LoadScene ("Thanks");
		}
	}
}
