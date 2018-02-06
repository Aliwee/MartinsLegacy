using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class GetLanguage : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Debug.Log ("language!");
		string language = UserDataManager.instance.GetLanguage();
		GameObject.Find("Language").GetComponent<Text>().text = language;
	}

}
