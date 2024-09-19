using UnityEngine;
using UnityEngine.UI;
//using NatCamU;
//using AndroidNativeCore;

using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


using System.Collections;
using System.Text;
using System;
//using Signalphire;

#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif

using UnityEngine.Android;
//using Vuforia;

public class EventSystemCameraOnePic : MonoBehaviour {//UnitygramBase {

	[Header("Barcodes")]
	//	public BarcodeFormat detectionFormat = BarcodeFormat.ALL;
	public bool continuousDetection = true;

	[Header("UI")]
	public Button switchCamButton;
	public Button flashButton;
	public Button barcodeButton;
	//public Image checkIco;
	//public Image flashIco;
	public Text flashText;
	public Text barcodeText;
	public Button m_ButtonBack;


	public GameObject m_ButtonMakePhoto2;
	public Text m_TextTask;
	//public Image m_ImageTask;

	public GameObject m_ButtonMakePhoto;
	public GameObject m_ButtonMakePhotoDisabled;

	bool m_bShowError;
	float m_ErrorTimer;
	public GameObject m_TextError;
	public GameObject m_BackError;

	string m_StrTextDebug = "";
	public GameObject m_TextDebug;


	public RawImage m_PreviewImageMade;
	public GameObject m_ButtonImageRepeat;
	public GameObject m_ButtonImageOk;
	public GameObject m_PreviewImageMadeGO;
	public GameObject m_PreviewFlash;
	//public RawImage m_PreviewFotoTest;

	bool m_bCompassEnabled;

	bool m_bPointInReach;


	public Text m_DebugText;


	float m_PinLocationLat;
	float m_PinLocationLng;

	int m_NrQuestsDone;

	bool m_bShowPreview = false;
	float m_ShowPreviewTimer;

	public SensorImagePortrait m_SensorImage;


	//public WebCamTexture mCamera2 = null;

	bool m_bShowGivePermission;

	void Awake() {
/*		NativeEssentials.OnAndroidPermissionCallback += (requestResult) =>
		{
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
	//	};
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
		yield return new WaitForSeconds (0.01f);
		/*for (int i = 0; i < 3; i++) {
			if (i == 0) {
				Screen.orientation = ScreenOrientation.Portrait;
			}  else if (i == 1) {
				Screen.orientation = ScreenOrientation.LandscapeLeft;
			}  else {*/
		//	Screen.autorotateToPortrait = true;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		//	}x
		yield return new WaitForSeconds (0.5f);
		//}
	}

	// Use this for initialization
	//public override void Start () {
	//int m_PermissionIter;
	bool m_bDebug;

//	public FlashLight m_FlashLight;
	//FlashlightController m_FlashlightController;
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

#if ASDFSADFASFD
		m_FlashOn.SetActive(false);
		m_FlashOff.SetActive(true);
#endif

		m_TextDebug.SetActive (false);
		if (PlayerPrefs.HasKey ("DebugEnabled")) {
			if (PlayerPrefs.GetInt ("DebugEnabled") == 1) {
				m_TextDebug.SetActive (true);
			}
		}
		m_TextDebug.SetActive(true);



#if UNITY_ANDROID

		if (Permission.HasUserAuthorizedPermission(Permission.Camera))
		{
			// The user authorized use of the microphone.
		}
		else
		{
			// We do not have permission to use the microphone.
			// Ask for permission or proceed without the functionality enabled.
			Permission.RequestUserPermission(Permission.Camera);
		}

//		flash = new Flash();
		//mCamera2 = null;


		/*		NativeEssentials.Instance.Initialize();
				// Comment next line in again
				PermissionsHelper.StatusResponse sr = NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);
				//PermissionsHelper.StatusResponse sr = PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_PROVIDE_RATIONALE;//NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);
				//sr =NativeEssentials.Instance.GetAndroidPermissionStatus(PermissionsHelper.Permissions.CAMERA);
				*/

		/**		if (m_bDebug || (Application.HasUserAuthorization (UserAuthorization.WebCam) && sr == PermissionsHelper.StatusResponse.PERMISSION_RESPONSE_GRANTED)) {
			*/    /*mCamera2 = new WebCamTexture ();
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
		//	}

		/*
		mCamera2 = new WebCamTexture ();
		m_PreviewFotoTest.texture = mCamera2;
		mCamera2.Play ();*/

#else
#if ASDFASDFASDFSAF
		m_FlashlightController = new FlashlightController();
		/*	mCamera2 = new WebCamTexture ();
			m_PreviewFotoTest.texture = mCamera2;
			mCamera2.Play ();*/
#endif
#endif


		m_bShowPreview = false;

		m_bPointVisible = false;

		m_bPointInReach = true;


		if(PlayerPrefs.HasKey("CurQuestReached")) {
			int inreach = PlayerPrefs.GetInt("CurQuestReached");
			if(inreach == 0) {
				m_bPointInReach = false;
			}
		}

		if (PlayerPrefs.HasKey ("NrQuestsDone")) {
			m_NrQuestsDone = PlayerPrefs.GetInt ("NrQuestsDone");
            //m_NrQuestsDone--; // Photo is done after nr quests has been incremented therefore decrease coutner again
		} else {
			m_NrQuestsDone = 0;
		}
        /*
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



		PlayerPrefs.Save ();*/

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


		m_ButtonMakePhoto.SetActive (false);
		m_ButtonMakePhoto2.SetActive (false);
		m_ButtonMakePhotoDisabled.SetActive (false);


		if (Application.systemLanguage == SystemLanguage.German  && false) {

		} else {
			m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Back");//"Back";
			m_TextTask.text = "";

			m_TextError.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("TakePhotoExplanation");//"Hold your device in landscape mode and move it into the displayed direction to take a picture.";


			m_ButtonImageRepeat.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Retake");//"Retake";
			m_ButtonImageOk.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Next");//"Continue";
			m_ButtonMakePhoto2.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("TakePhoto");//"Take Photo!";
			//	m_ButtonMakePhoto.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Take Photo!";
		}

		updateStep ();



		Input.compass.enabled = true;
		//Input.location.Start ();

		


#if PLATFORM_ANDROID
		if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
		{
			//Permission.RequestUserPermission(Permission.Camera);

			ShowEnableCameraMsg();
		}
#else
        if (Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
			ShowEnableCameraMsg ();
		}
#endif

		

		/*if (mCamera2 == null || mCamera2.isPlaying == false || Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
			ShowEnableCameraMsg ();
		} else if (m_bPointInReach) {
			CapturePhotoDisabled ();
		}*/

	}

	void updateStep() {

			if (Application.systemLanguage == SystemLanguage.German && false) {
				m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text> ().text = "Zurück";
			} else {
				m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Back");//"Back";
			}




			m_TextTask.text = "";//LocalizationSupport.GetString("TakePhotoBeautifulPlace");//"Take Picture towards North. (1/5)";


		m_ButtonMakePhoto.SetActive (false);
		m_ButtonMakePhoto2.SetActive (false);
		m_ButtonMakePhotoDisabled.SetActive (false);
	}

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
			m_ShowPreviewTimer += Time.deltaTime;

			float procflash = (m_ShowPreviewTimer - 0.3f) / 0.6f;
			if (procflash > 1.0f)
				procflash = 1.0f;
			if (procflash < 0.0f)
				procflash = 0.0f;

			procflash = 1.0f - procflash;
			byte alpha = (byte)(255 * procflash);
			m_PreviewFlash.GetComponent<UnityEngine.UI.Image>().color = new Color32(255,255,255,alpha);


			return;
		}


		if (m_bShowError) {
			m_ErrorTimer += 40.0f;


			if (/*mCamera2 == null || mCamera2.isPlaying == false ||*/ Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
				m_ErrorTimer = 2000.0f;
			}

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



		/*
		if (mCamera2 == null) {
			ShowEnableCameraMsg ();
			return;
		}*/


		if (/*mCamera2.isPlaying == false ||*/ Application.HasUserAuthorization(UserAuthorization.WebCam) == false)
		{
			ShowEnableCameraMsg();
			return;
		}
		else
		{
			if (m_bShowError)
			{
				m_bShowError = false;
				m_TextError.SetActive(false);
				m_BackError.SetActive(false);
			}
		}





		m_ButtonMakePhoto.SetActive (false);
		m_ButtonMakePhotoDisabled.SetActive (false);

		m_ButtonMakePhoto2.SetActive (true);
		/*
		if (m_bShowError) {
			if (m_ErrorTimer < 6500) {
				m_ErrorTimer = 6500;
			}
		}*/

		/*m_bShowError = false;
			m_TextError.SetActive (false);
			m_BackError.SetActive (false);
*/


		//	m_TextMakePicture.SetActive (true);
		//		m_ImageMakePicture.SetActive (true);

	}


	public void OnBackClicked() {
		m_ButtonImageRepeat.SetActive (false);
		m_ButtonImageOk.SetActive (false);
		m_PreviewImageMadeGO.SetActive (false);
		m_PreviewFlash.SetActive (false);
		m_bShowPreview = false;


		/*
		if(mCamera2 != null) 
			mCamera2.Stop ();*/

		//Application.LoadLevel("Report");


#if UNITY_ANDROID
		
			/*if (m_Camera != null)
			{
				m_Camera.LEDOff();
			m_Camera.release();
			}*/
		/*
		if (m_FlashLight.isLightOn)
		{
			m_FlashLight.closeFlash();
		}*/
		//m_Camera.release();
#else
#if ASDFASFASDFSAF
		m_FlashlightController.TurnOff();
#endif
#endif

		Application.LoadLevel("MultiplePhotos");
		//Application.LoadLevel("DemoMap");
	}



	public void ShowEnableCameraMsg() {
		if (Application.systemLanguage == SystemLanguage.German && false) {
			m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Gib der App bitte die Erlaubnis die Kamera zu benutzen.";
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
		m_StrTextDebug += "###Make photo";
		m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = m_StrTextDebug;//"Make photo";


		if (m_bShowPreview) {
			m_StrTextDebug += ", using preview";
			m_TextError.GetComponent<UnityEngine.UI.Text> ().text = m_StrTextDebug;//"Camera is using preview.";
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
		//if (NatCam.HasPermissions == false) {
		/*if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
		{
			Permission.RequestUserPermission(Permission.Camera);

			//if (Application.HasUserAuthorization (UserAuthorization.WebCam) == false) {
			if (Application.systemLanguage == SystemLanguage.German) {
				m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Geben Sie bitte der App die Erlaubnis die Kamera zu benutzen.";
			} else {
				m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Please give the app permissions to use the camera.";
			}

			m_bShowError = true;
			m_ErrorTimer = 0.0f;
			m_TextError.SetActive (true);
			m_BackError.SetActive (true);
			m_StrTextDebug += ", noauth";
			m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = m_StrTextDebug;// "Capture photo clicked";

			return;
		}*/

		m_StrTextDebug += ", captureclicked";
		m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = m_StrTextDebug;
		//m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = "Capture photo clicked";
		//int result = 0;// NatCam.CapturePhoto(OnCapturedPhoto);
		/*if (result < 2) {
			m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = "NatCam not running";
		} */
		//	if (result < 6) {

		/*	m_StrTextDebug += ", captureresult: " + result;
			m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = m_StrTextDebug;
/
			//m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = "Capture photo clicked result: " + result;
	//	}
		/**/
		/*
		if (mCamera2 == null) {
			return;
		}
		if (mCamera2.isPlaying == false || Application.HasUserAuthorization(UserAuthorization.WebCam) == false) {
			if (Application.systemLanguage == SystemLanguage.German && false) {
				m_TextError.GetComponent<UnityEngine.UI.Text> ().text = "Gib FotoQuest Go bitte die Erlaubnis die Kamera zu benutzen.";
			} else {
				m_TextError.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("CameraPermission");//"Please give FotoQuest Go permission to use the camera.";
			}

			m_bShowError = true;
			m_ErrorTimer = 0.0f;
			m_TextError.SetActive (true);
			m_BackError.SetActive (true);
			return;
		}

		NatCam.CapturePhoto(OnCapturedPhoto);/*
		Texture2D snap = new Texture2D(mCamera2.width, mCamera2.height, TextureFormat.ARGB32, false);

		//Texture2D snap = new Texture2D(mCamera2.width, mCamera2.height);
		snap.SetPixels(mCamera2.GetPixels());
		snap.Apply();

		OnCapturedPhoto (snap);

		snap = null;*/

		Texture2D snap = m_SensorImage.TakeSnap();
		OnCapturedPhoto(snap);
		snap = null;
	}

	bool m_bPointVisible = true;

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

	Texture2D RotateTexture(Texture2D source)
	{
		Texture2D newTex = new Texture2D(source.height, source.width);
		for(int x=0; x<source.width; x++)
		{
			for(int y=0; y<source.height; y++)
			{
				Color pixel = source.GetPixel(x, y);
				newTex.SetPixel(y, source.width - x - 1, pixel);
			}
		}
		newTex.Apply();
		return newTex;
	}

	void OnCapturedPhoto (Texture2D photo) {
		Debug.Log("Captured photo width: " + photo.width + " height: " + photo.height);

		photo = RotateTexture(photo);
		m_StrTextDebug += ", Captured photo";
		m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = m_StrTextDebug;

		//m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = "Captured photo";
		int m_CurrentStep = 1;// 10;

		if (PlayerPrefs.HasKey("Quest_" + m_NrQuestsDone + "_NrPhotos"))
		{
			m_CurrentStep = PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_NrPhotos") + 1;
		}

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

		
		PlayerPrefs.SetInt("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_WhichPicture", 
			PlayerPrefs.GetInt("CameraWhichPicture"));

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



		string theTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		string theTime2 = theTime;//theTime.Replace ("+", "%2B");
		//theTime2 += "00";
		Debug.Log ("CurrentTimestamp: " + theTime2);
		PlayerPrefs.SetString ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Timestamp", theTime2);



		PlayerPrefs.Save ();


		m_StrTextDebug += ", 2";
		m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = m_StrTextDebug;
		//m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = "Captured photo 2";

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
		Debug.Log("Saved image: " + name);

		m_StrTextDebug += ", 3";
		m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = m_StrTextDebug;
		//m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = "Captured photo 3";
		if (Application.platform == RuntimePlatform.Android) { 
			File.WriteAllBytes( Application.persistentDataPath+"/"+name+".jpg",bytes );
		} else {
			//	File.WriteAllBytes( Application.dataPath+"/Resources/save_screen/"+name+".jpg",bytes );
			File.WriteAllBytes( Application.persistentDataPath+"/"+name+".jpg",bytes );
		}






		m_StrTextDebug += ", 4";
		m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = m_StrTextDebug;
		//m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = "Captured photo 4";

		m_bShowPreview = true;
		m_ShowPreviewTimer = 0.0f;
		m_PreviewImageMade.texture = photo;
		m_PreviewImageMade.enabled = true;
		m_PreviewImageMadeGO.SetActive (true);
		m_PreviewFlash.SetActive (true);

		m_ButtonImageRepeat.SetActive (true);
		m_ButtonImageOk.SetActive (true);


		photo = null;

		m_ButtonMakePhoto2.SetActive (false);


		m_StrTextDebug += ", Successfully captured photo";
		m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = m_StrTextDebug;
		//m_TextDebug.GetComponent<UnityEngine.UI.Text> ().text = "Successfully captured photo";
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



	public void OnContinueClicked() {
        PlayerPrefs.SetInt("CameraBackToReport", 1);         PlayerPrefs.SetInt("ReportPhotoMade", 1);

		int nrphotos = 0;

		if (PlayerPrefs.HasKey("Quest_" + m_NrQuestsDone + "_NrPhotos"))
		{
			nrphotos = PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_NrPhotos") + 1;
		} else
        {
			nrphotos++;
        }
		PlayerPrefs.SetInt("Quest_" + m_NrQuestsDone + "_NrPhotos", nrphotos);
			 		PlayerPrefs.Save();
		Debug.Log("Save nr photos " + m_NrQuestsDone + "_NrPhotos: " + nrphotos);


#if UNITY_ANDROID
		/*if (m_Camera != null)
		{
			m_Camera.LEDOff();
			m_Camera.release();
		}*/
		/*
		if (m_FlashLight.isLightOn)
			m_FlashLight.closeFlash();*/
		//m_Camera.release();
#else
#if ASDFASDFASDFSF
		m_FlashlightController.TurnOff();
#endif
#endif

		Application.LoadLevel("MultiplePhotos");

		//Application.LoadLevel("QuestFinished");


		/*
		Application.LoadLevel("Report");*/

       // Application.LoadLevel("Report");
        /*
		PlayerPrefs.SetInt ("Questions_FromQuestFinished", 0);
		string endcameratime = System.DateTime.Now.ToString ("yyyy/MM/dd HH:mm:ss");
		PlayerPrefs.SetString ("CurQuestEndCameraTime", endcameratime);

		PlayerPrefs.Save ();
	//	mCamera2.Stop ();

		int campaign = PlayerPrefs.GetInt ("CampaignSelected");

		int m_NrQuestsDone = 0;
		if (PlayerPrefs.HasKey ("NrQuestsDone")) {
			m_NrQuestsDone = PlayerPrefs.GetInt ("NrQuestsDone");
		} else {
			m_NrQuestsDone = 0;
		}

		Debug.Log ("How many quests done: " + m_NrQuestsDone);
		if (m_NrQuestsDone < 2) {
			Application.LoadLevel ("QuestFinished");
		} else {
			Application.LoadLevel ("DynamicQuestionsPark");
		}*/

		/*
		m_ButtonImageRepeat.SetActive (false);
		m_ButtonImageOk.SetActive (false);
		m_PreviewImageMadeGO.SetActive (false);
		m_PreviewFlash.SetActive (false);
		m_bShowPreview = false;*/
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

	public bool m_bTorchEnabled = false;
	/*public GameObject m_FlashOn;
	public GameObject m_FlashOff;*/
	AndroidCamera m_Camera;

	AndroidJavaObject camera=null;
AndroidJavaObject cameraParameters=null;

void ToggleAndroidFlashlight()
{

    if (camera == null)
    {
        AndroidJavaClass cameraClass = new AndroidJavaClass("android.hardware.Camera"); 
        camera = cameraClass.CallStatic<AndroidJavaObject>("open", 0); 
        if (camera != null)
        {
				camera.Call("startPreview");
            cameraParameters = camera.Call<AndroidJavaObject>("getParameters");
            cameraParameters.Call("setFlashMode","torch"); 
            camera.Call("setParameters",cameraParameters); 
        }       
    }
    else
    {
        cameraParameters = camera.Call<AndroidJavaObject>("getParameters");
        string flashmode = cameraParameters.Call<string>("getFlashMode");
        if(flashmode!="torch")
            cameraParameters.Call("setFlashMode","torch"); 
        else
            cameraParameters.Call("setFlashMode","off"); 

        camera.Call("setParameters",cameraParameters); 
    }
}

void ReleaseAndroidJavaObjects()
{
    if (camera != null)
    {
        camera.Call("release");
        camera = null;
    }
}

//	Flash flash;
	public void ToggleFlashlight()
	{
#if ASDFASDFASFSF
		Debug.Log("ToggleFlashlight");
		try
		{
			/*if(m_Camera == null)
				m_Camera = new AndroidCamera();

			m_Camera.startPreview();
				m_Camera.LEDOn();*/


			m_bTorchEnabled = !m_bTorchEnabled;
			if (!m_bTorchEnabled)
			{
#if UNITY_ANDROID
		//	flash.setFlashEnable(true);
				//ToggleAndroidFlashlight();
				//m_Camera.LEDOff();
				/*
				m_FlashLight.openFlash();*/
				//m_FlashLight.openFlash();
				//CameraDevice.Instance.SetFlashTorchMode(true);
#else
			m_FlashlightController.TurnOff();
#endif
				m_FlashOn.SetActive(false);
				m_FlashOff.SetActive(true);


				//m_SensorImage.LaunchSensorImage();
			}
			else
			{
#if UNITY_ANDROID

			//	flash.setFlashEnable(false);
				//ToggleAndroidFlashlight();
				/*m_Camera.startPreview();
				m_Camera.LEDOn();
				*/
				//m_SensorImage.LaunchSensorImage();

				//m_FlashLight.closeFlash();
				/*
				m_Camera.startPreview();
				m_Camera.LEDOn();*/
				//m_Camera.LEDOff();
				//m_FlashLight.closeFlash();
				//CameraDevice.Instance.SetFlashTorchMode(false);
#else
			m_FlashlightController.TurnOn();
#endif
				m_FlashOn.SetActive(true);
				m_FlashOff.SetActive(false);
			}
		}
		catch (Exception e)
		{

		}
#endif
	}
}
