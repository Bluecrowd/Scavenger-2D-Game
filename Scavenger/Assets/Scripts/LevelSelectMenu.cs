using UnityEngine;
using System.Collections;

public class LevelSelectMenu : MonoBehaviour {

	public void TutorialLevel() {
		Application.LoadLevel ("Tutorial");
	}

	public void Level1() {
		Application.LoadLevel ("Level1");
	}

	public void Level2() {
		Application.LoadLevel ("Level2");
	}

	public void Level3() {
		Application.LoadLevel ("Level3");
	}

	public void Level4() {
		Application.LoadLevel ("Level4");
	}

	public void Level5() {
		Application.LoadLevel ("Level5");
	}

	public void Level6() {
		Application.LoadLevel ("Level6");
	}

	public void MainMenu() {
		Application.LoadLevel ("MainMenu");
	}


}
