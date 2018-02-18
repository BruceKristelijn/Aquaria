using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Painting : MonoBehaviour {

	public Sprite pineapple;
	public Sprite skeleton;
	public Image  target;

	void OnMouseOver(){
		target.sprite = skeleton;
	}
	void OnMouseExit(){
		target.sprite = pineapple;
	}
}
