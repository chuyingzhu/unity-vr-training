using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {
	public Light m_light;

	public void turnOn() {
		m_light.enabled = true;
	}

	public void turnOff() {
		m_light.enabled = false;
	}
}
