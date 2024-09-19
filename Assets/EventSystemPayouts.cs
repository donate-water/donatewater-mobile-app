using System;
using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EventSystemPayouts : MonoBehaviour
{
    public float m_WebScalerX = 1.5f;
    public float m_WebScalerY = 800.0f;
    
   // public GameObject m_BackImage;
    public GameObject m_Button;
    public GameObject m_Loading;
    public GameObject m_Content;
    public GameObject m_Rank;
    public GameObject m_LName;
    public GameObject m_Score;
    public GameObject m_RankS;
    public GameObject m_NameS;
    public GameObject m_ScoreS;
    public CanvasScaler m_Scaler;
    public GameObject m_BackgroundPortrait;
    public GameObject m_BackgroundLandscape;
    string m_CurPile;
    string m_CurValidationType;
    string m_CurFilter;

    ArrayList m_AddedTexts;


    IEnumerator changeFramerate()
    {
        yield return new WaitForSeconds(1);
        Application.targetFrameRate = 60;
    }


    public void ForceAutoRotate()
    {
        StartCoroutine(ForceAndFixAutoRotate());
    }

    IEnumerator ForceAndFixAutoRotate()
    {
        yield return new WaitForSeconds(0.01f);

        Screen.autorotateToPortraitUpsideDown = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToLandscapeLeft = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
        Screen.autorotateToPortrait = true;
        yield return new WaitForSeconds(0.5f);
    }



    public void ForcePortrait()
    {
        StartCoroutine(ForceAndFixPortrait());
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

    void UpdateBackgroundImage()
    {
        if (Screen.width > Screen.height)
        {
            m_BackgroundPortrait.SetActive(false);
            m_BackgroundLandscape.SetActive(true);
        }
        else
        {
            m_BackgroundPortrait.SetActive(true);
            m_BackgroundLandscape.SetActive(false);
        }
    }
    
    //------------------------------
    // Deep linking
  /*  void Awake()
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
    }*/
    //------------------------------

    void Start()
    {
        StartCoroutine(changeFramerate());
/*
#if UNITY_WEBGL
		ForceAutoRotate ();
#else
        ForcePortrait();
#endif*/

        if ((!LocalizationSupport.StringsLoaded))
            LocalizationSupport.LoadStrings();

        m_bLoadingLeaderboard = true;
        UpdateScaler();
        UpdateBackgroundImage();

        m_CurSelection = 0;
        /* m_CurFilter = "0";// "0";//"2";
         int sorting = PlayerPrefs.GetInt("CurrentlySorting");
         if (sorting == 1)
         {
             m_CurFilter = "2";
         }*/
        m_CurFilter = "0";
        m_AddedTexts = new ArrayList();
        m_Names = new ArrayList();
        m_Scores = new ArrayList();
        m_UserIds = new ArrayList();
        UpdateText();
        updateStates();

        m_bLoadingLeaderboard = false;
        loadLeaderboard();
       // loadBackgroundImage();
    }


    void UpdateScaler()
    {
        if (Screen.width > Screen.height)
        {
            m_Scaler.referenceResolution = new Vector2 (Screen.width * m_WebScalerX, m_WebScalerY);
            //m_Scaler.referenceResolution = new Vector2(Screen.width * 1.5f, 700);
        }
        else
        {
            m_Scaler.referenceResolution = new Vector2(800, 700);
        }
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape)) 
        //		Application.LoadLevel ("Piles");
        UpdateScaler();
        UpdateBackgroundImage();
    }

    int m_CurSelection = 0;
    void UpdateText()
    {
        m_Loading.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Loading");
    }

    int m_CurState = 0;
    public void updateStates()
    {
        m_Rank.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("PayoutDate");
        m_LName.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("PayoutTokens");
        m_Score.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("PayoutScore");
        m_Button.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnBack");//"CLOSE";
    }

    public void NextClicked()
    {
#if UNITY_WEBGL
        Application.LoadLevel("ProfileWeb");
#else
            Application.LoadLevel("Profile");
#endif
    }

    bool m_bLoadingLeaderboard = false;
    void loadLeaderboard()
    {
        if (m_bLoadingLeaderboard)
        {
            return;
        }

        m_bLoadingLeaderboard = true;

        m_CurPile = "-1";
        m_CurFilter = "0";
        m_CurValidationType = "Normal";

        Debug.Log("Load leaderboard pileid: " + m_CurPile + " filter: " + m_CurFilter+
            " validationtype: " + m_CurValidationType);
        
        for (int i = 0; i < m_AddedTexts.Count; i++)
        {
            GameObject go = (GameObject)m_AddedTexts[i];
            go.SetActive(false);
        }
        m_AddedTexts = new ArrayList();

         //   loadPileStatistics(m_CurPile, m_CurValidationType);
            
        m_Loading.SetActive(true);
        m_Loading.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Loading");

        //loadLeaderboardTable();
        loadEarnings();
    }
    
    void loadEarnings()
    {
        string url = "https://server.org/user/score";

        Debug.Log("Load PayPal: " + url);
        StartCoroutine(WaitForEarnings(url));
    }
    class AcceptAnyCertificate : CertificateHandler {
        protected override bool ValidateCertificate(byte[] certificateData) => true;
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
				
                m_Names = new ArrayList();
                m_Dates = new ArrayList();
                m_States = new ArrayList();
                m_Scores = new ArrayList();
                m_UserIds = new ArrayList();
                m_Ranks = new ArrayList();
                m_CurrentUser = new ArrayList();
                
                JSONObject j = new JSONObject(data);
                m_ReadingWhichEarning = -1;
                accessDataEarning(j);

                m_bEarningsLoaded = true;
                m_Loading.SetActive(false);

                
                createLeaderboard();
                m_Loading.SetActive(false);
                m_bLoadingLeaderboard = false;
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

                    if (key == "tokensPaid")
                    {
                        m_ReadingWhichEarning = 1;
                    }
                    else if (key == "creationTime")
                    {
                        m_ReadingWhichEarning = 2;
                    }
                    else if (key == "deductedScore")
                    {
                        m_ReadingWhichEarning = 3;
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
                if (m_ReadingWhichEarning == 2)
                {
                    System.DateTime dateTime = System.DateTime.Parse(obj.str);
                    m_Dates.Add("" +dateTime.Year + "-" + dateTime.Month + "-" + dateTime.Day);
                } 

                m_ReadingWhichEarning = 0;
                break;
            case JSONObject.Type.NUMBER:
                if (m_ReadingWhichEarning == 1)
                {
                    m_Scores.Add("" + obj.n + "");
                    //m_Earnings = obj.n;
                }
                else if (m_ReadingWhichEarning == 3)
                {
                    m_States.Add("" + obj.n);
                }

                m_ReadingWhichEarning = 0;
                break;
            case JSONObject.Type.BOOL:
                break;
            case JSONObject.Type.NULL:
                break;

        }
    }


    void loadLeaderboardTable()
    {
        string filter = "Total";
        if (m_CurFilter == "1")
        {
            filter = "Last7Days";
        }
        else if (m_CurFilter == "2")
        {
            filter = "Last30Days";
        }

        
        if (m_CurPile.CompareTo("-1") == 0)
        {
            string url = LocalizationSupport.GetString("DBUrl") + "leaderboard/ranks/user?maxresults=100&statsType=" +
                filter + "&validationType=" + m_CurValidationType;

            Debug.Log("loadPileStatistics: " + url);

            StartCoroutine(waitForLeaderboardTable(url));
        }
        else
        {
            string url = LocalizationSupport.GetString("DBUrl") + "leaderboard/ranks/user?maxresults=100&pileId=" + m_CurPile
                + "&statsType=" + filter + "&validationType=" + m_CurValidationType;

            Debug.Log("loadPileStatistics: " + url);
            StartCoroutine(waitForLeaderboardTable(url));
        }
    }


    bool m_bQuestsRead;
    bool m_bPhotosRead;
    bool m_bPercRead;
    string m_NrQuests;
    string m_NrPhotos;
    string m_PercDone;
    ArrayList m_Dates;
    ArrayList m_Names;
    ArrayList m_States;
    ArrayList m_Scores;
    ArrayList m_UserIds;
    ArrayList m_CurrentUser;
    ArrayList m_Ranks;

    IEnumerator waitForLeaderboardTable(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.certificateHandler = new AcceptAnyCertificate();
            string token = PlayerPrefs.GetString("PPPToken");
            Debug.Log("Token: " + token);
            www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
            www.SetRequestHeader("Authorization", "Bearer " + token);
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
                Debug.Log("WWW leaderboard table successfully loaded: " + data);

                string[] options = { "OK" };

                m_Names = new ArrayList();
                m_Dates = new ArrayList();
                m_States = new ArrayList();
                m_Scores = new ArrayList();
                m_UserIds = new ArrayList();
                m_Ranks = new ArrayList();
                m_CurrentUser = new ArrayList();

                JSONObject j = new JSONObject(data);
                m_ReadingWhich = -1;
                m_StartPosition = -1;
                accessData(j);

                m_Loading.SetActive(false);

                createLeaderboard();


                m_Loading.SetActive(false);
                m_bLoadingLeaderboard = false;
            }
        }
    }

    void refreshTokenLeaderboad()
    {
        if (PlayerPrefs.HasKey("PPPPassword") == false || PlayerPrefs.HasKey("PPPMail") == false)
        {
            return;
        }
        string user = PlayerPrefs.GetString("PPPMail");
        string password = PlayerPrefs.GetString("PPPPassword");
        string url = LocalizationSupport.GetString("DBUrl") + "connect/token";

        StartCoroutine(LoginLeaderboard(url, user, password));
    }

    IEnumerator LoginLeaderboard(string url, string user, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("client_id", "Platform_App");
        form.AddField("client_secret", "...");
        form.AddField("grant_type", "password"/*"password"*/);
        form.AddField("username", user);
        form.AddField("password", password);
        form.AddField("scope", "Platform");

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

                loadLeaderboardTable();
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
                    //Debug.Log("Access token read: " + obj.str);
                    PlayerPrefs.SetString("PPPToken", obj.str);
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


    int m_ReadingWhich;
    int m_StartPosition = -1;
    void accessData(JSONObject obj)
    {
        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];

                    if (key == "rank")
                    {
                        m_ReadingWhich = 1;
                    }
                    else if (key == "score")
                    {
                        m_ReadingWhich = 2;
                    }
                    else if (key == "userId")
                    {
                        m_ReadingWhich = 3;
                    }
                    else if (key == "userName")
                    {
                        m_ReadingWhich = 4;
                    }
                    else if (key == "currentUser")
                    {
                        m_ReadingWhich = 5;
                    }
                    else
                    {
                        m_ReadingWhich = 0;
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
                     //    Debug.Log("Added name: " + obj.str);

                     // m_TextDescription.GetComponentInChildren<UnityEngine.UI.Text>().text = obj.str + "\n";
                 }*/
                /* else if (m_ReadingWhich == 2)
                 {
                     m_Scores.Add(obj.str);
                     // m_TextDescription.GetComponentInChildren<UnityEngine.UI.Text>().text = obj.str + "\n";
                 }*/
                if (m_ReadingWhich == 3)
                {
                    m_UserIds.Add(obj.str + "");
                }
                else if (m_ReadingWhich == 4)
                {
                    m_Names.Add( obj.str + "");
                }
                break;
            case JSONObject.Type.NUMBER:
                if (m_ReadingWhich == 1)
                {
                    if (m_StartPosition < 0)
                    {
                        m_StartPosition = (int)obj.n;
                    }
                }
                else if (m_ReadingWhich == 2)
                {
                    //    Debug.Log("Score added: " + obj.n);
                    m_Scores.Add("" + obj.n);
                    //   m_Pins[m_CurrentPin].m_Weight = "" + obj.n;
                }
               /* else if (m_ReadingWhich == 3)
                {
                    //m_Names.Add("User " + obj.n + "");
                    //    Debug.Log("Score added: " + obj.n);
                    m_UserIds.Add("" + obj.n);
                    //   m_Pins[m_CurrentPin].m_Weight = "" + obj.n;
                }*/
                break;
            case JSONObject.Type.BOOL:
                if (m_ReadingWhich == 5)
                {
                    bool bCurrentUser = obj.b;
                    m_CurrentUser.Add(bCurrentUser);
                }
                break;
            case JSONObject.Type.NULL:
                break;

        }
    }

    public void createLeaderboard()
    {
        int nrentries = m_Dates.Count;//m_Names.Count;

        Debug.Log("CreateLeaderboard nrentries: " + nrentries);

        float sizeentry = 54.0f;// 53.0f;// 54.0f;// 52.0f;// 50.0f;// 53.0f;// 55.0f;// 50.0f;// 40.0f;
        RectTransform rectTransform2 = m_Content.GetComponent<RectTransform>();
        //rectTransform2.sizeDelta.
        //rt.sizeDelta = new Vector2 (100, 100);
        float scalex = rectTransform2.sizeDelta.x;
        float scaley = rectTransform2.sizeDelta.y;
        rectTransform2.sizeDelta = new Vector2(scalex, sizeentry * nrentries + 100.0f);

        string playerid = "";
        if (PlayerPrefs.HasKey("PlayerId"))
        {
            playerid = PlayerPrefs.GetString("PlayerId");
        }
        
#if UNITY_WEBGL
        sizeentry = 34;// 35;
#endif

        //Color col = new Color(60.0f / 255.0f, 197.0f / 255.0f, 255.0f / 255.0f);
        //Color col = new Color(21.0f / 255.0f, 144.0f / 255.0f, 179.0f / 255.0f);
        Color col = new Color(70.0f / 255.0f, 130.0f / 255.0f, 200.0f / 255.0f);


        for (int i = 0; i < nrentries; i++)
        {
          //  bool bCurrentUser = false;
            /* if(m_UserIds[i].ToString().CompareTo(playerid) == 0)
             {
                 bCurrentUser = true;
             }*/
          /*  if (m_Names[i].ToString().CompareTo(playerid) == 0)
            {
                bCurrentUser = true;
            }

            bCurrentUser = (bool)m_CurrentUser[i];*/

            GameObject copy;

           /* if(bCurrentUser)
                copy = (GameObject)GameObject.Instantiate(m_RankSUser);
            else*/
                copy = (GameObject)GameObject.Instantiate(m_RankS);

            RectTransform rectTransform;
            float curpos;
            float curposx;
            int currank;
            string text;

            copy.transform.SetParent(m_Content.transform, false);
            copy.SetActive(true);
            rectTransform = copy.GetComponent<RectTransform>();
            curpos = rectTransform.localPosition.y;
            curposx = rectTransform.localPosition.x;
            curpos -= i * sizeentry;
            rectTransform.localPosition = new Vector2(curposx, curpos);
            m_AddedTexts.Add(copy);
            currank = i + 1;
            text = "" + currank;

            //text = DateTime.Now.ToString("yyyy-MM-dd");

            text = (string)m_Dates[i];
            
            copy.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
           /* if (bCurrentUser)
            {
                copy.GetComponentInChildren<UnityEngine.UI.Text>().color = col;
            }*/


           /* if (bCurrentUser)
                copy = (GameObject)GameObject.Instantiate(m_NameSUser);
            else*/
                copy = (GameObject)GameObject.Instantiate(m_NameS);
            copy.transform.SetParent(m_Content.transform, false);
            copy.SetActive(true);
            rectTransform = copy.GetComponent<RectTransform>();
            curpos = rectTransform.localPosition.y;
            curposx = rectTransform.localPosition.x;
            curpos -= i * sizeentry;
            rectTransform.localPosition = new Vector2(curposx, curpos);
            m_AddedTexts.Add(copy);
            //text = "Tobias";
            text = (string)m_Dates[i];
           /* text = System.Text.RegularExpressions.Regex.Unescape(text);
            if (text.Length > 20)
            {
                text = text.Substring(0, 20);
            }*/

            text = (string)m_Scores[i];//"12.03 €";
            copy.GetComponentInChildren<UnityEngine.UI.Text>().text = text;
            /*if (bCurrentUser)
            {
                copy.GetComponentInChildren<UnityEngine.UI.Text>().color = col;
            }*/


            /*if (bCurrentUser)
                copy = (GameObject)GameObject.Instantiate(m_ScoreSUser);
            else*/
                copy = (GameObject)GameObject.Instantiate(m_ScoreS);

            copy.transform.SetParent(m_Content.transform, false);
            copy.SetActive(true);
            rectTransform = copy.GetComponent<RectTransform>();
            curpos = rectTransform.localPosition.y;
            curposx = rectTransform.localPosition.x;
            curpos -= i * sizeentry;
            rectTransform.localPosition = new Vector2(curposx, curpos);
            m_AddedTexts.Add(copy);
            string textscore = (string)m_States[i];
/*
            if (textscore != "" && textscore != null)
            {
                float money = float.Parse(textscore);
                //money /= 100;
                string strtotal = money.ToString();//(( ("F2");
                textscore = strtotal;
            }


            textscore = "Processed";*/
            copy.GetComponentInChildren<UnityEngine.UI.Text>().text = textscore;// + "€";//"2323";
           /* if (bCurrentUser)
            {
                copy.GetComponentInChildren<UnityEngine.UI.Text>().color = col;
            }*/
        }
    }

    /*void loadBackgroundImage()
    {
        int sorting = PlayerPrefs.GetInt("CurrentlySorting");
        if (sorting == 0)
        {
            return;
        }


        int pileimg = PlayerPrefs.GetInt("CurPileImage");
        if (pileimg == 0)
        {
            return;
        }

        string picid = "curpic";
        string name = Application.persistentDataPath + "/" + picid + ".png";
        if (File.Exists(name))
        {
            byte[] bytes = File.ReadAllBytes(name);
            if (bytes != null)
            {
                Texture2D texture = new Texture2D(1, 1);
                texture.LoadImage(bytes);

                Sprite sprite = Sprite.Create(texture, new Rect(0, 50, texture.width, 512 + 50), new Vector2(0, 0));


                UnityEngine.UI.Image image = m_BackImage.GetComponent<UnityEngine.UI.Image>();
                image.sprite = sprite;
                m_BackImage.GetComponent<UnityEngine.UI.Image>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            }
        }
        else
        {
            Debug.Log("Could not load image " + name);
        }
    }*/


    //---------------------------------------
    // Pile statistics
    //---------------------------------------

    void loadPileStatistics(string id, string validationtype)
    {
        Debug.Log(">> loadPileDescription: " + id);

        string filter = "Total";
        if(m_CurFilter == "1")
        {
            filter = "Last7Days";
        } else if(m_CurFilter == "2")
        {
            filter = "Last30Days";
        }

        if (id.CompareTo("-1") == 0)
        {
            string url = LocalizationSupport.GetString("DBUrl") + "leaderboard/summary?statsType=" +
                filter + "&validationType=" + validationtype;

            Debug.Log("loadPileStatistics: " + url);

            StartCoroutine(waitForStatistics(url));
        }
        else
        {
            string url = LocalizationSupport.GetString("DBUrl") + "leaderboard/summary?pileId=" + id
                + "&statsType=" + filter + "&validationType=" + validationtype;

            Debug.Log("loadPileStatistics: " + url);
            StartCoroutine(waitForStatistics(url));
        }
    }

    IEnumerator waitForStatistics(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            www.certificateHandler = new AcceptAnyCertificate();
            string token = PlayerPrefs.GetString("PPPToken");
            Debug.Log("Token: " + token);
            www.SetRequestHeader("X-Requested-With", "XMLHttpRequest");
            www.SetRequestHeader("Authorization", "Bearer " + token);
            yield return www.SendWebRequest();

            string[] options2 = { "Ok" };

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("Error loading stats info info: " + www.error);
                string data = www.downloadHandler.text;
                Debug.Log("Data: " + data);
                Debug.Log("Errorstr: " + www.ToString());
                refreshTokenStats();
            }
            else
            {
                string data = www.downloadHandler.text;
                Debug.Log("WWW stats info successfully loaded: " + data);
                JSONObject j = new JSONObject(data);
                m_ReadingWhichStats = -1;

                accessStatsData(j);
            }
        }
    }

    int m_ReadingWhichStats = 0;
    int m_TotalImages = 0;
    int m_SortedImages = 0;
    int m_NrUsers = 0;
    void accessStatsData(JSONObject obj)
    {
        switch (obj.type)
        {
            case JSONObject.Type.OBJECT:
                for (int i = 0; i < obj.list.Count; i++)
                {
                    string key = (string)obj.keys[i];
                    JSONObject j = (JSONObject)obj.list[i];

                    if (key == "totalImages")
                    {
                        m_ReadingWhichStats = 1;
                    }
                    else if (key == "sortedImages")
                    {
                        m_ReadingWhichStats = 2;
                    }
                    else if (key == "userCount")
                    {
                        m_ReadingWhichStats = 3;
                    }
                    else
                    {
                        m_ReadingWhichStats = 0;
                    }
                    accessStatsData(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                //	Debug.Log ("Array");
                foreach (JSONObject j in obj.list)
                {
                    accessStatsData(j);
                }
                break;
            case JSONObject.Type.STRING:
                break;
            case JSONObject.Type.NUMBER:
                if (m_ReadingWhichStats == 1)
                {
                    m_TotalImages = (int)obj.n;
                }
                else if (m_ReadingWhichStats == 2)
                {
                    m_SortedImages = (int)obj.n;
                }
                else if (m_ReadingWhichStats == 3)
                {
                    m_NrUsers = (int)obj.n;
                }
                break;
            case JSONObject.Type.BOOL:
                break;
            case JSONObject.Type.NULL:
                break;

        }
    }

    void refreshTokenStats()
    {
        if (PlayerPrefs.HasKey("PPPPassword") == false || PlayerPrefs.HasKey("PPPMail") == false)
        {
            return;
        }
        string user = PlayerPrefs.GetString("PPPMail");
        string password = PlayerPrefs.GetString("PPPPassword");
        string url = LocalizationSupport.GetString("DBUrl") + "connect/token";

        StartCoroutine(LoginStats(url, user, password));
    }

    IEnumerator LoginStats(string url, string user, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("client_id", "Platform_App");
        form.AddField("client_secret", "...");
        form.AddField("grant_type", "password"/*"password"*/);
        form.AddField("username", user);
        form.AddField("password", password);
        form.AddField("scope", "Platform");

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
                Debug.Log("Refresh token result: " + data);

                m_bReadingAccessToken = false;
                JSONObject j = new JSONObject(data);
                accessAccessTokenStats(j);

                loadPileStatistics(m_CurPile, m_CurValidationType);
            }
        }
    }

    bool m_bReadingAccessToken = false;
    void accessAccessTokenStats(JSONObject obj)
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
                    accessAccessTokenStats(j);
                }
                break;
            case JSONObject.Type.ARRAY:
                //	Debug.Log ("Array");
                foreach (JSONObject j in obj.list)
                {
                    accessAccessTokenStats(j);
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

}
