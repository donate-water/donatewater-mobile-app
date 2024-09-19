using UnityEngine;
using System.Collections;
//using Signalphire;
using UI.Pagination;


#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

public class EventSystemIntroduction : MonoBehaviour
{

	public float m_PageScrollSpeed = 3.0f;
	//public GameObject m_Text;
	public GameObject m_Text2;
	//public GameObject m_Logo;


	//public GameObject m_Page1Text;
	//public GameObject m_PrizeText;
	public GameObject m_Page3Text;
	//public GameObject m_Page3Title;
	public GameObject m_PageText3;
	public GameObject m_PageText4;
	public GameObject m_PageText5;

	public GameObject m_Button;

	public GameObject m_ButtonPoint1;
	public GameObject m_ButtonPoint2;
	public GameObject m_ButtonPoint3;
	public GameObject m_ButtonPoint4;
	public GameObject m_ButtonPoint5;
	public GameObject m_ButtonPoint6;

	public GameObject m_Point1;
	public GameObject m_Point2;
	public GameObject m_Point3;
	public GameObject m_Point4;
	public GameObject m_Point5;
	public GameObject m_Point6;
	/*
	public GameObject m_AppleDisclaimer;
	public GameObject m_ImageDisclaimer;
	public GameObject m_ImageNoDisclaimer;*/

	public GameObject m_BackgroundImage;

	public UI.Pagination.PagedRect_ScrollRect m_ScrollRect;
	public GameObject m_Page;


	IEnumerator changeFramerate() {
		yield return new WaitForSeconds(1);
		Application.targetFrameRate = 30;
	}
	IEnumerator ForceAndFixPortrait()
	{
		yield return new WaitForSeconds(0.01f);
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		yield return new WaitForSeconds(0.5f);
	}

	// Use this for initialization
	void Start () {
		StartCoroutine(changeFramerate());


		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();

		ForceAndFixPortrait();



		m_CurState = 0;
		updateStates ();

		/*bool bApple = false;
		if (!bApple) {
			m_AppleDisclaimer.SetActive (false);
			m_ImageDisclaimer.SetActive (false);
			m_ImageNoDisclaimer.SetActive (true);
		} else {
			m_AppleDisclaimer.SetActive (true);
			m_ImageDisclaimer.SetActive (true);
			m_ImageNoDisclaimer.SetActive (false);
		}*/

		UnityEngine.UI.Text text;


		/*	text = m_Page1Text.GetComponent<UnityEngine.UI.Text>();
		//	text.text = "Versuche Zielpunkte zu erreichen und hilf so beim Umweltschutz in Österreich!";
			text.text = LocalizationSupport.GetString("IntroText");//"Join the green side!\nHelp #FAOUN in the monitoring process of the world’s forests.";

			text = m_Text.GetComponent<UnityEngine.UI.Text>();
			//	text.text = "Versuche Zielpunkte zu erreichen und hilf so beim Umweltschutz in Österreich!";
			text.text = LocalizationSupport.GetString("IntroText");//"Join the green side!\nHelp #FAOUN in the monitoring process of the world’s forests.";


			text = m_PrizeText.GetComponent<UnityEngine.UI.Text>();
			text.text = "Du erhälst für jeden besuchten Punkt 1 Euro!";
*/
			/*text = m_Page2Text.GetComponent<UnityEngine.UI.Text>();
			text.text = "Jeden Tag werden in Österreich 150000 m² Land in Geschäfts-, Verkehrs-, Freizeit- und Wohnflächen umgewandelt. Dabei müssen fruchtbare Böden, Artenvielfalt und natürliche CO2-Speicher Asphalt und Beton weichen. Dies führt unter anderem dazu, dass das Risiko für Überschwemmungen steigt, landwirtschaftliche Flächen unproduktiv werden, Hitzewellen in Städten steigen und zur Erderwärmung beitragen.\n\n" +
			"Mit deiner Hilfe kann FotoQuest Go die Folgen der Veränderung unserer Landschaft aufzeichnen und dabei helfen, Österreichs Natur für zukünftige Generationen zu erhalten.\n\n" +
			"Wir haben 9000 Punkte auf ganz Österreich verteilt. Schaue auf die Karte, finde einen Punkt in deiner Nähe und folge dann den Anweisungen um eine Quest durchzufüren. Dabei musst du Bilder von der Landschaft machen und ein paar kurze Fragen beantworten.";
*/
			/*RectTransform rectTransform2 = m_Page2Text.GetComponent<RectTransform> ();
			float scalex = rectTransform2.sizeDelta.x;
			float heightentry = 1000.0f;
			rectTransform2.sizeDelta = new Vector2 (scalex, heightentry);


			float posx = rectTransform2.position.x;
			float posy = rectTransform2.position.y;
			float posz = rectTransform2.position.z;
			rectTransform2.position = new Vector3 (posx, -287.0f, posz);*/


		//	text = m_Page2Title.GetComponent<UnityEngine.UI.Text>();
	//		text.text = "Warum?";

			/*text = m_Page3Title.GetComponent<UnityEngine.UI.Text>();
			text.text = "Warum?";*/

			text = m_Page3Text.GetComponent<UnityEngine.UI.Text>();
			//text.text = "Alle gesammelten Daten werden für alle frei verfügbar sein und einen wichtigen Beitrag zum Naturschutz leisten!";
		//	text.text = "Für jeden besuchten Punkt verdienst du 1€!\n\nDas Geld wird nach dem Ende der Kampagne (28. Dezember 2017) auf dein PayPal Konto überwiesen.";
		//	text.text = "Once you complete your quest, scientists at IIASA will check the quality of your results within 24 hours. If your contribution passes the quality check you will earn 1 EUR.\n\nAt the end of the campaign (December 2017), your total earnings will be transferred to your PayPal account.";
			text.text = LocalizationSupport.GetString("IntroText2");//"FRA Quest is a crowdsourcing tool developed to gather landscape information. The app will guide you to a series of selected locations, collect cardinal pictures and get information about the land cover characteristics. The data collected will be stored in your phone and will be uploaded to a public database once you are connected again.";

		/*	text = m_AppleDisclaimer.GetComponent<UnityEngine.UI.Text>();
			text.text = "(Hinweis: Apple ist nicht Sponsor von FotoQuest Go)";*/

		text = m_PageText3.GetComponent<UnityEngine.UI.Text>();
		text.text = LocalizationSupport.GetString("IntroText3");


		text = m_PageText4.GetComponent<UnityEngine.UI.Text>();
		text.text = LocalizationSupport.GetString("IntroText4");


		text = m_PageText5.GetComponent<UnityEngine.UI.Text>();
		text.text = LocalizationSupport.GetString("IntroText5");

		m_Button.GetComponentInChildren<UnityEngine.UI.Text>().text =  LocalizationSupport.GetString("Next");//"WEITER";
		



#if PLATFORM_ANDROID
		if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
		{
			Permission.RequestUserPermission(Permission.FineLocation);
		}
#endif
        /*
#if UNITY_ANDROID
		NativeEssentials.Instance.Initialize();
		PermissionsHelper.StatusResponse sr;
		PermissionsHelper.StatusResponse sr2;
		PermissionsHelper.StatusResponse sr3;// = PermissionsHelper.StatusResponse.;//NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);
		sr =NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.ACCESS_FINE_LOCATION);
		sr2 =NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.ACCESS_COARSE_LOCATION);
		sr3 =NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);
		if (sr == PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_GRANTED && sr2 == PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_GRANTED) {

		} else {
			if (sr == PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_GRANTED && sr2 == PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_GRANTED) {
			} else {
				NativeEssentials.Instance.RequestAndroidPermissions(new string[] {PermissionsHelper.Permissions.ACCESS_FINE_LOCATION, PermissionsHelper.Permissions.ACCESS_COARSE_LOCATION, PermissionsHelper.Permissions.CAMERA
				});
			}
		}
		#endif*/
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			int instructionshown = PlayerPrefs.GetInt ("IntroductionShown");
			if (instructionshown != 0) {
				Application.LoadLevel ("DemoMap");
			}
		}


		//========================
		// Move background image
		UI.Pagination.PagedRect rect;
		rect = m_Page.GetComponent<UI.Pagination.PagedRect>();
		//float procpage = rect.ScrollRect.GetOffset () / rect.ScrollRect.GetPageSize ();//rect.ScrollRect.GetTotalSize ();
		float procpage = rect.ScrollRect.GetOffset () / rect.ScrollRect.GetTotalSize ();
		//Debug.Log("proc: " + procpage + " Offset: " + rect.ScrollRect.GetOffset () + " total: " + rect.ScrollRect.GetTotalSize ());
		if (procpage < 0.0f)
			procpage = 0.0f;
		RectTransform rt;
		rt = m_BackgroundImage.GetComponent<RectTransform> ();
		rt.position = new Vector3(Screen.width * -procpage * m_PageScrollSpeed/*3.0f*//*2.2f*//*1.8f/*1.8f*//*1.0f/*2.0f*/, Screen.height * 0.5f, 0);
		//rt.position = new Vector3(Screen.width * -0.5f * 1.8f/*1.8f*//*1.0f/*2.0f*/, Screen.height * 0.5f, 0);
		//rt.position = new Vector3 (0.0f, Screen.height*0.5f, 0);
		//rt.sizeDelta = new Vector2 (Screen.width * 1.0f, Screen.width * menuheight);


		//===========================
		// Force app to portrait mode

		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;
	}

	int m_CurState = 0;
	public void updateStates() {
	//	m_Logo.SetActive (false);
		//m_Text.SetActive (false);
		m_Text2.SetActive (false);


		if (m_CurState == 0) {
			m_Point1.SetActive (true);
			m_Point2.SetActive(false);
			m_Point3.SetActive(false);
			m_Point4.SetActive(false);
			m_Point5.SetActive(false);
			m_Point6.SetActive(false);

			m_ButtonPoint1.SetActive (false);
			m_ButtonPoint2.SetActive(true);
			m_ButtonPoint3.SetActive(true);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(true);


		} else if (m_CurState == 1) {
			m_Point1.SetActive (false);
			m_Point2.SetActive (true);
			m_Point3.SetActive (false);
			m_Point4.SetActive(false);
			m_Point5.SetActive(false);
			m_Point6.SetActive(false);


			m_ButtonPoint1.SetActive (true);
			m_ButtonPoint2.SetActive (false);
			m_ButtonPoint3.SetActive (true);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(true);
		}
		else if (m_CurState == 2)
		{
			m_Point1.SetActive(false);
			m_Point2.SetActive(false);
			m_Point3.SetActive(true);
			m_Point4.SetActive(false);
			m_Point5.SetActive(false);
			m_Point6.SetActive(false);


			m_ButtonPoint1.SetActive(true);
			m_ButtonPoint2.SetActive(true);
			m_ButtonPoint3.SetActive(false);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(true);
		}
		else if (m_CurState == 3)
		{
			m_Point1.SetActive(false);
			m_Point2.SetActive(false);
			m_Point3.SetActive(false);
			m_Point4.SetActive(true);
			m_Point5.SetActive(false);
			m_Point6.SetActive(false);


			m_ButtonPoint1.SetActive(true);
			m_ButtonPoint2.SetActive(true);
			m_ButtonPoint3.SetActive(true);
			m_ButtonPoint4.SetActive(false);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(true);
		}
		else if (m_CurState == 4)
		{
			m_Point1.SetActive(false);
			m_Point2.SetActive(false);
			m_Point3.SetActive(false);
			m_Point4.SetActive(false);
			m_Point5.SetActive(true);
			m_Point6.SetActive(false);


			m_ButtonPoint1.SetActive(true);
			m_ButtonPoint2.SetActive(true);
			m_ButtonPoint3.SetActive(true);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(false);
			m_ButtonPoint6.SetActive(true);
		}
		else if (m_CurState == 5)
		{
			m_Point1.SetActive(false);
			m_Point2.SetActive(false);
			m_Point3.SetActive(false);
			m_Point4.SetActive(false);
			m_Point5.SetActive(false);
			m_Point6.SetActive(true);


			m_ButtonPoint1.SetActive(true);
			m_ButtonPoint2.SetActive(true);
			m_ButtonPoint3.SetActive(true);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(false);
		}
		else {
			m_Point1.SetActive (false);
			m_Point2.SetActive(false);
			m_Point3.SetActive(false);
			m_Point4.SetActive(false);
			m_Point5.SetActive(false);
			m_Point6.SetActive(false);


			m_ButtonPoint1.SetActive (true);
			m_ButtonPoint2.SetActive(true);
			m_ButtonPoint3.SetActive(true);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(true);
		} 
	}

	public void NextClicked () {

		/*m_CurState++;
		if (m_CurState > 2) {
			m_CurState = 2;
			PlayerPrefs.SetInt ("IntroductionShown", 1);
			PlayerPrefs.Save ();
			Application.LoadLevel ("DemoMap");
		}
		updateStates ();*/
		m_CurState++;
		if (m_CurState >= 6) {
			m_CurState = 5;
			/*m_CurState = 2;
			*/PlayerPrefs.SetInt ("IntroductionShown", 1);
			PlayerPrefs.Save ();

			int instructionshown = PlayerPrefs.GetInt ("InstructionShown");
			/*if (instructionshown == 0) {
				Application.LoadLevel ("Instructions2");
			} else {*/
				Application.LoadLevel ("DemoMap");
		//	}
			/**/
		} else {
			UI.Pagination.PagedRect rect;
			rect = m_Page.GetComponent<UI.Pagination.PagedRect>();
			rect.SetCurrentPage (m_CurState+1, false);
		}
		updateButtonText ();
	}

	void updateButtonText()
	{
		if (m_CurState < 5) {
			if (Application.systemLanguage == SystemLanguage.German) {
				m_Button.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Next");//"WEITER";
			} else {
				m_Button.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Next");//"NEXT";
			}
		} else {
			if (Application.systemLanguage == SystemLanguage.German) {
				m_Button.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("LetsGo");//"LOS GEHTS!";
			} else {
				m_Button.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("LetsGo");//"LET'S GO!";
			}
		}
	}
	public void Point1Clicked () {
		UI.Pagination.PagedRect rect;
		rect = m_Page.GetComponent<UI.Pagination.PagedRect>();
		rect.SetCurrentPage (1, false);
		m_CurState = 0;
		updateButtonText ();
	}
	public void Point2Clicked () {
		UI.Pagination.PagedRect rect;
		rect = m_Page.GetComponent<UI.Pagination.PagedRect>();
		rect.SetCurrentPage (2, false);
		m_CurState = 1;
		updateButtonText ();
	}
	public void Point3Clicked () {
		UI.Pagination.PagedRect rect;
		rect = m_Page.GetComponent<UI.Pagination.PagedRect>();
		rect.SetCurrentPage (3, false);
		m_CurState = 2;
		updateButtonText ();
	}
	public void Point4Clicked () {
		UI.Pagination.PagedRect rect;
		rect = m_Page.GetComponent<UI.Pagination.PagedRect>();
		rect.SetCurrentPage (4, false);
		m_CurState = 3;
		updateButtonText ();
	}
	public void Point5Clicked () {
		UI.Pagination.PagedRect rect;
		rect = m_Page.GetComponent<UI.Pagination.PagedRect>();
		rect.SetCurrentPage (5, false);
		m_CurState = 4;
		updateButtonText ();
	}
	public void Point6Clicked () {
		UI.Pagination.PagedRect rect;
		rect = m_Page.GetComponent<UI.Pagination.PagedRect>();
		rect.SetCurrentPage (6, false);
		m_CurState = 5;
		updateButtonText ();
	}

	public void OnPageChanged (Page newpage, Page lastpage) {

		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();

		Debug.Log ("> OnPageChanged: " + newpage.PageNumber);

		if (newpage.PageNumber == 1) {
			m_ButtonPoint1.SetActive (false);
			m_ButtonPoint2.SetActive(true);
			m_ButtonPoint3.SetActive(true);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(true);

			m_Point1.SetActive (true);
			m_Point2.SetActive(false);
			m_Point3.SetActive(false);
			m_Point4.SetActive(false);
			m_Point5.SetActive(false);
			m_Point6.SetActive(false);
			m_CurState = 0;
		} else if (newpage.PageNumber == 2) {
			m_ButtonPoint1.SetActive (true);
			m_ButtonPoint2.SetActive (false);
			m_ButtonPoint3.SetActive (true);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(true);

			m_Point1.SetActive (false);
			m_Point2.SetActive (true);
			m_Point3.SetActive (false);
			m_Point4.SetActive(false);
			m_Point5.SetActive(false);
			m_Point6.SetActive(false);
			m_CurState = 1;
		} else if (newpage.PageNumber == 3) {
			m_ButtonPoint1.SetActive (true);
			m_ButtonPoint2.SetActive (true);
			m_ButtonPoint3.SetActive (false);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(true);

			m_Point1.SetActive (false);
			m_Point2.SetActive (false);
			m_Point3.SetActive (true);
			m_Point4.SetActive(false);
			m_Point5.SetActive(false);
			m_Point6.SetActive(false);
			m_CurState = 2;
		}
		else if (newpage.PageNumber == 4)
		{
			m_ButtonPoint1.SetActive(true);
			m_ButtonPoint2.SetActive(true);
			m_ButtonPoint3.SetActive(true);
			m_ButtonPoint4.SetActive(false);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(true);

			m_Point1.SetActive(false);
			m_Point2.SetActive(false);
			m_Point3.SetActive(false);
			m_Point4.SetActive(true);
			m_Point5.SetActive(false);
			m_Point6.SetActive(false);
			m_CurState = 3;
		}
		else if (newpage.PageNumber == 5)
		{
			m_ButtonPoint1.SetActive(true);
			m_ButtonPoint2.SetActive(true);
			m_ButtonPoint3.SetActive(true);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(false);
			m_ButtonPoint6.SetActive(true);

			m_Point1.SetActive(false);
			m_Point2.SetActive(false);
			m_Point3.SetActive(false);
			m_Point4.SetActive(false);
			m_Point5.SetActive(true);
			m_Point6.SetActive(false);
			m_CurState = 4;
		}
		else if (newpage.PageNumber == 6)
		{
			m_ButtonPoint1.SetActive(true);
			m_ButtonPoint2.SetActive(true);
			m_ButtonPoint3.SetActive(true);
			m_ButtonPoint4.SetActive(true);
			m_ButtonPoint5.SetActive(true);
			m_ButtonPoint6.SetActive(false);

			m_Point1.SetActive(false);
			m_Point2.SetActive(false);
			m_Point3.SetActive(false);
			m_Point4.SetActive(false);
			m_Point5.SetActive(false);
			m_Point6.SetActive(true);
			m_CurState = 5;
		}

		updateButtonText ();
	}
}
