using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//用于处理Esc键退出的类
public class ExitManager : MonoBehaviour {

	private bool isPaused;            //是否按下Esc键
	public Transform exitPanel;       //退出菜单面板

	// Use this for initialization
	void Start () {
		exitPanel.gameObject.SetActive (false);     //保证退出菜单面板一开始是隐藏的
		isPaused = false;                           //保证isPaused变量在每次场景打开的时候都是false
	}
	
	// Update is called once per frame
	void Update () {
		/**
		 * 如果按下Esc键并且没有暂停游戏，那么切换状态并显示退出菜单面板；
		 * 如果按下Esc键并且已经暂停游戏，那么又回到游戏状态并隐藏菜单面板
		**/
		if (Input.GetKeyDown (KeyCode.Escape) && !isPaused)
			pause ();
		else if (Input.GetKeyDown (KeyCode.Escape) && isPaused)
			unPause ();
	}

	//暂停游戏
	void pause() {
		isPaused = true;
		exitPanel.gameObject.SetActive (true);  //显示退出菜单面板
		Time.timeScale = 0f;    //暂停游戏
	}

	//继续游戏
	public void unPause() {
		isPaused = false;
		exitPanel.gameObject.SetActive (false);  //隐藏退出菜单面板
		Time.timeScale = 1f;   //继续游戏
	}

	//退出按钮
	public void ExitClick() {
		//解除暂停状态
		isPaused = false;
		Time.timeScale = 1f;

		//保存用户当前物品栏中的物品
		UserDataManager.instance.UpdateItemsAndLevelData ();

		//跳转至开始界面
		SceneManager.LoadScene ("Loading");
	}
}
