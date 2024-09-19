using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class EventSystemLeaderboard : MonoBehaviour
{
	private string m_Token = "";


	public GameObject m_Button;
	public GameObject m_Dropdown;
	public GameObject m_TextInfo;

	public GameObject m_Loading;

	public GameObject m_Content;
	public GameObject m_Rank;
	public GameObject m_LName;
	public GameObject m_Score;
	public GameObject m_RankS;
	public GameObject m_NameS;
	public GameObject m_ScoreS;

	ArrayList m_AddedTexts;


	IEnumerator changeFramerate() {
		yield return new WaitForSeconds(1);
		Application.targetFrameRate = 30;
	}


	// Use this for initialization
	void Start () {
		StartCoroutine(changeFramerate());


		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();
		

		m_CurSelection = 0;
		m_AddedTexts = new ArrayList();
		UpdateText ();
		updateStates ();

		//loadLeaderboard ();
		refreshTokenLeaderboad();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel ("DemoMap");
	}

	int m_CurSelection = 0;
	void UpdateText() {
		/*if (Application.systemLanguage == SystemLanguage.German ) {
			if (m_CurSelection == 0) {
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Fotos insgesamt:\nQuests insgesamt:\nOrte besucht:";
			} else if (m_CurSelection == 1) {
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Fotos diese Woche:\nQuests diese Woche:\nOrte besucht:";
			} else if (m_CurSelection == 2) {
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Fotos letzte Woche:\nQuests letzte Woche:\nOrte besucht:";
			} 

			m_Loading.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Wird geladen...";
		} else {
			if (m_CurSelection == 0) {
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Total photos:\nTotal quests:\nLocations visited:";
			} else if (m_CurSelection == 1) {
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Photos this week:\nQuests this week:\nLocations visited:";
			} else if (m_CurSelection == 2) {
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Photos last week:\nQuests last week:\nLocations visited:";
			} 
			m_Loading.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Loading...";
		}*/

		/*m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text =  LocalizationSupport.GetString("LeaderboardTotalPhotos") +
			"\n" + LocalizationSupport.GetString("LeaderboardTotalQuests") + "\n" +  LocalizationSupport.GetString("LeaderboardTotalLocations");
		*/m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text =  LocalizationSupport.GetString("LeaderboardTotalQuests") + "\n" +  LocalizationSupport.GetString("LeaderboardTotalPhotos");

		m_Loading.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Loading");
	}

	int m_CurState = 0;
	public void updateStates() {
		
		if (Application.systemLanguage == SystemLanguage.German ) {
			m_Button.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnBack");//"ZURÜCK";

			m_Rank.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("LeaderboardRank");//"Rang";
			m_LName.GetComponentInChildren<UnityEngine.UI.Text>().text =  LocalizationSupport.GetString("LeaderboardName");//"Name";
			m_Score.GetComponentInChildren<UnityEngine.UI.Text>().text =  LocalizationSupport.GetString("LeaderboardNrQuests");//"Quests gemacht";

			m_Dropdown.GetComponentInChildren<UnityEngine.UI.Text>().text = "Insgesamt";

			UnityEngine.UI.Dropdown.OptionData list = new UnityEngine.UI.Dropdown.OptionData("Insgesamt");
			UnityEngine.UI.Dropdown.OptionData list2 = new UnityEngine.UI.Dropdown.OptionData("Diese Woche");
			UnityEngine.UI.Dropdown.OptionData list3 = new UnityEngine.UI.Dropdown.OptionData("Letzte Woche");

			UnityEngine.UI.Dropdown dropdown;
			dropdown = m_Dropdown.GetComponent<UnityEngine.UI.Dropdown>();
			dropdown.options.Clear ();
			dropdown.options.Add (list);
			dropdown.options.Add (list2);
			dropdown.options.Add (list3);
		} else {
			//m_Score.GetComponentInChildren<UnityEngine.UI.Text>().text = "Quests done";

			m_Rank.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("LeaderboardRank");//"Rang";
			m_LName.GetComponentInChildren<UnityEngine.UI.Text>().text =  LocalizationSupport.GetString("LeaderboardName");//"Name";
			m_Score.GetComponentInChildren<UnityEngine.UI.Text>().text =  LocalizationSupport.GetString("LeaderboardNrQuests");//"Quests gemacht";


			m_Button.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnBack");//"CLOSE";

			m_Dropdown.GetComponentInChildren<UnityEngine.UI.Text>().text = "Total";

			UnityEngine.UI.Dropdown.OptionData list = new UnityEngine.UI.Dropdown.OptionData("Total");
			UnityEngine.UI.Dropdown.OptionData list2 = new UnityEngine.UI.Dropdown.OptionData("This week");
			UnityEngine.UI.Dropdown.OptionData list3 = new UnityEngine.UI.Dropdown.OptionData("Last week");

			UnityEngine.UI.Dropdown dropdown;
			dropdown = m_Dropdown.GetComponent<UnityEngine.UI.Dropdown>();
			dropdown.options.Clear ();
			dropdown.options.Add (list);
			dropdown.options.Add (list2);
			dropdown.options.Add (list3);
		}

	}

	public void NextClicked () {
		Application.LoadLevel ("DemoMap");
	//	Debug.Log ("OnSelected: " );
	}
	
	void refreshTokenLeaderboad()
	{
		Debug.Log("refreshTokenLeaderboad");
		if (PlayerPrefs.HasKey("PlayerPassword") == false || PlayerPrefs.HasKey("PlayerMail") == false)
		{
			return;
		}
		string user = PlayerPrefs.GetString("PlayerMail");
		string password = PlayerPrefs.GetString("PlayerPassword");
		string url = "https://server.org/connect/token";

		StartCoroutine(LoginLeaderboard(url, user, password));
	}
	class AcceptAnyCertificate : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData) => true;
	}

	IEnumerator LoginLeaderboard(string url, string user, string password)
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
			}
			else
			{
				string data = www.downloadHandler.text;
				Debug.Log("Refresh token leaderboard result: " + data);

				m_bReadingAccessTokenLeaderboard = false;
				JSONObject j = new JSONObject(data);
				accessAccessTokenLeaderboard(j);

				//loadLeaderboardTable();
				loadLeaderboard();
			}
		}
	}
	
	
	bool m_bReadingAccessTokenLeaderboard = false;
	void accessAccessTokenLeaderboard(JSONObject obj)
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
						m_bReadingAccessTokenLeaderboard = true;
					}
					accessAccessTokenLeaderboard(j);
				}
				break;
			case JSONObject.Type.ARRAY:
				//	Debug.Log ("Array");
				foreach (JSONObject j in obj.list)
				{
					accessAccessTokenLeaderboard(j);
				}
				break;
			case JSONObject.Type.STRING:
				if (m_bReadingAccessTokenLeaderboard)
				{
					Debug.Log("Access token read: " + obj.str);
					m_Token = obj.str;
					m_bReadingAccessTokenLeaderboard = false;
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

	void loadLeaderboard()
	{
		string url = "https://server.org/leaderboard";
		string param = "";
		Debug.Log ("login param: " + param);

		StartCoroutine(WaitForData(url));
	}


	bool m_bQuestsRead;
	bool m_bPhotosRead;
	bool m_bPercRead;
	string m_NrQuests;
	string m_NrPhotos;
	string m_PercDone;
	ArrayList m_Names;
	ArrayList m_Scores;

	private int m_NrTotalSurveys = 0;
	private int m_NrTotalImages = 0;

	IEnumerator WaitForData(string url)
	{
		using (UnityWebRequest www = UnityWebRequest.Get(url))
		{
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();
			www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.SetRequestHeader("Authorization", "Bearer " + m_Token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error loading stats info info: " + www.error);
				string data = www.downloadHandler.text;
				Debug.Log("Data: " + data);
				Debug.Log("Errorstr: " + www.ToString());
				refreshTokenLeaderboad();
			}
			else
			{
				string data = www.downloadHandler.text;
				//string[] parts = data.Split (":", 2);

				Debug.Log("Leaderboard result: " + data);

				JSONObject j = new JSONObject(data);
				m_ReadingWhich = -1;

				m_bQuestsRead = false;
				 m_bPhotosRead = false;
				 m_bPercRead = false;
				 
				 
				 m_Names = new ArrayList();
				 m_Scores = new ArrayList();
				 m_NrTotalSurveys = 0;
				 m_NrTotalImages = 0;

				accessPinData(j);
/*
				if (m_PercDone.Length > 0) {
					float percdone = float.Parse (m_PercDone);
					m_PercDone = percdone.ToString ("F1");
				}
*/
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text =  LocalizationSupport.GetString("LeaderboardTotalQuests") + " " + m_NrTotalSurveys +  "\n" +  LocalizationSupport.GetString("LeaderboardTotalPhotos") + " " + m_NrTotalImages;

				m_Loading.SetActive (false);
				createLeaderboard ();
			}
		}   
	} 

	int m_ReadingWhich;

	void accessPinData(JSONObject obj){
		switch(obj.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				string key = (string)obj.keys[i];
				JSONObject j = (JSONObject)obj.list[i];
					Debug.Log("key: " + key);
				/*if (key == "quests") {
					if (m_bQuestsRead == false) {
						m_ReadingWhich = 1;
					}
					m_bQuestsRead = true;
				}if (key == "photos") {
					if (m_bPhotosRead == false) {
						m_ReadingWhich = 2;
					}
					m_bPhotosRead = true;

				}if (key == "percentageTotal") {
					if (m_bPercRead == false) {
						m_ReadingWhich = 3;
					}
					m_bPercRead = true;
				}
				if (key == "highscore") {
					m_ReadingWhich = 4;

				}*/

				if (key == "userName") {
					m_ReadingWhich = 5;
				}
				else if (key == "score") {//"surveyCount") {
					m_ReadingWhich = 6;
				}
				else if (key == "totalImagesUploadedCount") {
					m_ReadingWhich = 7;
				}
				else
				{
					m_ReadingWhich = -1;
				}
				accessPinData(j);
			}
			break;
		case JSONObject.Type.ARRAY:
			//	Debug.Log ("Array");
			foreach(JSONObject j in obj.list){
				accessPinData(j);
			}
			break;
		case JSONObject.Type.STRING:
			Debug.Log ("string: " + obj.str);
			if (m_ReadingWhich == 1) {
				m_NrQuests = obj.str;
			}
			if (m_ReadingWhich == 2) {
				m_NrPhotos = obj.str;
			}
			if (m_ReadingWhich == 5) {
				m_Names.Add (obj.str);
			}
			if (m_ReadingWhich == 6) {
				m_Scores.Add (obj.str);
			}
			m_ReadingWhich = -1;
			break;
		case JSONObject.Type.NUMBER:
			Debug.Log ("number: " + obj.n);

			if (m_ReadingWhich == 3) {
				m_PercDone = obj.n + "";
			}
			else if (m_ReadingWhich == 6) {
				m_NrTotalSurveys += (int)obj.n;//
				m_Scores.Add (obj.n + "");
			}
			else if (m_ReadingWhich == 7)
			{
				m_NrTotalImages += (int)obj.n;//m_Scores.Add (obj.n + "");
			}
			m_ReadingWhich = -1;
			break;
		case JSONObject.Type.BOOL:
			//		Debug.Log("bool: " + obj.b);
			break;
		case JSONObject.Type.NULL:
			//	Debug.Log("NULL");
			break;

		}
	}

	public void OnSelectedRange( int value) {

		for (int i = 0; i < m_AddedTexts.Count; i++) {
			GameObject go = (GameObject)m_AddedTexts [i];
			Destroy (go);
		}
		m_AddedTexts = new ArrayList();

		UnityEngine.UI.Dropdown dropdown;
		dropdown = m_Dropdown.GetComponent<UnityEngine.UI.Dropdown>();

		Debug.Log ("OnSelected: " + value + "," + dropdown.value);

		m_CurSelection = dropdown.value;
		UpdateText ();

		loadLeaderboard ();

		/*
		int nrentries = 5;

		RectTransform rectTransform2 = m_Content.GetComponent<RectTransform> ();
		//rectTransform2.sizeDelta.
		//rt.sizeDelta = new Vector2 (100, 100);
		float scalex = rectTransform2.sizeDelta.x;
		float scaley = rectTransform2.sizeDelta.y;
		rectTransform2.sizeDelta = new Vector2 (scalex, 30.0f * nrentries);



		for (int i = 0; i < nrentries; i++) {
			GameObject copy = (GameObject)GameObject.Instantiate (m_RankS);

			copy.transform.SetParent (m_Content.transform, false);
			copy.SetActive (true);
			RectTransform rectTransform = copy.GetComponent<RectTransform> ();
			float curpos = rectTransform.localPosition.y;
			float curposx = rectTransform.localPosition.x;
			curpos -= i * 30.0f;
			rectTransform.localPosition = new Vector2 (curposx, curpos);
			m_AddedTexts.Add (copy);
			int currank = i + 1;
			string text = "" + currank;
			copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = text;


			copy = (GameObject)GameObject.Instantiate (m_NameS);
			copy.transform.SetParent (m_Content.transform, false);
			copy.SetActive (true);
			 rectTransform = copy.GetComponent<RectTransform> ();
			 curpos = rectTransform.localPosition.y;
			 curposx = rectTransform.localPosition.x;
			curpos -= i * 30.0f;
			rectTransform.localPosition = new Vector2 (curposx, curpos);
			m_AddedTexts.Add (copy);
			 text = "Tobias";
			copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = text;

			copy = (GameObject)GameObject.Instantiate (m_ScoreS);
			copy.transform.SetParent (m_Content.transform, false);
			copy.SetActive (true);
			rectTransform = copy.GetComponent<RectTransform> ();
			curpos = rectTransform.localPosition.y;
			curposx = rectTransform.localPosition.x;
			curpos -= i * 30.0f;
			rectTransform.localPosition = new Vector2 (curposx, curpos);
			m_AddedTexts.Add (copy);
			copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = "2323";

		}*/
	}

	public float m_SizeEntry = 40.0f;
	public void createLeaderboard()
	{
		int nrentries = m_Names.Count;

		float sizeentry = m_SizeEntry;//40.0f;
		RectTransform rectTransform2 = m_Content.GetComponent<RectTransform> ();
		//rectTransform2.sizeDelta.
		//rt.sizeDelta = new Vector2 (100, 100);
		float scalex = rectTransform2.sizeDelta.x;
		float scaley = rectTransform2.sizeDelta.y;
		rectTransform2.sizeDelta = new Vector2 (scalex, sizeentry * nrentries + 100.0f);

	Debug.Log("nrentries: " + nrentries + " m_Scores nr entries: " + m_Scores.Count);

		for (int i = 0; i < nrentries; i++) {
			GameObject copy = (GameObject)GameObject.Instantiate (m_RankS);

			copy.transform.SetParent (m_Content.transform, false);
			copy.SetActive (true);
			RectTransform rectTransform = copy.GetComponent<RectTransform> ();
			float curpos = rectTransform.localPosition.y;
			float curposx = rectTransform.localPosition.x;
			curpos -= i * sizeentry;
			rectTransform.localPosition = new Vector2 (curposx, curpos);
			m_AddedTexts.Add (copy);
			int currank = i + 1;
			string text = "" + currank;
			copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = text;


			copy = (GameObject)GameObject.Instantiate (m_NameS);
			copy.transform.SetParent (m_Content.transform, false);
			copy.SetActive (true);
			rectTransform = copy.GetComponent<RectTransform> ();
			curpos = rectTransform.localPosition.y;
			curposx = rectTransform.localPosition.x;
			curpos -= i * sizeentry;
			rectTransform.localPosition = new Vector2 (curposx, curpos);
			m_AddedTexts.Add (copy);
			//text = "Tobias";
			text = (string)m_Names [i];
			text = System.Text.RegularExpressions.Regex.Unescape (text);
			if (text.Length > 15) {
				text = text.Substring (0, 15);
			}
		//hallihallo
			copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = text;

			copy = (GameObject)GameObject.Instantiate (m_ScoreS);
			copy.transform.SetParent (m_Content.transform, false);
			copy.SetActive (true);
			rectTransform = copy.GetComponent<RectTransform> ();
			curpos = rectTransform.localPosition.y;
			curposx = rectTransform.localPosition.x;
			curpos -= i * sizeentry;
			rectTransform.localPosition = new Vector2 (curposx, curpos);
			m_AddedTexts.Add (copy);
			string textscore = (string)m_Scores [i];

			/*if(textscore != "" && textscore != null) {
				float money = float.Parse (textscore);
				money /= 100;
				string strtotal = money.ToString ();//(( ("F2");
				textscore = strtotal;
			}*/

				copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = textscore;// + "€";//"2323";

		}
	}

}
