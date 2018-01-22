using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreen : MonoBehaviour {

	// 这个是用来更新全屏幕的
	void Start () {
		// 抓取一个引用到附加的SpriteRenderer
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		// 确定sprite的大小，以及我们正在处理的大小和相机的大小
		float cameraHeight = Camera.main.orthographicSize * 2;
		Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
		Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

		// 确定需要多大的比例来填充相机
		Vector2 scale = transform.localScale;
		if (cameraSize.x >= cameraSize.y) { // 按照相机的宽度来放大
			scale *= cameraSize.x / spriteSize.x;
		} else { // 按照相机的高度来放大
			scale *= cameraSize.y / spriteSize.y;
		}

		transform.position = Vector2.zero; 
		transform.localScale = scale;
	}

}
