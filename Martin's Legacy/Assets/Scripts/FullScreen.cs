using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于在Canvas之外保证全屏的类-已抛弃!!!
public class FullScreen : MonoBehaviour {

	private SpriteRenderer spriteRenderer;

	//这个是用来更新全屏幕的
	void Start () {
		
		//抓取一个引用到附加的SpriteRenderer
		spriteRenderer = GetComponent<SpriteRenderer>();

		//获取当前屏幕分辨率和全屏模式
//		#if UNITY_STANDALONE_WIN      //Windows平台
//		Screen.SetResolution (Display.main.systemWidth, Display.main.systemHeight, true);
//		#elif UNITY_STANDALONE_OSX    //Mac平台
//			Screen.SetResolution (Screen.currentResolution.width, Screen.currentResolution.height, true);
//		#endif 

		//确定sprite的大小，以及我们正在处理的大小和相机的大小
		float cameraHeight = Camera.main.orthographicSize * 2;
		Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
		Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

		Vector2 scale = transform.localScale;
		float cameraSizeRatio = cameraSize.x / cameraSize.y;      //客户机分辨率
		float spriteSizeRatio = spriteSize.x / spriteSize.y;      //原始背景资源大小

		//确定需要多大的比例来填充相机，根据实际的客户机分辨率选择合适的缩放因子
		if (cameraSizeRatio <= spriteSizeRatio)
			scale *= cameraSize.x / spriteSize.x;
		else
			scale *= cameraSize.y / spriteSize.y;

		//应用缩放因子
		transform.position = Vector2.zero; 
		transform.localScale = scale;
	}
}
