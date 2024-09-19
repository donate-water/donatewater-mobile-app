//#define DEBUGAPP

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




public class Map : MonoBehaviour
{
	public GameObject m_ButtonBack;
	public GameObject m_Label1;
	public GameObject m_Label2;
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


	private void Start()
	{
		StartCoroutine(changeFramerate());
		ForceLandscapeLeft ();

		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();
		
		control = (OnlineMapsTileSetControl) OnlineMapsControlBase.instance;
		api = OnlineMaps.instance;

		OnlineMapsControlBase3D control2 = GetComponent<OnlineMapsControlBase3D>();

		if (control2 == null)
		{
			Debug.Log("You must use the 3D control (Texture or Tileset).");
			return;
		}
			

		OnlineMapsControlBase.instance.allowUserControl = false;//true;//false;
	//	OnlineMaps.instance.OnChangeZoom += OnChangeZoom;
		OnlineMapsTile.OnTileDownloaded += OnTileDownloaded;
		OnlineMaps.instance.OnStartDownloadTile += OnStartDownloadTile;

		control2.OnMapPress += OnMapPress;
		control2.OnMapRelease += OnMapRelease;
		control2.OnMapZoom += OnMapZoom;

		//----------------
		// Set 2d mode
		Camera c = Camera.main;
		c.orthographic = true;
		//---------------

/*		control2.setUpdateControl (true);
		control2.setAlwaysUpdateControl (true);*/
		control2.allowUserControl = true;

		m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("BtnBack");

		//OnLocationChanged (new Vector2 (16.363449f, 48.210033f));
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
		if (File.Exists(path) && false) // TODO: comment false out again
		{
			Debug.Log ("OnStartDownloadTile -> CACHED!!!");
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
	private void Update()
	{
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

		/*OnlineMapsControlBase3D control2 = GetComponent<OnlineMapsControlBase3D> ();
		control2.setUpdateControl (true);
		control2.setAlwaysUpdateControl (false);*/

		OnChangePosition ();
	}

	private void OnMapZoom()
	{
	/*	OnlineMapsControlBase3D control2 = GetComponent<OnlineMapsControlBase3D> ();
		control2.setUpdateControl (true);
		control2.setAlwaysUpdateControl (false);*/
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



	public void BackClicked () 
	{
		Application.LoadLevel ("DemoMap");
	}
}
