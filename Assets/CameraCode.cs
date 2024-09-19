using UnityEngine;
using UnityEngine.UI;
//using NatCamU;


using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


using System.Collections;
using System.Text;
//using Signalphire;

public class CameraCode : MonoBehaviour {//UnitygramBase {

	[Header("Barcodes")]
//	public BarcodeFormat detectionFormat = BarcodeFormat.ALL;
	public bool continuousDetection = true;

	[Header("UI")]
	public Button switchCamButton;
	public Button flashButton;
	public Button barcodeButton;
	public Image checkIco;
	public Image flashIco;
	public Text flashText;
	public Text barcodeText;
	public Button m_ButtonBack;
	public Button m_ButtonGuidelines;
	public Button m_ButtonDisableCompass;
	public GameObject m_ImageHorizon;
	public GameObject m_TextHorizon;
	public GameObject m_TextMakePicture;
	public GameObject m_ImageMakePicture;
	public GameObject m_ButtonMakePhoto2;
	public GameObject m_ButtonMakePhoto3;
	public Text m_TextTask;
	public Image m_ImageTask;
	public GameObject m_TextTaskGO;
	public GameObject m_ImageTaskGO;
	public GameObject m_TextTaskBigGO;
	public GameObject m_ImageTaskBigGO;
	public GameObject m_ButtonMakePhoto;
	public GameObject m_ButtonMakePhotoDisabled;
	public GameObject m_Arrow;

	bool m_bInGuide;
	public GameObject m_GuideBack;
	public GameObject m_GuideBack2;
	public GameObject m_GuidePhoto;
	public GameObject m_TextGuideTitle;
	public GameObject m_TextGuideTextAT;
	public GameObject m_TextGuideTextEN;
	public GameObject m_ButtonGuideClose;


	bool m_bShowError;
	float m_ErrorTimer;
	public GameObject m_TextError;
	public GameObject m_BackError;

	public GameObject m_TextDistance;
	public GameObject m_BackDistance;


	public Material m_MatRed;
	public Material m_MatGreen;

	public RawImage m_PreviewImageMade;
	public GameObject m_ButtonImageRepeat;
	public GameObject m_ButtonImageOk;
	public GameObject m_PreviewImageMadeGO;
	public GameObject m_PreviewFlash;
	public RawImage m_PreviewFotoTest;

	bool m_bCompassEnabled;

	bool m_bPointInReach;
	int m_CurrentStep;

	public GameObject m_ButtonYes;
	public GameObject m_ButtonNo;

	public Text m_DebugText;


	public GameObject m_PositionMarker;

	bool m_bInYesNo;

	float m_PinLocationLat;
	float m_PinLocationLng;

	int m_NrQuestsDone;

	bool m_bShowPreview;
	float m_ShowPreviewTimer;

	bool m_bArrowShown;
	int m_ArrowColor;

	//public WebCamTexture mCamera2 = null;

	bool m_bShowGivePermission;

	void Awake() {
		/*NativeEssentials.OnAndroidPermissionCallback += (requestResult) =>
		{*/
			/*StringBuilder sb = new StringBuilder();
			sb.Append("Results for premission request:");
			bool gotoAppSettings = false;
			foreach(var p in requestResult)
			{
				sb.Append("\nPermission: " + p.Permission);
				sb.Append(string.Format(", Result: {0} ({1})", p.Status.ToString(), p.RawStatus));
				sb.Append(", CanNotAskAgain: " + p.NeverAskAgain);

				

				// NeverAskAgain will be true if the user selected the checkbox to disable request for said permission.
				// To enable this permission, we will need to direct them to the app settings.
				if(p.NeverAskAgain)
				{
					gotoAppSettings = true;
				}
			}
			LogToUI(string.Format("[Android Permissions] {0}", sb.ToString()));

			// Ideally this should be done after providing some explanation as to why the permission crucial. For demo
			// purposes, we're just firing the call to show the app setting from there
			if(gotoAppSettings)
			{
				NativeEssentials.Instance.GotoAppSettings();
			}*/
		//};
	}


	IEnumerator changeFramerate() {
		yield return new WaitForSeconds(1);
		Application.targetFrameRate = 30;
	}

	public void ForceLandscapeLeft()
	{
		StartCoroutine(ForceAndFixLandscape());
	}

	IEnumerator ForceAndFixLandscape()
	{
		yield return new WaitForSeconds (0.05f);

		//	Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		Screen.autorotateToLandscapeLeft = true;
		yield return new WaitForSeconds (0.5f);
	}

	// Use this for initialization
	//public override void Start () {
	//int m_PermissionIter;
	bool m_bDebug;
	public void Start () {
		StartCoroutine(changeFramerate());
		ForceLandscapeLeft ();

		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();

		//Start base
	//	base.Start();
	//	m_PermissionIter = 0;

		//TODO: TURN THIS TO FALSE FOR RELEASE!!!
		m_bDebug = false;//false;//false;//false;//true;//false;//false;//false;//false;//true;

		m_bShowGivePermission = false;

		m_PreviewImageMade.enabled = false;
		m_ButtonImageRepeat.SetActive (false);
		m_ButtonImageOk.SetActive (false);
		m_PreviewImageMadeGO.SetActive (false);
		m_PreviewFlash.SetActive (false);

		#if UNITY_ANDROID
		//mCamera2 = null;

/*
		NativeEssentials.Instance.Initialize();
		// Comment next line in again
		PermissionsHelper.StatusResponse sr = NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);
		//PermissionsHelper.StatusResponse sr = PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_PROVIDE_RATIONALE;//NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);
		//sr =NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);
        */

/*		if (m_bDebug || (Application.HasUserAuthorization (UserAuthorization.WebCam) && sr == PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_GRANTED)) {
		/*mCamera2 = new WebCamTexture ();
		if (mCamera2 != null) {
		m_PreviewFotoTest.texture = mCamera2;
		mCamera2.Play ();
		}*/
		//} else {
		//	mCamera2 = null;
/*
		NativeEssentials.Instance.RequestAndroidPermissions(new string[] {
		PermissionsHelper.Permissions.CAMERA
		});*/
		//}

		/*
		mCamera2 = new WebCamTexture ();
		m_PreviewFotoTest.texture = mCamera2;
		mCamera2.Play ();*/

		#else
		/*	mCamera2 = new WebCamTexture ();
		m_PreviewFotoTest.texture = mCamera2;
		mCamera2.Play ();*/
		#endif

		m_bArrowShown = false;
		m_ArrowColor = 0;

		m_bAccSet = false;

		m_bInGuide = false;
		m_bShowPreview = false;

		m_bPointVisible = false;

		m_CurrentStep = 1;
		m_bPointInReach = true;

		m_bInYesNo = false;

		m_Arrow.SetActive (false);

		if(PlayerPrefs.HasKey("CurQuestReached")) {
			int inreach = PlayerPrefs.GetInt("CurQuestReached");
			if(inreach == 0) {
				m_bPointInReach = false;
			}
		}
		/*
		if (PlayerPrefs.HasKey ("CameraStartLastStep")) {
			int startlast = PlayerPrefs.GetInt ("CameraStartLastStep");
			if (startlast == 1) {
				if (m_bPointInReach) {
					m_CurrentStep = 5;
				} else {
					m_CurrentStep = 6;
				}
			}*/

			int pointvisible = PlayerPrefs.GetInt ("CurQuestVisible");
			if (pointvisible == 1) {
				m_bPointVisible = true;
			}
	//	}

		if (PlayerPrefs.HasKey ("NrQuestsDone")) {
			m_NrQuestsDone = PlayerPrefs.GetInt ("NrQuestsDone");
		} else {
			m_NrQuestsDone = 0;
		}

		int iPointInReach = m_bPointInReach ? 1 : 0;
		PlayerPrefs.SetInt ("Quest_" + m_NrQuestsDone + "_PointReached", iPointInReach);

		string curquestid = PlayerPrefs.GetString ("CurQuestId");
		PlayerPrefs.SetString ("Quest_" + m_NrQuestsDone + "_Id", curquestid);

		if (curquestid.Equals ("-1")) {
			Debug.Log ("Training point started!");

			PlayerPrefs.SetInt ("Quest_" + m_NrQuestsDone + "_TrainingPoint", 1);
			float strlat = PlayerPrefs.GetFloat ("CurQuestLat");
			float strlng = PlayerPrefs.GetFloat ("CurQuestLng");
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_TrainingPoint_Lat", strlat);
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_TrainingPoint_Lng", strlng);

			Debug.Log ("Saved training point lat: " + strlat);
			Debug.Log ("Saved training point lng: " + strlng);

		} else {
			PlayerPrefs.SetInt ("Quest_" + m_NrQuestsDone + "_TrainingPoint", 0);
		}

		string startcameratime = System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss");
		PlayerPrefs.SetString ("CurQuestStartCameraTime", startcameratime);



		PlayerPrefs.Save ();

		m_PinLocationLat = PlayerPrefs.GetFloat("CurQuestLat");
		m_PinLocationLng = PlayerPrefs.GetFloat("CurQuestLng");

		Debug.Log("Point in reach: " + m_bPointInReach);
		Debug.Log("m_PinLocationLat: " + m_PinLocationLat + " CurQuestLng: " + m_PinLocationLng);

		m_bCompassEnabled = true;



		//Create a barcode detection request
		/*BarcodeRequest request = new BarcodeRequest(OnDetectBarcode, detectionFormat, !continuousDetection); //Negate continuousDetection because when true, we don't want to automatically unsubscribe our callback
		//Request barcode detection
		NatCam.RequestBarcode(request);
		//Set the flash icon
		SetFlashIcon();
*/


		m_bShowError = false;
		m_TextError.SetActive (false);
		m_BackError.SetActive (false);

		m_TextDistance.SetActive (false);
		m_BackDistance.SetActive (false);

		m_GuideBack.SetActive (false);
		m_GuidePhoto.SetActive (false);
		m_GuideBack2.SetActive (false);
		m_TextGuideTitle.SetActive (false);
		m_TextGuideTextAT.SetActive (false);
		m_TextGuideTextEN.SetActive (false);
		m_ButtonGuideClose.SetActive (false);

		m_TextMakePicture.SetActive (false);
		m_ImageMakePicture.SetActive (false);
		m_ButtonMakePhoto.SetActive (false);
		m_ButtonMakePhoto2.SetActive (false);
		m_ButtonMakePhoto3.SetActive (false);
		m_ButtonMakePhotoDisabled.SetActive (false);

		m_ButtonYes.SetActive (false);
		m_ButtonNo.SetActive (false);

		m_PositionMarker.SetActive (false);

		if (Application.systemLanguage == SystemLanguage.German  && false) {
			m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Zurück";
			m_ButtonGuidelines.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Anleitung";
			m_ButtonDisableCompass.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Kompass ausschalten";
			m_TextHorizon.GetComponent<UnityEngine.UI.Text> ().text = "Horizont";
			//m_TextMakePicture.GetComponent<UnityEngine.UI.Text> ().text = "Mach ein Foto!";
			m_TextTask.text = "Mache Foto richtung norden";


			m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Halte die Kamera im Landschaftsmodus in die angezeigte Richtung um Fotos für die Quest zu machen.";


			m_TextGuideTitle.GetComponent<UnityEngine.UI.Text> ().text = "Anleitung";
			//m_TextGuideText.GetComponent<UnityEngine.UI.Text> ().text = "- Nimm das Landschaftsfoto so auf, dass zwei Drittel des Bildes den Boden zeigen und das obere Drittel den Himmel.\n\n- Falls das wegen einer Sichtbehinderung nicht möglich ist, halte das Handy einfach horizontal.\n\n- Versuche zu vermeiden, dass Gegenstände auf dem Foto sichtbar sind die eine Person oder Eigentum identifizieren könnten.";



			m_ButtonGuideClose.GetComponentInChildren<UnityEngine.UI.Text> ().text = "OK";


			m_ButtonImageRepeat.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Wiederholen";
			m_ButtonImageOk.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Weiter";


			m_ButtonYes.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Ja";
			m_ButtonNo.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Nein";


			m_ButtonMakePhoto2.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Mache Foto!";
			m_ButtonMakePhoto3.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Mache Foto!";
			//m_ButtonMakePhoto.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Mache Foto!";
		} else {
			m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Back");//"Back";
			m_ButtonGuidelines.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Guide");//"Guide";
			m_ButtonDisableCompass.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("DisableCompass");//"Disable Compass";
			m_TextHorizon.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Horizon");//"Horizon";
			//m_TextMakePicture.GetComponent<UnityEngine.UI.Text> ().text = "Take Photo!";
			m_TextTask.text = "";

			m_TextError.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("TakePhotoExplanation");//"Hold your device in landscape mode and move it into the displayed direction to take a picture.";

			m_TextGuideTitle.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Guide");//"Guide";
			//m_TextGuideText.GetComponent<UnityEngine.UI.Text> ().text = "- Take the photo of the landscape so that two-thirds of the picture is ground and one-third is sky\n\n- When taking photos when there is an obstruction (wall, building, hedgerow, etc.) just keep your device horizontal and disregard the rule above\n\n- As much as possible, avoid identifications of persons or property in the photos (e.g. name or car identification plates, people's faces, etc.)";
			m_ButtonGuideClose.GetComponentInChildren<UnityEngine.UI.Text> ().text = "OK";


			m_ButtonYes.GetComponentInChildren<UnityEngine.UI.Text> ().text =  LocalizationSupport.GetString("Yes");//"Yes";
			m_ButtonNo.GetComponentInChildren<UnityEngine.UI.Text> ().text =  LocalizationSupport.GetString("No");//"No";


			m_ButtonImageRepeat.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Retake");//"Retake";
			m_ButtonImageOk.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("BtnContinue");//"Continue";
			m_ButtonMakePhoto2.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("TakePhoto");//"Take Photo!";
			m_ButtonMakePhoto3.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("TakePhoto");//"Take Photo!";
		//	m_ButtonMakePhoto.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Take Photo!";
		}

		updateStep ();



		Input.compass.enabled = true;
		//Input.location.Start ();

		if (Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
			ShowEnableCameraMsg ();
		}/*

		if (mCamera2 == null || mCamera2.isPlaying == false || Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
			ShowEnableCameraMsg ();
		} else if (m_bPointInReach) {
			CapturePhotoDisabled ();
		}*/

	}

	void updateStep() {

		if (m_CurrentStep == 1) {
			if (Application.systemLanguage == SystemLanguage.German && false) {
				m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Zurück";
			} else {
				m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Back");//"Back";
			}
		} else {
			if (Application.systemLanguage == SystemLanguage.German && false) {
				m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Zurück";
			} else {
				m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Undo");//"Undo";
			}
		}


		m_PositionMarker.SetActive (false);

		if (m_bPointInReach) {
			m_TextTaskGO.SetActive (true);
			m_ImageTaskGO.SetActive (true);
			m_TextTaskBigGO.SetActive (false);
			m_ImageTaskBigGO.SetActive (false);

			if (m_CurrentStep == 1) {
				if (Application.systemLanguage == SystemLanguage.German && false) {
					m_TextTask.text = "Fotografiere in den Norden. (1/5)";
				} else {
					m_TextTask.text = LocalizationSupport.GetString("TakePhotoStep1");//"Take Picture towards North. (1/5)";
				}
			} else if (m_CurrentStep == 2) {
				if (Application.systemLanguage == SystemLanguage.German && false) {
					m_TextTask.text = "Fotografiere in den Osten. (2/5)";
				} else {
					m_TextTask.text = LocalizationSupport.GetString("TakePhotoStep2");//"Take Picture towards East. (2/5)";
				}
			} else if (m_CurrentStep == 3) {
				if (Application.systemLanguage == SystemLanguage.German && false) {
					m_TextTask.text = "Fotografiere in den Süden. (3/5)";
				} else {
					m_TextTask.text = LocalizationSupport.GetString("TakePhotoStep3");//"Take Picture towards South. (3/5)";
				}
			} else if (m_CurrentStep == 4) {

				m_TextHorizon.SetActive (true);
				m_ImageHorizon.SetActive (true);
				if (Application.systemLanguage == SystemLanguage.German && false) {
					m_TextTask.text = "Fotografiere in den Westen. (4/5)";
				} else {
					m_TextTask.text = LocalizationSupport.GetString("TakePhotoStep4");//"Take Picture towards West. (4/5)";
				}
			} else if (m_CurrentStep == 5) {

				m_TextTaskGO.SetActive (false);
				m_ImageTaskGO.SetActive (false);
				m_TextTaskBigGO.SetActive (true);
				m_ImageTaskBigGO.SetActive (true);

				//	m_PositionMarker.SetActive (true);
				//m_PositionMarker.SetActive (false);
				m_PositionMarker.SetActive (true);

				m_TextHorizon.SetActive (false);
				m_ImageHorizon.SetActive (false);
				/*if (Application.systemLanguage == SystemLanguage.German) {
					m_TextTaskBigGO.GetComponent<UnityEngine.UI.Text> ().text = "Gehe 5 Schritte zurück und fotografiere den Punkt an dem du gerade gestanden bist. (5/5)";
				} else {
					m_TextTaskBigGO.GetComponent<UnityEngine.UI.Text> ().text = "Walk 5 steps backwards and take a Picture of the point where you just stood (The blue pin should ma. (5/5)";
				}*/

				if (Application.systemLanguage == SystemLanguage.German && false ) {
				//	m_TextTaskBigGO.GetComponent<UnityEngine.UI.Text> ().text = "Gehe 5 Meter weg. Mache dann ein Foto von der Stelle wo du davor gestanden bist. (5/5)";
					m_TextTaskBigGO.GetComponent<UnityEngine.UI.Text> ().text = "Gehe 5 Meter weg. Platziere dann den blauen Pin an die Stelle wo du davor gestanden bist und mache ein Foto. (5/5)";
				} else {
			//		m_TextTaskBigGO.GetComponent<UnityEngine.UI.Text> ().text = "Walk 5 meters away. Then take a picture of the ground where you were standing before. (5/5)";
					m_TextTaskBigGO.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("TakePhotoStep5");//"Walk 5 meters away. Then place the blue pin on the position where you were standing before and take a Picture. (5/5)";

				}
			} 
		} else {

			m_TextTaskGO.SetActive (true);
			m_ImageTaskGO.SetActive (true);
			m_TextTaskBigGO.SetActive (false);
			m_ImageTaskBigGO.SetActive (false);


			if (m_CurrentStep == 1) {
				if (Application.systemLanguage == SystemLanguage.German && false) {
					m_TextTask.text = "Ist der Zielpunkt aus deiner Entfernung sichtbar? (1/6)";
				} else {
					m_TextTask.text = LocalizationSupport.GetString("TakePhotoNotInReachStep1");//"Can you see the point? (1/6)";
				}
				m_bInYesNo = true;
				m_ButtonYes.SetActive (true);
				m_ButtonNo.SetActive (true);

				m_TextDistance.SetActive (false);
				m_BackDistance.SetActive (false);

			} else if (m_CurrentStep == 2) {

				m_ButtonYes.SetActive (false);
				m_ButtonNo.SetActive (false);
				m_bInYesNo = false;

				if (Application.systemLanguage == SystemLanguage.German && false) {
					m_TextTask.text = "Fotografiere in die Richtung des Punktes. (2/6)";
				} else {
					m_TextTask.text = LocalizationSupport.GetString("TakePhotoNotInReachStep2");//"Take Picture towards the Target Point. (2/6)";
				}

				m_TextDistance.SetActive (false);
				m_BackDistance.SetActive (false);
			}  else if (m_CurrentStep == 3) {

				m_TextDistance.SetActive (false);
				m_BackDistance.SetActive (false);


				m_ButtonYes.SetActive (false);
				m_ButtonNo.SetActive (false);
				m_bInYesNo = false;
				if (Application.systemLanguage == SystemLanguage.German && false) {
					m_TextTask.text = "Fotografiere in den Norden. (3/6)";
				} else {
					m_TextTask.text = LocalizationSupport.GetString("TakePhotoNotInReachStep3");//"Take Picture towards North. (3/6)";
				}
			} else if (m_CurrentStep == 4) {
				if (Application.systemLanguage == SystemLanguage.German && false) {
					m_TextTask.text = "Fotografiere in den Osten. (4/6)";
				} else {
					m_TextTask.text = LocalizationSupport.GetString("TakePhotoNotInReachStep4");//"Take Picture towards East. (4/6)";
				}

				m_TextDistance.SetActive (false);
				m_BackDistance.SetActive (false);
			} else if (m_CurrentStep == 5) {
				if (Application.systemLanguage == SystemLanguage.German && false) {
					m_TextTask.text = "Fotografiere in den Süden. (5/6)";
				} else {
					m_TextTask.text = LocalizationSupport.GetString("TakePhotoNotInReachStep5");//"Take Picture towards South. (5/6)";
				}
			} else if (m_CurrentStep == 6) {
				if (Application.systemLanguage == SystemLanguage.German && false) {
					m_TextTask.text = "Fotografiere in den Westen. (6/6)";
				} else {
					m_TextTask.text = LocalizationSupport.GetString("TakePhotoNotInReachStep6");//"Take Picture towards West. (6/6)";
				}
			}
		}


		m_ButtonMakePhoto.SetActive (false);
		m_ButtonMakePhoto2.SetActive (false);
		m_ButtonMakePhoto3.SetActive (false);
		m_ButtonMakePhotoDisabled.SetActive (false);
	}

	float m_AccX;
	float m_AccY;
	float m_AccZ;
	float m_Heading;
	bool m_bAccSet;

	// Image m_Arrow = null;
//	float m_Angle = 0.0f;
	public void Update ()
	{
	/*	#if UNITY_ANDROID
		m_DebugText.text = "android";
		#else 
		m_DebugText.text = "ios";
		#endif
		//m_DebugText.text = "asdf";
*/
		/*PermissionsHelper.StatusResponse sr = PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_PROVIDE_RATIONALE;//NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);
		sr =NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);

		if (sr == PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_GRANTED) {
			m_DebugText.text = m_PermissionIter + " permission granted";
		} else {
			m_DebugText.text = m_PermissionIter + " permission not granted";
		}
		m_PermissionIter++;*/

		#if UNITY_ANDROID
		/*if (mCamera2 == null) {
			PermissionsHelper.StatusResponse sr = NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);

			if (Application.HasUserAuthorization (UserAuthorization.WebCam) && sr == PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_GRANTED) {
				mCamera2 = new WebCamTexture ();
				if (mCamera2 != null) {
					m_PreviewFotoTest.texture = mCamera2;
					mCamera2.Play ();

					CapturePhotoDisabled ();
				}
			}
		}*/
		#endif


		if (Input.GetKeyDown (KeyCode.Escape)) {
			OnBackClicked ();
		}

		if(m_bShowPreview) {
			m_Arrow.SetActive (false);
			m_ShowPreviewTimer += Time.deltaTime;

			float procflash = (m_ShowPreviewTimer - 0.3f) / 0.6f;
			if (procflash > 1.0f)
				procflash = 1.0f;
			if (procflash < 0.0f)
				procflash = 0.0f;
			
			procflash = 1.0f - procflash;
			byte alpha = (byte)(255 * procflash);
			m_PreviewFlash.GetComponent<UnityEngine.UI.Image>().color = new Color32(255,255,255,alpha);


			/*if (m_ShowPreviewTimer > 2.5f) {
				m_PreviewImageMadeGO.SetActive (false);
				m_PreviewFlash.SetActive (false);
				m_bShowPreview = false;
			}
			*/

			/*if (m_ShowPreviewTimer > 1.5f) {//2.5f) {
				m_ButtonImageRepeat.SetActive (false);
				m_ButtonImageOk.SetActive (false);
				m_PreviewImageMadeGO.SetActive (false);
				m_PreviewFlash.SetActive (false);
				m_bShowPreview = false;
			}*/

			return;
		}

		if (m_bInGuide) {
			m_Arrow.SetActive (false);
			return;
		}

		if (m_bArrowShown && m_ArrowColor == 2 && !m_bShowError) {
			if (Application.systemLanguage == SystemLanguage.German && false ) {
				m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Halte die Kamera im Landschaftsmodus in die angezeigte Richtung um Fotos für die Quest zu machen.";
			} else {
			m_TextError.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("TakePhotoExplanation");//"Hold your device in landscape mode and move it into the displayed direction to make a picture.";
			}

			m_bShowError = true;
			m_ErrorTimer = 0.0f;
			m_TextError.SetActive (true);
			m_BackError.SetActive (true);
		}

		if (m_bShowError) {
			m_ErrorTimer += 40.0f;

			if (m_bArrowShown && m_ArrowColor == 2) {
				m_ErrorTimer = 2000.0f;
			}


		if (/*mCamera2 == null || mCamera2.isPlaying == false ||*/ Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
			m_ErrorTimer = 2000.0f;
		}/*
		if (mCamera2 == null || mCamera2.isPlaying == false || Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
			m_ErrorTimer = 2000.0f;
		}*/

			if (m_ErrorTimer > 6500.0f) {
				m_BackError.SetActive (false);
				m_TextError.SetActive (false);
				m_bShowError = false;
			} else if (m_ErrorTimer > 6500.0f) {
				float proc = (m_ErrorTimer - 6500.0f) / 100.0f;
				if (proc > 1.0f)
					proc = 1.0f;
				proc = 1.0f - proc;

				byte alpha = (byte)(220 * proc);
				m_BackError.GetComponent<UnityEngine.UI.Image>().color = new Color32(255,255,255,alpha);
				alpha = (byte)(255 * proc);
				m_TextError.GetComponent<UnityEngine.UI.Text>().color = new Color32(0,0,0,alpha);

				m_BackError.SetActive (true);
				m_TextError.SetActive (true);
			} else {
				float proc = m_ErrorTimer / 500.0f;
				if (proc > 1.0f)
					proc = 1.0f;

				byte alpha = (byte)(220 * proc);
				m_BackError.GetComponent<UnityEngine.UI.Image>().color = new Color32(255,255,255,alpha);
				alpha = (byte)(255 * proc);
				m_TextError.GetComponent<UnityEngine.UI.Text>().color = new Color32(0,0,0,alpha);

				m_BackError.SetActive (true);
				m_TextError.SetActive (true);
			}
		}


		bool bShowArrow = true;
		if (m_bPointInReach) {
			if (m_CurrentStep == 5) {
				bShowArrow = false;
			}
		} /*else {
			if (m_CurrentStep == 6) {
				bShowArrow = false;
			}
		}*/

		/*if (mCamera2 == null) {
			ShowEnableCameraMsg ();
			return;
		}

		if (mCamera2.isPlaying == false || Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
			ShowEnableCameraMsg ();
			return;
		}*/

	if (/*mCamera2.isPlaying == false ||*/ Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
		ShowEnableCameraMsg ();
		return;
	}

		if (m_bShowGivePermission) {
			m_bShowGivePermission = false;
			CapturePhotoDisabled ();
		}

		if (m_bCompassEnabled && bShowArrow) {
			m_Arrow.SetActive (true);
			m_bArrowShown = true;

			if (!m_bAccSet) {
				m_AccX = Input.acceleration.x;
				m_AccY = Input.acceleration.y;
				m_AccZ = Input.acceleration.z;
				m_Heading = Input.compass.trueHeading;
				m_bAccSet = true;
		} else {
				float howmuchchange = Time.deltaTime * 7.0f;
				if (howmuchchange > 1.0f) {
					howmuchchange = 1.0f;
				}

			m_AccX = (Input.acceleration.x - m_AccX) * howmuchchange + m_AccX;
			m_AccY = (Input.acceleration.y - m_AccY) * howmuchchange + m_AccY;
			m_AccZ = (Input.acceleration.z - m_AccZ) * howmuchchange + m_AccZ;

				float difanglechange = (Input.compass.trueHeading - m_Heading);
			//	Debug.Log ("curheading: " + Input.compass.trueHeading);
			if (difanglechange > 180) {
				difanglechange = difanglechange - 360.0f;
			} else if(difanglechange < -180) {
				difanglechange += 360.0f;
				}

			m_Heading += difanglechange * howmuchchange;
			if(m_Heading < 0.0f) {
				m_Heading = 360 + m_Heading;
			} else if(m_Heading > 360) {
				m_Heading -= 360.0f;
			}

		//	m_Heading = (Input.compass.trueHeading - m_Heading) * 0.5f + m_Heading;
			}


		/*	var xrot = Mathf.Atan2 (Input.acceleration.z, Input.acceleration.y);
			var yzmag = Mathf.Sqrt (Mathf.Pow (Input.acceleration.y, 2) + Mathf.Pow (Input.acceleration.z, 2));
			var zrot = Mathf.Atan2 (Input.acceleration.x, yzmag);*/

		var xrot = Mathf.Atan2 (m_AccZ, m_AccY);
		var yzmag = Mathf.Sqrt (Mathf.Pow (m_AccY, 2) + Mathf.Pow (m_AccZ, 2));
		var zrot = Mathf.Atan2 (m_AccX, yzmag);

			var xangle = xrot * (180 / Mathf.PI) + 90;
			var zangle = -zrot * (180 / Mathf.PI);


			float curheading = m_Heading;//Input.compass.trueHeading;
			if (m_bPointInReach) {
				if (m_CurrentStep == 1) {
				} else if (m_CurrentStep == 2) {
					curheading -= 90;
					if (curheading > 360) {
						curheading -= 360;
					} else if (curheading < 0) {
						curheading += 360;
					}
				} else if (m_CurrentStep == 3) {
					curheading -= 180;
					if (curheading > 360) {
						curheading -= 360;
					} else if (curheading < 0) {
						curheading += 360;
					}
				} else if (m_CurrentStep == 4) {
					curheading -= 270;
					if (curheading > 360) {
						curheading -= 360;
					} else if (curheading < 0) {
						curheading += 360;
					}
				}
			} else {
				if (m_CurrentStep >= 1 && m_CurrentStep <= 2) {
					float curposlat = Input.location.lastData.latitude;// 30.0f;//48.210033f;
					float curposlng = Input.location.lastData.longitude;//12.0f;//16.363449f;

					//Debug.Log ("curposlat: " + curposlat + " lng: " + curposlng);

					float bearing = calcBearing ( curposlat, curposlng, m_PinLocationLat, m_PinLocationLng);
					curheading -= bearing;
					if (curheading < 0.0f) {
						curheading += 360;
					} else if (curheading > 360.0f) {
						curheading -= 360.0f;
					}

					float stepDistance = OnlineMapsUtils.DistanceBetweenPoints(new Vector2(m_PinLocationLng,m_PinLocationLat ), new Vector2(curposlng,curposlat )).magnitude;
					stepDistance *= 1000.0f;
					int meters = (int)stepDistance;

					if (Application.systemLanguage == SystemLanguage.German && false) {
						m_TextDistance.GetComponent<UnityEngine.UI.Text> ().text = "Distanz: " + meters + " m";
					} else {
					m_TextDistance.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Distance") + " " + meters + " m";
					}

					if (m_CurrentStep == 1) {
						m_TextDistance.SetActive (true);
						m_BackDistance.SetActive (true);
					} else {
						m_TextDistance.SetActive (false);
						m_BackDistance.SetActive (false);
					}

				} else if (m_CurrentStep == 3) {
					m_TextDistance.SetActive (false);
					m_BackDistance.SetActive (false);
				} else if (m_CurrentStep == 4) {
					m_TextDistance.SetActive (false);
					m_BackDistance.SetActive (false);
					curheading -= 90;
					if (curheading > 360) {
						curheading -= 360;
					} else if (curheading < 0) {
						curheading += 360;
					}
				} else if (m_CurrentStep == 5) {
					curheading -= 180;
					if (curheading > 360) {
						curheading -= 360;
					} else if (curheading < 0) {
						curheading += 360;
					}
				} else if (m_CurrentStep == 6) {
					curheading -= 270;
					if (curheading > 360) {
						curheading -= 360;
					} else if (curheading < 0) {
						curheading += 360;
					}
				}
			}

			m_Arrow.transform.eulerAngles = new Vector3 (xangle, 0, zangle - curheading);

			float tilted = m_AccZ;//Input.acceleration.z;
			float zacc = m_AccZ;// Input.acceleration.z;
		float yacc = m_AccY;
			if (tilted > 1.0f) {
				tilted = 1.0f;
			} else if (tilted < -1.0f) {
				tilted = -1.0f;
			}

			tilted *= 90.0f;
			m_Arrow.transform.eulerAngles = new Vector3 (90 + tilted, 0, curheading);


			float difangle = /*zangle -*/ curheading;
			if (difangle < 0.0f) {
				difangle *= -1.0f;
			}
			//float rot = zangle - curheading;
		m_DebugText.text = "heading: " + Input.compass.trueHeading + " difangle: " + difangle + " zangle: " + zangle + "accx: " + Input.acceleration.x + " accy: " + Input.acceleration.y + " accz: " + Input.acceleration.z + " acc: " + Input.location.lastData.horizontalAccuracy;//Input.compass.headingAccuracy;


			if (m_bInYesNo) {
				m_ButtonMakePhoto.SetActive (false);
				m_ButtonMakePhoto2.SetActive (false);
				m_ButtonMakePhoto3.SetActive (false);
				m_ButtonMakePhotoDisabled.SetActive (false);

				m_TextMakePicture.SetActive (false);
				m_ImageMakePicture.SetActive (false);


				/*Color32 col2 = m_MatGreen.color;
				col2.a = 150;
				m_MatGreen.color = col2;*/

				m_Arrow.GetComponent<Renderer>().material = m_MatGreen;
				m_ArrowColor = 1;
			} else {


				if ((difangle < 20.0f || difangle > (360.0f - 20.0f)) && zacc > -0.4f && zacc < 0.4f && yacc < -0.7f) {//Mathf.PI * 0.35f) {
					m_ButtonMakePhoto.SetActive (false);
					m_ButtonMakePhotoDisabled.SetActive (false);

					m_ButtonMakePhoto2.SetActive (true);
					m_ButtonMakePhoto3.SetActive (false);

					if (m_bShowError) {
						if (m_ErrorTimer < 6500) {
							m_ErrorTimer = 6500;
						}
					}

					/*m_bShowError = false;
					m_TextError.SetActive (false);
					m_BackError.SetActive (false);
*/


				//	m_TextMakePicture.SetActive (true);
			//		m_ImageMakePicture.SetActive (true);
					m_TextMakePicture.SetActive (false);
					m_ImageMakePicture.SetActive (false);

					/*Color32 col2 = m_MatGreen.color;
					col2.a = 150;
					m_MatGreen.color = col2;
*/
					m_Arrow.GetComponent<Renderer>().material = m_MatGreen;
					m_ArrowColor = 1;
				} else {
					m_ButtonMakePhoto.SetActive (false);
					m_ButtonMakePhoto2.SetActive (false);
					m_ButtonMakePhoto3.SetActive (false);
					//m_ButtonMakePhotoDisabled.SetActive (true);
					m_ButtonMakePhotoDisabled.SetActive (false);


					m_TextMakePicture.SetActive (false);
					m_ImageMakePicture.SetActive (false);

					/*Color32 col = m_MatRed.GetColor("_Color");
					col.a = 150;
					m_MatRed.SetColor("_Color", col);*/
					/*Color32 col2 = m_MatRed.color;
					col2.a = 150;
					m_MatRed.color = col2;
				*/
					m_Arrow.GetComponent<Renderer>().material = m_MatRed;
					m_ArrowColor = 2;
				}
			}
		} else {
			m_Arrow.SetActive (false);
			m_bArrowShown = false;
			m_ButtonMakePhoto2.SetActive (false);
			m_ButtonMakePhoto3.SetActive (false);

			if (m_bInYesNo) {
				m_ButtonMakePhoto.SetActive (false);
				m_ButtonMakePhotoDisabled.SetActive (false);
			} else {
				if (m_bCompassEnabled) {
					m_ButtonMakePhoto3.SetActive (true);
					m_ButtonMakePhoto.SetActive (false);
				} else {
					m_ButtonMakePhoto.SetActive (true);
				}

				m_ButtonMakePhotoDisabled.SetActive (false);
			}
		}


	/*	if (m_Arrow == null) {
			GameObject[] respawns = null;
			//if (respawns == null) {
			respawns = GameObject.FindGameObjectsWithTag ("Arrow");
			Debug.Log ("On Camera find");
			//}

			foreach (GameObject respawn in respawns) {
				Debug.Log ("On Camera deactivate");
			//	respawn.active = false;
				m_Arrow = respawn.GetComponent<Image>();//(Image)respawn;
			}
		}

		if (m_Arrow != null) {

			Debug.Log ("On Camera update angle: " + m_Angle);
			m_Angle += 0.1f;
			//m_Arrow.re
	//		m_Arrow.
			//m_Arrow.rectTransform.rotation = Quaternion.Euler(0,0,m_Angle);
			//			m_Arrow.rotation=Quaternion.Euler(0,0,m_Angle);
			m_Arrow.rectTransform.rotation = Quaternion.Euler(0,0,Input.compass.trueHeading);

		}*/
	}

	#region --NatCam and UI Callbacks--

	public void OnBackClicked() {
		m_ButtonImageRepeat.SetActive (false);
		m_ButtonImageOk.SetActive (false);
		m_PreviewImageMadeGO.SetActive (false);
		m_PreviewFlash.SetActive (false);
		m_bShowPreview = false;


		m_CurrentStep--;
		if (m_CurrentStep == 0) {
			Application.LoadLevel ("DynamicQuestionsFRA");/*
			if(mCamera2 != null) 
				mCamera2.Stop ();
			if (m_bPointInReach) {
				Application.LoadLevel ("DemoMap");
			} else {
				Application.LoadLevel ("NotInReach");
			}*/
		} else {
			updateStep ();
		}
	}
	public void OnCompassDisabledClicked() {
		/*if (mCamera2 == null) {
			return;
		}*/
		m_bCompassEnabled = !m_bCompassEnabled;


		if (Application.systemLanguage == SystemLanguage.German && false) {
			if (m_bCompassEnabled) {
				m_ButtonDisableCompass.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Kompass ausschalten";
			} else {
				m_ButtonDisableCompass.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Kompass einschalten";
			}

		} else {
			if (m_bCompassEnabled) {
			m_ButtonDisableCompass.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("DisableCompass");//"Disable Compass";
			} else {
			m_ButtonDisableCompass.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("EnableCompass");//"Enable Compass";
			}
		}

		if (!m_bCompassEnabled) {
			if (Application.systemLanguage == SystemLanguage.German && false) {
				m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Kompass ausgeschalten. Bitte kalibriere den Kompass wenn er nicht richtig funktionieren sollte.";
			} else {
			m_TextError.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("CompassDisabled");//"Compass turned off. Please calibrate your compass if it is not working properly.";
			}

			m_bShowError = true;
			m_ErrorTimer = 0.0f;
			m_TextError.SetActive (true);
			m_BackError.SetActive (true);
		} else {
			m_BackError.SetActive (false);
			m_TextError.SetActive (false);
			m_bShowError = false;
		}


		m_TextMakePicture.SetActive (false);
		m_ImageMakePicture.SetActive (false);
	}

	public void CapturePhotoDisabled () {
		/*if (mCamera2 == null) {
			return;
		}*/

		if (Application.systemLanguage == SystemLanguage.German && false) {
			m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Halte die Kamera im Landschaftsmodus in die angezeigte Richtung um Fotos für die Quest zu machen.";
		} else {
		m_TextError.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("TakePhotoExplanation");//"Hold your device in landscape mode and move it into the displayed direction to make a picture.";
		}


		m_bShowError = true;
		m_ErrorTimer = 0.0f;
		m_TextError.SetActive (true);
		m_BackError.SetActive (true);
	}

public void ShowEnableCameraMsg() {
		if (Application.systemLanguage == SystemLanguage.German && false) {
		m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Gib FotoQuest Go bitte die Erlaubnis die Kamera zu benutzen.";
	} else {
		m_TextError.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("CameraPermission");//"Please give FotoQuest Go permission to use the camera.";
	}

		if (!m_bShowError) {
			m_bShowError = true;
			m_ErrorTimer = 0.0f;
		}
		m_TextError.SetActive (true);
		m_BackError.SetActive (true);

		m_bShowGivePermission = true;
	}

	public void CapturePhoto () {
		if (m_bShowPreview) {
			return;
		}

		/*
		//Divert control if we are checking the captured photo
		if (checkIco.gameObject.activeInHierarchy) {
			OnCheckedPhoto();
			//Terminate this control chain
			return;
		}
		*/

		//Capture photo with our callback
	/*	if (NatCam.HasPermissions == false) {
			if (Application.systemLanguage == SystemLanguage.German) {
				m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Geben Sie bitte FotoQuest Go die Erlaubnis die Kamera zu benutzen.";
			} else {
				m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Please give FotoQuest Go permissions to use the camera.";
			}

			m_bShowError = true;
			m_ErrorTimer = 0.0f;
			m_TextError.SetActive (true);
			m_BackError.SetActive (true);

			return;
		}
		NatCam.CapturePhoto(OnCapturedPhoto);/**/

		/*if (mCamera2 == null) {
			return;
		}
		if (mCamera2.isPlaying == false || Application.HasUserAuthorization(UserAuthorization.WebCam) == false) {*/

		if (Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
			if (Application.systemLanguage == SystemLanguage.German) {
				m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Geben Sie bitte der App die Erlaubnis die Kamera zu benutzen.";
			} else {
				m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Please give the app permissions to use the camera.";
			}

			m_bShowError = true;
			m_ErrorTimer = 0.0f;
			m_TextError.SetActive (true);
			m_BackError.SetActive (true);
			return;
		}

		int result = 0;/* NatCam.CapturePhoto(OnCapturedPhoto);/*
		Texture2D snap = new Texture2D(mCamera2.width, mCamera2.height, TextureFormat.ARGB32, false);

		//Texture2D snap = new Texture2D(mCamera2.width, mCamera2.height);
		snap.SetPixels(mCamera2.GetPixels());
		snap.Apply();

		OnCapturedPhoto (snap);

		snap = null;*/
	}

	bool m_bPointVisible = true;

	public void OnYesSelected() {
		PlayerPrefs.SetInt ("Quest_" + m_NrQuestsDone + "_PointVisible", 1);
		PlayerPrefs.SetInt("CurQuestVisible", 1);
		PlayerPrefs.Save ();

		m_TextDistance.SetActive (false);
		m_BackDistance.SetActive (false);

		m_bPointVisible = true;

		m_CurrentStep++;
		updateStep ();
	}
	public void OnNoSelected() {
		PlayerPrefs.SetInt ("Quest_" + m_NrQuestsDone + "_PointVisible", 0);
		PlayerPrefs.SetInt("CurQuestVisible", 0);
		PlayerPrefs.Save ();

		m_TextDistance.SetActive (false);
		m_BackDistance.SetActive (false);


		m_bPointVisible = false;

		m_CurrentStep++;
		updateStep ();
	}

	private Texture2D ScaleTexture(Texture2D source,int targetWidth,int targetHeight) {
		Texture2D result=new Texture2D(targetWidth,targetHeight,source.format,true);
		Color[] rpixels=result.GetPixels(0);
		float incX=((float)1/source.width)*((float)source.width/targetWidth);
		float incY=((float)1/source.height)*((float)source.height/targetHeight);
		for(int px=0; px<rpixels.Length; px++) {
			rpixels[px] = source.GetPixelBilinear(incX*((float)px%targetWidth),
				incY*((float)Mathf.Floor(px/targetWidth)));
		}
		result.SetPixels(rpixels,0);
		result.Apply();
		return result;
	}

	void OnCapturedPhoto (Texture2D photo) {
		if (m_bCompassEnabled) {
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Heading", Input.compass.trueHeading);
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccX", Input.acceleration.x);
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccY", Input.acceleration.y);
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccZ", Input.acceleration.z);
		} else {
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Heading", -1.0f);
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccX", -1.0f);
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccY", -1.0f);
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccZ", -1.0f);
		}
		int compassenabled = m_bCompassEnabled ? 1 : 0;
		PlayerPrefs.SetInt ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_CompassEnabled", compassenabled);


		float tilted = Input.acceleration.z;
		if (tilted > 1.0f) {
			tilted = 1.0f;
		} else if (tilted < -1.0f) {
			tilted = -1.0f;
		}
		tilted *= 90.0f;
		if (m_bCompassEnabled) {
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Tilt", tilted);
		} else {
			PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Tilt", -1.0f);
		}

		float lat = Input.location.lastData.latitude;
		float lng = Input.location.lastData.longitude;

		PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Lat", lat);
		PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Lng", lng);

		//PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Accuracy", Input.compass.headingAccuracy);
		PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Accuracy", Input.location.lastData.horizontalAccuracy);



		string theTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sszz");
		string theTime2 = theTime;//theTime.Replace ("+", "%2B");
		//theTime2 += "00";
		Debug.Log ("CurrentTimestamp: " + theTime2);
		PlayerPrefs.SetString ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Timestamp", theTime2);



		PlayerPrefs.Save ();

		int w = photo.width;
		int h = photo.height;

		int newwidth = 128;
		int newheight = 128;

		if (w > h) {
			float newscale = 1024.0f / w;
			newwidth = (int)((float)w * newscale);
			newheight = (int)((float)h * newscale);
		} else {
			float newscale = 1024.0f / h;
			newwidth = (int)((float)w * newscale);
			newheight = (int)((float)h * newscale);
		}

		Texture2D scaledTex = ScaleTexture (photo, newwidth, newheight);
//		photo.Resize(newwidth, newheight);

		Debug.Log ("captureimage width: " + w + " height: " + h + " newwidth: " + newwidth + " newheight: " + newheight);
		//photo.Resize(newwidth, newheight);

		byte[] bytes = scaledTex.EncodeToJPG ();
//			photo.EncodeToJPG();

		string name = "Quest_Img_" + m_NrQuestsDone + "_" + m_CurrentStep;
		/*if (Application.platform == RuntimePlatform.Android) { 
			File.WriteAllBytes( Application.persistentDataPath+"/"+name+".jpg",bytes );
		} else {
			//	File.WriteAllBytes( Application.dataPath+"/Resources/save_screen/"+name+".jpg",bytes );
			File.WriteAllBytes( Application.persistentDataPath+"/"+name+".jpg",bytes );
		}*/
		if (Application.platform == RuntimePlatform.Android) { 
			File.WriteAllBytes( Application.persistentDataPath+"/"+name+".jpg",bytes );
		} else {
			//	File.WriteAllBytes( Application.dataPath+"/Resources/save_screen/"+name+".jpg",bytes );
			File.WriteAllBytes( Application.persistentDataPath+"/"+name+".jpg",bytes );
		}







		m_bShowPreview = true;
		m_ShowPreviewTimer = 0.0f;
		m_PreviewImageMade.texture = photo;
		m_PreviewImageMade.enabled = true;
		m_PreviewImageMadeGO.SetActive (true);
		m_PreviewFlash.SetActive (true);

		m_ButtonImageRepeat.SetActive (true);
		m_ButtonImageOk.SetActive (true);


		photo = null;
		//Set the rawImage
	//	RawImage.texture = photo;
		//Enable the check icon
		/*checkIco.gameObject.SetActive(true);
		//Disable the switch camera button
		switchCamButton.gameObject.SetActive(false);
		//Disable the flash button
		flashButton.gameObject.SetActive(false);*/
	}

/*	void OnDetectBarcode (Barcode code) {
		//Check if it is a hyperlink
		bool hyperlink = code.data.StartsWith("http") || code.data.StartsWith("www");
		//Set the button's interactable state
		barcodeButton.interactable = hyperlink;
		//Add a callback to open the link if it is a hyperlink
		barcodeButton.onClick.AddListener(() => Application.OpenURL(code.data));
		//Set the barcode's text
		barcodeText.text = string.Format(hyperlink ? "<i>{0}</i>" : "{0}", code.data);
		//Disable the barcode button after a while
		Invoke("DisableBarcodeButton", 4f);
	}*/
	#endregion


	#region --UI Ops--

	public void SwitchCamera () {
		//Switch camera
//		base.SwitchCamera();
		//Set the flash icon
	//	SetFlashIcon();
	}

	public void ToggleFlashMode () {
		//Set the active camera's flash mode
		//NatCam.ActiveCamera.FlashMode = NatCam.ActiveCamera.IsFlashSupported ? NatCam.ActiveCamera.FlashMode == FlashMode.Auto ? FlashMode.On : NatCam.ActiveCamera.FlashMode == FlashMode.On ? FlashMode.Off : FlashMode.Auto : NatCam.ActiveCamera.FlashMode;
		//Set the flash icon
		//SetFlashIcon();
	}

	void OnCheckedPhoto () {
		//Disable the check icon
		//checkIco.gameObject.SetActive(false);
		//Set the rawImage to the camera preview
	//	RawImage.texture = NatCam.Preview;
		//Enable the switch camera button
	//	switchCamButton.gameObject.SetActive(true);
		//Enable the flash button
	//	flashButton.gameObject.SetActive(true);
	}

	void SetFlashIcon () {
		//Null checking
		/*if (NatCam.ActiveCamera == null) return;
		//Set the icon
		flashIco.color = !NatCam.ActiveCamera.IsFlashSupported || NatCam.ActiveCamera.FlashMode == FlashMode.Off ? (Color)new Color32(200, 200, 200, 255) : Color.white;
		//Set the auto text for flash
		flashText.text = NatCam.ActiveCamera.IsFlashSupported && NatCam.ActiveCamera.FlashMode == FlashMode.Auto ? "A" : "";
*/	}
	#endregion


	#region --Misc--

	void DisableBarcodeButton () {
		//Set interactable false
		/*barcodeButton.interactable = false;
		//Clear the button's callbacks
		barcodeButton.onClick.RemoveAllListeners();
		//Empty the text
		barcodeText.text = "";*/
	}
	#endregion

	public void OnOpenGuide() {
		m_bInGuide = true;
		m_GuideBack.SetActive (true);
		m_GuidePhoto.SetActive (true);
		m_GuideBack2.SetActive (true);
		m_TextGuideTitle.SetActive (false);
		if (Application.systemLanguage == SystemLanguage.German) {
			m_TextGuideTextAT.SetActive (true);
			m_TextGuideTextEN.SetActive (false);
		} else {
			m_TextGuideTextEN.SetActive (true);
			m_TextGuideTextAT.SetActive (false);
		}
		m_ButtonGuideClose.SetActive (true);

	}
	public void OnCloseGuide() {
		m_bInGuide = false;
		m_GuideBack.SetActive (false);
		m_GuidePhoto.SetActive (false);
		m_GuideBack2.SetActive (false);
		m_TextGuideTitle.SetActive (false);
		m_TextGuideTextEN.SetActive (false);
		m_TextGuideTextAT.SetActive (false);
		m_ButtonGuideClose.SetActive (false);

	}

	public void OnContinueClicked() {
		m_CurrentStep++;
		updateStep ();
	/*
		if (mCamera2 == null) {
			return;
		}*/

		if (m_bPointInReach) {
			if (m_CurrentStep >= 6) {
				//Application.LoadLevel ("QuestFinished");
				PlayerPrefs.SetInt ("Questions_FromQuestFinished", 0);

				string endcameratime = System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss");
				PlayerPrefs.SetString ("CurQuestEndCameraTime", endcameratime);

				PlayerPrefs.Save ();

				//mCamera2.Stop ();

				//int campaign = PlayerPrefs.GetInt ("CampaignSelected");
				Application.LoadLevel ("QuestFinished");
			/*
				Application.LoadLevel ("DynamicQuestions");*//*
				if (campaign == 2) {
					Application.LoadLevel ("CampaignFlash");
				} else if (campaign == 3) {
					Application.LoadLevel ("CampaignHotspots");
				} else if (campaign == 4) {
					Application.LoadLevel ("CampaignLULC");
				} else {
					Application.LoadLevel ("DynamicQuestions");
				}*/
			}
		} else {
			if (m_CurrentStep >= 7) {

				//if (m_bPointVisible) {
					PlayerPrefs.SetInt ("Questions_FromQuestFinished", 0);
					string endcameratime = System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss");
					PlayerPrefs.SetString ("CurQuestEndCameraTime", endcameratime);

					PlayerPrefs.Save ();
					//mCamera2.Stop ();

			int campaign = PlayerPrefs.GetInt ("CampaignSelected");
			Application.LoadLevel ("QuestFinished");
			//Application.LoadLevel ("DynamicQuestions");
			/*
			if (campaign == 2) {
				Application.LoadLevel ("CampaignFlash");
			} else if (campaign == 3) {
				Application.LoadLevel ("CampaignHotspots");
			} else if (campaign == 4) {
				Application.LoadLevel ("CampaignLULC");
			} else {
				Application.LoadLevel ("DynamicQuestions");
			}
*/

					//Application.LoadLevel ("DynamicQuestions");
				/*} else {
					PlayerPrefs.SetInt ("Quest_" + m_NrQuestsDone + "_LandCoverId", 0);
					PlayerPrefs.SetInt ("Quest_" + m_NrQuestsDone + "_LandUseId", 0);

					//-----------------------------
					// Save timers
					string strtime = PlayerPrefs.GetString("CurQuestSelectedTime");
					PlayerPrefs.SetString ("Quest_" + m_NrQuestsDone + "_" + "SelectedTime", strtime);

					strtime = PlayerPrefs.GetString("CurQuestStartQuestTime");
					PlayerPrefs.SetString ("Quest_" + m_NrQuestsDone + "_" + "StartQuestTime", strtime);

					strtime = PlayerPrefs.GetString("CurQuestStartCameraTime");
					PlayerPrefs.SetString ("Quest_" + m_NrQuestsDone + "_" + "StartCameraTime", strtime);

					string endcameratime = System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss");
					PlayerPrefs.SetString ("Quest_" + m_NrQuestsDone + "_" + "EndCameraTime", endcameratime);
					//---------------------

					// Save positions
					float fvalue = PlayerPrefs.GetFloat("CurQuestStartPositionX");
					PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + "StartPositionX", fvalue);
					fvalue = PlayerPrefs.GetFloat("CurQuestStartPositionY");
					PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + "StartPositionY", fvalue);
					fvalue = PlayerPrefs.GetFloat("CurQuestEndPositionX");
					PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + "EndPositionX", fvalue);
					fvalue = PlayerPrefs.GetFloat("CurQuestEndPositionY");
					PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + "EndPositionY", fvalue);


					fvalue = PlayerPrefs.GetFloat("CurDistanceWalked");
					PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + "DistanceWalked", fvalue);

					int nrpositions = PlayerPrefs.GetInt("CurQuestNrPositions");
					PlayerPrefs.SetInt ("Quest_" + m_NrQuestsDone + "_" + "NrPositions", nrpositions);
					for (int pos = 0; pos < nrpositions; pos++) {
						float posx = PlayerPrefs.GetFloat ("CurQuestPositionX_" + pos);
						float posy = PlayerPrefs.GetFloat ("CurQuestPositionY_" + pos);

						PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + "PositionX_" + pos, posx);
						PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + "PositionY_" + pos, posy);
					}

					PlayerPrefs.Save ();
					mCamera2.Stop ();
					Application.LoadLevel ("QuestFinished");
				}*/
			}
		}

		m_ButtonImageRepeat.SetActive (false);
		m_ButtonImageOk.SetActive (false);
		m_PreviewImageMadeGO.SetActive (false);
		m_PreviewFlash.SetActive (false);
		m_bShowPreview = false;
	}

	public void OnRepeatClicked() {
		m_ButtonImageRepeat.SetActive (false);
		m_ButtonImageOk.SetActive (false);
		m_PreviewImageMadeGO.SetActive (false);
		m_PreviewFlash.SetActive (false);
		m_bShowPreview = false;
	}

	float calcBearing (float _lat1 , float _long1, float _lat2, float _long2)
	{
		float lat1 = _lat1 * (Mathf.PI / 180);
		float lat2 = _lat2 * (Mathf.PI / 180);
		float long1 = _long1 * (Mathf.PI / 180);
		float long2 = _long2 * (Mathf.PI / 180);


		float dLon = (long2 - long1);


		float y = Mathf.Sin(dLon) * Mathf.Cos(lat2);
		float x = Mathf.Cos(lat1) * Mathf.Sin(lat2) - Mathf.Sin(lat1) * Mathf.Cos(lat2) * Mathf.Cos(dLon);

		float brng = Mathf.Atan2(y, x);

		brng = ((brng) * (180.0f / Mathf.PI));

		return brng;
	}
}
