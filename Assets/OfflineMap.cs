﻿//#define DEBUGAPP

using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using Unitycoding.UIWidgets;
using UnityEngine.UI;
//using Signalphire;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Text;
//using Signalphire;
//using  UTNotifications;
using Vatio.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;




public class OfflineMap : MonoBehaviour
{
	public GameObject m_ButtonBack;
	public GameObject m_ButtonBack2;
	public GameObject m_ButtonBackImage;
	public GameObject m_ButtonBack2Image;
	public GameObject m_ButtonNext;

	public GameObject m_ExplanationBack;
	public GameObject m_ExplanationText;

	public GameObject m_ButtonReset;
	public Transform camera2D;
	public Transform camera3D;

	public Shader tileShader;

	public float CameraChangeTime = 1;

	private GUIStyle activeRowStyle;
	private float animValue;
	private OnlineMaps api;
	private OnlineMapsTileSetControl control;
	private bool is2D = false;//true;
	private bool isCameraModeChange;
	private GUIStyle rowStyle;
	private string search = "";


	public GameObject m_MapDeactivated;



	public GameObject m_DownloadBack;
	public GameObject m_DownloadText;
	public GameObject m_DownloadBtnStart;
	public GameObject m_DownloadBtnBack;
	public GameObject m_DownloadImageBack;
	public GameObject m_DownloadBtnCompleted;


    public GameObject m_ToggleDownloadMap;
    public GameObject m_ToggleDownloadMapText;

	public GameObject m_DownloadMapQualityText;
	public GameObject m_DownloadMapQualitySlider;
	public GameObject m_DownloadMapQualitySliderLow;
	public GameObject m_DownloadMapQualitySliderHigh;

	public GameObject m_ToggleDownloadSamples;
    public GameObject m_ToggleDownloadSamplesText;


	string m_CurPileId;

	private void OnGUI()
	{

	}

	public class MarkerComparer2 : System.Collections.Generic.IComparer<OnlineMapsMarker3D>
	{
		public DemoMap m_pDemoMap;

		public int Compare(OnlineMapsMarker3D m1, OnlineMapsMarker3D m2)
		{
			Debug.Log ("Marker compare");

			if (m1.position.y > m2.position.y) return -1;
			if (m1.position.y < m2.position.y) return 1;
			return 0;
		}
	}


	public class MarkerComparer : System.Collections.Generic.IComparer<OnlineMapsMarker>
	{
		public DemoMap m_pDemoMap;

		public int Compare(OnlineMapsMarker m1, OnlineMapsMarker m2)
		{
			Debug.Log ("Marker compare");

			if (m1.position.y > m2.position.y) return -1;
			if (m1.position.y < m2.position.y) return 1;
			return 0;
		}
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

		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeRight = false;
		Screen.autorotateToLandscapeLeft = false;
		Screen.orientation = ScreenOrientation.Portrait;
		Screen.autorotateToPortrait = true;
		yield return new WaitForSeconds (0.5f);
	}

	int m_MapType = 0;
	private void Start()
	{
		StartCoroutine(changeFramerate());
		ForceLandscapeLeft ();

		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();

		HideDownloading ();

		control = (OnlineMapsTileSetControl) OnlineMapsControlBase.instance;
		api = OnlineMaps.instance;

		map = OnlineMaps.instance;

		OnlineMapsControlBase3D control2 = GetComponent<OnlineMapsControlBase3D>();

		if (control2 == null)
		{
			Debug.Log("You must use the 3D control (Texture or Tileset).");
			return;
		}

		m_ButtonReset.SetActive (false);

		OnlineMapsControlBase.instance.allowUserControl = false;//true;//false;
		//	OnlineMaps.instance.OnChangeZoom += OnChangeZoom;
		OnlineMapsTile.OnTileDownloaded = OnTileDownloaded;
		OnlineMaps.instance.OnStartDownloadTile = OnStartDownloadTile;

		control2.OnMapPress = OnMapPress;
		control2.OnMapRelease = OnMapRelease;
		control2.OnMapZoom = OnMapZoom;

		//----------------
		// Set 2d mode
		Camera c = Camera.main;
		c.orthographic = true;
		c.transform.position = new Vector3(-512, 512, 512);
		//---------------

		OnlineMaps.instance.mapType = "google.satellite";
		m_MapType = 0;

		//m_bBlackGui = true;
		if (PlayerPrefs.HasKey("MapType"))
		{
			int maptype = PlayerPrefs.GetInt("MapType");
			if (maptype == 0)
			{
                OnlineMaps.instance.mapType = "google.terrain";
				m_MapType = 1;

				m_ButtonBack.SetActive(false);
				m_ButtonBackImage.SetActive(false);
			}
			else if (maptype == 2)
			{
				m_MapType = 2;
				OnlineMaps.instance.mapType = "osm.mapnik"; //""google.satellite";

				m_ButtonBack.SetActive(false);
				m_ButtonBackImage.SetActive(false);
			}
			else //if (maptype == 1)
			{
				OnlineMaps.instance.mapType = "google.satellite";
				m_MapType = 0;

				m_ButtonBack2.SetActive(false);
				m_ButtonBack2Image.SetActive(false);
			}
		} else
        {
			OnlineMaps.instance.mapType = "google.satellite";
			m_MapType = 0;

			m_ButtonBack2.SetActive(false);
			m_ButtonBack2Image.SetActive(false);
		}


		/*control2.setUpdateControl (true);
		control2.setAlwaysUpdateControl (true);*/
		control2.allowUserControl = true;

		m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Back");
		m_ButtonBack2.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Back");
		m_ButtonNext.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnDownload");
		m_ExplanationText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("ExplanationOfflineAccess");


		m_DownloadMapQualityText.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("MapResolution");

		m_DownloadMapQualitySliderLow.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Low");
		m_DownloadMapQualitySliderHigh.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("High");
		m_ButtonNext.SetActive (false);




		m_bLocationGPSDisabled = false;
		m_LocationGPSDisabledIter = 0;
//		m_bStartReadGPS = true;
		m_bLocationEnabled = false;
		//m_bDragging = false;


		/**/
	/*	m_MapDeactivated.SetActive(false); // Todo: Comment this out for final release
		m_bLocationEnabled = true;*/
		/**/

		Input.compass.enabled = true;
		Input.compensateSensors = true;

		Input.location.Start();


		OnLocationChanged (new Vector2 (0.0f, 0.0f));


		toUserLocation();
	}

    public void DownloadMapToggle(bool bValue)
    {
        if(bValue)
		{
			m_DownloadMapQualitySlider.SetActive(true);
			m_DownloadMapQualityText.SetActive(true);
			//m_DownloadMapQualitySlider.GetComponent<Slider>().enabled = true;
			//m_DownloadMapQualitySlider.GetComponent<Slider>().interactable = true;

		}
        else
		{
			m_DownloadMapQualitySlider.SetActive(false);
			m_DownloadMapQualityText.SetActive(false);
			//m_DownloadMapQualitySlider.GetComponent<Slider>().enabled = false;
			//m_DownloadMapQualitySlider.GetComponent<Slider>().interactable = false;

		}
    }

	int m_ZoomMaxLevel = 10;//18;
    void CollectMapTiles()
    {
		float lat1 = m_Pin1Position.y;
		float lat2 = m_Pin2Position.y;
		float lng1 = m_Pin1Position.x;
		float lng2 = m_Pin2Position.x;

		if (lat1 > lat2)
		{
			float temp = lat2;
			lat2 = lat1;
			lat1 = temp;
		}
		if (lng1 > lng2)
		{
			float temp = lng2;
			lng2 = lng1;
			lng1 = temp;
		}

		Debug.Log("lat1: " + lat1 + " lat2: " + lat2 + " lng1: " + lng1 + " lng2: " + lng2);
		//float border = 0.03f;


		OnlineMaps api = OnlineMaps.instance;
		double px, py;
		double px2, py2;


		m_DownloadTilesZoom = new ArrayList();
		m_DownloadTilesX = new ArrayList();
		m_DownloadTilesY = new ArrayList();


		for (int zoom = 4; zoom < m_ZoomMaxLevel; zoom++)
		{

			api.projection.CoordinatesToTile(lng1, lat1, zoom, out px, out py);
			Debug.Log("tile download test zoom bl: " + zoom + " x: " + px + " y: " + py);


			api.projection.CoordinatesToTile(lng2, lat2, zoom, out px2, out py2);
			Debug.Log("tile download test zoom tr: " + zoom + " x: " + px2 + " y: " + py2);

			int blx = (int)px;
			int trx = (int)px2;
			int bly = (int)py;
			int tr_y = (int)py2;
			if (blx > trx)
			{
				int temp = blx;
				blx = trx;
				trx = temp;
			}
			if (bly > tr_y)
			{
				int temp = bly;
				bly = tr_y;
				tr_y = temp;
			}

			//m_bDownloadingImage = false;
			for (int x = blx; x <= trx; x++)
			{
				for (int y = bly; y <= tr_y; y++)
				{
					m_DownloadTilesZoom.Add(zoom + "");
					m_DownloadTilesX.Add(x + "");
					m_DownloadTilesY.Add(y + "");
				}
			}
		}


		int kb = m_DownloadTilesZoom.Count * 10;

		if (kb < 1000)
		{
			m_DownloadText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Download1") + " " +
			kb + " KB " +
			LocalizationSupport.GetString("Download2");
			m_ToggleDownloadMapText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("DownloadMapData") + " (" + kb + " KB).";
		}
		else
		{
			float mb = kb / 1000.0f;
			m_DownloadText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Download1") + " " +
				mb + " MB " +
				LocalizationSupport.GetString("Download2");


			m_ToggleDownloadMapText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("DownloadMapData") + " (" + mb + " MB).";
		}
	}

    public void QualityChangeSlider(float value)
    {
		m_ZoomMaxLevel = (int)value;
		Debug.Log("QualityChangeSlider: " + value);
		CollectMapTiles();
	}

	private static string GetTilePath(OnlineMapsTile tile)
	{
		string[] parts =
		{
			Application.persistentDataPath,
			"OnlineMapsTileCache",
			tile.mapType.provider.id,
			tile.mapType.id,
			tile.zoom.ToString(),
			tile.x.ToString(),
			tile.y + ".png"
		};
		return string.Join("/", parts);
	}

	private void OnStartDownloadTile(OnlineMapsTile tile)
	{
		// Get local path.
		string path = GetTilePath(tile);

		// If the tile is cached.
		if (File.Exists(path) /*&& false*/) // TODO: comment false out again
		{
		//	Debug.Log ("OnStartDownloadTile -> CACHED!!!");
			// Load tile texture from cache.
			Texture2D tileTexture = new Texture2D(256, 256);
			tileTexture.LoadImage(File.ReadAllBytes(path));
			tileTexture.wrapMode = TextureWrapMode.Clamp;

			// Send texture to map.
			if (OnlineMaps.instance.target == OnlineMapsTarget.texture)
			{
				tile.ApplyTexture(tileTexture);
				OnlineMaps.instance.buffer.ApplyTile(tile);
			}
			else
			{
				tile.texture = tileTexture;
				tile.status = OnlineMapsTileStatus.loaded;
			}

			// Redraw map.
			OnlineMaps.instance.Redraw();
		}
		else
		{
			// If the tile is not cached, download tile with a standard loader.
			OnlineMaps.instance.StartDownloadTile(tile);
		}
	}

	void OnTileDownloaded(OnlineMapsTile tile)
	{
		//	Debug.Log ("OnTileDownloaded");
		string path = GetTilePath(tile);

		// Cache tile.
		FileInfo fileInfo = new FileInfo(path);
		DirectoryInfo directory = fileInfo.Directory;
		if (!directory.Exists) directory.Create();

		File.WriteAllBytes(path, tile.www.bytes);
	}

	int m_ChangePositionIter = 0;
	Vector3 m_MousePosition;
	float m_MouseDistance = 0.0f;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			BackClicked();
		}
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("#### CLICK #####");
			m_MousePosition = Input.mousePosition;
			m_MouseDistance = 0.0f;
		}
		if (Input.GetMouseButton (0)) {
			float _x = Input.mousePosition.x - m_MousePosition.x;
			float _y = Input.mousePosition.y - m_MousePosition.y;
			m_MouseDistance += Mathf.Sqrt (_x * _x + _y * _y);
			m_MousePosition = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp (0) && m_bLocationEnabled) {
			Debug.Log ("#### CLICK #####");
			Vector3 pos = Input.mousePosition;

			PointerEventData pointerData = new PointerEventData(EventSystem.current) {
				pointerId = -1,
			};

			pointerData.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerData, results);

			if (m_MouseDistance < 30.0f) {
				bool bHitPin = false;
				if (results.Count > 0) {
					Debug.Log ("Nr results pick gui: " + results.Count);
					Debug.Log ("pick gui element name: " + results [0].gameObject.name);
				} else {
					Debug.Log ("Mouse down -> check hit");
					RaycastHit hit;
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out hit)) {
						Debug.Log ("Mouse Down Hit the following object: " + hit.collider.name);
						if (hit.collider.name == "Map") {
							Debug.Log ("> Clicked on map");
						} else {
							Debug.Log ("> Clicked on marker");
							OnPinClick (hit.collider.gameObject);
							bHitPin = true;
						}
					}
				}

				if (pos.x < Screen.width * 0.2f && pos.y < Screen.width * 0.2f) {
					Debug.Log ("touched corner");
				} else {
					if (!bHitPin) {
						touchedPos (pos);
					}
				}
			}
		}

		if (m_bCalculatingSize) {
			m_CalculatingSizeIter++;
			if (m_CalculatingSizeIter > 4) {
				m_bCalculatingSize = false;
				StartCoroutine (DownloadTilesTest ());
			}
		}

		if (m_bDownloadingImages) {
			if (m_bDownloadingImage == false && m_bDownloadedImage == false) {
				if (m_DownloadingImageIter < m_DownloadTilesZoom.Count) {
					m_bDownloadingImage = true;
					StartCoroutine (DownloadTilesTest (m_DownloadingImageIter));
				} else {
					m_bDownloadingImages = false;
				}
			} else if (m_bDownloadedImage) {
				m_DownloadingImageIter++;
				int imageiter = m_DownloadingImageIter + 1;
				if (m_DownloadingImageIter < m_DownloadTilesZoom.Count) {
					m_DownloadText.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("Downloading1") + imageiter + " / "  + m_DownloadTilesZoom.Count +
						" " + 
						LocalizationSupport.GetString("Downloading2");

					m_bDownloadingImage = true;
					m_bDownloadedImage = false;
					StartCoroutine (DownloadTilesTest (m_DownloadingImageIter));
				} else {
                    /*if (m_ToggleDownloadSamples.GetComponent<Toggle>().isOn)
                    {
                        m_bDownloadingImages = false;
                        DownloadPoints();
                    } 
                    else 
                    {*/
                        m_DownloadText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("DownloadFinished");
                        m_DownloadBtnCompleted.SetActive(true);    
                   // }
				}
			}
		}


		UpdateLine ();

		if (!changed) return;
		changed = false;

		// If the number of points is less than 3, then return.
		/*
		if (markers.Count < 3)
		{
			map.Redraw();
			return;
		}

		// If the polygon is not created, then create.
		if (polygon == null)
		{
			int borderWidth = 5;
			// For points, reference to markerPositions. 
			// If you change the values ​​in markerPositions, value in the polygon will be adjusted automatically.
			polygon = new OnlineMapsDrawingPoly(markerPositions, Color.black, borderWidth, new Color(1, 1, 1, 0.3f));

			// Add an element to the map.
			map.AddDrawingElement(polygon);
		}
		map.Redraw();
		Debug.Log ("Polygin created");*/


		if (m_bLocationGPSDisabled && false)
		{//TODO: Comment false out again
			m_LocationGPSDisabledIter++;
			if (m_LocationGPSDisabledIter > 100)
			{
				m_LocationGPSDisabledIter = 0;
				if (!m_bLocationGPSDisabledReading)
				{
					toUserLocation();
				}
			}
		}

	}

	private void OnLocationChanged(Vector2 position)
	{

		OnlineMaps.instance.position = position;

		//updatePins ();
		//addLineToPin ();
	}

	bool m_bDragging = false;
	private void OnMapPress()
	{
		m_bDragging = true;
		Debug.Log ("OnMapPress");

	/*	OnlineMapsControlBase3D control2 = GetComponent<OnlineMapsControlBase3D> ();
		control2.setUpdateControl (true);
		control2.setAlwaysUpdateControl (true);*/
	}

	private void OnMapRelease()
	{
		m_bDragging = false;
		Debug.Log ("OnMapReleased");

		//OnlineMapsControlBase3D control2 = GetComponent<OnlineMapsControlBase3D> ();
//		control2.setUpdateControl (true);
//		control2.setAlwaysUpdateControl (false);

		OnChangePosition ();
	}

	private void OnMapZoom()
	{
	//	OnlineMapsControlBase3D control2 = GetComponent<OnlineMapsControlBase3D> ();
	//	control2.setUpdateControl (true);
	//	control2.setAlwaysUpdateControl (false);
	}

	bool m_bLastPositionSet = false;
	int m_LastZoom;
	Vector2 m_LastPosition;
	private void OnChangePosition()
	{
		if (m_bLastPositionSet == false) {
			m_LastPosition = OnlineMaps.instance.position;
			m_LastZoom = OnlineMaps.instance.zoom;
			m_bLastPositionSet = true;
		} else /*if(!m_bIn2dMap)*/ {
			float difx = OnlineMaps.instance.position.x - m_LastPosition.x;
			float dify = OnlineMaps.instance.position.y - m_LastPosition.y;

			if (difx < 0.0f)
				difx *= -1.0f;
			if (dify < 0.0f)
				dify *= -1.0f;

			Vector2 topleft = OnlineMaps.instance.topLeftPosition;
			Vector2 bottomright = OnlineMaps.instance.bottomRightPosition;

			float mapwidth = topleft.x - bottomright.x;
			float mapheight = topleft.y - bottomright.y;
			if (mapwidth < 0.0f)
				mapwidth *= -1.0f;
			if (mapheight < 0.0f)
				mapheight *= -1.0f;

			float maxdifx = mapwidth * 0.05f;
			float maxdify = mapheight * 0.05f;

			if (difx > maxdifx || dify > maxdify || OnlineMaps.instance.zoom != m_LastZoom) {
				m_LastPosition = OnlineMaps.instance.position;
				m_LastZoom = OnlineMaps.instance.zoom;
				m_bLastPositionSet = true;
			}
		}
	}


	public GameObject m_PinPlane;
	public GameObject m_PinPlane2;
	private OnlineMaps map;
	//private List<OnlineMapsMarker3D> markers = new List<OnlineMapsMarker3D>();
	//private List<Vector2> markerPositions = new List<Vector2>();
	private bool changed = false;
	private OnlineMapsDrawingPoly polygon;

	bool m_bPin1Set = false;
	OnlineMapsMarker3D m_Pin1;
	Vector2 m_Pin1Position;
	bool m_bPin2Set = false;
	OnlineMapsMarker3D m_Pin2;
	Vector2 m_Pin2Position;

	private void touchedPos(Vector3 pos)
	{
		Vector3 mouseGeoLocation = OnlineMapsControlBase.instance.GetCoords(pos);



			// Get the geographical coordinates of the cursor.
			//Vector2 cursorCoords = map.control.GetCoords();

			// Create a new marker at the specified coordinates.
			OnlineMapsControlBase3D control2 = GetComponent<OnlineMapsControlBase3D> ();

		if (m_MapType == 0)
		{
			if (m_bPin1Set == false)
			{
				OnlineMapsMarker3D marker = control2.AddMarker3D(mouseGeoLocation, m_PinPlane);
				m_Pin1 = marker;
				m_Pin1Position = mouseGeoLocation;
				m_bPin1Set = true;
			}
			else if (m_bPin2Set == false)
			{
				OnlineMapsMarker3D marker = control2.AddMarker3D(mouseGeoLocation, m_PinPlane);
				m_Pin2 = marker;
				m_Pin2Position = mouseGeoLocation;
				m_bPin2Set = true;
			}
		} else
        {
			if (m_bPin1Set == false)
			{
				OnlineMapsMarker3D marker = control2.AddMarker3D(mouseGeoLocation, m_PinPlane2);
				m_Pin1 = marker;
				m_Pin1Position = mouseGeoLocation;
				m_bPin1Set = true;
			}
			else if (m_bPin2Set == false)
			{
				OnlineMapsMarker3D marker = control2.AddMarker3D(mouseGeoLocation, m_PinPlane2);
				m_Pin2 = marker;
				m_Pin2Position = mouseGeoLocation;
				m_bPin2Set = true;
			}
		}

		//	OnlineMapsMarker marker = map.AddMarker(cursorCoords, m_PinPlane, "Marker " + (map.markers.Length + 1));

			// Save marker and coordinates.
		//	markerPositions.Add(mouseGeoLocation);
	//		markers.Add(marker);

			// Mark that markers changed.
			changed = true;


		m_ButtonReset.SetActive (true);

		addLineToPin ();
		Debug.Log ("Added marker at pos: " + mouseGeoLocation.x + " y: " + mouseGeoLocation.y);

		if (m_bPin1Set && m_bPin2Set) {
			m_ButtonNext.SetActive (true);
		} else {
			m_ButtonNext.SetActive (false);
		}

		m_ExplanationText.SetActive (false);
		m_ExplanationBack.SetActive (false);
	}


	private void OnPinClick(GameObject go)
	{
		OnlineMapsControlBase3D control2 = GetComponent<OnlineMapsControlBase3D> ();
		if (m_bPin1Set) {
			if (m_Pin1.instance == go) {
				m_bPin1Set = false;
			}
		}
		if (m_bPin2Set) {
			if (m_Pin2.instance == go) {
				m_bPin2Set = false;
			}
		}

		api.RemoveAllMarkers ();
		api.RemoveAllDrawingElements ();
		if (control2 != null) {
			control2.RemoveAllMarker3D ();
		}

		if (m_MapType == 0)
		{
			if (m_bPin1Set)
			{
				OnlineMapsMarker3D marker = control2.AddMarker3D(m_Pin1Position, m_PinPlane);
				m_Pin1 = marker;
			}
			if (m_bPin2Set)
			{
				OnlineMapsMarker3D marker = control2.AddMarker3D(m_Pin2Position, m_PinPlane);
				m_Pin2 = marker;
			}
		} else
        {
			if (m_bPin1Set)
			{
				OnlineMapsMarker3D marker = control2.AddMarker3D(m_Pin1Position, m_PinPlane2);
				m_Pin1 = marker;
			}
			if (m_bPin2Set)
			{
				OnlineMapsMarker3D marker = control2.AddMarker3D(m_Pin2Position, m_PinPlane2);
				m_Pin2 = marker;
			}
		}
		addLineToPin ();

		if (m_bPin1Set == false && m_bPin2Set == false) {
			m_ButtonReset.SetActive (false);
		}
		if (m_bPin1Set && m_bPin2Set) {
			m_ButtonNext.SetActive (true);
		} else {
			m_ButtonNext.SetActive (false);
		}
	}

	public void BackClicked () 
	{
		Application.LoadLevel ("DemoMap");
	}



	// Border
	private bool m_bLineInited;
	private bool m_bLineVisible;
	public Material material;
	public Material material2;
	private Vector2[] coords;
	private MeshFilter meshFilter;
	private MeshRenderer meshRenderer;
	private Mesh mesh;
	GameObject m_ContainerLine;

	public float m_SizeLine = 10;
	public Vector2 uvScale = new Vector2(2, 1);
	private float _size;

	void showLine()
	{
		if (!m_bLineInited) {
			initLine ();
		}
		if (m_bLineVisible) {
			return;
		}
		m_ContainerLine.SetActive (true);
		m_bLineVisible = true;
	}

	void hideLine()
	{
		if (!m_bLineInited) {
			return;
		}
		if (!m_bLineVisible) {
			return;
		}

		m_ContainerLine.SetActive (false);
		m_bLineVisible = false;
	}

	void initLine()
	{
		OnlineMaps api = OnlineMaps.instance;


		// Create a new GameObject.
		GameObject container = new GameObject("Dotted Line");
		m_ContainerLine = container;

		// Create a new Mesh.
		meshFilter = container.AddComponent<MeshFilter>();
		meshRenderer = container.AddComponent<MeshRenderer>();

		mesh = meshFilter.sharedMesh = new Mesh();
		mesh.name = "Dotted Line";

		if (m_MapType == 0)
		{
			meshRenderer.material = material;
		} else
        {
			meshRenderer.material = material2;
		}
		material.renderQueue = 2950;

		// Init coordinates of points.
		coords = new Vector2[2];

		coords[0] = new Vector2(16.363449f, 48.210033f);
		coords[1] = new Vector2(16.353449f, 48.220033f);

		m_bLineInited = true;
		m_bLineVisible = true;
	}

	public void addLineToPin()
	{
		OnlineMaps api = OnlineMaps.instance;


		if (m_bPin1Set == false || m_bPin2Set == false) {
			if (m_bLineInited && m_bLineVisible) {
				hideLine ();
			}
			return;
		}

		if (!m_bLineInited) {
			initLine ();
		} else if (!m_bLineVisible) {
			showLine ();
		}

		coords = new Vector2[5];
		coords[0] = new Vector2(m_Pin1Position.x, m_Pin1Position.y);//new Vector2(48.210033f, 16.363449f);//new Vector2();
		coords[1] = new Vector2((float)m_Pin2Position.x, (float)m_Pin1Position.y );
		coords[2] = new Vector2((float)m_Pin2Position.x, (float)m_Pin2Position.y );
		coords[3] = new Vector2((float)m_Pin1Position.x, (float)m_Pin2Position.y );
		coords[4] = new Vector2((float)m_Pin1Position.x, (float)m_Pin1Position.y );
		UpdateLine ();
	}



	private void UpdateLine()
	{
		if (m_bLineInited == false) {
			return;
		}
		_size = m_SizeLine;
		//return;

		float totalDistance = 0;
		Vector3 lastPosition = Vector3.zero;

		List<Vector3> vertices = new List<Vector3>();
		List<Vector2> uvs = new List<Vector2>();
		List<Vector3> normals = new List<Vector3>();
		List<int> triangles = new List<int>();

		List<Vector3> positions = new List<Vector3>();


		for (int i = 0; i < coords.Length; i++)
		{
			// Get world position by coordinates
			Vector3 position = OnlineMapsTileSetControl.instance.GetWorldPosition(coords[i]);
			positions.Add(position);

			if (i != 0)
			{
				// Calculate angle between coordinates.
				float a = OnlineMapsUtils.Angle2DRad(lastPosition, position, 90);

				// Calculate offset
				Vector3 off = new Vector3(Mathf.Cos(a) * m_SizeLine, 0, Mathf.Sin(a) * m_SizeLine);

				// Init verticles, normals and triangles.
				int vCount = vertices.Count;

				vertices.Add(lastPosition + off);
				vertices.Add(lastPosition - off);
				vertices.Add(position + off);
				vertices.Add(position - off);

				normals.Add(Vector3.up);
				normals.Add(Vector3.up);
				normals.Add(Vector3.up);
				normals.Add(Vector3.up);

				triangles.Add(vCount);
				triangles.Add(vCount + 3);
				triangles.Add(vCount + 1);
				triangles.Add(vCount);
				triangles.Add(vCount + 2);
				triangles.Add(vCount + 3);

				totalDistance += (lastPosition - position).magnitude;
			}

			lastPosition = position;
		}

		float tDistance = 0;

		for (int i = 1; i < positions.Count; i++)
		{
			float distance = (positions[i - 1] - positions[i]).magnitude;

			// Updates UV
			uvs.Add(new Vector2(0.0f, 0));
			uvs.Add(new Vector2(0.0f, 1));

			tDistance += distance;

			float proc = distance / 500.0f;
			uvs.Add(new Vector2(proc, 0));
			uvs.Add(new Vector2(proc, 1));
		}

		// Update mesh
		mesh.vertices = vertices.ToArray();
		mesh.normals = normals.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.triangles = triangles.ToArray();

		// Scale texture
		Vector2 scale = new Vector2(totalDistance / m_SizeLine, 1);
		scale.Scale(uvScale);
		meshRenderer.material.mainTextureScale = scale;
	}

	public void OnReset()
	{
		OnlineMapsControlBase3D control2 = GetComponent<OnlineMapsControlBase3D> ();
		api.RemoveAllMarkers ();
		api.RemoveAllDrawingElements ();
		if (control2 != null) {
			control2.RemoveAllMarker3D ();
		}
		m_ButtonReset.SetActive (false);
		m_ButtonNext.SetActive (false);

		m_bPin1Set = false;
		m_bPin2Set = false;
		hideLine ();
	}

	public void HideDownloading()
	{
		m_DownloadBack.SetActive (false);
		m_DownloadText.SetActive (false);
		m_DownloadBtnStart.SetActive (false);
		m_DownloadBtnBack.SetActive (false);
		m_DownloadImageBack.SetActive (false);
		m_DownloadBtnCompleted.SetActive (false);


        m_ToggleDownloadMap.SetActive(false);
        m_ToggleDownloadSamples.SetActive(false);

		m_DownloadMapQualitySlider.SetActive(false);
		m_DownloadMapQualityText.SetActive(false);
		m_bDownloadingImages = false;
	}

	bool m_bCalculatingSize = false;
	int m_CalculatingSizeIter = 0;
	public void StartDownloading()
	{
		m_DownloadText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Calculating");
		m_DownloadBack.SetActive (true);
		m_DownloadText.SetActive (true);
		m_DownloadBtnStart.SetActive (false);
		m_DownloadBtnBack.SetActive (true);
		m_DownloadImageBack.SetActive (true);
		m_DownloadBtnCompleted.SetActive (false);

		m_bCalculatingSize = true;
		m_CalculatingSizeIter = 0;

	}

	public void StartDownloadingNow()
	{
		m_DownloadBtnStart.SetActive (false);
        m_DownloadBtnCompleted.SetActive (false);
        m_ToggleDownloadMap.SetActive(false);         m_ToggleDownloadSamples.SetActive(false);
        m_DownloadText.SetActive(true);


		m_DownloadMapQualitySlider.SetActive(false);
		m_DownloadMapQualityText.SetActive(false);
		if (m_ToggleDownloadMap.GetComponent<Toggle>().isOn)
        {
            m_bDownloadingImages = true;
            m_bDownloadingImage = false;
            m_bDownloadedImage = false;
            m_DownloadingImageIter = 0;
        }
       /* else if (m_ToggleDownloadSamples.GetComponent<Toggle>().isOn)
        {
            m_bDownloadingImages = false;
            DownloadPoints();
        }*/
        else 
        {
            m_DownloadText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("DownloadFinished");
            m_DownloadBtnCompleted.SetActive(true);   
        }
	}


	ArrayList m_DownloadTilesZoom;
	ArrayList m_DownloadTilesX;
	ArrayList m_DownloadTilesY;

	IEnumerator DownloadTilesTest() {

	//	float longitude = 100.752858f;//-49.876562f;// OnlineMaps.instance.position.x;
//		float latitude = 0.639726f;//-13.169026f;//OnlineMaps.instance.position.y;



		CollectMapTiles();

        m_DownloadText.SetActive(false);


        m_ToggleDownloadMap.SetActive(true);
		m_DownloadMapQualityText.SetActive(true);
		m_DownloadMapQualitySlider.SetActive(true);
		//m_ToggleDownloadSamples.SetActive(true);
       // m_ToggleDownloadSamplesText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("DownloadPoints");

		int kb = m_DownloadTilesZoom.Count * 10;

		if (kb < 1000) {
			m_DownloadText.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("Download1") + " " +
			kb + " KB " +
			LocalizationSupport.GetString ("Download2");
            m_ToggleDownloadMapText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("DownloadMapData") + " (" + kb + " KB).";
		} else {
			float mb = kb / 1000.0f;
			m_DownloadText.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString ("Download1") + " " +
				mb + " MB " +
				LocalizationSupport.GetString ("Download2");


            m_ToggleDownloadMapText.GetComponent<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("DownloadMapData") + " (" + mb + " MB).";
		}
        m_DownloadText.GetComponent<UnityEngine.UI.Text>().text = "";

		m_DownloadBtnStart.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnDownloadNow");
		m_DownloadBtnStart.SetActive (true);
		yield return null;
	}


	private string GetTilePathDownloaded(string zoom, string x, string y)
	{
		if (m_MapType == 0)
		{
			string[] parts =
			    {
			    Application.persistentDataPath,
			    "OnlineMapsTileCache",
			    "google",
			    "satellite",
			    zoom+"",
			    x+"",
			    y + ".png"
		    };
			return string.Join("/", parts);
		} 
		else if (m_MapType == 2)
		{
			string[] parts =
			{
				Application.persistentDataPath,
				"OnlineMapsTileCache",
				"osm",
				"mapnik",
				zoom+"",
				x+"",
				y + ".png"
			};
			return string.Join("/", parts);
		} 
		else
        {
			string[] parts =
				{
				Application.persistentDataPath,
				"OnlineMapsTileCache",
				"google",
				"terrain",
				zoom+"",
				x+"",
				y + ".png"
			};
			return string.Join("/", parts);
		}
	}

	bool m_bDownloadingImages = false;
	bool m_bDownloadingImage = false;
	bool m_bDownloadedImage = false;
	int m_DownloadingImageIter = 0;
	IEnumerator DownloadTilesTest(int tile) {

		string url = "https://mt1.googleapis.com/vt/lyrs=y&hl=en&x=" + m_DownloadTilesX[tile] + "&y=" + m_DownloadTilesY[tile] + "&z=" + 
			m_DownloadTilesZoom[tile];

		if (m_MapType == 1) {
			//newUrl = Regex.Replace(url, @"!1m4!1i\d+!2i\d+!3i\d+!4i256", "!1m4!1i{zoom}!2i{x}!3i{y}!4i256");

			//url = "https://mt0.googleapis.com/vt?pb=!1m4!1m3!1i15!2i17873!3i11356!2m3!1e0!2sm!3i295124088!3m9!2sen!3sUS!5e18!12m1!1e47!12m3!1e37!2m1!1ssmartmaps!4e0";

			url = "https://mt0.googleapis.com/vt?pb=!1m4!1m3!1i" + m_DownloadTilesZoom[tile] + "!2i" +
				 m_DownloadTilesX[tile]
				+ "!3i" + m_DownloadTilesY[tile] + "!2m3!1e0!2sm!3i295124088!3m9!2sen!3sUS!5e18!12m1!1e47!12m3!1e37!2m1!1ssmartmaps!4e0";
		}
		else if (m_MapType == 2)
		{
			url = "https://a.tile.openstreetmap.org/" + m_DownloadTilesZoom[tile] + "/" + m_DownloadTilesX[tile] + "/" + 
			      m_DownloadTilesY[tile] + ".png";
			//new MapType("Mapnik") { urlWithLabels = "https://a.tile.openstreetmap.org/{zoom}/{x}/{y}.png" },
		}

		Texture2D tileTexture = new Texture2D(256, 256, TextureFormat.RGB24, false)
		{
			wrapMode = TextureWrapMode.Clamp
		};

		Debug.Log ("Map Url to download: " + url);

		WWW www = new WWW (url);
		while (!www.isDone && www.error == null) {
		}
		if (www.error == null) {
			www.LoadImageIntoTexture (tileTexture);

			string path = GetTilePathDownloaded (m_DownloadTilesZoom [tile] + "", m_DownloadTilesX [tile] + "", m_DownloadTilesY [tile] + "");
			Debug.Log ("save path: " + path);

			FileInfo fileInfo = new FileInfo (path);
			DirectoryInfo directory = fileInfo.Directory;
			if (!directory.Exists)
				directory.Create ();

			File.WriteAllBytes (path, www.bytes);

			m_bDownloadedImage = true;


			DestroyImmediate(tileTexture);

		} else {
			m_bDownloadingImages = false;
			m_bDownloadingImage = false;
			m_bDownloadedImage = false;
			m_DownloadingImageIter = 0;
		}

		www.Dispose ();
		Resources.UnloadUnusedAssets();
		yield return null;
	}



	public static string ComputeHash(string s) {
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


	public FotoQuestPin[] m_Pins;
	public int m_NrPins;

	void DownloadPoints()
	{
		m_Pins = new FotoQuestPin[2001];
		for (int i = 0; i < 2001; i++) {
			m_Pins[i] = new FotoQuestPin();
		}
		m_NrPins = 0;



		m_DownloadText.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("DownloadingPoints");

		string url = "https://geo-wiki.org/Application/api/campaign/FotoQuestLocations";
		string param = "{";

		if (PlayerPrefs.HasKey ("PlayerId")) {
			//	param += "\"userid\":\"" + PlayerPrefs.GetString("PlayerId") + "\",";
		}

		if (PlayerPrefs.HasKey ("PlayerMail")) {
			string email = PlayerPrefs.GetString ("PlayerMail");
			string password = PlayerPrefs.GetString ("PlayerPassword");
			string passwordmd5 = ComputeHash (password);

			param += "\"email\":" + "\"" + email + "\",\"md5password\":" + "\"" + passwordmd5 + "\"" + ",";
		}

		float lat1 = m_Pin1Position.y;
		float lat2 = m_Pin2Position.y;
		float lng1 = m_Pin1Position.x;
		float lng2 = m_Pin2Position.x;

		if (lat1 > lat2) {
			float temp = lat2;
			lat2 = lat1;
			lat1 = temp;
		}
		if (lng1 > lng2) {
			float temp = lng2;
			lng2 = lng1;
			lng1 = temp;
		}

		Debug.Log ("lat1: " + lat1 + " lat2: " + lat2 + " lng1: " + lng1 + " lng2: " + lng2);

		//param += "\"bbox\":{\"xmin\":\"" + api.topLeftPosition.x + "\",\"xmax\":\"" + api.bottomRightPosition.x + "\",\"ymin\":\"" + api.bottomRightPosition.y + "\",\"ymax\":\"" + api.topLeftPosition.y + "\"}";
		param += "\"bbox\":{\"xmin\":\"" +lng1 + "\",\"xmax\":\"" + lng2 + "\",\"ymin\":\"" + lat1+ "\",\"ymax\":\"" + lat2 + "\"}";

		Debug.Log("box: " + param);
			
		param += ",\"limit\":\"2000\"";
		//	param += ",\"limit\":\"5\"";

		//	param += ",\"platform\":{\"app\""+ ":" + "\"21\"" + "}";//Android
		//param += ",\"platform\":{\"app\""+ ":" + "\"20\"" + "}";//Android
		param += ",\"platform\":{\"app\""+ ":" + "\"21\"" + "}";//Android
		param += "}";
		Debug.Log ("loadPins param: " + param);


		WWWForm form = new WWWForm();
		form.AddField ("parameter", param);

		//Debug.Log ("Url data: " + System.Text.Encoding.UTF8.GetString(form.data));
		WWW www = new WWW(url, form);

		StartCoroutine(WaitForPins(www));

	}



	int m_CurrentPin = 0;
	int m_ReadingWhich = 0;
	int m_NearPinId = -1;
	int g_LoadMapsError = 0;

	IEnumerator WaitForPins(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Pins data!: " + www.text);
			m_NrPins = 0;
			m_CurrentPin = -1;
			m_ReadingWhich = 0;
			JSONObject j = new JSONObject(www.text);
			accessPinData(j);
			m_NrPins = m_CurrentPin + 1;
			cachePinData ();


			m_DownloadText.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("DownloadFinished");
			m_DownloadBtnCompleted.SetActive (true);
		} else {
			Debug.Log ("Could not load points");
			Debug.Log("WWW Error Pins: "+ www.error);
			Debug.Log("WWW Error Pins 2: "+ www.text);

			m_DownloadText.GetComponent<UnityEngine.UI.Text> ().text = LocalizationSupport.GetString("DownloadFinished");
			m_DownloadBtnCompleted.SetActive (true);
		}   
	} 


	void accessPinData(JSONObject obj){
		switch(obj.type){
		case JSONObject.Type.OBJECT:
			for(int i = 0; i < obj.list.Count; i++){
				string key = (string)obj.keys[i];
				JSONObject j = (JSONObject)obj.list[i];
				//	Debug.Log("key: " + key);
				if (key == "id") {
					m_CurrentPin++;
					if (m_CurrentPin >= 2000) {
						m_CurrentPin = 1999;
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
				} else if (key == "first_visitor") {
					m_ReadingWhich = 6;
				} else if (key == "count_visits") {
					m_ReadingWhich = 7;
				} else {
					m_ReadingWhich = 0;
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
			if (m_ReadingWhich == 1) {
				m_Pins [m_CurrentPin].m_Id = obj.str;
			} else if (m_ReadingWhich == 2) {
				m_Pins [m_CurrentPin].m_Lat = double.Parse(obj.str);
			} else if (m_ReadingWhich == 3) {
				m_Pins [m_CurrentPin].m_Lng = double.Parse(obj.str);
			} /*else if (m_ReadingWhich == 5) {
				m_Pins [m_CurrentPin].m_Color = obj.str;
			} else if (m_ReadingWhich == 6) {
				//		Debug.Log ("Read conquered by: " + obj.str);
				m_Pins [m_CurrentPin].m_Conquerer = obj.str;
			}*/
			break;
		case JSONObject.Type.NUMBER:
			/*if (m_ReadingWhich == 4) {
				m_Pins [m_CurrentPin].m_Weight = "" + obj.n;
			} else if (m_ReadingWhich == 7) {
				m_Pins [m_CurrentPin].m_NrVisits = (int)obj.n;
			}*/
			break;
		case JSONObject.Type.BOOL:
			//		Debug.Log("bool: " + obj.b);
			break;
		case JSONObject.Type.NULL:
			//	Debug.Log("NULL");
			break;

		}
	}


	void cachePinData()
	{
		int nrpins = m_CurrentPin + 1;
		Debug.Log ("cachePinData nr pins: " + nrpins);
		PlayerPrefs.SetInt("CacheNrPins", nrpins);

		for (int i = 0; i < nrpins; i++) {
			PlayerPrefs.SetString ("CachePin_" + i + "_Id", m_Pins [i].m_Id);
		/*	PlayerPrefs.SetString ("CachePin_" + i + "_Weight", m_Pins [i].m_Weight);
			PlayerPrefs.SetString ("CachePin_" + i + "_Color", m_Pins [i].m_Color);*/

			float lat = (float)m_Pins [i].m_Lat;
			float lng = (float)m_Pins [i].m_Lng;
			PlayerPrefs.SetFloat ("CachePin_" + i + "_Lat", lat);
			PlayerPrefs.SetFloat ("CachePin_" + i + "_Lng", lng);

			/*int nrvisits = m_Pins [i].m_NrVisits;
			PlayerPrefs.SetInt ("CachePin_" + i + "_NrVisits", nrvisits);

			string conquerer = m_Pins [i].m_Conquerer;
			PlayerPrefs.SetString ("CachePin_" + i + "_Conquerer", conquerer);*/
		}
		PlayerPrefs.Save ();
	}


	public void toUserLocation()
	{
		Debug.Log("Start location service");
		//m_bLocationGPSDisabledReading = true;

		StartCoroutine(StartLocations());
	}

	int m_LocationGPSDisabledIter = 0;
	bool m_bLocationEnabled;
	bool m_bFollowingGPS = true;//true;
	bool m_bLocationGPSDisabled;
	bool m_bLocationGPSDisabledReading;

	IEnumerator StartLocations()
	{
		//UnityEngine.UI.Text textdebug;
		//textdebug = m_TextDebug.GetComponent<UnityEngine.UI.Text>();


		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
		{

			m_bLocationGPSDisabled = true;
			m_bLocationGPSDisabledReading = false;

			yield break;
		}

		Input.location.Stop();
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;

		while (Input.location.status == LocationServiceStatus.Initializing /*&& maxWait > 0*/)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}


		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{

			m_bLocationGPSDisabled = true;
			m_bLocationGPSDisabledReading = false;

			yield break;
		}
		else
		{

			m_bLocationGPSDisabled = false;
			m_bLocationGPSDisabledReading = false;
			m_bLocationEnabled = true;



			Vector2 pos;
			pos.y = Input.location.lastData.latitude;// 30.0f;//48.210033f;
			pos.x = Input.location.lastData.longitude;//12.0f;//16.363449f;
			m_bFollowingGPS = true;
			OnLocationChanged(pos);

			Debug.Log("GPS has been read");


			m_MapDeactivated.SetActive(false);

			yield break;
		}
	}
}
