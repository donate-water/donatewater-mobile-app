using UnityEngine;
using System.Collections;
using Unitycoding.UIWidgets;
using UnityEngine.UI;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;

public class EventSystemRegister : MonoBehaviour {


	public GameObject m_ButtonBack;
	public GameObject m_TextTitle;
	public GameObject m_TextName;
	public GameObject m_TextEMail;
	public GameObject m_TextPassword;
	public GameObject m_TextPasswordRepeat;

	public GameObject m_ButtonLogin;


	public GameObject m_InputName;
	public GameObject m_InputMail;
	public GameObject m_InputPassword;
	public GameObject m_InputPasswordRepeat;

	private Rect windowRect = new Rect (20, 20, 120, 50);


	public GameObject m_ToggleAgree;
	public GameObject m_BtnTerms;
	public GameObject m_BtnDataPolicy;

	public MessageBox messageBox;
	//private MessageBox verticalMessageBox;

	private int m_Show = 0;


	public GameObject m_ErrorBack;
	public GameObject m_ErrorText;
	public GameObject m_ErrorButton;
	int m_ErrorStep;
	string m_StrError;
	string m_UrlParams;


	public GameObject m_TermsBack;
	public GameObject m_TermsTitle;
	public GameObject m_TermsTextBack;
	public GameObject m_TermsScrollbarAT;
	public GameObject m_TermsImageAT;
	public GameObject m_TermsScrollbarEN;
	public GameObject m_TermsImageEN;
	public GameObject m_TermsBtnAccept;
	public GameObject m_TermsBtnDecline;

	public GameObject m_LoadingBack;
	public GameObject m_LoadingText;




	public GameObject m_TermsBack2;
	public GameObject m_TermsBtnAccept2;
	public GameObject m_TermsBtnDecline2;
	public GameObject m_ToggleAgree2;
	public GameObject m_ToggleAgreeText2;
	public GameObject m_PanelAgree2;




	public GameObject m_NewsletterBack;
	public GameObject m_NewsletterToggle;
	public GameObject m_NewsletterButtonAccept;
	public GameObject m_NewsletterButtonReject;

	public CanvasScaler m_Scaler;
	public GameObject m_BackgroundPortrait;
	public GameObject m_BackgroundLandscape;

	public void ForceLandscapeLeft()
	{
		StartCoroutine(ForceAndFixLandscape());
	}

	IEnumerator ForceAndFixLandscape()
	{
		yield return new WaitForSeconds (0.01f);

		Screen.autorotateToPortraitUpsideDown = true;
		Screen.autorotateToLandscapeRight = true;
		Screen.autorotateToLandscapeLeft = true;
		Screen.orientation = ScreenOrientation.AutoRotation;
		Screen.autorotateToPortrait = true;
		yield return new WaitForSeconds (0.5f);
	}


	public void ForcePortrait()
	{
		StartCoroutine(ForceAndFixPortrait());
	}

	IEnumerator ForceAndFixPortrait()
	{
		yield return new WaitForSeconds (0.01f);
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		yield return new WaitForSeconds (0.5f);
	}

	void UpdateBackgroundImage()
	{
		if (Screen.width > Screen.height) {
			m_BackgroundPortrait.SetActive (false);
			m_BackgroundLandscape.SetActive (true);
		} else {
			m_BackgroundPortrait.SetActive (true);
			m_BackgroundLandscape.SetActive (false);
		}
	}

	// Use this for initialization
	void Start () {

		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();

		#if UNITY_WEBGL
		ForceLandscapeLeft ();
		#else 
		ForcePortrait ();
		#endif

		UpdateBackgroundImage ();

		/*
		Screen.orientation = ScreenOrientation.Portrait;

		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;*/


		m_ErrorBack.SetActive (false);
		m_ErrorText.SetActive (false);
		m_ErrorButton.SetActive (false);
		m_ErrorStep = 0;

		m_NewsletterBack.SetActive (false);
		m_NewsletterToggle.SetActive (false);
		m_NewsletterButtonAccept.SetActive (false);
		m_NewsletterButtonReject.SetActive (false);

		m_LoadingBack.SetActive (false);
		m_LoadingText.SetActive (false);

		if (Application.systemLanguage == SystemLanguage.German) {
			m_LoadingText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Ein Moment...";
		//	m_ToggleAgreeText2.GetComponent<UnityEngine.UI.Text> ().text = "Ich gebe hiermit meine Zustimmung, dass mein Benutzername auf den unten genannten Internetseiten von IIASA, in der App und den Social Media Kanälen der Kampagne veröffentlicht werden darf. IIASA verwendet diese Daten in Verbindung mit den hochgeladenen Fotos oder der Bestenliste inklusive meiner gesammelten Belohnungen und Anzahl der abgeschlossenen Quests in der App und auf folgenden Internetseiten, die von IIASA betreut werden:\n\n- www.Geo-Wiki.org\n- www.Fotoquest-go.org\n- www.LandSense.eu\n- www.iiasa.ac.at\nSowie auch auf unseren Social Media Kanälen:\n- Facebook (www.facebook.com, geführt von Facebook, Inc., 1601 Willow Road, Menlo Park, California 94025; und Facebook Ireland Ltd, 4 Grand Canal Square, Grand Canal Harbour, Dublin 2 Ireland)\n- Twitter (www.twitter.com/, geführt von Twitter International Company, One Cumberland Place, Fenian Street Dublin 2, D02 AX07 Ireland; and 1355 Market Street, Suite 900, San Francisco, CA 94103, United States)\n\nFalls Sie nicht möchten, dass Ihr echter Name auf dem Leaderboard erscheint, verwenden Sie bitte einen erfundenen Nicknamen, oder ein Pseudonym als Benutzername.";
		} else {
			m_LoadingText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "One moment...";
		}


        m_ToggleAgree.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TermsAcceptText");

        m_ToggleAgreeText2.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TermsAcceptText2");


		hideTerms ();
		hideTerms2 ();

		Debug.Log ("Start");
		string userid = PlayerPrefs.GetString ("PlayerId");
		Debug.Log ("userid: " + userid);
		if (userid == null) {
			Debug.Log ("No user set");
		} else if (userid.Length <= 0) {
			Debug.Log ("No user set 2");
		}
		/*
		Debug.Log ("Has key: " + PlayerPrefs.HasKey ("nrquests"));
		int nrqueststest = PlayerPrefs.GetInt ("nrquests");
		Debug.Log ("nrqueststest: " + nrqueststest);
		PlayerPrefs.SetInt ("nrquests", 1111);
		PlayerPrefs.Save ();*/

		updateStates ();


		//	messageBox = UIUtility.Find<MessageBox> ("MessageBox");
		//	verticalMessageBox = UIUtility.Find<MessageBox> ("VerticalMessageBox");

		/*
			messageBox.Show(title,message,icon,null,options);
		*/


		if (messageBox == null) {
			Debug.Log ("No message box set");
		} else {
			Debug.Log ("Message set");
		}


	}


	void OnGUI () {
//		windowRect = GUI.Window (0, windowRect, WindowFunction, "My Window");
	}



	void WindowFunction (int windowID) {
		// Draw any Controls inside the window here
	}

	bool m_bShown = false;


	void UpdateScaler()
	{
		if (Screen.width > Screen.height) {
			m_Scaler.referenceResolution = new Vector2 (Screen.width * 1.5f, 700);
		} else {
			m_Scaler.referenceResolution = new Vector2 (800, 700);
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("StartScreen");
		}

		UpdateScaler ();
		UpdateBackgroundImage ();

		/*
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;*/
		/*
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel ("DemoMap");*/
		
		/*m_Show++;
		if (m_Show >= 3 && !m_bShown) {
			if (Application.systemLanguage == SystemLanguage.German) {
				string[] options = { "OK" };
				messageBox.Show ("", "Verwende deinen existierenden Geo-Wiki (www.geo-wiki.org) Account, oder erzeuge einen neuen, um dich bei FotoQuest einzuloggen. Bitte beachte, dass dein Benutzername im Leaderboard erscheinen wird, das öffentlich zugänglich ist.", options);
			} else {
				string[] options = { "OK" };
				messageBox.Show ("", "Use your existing Geo-Wiki Account (www.geo-wiki.org) or create a new one to login. Please note that your username will be publicly visible in the leaderboard.", options);
			}
			m_bShown = true;
		}*/
	}

	public void updateStates() {
		if (Application.systemLanguage == SystemLanguage.German ) {
			//m_ToggleAgreeText2.GetComponent<UnityEngine.UI.Text> ().text = "Bitte setze dieses Häkchen, wenn du zustimmst, dass dein Benutzername auf den unten genannten Internetseiten von IIASA, in der App und den Social Media Kanälen veröffentlicht werden darf:\n\n- www.Geo-Wiki.org\n- www.LandSense.eu\n- www.iiasa.ac.at\n\nSowie auch auf unseren Social Media Kanälen:\n- Facebook (www.facebook.com, geführt von Facebook, Inc., 1601 Willow Road, Menlo Park, California 94025; und Facebook Ireland Ltd, 4 Grand Canal Square, Grand Canal Harbour, Dublin 2 Ireland)\n- Twitter (www.twitter.com/, geführt von Twitter International Company, One Cumberland Place, Fenian Street Dublin 2, D02 AX07 Ireland; and 1355 Market Street, Suite 900, San Francisco, CA 94103, United States)";

			m_TextTitle.GetComponentInChildren<UnityEngine.UI.Text>().text = "Registrieren";

			m_ButtonLogin.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnRegister");//"EINLOGGEN";
			m_TextName.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("LoginName");
			m_TextEMail.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("LoginMail");//"E-Mail:";
			m_TextPassword.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("LoginPassword");//"Passwort:";
			m_TextPasswordRepeat.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("RegisterRepeatPassword");//"Passwort:";

			m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Back");//"Zurück";

			m_LoadingText.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Loading");//"Laden...";


			/*

			m_ButtonLogin.GetComponentInChildren<UnityEngine.UI.Text>().text = "Registrieren";
			m_TextName.GetComponentInChildren<UnityEngine.UI.Text>().text = "Name:";
			m_TextEMail.GetComponent<UnityEngine.UI.Text> ().text = "E-Mail:";
			m_TextPassword.GetComponent<UnityEngine.UI.Text> ().text = "Passwort:";
			m_TextPasswordRepeat.GetComponent<UnityEngine.UI.Text> ().text = "Wiederhole\nPasswort:";*/

			m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text>().text = "Zurück";

			m_TermsTitle.GetComponentInChildren<UnityEngine.UI.Text>().text = "Teilnahmebedingungen";


			m_TermsBtnAccept.GetComponentInChildren<UnityEngine.UI.Text>().text = "ANNEHMEN";
			m_TermsBtnDecline.GetComponentInChildren<UnityEngine.UI.Text>().text = "ABLEHNEN";
			//m_ToggleAgree.GetComponentInChildren<UnityEngine.UI.Text>().text = "Ich stimme zu, die Picture Pile Terms of Use und Privacy Policy gelesen und akzeptiert zu haben. Verantwortlich für die ordnungsgemäße Verwendung dieser Daten ist das Internationale Institut für angewandte Systemanalyse (IIASA) in Laxenburg, Österreich. Diese Zustimmungserklärung kann jederzeit, jedoch nicht rückwirkend bei picturepile@iiasa.ac.at widerrufen werden.";

			m_BtnTerms.GetComponentInChildren<UnityEngine.UI.Text>().text = "Terms of Use";
			m_BtnDataPolicy.GetComponentInChildren<UnityEngine.UI.Text>().text = "Privacy Policy";

			m_NewsletterToggle.GetComponentInChildren<UnityEngine.UI.Text>().text = "Bitte setze dieses Häkchen, wenn du unseren Geo-Wiki Newsletter bekommen möchtest. Du bekommst dann Infos zu unseren laufenden und auch neuen Kampagnen, Publikationen und Forschung.";
			m_NewsletterButtonAccept.GetComponentInChildren<UnityEngine.UI.Text>().text = "WEITER";
			m_NewsletterButtonReject.GetComponentInChildren<UnityEngine.UI.Text>().text = "ZURÜCK";


            m_ToggleAgree.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TermsAcceptText");

            m_ToggleAgreeText2.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TermsAcceptText2");


			m_TermsBtnAccept2.GetComponentInChildren<UnityEngine.UI.Text>().text = "WEITER";
			m_TermsBtnDecline2.GetComponentInChildren<UnityEngine.UI.Text>().text = "ZURÜCK";
		} else {
            m_TextTitle.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Register");

			/*m_ButtonLogin.GetComponentInChildren<UnityEngine.UI.Text>().text = "Register";
			m_TextName.GetComponentInChildren<UnityEngine.UI.Text>().text = "Name:";
			m_TextEMail.GetComponent<UnityEngine.UI.Text> ().text = "E-Mail:";
			m_TextPassword.GetComponent<UnityEngine.UI.Text> ().text = "Password:";
			m_TextPasswordRepeat.GetComponent<UnityEngine.UI.Text> ().text = "Repeat\npassword:";
			m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text>().text = "Back";
*/

			m_ButtonLogin.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnRegister");//"EINLOGGEN";
			m_TextName.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("LoginName");
			m_TextEMail.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("LoginMail");//"E-Mail:";
			m_TextPassword.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("LoginPassword");//"Passwort:";
			m_TextPasswordRepeat.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("RegisterRepeatPassword");//"Passwort:";

			m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Back");//"Zurück";

			m_LoadingText.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Loading");//"Laden...";


			m_TermsTitle.GetComponentInChildren<UnityEngine.UI.Text>().text = "Terms and Conditions";

            m_TermsBtnAccept.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnAccept");//"ACCEPT";
            m_TermsBtnDecline.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnDecline");//"DECLINE";
			//m_ToggleAgree.GetComponentInChildren<UnityEngine.UI.Text>().text = "I declare that I have read and accepted the Picture Pile Terms of Use and Privacy Policy. Responsible for the proper use of these data is the International Institute for applied Systems Analysis (IIASA) in Laxenburg, Austria. This declaration can always be revoked, but not retrospectively, by contacting picturepile@iiasa.ac.at.";


            m_ToggleAgree.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TermsAcceptText");

            m_ToggleAgreeText2.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TermsAcceptText2");


            m_BtnTerms.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnTermsOfUse");//"Terms of Use";
            m_BtnDataPolicy.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnPrivacyPolicy");//"Privacy Policy";


            m_TermsBtnAccept2.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Next");//"NEXT";
            m_TermsBtnDecline2.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnBack");//"BACK";

            m_NewsletterToggle.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TermsNewsletter");//"Please tick this box if you want to receive our Geo-Wiki newsletter that includes updates on our current and future campaigns, publications and research.";
            m_NewsletterButtonAccept.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Next");//"NEXT";
            m_NewsletterButtonReject.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnBack");//"BACK";
		}

	}

	public static string ComputeHash(string s){
		// Form hash
		System.Security.Cryptography.MD5 h = System.Security.Cryptography.MD5.Create();
		byte[] data = h.ComputeHash(System.Text.Encoding.Default.GetBytes(s));
		// Create string representation
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
		for (int i = 0; i < data.Length; ++i) {
			sb.Append(data[i].ToString("x2"));
		}
		return sb.ToString();
	}

	public void LoginClicked () {
	/*	string[] options = { "OK" };
	//	messageBox.Show("Title","Message",null,null,options);
		messageBox.Show("Title","Message",options);
*/

		m_ErrorStep = 0;

		//messageBox.Show(
		Debug.Log("Register clicked");

		//string user = m_InputName.GetComponent<UnityEngine.UI.Text> ().text;
		//string mail = m_InputMail.GetComponent<UnityEngine.UI.Text> ().text;
	//	string password = m_InputPassword.GetComponent<UnityEngine.UI.InputField> ().text;
//		string passwordrepeat = m_InputPasswordRepeat.GetComponent<UnityEngine.UI.InputField> ().text;
		UnityEngine.UI.InputField inputfield = m_InputName.GetComponent<UnityEngine.UI.InputField> ();
		string user = inputfield.text;

		UnityEngine.UI.InputField inputfield2 = m_InputMail.GetComponent<UnityEngine.UI.InputField> ();
		string mail = inputfield2.text;


		UnityEngine.UI.InputField textinput;
		textinput = m_InputPassword.GetComponent<UnityEngine.UI.InputField>();
		string password = textinput.text;

		textinput = m_InputPasswordRepeat.GetComponent<UnityEngine.UI.InputField>();
		string passwordrepeat = textinput.text;

		string value = user + "," + password;
		string[] options = { "Ok" };
		//messageBox.Show ("", value, options);

        if (user.Length <= 0)
        {
            if (Application.systemLanguage == SystemLanguage.German && false)
            {
                messageBox.Show("", "Du hast keinen Name angegeben.", options);
            }
            else
            {
                messageBox.Show("", LocalizationSupport.GetString("RegisterNoName"), options);
            }
            return;
        }
        if (user.Length <= 3)
        {
            if (Application.systemLanguage == SystemLanguage.German && false)
            {
                messageBox.Show("", "Der eingegebene Name ist zu kurz. Verwende mindestens 4 Buchstaben.", options);
            }
            else
            {
                messageBox.Show("", LocalizationSupport.GetString("RegisterNameToShort"), options);
            }
            return;
        }
        if (mail.Length <= 0)
        {
            if (Application.systemLanguage == SystemLanguage.German && false)
            {
                messageBox.Show("", "Du hast keine Mail Adresse angegeben.", options);
            }
            else
            {
                messageBox.Show("", LocalizationSupport.GetString("RegisterNoMail"), options);
            }
            return;
        }

        if (password.Length <= 0)
        {
            if (Application.systemLanguage == SystemLanguage.German && false)
            {
                messageBox.Show("", "Du hast kein Passwort angegeben.", options);
            }
            else
            {
                messageBox.Show("", LocalizationSupport.GetString("RegisterNoPassword"), options);
            }
            return;
        }
		if (password.Length < 6)
		{
			if (Application.systemLanguage == SystemLanguage.German && false)
			{
				messageBox.Show("", "Du hast kein Passwort angegeben.", options);
			}
			else
			{
				messageBox.Show("", LocalizationSupport.GetString("RegisterPasswordLength"), options);
			}
			return;
		}

		if (passwordrepeat.Length <= 0)
        {
            if (Application.systemLanguage == SystemLanguage.German && false)
            {
                messageBox.Show("", "Bitte wiederhole das Passwort.", options);
            }
            else
            {
                messageBox.Show("", LocalizationSupport.GetString("RegisterNotRepeatedPassword"), options);
            }
            return;
        }
        if (password.Equals(passwordrepeat) == false)
        {
            if (Application.systemLanguage == SystemLanguage.German && false)
            {
                messageBox.Show("", "Deine eingegebenen Passwörter stimmen nicht überein.", options);
            }
            else
            {
                messageBox.Show("", LocalizationSupport.GetString("RegisterPasswordsDontMatch"), options);
            }
            return;
        }

		/*
		if (m_ToggleAgree.GetComponent<Toggle> ().isOn == false) {
			if (Application.systemLanguage == SystemLanguage.German) {
				messageBox.Show ("", "Du musst zuerst die Teilnahmebedingungen und Datenschutzrichtlinien akzeptieren.", options);
			} else {
				messageBox.Show ("", "You need to accept the terms and conditions and data policy usage in order to be able to register.", options);
			}
			return;
		}*/

		//	startRegistering ();
		//showTerms ();
		startRegistering();
	}

	public void hideTerms()
	{
		m_TermsBack.SetActive (false);
		m_TermsTitle.SetActive (false);
		m_TermsScrollbarAT.SetActive (false);
		m_TermsTextBack.SetActive (false);
		m_TermsImageAT.SetActive (false);
		m_TermsBtnAccept.SetActive (false);
		m_TermsBtnDecline.SetActive (false);
		m_TermsScrollbarEN.SetActive (false);
		m_TermsImageEN.SetActive (false);
		m_ToggleAgree.SetActive (false);

		m_BtnTerms.SetActive (false);
		m_BtnDataPolicy.SetActive (false);
	}
	void showTerms()
	{
		m_TermsBack.SetActive (true);
	//	m_TermsTitle.SetActive (true);

		/*if (Application.systemLanguage == SystemLanguage.German ) {
			m_TermsScrollbarAT.SetActive (true);
			m_TermsImageAT.SetActive (true);
			m_TermsScrollbarEN.SetActive (false);
			m_TermsImageEN.SetActive (false);
		} else {
			m_TermsScrollbarAT.SetActive (false);
			m_TermsImageAT.SetActive (false);
			m_TermsScrollbarEN.SetActive (true);
			m_TermsImageEN.SetActive (true);
		}*/

		m_ToggleAgree.SetActive (true);
		//m_TermsTextBack.SetActive (true);
		m_TermsBtnAccept.SetActive (true);
		m_TermsBtnDecline.SetActive (true);
		m_BtnTerms.SetActive (true);
		m_BtnDataPolicy.SetActive (true);
	}



	public void hideTerms2()
	{
		m_TermsBack2.SetActive (false);
		m_ToggleAgree2.SetActive (false);
		m_TermsBtnAccept2.SetActive (false);
		m_TermsBtnDecline2.SetActive (false);
		m_PanelAgree2.SetActive (false);
	}
	void showTerms2()
	{
		m_TermsBack2.SetActive (true);
		m_ToggleAgree2.SetActive (true);
		m_TermsBtnAccept2.SetActive (true);
		m_TermsBtnDecline2.SetActive (true);
		m_PanelAgree2.SetActive (true);
	}




	public void acceptedTerms()
	{
		string[] options = { "Ok" };
		if (m_ToggleAgree.GetComponent<Toggle> ().isOn == false) {
			if (Application.systemLanguage == SystemLanguage.German) {
                messageBox.Show ("", LocalizationSupport.GetString("MsgTermsAccept"), options);
			} else {
                messageBox.Show ("", LocalizationSupport.GetString("MsgTermsAccept"), options);
			}
			return;
		}

		hideTerms ();

		showTerms2 ();
	}


	void OnDontAcceptTerms2Clicked(string result) {
		Debug.Log ("OnDontAcceptTerms2Clicked: " + result);
        if (result == LocalizationSupport.GetString("BtnNo")/*"No" || result == "Nein"*/) {
			acceptedTerms2Ended ();
		}
	}
	public void acceptedTerms2()
	{
		/*string[] options = { "Ok" };
		if (m_ToggleAgree2.GetComponent<Toggle> ().isOn == false) {
			if (Application.systemLanguage == SystemLanguage.German) {
				messageBox.Show ("", "Du musst den Bedingungen zustimmen indem du ein Häckchen in dem Ankreuzfeld setzt.", options);
			} else {
				messageBox.Show ("", "You need to agree to the terms by ticking the checkbox.", options);
			}
			return;
		}*/

		if (m_ToggleAgree2.GetComponent<Toggle> ().isOn == false) {
			UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnDontAcceptTerms2Clicked);
			if (Application.systemLanguage == SystemLanguage.German) {
                string[] options = { LocalizationSupport.GetString("BtnBack"), LocalizationSupport.GetString("BtnNo") };
                messageBox.Show ("", LocalizationSupport.GetString("MsgReallyNoUsername"), ua, options);
			} else {
                string[] options = { LocalizationSupport.GetString("BtnBack"), LocalizationSupport.GetString("BtnNo") };
                messageBox.Show ("", LocalizationSupport.GetString("MsgReallyNoUsername"), ua, options);
			}
			return;
		}

		acceptedTerms2Ended ();
	}
	void acceptedTerms2Ended()
	{
		hideTerms2 ();

		m_NewsletterBack.SetActive (true);
		m_NewsletterToggle.SetActive (true);
		m_NewsletterButtonAccept.SetActive (true);
		m_NewsletterButtonReject.SetActive (true);
	}


	public void declineTerms2()
	{
		hideTerms2 ();
	}

	public void newsletterNext()
	{
		if (m_NewsletterToggle.GetComponent<Toggle> ().isOn == false) {
			UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (newsletterReally);
            if (Application.systemLanguage == SystemLanguage.German ) {
                string[] options = { LocalizationSupport.GetString("BtnBack"), LocalizationSupport.GetString("BtnNo") };
                messageBox.Show ("", LocalizationSupport.GetString("MsgReallyNoNewsletter"), ua, options);
			} else {
                string[] options = { LocalizationSupport.GetString("BtnBack"), LocalizationSupport.GetString("BtnNo") };
                messageBox.Show ("", LocalizationSupport.GetString("MsgReallyNoNewsletter"), ua, options);
			}
			return;
		}

		startRegistering ();
	}

	public void newsletterBack()
	{
		m_NewsletterBack.SetActive (false);
		m_NewsletterToggle.SetActive (false);
		m_NewsletterButtonAccept.SetActive (false);
		m_NewsletterButtonReject.SetActive (false);
		showTerms2 ();
	}


	void newsletterReally(string result) {
        if (result == LocalizationSupport.GetString("BtnNo")) {
			startRegistering ();
		}
	}

	public void startRegistering()
	{
		m_NewsletterBack.SetActive (false);
		m_NewsletterToggle.SetActive (false);
		m_NewsletterButtonAccept.SetActive (false);
		m_NewsletterButtonReject.SetActive (false);

		/*m_LoadingBack.SetActive (false);
		m_LoadingText.SetActive (false);*/
		/*
		string[] options = { "OK" };
		if (Application.systemLanguage == SystemLanguage.German) {
			messageBox.Show ("", "Registrierung fehlgeschlagen. Versuche es bitte erneut.", options);
		} else {
			messageBox.Show ("", "Registration failed. Please try again.", options);
		}

		return;*/

		m_LoadingBack.SetActive (true);
		m_LoadingText.SetActive (true);

		m_ErrorStep = 0;

		//messageBox.Show(
		Debug.Log("Register clicked");

		//string user = m_InputName.GetComponent<UnityEngine.UI.Text> ().text;
		//string mail = m_InputMail.GetComponent<UnityEngine.UI.Text> ().text;
		//	string password = m_InputPassword.GetComponent<UnityEngine.UI.InputField> ().text;
		//		string passwordrepeat = m_InputPasswordRepeat.GetComponent<UnityEngine.UI.InputField> ().text;
		UnityEngine.UI.InputField inputfield = m_InputName.GetComponent<UnityEngine.UI.InputField> ();
		string user = inputfield.text;

		UnityEngine.UI.InputField inputfield2 = m_InputMail.GetComponent<UnityEngine.UI.InputField> ();
		string mail = inputfield2.text;


		UnityEngine.UI.InputField textinput;
		textinput = m_InputPassword.GetComponent<UnityEngine.UI.InputField>();
		string password = textinput.text;

		textinput = m_InputPasswordRepeat.GetComponent<UnityEngine.UI.InputField>();
		string passwordrepeat = textinput.text;

		string value = user + "," + password;
		//messageBox.Show ("", value, options);


		Debug.Log (user + "," + mail + "," + password);

		string passwordmd5 = ComputeHash (password);

	
		string url = "https://server.org/api/account/register";
		string param = "";
		
	param = "{\"userName\":\"" + user + "\",\"emailAddress\":\"" + mail + "\",\"password\":\"" + password
	        + "\",\"appName\":\"AppName_App\"}";

		Debug.Log ("Params: " + param);

		m_UrlParams = param;

		StartCoroutine(Register(url, param));



	}
	
	class AcceptAnyCertificate : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData) => true;
	}

	IEnumerator Register(string url, string param)
	{
		Debug.Log("Register url: " + url);
		//UnityWebRequest.Post()
		//using (UnityWebRequest www = UnityWebRequest.Post(url, UnityWebRequest.kHttpVerbPOST/*, "POST"*/))//UnityWebRequest.Post(url, param))
		using (UnityWebRequest www = UnityWebRequest.Post(url, "POST")) //UnityWebRequest.Post(url, param))
		{
			byte[] bytes = Encoding.UTF8.GetBytes(param);
			//byte[] bytes = new System.Text.UTF8Encoding().GetBytes(param);
			www.uploadHandler = (UploadHandler) new UploadHandlerRaw(bytes);
			www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
			www.SetRequestHeader("Content-Type", "application/json");
			www.certificateHandler = new AcceptAnyCertificate();

			//  www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

//#if ASDFASFASDFASDFSDF
			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
			/*	Debug.Log("Error http error: " + www.error);

				messageBox.Show("", "Registering failed: " + www.error, options2);
				*/
			
				messageBox.Show("", "Registering failed: " + www.error, options2);
			
				m_LoadingText.SetActive(false);
				m_LoadingBack.SetActive(false);
			}
			else
			{
				string data = www.downloadHandler.text;

				Debug.Log("Registering successful result: " + data);

				JSONObject j = new JSONObject(data);
				m_ReadingWhich = -1;
				m_bValid = false;
				m_StrErrorText = "";
				accessRegistrationData(j);

				if(!m_bValid)
                {
					m_LoadingBack.SetActive(false);
					m_LoadingText.SetActive(false);

					messageBox.Show("", m_StrErrorText, options2);
					yield return www;
				}
				else
                {
					UnityEngine.UI.InputField textinput;
					textinput = m_InputPassword.GetComponent<UnityEngine.UI.InputField>();
					string password = textinput.text;
					PlayerPrefs.SetString("PlayerPassword", password);


					UnityEngine.UI.InputField inputfield2 = m_InputMail.GetComponent<UnityEngine.UI.InputField>();
					string mail = inputfield2.text;
					PlayerPrefs.SetString("PlayerMail", mail);


					UnityEngine.UI.InputField inputfieldname = m_InputName.GetComponent<UnityEngine.UI.InputField>();
					string username = inputfieldname.text;
					PlayerPrefs.SetString("PlayerName", username);

					Debug.Log("User saved name: " + username + " Password: " + password + " mail: " + mail);

					PlayerPrefs.SetInt("LoggedOut", 0);

					PlayerPrefs.Save();
					
					Debug.Log("Login successful");

					bool bDontGoToQuestsPage = false;
					if (PlayerPrefs.HasKey("LoginReturnToQuests"))
					{
						int returntoquests = PlayerPrefs.GetInt("LoginReturnToQuests");
						if (returntoquests == 1)
						{
							Application.LoadLevel("Quests");
							bDontGoToQuestsPage = true;
							yield return www;
						}
					}

					if (bDontGoToQuestsPage == false)
					{
						Application.LoadLevel("DemoMap");
					}
					else
					{
						m_LoadingBack.SetActive(false);
						m_LoadingText.SetActive(false);
					}
					yield return www;
				}
			}
//#endif
		}

#if ASDFASFASFSASADFSDF
		UnityEngine.WWWForm form = new UnityEngine.WWWForm();
		form.AddField("application/json", param);

		using (UnityWebRequest www = UnityWebRequest.Post(url, form))//UnityWebRequest.Post(url, param))
		{
			www.SetRequestHeader("Accept", "application/json");
			www.uploadHandler.contentType = "application/json";
			www.SetRequestHeader("Content-Type", "application/json");
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();


			//  www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error: " + www.error);

				messageBox.Show("", "Registering failed: " + www.error, options2);
			}
			else
			{
				string data = www.downloadHandler.text;

				Debug.Log("Registering successful result: " + data);
				/*

				JSONObject j = new JSONObject(data);
				m_ReadingWhichItem = -1;
				m_ItemId = "";
				accessContentItem(j);
				Debug.Log("m_ItemId: " + m_ItemId);

				*/
			}
		}
#endif

#if ASDFASFSFSF
		using (UnityWebRequest www = UnityWebRequest.Post(url, "POST"))//UnityWebRequest.Post(url, param))
		{
			byte[] bytes = Encoding.UTF8.GetBytes(param);

			UnityEngine.WWWForm form = new UnityEngine.WWWForm();
			form.AddField("application/json", param);

			//byte[] bytes = new System.Text.UTF8Encoding().GetBytes(param);
			www.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.SetRequestHeader("Content-Type", "application/json");


			//  www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error: " + www.error);

				messageBox.Show("", "Registering failed: " + www.error, options2);
			}
			else
			{
				string data = www.downloadHandler.text;

				Debug.Log("Registering successful result: " + data);
				/*

				JSONObject j = new JSONObject(data);
				m_ReadingWhichItem = -1;
				m_ItemId = "";
				accessContentItem(j);
				Debug.Log("m_ItemId: " + m_ItemId);

				*/
			}
		}
#endif
	}


	int m_ReadingWhich;
	bool m_bValid;
	string m_StrErrorText = "";

	void accessRegistrationData(JSONObject obj)
	{
		switch (obj.type)
		{
			case JSONObject.Type.OBJECT:
				for (int i = 0; i < obj.list.Count; i++)
				{
					string key = (string)obj.keys[i];
					JSONObject j = (JSONObject)obj.list[i];

					if (key == "isValid")
					{
						m_ReadingWhich = 1;
					}
					else if (key == "errorMessage")
					{
						m_ReadingWhich = 2;
					} else
                    {
						m_ReadingWhich = -1;
                    }
					accessRegistrationData(j);
				}
				break;
			case JSONObject.Type.ARRAY:
				//  Debug.Log ("Array");
				foreach (JSONObject j in obj.list)
				{
					accessRegistrationData(j);
				}
				break;
			case JSONObject.Type.STRING:
				if (m_ReadingWhich == 2)
				{
					m_StrErrorText = obj.str;
				}
				break;
			case JSONObject.Type.NUMBER:
				

				break;
			case JSONObject.Type.BOOL:
				if(m_ReadingWhich == 1)
                {
					m_bValid = obj.b;
                }
				break;
			case JSONObject.Type.NULL:
				//  Debug.Log("NULL");
				break;
		}
	}



	IEnumerator WaitForData(WWW www)
	{
		
		yield return www;

		string[] options = { "Ok" };

		// check for errors
		if (www.error == null)
		{
			string data = www.text;
			//string[] parts = data.Split (":", 2);

			Debug.Log ("Registering result: " + data);

			/*
			string[] parts = data.Split(new string[] { ":" }, 0);
			string[] parts2 = parts[1].Split(new string[] { "," }, 0);
			string part3 = parts2 [0];

			Debug.Log("WWW Ok!: " + www.text);
			Debug.Log("part1: " + parts[0]);
			Debug.Log("part2: " + parts[1]);
			Debug.Log("part3: " + part3);

			part3 = part3.Replace ("\"", "");
			part3 = part3.Replace ("}", "");


			if (part3.Equals ("null")) {
				m_LoadingBack.SetActive (false);
				m_LoadingText.SetActive (false);
				if (Application.systemLanguage == SystemLanguage.German) {
					messageBox.Show ("", "Registrierung fehlgeschlagen. Versuche es bitte erneut.", options);
				} else {
                    messageBox.Show("", LocalizationSupport.GetString("RegisterFailed"), options);
				}
				yield return www;
			} else {
				PlayerPrefs.SetString("PlayerId",part3);


				UnityEngine.UI.InputField textinput;
				textinput = m_InputPassword.GetComponent<UnityEngine.UI.InputField>();
				string password = textinput.text;

				PlayerPrefs.SetString("PlayerPassword",password);


				UnityEngine.UI.InputField inputfield2 = m_InputMail.GetComponent<UnityEngine.UI.InputField> ();
				string mail = inputfield2.text;
				//string mail = m_InputMail.GetComponent<UnityEngine.UI.Text> ().text;
				PlayerPrefs.SetString("PlayerMail",mail);


				UnityEngine.UI.InputField inputfieldname = m_InputName.GetComponent<UnityEngine.UI.InputField> ();
				string username = inputfieldname.text;
				PlayerPrefs.SetString("PlayerName",username);

				PlayerPrefs.SetInt ("TermsAndConditionsAccepted", 1);


				PlayerPrefs.SetInt ("LoggedOut", 0);

				PlayerPrefs.Save ();

				Debug.Log ("saved login mail: " + mail + " password: " + password + " playerid: " + part3);


				bool bDontGoToQuestsPage = false;
				if (PlayerPrefs.HasKey ("LoginReturnToQuests")) {
					int returntoquests = PlayerPrefs.GetInt ("LoginReturnToQuests");
					if (returntoquests == 1) {
						Application.LoadLevel ("Quests");
						bDontGoToQuestsPage = true;
						yield return www;
					}
				}

				if (bDontGoToQuestsPage == false) {
					Application.LoadLevel ("DemoMap");
				} else {
					m_LoadingBack.SetActive (false);
					m_LoadingText.SetActive (false);
				}
				yield return www;
			}
			*/

		} else {
			Debug.Log("WWW Error: "+ www.error);
			Debug.Log("WWW Error 2: "+ www.text);
			m_StrError = www.text;

			m_LoadingBack.SetActive (false);
			m_LoadingText.SetActive (false);

			if (Application.systemLanguage == SystemLanguage.German) {
				messageBox.Show ("","Registrierung fehlgeschlagen: " + m_StrError, options);
			} else {
				messageBox.Show ("", "Registration not successful: " + m_StrError, options);
			}
		}   
		yield return www;
	} 


	IEnumerator WaitForLoginData(WWW www)
	{
		yield return www;
		string[] options = { "Ok" };

		if (www.error == null)
		{
			string data = www.text;

			int playerid = int.Parse (data);

			if (playerid != -1) {
				PlayerPrefs.SetString ("PlayerId", "" + playerid);
				PlayerPrefs.SetInt ("LoggedOut", 0);
				PlayerPrefs.Save ();

				Application.LoadLevel ("Piles");
			} else {
				m_LoadingText.SetActive (false);
				m_LoadingBack.SetActive (false);

				if (Application.systemLanguage == SystemLanguage.German) {
					messageBox.Show ("", LocalizationSupport.GetString("LoginFailed"), options);
				} else {
					messageBox.Show ("", LocalizationSupport.GetString("LoginFailed"), options);
				}
				yield return www;
			}
		} else {
			Debug.Log("WWW Error: "+ www.error);
			Debug.Log("WWW Error 2: "+ www.text);


			m_LoadingText.SetActive (false);
			m_LoadingBack.SetActive (false);

			if (Application.systemLanguage == SystemLanguage.German) {
				messageBox.Show ("", LocalizationSupport.GetString("LoginFailedNoInternet"), options);
			} else {
				messageBox.Show ("", LocalizationSupport.GetString("LoginFailedNoInternet"), options);
			}
		}   
	} 

	public void OnErrorNextClicked() {
		if (m_ErrorStep == 0) {
			m_ErrorText.GetComponentInChildren<UnityEngine.UI.Text> ().text = m_StrError;
			m_ErrorStep++;
		} else if (m_ErrorStep == 1) {
			m_ErrorText.GetComponentInChildren<UnityEngine.UI.Text> ().text = m_UrlParams;
			m_ErrorStep++;
		} else {
			m_ErrorText.SetActive (false);
			m_ErrorBack.SetActive (false);
			m_ErrorButton.SetActive (false);
		}
	}


	public void RegisterClicked () {
	}
	public void OnBackClicked () {
		Application.LoadLevel ("StartScreen");
	}
	public void OnForgotClicked () {
		Application.OpenURL("http://www.geo-wiki.org/Security/lostpassword");
	}
	public void OnOpenTerms () {
	/*	if (Application.systemLanguage == SystemLanguage.German) {
			Application.OpenURL("https://www.geo-wiki.org/assets/legal/FotoQuestGO/1.0/DE/terms-of-use.html");
		} else {
			Application.OpenURL("https://www.geo-wiki.org/assets/legal/FotoQuestGO/1.0/EN/terms-of-use.html");
		}*/
		if (Application.systemLanguage == SystemLanguage.German) {
			Application.OpenURL("http://www.fotoquest-go.org/de/terms-of-use/");
		} else {
			Application.OpenURL("http://www.fotoquest-go.org/en/terms-of-use/");
		}
	}
	public void OnOpenDataPolicy () {
		/*if (Application.systemLanguage == SystemLanguage.German) {
			Application.OpenURL("https://www.geo-wiki.org/assets/legal/FotoQuestGO/1.0/DE/privacy-statement.html");
		} else {
			Application.OpenURL("https://www.geo-wiki.org/assets/legal/FotoQuestGO/1.0/EN/privacy-statement.html");
		}*/
		if (Application.systemLanguage == SystemLanguage.German) {
			Application.OpenURL("http://www.fotoquest-go.org/de/privacy-policy/");
		} else {
			Application.OpenURL("http://www.fotoquest-go.org/en/privacy-policy/");
		}
	}
}
