////////////////////////////////////////////////////////////////////////////////
//  
// @module Android DeepLink Plugin for Unity
// @author CausalLink Assets
// @support causallink.assets@gmail.com
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;
using UnityEngine.UI;

public class main : MonoBehaviour {

    public Text uri_text;
    public Text left_text;
    public Text right_text;
    public Text string_text;
    public Image cube;
	// Use this for initialization






    
    int color_red = 0;
    int color_green = 0;
    int color_blue= 0;

    int intvalue = 0;
    float floatvalue = 0.0f;
    string stringvalue = "";
    int increment_intvalue = 0;
    float increment_floatvalue = 0.0f;

    void OnApplicationPause(bool pauseStatus)
    {

        if (!pauseStatus)
        {
            color_red = AndroidDeepLink.GetValueInInt("r");
            color_green = AndroidDeepLink.GetValueInInt("g");
            color_blue = AndroidDeepLink.GetValueInInt("b");

            intvalue = AndroidDeepLink.GetValueInInt("intval");
            floatvalue = AndroidDeepLink.GetValueInFloat("floatval");

            stringvalue = AndroidDeepLink.GetValueInString("sval");

            increment_intvalue = intvalue;
            increment_floatvalue = floatvalue;

            uri_text.text = AndroidDeepLink.GetURL();
            left_text.text = color_red + "\n" + color_green + "\n" + color_blue;
            right_text.text = intvalue + "             " + increment_intvalue + "\n" + floatvalue + "          " + increment_floatvalue;
            string_text.text = stringvalue;

            cube.color = new Color(color_red / 255.0f, color_green / 255.0f, color_blue / 255.0f);
        }
    }


	void Start () {

        //How uri should be defined
        //myapp://com.deeplink.test?r=128&g=22&b=2&intval=323&floatval=10.234&sval=HelloWorld

        color_red = AndroidDeepLink.GetValueInInt("r");
        color_green = AndroidDeepLink.GetValueInInt("g");
        color_blue = AndroidDeepLink.GetValueInInt("b");

        intvalue = AndroidDeepLink.GetValueInInt("intval");
        floatvalue = AndroidDeepLink.GetValueInFloat("floatval");

        stringvalue = AndroidDeepLink.GetValueInString("sval");
    
        increment_intvalue=intvalue;
        increment_floatvalue = floatvalue;

        uri_text.text = AndroidDeepLink.GetURL();
        left_text.text = color_red + "\n" + color_green + "\n" + color_blue;
        right_text.text = intvalue + "             " + increment_intvalue + "\n" + floatvalue + "          " + increment_floatvalue;
        string_text.text = stringvalue;

        cube.color = new Color(color_red / 255.0f,color_green / 255.0f,color_blue / 255.0f);
	}

    
	void Update () {

        if (Time.frameCount % 20 == 0)
        {
            increment_intvalue += 1;
            
            increment_floatvalue += 0.05f;

            right_text.text = intvalue + "                 " + increment_intvalue + "\n" + floatvalue + "              " + increment_floatvalue;
            left_text.text = color_red + "\n" + color_green + "\n" + color_blue;
        
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}

