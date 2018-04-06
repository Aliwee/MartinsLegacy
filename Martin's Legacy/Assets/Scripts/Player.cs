using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float smoothing = 6;
	private Rigidbody2D rb;
    private Vector2 pos;
    private Vector2 xiuzheng = new Vector2(317, 178);

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButton(0))
        {
            Vector2 direction = Input.mousePosition;
            pos = new Vector2(direction.x - 317 , direction.y - 180 );
            Debug.Log(pos);


            if (pos.x > 250)
            {
                pos.x = 250;
            }
            if (pos.x < -250)
            {
                pos.x = -250;
            }
            if (pos.y > 88)
            {
                pos.y = 88;
            }
            if (pos.y < -88)
            {
                pos.y = -88;
            }

        }
        rb.MovePosition(Vector2.Lerp(transform.position, pos + xiuzheng, smoothing * Time.deltaTime));

    }
}
