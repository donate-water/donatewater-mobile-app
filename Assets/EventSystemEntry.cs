using UnityEngine;
using System.Collections;

public class EventSystemEntry : MonoBehaviour {

	//public GameObject m_Instructions;

	// Use this for initialization
	void Start () {
		//Application.LoadLevel ("Demo");

		/*PlayerPrefs.SetInt ("OpenMsg", 1);
		PlayerPrefs.Save ();
*/
		/*PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();*/

		PlayerPrefs.SetInt("LoadPointsFromWWW", 1);
		PlayerPrefs.Save();

		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();
		
		System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
		
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;

		/*UnityEngine.UI.Text textdebug;
		textdebug = m_Instructions.GetComponent<UnityEngine.UI.Text>();

		if (Application.systemLanguage == SystemLanguage.German ) {
			textdebug.text = "Bleibe wachsam. Behalte immer deine Umgebung im Auge!";
		} else {
			textdebug.text = "Remember to be alert at all times. Stay aware of your surroundings.";
		}
		*/
		StartCoroutine(ShowSomeTime());


	}

	void Update()
	{
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;
	}

	IEnumerator ShowSomeTime() {
		yield return new WaitForSeconds(3.0f);


		//Application.LoadLevel ("StartScreen");/**/
		if (PlayerPrefs.HasKey ("LoggedOut")) {
			int loggedout = PlayerPrefs.GetInt ("LoggedOut");
			if (loggedout == 1) {
				Application.LoadLevel ("StartScreen");
				//return;
				yield return null;
			}
		}



		if (PlayerPrefs.HasKey ("NrQuestsDone")) {
			int nrquestsdone = PlayerPrefs.GetInt ("NrQuestsDone");
			Application.LoadLevel ("DemoMap");
			//return;
			yield return null;
		} 

		if (!PlayerPrefs.HasKey ("IntroductionShown")) {
			Application.LoadLevel ("Introduction");
		} else {
			Application.LoadLevel ("DemoMap");
		}/**/
	}

}
