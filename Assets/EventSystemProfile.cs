using System;
using UnityEngine;
using System.Collections;
using Unitycoding.UIWidgets;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EventSystemProfile : MonoBehaviour {

	public float m_WebScalerX = 1.5f;
	public float m_WebScalerY = 800.0f;
	
	//public GameObject m_BackImage;
	public MessageBox messageBox;
	public GameObject m_Button;
	public GameObject m_HelloName;
	//public GameObject m_Progress;
	public GameObject m_NrSurveysLeft;
	public GameObject m_NrSurveys;
	public GameObject m_ButtonRedeemTokens;
	public GameObject m_ButtonRedemptions;
	public GameObject m_UserName;
	public GameObject m_FirstName;
	public GameObject m_LastName;
	public GameObject m_Hometown;
	public GameObject m_Age;
	public GameObject m_Gender;
	public GameObject m_Interests;
	public GameObject m_Toggle1;
	public GameObject m_Toggle2;
	public GameObject m_Toggle3;
	public GameObject m_Toggle4;
	public GameObject m_Toggle5;
	public GameObject m_Toggle6;
	public GameObject m_Toggle7;
	public GameObject m_Toggle8;
	public GameObject m_Toggle9;
	public GameObject m_Toggle10;
	public GameObject m_Toggle11;
	public GameObject m_Toggle12;
	public GameObject m_InputFirstName;
	public GameObject m_InputLastName;
	public GameObject m_InputHometown;
	public GameObject m_ComboGender;
	public GameObject m_ComboAge;
	public GameObject m_GOTotalScore;
	public GameObject m_GOScore;
	public GameObject m_GOTotalScoreLeft;
	public GameObject m_GOScoreLeft;
	public GameObject m_GOYomaBalanceLeft;
	public GameObject m_GOYomaBalance;

	public GameObject m_LableShowUsername;
	public GameObject m_ToggleShowUsername;

	/*public GameObject m_LevelTitle;
	public GameObject m_LevelText;
	public GameObject m_LevelBar;
*/
	public GameObject m_BtnBack;

	

	public GameObject m_TextUploading;
	public GameObject m_ImageTextUploading;
	public GameObject m_TextCommentAdditionalInfo;


/*
	public GameObject m_ImageCheckInternetPortrait;
	public GameObject m_ImageCheckInternetLandscape;
	public GameObject m_TextCheckInternet;
	public GameObject m_BtnBackCheckInternet;
	public GameObject m_ImageBackCheckInternet;
	public GameObject m_BtnCheckInternet;
*/

	/*public CanvasScaler m_Scaler;
	public GameObject m_BackgroundPortrait;
	public GameObject m_BackgroundLandscape;
*/
	IEnumerator changeFramerate() {
		yield return new WaitForSeconds(1);
		Application.targetFrameRate = 60;
	}


	public void ForceAutoRotate()
	{
		StartCoroutine(ForceAndFixAutoRotate());
	}

	IEnumerator ForceAndFixAutoRotate()
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


/*	void UpdateBackgroundImage()
	{
		if (Screen.width > Screen.height) {
			m_BackgroundPortrait.SetActive (false);
			m_BackgroundLandscape.SetActive (true);
		} else {
			m_BackgroundPortrait.SetActive (true);
			m_BackgroundLandscape.SetActive (false);
		}
	}*/

	//------------------------------
	// Deep linking
	void Awake()
	{
		Application.deepLinkActivated += onDeepLinkActivated;
	}
	void OnDestroy()
	{
		Application.deepLinkActivated -= onDeepLinkActivated;
	}
	private void onDeepLinkActivated(string url)
	{
		PlayerPrefs.SetString("AbsoluteURL", Application.absoluteURL);
		PlayerPrefs.SetInt("AbsoluteURLSet", 1);
		PlayerPrefs.Save();
		Application.LoadLevel ("Piles");
	}
	//------------------------------
	
	// Use this for initialization
	void Start () {
		StartCoroutine(changeFramerate());

		/*#if UNITY_WEBGL
		ForceAutoRotate ();
		#else 
		ForcePortrait ();
		#endif*/

		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();

		//UpdateScaler ();

	//	UpdateBackgroundImage ();

		m_TextUploading.SetActive (false);
		m_ImageTextUploading.SetActive (false);


		UnityEngine.UI.Dropdown dropdown;
		UnityEngine.UI.Dropdown.OptionData list;


		m_BtnBack.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("Back");

		m_UserName.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString ("ProfileUsername");//"Username:";


			m_TextCommentAdditionalInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Please provide us with some information about yourself. This info is only for us to further improve FotoQuest:";

			
			m_NrSurveysLeft.GetComponentInChildren<UnityEngine.UI.Text> ().text =LocalizationSupport.GetString ("ProfileNrSurveys");
			m_ButtonRedeemTokens.GetComponentInChildren<UnityEngine.UI.Text> ().text =LocalizationSupport.GetString ("ProfileRedeemTokens");
			m_ButtonRedemptions.GetComponentInChildren<UnityEngine.UI.Text>().text =
				LocalizationSupport.GetString("ProfileRedemtions");
			
			
			m_GOTotalScoreLeft.GetComponentInChildren<UnityEngine.UI.Text>().text =
				LocalizationSupport.GetString("ProfileNrSurveysAccepted");//ProfileScore");
			m_GOScoreLeft.GetComponentInChildren<UnityEngine.UI.Text>().text =
				LocalizationSupport.GetString("ProfileToken");
			m_GOYomaBalanceLeft.GetComponentInChildren<UnityEngine.UI.Text>().text =
				LocalizationSupport.GetString("ProfileYomaBalance");

			m_FirstName.GetComponentInChildren<UnityEngine.UI.Text> ().text =LocalizationSupport.GetString ("ProfileFirstname");// "First Name:";
			m_LastName.GetComponentInChildren<UnityEngine.UI.Text> ().text =LocalizationSupport.GetString ("ProfileLastname");// "Last Name:";
			m_Hometown.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileHometown");//"Hometown:";
			m_Age.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileAgeGroup");//"Age Group:";
			m_Gender.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileGender");//"Gender:";
			m_Interests.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileInterests");//"Interests:";


			m_Toggle1.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileInterestScience");//"Science";
			m_Toggle2.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileInterestSports");//"Sports";
			m_Toggle3.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileInterestNature");//"Nature";
			m_Toggle4.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileInterestBiology");//"Biology";
			m_Toggle5.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileInterestGeography");//"Geography";
			m_Toggle6.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileInterestMusic");//"Music";
			m_Toggle7.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileInterestComputers");//"Computers";
			m_Toggle8.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileInterestBooks");//"Books";
			m_Toggle9.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileInterestAgriculture");//"Agriculture";

//		m_BtnCheckInternet.GetComponentInChildren<UnityEngine.UI.Text> ().text =LocalizationSupport.GetString ("BtnCheck");// "First Name:";
//		m_TextCheckInternet.GetComponent<UnityEngine.UI.Text> ().text =LocalizationSupport.GetString ("CheckInternet");// "First Name:";


		m_LableShowUsername.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("ProfileShowUsername");

			dropdown = m_ComboGender.GetComponent<UnityEngine.UI.Dropdown>();
			dropdown.options.Clear ();
			list = new UnityEngine.UI.Dropdown.OptionData(LocalizationSupport.GetString ("ProfileSelect"));
			dropdown.options.Add (list);

			list = new UnityEngine.UI.Dropdown.OptionData(LocalizationSupport.GetString ("ProfileFemale"));
			dropdown.options.Add (list);
			list = new UnityEngine.UI.Dropdown.OptionData(LocalizationSupport.GetString ("ProfileMale"));
			dropdown.options.Add (list);
			dropdown.value = 0;


			UnityEngine.UI.InputField textinput;
			textinput = m_InputFirstName.GetComponent<UnityEngine.UI.InputField>();
			textinput.text = "";

			textinput = m_InputLastName.GetComponent<UnityEngine.UI.InputField>();
			textinput.text = "";

			textinput = m_InputHometown.GetComponent<UnityEngine.UI.InputField>();
			textinput.text = "";



			m_TextUploading.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("IsUploading");




		m_ComboGender.GetComponentInChildren<UnityEngine.UI.Text> ().text = "";
		m_ComboAge.GetComponentInChildren<UnityEngine.UI.Text> ().text = "";

		dropdown = m_ComboAge.GetComponent<UnityEngine.UI.Dropdown>();
		dropdown.options.Clear ();
		if (Application.systemLanguage == SystemLanguage.German && false) {
			list = new UnityEngine.UI.Dropdown.OptionData ("(Bitte auswählen)");
			dropdown.options.Add (list);
		} else {
			list = new UnityEngine.UI.Dropdown.OptionData (LocalizationSupport.GetString ("ProfileSelect"));
			dropdown.options.Add (list);
		}

		/*list = new UnityEngine.UI.Dropdown.OptionData("< 10");
		dropdown.options.Add (list);
		list = new UnityEngine.UI.Dropdown.OptionData("10-14");
		dropdown.options.Add (list);*/
		list = new UnityEngine.UI.Dropdown.OptionData("18-19");
		dropdown.options.Add (list);
		list = new UnityEngine.UI.Dropdown.OptionData("20-29");
		dropdown.options.Add (list);
		list = new UnityEngine.UI.Dropdown.OptionData("30-39");
		dropdown.options.Add (list);
		list = new UnityEngine.UI.Dropdown.OptionData("40-49");
		dropdown.options.Add (list);
		list = new UnityEngine.UI.Dropdown.OptionData("50-59");
		dropdown.options.Add (list);
		list = new UnityEngine.UI.Dropdown.OptionData("60+");
		dropdown.options.Add (list);
		dropdown.value = 0;



		updateStates ();
		loadBackgroundImage ();



		//messageBox = UIUtility.Find<MessageBox> ("MessageBox");


		UnityEngine.UI.Text text;
		string progressname = PlayerPrefs.GetString("PlayerName");//LocalizationSupport.GetString ("Hello");
		text = m_HelloName.GetComponent<UnityEngine.UI.Text>();
		text.text = progressname;


	/*	text = m_Progress.GetComponent<UnityEngine.UI.Text>();
		int sorting = PlayerPrefs.GetInt ("CurrentlySorting");
		text.GetComponentInChildren<UnityEngine.UI.Text>().text = "";*//*LocalizationSupport.GetString ("Loading") +
		                                                           "asdf";*/

		/*m_LevelTitle.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("Loading");
		m_LevelText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "";
		m_LevelBar.SetActive(false);
*/
		loadStatistics ();
       // loadProgress ();
        //loadAnonym();
		//loadSettings ();

	}


	/*void UpdateScaler()
	{
		if (Screen.width > Screen.height) {
			//m_Scaler.referenceResolution = new Vector2 (Screen.width * 1.5f, 700);
			m_Scaler.referenceResolution = new Vector2 (Screen.width * m_WebScalerX, m_WebScalerY);
		} else {
			m_Scaler.referenceResolution = new Vector2 (800, 700);
		}
	}*/

	void Update () {

		//if (Input.GetKeyDown(KeyCode.Escape)) 
		//	Application.LoadLevel ("DemoMap");
		//UpdateScaler();


		//
		if(m_bRequestPayout)
		{
			m_RequestPayoutIter++;
			if(m_RequestPayoutIter > 2)
			{
				m_bRequestPayout = false;
				m_TokenRedirect = 2;
				refreshToken();
				return;
			}
		}

//		UpdateBackgroundImage ();
	}

	private bool m_bInitToggles = false;
	int m_CurState = 0;
	public void updateStates() {

		m_Button.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString ("Back");
		
		m_bInitToggles = true;
		if (PlayerPrefs.GetInt("HideUsername") == 1)
		{
			m_ToggleShowUsername.GetComponent<Toggle>().isOn = false;
		} else
		{
			m_ToggleShowUsername.GetComponent<Toggle>().isOn = true;
		}
		m_bInitToggles = false;
	}

	public void NextClicked ()
	{
	/*	if (m_PaypalAddress != m_InputPayPal.GetComponent<InputField>().text)
		{
			SavePayPal();
		}
		else
		{*/
			OnBackClicked();
	//	}
	}



    public static string ComputeHash(string s){
        if (PlayerPrefs.GetInt("PlayerPasswordNoHash") == 1)
        {
            return s;
        }
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

	void loadStatistics() {
		/*Debug.Log (">> loadStatistics");

        string userid = PlayerPrefs.GetString ("PlayerId");
        string url = LocalizationSupport.GetString("ServerURL") + "getNrClassificationsDone/" + userid;
		WWW www = new WWW(url);
		StartCoroutine(waitForStatistics(www));*/

		m_TokenRedirect = 1;
		refreshToken();
	}
	
	int m_TokenRedirect = -1;
	
	void refreshToken()
	{
		if (!PlayerPrefs.HasKey("PlayerPassword") ||
		    !PlayerPrefs.HasKey("PlayerName"))
		{
		//	m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text>().text = "Load token error no user given";
			return;
		}
		
		string user = PlayerPrefs.GetString("PlayerName");
		string password = PlayerPrefs.GetString("PlayerPassword");
		string url = "https://server.org/connect/token";

		StartCoroutine(Login(url, user, password));
	}
	class AcceptAnyCertificate : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData) => true;
	}
	
	IEnumerator Login(string url, string user, string password)
	{
		WWWForm form = new WWWForm();
		form.AddField("client_id", "AppName_App");
		form.AddField("client_secret", "...");
		form.AddField("grant_type", "password");
		form.AddField("username", user);
		form.AddField("password", password);
		form.AddField("scope", "...");

		Debug.Log("Url: " + url);


		using (UnityWebRequest www = UnityWebRequest.Post(url, form))
		{
			www.SetRequestHeader("Content-Type", "application/x-www-form-urlencoded");
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error getting refresh token: " + www.error);
				string data = www.downloadHandler.text;
				Debug.Log("Data: " + data);
				Debug.Log("Errorstr: " + www.ToString());

				m_TextUploading.SetActive (false);
				m_ImageTextUploading.SetActive (false);
			}
			else
			{
				string data = www.downloadHandler.text;
				Debug.Log("Refresh token result: " + data);

				m_bReadingAccessToken = false;
				JSONObject j = new JSONObject(data);
				accessAccessToken(j);
				
				if(m_TokenRedirect == 1)
					loadProfileStats();
				else if (m_TokenRedirect == 2)
				{
					//LoadYomaTokenStatus(); // <<< no more check for now
					SaveYomaAddress();
				}
			}
		}
	}

	bool m_bReadingAccessToken = false;
	void accessAccessToken(JSONObject obj)
	{
		switch (obj.type)
		{
			case JSONObject.Type.OBJECT:
				for (int i = 0; i < obj.list.Count; i++)
				{
					string key = (string)obj.keys[i];
					JSONObject j = (JSONObject)obj.list[i];
					
					if (key == "access_token")
					{
						m_bReadingAccessToken = true;
					}
					accessAccessToken(j);
				}
				break;
			case JSONObject.Type.ARRAY:
				//	Debug.Log ("Array");
				foreach (JSONObject j in obj.list)
				{
					accessAccessToken(j);
				}
				break;
			case JSONObject.Type.STRING:
				if (m_bReadingAccessToken)
				{
					Debug.Log("Access token read: " + obj.str);
					PlayerPrefs.SetString("PPPToken", obj.str);
					m_bReadingAccessToken = false;
				}
				break;
			case JSONObject.Type.NUMBER:
				break;
			case JSONObject.Type.BOOL:
				break;
			case JSONObject.Type.NULL:
				break;

		}
	}

	void loadProfileStats()
	{
		string url = "https://server.org/user/score";

		Debug.Log("LoadPilesInfo: " + url);
		//WWW www = new WWW(url);
		StartCoroutine(WaitForProfileStats(url));
	}
	
	IEnumerator WaitForProfileStats(string url)
	{
		using (UnityWebRequest www = UnityWebRequest.Get(url))
		{
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();
			string token = PlayerPrefs.GetString("PPPToken");
			//www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error loading profile stats: " + www.error);
				string data = www.downloadHandler.text;
				Debug.Log("Data: " + data);
				Debug.Log("Errorstr: " + www.ToString());
				refreshToken();
			}
			else
			{
				string data = www.downloadHandler.text;
				Debug.Log("WWW Profile stats successfully loaded: " + data);
				
				JSONObject j = new JSONObject(data);
				m_ReadingWhich = -1;
				m_NrImagesSorted = "0";
				m_NrPilesSorted = "0";
				m_NrTimesAgreedWithExpert = 0;
				m_NrTimesDisagreedWithExpert = 0;

				m_TotalScore = 0.0f;
				m_Score = 0.0f;
				m_NrSurveysValue = 0.0f;
				m_YomaBalance = "0";//0.0f;
				accessData(j);
				
				m_GOTotalScore.GetComponent<UnityEngine.UI.Text>().text = "" + m_TotalScore;
				m_GOScore.GetComponent<UnityEngine.UI.Text>().text = "" + m_Score;
				m_NrSurveys.GetComponent<UnityEngine.UI.Text>().text = "" + m_NrSurveysValue;
				m_GOYomaBalance.GetComponent<UnityEngine.UI.Text>().text = "" + m_YomaBalance;

				LoadYomaAddress();
/*

				string nrpicturessorted = m_NrImagesSorted;

				string text = "";
				if (int.Parse(m_NrPilesSorted) > 1)
				{
					text =
						"You have sorted " + nrpicturessorted + " pictures in " +
						m_NrPilesSorted + " piles.";
				}
				else
				{
					text =
						"You have sorted " + nrpicturessorted + " pictures in " +
						m_NrPilesSorted + " pile.";
				}
*/
				/*text = text + 
				       "\nYou have agreed " + m_NrTimesAgreedWithExpert + " times and disagreed " 
				       + m_NrTimesDisagreedWithExpert + " times with an expert.";
*/
				/*text = text + "\nYour quality score is: " + m_QualityScore + " (";
				
				text = text + 
				       "" + m_NrTimesAgreedWithExpert + " expert agreements, " 
				       + m_NrTimesDisagreedWithExpert + " expert disagreements)";

				m_Progress.GetComponent<UnityEngine.UI.Text>().text = text;

				updateProgressValues (int.Parse (nrpicturessorted));
				loadUsername();*/
				//loadPayPal();
				//loadEarnings();
			}
		}

		yield return null;
	}
	
	void loadUsername()
	{
		string url = LocalizationSupport.GetString("DBUrl") + "profile/username";

		Debug.Log("LoadUsername: " + url);
		StartCoroutine(WaitForUsername(url));
	}
	
	IEnumerator WaitForUsername(string url)
	{
		using (UnityWebRequest www = UnityWebRequest.Get(url))
		{
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();
			string token = PlayerPrefs.GetString("PPPToken");
			//www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				
			}
			else
			{
				string username = www.downloadHandler.text;

			/*	UnityEngine.UI.Text text;
				string progressname = LocalizationSupport.GetString ("Hello") + " " + username + ",";
				text = m_HelloName.GetComponent<UnityEngine.UI.Text>();
				text.text = progressname;*/
			}
		}

		yield return null;
	}

	/*public GameObject m_InputPayPal;
	private string m_PaypalAddress = "";
	void loadPayPal()
	{
		string url = LocalizationSupport.GetString("DBUrl") + "user/payout/address";

		Debug.Log("Load PayPal: " + url);
		StartCoroutine(WaitForPayPal(url));
	}
	
	IEnumerator WaitForPayPal(string url)
	{
		using (UnityWebRequest www = UnityWebRequest.Get(url))
		{
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

			string token = PlayerPrefs.GetString("PPPToken");
			//www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				
			}
			else
			{
				string paypal = www.downloadHandler.text;

				m_InputPayPal.GetComponent<InputField>().text = paypal;
				m_PaypalAddress = paypal;
				Debug.Log("Paypal loaded: " + paypal);
			}
		}

		yield return null;
	}

	void SavePayPal()
	{
		string paypal = m_InputPayPal.GetComponent<InputField>().text;
		string url = LocalizationSupport.GetString("DBUrl") + "user/payout/address?paymentAddress=" + paypal;

		Hashtable postHeader = new Hashtable();
		postHeader.Add("Content-Type", "application/json");
		postHeader.Add("X-Requested-With", "XMLHttpRequest");
		string token = PlayerPrefs.GetString("PPPToken");
		postHeader.Add("Authorization", "Bearer " + token);

		string data = "{}";
		var formData = System.Text.Encoding.UTF8.GetBytes(data);
		WWW www = new WWW(url, formData, postHeader);

		StartCoroutine(waitForSavingPayPal(www));
	}

	IEnumerator waitForSavingPayPal(WWW www)
	{
		yield return www;
		if (www.error == null)
		{
			Debug.Log("Saved paypal");
			
			OnBackClicked();
		} else
		{
			Debug.Log("SAVING PAYPAL ERROR");
			Debug.Log("WWW Error: " + www.error);
			Debug.Log("WWW Error 2: " + www.text);
		}

		www.Dispose();
		Resources.UnloadUnusedAssets();
	}*/
	
	void loadEarnings()
	{
		string url = LocalizationSupport.GetString("DBUrl") + "user/payout/earnings";

		Debug.Log("Load PayPal: " + url);
		StartCoroutine(WaitForEarnings(url));
	}
	
	IEnumerator WaitForEarnings(string url)
	{
		using (UnityWebRequest www = UnityWebRequest.Get(url))
		{
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();
			string token = PlayerPrefs.GetString("PPPToken");
			//www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				string earnings = www.downloadHandler.text;
				Debug.Log("Earnings loaded: " + earnings);
			}
			else
			{
				string data = www.downloadHandler.text;
				Debug.Log("WWW earnings successfully loaded: " + data);
				
				JSONObject j = new JSONObject(data);
				m_ReadingWhichEarning = -1;
				accessDataEarning(j);

				m_TextEarning.GetComponent<Text>().text = "Earnings: " + m_Earnings + "€";
				m_bEarningsLoaded = true;
			}
		}

		yield return null;
	}

	private int m_ReadingWhichEarning = -1;
	private float m_Earnings = 0.0f;
	private bool m_bEarningsLoaded = false;
	public GameObject m_TextEarning;
	void accessDataEarning(JSONObject obj)
    {
        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];

                    if (key == "totalEarnedAmount")
                    {
	                    m_ReadingWhichEarning = 1;
                    }
                    else
                    {
                        m_ReadingWhichEarning = 0;
                    }
                    accessDataEarning(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                //  Debug.Log ("Array");
                foreach (JSONObject j in obj.list)
                {
	                accessDataEarning(j);
                }
                break;
            case JSONObject.Type.STRING:
                

                m_ReadingWhichEarning = 0;
                break;
            case JSONObject.Type.NUMBER:
	            if (m_ReadingWhichEarning == 1)
	            {
		            m_Earnings = obj.n;
	            }

	            m_ReadingWhichEarning = 0;
                break;
            case JSONObject.Type.BOOL:
                break;
            case JSONObject.Type.NULL:
                break;

        }
    }

	int m_ReadingWhich = -1;
    string m_NrImagesSorted = "";
    string m_NrPilesSorted = "";
    private float m_TotalScore = 0.0f;
    private float m_Score = 0.0f;
    private float m_NrSurveysValue = 0.0f;
    private string m_YomaBalance = "0";//0.0f;
    private float m_QualityScore = 1.0f;
    private int m_NrTimesAgreedWithExpert = 0;
    private int m_NrTimesDisagreedWithExpert = 0;
    void accessData(JSONObject obj)
    {
        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];

                   /* if (key == "totalSortedImage")
                    {
	                    m_ReadingWhich = 1;
                    }
                    else if (key == "totalPiles")
                    {
	                    m_ReadingWhich = 2;
                    }
                    else if (key == "aggregateAvgQualityScore")
                    {
	                    m_ReadingWhich = 3;
                    }
                    else if (key == "totalImagesAgreeWithExpert")
                    {
	                    m_ReadingWhich = 4;
                    }
                    else if (key == "totalImagesDisagreeWithExpert")
                    {
	                    m_ReadingWhich = 5;
                    }
                    else
                    {
                        m_ReadingWhich = 0;
                    }*/
                   
                   if(key == "acceptedSurveyCount")//"score")
                   {
	                   m_ReadingWhich = 1;
                   }
                   else if (key == "userTokens")
                   {
	                   m_ReadingWhich = 2;
                   }
                   else if (key == "surveyCount")
                   {
	                   m_ReadingWhich = 3;
                   }
                   else if (key == "userTokenBalance")
                   {
	                   m_ReadingWhich = 4;
                   }
                   else
                   {
	                   m_ReadingWhich = -1;
                   }
                    accessData(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                //  Debug.Log ("Array");
                foreach (JSONObject j in obj.list)
                {
                    accessData(j);
                }
                break;
            case JSONObject.Type.STRING:
               /* if (m_ReadingWhich == 1)
                {
                    m_NrImagesSorted = obj.str;
                }*/
               
               if (m_ReadingWhich == 4)
               {
	               m_YomaBalance = obj.str;
               }
                break;
            case JSONObject.Type.NUMBER:
	            /*if (m_ReadingWhich == 1)
	            {
		            m_NrImagesSorted = "" + obj.n;
		            //   m_Pins[m_CurrentPin].m_Weight = "" + obj.n;
	            }
	            else if (m_ReadingWhich == 2)
	            {
		            m_NrPilesSorted = "" + obj.n;
	            }
	            else if (m_ReadingWhich == 3)
	            {
		            m_QualityScore = obj.n;
	            }
	            else if (m_ReadingWhich == 4)
	            {
		            m_NrTimesAgreedWithExpert = (int)obj.n;
	            }
	            else if (m_ReadingWhich == 5)
	            {
		            m_NrTimesDisagreedWithExpert = (int)obj.n;
	            }
*/
	            if (m_ReadingWhich == 1)
	            {
		            m_TotalScore = obj.n;
	            } else if (m_ReadingWhich == 2)
	            {
		            m_Score = obj.n;
	            } else if (m_ReadingWhich == 3)
	            {
		            m_NrSurveysValue = obj.n;
	            } /*else if (m_ReadingWhich == 4)
	            {
		            m_YomaBalance = obj.n;
	            }*/
	            m_ReadingWhich = 0;
                break;
            case JSONObject.Type.BOOL:
                break;
            case JSONObject.Type.NULL:
                break;

        }
    }


	void updateProgressValues(int nrpicturessorted)
	{
		/*float levelbase = 100;
		float levelgrowth = 1.8f;

		int level = 0;
		int lastlevel = 0;
		int nextlevel = (int)levelbase;
		while (nrpicturessorted > levelbase) {
			levelbase *= levelgrowth;

			level++;
			lastlevel = (int)nextlevel;
			nextlevel = (int)levelbase;
		}

		level++;

		if (level == 1) {
			m_LevelTitle.GetComponent<UnityEngine.UI.Text> ().text = "You are on Level " + 1 + ".";
		} else {
			m_LevelTitle.GetComponent<UnityEngine.UI.Text> ().text = "You have reached Level " + level + "!";
		}
		int picturesToNext = nextlevel - nrpicturessorted;
		m_LevelText.GetComponent<UnityEngine.UI.Text> ().text = picturesToNext + " pictures until next level";

		// Adjust bar size
		float proc = ((float)nrpicturessorted - (float)lastlevel) / ((float)nextlevel - (float)lastlevel);
		RectTransform rt = m_LevelBar.GetComponent<RectTransform>();
		Vector2 size = rt.sizeDelta;
		size.x *= proc;
		rt.sizeDelta = size;
		Vector3 position = rt.position;
		position.x -= Screen.width * 0.43f * (1.0f - proc);
		rt.position = position;
		m_LevelBar.SetActive (true);*/
	}

	/*void loadProgress()
	{
		if (PlayerPrefs.HasKey ("PlayerPassword") == false || PlayerPrefs.HasKey ("PlayerMail") == false) {
			Debug.Log ("Did not login yet");
			return;
		}

        string url = LocalizationSupport.GetString("ServerUserURL") + "profile";
		//string url = "https://geo-wiki.org/Application/api/User/profile";
		string param = "";

		string email = PlayerPrefs.GetString ("PlayerMail");
		string password = PlayerPrefs.GetString ("PlayerPassword");
		string passwordmd5 = ComputeHash (password);
		int randnr = Random.Range(0, 10000000);
		//param += "{\"email\":" + "\"" + email + "\",\"md5password\":" + "\"" + passwordmd5 + "\"" + "\",\"randnr\":" + "\"" + passwordmd5 + "\"" + ",\"scope\":" + "\"total\",\"limit\":\"" + "0" + "\""+ ",\"platform\":{" + "\""+ "app"+ "\":" + "\"14\"" + "}";
		param += "{\"email\":" + "\"" + email + "\",\"md5password\":" + "\"" + passwordmd5 + "\""  + "}";



		Debug.Log ("loadProgress: " + param);


		WWWForm form = new WWWForm();
		form.AddField ("parameter", param);
		WWW www = new WWW(url, form);

		StartCoroutine(WaitForProgressData(www));
	}*/


	int m_ReadingProgressWhich = 0;
	string m_ReadingProgessValue = "";
	int m_NrScoresMade = 0;

	string m_ReadingFirstName;
	string m_ReadingLastName;
	string m_ReadingHometown;
	string m_ReadingPaypal;
	int m_GenderSelected;
	int m_AgeSelected;

	IEnumerator WaitForProgressData(WWW www)
	{
		yield return www;

		string[] options = { "OK" };
		if (www.error == null)
		{
			string data = www.text;
			//string[] parts = data.Split (":", 2);

			m_ReadingFirstName = "";
			 m_ReadingLastName = "";
			 m_ReadingHometown = "";
			m_ReadingPaypal = "";
			m_GenderSelected = -1;
			m_AgeSelected = -1;

			Debug.Log ("loadProgress result: " + data);

			JSONObject j = new JSONObject(www.text);
			m_ReadingProgressWhich = 0;
			m_ReadingProgessValue = "0";
			accessProgressData(j);

			Debug.Log ("Result first name: " + m_ReadingFirstName);
			Debug.Log ("Result last name: " + m_ReadingLastName);
			Debug.Log ("Result hometown: " + m_ReadingHometown);

			UnityEngine.UI.InputField textinput;
			textinput = m_InputFirstName.GetComponent<UnityEngine.UI.InputField>();
			textinput.text = m_ReadingFirstName;

			textinput = m_InputLastName.GetComponent<UnityEngine.UI.InputField>();
			textinput.text = m_ReadingLastName;

			textinput = m_InputHometown.GetComponent<UnityEngine.UI.InputField>();
			textinput.text = m_ReadingHometown;


			UnityEngine.UI.Text text;
		/*	text = m_HelloName.GetComponent<UnityEngine.UI.Text>();
			text.text = LocalizationSupport.GetString ("Hello") + " " + m_ReadingFirstName + ",";
*/


			UnityEngine.UI.Dropdown dropdown;
			dropdown = m_ComboGender.GetComponent<UnityEngine.UI.Dropdown>();
			m_ComboAge.GetComponentInChildren<UnityEngine.UI.Text> ().text = "";
			m_ComboGender.GetComponentInChildren<UnityEngine.UI.Text> ().text = "";

			if (Application.systemLanguage == SystemLanguage.German) {
				if (m_GenderSelected == 1) {
					m_ComboGender.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Männlich";
					dropdown.value = 2;
				} else if (m_GenderSelected == 2) {
					m_ComboGender.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Weiblich";
					dropdown.value = 1;
				}
			} else {
				if (m_GenderSelected == 1) {
					m_ComboGender.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Male";
					dropdown = m_ComboGender.GetComponent<UnityEngine.UI.Dropdown>();
					dropdown.value = 2;
				} else if (m_GenderSelected == 2) {
					m_ComboGender.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Female";
					dropdown = m_ComboGender.GetComponent<UnityEngine.UI.Dropdown>();
					dropdown.value = 1;
				}
			}

			dropdown = m_ComboAge.GetComponent<UnityEngine.UI.Dropdown>();
			if (m_AgeSelected == 1) {
				m_ComboAge.GetComponentInChildren<UnityEngine.UI.Text> ().text = "< 10";
				dropdown.value = 1;
			} else if (m_AgeSelected == 2) {
				m_ComboAge.GetComponentInChildren<UnityEngine.UI.Text> ().text = "10-14";
				dropdown.value = 2;
			} else if (m_AgeSelected == 3) {
				m_ComboAge.GetComponentInChildren<UnityEngine.UI.Text> ().text = "15-19";
				dropdown.value = 3;
			} else if (m_AgeSelected == 4) {
				m_ComboAge.GetComponentInChildren<UnityEngine.UI.Text> ().text = "20-29";
				dropdown.value = 4;
			} else if (m_AgeSelected == 5) {
				m_ComboAge.GetComponentInChildren<UnityEngine.UI.Text> ().text = "30-39";
				dropdown.value = 5;
			} else if (m_AgeSelected == 6) {
				m_ComboAge.GetComponentInChildren<UnityEngine.UI.Text> ().text = "40-49";
				dropdown.value = 6;
			} else if (m_AgeSelected == 7) {
				m_ComboAge.GetComponentInChildren<UnityEngine.UI.Text> ().text = "50-59";
				dropdown.value = 7;
			} else if (m_AgeSelected == 8) {
				m_ComboAge.GetComponentInChildren<UnityEngine.UI.Text> ().text = "60+";
				dropdown.value = 8;
			}
		} else {
			Debug.Log("WWW Error: "+ www.error);
			Debug.Log("WWW Error 2: "+ www.text);
//			checkInternet ();
		}   
	} 


	void accessProgressData(JSONObject obj){
		switch(obj.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				string key = (string)obj.keys[i];
				JSONObject j = (JSONObject)obj.list[i];
				if (key == "firstname") {
					m_ReadingProgressWhich = 7;
				} else if (key == "lastname") {
					m_ReadingProgressWhich = 8;
				} else if (key == "hometown") {
					m_ReadingProgressWhich = 9;
				}else if (key == "paypal") {
					m_ReadingProgressWhich = 11;
				} else if (key == "attributes") {
					m_ReadingProgressWhich = 10;
				}
				accessProgressData(j);
			}
			break;
		case JSONObject.Type.ARRAY:
			/*foreach(JSONObject j in obj.list){
				accessProgressData(j);
			}*/

			foreach(JSONObject j in obj.list){
				accessProgressData(j);
				//Debug.Log("Array number: " + j.n);
			}
			break;
		case JSONObject.Type.STRING:
			if (m_ReadingProgressWhich == 6) {
				m_ReadingProgessValue = obj.str;
			}
			if (m_ReadingProgressWhich == 7) {
				m_ReadingFirstName = obj.str;
			}
			if (m_ReadingProgressWhich == 8) {
				m_ReadingLastName = obj.str;
			}
			if (m_ReadingProgressWhich == 9) {
				m_ReadingHometown = obj.str;
			}
			if (m_ReadingProgressWhich == 11) {
				m_ReadingPaypal = obj.str;
			}
			if (m_ReadingProgressWhich == 10) {
				Debug.Log ("Strnumber: " + obj.str);
			}
		//	Debug.Log ("Str: " + obj.str);
			m_ReadingProgressWhich = -1;

			break;
		case JSONObject.Type.NUMBER:
			//m_ReadingProgressWhich = -1;
			if (m_ReadingProgressWhich == 10) {
				Debug.Log ("Number: " + obj.n);

				if (obj.n == 1) {
					m_GenderSelected = 1;
				} else if (obj.n == 2) {
					m_GenderSelected = 2;
				} else if (obj.n == 3) {
					m_AgeSelected = 1;
				} else if (obj.n == 4) {
					m_AgeSelected = 2;
				} else if (obj.n == 5) {
					m_AgeSelected = 3;
				} else if (obj.n == 6) {
					m_AgeSelected = 4;
				} else if (obj.n == 7) {
					m_AgeSelected = 5;
				} else if (obj.n == 8) {
					m_AgeSelected = 6;
				} else if (obj.n == 9) {
					m_AgeSelected = 7;
				} else if (obj.n == 10) {
					m_AgeSelected = 8;
				} else if (obj.n == 11) {
					m_Toggle1.GetComponent<Toggle> ().isOn = true;
				} else if (obj.n == 12) {
					m_Toggle2.GetComponent<Toggle> ().isOn = true;
				} else if (obj.n == 13) {
					m_Toggle3.GetComponent<Toggle> ().isOn = true;
				} else if (obj.n == 14) {
					m_Toggle4.GetComponent<Toggle> ().isOn = true;
				} else if (obj.n == 15) {
					m_Toggle5.GetComponent<Toggle> ().isOn = true;
				} else if (obj.n == 16) {
					m_Toggle6.GetComponent<Toggle> ().isOn = true;
				} else if (obj.n == 17) {
					m_Toggle7.GetComponent<Toggle> ().isOn = true;
				} else if (obj.n == 18) {
					m_Toggle8.GetComponent<Toggle> ().isOn = true;
				} else if (obj.n == 19) {
					m_Toggle9.GetComponent<Toggle> ().isOn = true;
				} 
			}
			break;
		case JSONObject.Type.BOOL:
			break;
		case JSONObject.Type.NULL:
			break;

		}
	}





	public void OnBackClicked()
	{
		/*int sorting = PlayerPrefs.GetInt("CurrentlySorting");
		if (sorting == 1)
		{
			string sortingtype = PlayerPrefs.GetString("CurPile_PileSortingType");
			if (sortingtype == "Binary")
			{
				Debug.Log("Open Binary Sorting Type");
				Application.LoadLevel("Sorting");
			}
			else if (sortingtype == "MultiClass")
			{
				Debug.Log("Open MultiClass Sorting Type");
				Application.LoadLevel("SortingMultiClass");
			}
			else if (sortingtype == "ContinuousScale")
			{
				Debug.Log("Open Scale Sorting Type");
				Application.LoadLevel("SortingScale");
			}
		}
		else
		{
			// Todo change this again
			//Application.LoadLevel("Piles");
			//Application.LoadLevel("Chat");
			SceneManager.LoadScene("Piles");
		}*/
		
		SceneManager.LoadScene("DemoMap");
	}


	void loadBackgroundImage()
	{
		int sorting = PlayerPrefs.GetInt ("CurrentlySorting");
		if (sorting == 0) {
			return;
		}


		int pileimg = PlayerPrefs.GetInt ("CurPileImage");
		if (pileimg == 0) {
			return;
		}
		/*
		string picid = "curpic";
		string name = Application.persistentDataPath+"/"+picid+".png";
		if (File.Exists (name)) {
			byte[] bytes = File.ReadAllBytes (name);
			if (bytes != null) {
				Texture2D texture = new Texture2D (1, 1);
				texture.LoadImage (bytes);

				Sprite sprite = Sprite.Create(texture, new Rect(0, 50, texture.width,512+50), new Vector2(0, 0));


				UnityEngine.UI.Image image = m_BackImage.GetComponent<UnityEngine.UI.Image> ();
				image.sprite = sprite;
				m_BackImage.GetComponent<UnityEngine.UI.Image> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

				image = m_ImageUploading.GetComponent<UnityEngine.UI.Image> ();
				image.sprite = sprite;
				m_ImageUploading.GetComponent<UnityEngine.UI.Image> ().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			}
		} else {
			Debug.Log ("Could not load image " + name);
		}*/
	}


	/*public void checkInternet()
	{
		if (Screen.width > Screen.height) {
			m_ImageCheckInternetPortrait.SetActive (false);
			m_ImageCheckInternetLandscape.SetActive (true);
		} else {
			m_ImageCheckInternetPortrait.SetActive (true);
			m_ImageCheckInternetLandscape.SetActive (false);
		}

		m_TextCheckInternet.SetActive (true);
		m_BtnBackCheckInternet.SetActive (true);
		m_ImageBackCheckInternet.SetActive (true);
		m_BtnCheckInternet.SetActive (true);
	}


	public void hideCheckInternet()
	{
		m_ImageCheckInternetPortrait.SetActive (false);
		m_ImageCheckInternetLandscape.SetActive (false);
		m_TextCheckInternet.SetActive (false);
		m_BtnBackCheckInternet.SetActive (false);
		m_ImageBackCheckInternet.SetActive (false);
		m_BtnCheckInternet.SetActive (false);
	}*/

    //-----------------------------
	// Show username
	//-----------------------------

	public void ToggleUsername(bool bShow)
	{
		Debug.Log("Toggle username: " + bShow);

		if (m_bInitToggles)
			return;


		if (PlayerPrefs.HasKey("PPPPassword") == false || PlayerPrefs.HasKey("PPPMail") == false)
		{
			return;
		}
		Debug.Log("Toggle username changed: " + bShow);

		if (bShow)
		{
			PlayerPrefs.SetInt("HideUsername", 0);
		} else
		{
			PlayerPrefs.SetInt("HideUsername", 1);
		}
		PlayerPrefs.Save();
		UploadShowUser();
	}


	void UploadShowUser()
    {
		string url = LocalizationSupport.GetString("DBUrl") + "profile/username/true";

		if(PlayerPrefs.GetInt("HideUsername") == 1)
        {
			url = LocalizationSupport.GetString("DBUrl") + "profile/username/false";
		}
		StartCoroutine(StartUploadingShowUser(url));
	}

	IEnumerator StartUploadingShowUser(string url)
	{
		string[] options3 = { LocalizationSupport.GetString("Login") };

		Debug.Log("Url: " + url);
		string param = "  ";

		using (UnityWebRequest www = UnityWebRequest.Put(url, "PUT"))//UnityWebRequest.Post(url, param))
		{
			www.SetRequestHeader("Content-Type", "application/json");
			www.certificateHandler = new AcceptAnyCertificate();
			string token = PlayerPrefs.GetString("PPPToken");
			www.SetRequestHeader("Authorization", "Bearer " + token);
			www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");


			//www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error: " + www.error);
			}
			else
			{
				string data = www.downloadHandler.text;
				//string[] parts = data.Split (":", 2);

				Debug.Log("Successfully changed show user: " + data);
			}
		}
	}


	void CopyLink(string text)
    {
		if(text == "Copy Link")
		{
			Debug.Log("Copy link");
			GUIUtility.systemCopyBuffer = "https://yoma.abc-research.at";
		}
    }

	public void ViewPayouts()
	{
           // Application.LoadLevel("Payout");
		//Application.OpenURL("https://yoma.abc-research.at");
				UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string>(CopyLink);
		string[] options = { "Copy Link", "OK", };
			messageBox.Show("", "Open \"https://yoma.abc-research.at\" in your Brave browser to manage your tokens.", ua, options);
			
	}

	public GameObject m_InputYomaAddress;

	void SaveYomaAddress()
	{
		if (m_InputYomaAddress.GetComponent<InputField>().text.Length <= 0)
		{
			string[] options = {"OK"};
			messageBox.Show("", "Specify your Brave wallet address first to which your tokens should be redeemed.", options);

				m_TextUploading.SetActive (false);
				m_ImageTextUploading.SetActive (false);
			return;
		}
		
		string address = m_InputYomaAddress.GetComponent<InputField>().text;


		if (m_YomaAddress != address)
		{
			string url = "https://server.org/user/crypto/address";

			string data = "{\"address\": \"" + address + "\"}";

			StartCoroutine(waitForSavingYomaAddress(url, data));
		}
		else
		{
			StartRequestingPayout();
		}
	}

	IEnumerator waitForSavingYomaAddress(string url, string param)
	{
		using (UnityWebRequest www = UnityWebRequest.Post(url, "POST")) //UnityWebRequest.Post(url, param))
		{
			byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(param);
			www.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
			www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
			www.SetRequestHeader("Content-Type", "application/json");
			string token = PlayerPrefs.GetString("PPPToken");
			www.SetRequestHeader("Authorization", "Bearer " + token);
			www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.certificateHandler = new AcceptAnyCertificate();

			//www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error: " + www.error);

				m_TextUploading.SetActive (false);
				m_ImageTextUploading.SetActive (false);
			}
			else
			{
				string data = www.downloadHandler.text;
				
				Debug.Log("Yoma address saved correctly: " + data);
				m_YomaAddress = m_InputYomaAddress.GetComponent<InputField>().text;
				StartRequestingPayout();
			}
		}
	}

	bool m_bRequestPayout = false;
	int m_RequestPayoutIter = 0;

	public void RequestPayout()
	{
		//m_TokenRedirect = 2;
		//refreshToken();
		m_bRequestPayout = true;
		m_RequestPayoutIter = 0;

		m_TextUploading.GetComponent<Text>().text = "Redeem tokens...";
		m_TextUploading.SetActive (true);
		m_ImageTextUploading.SetActive (true);
	}

	void LoadYomaTokenStatus()
	{
		string url = "https://server.org/user/tokens/payout/status";

		StartCoroutine(WaitForYomaTokenStatus(url));
	}
	
	IEnumerator WaitForYomaTokenStatus(string url)
	{
		using (UnityWebRequest www = UnityWebRequest.Get(url))
		{
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();
			string token = PlayerPrefs.GetString("PPPToken");
			//www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error loading yoma token status: " + www.error);
				string data = www.downloadHandler.text;
				Debug.Log("Data: " + data);
				Debug.Log("Errorstr: " + www.ToString());
			}
			else
			{
				string data = www.downloadHandler.text;
				Debug.Log("WWW yoma token status successfully loaded: " + data);

				if (data == "false")
				{
					string[] options = {"OK"};
					messageBox.Show("", "Tokens can be redeemed at the end of the challenge.", options);
				}
				else
				{
					/*if (m_Score <= 0.0f)
					{
						string[] options = {"OK"};
						messageBox.Show("", "You don't have tokens to redeem.", options);
						//return;
					}
					else
					{*/
						SaveYomaAddress();
					//}
				}
				
				/*
*/
			}
		}

		yield return null;
	}

	void StartRequestingPayout()
	{
		if (m_Score <= 0.0f)
		{
			string[] options = {"OK"};
			messageBox.Show("", "You don't have tokens to transfer.", options);

			m_TextUploading.SetActive (false);
			m_ImageTextUploading.SetActive (false);
			return;
		}
		string url = "https://server.org/user/payout"; 
		string param = "";
		param += "{" +
		         "\"tokensPaid\":" + m_Score;
		param += "}";
		Debug.Log("StartRequestingPayout: " + param);
		StartCoroutine(waitForPayout2(url, param));
	}
	
	IEnumerator waitForPayout2(string url, string param)
	{
		using (UnityWebRequest www = UnityWebRequest.Post(url, "POST")) //UnityWebRequest.Post(url, param))
		{
			byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(param);
			www.uploadHandler = (UploadHandler) new UploadHandlerRaw(jsonToSend);
			www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
			www.SetRequestHeader("Content-Type", "application/json");
			string token = PlayerPrefs.GetString("PPPToken");
			www.SetRequestHeader("Authorization", "Bearer " + token);
			www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.certificateHandler = new AcceptAnyCertificate();

			//www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error: " + www.error);
				
				string data = www.downloadHandler.text;
				string[] options = { "OK" };
				messageBox.Show("", "Redemption not successful. Please try again later. " + data, options);


				m_TextUploading.SetActive (false);
				m_ImageTextUploading.SetActive (false);
			}
			else
			{
				string data = www.downloadHandler.text;
				Debug.Log("payout successful: " + data);
			
				string[] options = { "OK" };
				messageBox.Show("", "Your redemption request has been successful. Open \"https://yoma.abc-research.at\" in your Brave browser to manage your tokens.", options);
			
				m_TotalScore = 0.0f;
				m_Score = 0.0f;
				
				m_GOTotalScore.GetComponent<UnityEngine.UI.Text>().text = "" + m_TotalScore;
				m_GOScore.GetComponent<UnityEngine.UI.Text>().text = "" + m_Score;


				m_TextUploading.SetActive (false);
				m_ImageTextUploading.SetActive (false);
				yield return www;
			}
		}
	}

	IEnumerator RequestPayout2(string url)
	{
		using (UnityWebRequest www = UnityWebRequest.Post(url, "POST"))//UnityWebRequest.Post(url, param))
		{
			//byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(param);
			//www.uploadHandler = (UploadHandler)new UploadHandlerRaw();
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.SetRequestHeader("Content-Type", "application/json");
			www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("PPPToken"));
			www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.certificateHandler = new AcceptAnyCertificate();

			//www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error loading payout: " + www.error);
				string data = www.downloadHandler.text;
				Debug.Log("Data: " + data);
				Debug.Log("Errorstr: " + www.ToString());
				
				/*
				m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text>().text = "Error loading token: " + data;
				m_bErrorLoadingToken = true;
				m_ErrorLoadingTokenIter = 0;*/
				//loadToken();
			}
			else
			{
				string data = www.downloadHandler.text;

				Debug.Log("Loading payout result: " + data);
			}
		}
	}
	
	
	IEnumerator waitForPayout(WWW www)
	{
		yield return www;

		if (www.error == null)
		{
			string data = www.text;
			Debug.Log("payout successful: " + data);
			
			string[] options = { "OK" };
			messageBox.Show("", "Your redemption request has been successful. Open \"https://yoma.abc-research.at\" in your Brave browser to manage your tokens.", options);
			
			m_TotalScore = 0.0f;
			m_Score = 0.0f;
				
			m_GOTotalScore.GetComponent<UnityEngine.UI.Text>().text = "" + m_TotalScore;
			m_GOScore.GetComponent<UnityEngine.UI.Text>().text = "" + m_Score;
			yield return www;
		}
		else
		{
			Debug.Log("UPLOAD ERROR");
			Debug.Log("WWW Error: " + www.error);
			Debug.Log("WWW Error 2: " + www.text);
			
			
			string[] options = { "OK" };
			//messageBox.Show("", "Failure with the payout request.", options);
			messageBox.Show("", www.text, options);
		}

		www.Dispose();
		Resources.UnloadUnusedAssets();
	}
	
	void LoadYomaAddress()
	{
		string url = "https://server.org/user/crypto/address";

		Debug.Log("LoadPilesInfo: " + url);
		StartCoroutine(WaitForYomaAddress(url));
	}

	private string m_YomaAddress = "";
	IEnumerator WaitForYomaAddress(string url)
	{
		using (UnityWebRequest www = UnityWebRequest.Get(url))
		{
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();
			string token = PlayerPrefs.GetString("PPPToken");
			//www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error loading yoma address: " + www.error);
				string data = www.downloadHandler.text;
				Debug.Log("Data: " + data);
				Debug.Log("Errorstr: " + www.ToString());
			}
			else
			{
				string data = www.downloadHandler.text;
				Debug.Log("WWW yoma address successfully loaded: " + data);
				
				
				JSONObject j = new JSONObject(data);
				m_ReadingWhich = -1;
				accessYomaAddress(j);

				m_InputYomaAddress.GetComponent<InputField>().text = m_YomaAddress;
			}
		}

		yield return null;
	}
	
	void accessYomaAddress(JSONObject obj)
    {
        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];
                    
                   if(key == "address")
                   {
	                   m_ReadingWhich = 1;
                   }
                   else
                   {
	                   m_ReadingWhich = -1;
                   }
                   accessYomaAddress(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                //  Debug.Log ("Array");
                foreach (JSONObject j in obj.list)
                {
	                accessYomaAddress(j);
                }
                break;
            case JSONObject.Type.STRING:
                if (m_ReadingWhich == 1)
                {
	                m_YomaAddress = obj.str;
                }
                break;
            case JSONObject.Type.NUMBER:
                break;
            case JSONObject.Type.BOOL:
                break;
            case JSONObject.Type.NULL:
                break;

        }
    }

} 




