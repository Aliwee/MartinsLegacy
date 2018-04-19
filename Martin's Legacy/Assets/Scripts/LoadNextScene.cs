using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//用于点击Walkable标签的时候到达下一个场景
public class LoadNextScene : MonoBehaviour {
	public void Click() {
		SceneManager.LoadScene (this.name);
	}
}
