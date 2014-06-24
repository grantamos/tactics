using UnityEngine;
using System.Collections;

public class Highlighter : MonoBehaviour {

	public GameObject highlightObject;

	void Start() {
		if (highlightObject)
			highlightObject.renderer.enabled = false;
	}

	public void EnableHighlight() {
		if (highlightObject)
			highlightObject.renderer.enabled = true;
	}

	public void DisableHighlight() {
		if (highlightObject)
			highlightObject.renderer.enabled = false;
	}
}
