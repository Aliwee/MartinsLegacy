using UnityEngine;
//用于加载
public class LoadCSV : MonoBehaviour {

	private const string loadPath = "Texts/itemsDescriptions";

	public static LoadCSV instance = null;

	//Awake总是在任何Start方法之前调用
	void Awake() {
		//判断是否已有一个实例
		if (instance == null)
			instance = this;   //如果没有，那么设置实例为该实例
		else if (instance != this)
			Destroy (gameObject);    //保证只能有一个实例

		//设置为重新加载场景时不被销毁
		DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update () {
		
	}

	public string getText(string key, string language)
   {
        string[][] Array;

        //将CSV的内容读到数组里
		TextAsset binAsset = Resources.Load(loadPath, typeof(TextAsset)) as TextAsset;
        string[] lineArray = binAsset.text.Split("\r"[0]);
        Array = new string[lineArray.Length][];

        for (int i = 0; i < lineArray.Length; i++)
        {
            Array[i] = lineArray[i].Split(',');
        }

        //根据key和语言获取文本
        for (int i = 1; i < Array.Length; i++)
        {
            string key1 = string.Format("\n{0}", key);
            if (Array[i][0] == key1)
            {
                for (int j = 0; j < Array[0].Length; j++)
                {
                    if (Array[0][j] == language)
                    {
                        return Array[i][j];
                    }
                }
            }
        }

        return "哪里出了点小问题";

    }



}
