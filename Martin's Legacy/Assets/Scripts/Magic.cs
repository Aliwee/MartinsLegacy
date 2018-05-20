using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Magic : MonoBehaviour {

	private double leftx;
    private double rightx;
	private Animator animator1,animator2;
	public double leftFactor;
	public double rightFactor;
	private int time = 1;
	public GameObject icon, icon_lighting,background,oldScene,newScene;
	public Button icon02;

    public void function1()
    {
		icon.SetActive (false);
		icon_lighting.SetActive (true);
    }

    public void function2()
    {
		icon.SetActive (true);
		icon_lighting.SetActive (false);
    }

	void clickIcon02() {
		time = 0;
		animator1.SetTrigger("magic");
		Invoke("ChangeBg", 2);
	}

	void ChangeBg() {
		animator2.SetTrigger ("start");
		Invoke("ChangeBg2", 1.9f);
	}

	void ChangeBg2() {
		if(time == 1) {
			oldScene.SetActive (true);
			newScene.SetActive (false);
		}
		else {
			oldScene.SetActive (false);
			newScene.SetActive (true);
			time = 1;
		}
	}

    // Use this for initialization
    void Start () {

		leftx = (double)Screen.width * leftFactor;
		rightx = (double)Screen.width * rightFactor;
		animator1 = GetComponent<Animator> ();
		animator2 = background.GetComponent<Animator> ();
		icon02.onClick.AddListener (clickIcon02);
    }

    // Update is called once per frame
    void Update () {

		Debug.Log (transform.position.x);

        if(transform.position.x> leftx && transform.position.x < rightx)
        {
            //变亮
            function1();

            /*使用法杖
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("magic");
                Invoke("ChangeBg", 2);
            }*/

        }
        else
        {

            //取消变亮
            function2();

        }
    }
}
