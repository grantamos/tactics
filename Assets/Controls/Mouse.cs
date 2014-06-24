using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {

	private GameObject previousHit;
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;
		Physics.Raycast(Camera.main.ScreenPointToRay (Input.mousePosition), out hit);
		
		if(hit.collider != null)
		{
			if (previousHit != null)
				previousHit.SendMessage("DisableHighlight", SendMessageOptions.DontRequireReceiver);

			hit.collider.gameObject.SendMessage("EnableHighlight", SendMessageOptions.DontRequireReceiver);
			previousHit = hit.collider.gameObject;
		}
	}
}
