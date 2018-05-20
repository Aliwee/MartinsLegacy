using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MoveShort : MonoBehaviour {
	private float smoothing;
	public GameObject menuDialog,pausePanel; 
	public float xMaxFactor, xMinFactor;
    private Rigidbody2D rd;
    private Animator an;
	private Vector2 none = new Vector2(0, 0);
	private Vector2 pos = new Vector2(0, 0);
	private float maxX,minX;
	private string parentName;
	public GameObject itemsManagerScript;
	private ItemsManager im;

    // Use this for initialization
    void Start () {
		#if UNITY_EDITOR              //如果是Editor模式
		smoothing = 95;

		#elif UNITY_STANDALONE_WIN    //如果是Windows平台模式
		smoothing = 300;

		#elif UNITY_STANDALONE_OSX    //如果是MACOS平台
		smoothing = 300;
		#endif 

        rd = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
		maxX = (float)xMaxFactor * Screen.width;
		minX = (float)xMinFactor * Screen.width;
		im = itemsManagerScript.GetComponent<ItemsManager> ();
    }

    // Update is called once per frame
    void Update() {


        //if (Cursor.lockState == CursorLockMode.Locked)
        //  {
        //   Vector2 direction = Input.mousePosition;
        //  pos.x = (Screen.width/2);
        //   pos.y = transform.position.y;
        //  }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 direction = Input.mousePosition;

			if (Cursor.lockState != CursorLockMode.Locked && !menuDialog.activeSelf)
            {

                GameObject btn = EventSystem.current.currentSelectedGameObject;
				
				if (btn == null || btn.tag=="PickableItem")
                {
                    Debug.Log("移动");

                    pos.x = direction.x;
                    pos.y = transform.position.y;

                    if (pos.x > maxX)
                    {
                        pos.x = maxX;
                    }
                    if (pos.x < minX)
                    {
                        pos.x = minX;
                    }
                    if (pos.x > transform.position.x)
                    {
                        an.SetInteger("zhuanshen", 0);           
                        an.SetInteger("move", 1);
                    }

                    if (pos.x < transform.position.x)
                    {
                        an.SetInteger("zhuanshen", 0);
                        an.SetInteger("move", -1);
                    }

                }
				else 
                {
					if (!btn.transform.parent.gameObject.name.StartsWith("Unit") && btn.tag != "Menu") {
						Debug.Log(btn.name);

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

		if ( Mathf.Abs(pos.x - transform.position.x) < 10 )
		{
			an.SetInteger("move", 0);
			im.LoadItemsInPack ();
			GameObject go = ItemsInteractiveManager.instance.GetLastPickedItem ();
			Destroy (go);
			ItemsInteractiveManager.instance.SetLastPickedItem (null);
		}
		if (pos != none && !pausePanel.activeSelf && !Input.anyKeyDown)
		{
			rd.MovePosition(Vector2.Lerp(transform.position, pos, 1 / (((Vector2)transform.position - pos).magnitude) * smoothing * Time.deltaTime));
		}
    }
}
