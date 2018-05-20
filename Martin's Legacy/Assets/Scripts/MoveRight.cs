using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MoveRight : MonoBehaviour
{
    private float smoothing;
    public GameObject menuDialog, pausePanel, bgLong, bgLong2;
    private Rigidbody2D rd;
    private Rigidbody2D bgrd, bgrd2;
    private Animator an;
    private Vector2 none = new Vector2(0, 0);
    private Vector2 pos = new Vector2(0, 0);
    private Vector2 pos2 = new Vector2(0, 0);
    private string parentName;
    private Image bg;
    private float temp = 0;
    private float tempend = 0;
    private float position = 0;
    private int mode = 5;
    private float width = 0;
    // Use this for initialization
    void Start()
    {
#if UNITY_EDITOR              //如果是Editor模式
        smoothing = 95;

#elif UNITY_STANDALONE_WIN    //如果是Windows平台模式
		smoothing = 300;

#elif UNITY_STANDALONE_OSX    //如果是MACOS平台
		smoothing = 300;
#endif

        rd = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

        bg = bgLong.GetComponent<Image>();
        bgrd = bgLong.GetComponent<Rigidbody2D>();
        bgrd2 = bgLong2.GetComponent<Rigidbody2D>();
        RectTransform rt = bg.GetComponent<RectTransform>();

        width = rt.rect.width / 800 * Screen.width;
        position = transform.position.x;
        tempend = bg.transform.position.x;
        temp = bg.transform.position.x + width - Screen.width;


    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

            Vector2 direction = Input.mousePosition;

            if (Cursor.lockState != CursorLockMode.Locked && !menuDialog.activeSelf)
            {

                GameObject btn = EventSystem.current.currentSelectedGameObject;

                position = transform.position.x - bg.transform.position.x + temp;

                if (btn == null || btn.tag == "PickableItem")
                {

                    if (position <= (0.5 * Screen.width) && direction.x <= (0.5 * Screen.width))
                    {

                    
                        if (direction.x < (0.05 * Screen.width))
                        {
                            direction.x = (float)(0.05 * Screen.width);
                        }
                        pos.x = direction.x;
                        pos.y = transform.position.y;

                        pos2 = bg.transform.position;

                        mode = 0;

                    }
                    else if (position <= (0.5 * Screen.width) && direction.x > (0.5 * Screen.width))
                    {
                        pos.x = (float)(0.5 * Screen.width);
                        pos.y = transform.position.y;

                        pos2.x = -direction.x + (float)0.5 * Screen.width + bg.transform.position.x;
                        pos2.y = bg.transform.position.y;
                        mode = 0;

                    }
                    else if (position >= (0.5 * Screen.width) && (position + direction.x - transform.position.x) < (0.5 * Screen.width))
                    {
                        pos.x = direction.x;
                        pos.y = transform.position.y;

                        pos2.x = temp;
                        pos2.y = bg.transform.position.y;
                        mode = 1;
                    }
                    else if (position < (width - (0.5 * Screen.width)) && (position + direction.x - transform.position.x) > (width - (0.5 * Screen.width)))
                    {
                        pos.x = direction.x;
                        pos.y = transform.position.y;

                        pos2.x = tempend;
                        pos2.y = bg.transform.position.y;
                        mode = 1;
                    }

                    else if (position > (width - (0.5 * Screen.width)) && direction.x < (0.5 * Screen.width))
                    {
                        pos.x = (float)(0.5 * Screen.width);
                        pos.y = transform.position.y;

                        pos2.x = tempend - direction.x + (float)0.5 * Screen.width;
                        pos2.y = bg.transform.position.y;
                        mode = 0;
                    }

                    else if (position >= (width - (0.5 * Screen.width)) && direction.x > (0.5 * Screen.width))
                    {

                        if (direction.x > (0.95 * Screen.width))
                        {
                            direction.x = (float)(0.95 * Screen.width);
                        }

                        pos.x = direction.x;
                        pos.y = transform.position.y;

                        pos2.x = bg.transform.position.x;
                        pos2.y = bg.transform.position.y;
                        mode = 0;
                    }
                    else
                    {
                        pos = transform.position;

                        pos2.x = -direction.x + (float)0.5 * Screen.width + bg.transform.position.x;
                        pos2.y = bg.transform.position.y;
                        mode = 1;
                    }


                    if (pos.x > transform.position.x || pos2.x < bg.transform.position.x)
                    {
                        an.SetInteger("zhuanshen", 0);
                        an.SetInteger("move", 1);
                    }

                    if (pos.x < transform.position.x || pos2.x > bg.transform.position.x)
                    {
                        an.SetInteger("zhuanshen", 0);
                        an.SetInteger("move", -1);
                    }

                }
                else
                {
					if (!btn.transform.parent.gameObject.name.StartsWith("Unit") && btn.tag != "Menu")
                    {

                        if (direction.x > transform.position.x)
                        {
                            an.SetInteger("zhuanshen", 1);
                        }

                        if (direction.x < transform.position.x)
                        {
                            an.SetInteger("zhuanshen", -1);
                        }
                    }
                }
            }
        }

        if (Mathf.Abs(pos.x - transform.position.x) < 10 && Mathf.Abs(pos2.x - bg.transform.position.x) < 10)
        {
            an.SetInteger("move", 0);
        }

        if (Mathf.Abs(pos.x - transform.position.x) < 10)
        {
            mode = 1;
        }

        if (Mathf.Abs(pos2.x - bg.transform.position.x) < 10)
        {
            mode = 0;
        }


        if (pos != none && !pausePanel.activeSelf && !Input.anyKeyDown)
        {
            if (mode == 0)
            {
                rd.MovePosition(Vector2.Lerp(transform.position, pos, 1 / (((Vector2)transform.position - pos).magnitude) * smoothing * Time.deltaTime));
            }
            if (mode == 1)
            {
                bgrd.MovePosition(Vector2.Lerp(bg.transform.position, pos2, 1 / (((Vector2)bg.transform.position - pos2).magnitude) * smoothing * Time.deltaTime));
                bgrd2.MovePosition(Vector2.Lerp(bg.transform.position, pos2, 1 / (((Vector2)bg.transform.position - pos2).magnitude) * smoothing * Time.deltaTime));
            }


        }
    }
}
