using System;
using UnityEngine;
using System.Collections;
using Unitycoding.UIWidgets;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.EventSystems;
using NativeCameraNamespace;

public class EventSystemMultiplePhotos : MonoBehaviour
{
	public GameObject m_ButtonNext;
	public GameObject m_ButtonBack;

	public MessageBox messageBox;

	public GameObject m_Title;
	public GameObject m_TextPage;
	public GameObject m_ButtonTakePhoto;
	public GameObject m_NrPhotosTaken;
	public GameObject m_ButtonTakePhoto2;
	public GameObject m_NrPhotosTaken2;
	public GameObject m_ButtonTakePhoto3;
	public GameObject m_NrPhotosTaken3;
	//public GameObject m_NrPhotosTakenDone;


	IEnumerator changeFramerate() {
		yield return new WaitForSeconds(1);
		Application.targetFrameRate = 30;
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

	// Use this for initialization
	
	
	public EventSystem m_EventSystem;
	void Start () {
		StartCoroutine(changeFramerate());

		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();
		
		m_EventSystem.pixelDragThreshold = 30;//

		ForceAndFixPortrait();


		//messageBox = UIUtility.Find<MessageBox> ("MessageBox");

		UpdateText();

	}


	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OnBackClicked();
		}

		if(m_bPhotoTaken)
        {
			m_PhotoTakenIter++;
			if(m_PhotoTakenIter > 2)
            {
				m_bPhotoTaken = false;
				if (File.Exists(m_FilenamePhotoTaken))
				{
					StartCoroutine(PhotoTaken(true, m_FilenamePhotoTaken));
				}
            }

		} /*else if (m_bQuestFinished)
		{
			if (m_bSavePhoto == false)
			{
				m_bQuestFinished = false;

				UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string>(MessageBoxFinish);
				string[] options = { "BACK", "YES" };
				messageBox.Show("", LocalizationSupport.GetString("MessageFinish"), ua, options);
			}
		}*/
	}

    void UpdateText()
    {
		m_TextPage.GetComponentInChildren<UnityEngine.UI.Text>().text =  4 + " / " + 4;
		int nrphotos = 0;

		int m_NrQuestsDone = 0;
		if (PlayerPrefs.HasKey("NrQuestsDone"))
		{
			m_NrQuestsDone = PlayerPrefs.GetInt("NrQuestsDone");
		}
		else
		{
			m_NrQuestsDone = 0;
		}

		if (PlayerPrefs.HasKey("Quest_" + m_NrQuestsDone + "_NrPhotos"))
		{
			nrphotos = PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_NrPhotos");
		}
		/*m_NrPhotosTaken.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TakePhotosNrPhotosTaken")
			+ " " + nrphotos;// 4 + " / " + 4;
*/
		m_NrPhotosTaken.GetComponentInChildren<UnityEngine.UI.Text>().text =
			LocalizationSupport.GetString("TakePhotos1");
		m_NrPhotosTaken2.GetComponentInChildren<UnityEngine.UI.Text>().text =
			LocalizationSupport.GetString("TakePhotos2");
		m_NrPhotosTaken3.GetComponentInChildren<UnityEngine.UI.Text>().text =
			LocalizationSupport.GetString("TakePhotos3");
		

		m_ButtonTakePhoto.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TakePhotosTakePhoto");
		m_Title.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TakePhotosTitle");

		
		m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Back");
		UpdateNrPhotosTaken();
    }

    void UpdateNrPhotosTaken()
    {
	    m_ButtonTakePhoto.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TakePhotosTakePhoto");
	    m_ButtonTakePhoto2.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TakePhotosTakePhoto");
	    m_ButtonTakePhoto3.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("TakePhotosTakePhoto");
	    
	    int nrphotos = 0;// 10;
	    
	    if (PlayerPrefs.HasKey ("NrQuestsDone")) {
		    m_NrQuestsDone = PlayerPrefs.GetInt ("NrQuestsDone");
	    } else {
		    m_NrQuestsDone = 0;
	    }

	    if (PlayerPrefs.HasKey("Quest_" + m_NrQuestsDone + "_NrPhotos"))
	    {
		    nrphotos = PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_NrPhotos");
	    }
	    
	    Debug.Log("Load nr photos: " + "Quest_" + m_NrQuestsDone + "_NrPhotos");

	    Debug.Log("m_CurrentStep photos: " + nrphotos);
	    int nrphotos1 = 0;
	    int nrphotos2 = 0;
	    int nrphotos3 = 0;

	    for (int i = 1; i <= nrphotos; i++)
	    {
		    if (PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_" + i + "_WhichPicture") == 1)
		    {
			    nrphotos1++;
		    }
		    else if (PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_" + i + "_WhichPicture") == 2)
		    {
			    nrphotos2++;
		    }
		    else if (PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_" + i + "_WhichPicture") == 3)
		    {
			    nrphotos3++;
		    }
	    }
	    Debug.Log("nrphotos1: " + nrphotos1 + " nrphotos2: " + nrphotos2 + " nrphotos3: " + nrphotos3);

	    if (nrphotos1 > 0)
	    {
		    if (nrphotos1 > 1)
		    {
			    m_ButtonTakePhoto.GetComponentInChildren<Text>().text =
				    nrphotos1 + " " + LocalizationSupport.GetString("TakePhotosTaken");
		    }
		    else
		    {
			    m_ButtonTakePhoto.GetComponentInChildren<Text>().text =
				    nrphotos1 + " " + LocalizationSupport.GetString("TakePhotoTaken");
		    }
	    }
	    if (nrphotos2 > 0)
	    {
		    if (nrphotos2 > 1)
		    {
			    m_ButtonTakePhoto2.GetComponentInChildren<Text>().text =
				    nrphotos2 + " " + LocalizationSupport.GetString("TakePhotosTaken");
		    }
		    else
		    {
			    m_ButtonTakePhoto2.GetComponentInChildren<Text>().text =
				    nrphotos2 + " " + LocalizationSupport.GetString("TakePhotoTaken");
		    }
	    }
	    if (nrphotos3 > 0)
	    {
		    if (nrphotos3 > 1)
		    {
			    m_ButtonTakePhoto3.GetComponentInChildren<Text>().text =
				    nrphotos3 + " " + LocalizationSupport.GetString("TakePhotosTaken");
		    }
		    else
		    {
			    m_ButtonTakePhoto3.GetComponentInChildren<Text>().text =
				    nrphotos3 + " " + LocalizationSupport.GetString("TakePhotoTaken");
		    }
	    }
    }

	private Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
		Color[] rpixels = result.GetPixels(0);
		float incX = ((float)1 / source.width) * ((float)source.width / targetWidth);
		float incY = ((float)1 / source.height) * ((float)source.height / targetHeight);
		for (int px = 0; px < rpixels.Length; px++)
		{
			rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth),
				incY * ((float)Mathf.Floor(px / targetWidth)));
		}
		result.SetPixels(rpixels, 0);
		result.Apply();
		return result;
	}

	bool m_bSavePhoto = false;


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

	IEnumerator PhotoTaken(bool success, string path)
	{
		yield return null;
		if (success)
		{
			Texture2D tex = null;
			byte[] fileData;

			if (File.Exists(path))
			{
				fileData = File.ReadAllBytes(path);
				tex = new Texture2D(2, 2);
				tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.

				if (tex.width > 200 && tex.height > 200)
				{

					m_bSavePhoto = true;


					if (PlayerPrefs.HasKey("NrQuestsDone"))
					{
						m_NrQuestsDone = PlayerPrefs.GetInt("NrQuestsDone");
						//m_NrQuestsDone--; // Photo is done after nr quests has been incremented therefore decrease coutner again
					}
					else
					{
						m_NrQuestsDone = 0;
					}


					int m_CurrentStep = 1; // 10;
					if (PlayerPrefs.HasKey("Quest_" + m_NrQuestsDone + "_NrPhotos"))
					{
						m_CurrentStep = PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_NrPhotos") + 1;
					}


					int nrphotos = 0;
					if (PlayerPrefs.HasKey("Quest_" + m_NrQuestsDone + "_NrPhotos"))
					{
						nrphotos = PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_NrPhotos") + 1;
					}
					else
					{
						nrphotos++;
					}

					PlayerPrefs.SetInt("Quest_" + m_NrQuestsDone + "_NrPhotos", nrphotos);




					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Heading",
						Input.compass.trueHeading);
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccX",
						Input.acceleration.x);
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccY",
						Input.acceleration.y);
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccZ",
						Input.acceleration.z);
					/*
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Heading", -1.0f);
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccX", -1.0f);
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccY", -1.0f);
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_AccZ", -1.0f);*/
					bool m_bCompassEnabled = false;

					int compassenabled = m_bCompassEnabled ? 1 : 0;
					PlayerPrefs.SetInt("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_CompassEnabled",
						compassenabled);


					PlayerPrefs.SetInt("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_WhichPicture",
						PlayerPrefs.GetInt("CameraWhichPicture"));

					float tilted = Input.acceleration.z;
					if (tilted > 1.0f)
					{
						tilted = 1.0f;
					}
					else if (tilted < -1.0f)
					{
						tilted = -1.0f;
					}

					tilted *= 90.0f;
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Tilt", tilted); /*
				if (m_bCompassEnabled)
				{
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Tilt", tilted);
				}
				else
				{
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Tilt", -1.0f);
				}*/

					float lat = Input.location.lastData.latitude;
					float lng = Input.location.lastData.longitude;

					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Lat", lat);
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Lng", lng);

					//PlayerPrefs.SetFloat ("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Accuracy", Input.compass.headingAccuracy);
					PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Accuracy",
						Input.location.lastData.horizontalAccuracy);



					string theTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:sszz");
					string theTime2 = theTime; //theTime.Replace ("+", "%2B");
					//theTime2 += "00";
					Debug.Log("CurrentTimestamp: " + theTime2);
					PlayerPrefs.SetString("Quest_" + m_NrQuestsDone + "_" + m_CurrentStep + "_Timestamp", theTime2);



					PlayerPrefs.Save();


					int w = tex.width;
					int h = tex.height;

					int newwidth = 128;
					int newheight = 128;

					if (w > h)
					{
						//float newscale = 1024.0f / w;
						float newscale = 1600.0f / w;
						newwidth = (int) ((float) w * newscale);
						newheight = (int) ((float) h * newscale);
					}
					else
					{
						//float newscale = 1024.0f / h;
						float newscale = 1600.0f / h;
						newwidth = (int) ((float) w * newscale);
						newheight = (int) ((float) h * newscale);
					}

					Texture2D scaledTex = ScaleTexture(tex, newwidth, newheight);
					//		photo.Resize(newwidth, newheight);


					scaledTex = RotateTexture(scaledTex);

					Debug.Log("captureimage width: " + w + " height: " + h + " newwidth: " + newwidth + " newheight: " +
					          newheight);
					//photo.Resize(newwidth, newheight);

					byte[] bytes = scaledTex.EncodeToJPG();
					//			photo.EncodeToJPG();

					string name = "Quest_Img_" + m_NrQuestsDone + "_" + m_CurrentStep;
					/*if (Application.platform == RuntimePlatform.Android) { 
						File.WriteAllBytes( Application.persistentDataPath+"/"+name+".jpg",bytes );
					} else {
						//	File.WriteAllBytes( Application.dataPath+"/Resources/save_screen/"+name+".jpg",bytes );
						File.WriteAllBytes( Application.persistentDataPath+"/"+name+".jpg",bytes );
					}*/
					Debug.Log("Saved image: " + name);


					if (Application.platform == RuntimePlatform.Android)
					{
						File.WriteAllBytes(Application.persistentDataPath + "/" + name + ".jpg", bytes);
					}
					else
					{
						//	File.WriteAllBytes( Application.dataPath+"/Resources/save_screen/"+name+".jpg",bytes );
						File.WriteAllBytes(Application.persistentDataPath + "/" + name + ".jpg", bytes);
					}
				}

				File.Delete(path);

				m_bSavePhoto = false;
				UpdateNrPhotosTaken();
			}
		}

		yield return null;
	}


	int m_NrQuestsDone;
	//public SunshineNativeCameraHandler m_CameraController;
	bool m_bPhotoTaken = false;
	int m_PhotoTakenIter = 0;
	string m_FilenamePhotoTaken = "";

	void PictureTakenAndroid(string path)
	{
		PlayerPrefs.SetInt("CameraWhichPicture", 1);
		PlayerPrefs.Save();
		
		m_PhotoTakenIter = 0;
		m_bPhotoTaken = true;
		m_FilenamePhotoTaken = path;
	}
	void PictureTakenAndroid2(string path)
	{
		PlayerPrefs.SetInt("CameraWhichPicture", 2);
		PlayerPrefs.Save();
		
		m_PhotoTakenIter = 0;
		m_bPhotoTaken = true;
		m_FilenamePhotoTaken = path;
	}
	void PictureTakenAndroid3(string path)
	{
		PlayerPrefs.SetInt("CameraWhichPicture", 3);
		PlayerPrefs.Save();
		
		m_PhotoTakenIter = 0;
		m_bPhotoTaken = true;
		m_FilenamePhotoTaken = path;
	}
	public void TakePhoto()
	{
		#if UNITY_EDITOR
		PlayerPrefs.SetInt("CameraWhichPicture", 1);
		PlayerPrefs.Save();
		Application.LoadLevel("CameraOnePic");
		return;
		#endif

		#if ASDFASFASDFASF
		NativeCamera.Permission NativeCamera.TakePicture( ( path ) =>
		{
			Debug.Log( "Image path: " + path );
			if( path != null )
			{
				// Create a Texture2D from the captured image
			/*	Texture2D texture = NativeCamera.LoadImageAtPath( path, maxSize );
				if( texture == null )
				{
					Debug.Log( "Couldn't load texture from " + path );
					return;
				}

				// Assign texture to a temporary quad and destroy it after 5 seconds
				GameObject quad = GameObject.CreatePrimitive( PrimitiveType.Quad );
				quad.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
				quad.transform.forward = Camera.main.transform.forward;
				quad.transform.localScale = new Vector3( 1f, texture.height / (float) texture.width, 1f );
			
				Material material = quad.GetComponent<Renderer>().material;
				if( !material.shader.isSupported ) // happens when Standard shader is not included in the build
					material.shader = Shader.Find( "Legacy Shaders/Diffuse" );

				material.mainTexture = texture;
				
				Destroy( quad, 5f );

				// If a procedural texture is not destroyed manually, 
				// it will only be freed after a scene change
				Destroy( texture, 5f );*/
			}
		} );
#endif

#if PLATFORM_ANDROID
		/*m_NrPhotosTakenDone.GetComponent<Text>().text = "Start";
		m_NrPhotosTakenDone.SetActive(true);*/

		NativeCamera.TakePicture(PictureTakenAndroid);

#if ASDFASDFASDF
		m_CameraController.TakePicture("imageTemp.jpg", (SunshineNativeCameraHandler.OnTakePictureCallbackHandler)((bool success, string path) =>
		{
			PlayerPrefs.SetInt("CameraWhichPicture", 1);
			PlayerPrefs.Save();
			if (success)
			{
				m_PhotoTakenIter = 0;
				m_bPhotoTaken = true;
				m_FilenamePhotoTaken = path;
/*				if (PlayerPrefs.HasKey("NrQuestsDone"))
				{
					m_NrQuestsDone = PlayerPrefs.GetInt("NrQuestsDone");
					//m_NrQuestsDone--; // Photo is done after nr quests has been incremented therefore decrease coutner again
				}
				else
				{
					m_NrQuestsDone = 0;
				}

				int nrphotostaken = 0;
				if (PlayerPrefs.HasKey("Quest_" + m_NrQuestsDone + "_NrPhotos"))
				{
					nrphotostaken = PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_NrPhotos") + 1;
				}
				else
				{
					nrphotostaken++;
				}
				m_NrPhotosTaken.GetComponentInChildren<UnityEngine.UI.Text>().text = "Number photos taken: " + nrphotostaken;// 4 + " / " + 4;
*/
			}
		}));
		#endif
#else
		PlayerPrefs.SetInt("CameraWhichPicture", 1);
		PlayerPrefs.Save();
		Application.LoadLevel("CameraOnePic");
#endif
	}
	
	public void TakePhoto2()
	{
#if UNITY_EDITOR
		PlayerPrefs.SetInt("CameraWhichPicture", 2);
		PlayerPrefs.Save();
		Application.LoadLevel("CameraOnePic");
		return;
#endif
#if PLATFORM_ANDROID
		/*m_NrPhotosTakenDone.GetComponent<Text>().text = "Start";
		m_NrPhotosTakenDone.SetActive(true);*/

		NativeCamera.TakePicture(PictureTakenAndroid2);
#if ASDFASDFSADFSAF
		m_CameraController.TakePicture("imageTemp.jpg", (SunshineNativeCameraHandler.OnTakePictureCallbackHandler)((bool success, string path) =>
		{
			PlayerPrefs.SetInt("CameraWhichPicture", 2);
			PlayerPrefs.Save();
			if (success)
			{
				m_PhotoTakenIter = 0;
				m_bPhotoTaken = true;
				m_FilenamePhotoTaken = path;
				/*if (PlayerPrefs.HasKey("NrQuestsDone"))
				{
					m_NrQuestsDone = PlayerPrefs.GetInt("NrQuestsDone");
					//m_NrQuestsDone--; // Photo is done after nr quests has been incremented therefore decrease coutner again
				}
				else
				{
					m_NrQuestsDone = 0;
				}

				int nrphotostaken = 0;
				if (PlayerPrefs.HasKey("Quest_" + m_NrQuestsDone + "_NrPhotos"))
				{
					nrphotostaken = PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_NrPhotos") + 1;
				}
				else
				{
					nrphotostaken++;
				}
				m_NrPhotosTaken.GetComponentInChildren<UnityEngine.UI.Text>().text = "Number photos taken: " + nrphotostaken;// 4 + " / " + 4;
*/
			}
		}));
		#endif
#else
		PlayerPrefs.SetInt("CameraWhichPicture", 2);
		PlayerPrefs.Save();
		Application.LoadLevel("CameraOnePic");
#endif
	}
	
	public void TakePhoto3()
	{
#if UNITY_EDITOR
		PlayerPrefs.SetInt("CameraWhichPicture", 3);
		PlayerPrefs.Save();
		Application.LoadLevel("CameraOnePic");
		return;
#endif
		
#if PLATFORM_ANDROID
		NativeCamera.TakePicture(PictureTakenAndroid3);
#else
		PlayerPrefs.SetInt("CameraWhichPicture", 3);
		PlayerPrefs.Save();
		Application.LoadLevel("CameraOnePic");
#endif
	}

	bool m_bQuestFinished = false;
	public void NextClicked () {
		string[] options2 = { "Ok" };
		int nrphotos = 0;// 10;
	    
		if (PlayerPrefs.HasKey ("NrQuestsDone")) {
			m_NrQuestsDone = PlayerPrefs.GetInt ("NrQuestsDone");
		} else {
			m_NrQuestsDone = 0;
		}

		if (PlayerPrefs.HasKey("Quest_" + m_NrQuestsDone + "_NrPhotos"))
		{
			nrphotos = PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_NrPhotos");
		}
	    
		Debug.Log("Load nr photos: " + "Quest_" + m_NrQuestsDone + "_NrPhotos");

		Debug.Log("m_CurrentStep photos: " + nrphotos);
		int nrphotos1 = 0;
		int nrphotos2 = 0;
		int nrphotos3 = 0;

		for (int i = 1; i <= nrphotos; i++)
		{
			if (PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_" + i + "_WhichPicture") == 1)
			{
				nrphotos1++;
			}
			else if (PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_" + i + "_WhichPicture") == 2)
			{
				nrphotos2++;
			}
			else if (PlayerPrefs.GetInt("Quest_" + m_NrQuestsDone + "_" + i + "_WhichPicture") == 3)
			{
				nrphotos3++;
			}
		}

		if (nrphotos1 == 0 || nrphotos2 == 0 || nrphotos3 == 0)
		{
			/*if (nrphotos1 == 0)
			{
				messageBox.Show("", LocalizationSupport.GetString("SurveyMakePictureField"), options2);
			} else if (nrphotos2 == 0)
			{
				messageBox.Show("", LocalizationSupport.GetString("SurveyMakePictureFarmer"), options2);
			}else if (nrphotos3 == 0)
			{
				messageBox.Show("", LocalizationSupport.GetString("SurveyMakePictureDetail"), options2);
			}*/
			//return;
		}

		if (nrphotos1 == 0)
		{
			UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string>(MessageBoxFinish);
			string[] options = {LocalizationSupport.GetString("Ok")};
			messageBox.Show("", LocalizationSupport.GetString("TakePhotos1NotDone"), ua, options);
			return;
		}
		
		if (nrphotos2 == 0)
		{
			UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string>(MessageBoxFinish);
			string[] options = {LocalizationSupport.GetString("Ok")};
			messageBox.Show("", LocalizationSupport.GetString("TakePhotos2NotDone"), ua, options);
			return;
		}
		
		if (nrphotos3 == 0)
		{
			UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string>(MessageBoxFinish);
			string[] options = {LocalizationSupport.GetString("Ok")};
			messageBox.Show("", LocalizationSupport.GetString("TakePhotos3NotDone"), ua, options);
			return;
		}
		

		//Application.LoadLevel("CapturePhoto");
		/*if (m_bSavePhoto)
		{
			m_bQuestFinished = true;
		}
		else
		{*/
			Application.LoadLevel("QuestionsEShape");/*
			UnityEngine.Events.UnityAction<string> ua = new UnityEngine.Events.UnityAction<string>(MessageBoxFinish);
			string[] options = { LocalizationSupport.GetString("BtnBack"), LocalizationSupport.GetString("BtnYes") };
			messageBox.Show("", LocalizationSupport.GetString("MessageFinish"), ua, options);
*/
			//Application.LoadLevel("QuestFinished");
		//}
	}

	bool m_bInMain = true;
	public void OnBackClicked()
	{
		//PlayerPrefs.SetInt("CameFromFromCamera", 1);
		//PlayerPrefs.Save();
		//Application.LoadLevel("QuestionsEShape");
		Application.LoadLevel("DemoMap");
	}


	void MessageBoxFinish(string answer)
    {
		if(answer == LocalizationSupport.GetString("BtnYes"))
        {
			Application.LoadLevel("QuestionsEShape");
		}
    }

}




