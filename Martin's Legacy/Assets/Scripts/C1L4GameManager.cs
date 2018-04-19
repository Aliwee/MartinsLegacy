using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Fungus;

public class C1L4GameManager : MonoBehaviour {

    //图片数组
	public Sprite[] PicArray;
    //三个按钮
	public Button Button1;
	public Button Button2;
	public Button Button3;
	public Button Button4;

    //第一个图片 
    public GameObject Pic1;
	public Button img1;
	[SerializeField]
	private int picN1 = 3;
	[SerializeField]
	private int picC1 = 0;

    //第二个图片 
    public GameObject Pic2;
	public Button img2;
	[SerializeField]
	private int picN2;
	[SerializeField]
	private int picC2;

    //第三个图片 
    public GameObject Pic3;
	public Button img3;
	[SerializeField]
	private int picN3;
	[SerializeField]
	private int picC3;
    
    public int lev1WIN;

	public GameObject lockLeft,lockRight;
	private Animator an1,an2;

	public GameObject panel;

	public Fungus.Flowchart flowchart;

    private void Start()
    {
		Button1.onClick.AddListener (changeImageButtonClick);
		Button2.onClick.AddListener (changeImageButtonClick);
		Button3.onClick.AddListener (changeImageButtonClick);
		Button4.onClick.AddListener (submitFirstLock);
		img1.onClick.AddListener (pressImage);
		img2.onClick.AddListener (pressImage);
		img3.onClick.AddListener (pressImage);
		an1 = lockLeft.GetComponent<Animator> ();
		an2 = lockRight.GetComponent<Animator> ();
		lev1WIN = 0;
    }

    // Update is called once per frame
    void Update () {
		if (lev1WIN == 1 && panel.activeSelf)
			an1.SetInteger ("c1l4left", 1);
        //第二道关卡的解锁
        if (picC1 == 4 && picC2 == 7 && picC3 == 5)
        {
			an2.SetInteger ("c1l4right", 1);
			wait ();
        }
    }

	/// <summary>
	/// Wait this instance.
	/// </summary>
	void wait() {
		clear ();
		//执行fungus对话
		flowchart.ExecuteBlock ("puzzleDone");
	}

	/// <summary>
	/// Changes the image button click.
	/// </summary>
	void changeImageButtonClick() {
		if (lev1WIN == 0) {
			//获得当前点击的按钮
			GameObject btn =  EventSystem.current.currentSelectedGameObject;
			//按钮的名字
			string name = btn.name;
			//根据名字执行
			switch (name) {
			case "按钮1":
				if (picN1 == 6)
				{
					picN1 = 3;
				}
				else
				{
					picN1 += 1;
				}
				Pic1.GetComponent <Image> ().sprite = PicArray[picN1];
				break;
			case "按钮2":
				if (picN2 == 9)
				{
					picN2 = 6;
				}
				else
				{
					picN2 += 1;
				}
				Pic2.GetComponent <Image> ().sprite = PicArray[picN2];
				break;
			case "按钮3":
				if (picN3 == 3)
				{
					picN3 = 0;
				}
				else
				{
					picN3 += 1;
				}
				Pic3.GetComponent <Image> ().sprite = PicArray[picN3];
				break;
			}
		}

	}

	/// <summary>
	/// Submits the first lock.
	/// </summary>
	void submitFirstLock() {
		if (picN1 == 5 && picN2 == 8 && picN3 == 2 && lev1WIN == 0)
		{
			Debug.Log("解锁关卡1成功");
			lev1WIN = 1;
			an1.SetInteger("c1l4left", 1);
			flowchart.ExecuteBlock ("removeButtons");
		}
		else 
		{
			Debug.Log("解锁关卡1失败"+picN1 + picN2 + picN3 );          
		}
	}

	/// <summary>
	/// Presses the image.
	/// </summary>
	void pressImage() {
		if (lev1WIN == 1) {
			//获得当前点击的按钮
			GameObject btn =  EventSystem.current.currentSelectedGameObject;
			//按钮的名字
			string name = btn.name;
			//根据名字执行
			switch (name) {
			case "图标1":
				picC1 += 1;
				break;
			case "图标2":
				if(picC1 != 4)
				{
					picC1 = 0;
				}
				else
				{
					picC2 += 1;
					Debug.Log("尝试解锁关卡2 ，当前第二个图片点击次数为" + picC2);

				}
				break;
			case "图标3":
				if (picC2 != 7)
				{
					picC2 = 0;
					picC1 = 0;
				}
				else
				{
					picC3 += 1;
					Debug.Log("尝试解锁关卡2 ，当前第三个图片点击次数为" + picC3);

				}
				break;
			}
		}

	}

	/// <summary>
	/// Clear this instance.
	/// </summary>
	public void clear() {
		//清空数据
		picC1 = 0;
		picC2 = 0;
		picC3 = 0;
	}
}
