using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SensorImagePortrait : MonoBehaviour {
    private WebCamTexture webcamTexture ;
    private RawImage m_RawImage ;


    private Rect rectDefault = new Rect(0f, 0f, 1f, 1f);
    private Rect rectMirrorY = new Rect(0f, 1f, 1f, -1f);

    public Texture2D TakeSnap()
    {
        Texture2D snap = new Texture2D(webcamTexture.width, webcamTexture.height, TextureFormat.ARGB32, true);// false  ) ;
        snap.SetPixels(webcamTexture.GetPixels());
        snap.Apply();

        return snap;
    }

    private void SetTexture ( Texture texture )
    {
        m_RawImage.texture = texture ;
    }

    public void StopSensor()
    {
        Debug.Log("#### SensorImage stop ####");
        SetTexture(Texture2D.blackTexture);
        webcamTexture.Stop();
    }

    // Use this for initialization
    void Start () {
        LaunchSensorImage();
    }

    public void LaunchSensorImage()
    {
        m_RawImage = GetComponent<RawImage>();
        webcamTexture = new WebCamTexture();
        webcamTexture.filterMode = FilterMode.Bilinear;

        SetTexture(webcamTexture);

        GetComponent<RawImage>().color = Color.white;

        GetComponent<RawImage>().uvRect = rectMirrorY;//Application.platform == RuntimePlatform.IPhonePlayer ? rectMirrorY : rectDefault;

        webcamTexture.Play();
    }

    int m_DisabledIter = 0;

    // Update is called once per frame
    private void Update()
    {
        if(webcamTexture.isPlaying == false) {
            m_DisabledIter++;
            if(m_DisabledIter > 50) {
                webcamTexture.Stop();
                webcamTexture = new WebCamTexture();// new WebCamTexture(1280, 720);
                webcamTexture.filterMode = FilterMode.Bilinear;

                SetTexture(webcamTexture);

                GetComponent<RawImage>().color = Color.white;

                GetComponent<RawImage>().uvRect = Application.platform == RuntimePlatform.IPhonePlayer ? rectMirrorY : rectDefault;

                webcamTexture.Play();
                m_DisabledIter = 0;
            }
        }
    }
}
