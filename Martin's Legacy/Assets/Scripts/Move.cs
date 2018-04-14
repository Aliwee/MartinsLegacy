using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Move : MonoBehaviour {
    public float smoothing = 80;
	public GameObject menuDialog,pausePanel; 
    private Rigidbody2D rd;
    private Animator an;
    public Vector2 none = new Vector2(0, 0);
    public Vector2 pos = new Vector2(0, 0);
    private float maxX = (float)0.6 * Screen.width;
    private float minX = (float)0.2 * Screen.width;
	private string parentName;

    // Use this for initialization
    void Start () {
        rd = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

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
				
                if (btn == null)
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
					if (!btn.transform.parent.gameObject.name.StartsWith("Unit")) {
						Debug.Log("转身");

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
		}
		if (pos != none && !pausePanel.activeSelf && !Input.anyKeyDown)
		{
			rd.MovePosition(Vector2.Lerp(transform.position, pos, 1 / (((Vector2)transform.position - pos).magnitude) * smoothing * Time.deltaTime));
		}
    }
}
