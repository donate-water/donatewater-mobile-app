using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class FotoQuestPinCluster
{
	public List<FotoQuestPin> m_Childs = new List<FotoQuestPin>();
	public OnlineMapsMarker3D m_Marker = null;
}

public class FotoQuestPin
{
	public string m_Id = "";
	//public string m_Weight = "";
	//public string m_Color = "";
	public double m_Lat = 0.0;
	public double m_Lng = 0.0;
	//public OnlineMapsMarker m_Marker;
	public OnlineMapsMarker3D m_Marker;
	public OnlineMapsMarker m_Marker2;
	//public bool m_bDone = false;
	//public int m_NrVisits;
	//public string m_Conquerer;
	//public int m_StorageType = -1;
	public string m_Name;

	public float m_ScreenPositionX;
	public float m_ScreenPositionY;
	public FotoQuestPinCluster m_Cluster;
	public bool m_bVisible;

	public Vector3[] vertices;

	public FotoQuestPin()
	{
		InitValues();
	}

	public void InitValues()
	{
	//	m_bDone = false;
//		m_NrVisits = 0;
	//	m_Conquerer = "";
		//Debug.Log ("Pin initvalues");
		/*longitude = currentLongitude = route[pointIndex];
		latitude = currentLatitude = route[pointIndex + 1];
		nextLongitude = route[pointIndex + 2];
		nextLatitude = route[pointIndex + 3];

		double dx, dy;
		OnlineMapsUtils.DistanceBetweenPoints(longitude, latitude, nextLongitude, nextLatitude, out dx, out dy);
		stepDistance = Math.Sqrt(dx * dx + dy * dy);
		totalTime = stepDistance / speed * 3600;

		rotation = 450 - OnlineMapsUtils.Angle2D(currentLongitude, currentLatitude, nextLongitude, nextLatitude);
*/
		Matrix4x4 matrix = new Matrix4x4();
		//matrix.SetTRS(Vector3.zero, Quaternion.Euler(0, (float)rotation, 0), Vector3.one);
		matrix.SetTRS(Vector3.zero, Quaternion.Euler(0, (float)0.0f, 0), Vector3.one);
		float halfSize = 30.0f;
		vertices = new []
		{
			matrix.MultiplyPoint(new Vector3(-halfSize, 0, -halfSize)),
			matrix.MultiplyPoint(new Vector3(-halfSize, 0, halfSize)),
			matrix.MultiplyPoint(new Vector3(halfSize, 0, halfSize)),
			matrix.MultiplyPoint(new Vector3(halfSize, 0, -halfSize))
		};
	}
}

	public class MapTest : MonoBehaviour
	{
	public FotoQuestPin[] m_Pins;
	public int m_NrPins;

	int m_bLoadMarkers = 0;

	public GameObject prefab;

	public Material g_Material;

	public Texture2D m_Pin;
		/// <summary>
		/// Move duration (sec)
		/// </summary>
		public float time = 3;

		/// <summary>
		/// Relative position (0-1) between from and to
		/// </summary>
		private float angle;

		/// <summary>
		/// Movement trigger
		/// </summary>
		private bool isMovement;

		private Vector2 fromPosition;
		private Vector2 toPosition;

	private void removePins()
	{
		OnlineMaps api = OnlineMaps.instance;

		//if (m_bLoadMarkers == 0) {
			api.RemoveAllMarkers ();
	//	}
//		if (m_bLoadMarkers == 2) {
			api.RemoveAllDrawingElements ();
	//	}
	}

	private void addMarkers()
	{
#if ASDFASDFSDFSDFSF
		Vector2 pos;
		pos.y = 48.210033f;
		pos.x = 16.363449f;

		// to GPS position;
		toPosition = pos;//OnlineMaps.instance.GetComponent<OnlineMapsLocationService>().position;

		// calculates tile positions
		Vector2 fromTile = OnlineMapsUtils.LatLongToTilef(fromPosition, OnlineMaps.instance.zoom);
		Vector2 toTile = OnlineMapsUtils.LatLongToTilef(toPosition, OnlineMaps.instance.zoom);

		OnlineMaps.instance.position = toPosition;

		OnlineMaps api = OnlineMaps.instance;

		/*OnlineMapsControlBase3D control = GetComponent<OnlineMapsControlBase3D>();

		if (control == null)
		{
			Debug.LogError("You must use the 3D control (Texture or Tileset).");
			return;
		}*/

		for (int i = 0; i < 10; i++) {
			Vector2 markerpos;
			markerpos.x = pos.x + Random.Range (-0.1f, 0.1f);
			markerpos.y = pos.y + Random.Range (-0.1f, 0.1f);

			//OnlineMapsMarker dynamicMarker = api.AddMarker (markerpos, m_Pin, "Dynamic marker");
			//OnlineMapsMarker dynamicMarker = api.AddMarker (markerpos, m_Pin);
		//	control.AddMarker3D(markerpos, prefab);
		//	api.AddDrawingElement
			//dynamicMarker.OnClick += OnMarkerClick;
		}
#endif
	}

	private void loadPins()
	{
		if (m_bLoadMarkers == 1) {
			return;
		}
		OnlineMaps api = OnlineMaps.instance;

		Debug.Log ("mapx : " + api.bottomRightPosition.x + " y: " + api.bottomRightPosition.y + " x: " + api.topLeftPosition.x + " y: " + api.topLeftPosition.y);

		string url = "https://geo-wiki.org/Application/api/campaign/FotoQuestLocations";
		string param = "{";
		//param += "\"userid\":\"13378\",";
		//param += "\"bbox\":{\"xmin\":\"16.076067\",\"xmax\":\"16.622953\",\"ymin\":\"47.982159\",\"ymax\":\"48.362637\"}";
		param += "\"bbox\":{\"xmin\":\"" + api.topLeftPosition.x + "\",\"xmax\":\"" + api.bottomRightPosition.x + "\",\"ymin\":\"" + api.bottomRightPosition.y + "\",\"ymax\":\"" + api.topLeftPosition.y + "\"}";
		param += ",\"limit\":\"100\"";
		//param += ",\"app\":\"10\"";
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

	IEnumerator WaitForPins(WWW www)
	{
		yield return www;
			// check for errors
			if (www.error == null)
			{
				Debug.Log("WWW Ok!: " + www.text);
			m_NrPins = 0;
			m_CurrentPin = -1;
			m_ReadingWhich = 0;
			JSONObject j = new JSONObject(www.text);
			accessPinData(j);
			m_NrPins = m_CurrentPin + 1;
			removePins ();
			addPins ();

		} else {
			Debug.Log("WWW Error: "+ www.error);
			Debug.Log("WWW Error 2: "+ www.text);
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
		//	Debug.Log ("string: " + obj.str);
			if (m_ReadingWhich == 1) {

				//Debug.Log ("m_CurrentPin: " + m_CurrentPin);
				m_Pins [m_CurrentPin].m_Id = obj.str;
				//Debug.Log ("Read pin id: " + obj.str);
			} else if (m_ReadingWhich == 2) {
				m_Pins [m_CurrentPin].m_Lat = double.Parse(obj.str);
			} else if (m_ReadingWhich == 3) {
				m_Pins [m_CurrentPin].m_Lng = double.Parse(obj.str);
			} /*else if (m_ReadingWhich == 5) {
				m_Pins [m_CurrentPin].m_Color = obj.str;
			}*/
			break;
		case JSONObject.Type.NUMBER:
	//		Debug.Log ("number: " + obj.n);
			if (m_ReadingWhich == 4) {
	//			m_Pins [m_CurrentPin].m_Weight = "" + obj.n;
			}
			break;
		case JSONObject.Type.BOOL:
	//		Debug.Log("bool: " + obj.b);
			break;
		case JSONObject.Type.NULL:
		//	Debug.Log("NULL");
			break;

		}
	}

	private void addPins()
	{
		/*
		OnlineMaps api = OnlineMaps.instance;

		for (int i = 0; i < m_NrPins; i++) {
			Vector2 markerpos;
			markerpos.x = (float)m_Pins [i].m_Lng;//pos.x + Random.Range (-0.1f, 0.1f);
			markerpos.y = (float)m_Pins [i].m_Lat;//pos.y + Random.Range (-0.1f, 0.1f);

			if (m_bLoadMarkers == 0) {
				OnlineMapsMarker dynamicMarker = api.AddMarker (markerpos, m_Pin, m_Pins [i].m_Weight);
				dynamicMarker.OnClick += OnMarkerClick;
			} else if (m_bLoadMarkers == 2) {
				Vector2 markerpos2;
				markerpos2.x = 0.005f;// (float)m_Pins [i].m_Lng + 0.000001f;//pos.x + Random.Range (-0.1f, 0.1f);
				markerpos2.y = 0.005f;// (float)m_Pins [i].m_Lat + 0.000001f;//pos.y + Random.Range (-0.1f, 0.1f);

				api.AddDrawingElement(new OnlineMapsDrawingRect(markerpos, markerpos2, Color.green, 1,Color.blue));
			} else if (m_bLoadMarkers == 4) {
				OnlineMapsMarker dynamicMarker = api.AddMarker (markerpos);
			}
		}*/
	}

	private void OnMarkerClick(OnlineMapsMarkerBase marker)
	{
		// Show in console marker label.
		Debug.Log(marker.label);
	}


	public void OnRenderObject()
	{
		//Debug.Log ("OnRenderObject");
		if (m_bLoadMarkers != 3) {
			return;
		}

		int countItems = m_NrPins;
		OnlineMaps map = OnlineMaps.instance;

		if (countItems == 0) return;

		double sx = map.tilesetSize.x * OnlineMapsUtils.tileSize / -map.tilesetWidth;
		double sy = map.tilesetSize.y * OnlineMapsUtils.tileSize / map.tilesetHeight;

		double tlx, tly;
		map.GetTopLeftPosition(out tlx, out tly);
		int zoom = map.zoom;
		OnlineMapsProjection projection = map.projection;
		projection.CoordinatesToTile(tlx, tly, zoom, out tlx, out tly);

		Vector3 mapPos = map.transform.position;
		int maxX = 1 << zoom;
		int halfMaxX = maxX / -2;

		GL.PushMatrix();
		GL.MultMatrix(transform.localToWorldMatrix);

		Debug.Log ("OnRenderPins: " + countItems);


		g_Material.SetPass(0);

		for (int j = 0; j < countItems; j++)
		{
			FotoQuestPin item = m_Pins[j];
		//	int materialIndex = item.materialIndex;
	//		if (i != materialIndex) continue;

			double px, py;
			projection.CoordinatesToTile(item.m_Lng, item.m_Lat, zoom, out px, out py);
			px -= tlx;
			py -= tly;

			if (px < halfMaxX) px += maxX;
			px *= sx;
			py *= sy;

			float fx = (float) px + mapPos.x;
			float fy = mapPos.y;
			float fz = (float) py + mapPos.z;

			Vector3 p1 = item.vertices[0];
			Vector3 p2 = item.vertices[1];
			Vector3 p3 = item.vertices[2];
			Vector3 p4 = item.vertices[3];

			fx = Random.Range (-100.0f, 100.0f);
			fy = Random.Range (-100.0f, 100.0f);
			fz = Random.Range (-100.0f, 100.0f);

			p1.x = Random.Range (-1.0f, 1.0f);
			p1.y = Random.Range (-1.0f, 1.0f);
			p1.z = Random.Range (-1.0f, 1.0f);


			p2.x = Random.Range (-1.0f, 1.0f);
			p2.y = Random.Range (-1.0f, 1.0f);
			p2.z = Random.Range (-1.0f, 1.0f);


			p3.x = Random.Range (-1.0f, 1.0f);
			p3.y = Random.Range (-1.0f, 1.0f);
			p3.z = Random.Range (-1.0f, 1.0f);


			p4.x = Random.Range (-1.0f, 1.0f);
			p4.y = Random.Range (-1.0f, 1.0f);
			p4.z = Random.Range (-1.0f, 1.0f);

			float v1 = p1.x + fx;
			float v2 = p1.y + fy;
			float v3 = p1.z + fz;
			Debug.Log ("x: " + v1 + " y: " + v2 + " z: " + v3);
			GL.Begin(GL.QUADS);
			GL.TexCoord2(0, 1);
			GL.Vertex3(p1.x + fx, p1.y + fy, p1.z + fz);
			GL.TexCoord2(0, 0);
			GL.Vertex3(p2.x + fx, p2.y + fy, p2.z + fz);
			GL.TexCoord2(1, 0);
			GL.Vertex3(p3.x + fx, p3.y + fy, p3.z + fz);
			GL.TexCoord2(1, 1);
			GL.Vertex3(p4.x + fx, p4.y + fy, p4.z + fz);
			GL.End();


			GL.Begin(GL.QUADS);
			GL.TexCoord2(0, 1);
			GL.Vertex3(-1.0f, 0.5f, 1.0f);
			GL.TexCoord2(0, 0);
			GL.Vertex3(1.0f, 0.5f, -1.0f);
			GL.TexCoord2(1, 0);
			GL.Vertex3(1.0f, 0.5f, -1.0f);
			GL.TexCoord2(1, 1);
			GL.Vertex3(1.0f, 0.5f, -1.0f);
			GL.End();

			GL.Begin(GL.QUADS);
			GL.TexCoord2(0, 1);
			GL.Vertex3(1.0f, 0.5f, 1.0f);
			GL.TexCoord2(0, 0);
			GL.Vertex3(1.0f, 0.5f, -1.0f);
			GL.TexCoord2(1, 0);
			GL.Vertex3(-1.0f, 0.5f, -1.0f);
			GL.TexCoord2(1, 1);
			GL.Vertex3(1.0f, 0.5f, -1.0f);
			GL.End();


			GL.Begin(GL.QUADS);
			GL.TexCoord2(0, 1);
			GL.Vertex3(-1.0f, 1.0f, 0.5f);
			GL.TexCoord2(0, 0);
			GL.Vertex3(1.0f, -1.0f, 0.5f);
			GL.TexCoord2(1, 0);
			GL.Vertex3(1.0f, -1.0f, 0.5f);
			GL.TexCoord2(1, 1);
			GL.Vertex3(1.0f, -1.0f, 0.5f);
			GL.End();
		}

		GL.PopMatrix();
	}

	private void ChangeScene()
	{
		m_bLoadMarkers++;
		if (m_bLoadMarkers > 4) {
			m_bLoadMarkers = 0;
		}
		//m_bLoadMarkers = !m_bLoadMarkers;
		removePins ();
		/*
		Debug.Log ("ChangeScene");
		Application.LoadLevel ("GuiTests");*/
	}

	private void ClickedLocation()
	{

		// from current map position
		//fromPosition = OnlineMaps.instance.position;

		Vector2 pos;
		pos.y = 48.210033f;
		pos.x = 16.363449f;

		// to GPS position;
		toPosition = pos;//OnlineMaps.instance.GetComponent<OnlineMapsLocationService>().position;
		//toPosition = OnlineMaps.instance.GetComponent<OnlineMapsLocationService>().position;

		// calculates tile positions
	//	Vector2 fromTile = OnlineMapsUtils.LatLongToTilef(fromPosition, OnlineMaps.instance.zoom);
		//Vector2 toTile = OnlineMapsUtils.LatLongToTilef(toPosition, OnlineMaps.instance.zoom);

		// if tile offset < 4, then start smooth movement
		/*if ((fromTile - toTile).magnitude < 4)
		{
			// set relative position 0
			angle = 0;

			// start movement
			isMovement = true;
		}
		else // too far
		{
			OnlineMaps.instance.position = toPosition;
		}*/
		//removePins ();
		loadPins ();
	}

	private void Start()
	{
		Debug.Log ("Start map");
		m_Pins = new FotoQuestPin[1000];
		for (int i = 0; i < 1000; i++) {
			m_Pins[i] = new FotoQuestPin();
		}
		m_NrPins = 0;

		// Subscribe to change position event.
		//OnlineMaps.instance.O
		//OnlineMaps.instance.OnChangePosition += OnChangePosition;
		OnlineMapsControlBase.instance.OnMapRelease += OnChangePosition;
	//	OnlineMapsControlBase.instance.OnMapZoom += OnChangeZoom;
	//	OnlineMaps.instance.OnMapUpdated += OnChangePosition;
//		OnlineMaps.instance.On

		// Subscribe to change zoom event.
		//OnlineMaps.instance.OnChangeZoom += OnChangeZoom;

		addMarkers ();


		// Subscribe to the event of success download tile.
		OnlineMapsTile.OnTileDownloaded += OnTileDownloaded;

		// Intercepts requests to the download of the tile.
		OnlineMaps.instance.OnStartDownloadTile += OnStartDownloadTile;
	//	loadPins ();
	}



		private void OnGUI()
		{
			// On click button, starts movement
			if (GUI.Button(new Rect(5, 5, 100, 30), "Goto marker"))
			{
			ClickedLocation ();
			}

			if (GUI.Button(new Rect(5, 35, 100, 30), "Change scene"))
			{
				ChangeScene ();
			}
		}

		private void Update()
		{
			// if not movement then return
			if (!isMovement) return;

			// update relative position
			angle += Time.deltaTime / time;

			if (angle > 1)
			{
				// stop movement
				isMovement = false;
				angle = 1;
			}

			// Set new position
			OnlineMaps.instance.position = Vector2.Lerp(fromPosition, toPosition, angle);
		}

	private void OnChangePosition()
	{
		// When the position changes you will see in the console new map coordinates.
	//	Debug.Log(OnlineMaps.instance.position);
		Debug.Log ("OnChangePosition");
		loadPins ();
	}

	private void OnChangeZoom()
	{
		// When the zoom changes you will see in the console new zoom.
	//	Debug.Log(OnlineMaps.instance.zoom);
		Debug.Log ("OnChangeZoom");
		loadPins ();
	}


	//---------------------------
	// Cache map

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

	/// <summary>
	/// This method is called when loading the tile.
	/// </summary>
	/// <param name="tile">Reference to tile</param>
	private void OnStartDownloadTile(OnlineMapsTile tile)
	{
		// Get local path.
		string path = GetTilePath(tile);

		// If the tile is cached.
		if (File.Exists(path))
		{
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

	/// <summary>
	/// This method is called when tile is success downloaded.
	/// </summary>
	/// <param name="tile">Reference to tile.</param>
	private void OnTileDownloaded(OnlineMapsTile tile)
	{
		// Get local path.
		string path = GetTilePath(tile);

		// Cache tile.
		FileInfo fileInfo = new FileInfo(path);
		DirectoryInfo directory = fileInfo.Directory;
		if (!directory.Exists) directory.Create();

		File.WriteAllBytes(path, tile.www.bytes);
	}


	}