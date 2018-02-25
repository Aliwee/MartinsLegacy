using System.Collections;
using System.Collections.Generic;

//物品类,用于保存和显示物品栏中的物品
public class Item {
	public Item (string name, string tag) {
		this.name = name;
		this.tag = tag;
	}

	public string name { get; set;}

	public string tag { get; set;}
}
