using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using Unitycoding.UIWidgets;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.Text;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.EventSystems;

public class EventSystemQuests : MonoBehaviour
{

	public TMP_Text m_TMPDebug;

	public GameObject m_Button;
	public GameObject m_TextInfo;
	public GameObject m_Info;

	public GameObject m_TextNoQuests;

	public GameObject m_Content;
	public GameObject m_Rank;
	public GameObject m_LName;
	public GameObject m_Score;
	public GameObject m_RankS;
	public GameObject m_NameS;
	public GameObject m_Desc;
	public GameObject m_ScoreS;
	public GameObject m_Image;
	public GameObject m_UploadQuest;
	public GameObject m_ShowQuest;
	public GameObject m_ShowQuestPhotos;


	public GameObject m_UploadingBack;
	public GameObject m_UploadingText;
	public GameObject m_UploadingTextWait;
	public GameObject m_UploadingImage;

	public GameObject m_DebugText;
	public GameObject m_DebugInputField;

	public GameObject m_UploadingAll;


	public GameObject m_ImageQuest;
	public GameObject m_SkyQuestBack;
	public GameObject m_QuestTitle;
	public GameObject m_QuestText;
	public GameObject m_BtnDeleteQuest;
	public GameObject m_BtnCloseQuest;
	public GameObject m_Image1Quest;
	public GameObject m_Image2Quest;
	public GameObject m_Image3Quest;
	public GameObject m_Image4Quest;
	public GameObject m_Image5Quest;

	public GameObject m_BtnEditImage1;
	public GameObject m_BtnEditImage2;
	public GameObject m_BtnEditImage3;
	public GameObject m_BtnEditImage4;
	public GameObject m_BtnEditImage5;

	public GameObject m_TextQuestExplanation;


	public MessageBox messageBox;
	public MessageBox messageBoxSurvey;
	//private MessageBox verticalMessageBox;

	//public Sprite m_Sprite;

	ArrayList m_AddedTexts;

	int m_NrQuestsDone;
//	int m_NrPoints;
//	int m_LastLevel;

	bool m_bShowQuest;

	//---------------------
	// Edit image

	public GameObject m_EditImageBack;
	public GameObject m_EditImage;
	public GameObject m_EditImageClose;
	public GameObject m_EditImageClose2;
	public GameObject m_EditImageExpl;
	public GameObject m_EditImageExplBack;

	string m_StrImg1;
	bool m_bImg1Set;
	string m_StrImg2;
	bool m_bImg2Set;
	string m_StrImg3;
	bool m_bImg3Set;
	string m_StrImg4;
	bool m_bImg4Set;
	string m_StrImg5;
	bool m_bImg5Set;


	IEnumerator changeFramerate() {
		yield return new WaitForSeconds(1);
		Application.targetFrameRate = 30;
	}

	
	public EventSystem m_EventSystem;
	
	// Use this for initialization
	void Start () {
		Debug.Log ("OpenQuests");
		StartCoroutine(changeFramerate());

		
		//--------------------
		// Commment this out again
		/*m_bStartSurvey = true;
		m_StartSurveyIter = 0;*/
		//--------------------
		m_EventSystem.pixelDragThreshold = 30;//

		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();

		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;

		m_bShowMessageBluring = true;
		m_ShowMessageBluringIter = 0;


		if (Application.systemLanguage == SystemLanguage.German && false) {
			m_EditImageExpl.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Drücke auf das was du verwischen möchtest.";
			m_TextQuestExplanation.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Bitte verwische in den Bildern alles was eine Person oder Eigentum identifizieren könnte!";
			m_TextNoQuests.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Du hast noch keine Quests gemacht.";
		} else {
			m_EditImageExpl.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("BlurHelp");//"Touch the parts you want to blur.";
			m_TextQuestExplanation.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("BlurHint");//"Please blur out all parts in the pictures that could identify a person or property!";
			m_TextNoQuests.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("NoQuestsDone");//"You have not made any quests yet.";
		}

		m_bHideEditImageExplanation = false;
		m_bHiddenEditImageExplanation = false;

//		m_NrPoints = -1;
//		m_LastLevel = -1;
		
		m_CurSelection = 0;
		m_AddedTexts = new ArrayList();

		m_UploadingBack.SetActive (false);
		m_UploadingText.SetActive (false);
	 	m_UploadingTextWait.SetActive (false);
		m_UploadingImage.SetActive (false);

		m_BtnEditImage1.SetActive (false);
		m_BtnEditImage2.SetActive (false);
		m_BtnEditImage3.SetActive (false);
		m_BtnEditImage4.SetActive (false);
		m_BtnEditImage5.SetActive (false);
		m_TextQuestExplanation.SetActive (false);

		m_bShowQuest = false;
		m_BtnCloseQuest.SetActive (false);
		m_BtnDeleteQuest.SetActive (false);
		m_ImageQuest.SetActive (false);
		m_SkyQuestBack.SetActive (false);
		m_QuestTitle.SetActive (false);
		m_QuestText.SetActive (false);
		m_Image1Quest.SetActive (false);
		m_Image2Quest.SetActive (false);
		m_Image3Quest.SetActive (false);
		m_Image4Quest.SetActive (false);
		m_Image5Quest.SetActive (false);

		m_EditImageBack.SetActive (false);
		m_EditImage.SetActive (false);
		m_EditImageClose.SetActive (false);
		m_EditImageClose2.SetActive (false);
		m_EditImageExpl.SetActive (false);
		m_EditImageExplBack.SetActive (false);

	//	PlayerPrefs.DeleteAll ();

		if (PlayerPrefs.HasKey ("NrQuestsDone")) {
			m_NrQuestsDone = PlayerPrefs.GetInt ("NrQuestsDone");
		} else {
			m_NrQuestsDone = 0;
		}

		if (m_NrQuestsDone > 0) {
			m_TextNoQuests.SetActive (false);
		}

		bool bOneQuestNotUploaded = false;
		for (int i = 0; i < m_NrQuestsDone && !bOneQuestNotUploaded; i++) {
			string stralreadyuploaded = "Quest_" + i + "_Uploaded";
			if (PlayerPrefs.HasKey (stralreadyuploaded) == false) {
				bOneQuestNotUploaded = true;
			}
		}
		if (bOneQuestNotUploaded) {
			m_UploadingAll.SetActive (true);
		} else {
			m_UploadingAll.SetActive (false);
		}

		m_bOneQuestNotUploaded = bOneQuestNotUploaded;
		if (PlayerPrefs.GetInt("QuestJustCompleted") == 0)
		{
			m_bOneQuestNotUploaded = false;
		}
		else
		{
			PlayerPrefs.SetInt("QuestJustCompleted", 0);
			PlayerPrefs.Save();
		}

		//messageBox = UIUtility.Find<MessageBox> ("MessageBox");



		PlayerPrefs.SetInt ("LoginReturnToQuests", 0);
		PlayerPrefs.Save ();

		UpdateText ();
		updateStates ();

			m_bCreateQuestsList = true;
	}

	void LoadQuestStatus(int index, string questid, GameObject go)
	{
		loadReviewsToken(index, questid, go);
	}
	void loadReviewsToken(int index, string questid, GameObject go)
	{
		if (!PlayerPrefs.HasKey("PlayerPassword") ||
			!PlayerPrefs.HasKey("PlayerName"))
		{
			return;
		}

		string url = "https://server.org/connect/token";

		StartCoroutine(loadingTokenReview(url, PlayerPrefs.GetString("PlayerName"), PlayerPrefs.GetString("PlayerPassword"),
			index, questid, go));
	}

	class AcceptAnyCertificate : CertificateHandler {
		protected override bool ValidateCertificate(byte[] certificateData) => true;
	}
	
	IEnumerator loadingTokenReview(string url, string user, string password, int index, string questid, GameObject go)
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
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error loading token: " + www.error);
				string data = www.downloadHandler.text;
				Debug.Log("Data: " + data);
				Debug.Log("Errorstr: " + www.ToString());
			}
			else
			{
				string data = www.downloadHandler.text;

				Debug.Log("Loading token: " + data);
				JSONObject j = new JSONObject(data);
				m_ReadingWhichToken = -1;
				m_Token = "";
				accessToken(j);
				Debug.Log("Loaded token: " + m_Token);
				loadReviews(index, questid, go);
			}
		}
	}
	
	void loadReviews(int index, string questid, GameObject go)
	{
		string url = "https://server.org/survey/" + questid + "/review";

		Debug.Log("Load review: " + url + " index: " + index);
		StartCoroutine(WaitForReviews(url, index, questid, go));
	}
	
	void accessStatus(JSONObject obj, bool bFound, out string valueFound)
	{
		valueFound = "";
		switch (obj.type)
		{
			case JSONObject.Type.OBJECT:
				for (int i = 0; i < obj.list.Count; i++)
				{
					string key = (string)obj.keys[i];
					JSONObject j = (JSONObject)obj.list[i];

					if (key == "status")
					{
						accessStatus(j, true, out valueFound);
					}
					/*else
					{
						accessStatus(j, false, out valueFound);
					}*/
				}
				break;
			case JSONObject.Type.ARRAY:
				//  Debug.Log ("Array");
				/*foreach (JSONObject j in obj.list)
				{
					accessStatus(j, false, out valueFound);
				}*/
				break;
			case JSONObject.Type.STRING:
				if (bFound) valueFound = obj.str;
				break;
			case JSONObject.Type.NUMBER:
				break;
			case JSONObject.Type.BOOL:
				break;
			case JSONObject.Type.NULL:
				//  Debug.Log("NULL");
				break;
		}
	}
	
	void accessScore(JSONObject obj, bool bFound, out float valueFound)
	{
		valueFound = 0.0f;//"";
		switch (obj.type)
		{
			case JSONObject.Type.OBJECT:
				for (int i = 0; i < obj.list.Count; i++)
				{
					string key = (string)obj.keys[i];
					JSONObject j = (JSONObject)obj.list[i];

					if (key == "score")
					{
						accessScore(j, true, out valueFound);
					}
					/*else
					{
						accessScore(j, false, out valueFound);
					}*/
				}
				break;
			case JSONObject.Type.ARRAY:
				//  Debug.Log ("Array");
				/*foreach (JSONObject j in obj.list)
				{
					accessScore(j, false, out valueFound);
				}*/
				break;
			case JSONObject.Type.STRING:
				//if (bFound) valueFound = obj.str;
				break;
			case JSONObject.Type.NUMBER:
				if (bFound) valueFound = obj.n;
				break;
			case JSONObject.Type.BOOL:
				break;
			case JSONObject.Type.NULL:
				//  Debug.Log("NULL");
				break;
		}
	}
	
	void accessComment(JSONObject obj, bool bFound, out string valueFound)
	{
		valueFound = "";
		switch (obj.type)
		{
			case JSONObject.Type.OBJECT:
				for (int i = 0; i < obj.list.Count; i++)
				{
					string key = (string)obj.keys[i];
					JSONObject j = (JSONObject)obj.list[i];

					if (key == "comment")
					{
						accessComment(j, true, out valueFound);
					}
					/*else
					{
						accessScore(j, false, out valueFound);
					}*/
				}
				break;
			case JSONObject.Type.ARRAY:
				//  Debug.Log ("Array");
				/*foreach (JSONObject j in obj.list)
				{
					accessScore(j, false, out valueFound);
				}*/
				break;
			case JSONObject.Type.STRING:
				if (bFound) valueFound = obj.str;
				break;
			case JSONObject.Type.NUMBER:
				break;
			case JSONObject.Type.BOOL:
				break;
			case JSONObject.Type.NULL:
				//  Debug.Log("NULL");
				break;
		}
	}
	
	IEnumerator WaitForReviews(string url, int index, string questid, GameObject go)
	{
		using (UnityWebRequest www = UnityWebRequest.Get(url))
		{
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();
			www.SetRequestHeader("Authorization", "Bearer " + m_Token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				string earnings = www.downloadHandler.text;
				
				/*
				index = m_UploadQuestUploaded.Count - index - 1;
				
				
				Debug.Log("review could not be loaded: " + earnings + " index: " + index);
				if (index >= 0 && index < m_UploadQuestUploaded.Count)
				{
					GameObject obj = (GameObject) m_UploadQuestUploaded[index];
					obj.GetComponent<Text>().text = "Error loading status";
					
					Debug.Log("Error text set: " + index);
				}
				else
				{
					Debug.Log("Error text not set: " + index);
				}*/
				
				go.GetComponent<Text>().text = "Error loading status";
			}
			else
			{
				string data = www.downloadHandler.text;
				Debug.Log("WWW review successfully loaded: " + data);
                
				JSONObject j = new JSONObject(data);

				string resultStatus = "";
				accessStatus(j, false, out resultStatus);

				float score = 0.0f;//"";
				accessScore(j, false, out score);

				string comment = "";//0.0f;//"";
				accessComment(j, false, out comment);
				
				Debug.Log("Status: " + resultStatus + " score: " + score + " comment: " + comment);

				/*index = m_UploadQuestUploaded.Count - index - 1;
				if (index >= 0 && index < m_UploadQuestUploaded.Count)
				{
					GameObject obj = (GameObject) m_UploadQuestUploaded[index];*/
					if (resultStatus.Equals("NotReviewed"))
					{
						go.GetComponent<Text>().text = LocalizationSupport.GetString("QuestUploadedNotReviewed");
					}
					else
					{
						go.GetComponent<Text>().text = LocalizationSupport.GetString("Status") + " " + resultStatus + "\n" +
						                                LocalizationSupport.GetString("Score") + " " + score +
						                                "\n" + LocalizationSupport.GetString("Comment") + " " + comment;
					}
					
				//}
			}
		}

		yield return null;
	}

	bool m_bCreateQuestsList = false;
	int m_IterCreateQuestsList = 0;

	float m_TouchPosX;
	float m_TouchPosY;

	bool m_bHideEditImageExplanation;
	bool m_bHiddenEditImageExplanation;
	float m_HideEditImageExplanationTime;

	bool m_bShowMessageBluring;
	int m_ShowMessageBluringIter;

	bool m_bTest = false;
	int m_TestIter = 0;

	bool m_bErrorLoadingToken = false;
	int m_ErrorLoadingTokenIter = 0;


	private bool m_bOneQuestNotUploaded = false;

	public int m_IterHelp = 0;
	//int m_DebugIter = 0;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel ("DemoMap");

		if (m_bOneQuestNotUploaded)
		{
			m_IterHelp++;
			if (m_IterHelp > 5)
			{
				m_bOneQuestNotUploaded = false;
				string[] options2 = { LocalizationSupport.GetString("Ok") };
				//messageBox.Show ("", LocalizationSupport.GetString ("StartSurveyQuestion"), ua, options2);
				messageBoxSurvey.Show ("", LocalizationSupport.GetString ("UploadQuestHelp"), options2);
			}
		} else if (m_bStartSurvey)
		{
			m_StartSurveyIter++;
			if (m_StartSurveyIter > 20)
			{
				UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnMsgBoxStartSurveyClicked);
				string[] options2 = { LocalizationSupport.GetString("No"), LocalizationSupport.GetString("Later"), LocalizationSupport.GetString("Sure") };
				//messageBox.Show ("", LocalizationSupport.GetString ("StartSurveyQuestion"), ua, options2);
				messageBoxSurvey.Show ("", LocalizationSupport.GetString ("StartSurveyQuestion"), ua, options2);

				m_bStartSurvey = false;
			}
		}

		if (m_bErrorLoadingToken)
		{
			m_ErrorLoadingTokenIter++;
			if (m_ErrorLoadingTokenIter > 200)
			{
				m_bErrorLoadingToken = false;
				loadToken();
			}
		}
		/*m_DebugIter++;
		if (m_DebugIter == 20) {
			Application.LoadLevel ("Quests");// Test if creating memory leak
		}*/
		/*
		if (!m_bTest) {
			m_TestIter++;
			if (m_TestIter > 10) {
				UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnMsgBoxAllUploadedClicked);
				string[] options = { "Ok"};
				messageBox.Show ("", LocalizationSupport.GetString("QuestUploadSuccessfulPrize"), ua, options);
				m_bTest = true;
			}
		}*/

		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;

		/*if (m_bShowMessageBluring) {
			m_ShowMessageBluringIter++;
			if (m_ShowMessageBluringIter > 3) {
				m_bShowMessageBluring = false;
				if (Application.systemLanguage == SystemLanguage.German && false ) {
					string[] options2 = { "OK" };
					messageBox.Show ("", "Bevor du hochladest, editiere bitte die Bilder und verwische alles was eine Person oder Eigentum identifizieren könnte. Vielen Dank!", options2);
				} else {
					string[] options2 = { "OK" };
					messageBox.Show ("", LocalizationSupport.GetString("PleaseBlur"), options2);			
				}
			}
		}*/

		if (m_bCreateQuestsList) {
			m_IterCreateQuestsList++;
			if (m_IterCreateQuestsList > 3) {
				m_bCreateQuestsList = false;
				createQuestList ();
				Debug.Log ("start loading leaderboard");
				loadLeaderboard ();
			}
		}

		if (m_bLoadImagesForQuest && m_LoadImagesForQuestIter > 3) {
			m_bLoadImagesForQuest = false;
			StartCoroutine (loadQuestImages (m_LoadImagesForQuestInReach, m_LoadImagesForQuestId));
		} else if(m_bLoadImagesForQuest) {
			m_LoadImagesForQuestIter++;
		}


		if (m_bBluring) {
			blurImage (m_BluringPosX, m_BluringPosY);
		}
		/*
		if (m_bHideEditImageExplanation) {
			m_HideEditImageExplanationTime += Time.deltaTime * 1000.0f;

			float proc = (m_HideEditImageExplanationTime) / 500.0f;
			Debug.Log ("m_HideEditImageExplanationTime: " + m_HideEditImageExplanationTime);
			if (proc > 1.0f) {
				m_bHideEditImageExplanation = false;
				m_EditImageExplBack.SetActive (false);
				m_EditImageExpl.SetActive (false);
			} else {
				if (proc > 1.0f)
					proc = 1.0f;
				proc = 1.0f - proc;

				byte alpha = (byte)(208 * proc);
				m_EditImageExplBack.GetComponent<UnityEngine.UI.Image> ().color = new Color32 (255, 255, 255, alpha);
				alpha = (byte)(255 * proc);
				m_EditImageExpl.GetComponent<UnityEngine.UI.Text> ().color = new Color32 (0, 0, 0, alpha);


				m_EditImageExplBack.SetActive (true);
				m_EditImageExpl.SetActive (true);
			}
		}*/

	}

	int m_CurSelection = 0;
	void UpdateText() {
		/*if (Application.systemLanguage == SystemLanguage.German && false ) {
			

			//m_UploadingAll.GetComponentInChildren<UnityEngine.UI.Text>().text = "Alle hochladen";
			m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Quests hochgeladen: ";
		} else {
		//	m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "This week: " + "\nLast week: \nTotal: ";
			m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("QuestsUploaded") + " ";//"Quests uploaded: ";
		}
*/
		m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("QuestsUploaded") + " ";//"Quests uploaded: ";

		m_UploadingAll.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("UploadAll");//"Alle hochladen";

		/*if (m_CurSelection == 0) {
			m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text>().text = "Total Photos:\nTotal Quests:\nLocations visited:";
		} else if (m_CurSelection == 1) {
			m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text>().text = "Photos this week:\nQuests this week:\nLocations visited:";
		} else if (m_CurSelection == 2) {
			m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text>().text = "Photos last week:\nQuests last week:\nLocations visited:";
		} */
	}

	int m_CurState = 0;
	public void updateStates() {
		
		if (Application.systemLanguage == SystemLanguage.German && false) {
			m_Button.GetComponentInChildren<UnityEngine.UI.Text>().text = "ZURÜCK";

			UnityEngine.UI.Dropdown.OptionData list = new UnityEngine.UI.Dropdown.OptionData("Insgesamt");
			UnityEngine.UI.Dropdown.OptionData list2 = new UnityEngine.UI.Dropdown.OptionData("Diese Woche");
			UnityEngine.UI.Dropdown.OptionData list3 = new UnityEngine.UI.Dropdown.OptionData("Letzte Woche");
			m_BtnCloseQuest.GetComponentInChildren<UnityEngine.UI.Text>().text = "ZURÜCK";
			m_BtnDeleteQuest.GetComponentInChildren<UnityEngine.UI.Text>().text = "LÖSCHE QUEST";

		} else {
			int btncontinue = 0;
			if(PlayerPrefs.HasKey("QuestsBtnContinue")) {
				btncontinue = PlayerPrefs.GetInt("QuestsBtnContinue");
				PlayerPrefs.SetInt("QuestsBtnContinue", 0);
				PlayerPrefs.Save();
			}

			if(btncontinue == 1) {
				m_Button.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnContinue");//"CLOSE";
			} else {
				m_Button.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnBack");//"CLOSE";
			}

			m_BtnCloseQuest.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Close");//"CLOSE";
			m_BtnDeleteQuest.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("DeleteQuest");//"DELETE QUEST";
			UnityEngine.UI.Dropdown.OptionData list = new UnityEngine.UI.Dropdown.OptionData("Total");
			UnityEngine.UI.Dropdown.OptionData list2 = new UnityEngine.UI.Dropdown.OptionData("This week");
			UnityEngine.UI.Dropdown.OptionData list3 = new UnityEngine.UI.Dropdown.OptionData("Last week");

		}

	}

	public void NextClicked () {
		Application.LoadLevel ("DemoMap");
	//	Debug.Log ("OnSelected: " );
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

	int m_WhichLeaderboard = 0;

	void stardLoading() {
		m_WhichLeaderboard = 0;
		m_NrQuests = "";
		m_NrPhotos = "";
		m_PercDone = "";
		loadLeaderboard ();
	}

	void loadLeaderboard()
	{
		return;
		Debug.Log ("loadLeaderboard");
		if (PlayerPrefs.HasKey ("PlayerPassword") == false || PlayerPrefs.HasKey ("PlayerMail") == false) {
			Debug.Log ("Points: Did not login yet");
			if (Application.systemLanguage == SystemLanguage.German && false) {
				//m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Diese Woche:" + "\nLetzte Woche:\nInsgesamt:";
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Quests hochgeladen: 0";
			} else {
				//m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "This week:" + "\nLast week:\nTotal:";
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("QuestsUploaded") + " 0";//Quests uploaded: 0";
			}
			return;
		}


		string url = "https://geo-wiki.org/Application/api/Campaign/FotoQuestStats";
		string param = "";

		string email = PlayerPrefs.GetString ("PlayerMail");
		string password = PlayerPrefs.GetString ("PlayerPassword");
		string passwordmd5 = ComputeHash (password);



	//	if (m_WhichLeaderboard == 0) {
			param += "{\"email\":" + "\"" + email + "\",\"md5password\":" + "\"" + passwordmd5 + "\"" + ",\"scope\":" + "\"total\",\"limit\":\"" + "0" + "\""+ ",\"platform\":{" + "\""+ "app"+ "\":" + "\"21\"" + "}";
	/*	} else if (m_WhichLeaderboard == 1) {
			param += "{\"email\":" + "\"" + email + "\",\"md5password\":" + "\""+ passwordmd5 + "\"" + ",\"scope\":" + "\"thisweek\",\"limit\":\"" + "0" + "\""+ ",\"platform\":{" + "\""+"app"+ "\":"  + "\"14\"" + "}";
		} else if (m_WhichLeaderboard == 2) {
			param += "{\"email\":" + "\"" + email + "\",\"md5password\":" + "\""+ passwordmd5 + "\"" + ",\"scope\":" + "\"lastweek\",\"limit\":\"" + "0" + "\""+ ",\"platform\":{" + "\""+"app"+ "\":"  + "\"14\"" + "}";
			
		//	param += "{\"email\":" + "\"" + email + "\",\"md5password\":" + "\"" + passwordmd5 + "\"" + ",\"scope\":" + "\"lastweek\",\"limit\":\"" + "0" + "\""+ ",\"platform\":{" + "\""+ "app"+ "\":" + "\"11\"" + "}";
		}*/

		/*if (m_WhichLeaderboard == 0) {
			param += "{\"email\":" + "\"" + email + "\",\"md5password\":" + "\"" + passwordmd5 + "\"" + ",\"scope\":" + "\"total\",\"limit\":\"" + "0" + "\""+ ",\"platform\":{" + "\""+ "app"+ "\":" + "\"11\"" + "}";
		} else if (m_WhichLeaderboard == 1) {
			param += "{\"email\":" + "\"" + email + "\",\"md5password\":" + "\""+ passwordmd5 + "\"" + ",\"scope\":" + "\"thisweek\",\"limit\":\"" + "0" + "\""+ ",\"platform\":{" + "\""+"app"+ "\":"  + "\"11\"" + "}";
		} else if (m_WhichLeaderboard == 2) {
			param += "{\"email\":" + "\"" + email + "\",\"md5password\":" + "\""+ passwordmd5 + "\"" + ",\"scope\":" + "\"lastweek\",\"limit\":\"" + "0" + "\""+ ",\"platform\":{" + "\""+"app"+ "\":"  + "\"11\"" + "}";

			//	param += "{\"email\":" + "\"" + email + "\",\"md5password\":" + "\"" + passwordmd5 + "\"" + ",\"scope\":" + "\"lastweek\",\"limit\":\"" + "0" + "\""+ ",\"platform\":{" + "\""+ "app"+ "\":" + "\"11\"" + "}";
		}*/

		param += "}";


		Debug.Log ("login param: " + param);


		WWWForm form = new WWWForm();
		form.AddField ("parameter", param);

		//Debug.Log ("Url data: " + System.Text.Encoding.UTF8.GetString(form.data));
		WWW www = new WWW(url, form);


		StartCoroutine(WaitForData(www));





		return;
/*		string url = "https://geo-wiki.org/Application/api/Campaign/FotoQuestStats";
		string param = "";

				if (m_CurSelection == 0) {
			param += "{\"scope\":\"" + "total" + "\",\"limit\":\"" + "-1" + "\""+ ",\"platform\":{" + "\""+ "app"+ "\":" + "\"11\"" + "}";
				} else if (m_CurSelection == 1) {
			param += "{\"scope\":\"" + "thisweek" + "\",\"limit\":\"" + "-1" + "\""+ ",\"platform\":{" + "\""+"app"+ "\":"  + "\"11\"" + "}";
				} else if (m_CurSelection == 2) {
			param += "{\"scope\":\"" + "lastweek" + "\",\"limit\":\"" + "-1" + "\""+ ",\"platform\":{" + "\""+ "app"+ "\":" + "\"11\"" + "}";
				}

		param += "}";


		Debug.Log ("login param: " + param);


		WWWForm form = new WWWForm();
		form.AddField ("parameter", param);

		//Debug.Log ("Url data: " + System.Text.Encoding.UTF8.GetString(form.data));
		WWW www = new WWW(url, form);

		StartCoroutine(WaitForData(www));*/
	}


	bool m_bQuestsRead;
	bool m_bPhotosRead;
	bool m_bPercRead;
	string m_NrQuests;
	string m_NrPhotos;
	string m_PercDone;
	ArrayList m_Names;
	ArrayList m_Scores;


	ArrayList m_UploadButtons;
	ArrayList m_EditButtons;
	ArrayList m_UploadQuestUploaded;
	ArrayList m_UploadLabels;
	ArrayList m_UploadButtonsQuestId;
	ArrayList m_UploadShowPictures;


	float m_MoneyEarned;
	float m_MoneyPending;

	IEnumerator WaitForData(WWW www)
	{
		yield return www;

		string[] options = { "Ok" };


		m_Names = new ArrayList();
		m_Scores = new ArrayList();


		// check for errors
		if (www.error == null)
		{
		

			string data = www.text;
			//string[] parts = data.Split (":", 2);

			Debug.Log ("Leaderboard result: " + data);

			JSONObject j = new JSONObject(www.text);
			m_ReadingWhich = -1;

			m_bQuestsRead = false;
			 m_bPhotosRead = false;
			 m_bPercRead = false;

			if (m_WhichLeaderboard == 0) {
				m_PercDone = "0";
			} else if (m_WhichLeaderboard == 1) {
				m_NrQuests = "0";
			} else {
				m_NrPhotos = "0";
			}


		m_MoneyEarned = 0;
		m_MoneyPending = 0;

			accessPinData(j);


		/*	if (m_CurSelection == 0) {
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Points this week: " + "\nPoints last week: 0\nPoints total: 0";
				
				//m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text>().text = "Total Photos: "+ m_NrPhotos+"\nTotal Quests: " + m_NrQuests +  "\nLocations visited: " + m_PercDone +"%";
			} else if (m_CurSelection == 1) {
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text>().text = "Photos this week: "+ m_NrPhotos+"\nQuests this week: " + m_NrQuests +  "\nLocations visited: " + m_PercDone +"%";
			} else if (m_CurSelection == 2) {
				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text>().text = "Photos last week: "+ m_NrPhotos+"\nQuests last week: " + m_NrQuests + "\nLocations visited: " + m_PercDone  +"%";
			} 
*/			
			Debug.Log ("Thisweek: " + m_NrQuests);
			Debug.Log ("LastWeek: " + m_NrPhotos);
			Debug.Log ("Total: " + m_PercDone);

			string strthisweek = "";
			string strlastweek = "";
		//	string strtotal = "";

			m_MoneyEarned /= 100.0f;
			m_MoneyPending /= 100.0f;

			strthisweek = m_NrQuests;//m_MoneyEarned + "";

			/*//if(m_NrQuests != "" && m_NrQuests != null) {
			//	float thisweek = float.Parse (m_NrQuests);
			strthisweek = m_MoneyEarned.ToString ("F2") + "€";
		//	}
		//	if(m_NrPhotos != "" && m_NrPhotos != null) {
		//		float lastweek = float.Parse (m_NrPhotos);
				strlastweek = m_MoneyPending.ToString ("F2") + "€";
		//	}
		*/
			/*if(m_PercDone != "" && m_PercDone != null) {
				float total = float.Parse (m_PercDone);
				strtotal = total.ToString ("F2") + "€";
			}*/


			if (Application.systemLanguage == SystemLanguage.German) {
				string strtext;// = "Diese Woche: " + strthisweek + "\nLetzte Woche: " + strlastweek + "\nInsgesamt: " + strtotal;
				strtext = "Quests hochgeladen: " + strthisweek;
					m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = strtext;
			} else {
			string strtext;// = "This week: " + strthisweek + "\nLast week: " + strlastweek + "\nTotal: " + strtotal;

				strtext = "Quests uploaded: " + strthisweek;

				m_TextInfo.GetComponentInChildren<UnityEngine.UI.Text> ().text = strtext;
			}
		/*
			m_WhichLeaderboard++;
			if (m_WhichLeaderboard <= 2) {
				loadLeaderboard ();
			} */

			/*
			string[] parts = data.Split(new string[] { ":" }, 0);
			string[] parts2 = parts[1].Split(new string[] { "," }, 0);
			string part3 = parts2 [0];

			Debug.Log("WWW Ok!: " + www.data);
			Debug.Log("part1: " + parts[0]);
			Debug.Log("part2: " + parts[1]);
			Debug.Log("part3: " + part3);

			part3 = part3.Replace ("\"", "");
			part3 = part3.Replace ("}", "");


			if (part3.Equals ("null")) {
				if (Application.systemLanguage == SystemLanguage.German) {
					messageBox.Show ("", "Registrierung fehlgeschlagen. Versuchen sie es bitte erneut.", options);
				} else {
					messageBox.Show ("", "Registration failed. Please try again.", options);
				}
				yield return www;
			} else {
				PlayerPrefs.SetString("PlayerId",part3);
				PlayerPrefs.Save ();

				Application.LoadLevel ("DemoMap");
			}*/


		} else {
			Debug.Log("WWW Error: "+ www.error);
			Debug.Log("WWW Error 2: "+ www.text);
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

				}

				if (key == "user") {
					m_ReadingWhich = 5;
				}*/
			if (key == "quests") {
				m_ReadingWhich = 6;
			} else if (key == "moneypending") {
				m_ReadingWhich = 7;
			}
			/*	if (key == "id") {
					m_CurrentPin++;
					if (m_CurrentPin >= 1000) {
						m_CurrentPin = 999;
					}
					m_ReadingWhich = 1;
				} else if (key == "lat") {
					m_ReadingWhich = 2;
				} else if (key == "lon") {
					m_ReadingWhich = 3;
				} else if (key == "weight") {
					m_ReadingWhich = 4;
				} else if (key == "color") {
					m_ReadingWhich = 5;
				} else {
					m_ReadingWhich = 0;
				}*/
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
			//Debug.Log ("string: " + obj.str);
			if (m_ReadingWhich == 6) {
				/*if (m_WhichLeaderboard == 0) {
					m_PercDone = obj.str;
				} else if (m_WhichLeaderboard == 1) {
					m_NrQuests = obj.str;
				} else {
					m_NrPhotos = obj.str;
				}*/
				m_NrQuests = obj.str;
			} else if (m_ReadingWhich == 7) {
				m_NrPhotos = obj.str;
			}
			/*
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
			}*/
			m_ReadingWhich = -1;
			/*if (m_ReadingWhich == 1) {

				//Debug.Log ("m_CurrentPin: " + m_CurrentPin);
				m_Pins [m_CurrentPin].m_Id = obj.str;
				//Debug.Log ("Read pin id: " + obj.str);
			} else if (m_ReadingWhich == 2) {
				m_Pins [m_CurrentPin].m_Lat = double.Parse(obj.str);
			} else if (m_ReadingWhich == 3) {
				m_Pins [m_CurrentPin].m_Lng = double.Parse(obj.str);
			} else if (m_ReadingWhich == 5) {
				m_Pins [m_CurrentPin].m_Color = obj.str;
			}*/
			break;
		case JSONObject.Type.NUMBER:
			Debug.Log ("number: " + obj.n);
			/*
			if (m_ReadingWhich == 3) {
				m_PercDone = obj.n + "";//"asdf";//obj.n + "";
			}*/
	/*		if (m_ReadingWhich == 4) {
				m_Pins [m_CurrentPin].m_Weight = "" + obj.n;
			}*/
			if (m_ReadingWhich == 6) {
				Debug.Log ("Money earned number: " + obj.n);
				m_MoneyEarned = obj.n;
		} else if (m_ReadingWhich == 7) {
			Debug.Log ("Money pending number: " + obj.n);
				m_MoneyPending = obj.n;
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


	}

	public void createQuestList()
	{
//		return;
		int nrentries = 0;// m_Names.Count;

		nrentries = m_NrQuestsDone;

		Debug.Log ("createQuestLit nrquests: " + m_NrQuestsDone);

		int nrentriesactive = 0;
		//for (int i = 0; i < nrentries; i++) {
		for (int i = nrentries-1; i >= 0; i--) {

			/*string strcurquestidparam = "Quest_" + i + "_Id";
			string strcurquestid = PlayerPrefs.GetString(strcurquestidparam);
			Debug.Log ("Quest id " + i + ": " + strcurquestid);*/

			string strdeleted = "Quest_" + i + "_Del";
			int deleted = 0;
			if (PlayerPrefs.HasKey (strdeleted)) {
				deleted = PlayerPrefs.GetInt (strdeleted);
			}

			string stralreadyuploaded = "Quest_" + i + "_Uploaded";
			bool bUploaded = false;
			if (PlayerPrefs.HasKey (stralreadyuploaded)) {
				bUploaded = true;
			}


			if (deleted == 0 && (!bUploaded || nrentriesactive < 50)) { // First 10 entries should be shown no matter if they have already been uploaded
				nrentriesactive++;
			}
		}



		m_UploadButtons = new ArrayList ();
		m_EditButtons = new ArrayList ();
		m_UploadQuestUploaded = new ArrayList ();

		m_UploadLabels = new ArrayList ();
		m_UploadButtonsQuestId = new ArrayList ();
		m_UploadShowPictures = new ArrayList ();


		ArrayList questsshown = new ArrayList ();

		//nrentriesactive = 10;//For test, comment this out again

		RectTransform rectTransform2 = m_Content.GetComponent<RectTransform> ();
		//rectTransform2.sizeDelta.
		//rt.sizeDelta = new Vector2 (100, 100);
		float scalex = rectTransform2.sizeDelta.x;
		float scaley = rectTransform2.sizeDelta.y;
		float heightentry = 350.0f;//300.0f;//240.0f;//250.0f;//200.0f;
		//rectTransform2.sizeDelta = new Vector2 (scalex, heightentry * nrentries + 100.0f);

		//nrentriesactive = 10;
		//nrentriesactive = 40;
		rectTransform2.sizeDelta = new Vector2 (scalex, heightentry * nrentriesactive + 100.0f);

		float posoffset = 0;
		int nrentriesadded = 0;
		int curreport = 1;
		//nrentries = 40;
		for (int i = nrentries-1; i >= 0; i--) {
		//for(int testi=0; testi<10; testi++) { // For test
		//	int i = nrentries - 1; // For test
			Debug.Log("Create quest entry: " + i);

			GameObject copy;
			RectTransform rectTransform;
			float curpos;
			float curposx;
			int currank;
			string text;

			/* = (GameObject)GameObject.Instantiate (m_RankS);

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
*/

		//Debug.Log("Quest_1");
			string strdeleted = "Quest_" + i + "_Del";
			int deleted = 0;
			if (PlayerPrefs.HasKey (strdeleted)) {
				deleted = PlayerPrefs.GetInt (strdeleted);
			}

			string stralreadyuploaded = "Quest_" + i + "_Uploaded";
			bool bUploaded = false;
			if (PlayerPrefs.HasKey (stralreadyuploaded)) {
				bUploaded = true;
			}

			if (deleted == 0 && (!bUploaded || nrentriesadded < 50)) {
				nrentriesadded++;
				//Debug.Log("Quest_2");

				copy = (GameObject)GameObject.Instantiate (m_NameS);
				copy.transform.SetParent (m_Content.transform, false);
				copy.SetActive (true);
				rectTransform = copy.GetComponent<RectTransform> ();
				curpos = rectTransform.localPosition.y;
				curposx = rectTransform.localPosition.x;
				curpos -= posoffset;//i * heightentry;
				rectTransform.localPosition = new Vector2 (curposx, curpos);
				m_AddedTexts.Add (copy);
				text = "Tobias";


			//	Debug.Log("Quest_3");

				int curquestid = i + 1;
				string strinreach = "Quest_" + i + "_PointReached";
				int inreach = PlayerPrefs.GetInt (strinreach);

				text = "inr: " + inreach + " h: ";
			text = LocalizationSupport.GetString("Survey") + " " + curquestid;//"Quest";// + curquestid;

				string questtype = "Quest_" + i + "_" + "Report";
				bool bIsReport = false;
			if(PlayerPrefs.HasKey(questtype)) {
				int questtypei = PlayerPrefs.GetInt (questtype);
				if(questtypei == 1) {
					text = "Report";//+ curreport;
					curreport++;
						bIsReport = true;
				}
			}


/*			//float heading = 120.0f;
			if (inreach == 1) {
				for (int photo = 1; photo < 6; photo++) {
					string strheading = "Quest_" + i + "_" + photo + "_Heading";
					float heading = PlayerPrefs.GetFloat (strheading);
					text += heading + " ";
				}
			} else {
				for (int photo = 2; photo < 7; photo++) {
					string strheading = "Quest_" + i + "_" + photo + "_Heading";
					float heading = PlayerPrefs.GetFloat (strheading);
					text += heading + " ";
				}
			}*/


			//	Debug.Log("Quest_4");
				//text = (string)m_Names [i];
				copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = text;


			//------------------

			copy = (GameObject)GameObject.Instantiate (m_Desc);
			copy.transform.SetParent (m_Content.transform, false);
			copy.SetActive (true);
			rectTransform = copy.GetComponent<RectTransform> ();
			curpos = rectTransform.localPosition.y;
			curposx = rectTransform.localPosition.x;
			curpos -= posoffset;//i * heightentry;
			rectTransform.localPosition = new Vector2 (curposx, curpos);
			m_AddedTexts.Add (copy);
			text = "Tobias";

			/*
			//	Debug.Log("Quest_3");

			int curquestid = i + 1;
			string strinreach = "Quest_" + i + "_PointReached";
			int inreach = PlayerPrefs.GetInt (strinreach);

			text = "inr: " + inreach + " h: ";*/
				text = "";//Date: ";//"Done on ";

			string starttime = "Quest_" + i + "_" + "StartQuestTime";
				Debug.Log ("Starttime: " + starttime);
				if (PlayerPrefs.HasKey (starttime)) {
					string date = PlayerPrefs.GetString (starttime);
					string[] strArr;
					strArr = date.Split(new string[] { " " }, System.StringSplitOptions.None);
					text += strArr [0];
				} else {
					text += "No date";
				}
				/*if (bIsReport) {
					int reportanswer = PlayerPrefs.GetInt ("Quest_" + i + "_" + "Report_Type");
					if (reportanswer == 1) {
						text += "\nBuilding has been destroyed.";
					} else {
						text += "\nA Building has been built.";
					}

				} else {

					if (PlayerPrefs.HasKey ("Quest_" + i + "_" + "CampaignType")) {
						int campaigntype = PlayerPrefs.GetInt ("Quest_" + i + "_" + "CampaignType");
						if (campaigntype == 0) {
							text += "\nBuilding campaign";
						} else if (campaigntype == 1) {
							text += "\nFlash campaign";
						} else if (campaigntype == 2) {
							text += "\nLULC campaign";
						}
					} else {
					//	int campaigntype = PlayerPrefs.GetInt ("Quest_" + i + "_" + "CampaignType");
				//		text += "\nOther campaign " + campaigntype;
					}
				}*/


			/*if(PlayerPrefs.HasKey("Quest_" + i + "_" + "EndPositionX")) {
				text += "\nCoordinate: " + PlayerPrefs.GetFloat("Quest_" + i + "_" + "EndPositionX") + ", " + PlayerPrefs.GetFloat("Quest_" + i + "_" + "EndPositionY");
			} else {
				text += "\nCoordinate: No coordinate";
			}*/
			/*			//float heading = 120.0f;
			if (inreach == 1) {
				for (int photo = 1; photo < 6; photo++) {
					string strheading = "Quest_" + i + "_" + photo + "_Heading";
					float heading = PlayerPrefs.GetFloat (strheading);
					text += heading + " ";
				}
			} else {
				for (int photo = 2; photo < 7; photo++) {
					string strheading = "Quest_" + i + "_" + photo + "_Heading";
					float heading = PlayerPrefs.GetFloat (strheading);
					text += heading + " ";
				}
			}*/


			//	Debug.Log("Quest_4");
			//text = (string)m_Names [i];
			copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = text;


			//------------------



				copy = (GameObject)GameObject.Instantiate (m_Image);
				copy.transform.SetParent (m_Content.transform, false);
				copy.SetActive (true);
				rectTransform = copy.GetComponent<RectTransform> ();
				curpos = rectTransform.localPosition.y;
				curposx = rectTransform.localPosition.x;
				curpos -= posoffset + 71;// - 80;
				rectTransform.localPosition = new Vector2 (curposx, curpos);
				m_AddedTexts.Add (copy);

				UnityEngine.UI.Image image = copy.GetComponent<UnityEngine.UI.Image> ();


			//	Debug.Log("Quest_5");

				string name = Application.persistentDataPath + "/" + "Quest_Img_" + i + "_" + 1 + ".jpg";
			/*	if (inreach == 0) {
					name = Application.persistentDataPath + "/" + "Quest_Img_" + i + "_" + 2 + ".jpg";
				}
			*/
			//	Debug.Log ("load file: " + name);

				if (File.Exists (name)) {
					byte[] bytes = File.ReadAllBytes (name);

					if (bytes != null) {
						Texture2D texture = new Texture2D (1, 1);
						texture.LoadImage (bytes);
						//			text.LoadImage(Convert.FromBase64String(PlayerPrefs.GetString("PrimaryImage_ByteString")));

						Sprite sprite = Sprite.Create (texture, new Rect (0, 0, texture.width, texture.height), new Vector2 (.5f, .5f));


						image.sprite = sprite;//m_Sprite;
					}
				}


			//	Debug.Log("Quest_6");

				
				//	bUploaded = true;

				//	if (bUploaded) {
				copy = (GameObject)GameObject.Instantiate (m_Info);
				copy.transform.SetParent (m_Content.transform, false);
				if (bUploaded) {
					copy.SetActive (true);
				} else {
					copy.SetActive (false);
				}
				rectTransform = copy.GetComponent<RectTransform> ();
				curpos = rectTransform.localPosition.y;
				curposx = rectTransform.localPosition.x;
				curpos -= posoffset;//i * heightentry;
				rectTransform.localPosition = new Vector2 (curposx, curpos);
				m_AddedTexts.Add (copy);

				m_UploadQuestUploaded.Add (copy);

				string strcurquestidparam = "Quest_" + i + "_Id";
				string strcurquestid = PlayerPrefs.GetString(strcurquestidparam);

				// Check if quest already added, if added then quest has beed declined otherwise could not get into it
				bool bAlreadyAdded = false;
				for (int iterquest = 0; iterquest < questsshown.Count; iterquest++) {
					if(strcurquestid.CompareTo(questsshown [iterquest]) == 0) {
						bAlreadyAdded = true;
					}
				}
				questsshown.Add (strcurquestid);

				/*int curstatus = 2;
				if (!bAlreadyAdded) {
					curstatus = getStatusOfQuest (strcurquestid);

					if (curstatus == 2) {
						// Quest has been rejected -> see if quest has already been made before if yes check that quest 
						int howmanystatuses = howManyStatusesForQuest(strcurquestid);
						int howmanyquests = howManyTimesMadeQuest (strcurquestid);

						if (howmanyquests > howmanystatuses) {
							curstatus = 0;
						}
					}
				}
				if (curstatus == 0) {*/
				/*	if (Application.systemLanguage == SystemLanguage.German) {
						text = "Quest hochgeladen.";
					} else {
						text = "Quest uploaded.";
					}*/
				text = "";//LocalizationSupport.GetString("QuestUploaded");
				/*} else if (curstatus == 1) {
					if (Application.systemLanguage == SystemLanguage.German) {
						text = "Quest wurde akzeptiert.";
					} else {
					text = "Quest accepted.";
					}
				} else if (curstatus == 2) {
					if (Application.systemLanguage == SystemLanguage.German) {
						text = "Quest wurde abgelehnt.";
					} else {
					text = "Quest rejected.";
					}
				}*/

				copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = text;
				GameObject textuploaded = copy;
				//} else {


			//	Debug.Log("Quest_7");

				copy = (GameObject)GameObject.Instantiate (m_UploadQuest);
				copy.transform.SetParent (m_Content.transform, false);
				if (bUploaded) {
					copy.SetActive (false);
					
					string struploadid = "Quest_" + i + "_Uploaded_Id";
					if (PlayerPrefs.HasKey(struploadid))
					{
						LoadQuestStatus(i, PlayerPrefs.GetString(struploadid), textuploaded);
					}
					else
					{
						textuploaded.GetComponent<Text>().text = LocalizationSupport.GetString("QuestUploaded");
					}
				} else {
					copy.SetActive (true);
				}
				rectTransform = copy.GetComponent<RectTransform> ();
				curpos = rectTransform.localPosition.y;
				curposx = rectTransform.localPosition.x;
				curpos -= posoffset;//i * heightentry;
				rectTransform.localPosition = new Vector2 (curposx, curpos);
				m_AddedTexts.Add (copy);

				m_UploadButtons.Add (copy);
				m_UploadButtonsQuestId.Add (i + "");


				if (Application.systemLanguage == SystemLanguage.German && false/*|| true*/) {
					copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = "HOCHLADEN";
				} else {
					copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Upload");//"UPLOAD";
				}

				UnityEngine.UI.Button b = copy.GetComponent<UnityEngine.UI.Button> ();
				//	b.onClick.AddListener(() => AddListener(b, i+""));
				AddListener (b, i + "");




				//Debug.Log("Quest_8");
		
				copy = (GameObject)GameObject.Instantiate (m_ShowQuestPhotos);
				copy.transform.SetParent (m_Content.transform, false);

				if (bUploaded) {
					copy.SetActive (false);
				} else {
					copy.SetActive (true);
				}

				rectTransform = copy.GetComponent<RectTransform> ();
				curpos = rectTransform.localPosition.y;
				curposx = rectTransform.localPosition.x;
				curpos -= posoffset;//i * heightentry;
				rectTransform.localPosition = new Vector2 (curposx, curpos);
				//m_AddedTexts.Add (copy);

				m_EditButtons.Add (copy);
				//m_UploadButtons.Add (copy);
				//m_UploadButtonsQuestId.Add (i + "");


				if (Application.systemLanguage == SystemLanguage.German && false) {
					copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = "EDITIEREN";
				} else {
			copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Edit");// "EDIT";
				}

				UnityEngine.UI.Button b2 = copy.GetComponent<UnityEngine.UI.Button> ();
				//AddListener (b2, i + "");
				AddListenerShow (b2, i + "");




				//----------------------------
				// View quest hallohallo


		/*	Debug.Log("Quest_9");

				copy = (GameObject)GameObject.Instantiate (m_ShowQuest);
				copy.transform.SetParent (m_Content.transform, false);
				copy.SetActive (true);
		
				rectTransform = copy.GetComponent<RectTransform> ();
				curpos = rectTransform.localPosition.y;
				curposx = rectTransform.localPosition.x;
				curpos -= posoffset;//i * heightentry;
				rectTransform.localPosition = new Vector2 (curposx, curpos);
				


				if (Application.systemLanguage == SystemLanguage.German) {
					copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Quest hochladen";
				}

				UnityEngine.UI.Button bquest = copy.GetComponent<UnityEngine.UI.Button> ();
				//	b.onClick.AddListener(() => AddListener(b, i+""));
				AddListenerImage (bquest, i + "");
*/
				//	}


				posoffset += heightentry;

				/*
			copy = (GameObject)GameObject.Instantiate (m_ScoreS);
			copy.transform.SetParent (m_Content.transform, false);
			copy.SetActive (true);
			rectTransform = copy.GetComponent<RectTransform> ();
			curpos = rectTransform.localPosition.y;
			curposx = rectTransform.localPosition.x;
			curpos -= i * 30.0f;
			rectTransform.localPosition = new Vector2 (curposx, curpos);
			m_AddedTexts.Add (copy);
			string textscore = "23422";//(string)m_Scores [i];
			copy.GetComponentInChildren<UnityEngine.UI.Text> ().text = textscore;//"2323";
			*/

			//	Debug.Log("Quest_10");

			}

		}

		Debug.Log ("Quest list created");
		questsshown = null;

	}


	void AddListener(UnityEngine.UI.Button b, string value) 
	{
		b.onClick.AddListener(() => OnQuestUploadClickedValue(value));
	}


	void AddListenerImage(UnityEngine.UI.Button b, string value) 
	{
		b.onClick.AddListener(() => OnShowQuestClickedValue(value));
	}


	void AddListenerShow(UnityEngine.UI.Button b, string value) 
	{
		b.onClick.AddListener(() => OnShowQuestClickedValue(value));
	}

	bool checkLoggedIn() {
		
		//-------------------
		// For prototype no uploading
		
		/*string[] options = {"Ok" };
		messageBox.Show ("", "In this version uploading is not possible.", null, options);
		return false;*/
		//-------------------
		
		//return true;
		if (PlayerPrefs.HasKey ("PlayerName") == false || PlayerPrefs.GetInt("LoggedOut") == 1) {
			UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnMsgBoxClicked);
			if (Application.systemLanguage == SystemLanguage.German && false) {
				string[] options = { "Abbrechen", "Login" };
				messageBox.Show ("", "Du musst dich anmelden um Quests hochladen zu können.", ua, options);
			} else {
				string[] options = { LocalizationSupport.GetString("BtnLater"), LocalizationSupport.GetString("BtnLogin") };
				messageBox.Show ("", LocalizationSupport.GetString("UploadLoginFirst"), ua, options);
			}
			return false;
		}
		return true;
	}


	public void OnShowQuestClickedValue(string param) {
		Debug.Log ("OnShowQuestClickedValue: " + param);
		openQuest (param);
	}

	public void OnQuestUploadClickedValue(string param) {
		Debug.Log ("OnQuestUploadClickedValue: " + param);

		if (checkLoggedIn () == false) {
			return;
		}


		m_UploadingBack.SetActive (true);
		m_UploadingText.SetActive (true);
		m_UploadingTextWait.SetActive (true);
		m_UploadingImage.SetActive (true);

		if (Application.systemLanguage == SystemLanguage.German && false) {
			m_UploadingTextWait.GetComponentInChildren<UnityEngine.UI.Text> ().text =  "Bitte warten...";
			m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Quests werden hochgeladen.";
		} else {
		m_UploadingTextWait.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("PleaseWait");//"Please wait...";
		m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("UploadingQuests");//"Uploading all quests.";
		}

		m_bUploadAll = false;
		m_UploadAllIter = int.Parse (param);

		uploadQuest(int.Parse (param));
	} 

	bool m_bUploadAll = false;
	int m_UploadAllIter = 0;

	public void OnQuestUploadAllClicked() {
		Debug.Log ("OnQuestUploadAllClicked");

		if (checkLoggedIn () == false) {
			return;
		}


		if (m_NrQuestsDone <= 0) {
			return;
		}

/*
	string[] options2 = { "Ok" };
	messageBox.Show ("", LocalizationSupport.GetString("QuestsUploadSuccessful"), options2);
		return;*/

		m_bUploadAll = true;
		m_UploadAllIter = 0;

		m_UploadingBack.SetActive (true);
		m_UploadingText.SetActive (true);
		m_UploadingTextWait.SetActive (true);
		m_UploadingImage.SetActive (true);

		if (Application.systemLanguage == SystemLanguage.German && false) {
			m_UploadingTextWait.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Bitte warten...";
			m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Quests werden hochgeladen.";
		} else {
			m_UploadingTextWait.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("PleaseWait");//"Please wait...";
			m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("UploadingQuests");//"Uploading all quests.";
		}

		uploadQuest (0);
	}

	public void onCloseQuestClicked() {
		m_bShowQuest = false;
		m_BtnCloseQuest.SetActive (false);
		m_BtnDeleteQuest.SetActive (false);
		m_ImageQuest.SetActive (false);

		m_SkyQuestBack.SetActive (false);

		m_QuestTitle.SetActive (false);
		m_QuestText.SetActive (false);

		m_Image1Quest.SetActive (false);
		m_Image2Quest.SetActive (false);
		m_Image3Quest.SetActive (false);
		m_Image4Quest.SetActive (false);
		m_Image5Quest.SetActive (false);

		m_BtnEditImage1.SetActive (false);
		m_BtnEditImage2.SetActive (false);
		m_BtnEditImage3.SetActive (false);
		m_BtnEditImage4.SetActive (false);
		m_BtnEditImage5.SetActive (false);
		m_TextQuestExplanation.SetActive (false);
	}

	public void onDeleteQuestClicked() {
		UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnDeleteClicked);
		if (Application.systemLanguage == SystemLanguage.German && false) {
			string[] options = { "Abbrechen", "Löschen" };
			messageBox.Show ("", "Willst du wirklich die Quest löschen?", ua, options);
		} else {
		string[] options = { LocalizationSupport.GetString("Cancel"), LocalizationSupport.GetString("Delete") };
		messageBox.Show ("", LocalizationSupport.GetString("ReallyDelete"), ua, options);
		}
	}

	void OnDeleteClicked(string result) {
		Debug.Log ("OnMsgBoxClicked: " + result);
		if (result == "Löschen" || result == "Delete") {
			Debug.Log ("Quest deleted!");

			string strdeleted = "Quest_" + m_OpenQuestId + "_Del";
			PlayerPrefs.SetInt (strdeleted, 1);

			Application.LoadLevel ("Quests");
		} else {
			Debug.Log ("Quest not deleted");
		}
	}

	IEnumerator loadQuestImages(int inreach, int questid) {
		string name;

		//if (inreach == 1) {
			name = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 1 + ".jpg";
		/*} else {
			name = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 2 + ".jpg";
		}*/
		m_bImg1Set = false;
		UnityEngine.UI.Image image = m_Image1Quest.GetComponent<UnityEngine.UI.Image>();
		if(File.Exists(name)) {
			m_StrImg1 = name;
			m_bImg1Set = true;
			byte[] bytes = File.ReadAllBytes (name);
			if(bytes != null) {
				Texture2D texture = new Texture2D(1, 1);
				texture.LoadImage (bytes);
				Sprite sprite = Sprite.Create (texture, new Rect(0,0,texture.width,texture.height), new Vector2(.5f,.5f));
				image.sprite = sprite;//m_Sprite;
			}
		}


		//if (inreach == 1) {
			name = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 2 + ".jpg";
		/*} else {
			name = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 3 + ".jpg";
		}*/
		m_bImg2Set = false;
		image = m_Image2Quest.GetComponent<UnityEngine.UI.Image>();
		if(File.Exists(name)) {
			m_StrImg2 = name;
			m_bImg2Set = true;
			byte[] bytes = File.ReadAllBytes (name);
			if(bytes != null) {
				Texture2D texture = new Texture2D(1, 1);
				texture.LoadImage (bytes);
				Sprite sprite = Sprite.Create (texture, new Rect(0,0,texture.width,texture.height), new Vector2(.5f,.5f));
				image.sprite = sprite;//m_Sprite;
			}
		}


		//if (inreach == 1) {
			name = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 3 + ".jpg";
		/*} else {
			name = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 4 + ".jpg";
		}*/
		m_bImg3Set = false;
		image = m_Image3Quest.GetComponent<UnityEngine.UI.Image>();
		if(File.Exists(name)) {
			m_StrImg3 = name;
			m_bImg3Set = true;
			byte[] bytes = File.ReadAllBytes (name);
			if(bytes != null) {
				Texture2D texture = new Texture2D(1, 1);
				texture.LoadImage (bytes);
				Sprite sprite = Sprite.Create (texture, new Rect(0,0,texture.width,texture.height), new Vector2(.5f,.5f));
				image.sprite = sprite;//m_Sprite;
			}
		}


		//if (inreach == 1) {
			name = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 4 + ".jpg";
		/*} else {
			name = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 5 + ".jpg";
		}*/
		image = m_Image4Quest.GetComponent<UnityEngine.UI.Image>();
		m_bImg4Set = false;
		if(File.Exists(name)) {
			m_StrImg4 = name;
			m_bImg4Set = true;
			byte[] bytes = File.ReadAllBytes (name);
			if(bytes != null) {
				Texture2D texture = new Texture2D(1, 1);
				texture.LoadImage (bytes);
				Sprite sprite = Sprite.Create (texture, new Rect(0,0,texture.width,texture.height), new Vector2(.5f,.5f));
				image.sprite = sprite;//m_Sprite;
			}
		}


		//if (inreach == 1) {
			name = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 5 + ".jpg";
		/*} else {
			name = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 6 + ".jpg";
		}*/
		image = m_Image5Quest.GetComponent<UnityEngine.UI.Image>();
		m_bImg5Set = false;
		if(File.Exists(name)) {
			m_StrImg5 = name;
			m_bImg5Set = true;
			byte[] bytes = File.ReadAllBytes (name);
			if(bytes != null) {
				Texture2D texture = new Texture2D(1, 1);
				texture.LoadImage (bytes);
				Sprite sprite = Sprite.Create (texture, new Rect(0,0,texture.width,texture.height), new Vector2(.5f,.5f));
				image.sprite = sprite;//m_Sprite;
			}
		}


		m_Image1Quest.SetActive (true);
		m_Image2Quest.SetActive (true);
		m_Image3Quest.SetActive (true);
		m_Image4Quest.SetActive (true);
		m_Image5Quest.SetActive (true);

		m_BtnEditImage1.SetActive (true);
		m_BtnEditImage2.SetActive (true);
		m_BtnEditImage3.SetActive (true);
		m_BtnEditImage4.SetActive (true);
		m_BtnEditImage5.SetActive (true);
		m_TextQuestExplanation.SetActive (true);
		m_QuestText.SetActive (false);

		yield return new WaitForSeconds(0);
	}


	int m_OpenQuestId = -1;
	string m_CurrentOpenQuest;
	bool m_bLoadImagesForQuest = false;
	int m_LoadImagesForQuestInReach = 0;
	int m_LoadImagesForQuestId = 0;
	int m_LoadImagesForQuestIter = 0;
	void openQuest(string whichquest) {

		string stralreadyuploaded = "Quest_" + whichquest + "_Uploaded";
		if(PlayerPrefs.HasKey(stralreadyuploaded)) {
				return; // Dont open uploaded quests anymore
		}


		int iquest = int.Parse(whichquest);

		int questtitlenr = iquest + 1;
		m_QuestTitle.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Quest " + questtitlenr;

		m_ImageQuest.SetActive (true);
		m_QuestTitle.SetActive (true);

		m_OpenQuestId = iquest;
		m_CurrentOpenQuest = whichquest;

		int questid = iquest;
		int inreach = 1;
		if (PlayerPrefs.HasKey ("Quest_" + questid + "_PointReached")) {
			inreach = PlayerPrefs.GetInt ("Quest_" + questid + "_PointReached");
			if (inreach == 0) {
			}
		}

		float lat = 0.0f;
		float lng = 0.0f;

		lat = PlayerPrefs.GetFloat ("Quest_" + questid + "_" + "LandCover" + "_Lat");
		lng = PlayerPrefs.GetFloat ("Quest_" + questid + "_" + "LandCover" + "_Lng");
		string date = PlayerPrefs.GetString ("Quest_" + questid + "_" + "LandCover" + "_Timestamp");



		







		//Debug.Log("Quest coord: " + PlayerPrefs.GetFloat ("Quest_" + questid + "_" + "LandCover" + "_Lat"));
		iquest++;
		m_bShowQuest = true;
		m_BtnCloseQuest.SetActive (true);
		m_BtnDeleteQuest.SetActive (true);
		m_ImageQuest.SetActive (true);
		m_SkyQuestBack.SetActive (false);
		m_QuestTitle.SetActive (true);
	//	m_QuestText.SetActive (true);

	/*
		m_Image1Quest.SetActive (true);
		m_Image2Quest.SetActive (true);
		m_Image3Quest.SetActive (true);
		m_Image4Quest.SetActive (true);
		m_Image5Quest.SetActive (true);

		m_BtnEditImage1.SetActive (true);
		m_BtnEditImage2.SetActive (true);
		m_BtnEditImage3.SetActive (true);
		m_BtnEditImage4.SetActive (true);
		m_BtnEditImage5.SetActive (true);
		m_TextQuestExplanation.SetActive (true);*/

		//m_QuestTitle.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Quest " + iquest;
	/*
		if (inreach == 1) {
			if (Application.systemLanguage == SystemLanguage.German) {
				m_QuestText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Datum: " + date + "\nPunkt erreicht.\nKoordinate: " + lat + ", " + lng;
			} else {
				m_QuestText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Date: " + date + "\nPoint reached.\nCoordinate: " + lat + ", " + lng;
			}
		} else {
			if (Application.systemLanguage == SystemLanguage.German) {
				m_QuestText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Datum: " + date + "\nPunkt nicht erreicht.\nKoordinate: " + lat + ", " + lng;
			} else {
				m_QuestText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Date: " + date + "\nPoint not reached.\nCoordinate: " + lat + ", " + lng;
			}
		}
*/
		if (Application.systemLanguage == SystemLanguage.German && false) {
			m_QuestText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Wird geladen...";
		} else {
		m_QuestText.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Loading");//"Loading...";
		}
		m_QuestText.SetActive (true);
		m_LoadImagesForQuestIter = 0;
		m_bLoadImagesForQuest = true;
		m_LoadImagesForQuestInReach = inreach;
		m_LoadImagesForQuestId = questid;
		//StartCoroutine(loadQuestImages(inreach, questid));

	}

	void OnMsgBoxClicked(string result) {
		Debug.Log ("OnMsgBoxClicked: " + result);
		if (result.CompareTo(LocalizationSupport.GetString("BtnLogin")) == 0) {
			PlayerPrefs.SetInt ("LoginReturnToQuests", 1);
			PlayerPrefs.SetInt ("RegisterMsgShown", 0);
			PlayerPrefs.Save ();

			Application.LoadLevel ("StartScreen");
		}
	}

	void OnMsgBoxStartSurveyClicked(string result)
	{
		if (result.CompareTo(LocalizationSupport.GetString("No")) == 0)
		{
			PlayerPrefs.SetInt("SurveyFilledOut3", 1);
			PlayerPrefs.Save();
		} 
		else if (result.CompareTo(LocalizationSupport.GetString("Sure")) == 0)
		{
			PlayerPrefs.SetInt("SurveyFilledOut3", 1);
			PlayerPrefs.Save();
			Application.OpenURL("https://docs.google.com/forms/d/1KSOgj29W243KPeMC9AOHLVCk4HlYUPkLonRLluT3eKU/viewform?edit_requested=true");
		}
	}

	void uploadQuest(int questid)
	{
		if (!m_bLoadToken)
		{
			m_bLoadToken = true;
			m_LoadTokenQuestId = questid;
			loadToken();
		}
	}
	
	void uploadQuestTokenLoaded(int questid)
	{
		Debug.Log("Upload quest token loaded: " + questid);
		int whichquest = questid + 1;
		if (Application.systemLanguage == SystemLanguage.German && false) {
			m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Quest " + whichquest + " wird hochgeladen.";
		} else {
			m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("UploadingQuest1") + " " + whichquest +" " + LocalizationSupport.GetString("UploadingQuest2");
		}

		string strdeleted = "Quest_" + questid + "_Del";
		int deleted = 0;
		if (PlayerPrefs.HasKey (strdeleted)) {
			deleted = PlayerPrefs.GetInt (strdeleted);
		}



		string stralreadyuploaded = "Quest_" + questid + "_Uploaded";
		bool bUploaded = false;
		if (PlayerPrefs.HasKey (stralreadyuploaded) || deleted==1) {

			if (!m_bUploadAll) {
				m_UploadingBack.SetActive (false);
				m_UploadingText.SetActive (false);
				m_UploadingTextWait.SetActive (false);
				m_UploadingImage.SetActive (false);
			} else {
				m_UploadAllIter++;
				if (m_UploadAllIter < m_NrQuestsDone) {
					uploadQuest (m_UploadAllIter);
				} else {
					m_UploadingBack.SetActive (false);
					m_UploadingText.SetActive (false);
					m_UploadingTextWait.SetActive (false);
					m_UploadingImage.SetActive (false);
				}
			}

			return;
		}

		/*string param = "{\"location\": {";
		float lat = PlayerPrefs.GetFloat("Quest_" + questid + "_LandCover_Lat");
		param += "\"x\":" + lat;

		float lng = PlayerPrefs.GetFloat("Quest_" + questid + "_LandCover_Lng");
		param += ",\"y\":" + lng;

		param += "},\"storageType\": ";
		param += "" + PlayerPrefs.GetInt("Quest_" + questid + "_StorageType");
		param += ",\"extData\": \"";
		

		string data = PlayerPrefs.GetString("Quest_" + questid + "_CropDesc");

		string datadecoded = UnityWebRequest.EscapeURL(data);
		param += datadecoded;// "This is a test";

		param += "\"";
		param += "}";
		Debug.Log("body upload: " + param);*/
		
		string param = "{\"location\": [";

		float lat = 0.0f;
		float lng = 0.0f;
		
		int nrpolygons = PlayerPrefs.GetInt("Quest_" + questid + "_NrPolygons");
		if (nrpolygons > 0)
		{
			for (int i = 0; i < nrpolygons; i++)
			{
				if (i != 0)
				{
					param += ",";
				}
				param += "{";
				lat = PlayerPrefs.GetFloat("Quest_" + questid + "_Poly" + i + "_Y");
				param += "\"yLat\":" + lat;

				lng = PlayerPrefs.GetFloat("Quest_" + questid + "_Poly" + i + "_X");
				param += ",\"xLng\":" + lng;
				param += "}";
			}
			
			param += ",{";
			 lat = PlayerPrefs.GetFloat("Quest_" + questid + "_Poly" + 0 + "_Y");
			param += "\"yLat\":" + lat;

			lng = PlayerPrefs.GetFloat("Quest_" + questid + "_Poly" + 0 + "_X");
			param += ",\"xLng\":" + lng;
			param += "}";
		}
		else
		{
			param += "{";
			 lat = PlayerPrefs.GetFloat("Quest_" + questid + "_LandCover_Lat");
			param += "\"yLat\":" + lat;

			 lng = PlayerPrefs.GetFloat("Quest_" + questid + "_LandCover_Lng");
			param += ",\"xLng\":" + lng;
			param += "}";
		}
		
		/*
		float lat = PlayerPrefs.GetFloat("Quest_" + questid + "_LandCover_Lat");
		param += "\"x\":" + lat;

		float lng = PlayerPrefs.GetFloat("Quest_" + questid + "_LandCover_Lng");
		param += ",\"y\":" + lng;*/

		param += "],\"data\": \"";
		

		string data = PlayerPrefs.GetString("Quest_" + questid + "_CropDesc");

		/*float value = 123.456f;
		m_DataToSend = data;
		m_DataToSend = "Value test: " + value;*/
		string datadecoded = UnityWebRequest.EscapeURL(data);
		param += datadecoded;// "This is a test";

		param += "\"";
		param += "}";
		Debug.Log("body upload: " + param);

		string url = "https://server.org/survey";
		StartCoroutine(StartUploadingQuest(url, param)); 
	}

	private string m_ParamError = "";
	//private string m_DataToSend = "";
	
	IEnumerator StartUploadingQuest(string url, string param)
	{
		//string token = PlayerPrefs.GetString("Token");
		
		string[] options3 = { LocalizationSupport.GetString("Login") };

		//Debug.Log("header: " + "Bearer " + token);
		Debug.Log("Body: " + param);
		Debug.Log("Url: " + url);

		using (UnityWebRequest www = UnityWebRequest.Post(url, "POST"))//UnityWebRequest.Post(url, param))
		{
			byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(param);
			www.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.SetRequestHeader("Content-Type", "application/json");
			www.SetRequestHeader("Authorization", "Bearer " + m_Token);
			www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.certificateHandler = new AcceptAnyCertificate();

			//www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error: " + www.error);

				/*if (www.error == "HTTP/1.1 401 Unauthorized")
				{
					//UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string>(OnLoginAgainClicked);

					//UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string>(OnLoginAgainClicked);
					//messageBox.Show("", LocalizationSupport.GetString("LoginExpired"), ua, options3);
				}
				else
				{*/
				UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnMsgBoxErrorClicked);
				
				
					if (!m_bUploadAll)
					{
						m_UploadingBack.SetActive(false);
						m_UploadingText.SetActive(false);
					//	m_UploadingText2.SetActive(false);
						m_UploadingTextWait.SetActive(false);
						m_UploadingImage.SetActive(false);

						string[] options2 = { "Ok" };
						string errormsg = www.downloadHandler.text;
						if (errormsg.Length <= 0)
						{
							messageBox.Show("", "Upload failed 1: " + www.error, ua, options2);
							m_ParamError = param;
						}
						else
						{
							GUIUtility.systemCopyBuffer = www.downloadHandler.text;
							messageBox.Show("", "Upload failed 2: " + www.downloadHandler.text, ua, options2);
							m_ParamError = param;
						}

						//m_TMPDebug.text = m_DataToSend;
					}
					else
					{
						m_UploadAllIter++;
						if (m_UploadAllIter < m_NrQuestsDone)
						{
							uploadQuest(m_UploadAllIter);
						}
						else
						{
							m_UploadingBack.SetActive(false);
							m_UploadingText.SetActive(false);
						//	m_UploadingText2.SetActive(false);
							m_UploadingTextWait.SetActive(false);
							m_UploadingImage.SetActive(false);

							string[] options2 = { "Ok" };
							//messageBox.Show ("", "Upload failed: " + www.data , options2);

							string errormsg = www.error;
							if (errormsg.Length <= 0)
							{
								m_ParamError = param;
								messageBox.Show("", "Upload failed 2: " + www.error, ua, options2);
							}
							else
							{
								m_ParamError = param;
								string data = www.downloadHandler.text;
								if (data.Contains("Location is outside dataset extent."))
								{
									messageBox.Show("", "Upload failed: Location is outside dataset extent.", ua, options2);
								}
								else
								{
									GUIUtility.systemCopyBuffer = www.downloadHandler.text;
									
									messageBox.Show("", "Upload failed 2: " + www.downloadHandler.text, ua, options2);
									m_ParamError = param;
									
									//m_TMPDebug.text = m_DataToSend;
								}
							}

						}
					}
				//}
			}
			else
			{
				string data = www.downloadHandler.text;
				//string[] parts = data.Split (":", 2);

				Debug.Log("Upload successful result: " + data + " iter: " + m_UploadAllIter);
				
				
				string struploadedid = "Quest_" + m_UploadAllIter + "_Uploaded_Id";
				PlayerPrefs.SetString (struploadedid, data);
				PlayerPrefs.Save ();

				questUploadImages(data);
			}
			}
	}

	int m_ImagesUploaded = 0;
	int m_NrImagesToUpload = 0;
	void questUploadImages(string surveyid)
    {
		Debug.Log("Quest questUploadImages: " + surveyid);
		int questid = m_UploadAllIter;

		m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text>().text = "Upload images";

		string filename = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + 1 + ".jpg";
		if (File.Exists(filename))
		{
			m_ImagesUploaded = 0;
			m_NrImagesToUpload = PlayerPrefs.GetInt("Quest_" + questid + "_NrPhotos");
			if (m_NrImagesToUpload > 5) m_NrImagesToUpload = 5;
			
			for (int i = 0; i < m_NrImagesToUpload; i++)
			{
				int imgid = i + 1;
			
				string url = "https://server.org/survey/" + surveyid + "/image";

				StartCoroutine(questUploadImagesStart(url, filename, imgid));
			}
			if(m_NrImagesToUpload <= 0)
            {
				questSuccessfullyUploaded();
			}
		}
		else
		{
			Debug.Log("Image File does not exist");
			questSuccessfullyUploaded();
		}
	}

	void UploadImagesAgain(string url, string filename, int imgid)
    {
		StartCoroutine(questUploadImagesStart(url, filename, imgid));
	}

	public int m_FailureUploadingImageIter = 0;

	IEnumerator questUploadImagesStart(string url, string filename, int imgid)
    {
		Debug.Log("questUploadImagesStart url: " + url + " filename: " + filename + " imgid: " + imgid);

		
		//string replacedText = encodedText.Replace("+", "%2B");

		Debug.Log("Upload image url: " + url);

		WWWForm formrequest = new WWWForm();
		/*
		formrequest.AddBinaryData("imageFiles", bytes, "Image.jpeg", "image/jpeg");
		formrequedst.AddBinaryData("imageFiles", bytes, "Image.jpeg", "image/jpeg");
		formrequest.AddBinaryData("imageFiles", bytes, "Image.jpeg", "image/jpeg");*/
		int questid = m_UploadAllIter;

		filename = Application.persistentDataPath + "/" + "Quest_Img_" + questid + "_" + imgid + ".jpg";
		if (File.Exists(filename))
		{
			Debug.Log("Image filename: " + filename);
			byte[] bytes = File.ReadAllBytes(filename);
			formrequest.AddBinaryData("imageFile", bytes, "Image.jpeg", "image/jpeg");

			//-------------------
			string customData = "{";
			customData += "\"heading\":" + PlayerPrefs.GetFloat ("Quest_" + questid + "_" + imgid + "_Heading");
			customData += ",";
			customData += "\"tilt\":" + PlayerPrefs.GetFloat ("Quest_" + questid + "_" + imgid + "_Tilt");
			customData += ",";
			customData += "\"lat\":" + PlayerPrefs.GetFloat ("Quest_" + questid + "_" + imgid + "_Lat");
			customData += ",";
			customData += "\"lng\":" + PlayerPrefs.GetFloat ("Quest_" + questid + "_" + imgid + "_Lng");
			customData += ",";

			if (PlayerPrefs.GetInt("Quest_" + questid + "_" + imgid + "_WhichPicture") == 1)
			{
				customData += "\"type\":\"waterSourceDistanceImages\"";
			} else if (PlayerPrefs.GetInt("Quest_" + questid + "_" + imgid + "_WhichPicture") == 2)
			{
				customData += "\"type\":\"waterSourceCloseupImages\"";
			}else if (PlayerPrefs.GetInt("Quest_" + questid + "_" + imgid + "_WhichPicture") == 3)
			{
				customData += "\"type\":\"waterSourceSurroundImages\"";
			}
			else
			{
				customData += "\"type\":\"notspecified\"";
			}
			customData += ",";
			customData += "\"timestamp\":" + "\"" + PlayerPrefs.GetString ("Quest_" + questid + "_" + imgid + "_Timestamp") + "\"";
			customData += "}";
			Debug.Log("Custom data for image: " + customData);
			//customData = "{}";
			
			
			string customDataDecoded = UnityWebRequest.EscapeURL(customData);
			formrequest.AddField("Data", customDataDecoded);
			//-------------------
			Debug.Log("Image added");

			//UnityWebRequest www = UnityWebRequest.Post(url, formrequest);
			UnityWebRequest www = UnityWebRequest.Post(url, formrequest);
			www.SetRequestHeader("Authorization", "Bearer " + m_Token);
			www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
			www.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();
			Debug.Log("With download handler 4");

			yield return www.SendWebRequest();


			if (www.isNetworkError || www.isHttpError)
			{
				m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text>().text =
					"Failure uploading image (" + m_ImagesUploaded + "/" + m_NrImagesToUpload + ") " + url + " (" + m_FailureUploadingImageIter + ") " + www.error;

				m_FailureUploadingImageIter++;
				Debug.Log("Error in image upload: " + www.error);
				Debug.Log("Error text: " + www.error.ToString());
				Debug.Log("UploadImageResult: " + www.downloadHandler.text);
				UploadImagesAgain(url, filename, imgid);
			}
			else
			{
				m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text>().text =
					"Uploaded image " + imgid + "/" + m_NrImagesToUpload + ".";
				
				Debug.Log("UploadImageResult: " + www.downloadHandler.text);
				m_ImagesUploaded++;
				Debug.Log("Nr images uploaded: " + m_ImagesUploaded);
				if (m_ImagesUploaded >= m_NrImagesToUpload)
				{
					questSuccessfullyUploaded();
				}
			}
		}
		else
		{
			m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text>().text =
				"Uploaded image " + imgid + "/" + m_NrImagesToUpload + ".";

			m_ImagesUploaded++;
			Debug.Log("Nr images uploaded: " + m_ImagesUploaded);
			if (m_ImagesUploaded >= m_NrImagesToUpload)
			{
				questSuccessfullyUploaded();
			}
		}
    }

	public void questSuccessfullyUploaded()
	{
		string stralreadyuploaded = "Quest_" + m_UploadAllIter + "_Uploaded";
		PlayerPrefs.SetInt (stralreadyuploaded, 1);
		PlayerPrefs.Save ();

		Debug.Log ("=== Disable upload buttons nr buttons: " + m_UploadButtons.Count + " ===");
		for (int i = 0; i < m_UploadButtons.Count; i++) {
			string thequestid = (string)m_UploadButtonsQuestId [i];
			int ithequestid = int.Parse (thequestid);
			Debug.Log (i + " quest id: " + ithequestid);
			if (ithequestid == m_UploadAllIter) {
				Debug.Log ("Found entry -> disable upload button");
				GameObject obj = (GameObject)m_UploadQuestUploaded [i];
				obj.GetComponent<Text>().text = LocalizationSupport.GetString("QuestUploadedNotReviewed");
				obj.SetActive (true);
				GameObject obj2 = (GameObject)m_UploadButtons [i];
				obj2.SetActive (false);
				GameObject obj3 = (GameObject)m_EditButtons [i];
				obj3.SetActive (false);
			}
		}



		if (!m_bUploadAll) {
			m_UploadingBack.SetActive (false);
			m_UploadingText.SetActive (false);
			m_UploadingTextWait.SetActive (false);
			m_UploadingImage.SetActive (false);

			bool bOneQuestNotUploaded = false;
			for (int i = 0; i < m_NrQuestsDone && !bOneQuestNotUploaded; i++) {
				string stralreadyuploadedquest = "Quest_" + i + "_Uploaded";
				if (PlayerPrefs.HasKey (stralreadyuploadedquest) == false) {
					bOneQuestNotUploaded = true;
				}
			}
			if (bOneQuestNotUploaded) {
				m_UploadingAll.SetActive (true);
			} else {
				m_UploadingAll.SetActive (false);
			}


		/*	if (m_NrQuestsDone >= 5 && bOneQuestNotUploaded == false) {
				UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnMsgBoxAllUploadedClicked);
				string[] options = { "Ok"} ;
				messageBox.Show ("", LocalizationSupport.GetString("QuestUploadSuccessfulPrize"), ua, options);
			} else {*/
		
				UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnMsgBoxUploadedClicked);
				string[] options2 = { "Ok" };
				messageBox.Show ("", LocalizationSupport.GetString ("QuestUploadSuccessful"), ua, options2);
		//	}

			stardLoading ();
		} else {
			m_UploadAllIter++;
			if (m_UploadAllIter < m_NrQuestsDone) {
				uploadQuest (m_UploadAllIter);
			} else {
				m_UploadingBack.SetActive (false);
				m_UploadingText.SetActive (false);
				m_UploadingTextWait.SetActive (false);
				m_UploadingImage.SetActive (false);

				bool bOneQuestNotUploaded = false;
				for (int i = 0; i < m_NrQuestsDone && !bOneQuestNotUploaded; i++) {
					string stralreadyuploadedquest = "Quest_" + i + "_Uploaded";
					if (PlayerPrefs.HasKey (stralreadyuploadedquest) == false) {
						bOneQuestNotUploaded = true;
					}
				}
				if (bOneQuestNotUploaded) {
					m_UploadingAll.SetActive (true);
				} else {
					m_UploadingAll.SetActive (false);
				}

			/*if (m_NrQuestsDone >= 5) {
				UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnMsgBoxAllUploadedClicked);
				string[] options = { "Ok"} ;
				messageBox.Show ("", LocalizationSupport.GetString("QuestUploadSuccessfulPrize"), ua, options);
			} else {*/
				string[] options2 = { "Ok" };
				
				UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnMsgBoxUploadedClicked);
				messageBox.Show ("", LocalizationSupport.GetString ("QuestsUploadSuccessful"), ua, options2);
		//	}


				stardLoading ();
			}
		}
	}

	string m_EditImageFile;
	public void EditImage1()
	{
		m_EditImageFile = m_StrImg1;
		openEditImage ();
	}
	public void EditImage2()
	{
		m_EditImageFile = m_StrImg2;
		openEditImage ();
	}
	public void EditImage3()
	{
		m_EditImageFile = m_StrImg3;
		openEditImage ();
	}
	public void EditImage4()
	{
		m_EditImageFile = m_StrImg4;
		openEditImage ();
	}
	public void EditImage5()
	{
		m_EditImageFile = m_StrImg5;
		openEditImage ();
	}
	void openEditImage()
	{
		UnityEngine.UI.Image image = m_EditImage.GetComponent<UnityEngine.UI.Image>();
		if(File.Exists(m_EditImageFile)) {
			byte[] bytes = File.ReadAllBytes (m_EditImageFile);
			if(bytes != null) {
				Texture2D texture = new Texture2D(1, 1);
				texture.LoadImage (bytes);
				Sprite sprite = Sprite.Create (texture, new Rect(0,0,texture.width,texture.height), new Vector2(.5f,.5f));
				image.sprite = sprite;//m_Sprite;

				image.preserveAspect = true;
				Debug.Log ("image width: " + texture.width + " height: " + texture.height);
			}
		}


		Debug.Log ("openEditImage");
		m_EditImageBack.SetActive (true);
		m_EditImage.SetActive (true);
		m_EditImageClose.SetActive (true);
		m_EditImageClose2.SetActive (true);
		m_EditImageExpl.SetActive (true);
		m_EditImageExplBack.SetActive (true);

		m_bHideEditImageExplanation = false;
		m_bHiddenEditImageExplanation = false;
		float proc = 1.0f;
		byte alpha = (byte)(208 * proc);
		m_EditImageExplBack.GetComponent<UnityEngine.UI.Image> ().color = new Color32 (255, 255, 255, alpha);
		alpha = (byte)(255 * proc);
		m_EditImageExpl.GetComponent<UnityEngine.UI.Text> ().color = new Color32 (0, 0, 0, alpha);
	}

	public void CloseEditImage()
	{
		m_bBluring = false;
		m_EditImageBack.SetActive (false);
		m_EditImage.SetActive (false);
		m_EditImageClose.SetActive (false);
		m_EditImageClose2.SetActive (false);
		m_EditImageExpl.SetActive (false);
		m_EditImageExplBack.SetActive (false);
	}

	public void SaveEditImage() 
	{

		UnityEngine.UI.Image image = m_EditImage.GetComponent<UnityEngine.UI.Image>();
		Sprite sprite = image.sprite;
		Texture2D tex = sprite.texture;


		byte[] bytes = tex.EncodeToJPG ();
		//string name = "Quest_Img_" + m_NrQuestsDone + "_" + m_CurrentStep;


		if (Application.platform == RuntimePlatform.Android) { 
		//	File.WriteAllBytes( Application.persistentDataPath+"/"+name+".jpg",bytes );
			File.WriteAllBytes( m_EditImageFile,bytes );
		} else {
		//	File.WriteAllBytes( Application.persistentDataPath+"/"+name+".jpg",bytes );
			File.WriteAllBytes( m_EditImageFile,bytes );
		}

		openQuest (m_CurrentOpenQuest);
		CloseEditImage ();

	}


bool m_bBluring = false;
int m_BluringPosX = 0;
int m_BluringPosY = 0;

void blurImage(int curposx, int curposy)
{

	UnityEngine.UI.Image image = m_EditImage.GetComponent<UnityEngine.UI.Image>();
	Sprite sprite = image.sprite;
	Texture2D tex = sprite.texture;
	int width = tex.width;
	int height = tex.height;
	Debug.Log("texturewidth: " + width + " height: " + height);

	int border = 30;//40;
	int core = 3;//3;//6;


	int corex;
	int corey;

	float r = 0;
	float g = 0;
	float b = 0;
	int nrcores = 0;
	int _x;
	int _y;
	Color newcol;

	for (int x = -border; x < border; x++) {
		for (int y = -border; y < border; y++) {
			corex = x + curposx;
			corey = y + curposy;

			r = 0;
			g = 0;
			b = 0;
			nrcores = 0;

			for (_x = corex - core; _x <= corex + core; _x++) {
				for (_y = corey - core; _y <= corey + core; _y++) {
					nrcores++;

					Color col = tex.GetPixel (_x, _y);
					r += col.r;
					g += col.g;
					b += col.b;
				}
			}

			r /= nrcores;
			g /= nrcores;
			b /= nrcores;

			newcol = new Color (r, g, b);

			tex.SetPixel (corex, corey, newcol);
		}
	}
	tex.Apply();

}

public void OnImageReleased(BaseEventData data)
{
		m_bBluring = false;
}

	public void OnImageClicked(BaseEventData data)
	{
		m_bBluring = true;
		m_EditImageExplBack.SetActive (false);
		m_EditImageExpl.SetActive (false);




		PointerEventData pointerData = data as PointerEventData;
		Debug.Log ("OnImageClicked x: " + pointerData.position.x + " y: " + pointerData.position.y);

		UnityEngine.UI.Image image = m_EditImage.GetComponent<UnityEngine.UI.Image>();
		Sprite sprite = image.sprite;
		Texture2D tex = sprite.texture;
		int width = tex.width;
		int height = tex.height;
		Debug.Log("texturewidth: " + width + " height: " + height);


int curposx = (int)pointerData.position.x;
int curposy = (int)pointerData.position.y;

float procx = pointerData.position.x / (float)Screen.width;


	float ratio = (float)height / (float)width;
	float imgHeightScreen = (float)Screen.width * ratio;

	float dify = pointerData.position.y - (int)(Screen.height * 0.5f);
	float procy = dify / (float)imgHeightScreen;
	procy += 0.5f;
	if(procy > 1.0f) procy = 1.0f;
	if(procy < 0.0f) procy = 0.0f;

	curposx = (int)(procx * (float)width);
	curposy = (int)(procy * (float)height);

#if ASDFASDFASDFASF
	float screenwidth = Screen.width;
	float screenheight = Screen.height;
	Debug.Log ("Screenwidth: " + screenwidth + " height: " + screenheight);

	/*float procx = ((float)pointerData.position.y / (float)screenheight);
	float procy = 1.0f - ((float)pointerData.position.x / (float)screenwidth);
	Debug.Log ("procx: " + procx + " y: " + procy);*/
	float procx = ((float)pointerData.position.x / (float)screenheight);
	float procy = ((float)pointerData.position.y / (float)screenwidth);
	Debug.Log("Blur procx: " + procx + " y: " + procy);

	float ratio = screenwidth / screenheight;
	float heightshown = width * ratio;
	Debug.Log ("Height shown: " + heightshown);



	float heightdif = heightshown - height;
	heightdif *= 0.5f;

	int curposx = (int)(procx * width);
	int curposy = (int)(procy * height);
	#endif
	//int curposy = (int)(procy * heightshown);
	//curposy -= (int)heightdif;

	Debug.Log ("paint posx: " + curposx + " posy: " + curposy);

		m_bBluring = true;
		m_BluringPosX = curposx;
		m_BluringPosY = curposy;

		blurImage (curposx, curposy);


	/**/
		/*
		//---------------------
		// For test
		border = 6;
		for (int x = -border; x < border; x++) {
			for (int y = -border; y < border; y++) {
				corex = x + curposx;
				corey = y + curposy;


				newcol = new Color (1.0f, 0.0f, 0.0f);

				tex.SetPixel (corex, corey, newcol);
			}
		}
		tex.Apply();*/

	}


	void OnMsgBoxErrorClicked(string result)
	{
		GUIUtility.systemCopyBuffer = m_ParamError;
		string[] options2 = { "Ok" };
		messageBox.Show ("", "Param: " + m_ParamError, options2);
	}
	

	void OnMsgBoxAllUploadedClicked(string result) {
		if (PlayerPrefs.HasKey ("ProfileSaved") == false) {
			UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string> (OnMsgBoxProfileClicked);
			string[] options = { LocalizationSupport.GetString("QuestionProfileTimeNo"), LocalizationSupport.GetString("QuestionProfileTimeYes")};
			messageBox.Show ("", LocalizationSupport.GetString("QuestionProfileTime"), ua, options);
		}
	}

	private bool m_bStartSurvey = false;
	private int m_StartSurveyIter = 0;

	void OnMsgBoxUploadedClicked(string result)
	{
		int nrtries = PlayerPrefs.GetInt("SurveyFilledOutTries3");
		nrtries++;
		PlayerPrefs.SetInt("SurveyFilledOutTries3", nrtries);
		PlayerPrefs.Save();
		if ((PlayerPrefs.GetInt("SurveyFilledOut3") == 0 && nrtries < 4))
		{
			m_bStartSurvey = true;
			m_StartSurveyIter = 0;
		}
	}

	void OnMsgBoxProfileClicked(string result) {
		if (result.CompareTo( LocalizationSupport.GetString("QuestionProfileTimeYes")) == 0) {
			Application.targetFrameRate = 30;
			//	Application.LoadLevel ("DynamicQuestionsPark");
			Application.LoadLevel ("Profile");
		}
	}


	bool m_bLoadedToken = false;
	bool m_bLoadToken = false;
	int m_LoadTokenQuestId = -1;

	void loadToken()
	{
		if (!PlayerPrefs.HasKey("PlayerPassword") ||
			!PlayerPrefs.HasKey("PlayerName"))
		{
			m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text>().text = "Load token error no user given";
			return;
		}

	
		m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text>().text = "Load token";
		string url = "https://server.org/connect/token";

		StartCoroutine(loadingToken(url, PlayerPrefs.GetString("PlayerName"), PlayerPrefs.GetString("PlayerPassword")));
	}

	IEnumerator loadingToken(string url, string user, string password)
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
			www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
			www.certificateHandler = new AcceptAnyCertificate();

			//  www.SetRequestHeader("Authorization", "Bearer " + token);
			yield return www.SendWebRequest();

			string[] options2 = { "Ok" };

			if (www.isNetworkError || www.isHttpError)
			{
				Debug.Log("Error loading token: " + www.error);
				string data = www.downloadHandler.text;
				Debug.Log("Data: " + data);
				Debug.Log("Errorstr: " + www.ToString());
				
				m_UploadingText.GetComponentInChildren<UnityEngine.UI.Text>().text = "Error loading token: " + data;
				m_bErrorLoadingToken = true;
				m_ErrorLoadingTokenIter = 0;
				//loadToken();
			}
			else
			{
				string data = www.downloadHandler.text;

				Debug.Log("Loading token: " + data);
				JSONObject j = new JSONObject(data);
				m_ReadingWhichToken = -1;
				m_Token = "";
				accessToken(j);
				Debug.Log("Loaded token: " + m_Token);
				m_bLoadedToken = true;
				m_bLoadToken = false;
				//uploadQuest(m_LoadTokenQuestId);
				uploadQuestTokenLoaded(m_LoadTokenQuestId);
			}
		}
	}
	int m_ReadingWhichToken;
	string m_Token = "";

	void accessToken(JSONObject obj)
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
						m_ReadingWhichToken = 1;
					}
					else
					{
						m_ReadingWhichToken = -1;
					}
					accessToken(j);
				}
				break;
			case JSONObject.Type.ARRAY:
				//  Debug.Log ("Array");
				foreach (JSONObject j in obj.list)
				{
					accessToken(j);
				}
				break;
			case JSONObject.Type.STRING:
				if (m_ReadingWhichToken == 1)
				{
					m_Token = obj.str;
				}
				break;
			case JSONObject.Type.NUMBER:


				break;
			case JSONObject.Type.BOOL:
				break;
			case JSONObject.Type.NULL:
				//  Debug.Log("NULL");
				break;
		}
	}
}
