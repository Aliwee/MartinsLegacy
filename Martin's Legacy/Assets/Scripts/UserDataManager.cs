using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEngine.UI;

public class UserDataManager : MonoBehaviour {
	
	private string userDataXmlPath;    //xml文件路径
	private XmlDocument userDataXml;   //userData.xml文件
	private XmlReaderSettings readerSetting;   //xml reader设置
	private XmlNodeList userDataNodes;      //<component>子节点列表
	private string language;   //用户所选语言
	private string fullScreen; //用户所选屏幕是否为全屏

	public static UserDataManager instance = null;

	//Awake总是在任何Start方法之前调用
	void Awake() {
		//判断是否已有一个实例
		if (instance == null)
			instance = this;   //如果没有，那么设置实例为该实例
		else if (instance != this)
			Destroy (gameObject);    //保证只能有一个实例
		
		//设置为重新加载场景时不被销毁
		DontDestroyOnLoad(gameObject);


		userDataXmlPath = Application.persistentDataPath + "/UserData/UserData.xml";
		userDataXml = new XmlDocument ();
		readerSetting = new XmlReaderSettings ();

		//找到xml文件目录
		findXML ();

		//加载xml文件
		LoadXML ();

		//获得用户配置数据
		GetSetting ();
	}

	//使用这个创建和查找xml文件目录
	void findXML() {
		//如果不存在那么就创建并且拷贝初始存档
		string path = Application.persistentDataPath + "/UserData";
		if (!Directory.Exists (path)) {
			//创建存档目录
			Directory.CreateDirectory (path);
			//拷贝初始存档至该目录
			StartCoroutine (createOriginXML ());
		}
			
	}

	//第一次使用时写入一个默认的游戏存档
	IEnumerator createOriginXML() {
		//默认初始存档的路径是在StreamingAssets
		string XMLScrPath = GetStreamingAssetsPath ();
		string XMLDesPath = userDataXmlPath;

		//使用WWW方法
		using (WWW www = new WWW (XMLScrPath)) {
			yield return www;

			//出错
			if (!string.IsNullOrEmpty (www.error))
				Debug .Log (www.error);
			//没出错就下载文件
			else {
				FileStream originXML = File.Create (XMLDesPath);
				originXML.Write (www.bytes, 0, www.bytes.Length);
				originXML.Flush ();
				originXML.Close ();
			}			
		}
	}

	//获得StreamingAssets路径
	string GetStreamingAssetsPath() {
		string prefix;    //前缀

		#if UNITY_EDITOR              //如果是Editor模式
		prefix = "file://";
		 
		#elif UNITY_STANDALONE_WIN    //如果是Windows平台模式
		prefix = "file:///";
		#endif 

		string path = prefix + Application.streamingAssetsPath + "/UserData/UserData.xml";

		return path;
	}

	//使用这个读取xml文件
	void LoadXML() {
		//创建xml文档
		readerSetting.IgnoreComments = true;    //忽略xml文件中注释的影响
		userDataXml.Load (XmlReader.Create (userDataXmlPath, readerSetting));

		//得到xml文件中<data>标签下的所有<component>子节点
		userDataNodes = userDataXml.SelectSingleNode ("data").ChildNodes;
	}

	//获得用户配置数据
	void GetSetting() {
		//遍历所有<component>子节点
		foreach (XmlElement component in userDataNodes) {
			//选择标签为setting的<component>
			if (component.GetAttribute ("type") == "setting") {
				XmlNodeList userSettingNodes = component.ChildNodes;   //获得标签为setting的<component>下的子节点
				//遍历所有<setting>子节点
				foreach (XmlElement setting in userSettingNodes ) {
					if (setting.GetAttribute ("type") == "language")
						language = setting.InnerText;    //获得语言信息
					else if (setting .GetAttribute ("type") == "fullScreen") {
						fullScreen = setting.InnerText;  //获得屏幕信息
					}

				}
			}
		}
	}

	//获得用户存档路径
	public string GetUserDataPath() {
		return this.userDataXmlPath;
	}

	//获得所选语言项
	public string GetLanguage() {
		return this.language;
	}

	//获得是否全屏项
	public string GetIsFullScreen() {
		return this.fullScreen;
	}
}
