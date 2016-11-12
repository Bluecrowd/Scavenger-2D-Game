using UnityEngine;
using System.Collections;

public class TitleScreenMenu : MonoBehaviour {

	public void backToMainMenu() {
		Application.LoadLevel ("MainMenu");
	}
}
