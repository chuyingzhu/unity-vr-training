using UnityEngine;
using System;
using System.Text.RegularExpressions;
using System.Collections;

namespace VRKeys {
	public class DemoScene : MonoBehaviour {
		public Keyboard keyboard;
		public Camera cam;

		// Show the keyboard with a custom input message. Attaching events dynamically,
		// but you can also use the inspector.
		private void OnEnable() {
			// Automatically creating camera here to show how
			//GameObject camera = new GameObject ("Main Camera");
			//Camera cam = camera.AddComponent<Camera> ();
			//cam.nearClipPlane = 0.1f;
			//camera.AddComponent<AudioListener> ();

			// Improves event system performance
			Canvas canvas = keyboard.canvas.GetComponent<Canvas>();
			canvas.worldCamera = cam;

			keyboard.Enable();
			keyboard.SetPlaceholderMessage("Please enter your username");

			keyboard.OnUpdate.AddListener(HandleUpdate);
			keyboard.OnSubmit.AddListener(HandleSubmit);
			//keyboard.OnCancel.AddListener(HandleCancel);
		}

		private void OnDisable() {
			keyboard.OnUpdate.RemoveListener(HandleUpdate);
			keyboard.OnSubmit.RemoveListener(HandleSubmit);
			//keyboard.OnCancel.RemoveListener(HandleCancel);

			keyboard.Disable ();
		}

		// Press space to show/hide the keyboard.
		private void Update() {
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (keyboard.disabled) {
					keyboard.Enable ();
				} else {
					keyboard.Disable ();
				}
			}
		}

		// Hide the validation message on update. Connect this to OnUpdate.
		public void HandleUpdate(string text) {
			keyboard.HideValidationMessage();
		}

		// Validate the input and simulate a form submission. Connect this to OnSubmit.
		public void HandleSubmit(string text) {
			keyboard.DisableInput();

			if (!ValidateInput(text)) {
				keyboard.ShowValidationMessage("Please enter a valid input");
				keyboard.EnableInput ();
				return;
			}
		}

		public void HandleCancel() {
			Debug.Log("Cancelled keyboard input!");
		}
/* 
		/// <summary>
		/// Pretend to submit the email before resetting.
		/// </summary>
		private IEnumerator SubmitEmail (string email) {
			keyboard.ShowInfoMessage ("Sending lots of spam, please wait... ;)");

			yield return new WaitForSeconds (2f);

			keyboard.ShowSuccessMessage ("Lots of spam sent to " + email);

			yield return new WaitForSeconds (2f);

			keyboard.HideSuccessMessage ();
			keyboard.SetText ("");
			keyboard.EnableInput ();
		}*/

		private bool ValidateInput(string text) {
			if (text.Equals("azhu")) {
				return true;
			}
			return false;
		}
	}
}