using UnityEngine;
//用于加载
public class LoadCSV : MonoBehaviour {

	private const string loadPath1 = "Texts/itemsDescriptions";
	private const string loadPath2 = "Texts/peopleDescriptions";
	private const string loadPath3 = "Texts/puzzleDescriptions";

	private TextAsset itemDescrition,peopleDescription,puzzleDescription;
	private string[][] arrayItem,arrayPeople,arrayPuzzle;
	private string[] lineArrayItem,lineArrayPeople,lineArrayPuzzle;
	private int itemNum, peopleNum, puzzleNum;

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

		//将CSV的内容读到数组里
		itemDescrition = Resources.Load(loadPath1, typeof(TextAsset)) as TextAsset;
		peopleDescription = Resources.Load (loadPath2, typeof(TextAsset)) as TextAsset;
		puzzleDescription = Resources.Load (loadPath3, typeof(TextAsset)) as TextAsset;

		//将CSV的内容读到数组里
		lineArrayItem = itemDescrition.text.Split("\r"[0]);
		lineArrayPeople = peopleDescription.text.Split ("\r"[0]);
		lineArrayPuzzle = puzzleDescription.text.Split ("\r" [0]);

		itemNum = lineArrayItem.Length;
		peopleNum = lineArrayPeople.Length;
		puzzleNum = lineArrayPuzzle.Length;

		arrayItem = new string[itemNum][];
		arrayPeople = new string[peopleNum][];
		arrayPuzzle = new string[puzzleNum][];

		for (int i = 0; i < itemNum; i++)
		{
			arrayItem[i] = lineArrayItem[i].Split(',');
		}
		for (int i = 0; i < peopleNum; i++) {
			arrayPeople [i] = lineArrayPeople [i].Split (',');
		}
		for (int i = 0; i < puzzleNum; i++) {
			arrayPuzzle [i] = lineArrayPuzzle [i].Split (',');
		}

    }
		
	public string getText(string key, string language, string type)
   {
		string result = "哪里出了点小问题";
		int length;

		switch (type) {
		case "item":
			//根据key和语言获取文本
			length = arrayItem.Length;
			for (int i = 1; i < length; i++) {
				string key1 = string.Format ("\n{0}", key);
				if (arrayItem [i] [0] == key1) {
					for (int j = 0; j < arrayItem [0].Length; j++) {
						if (arrayItem [0] [j] == language) {
							result = arrayItem [i] [j];
							return result;
						}
					}
				}
			}
			break;
		case "people":
			//根据key和语言获取文本
			length = arrayPeople.Length;
			for (int i = 1; i < length; i++) {
				string key1 = string.Format ("\n{0}", key);
				if (arrayPeople [i] [0] == key1) {
					for (int j = 0; j < arrayPeople [0].Length; j++) {
						
						if (arrayPeople [0] [j] == language) {
							result = arrayPeople [i] [j];
							return result;
						}
					}
				}
			}
			break;
		case "puzzle":
			//根据key和语言获取文本
			length = arrayPuzzle.Length;
			for (int i = 1; i < length; i++) {
				string key1 = string.Format ("\n{0}", key);
				if (arrayPuzzle [i] [0] == key1) {
					for (int j = 0; j < arrayPuzzle [0].Length; j++) {
						if (arrayPuzzle [0] [j] == language) {
							result = arrayPuzzle [i] [j];
							return result;
						}
					}
				}
			}
			break;
		}

		return result;
    }
}
