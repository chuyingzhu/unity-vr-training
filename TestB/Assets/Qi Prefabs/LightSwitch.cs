using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {

	public UnityEngine.Events.UnityEvent BombsToTrigger;

	void update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			BombsToTrigger.Invoke();
		}
			
	}

}
