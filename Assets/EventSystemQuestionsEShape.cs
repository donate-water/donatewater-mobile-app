using System;
using UnityEngine;
using System.Collections;
using Unitycoding.UIWidgets;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class EventSystemQuestionsEShape: MonoBehaviour
{
	public GameObject m_ButtonNext;
	public GameObject m_ButtonNextAbandoned;
	public GameObject m_ButtonBack;

	public MessageBox messageBox;

	
	public GameObject m_TextTitle;
	//public GameObject m_TextDate;
	
	/*
	public GameObject m_ComboCountries;
	public TextAsset m_CountriesText;*/


/*	public GameObject m_CategoryMain;
	public GameObject m_Category1;
	public GameObject m_Category2;
	public GameObject m_Category3;
	public GameObject m_Category4;*/
	public GameObject m_Description;

	//public GameObject m_TextPage;

	public GameObject m_ImageLoading;
	//int m_Page = 0;

	private bool m_bWaterWhyNoWaterOther = false;


	IEnumerator changeFramerate() {
		yield return new WaitForSeconds(1);
		Application.targetFrameRate = 30;
	}


	public void ForcePortrait()
	{
		StartCoroutine(ForceAndFixPortrait());
	}

	public GameObject m_ContentView;

	/*
	public GameObject m_BtnStartDate;
	public GameObject m_BtnEndDate;
	public GameObject m_BtnStartDateSet;
	public GameObject m_BtnEndDateSet;
	public GameObject m_TextStartDate;
	public GameObject m_TextEndDate;
	
	public GameObject m_ComboFrom;
	public GameObject m_ComboTo;
	
	
	public GameObject m_ComboPlantDay;
	public GameObject m_ComboPlantMonth;
	public GameObject m_ComboHarvestDay;
	public GameObject m_ComboHarvestMonth;

	public GameObject m_ClosestVillageName;
	public GameObject m_FieldSize;
	public GameObject m_FieldSizeCombo;
	
	
	public GameObject m_CropCombo;
	public GameObject m_CropOther;
	
	public GameObject m_TargetYield;
	public GameObject m_TargetYieldCombo;
	public GameObject m_TargetYieldOther;
	public GameObject m_ActualYield;
	public GameObject m_ActualYieldCombo;
	public GameObject m_ActualYieldOther;
	
	public GameObject m_Peril1;
	public GameObject m_Peril1Other;
	public GameObject m_Peril1Impact;
	public GameObject m_Peril2;
	public GameObject m_Peril2Other;
	public GameObject m_Peril2Impact;
	public GameObject m_Peril3;
	public GameObject m_Peril3Other;
	public GameObject m_Peril3Impact;
	public GameObject m_Peril4;
	public GameObject m_Peril4Other;
	public GameObject m_Peril4Impact;
	public GameObject m_Peril5;
	public GameObject m_Peril5Other;
	public GameObject m_Peril5Impact;
	


	public GameObject m_Peril1_Other;
	public GameObject m_Peril1Other_Other;
	public GameObject m_Peril2_Other;
	public GameObject m_Peril2Other_Other;
	public GameObject m_Peril3_Other;
	public GameObject m_Peril3Other_Other;
	public GameObject m_Peril4_Other;
	public GameObject m_Peril4Other_Other;
	public GameObject m_Peril5_Other;
	public GameObject m_Peril5Other_Other;
	
	public GameObject m_PerilOtherImpact_1;
	public GameObject m_PerilOtherImpact_2;
	public GameObject m_PerilOtherImpact_3;
	public GameObject m_PerilOtherImpact_4;
	public GameObject m_PerilOtherImpact_5;
	*/

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

	
	public EventSystem m_EventSystem;

	private float m_FillOutStart = 0.0f;
	// Use this for initialization
	void Start ()
	{
		m_FillOutStart = Time.realtimeSinceStartup;
		StartCoroutine(changeFramerate());

		if ((!LocalizationSupport.StringsLoaded))
			LocalizationSupport.LoadStrings();

		ForceAndFixPortrait();


		m_EventSystem.pixelDragThreshold = 30;//50;//50.0f;


		m_ImageLoading.SetActive(true);
		//messageBox = UIUtility.Find<MessageBox> ("MessageBox");

		UpdateText();

		m_ButtonNext.SetActive(false);

		/*m_CategoryMain.SetActive(true);
	    m_Category1.SetActive(false);
		m_Category2.SetActive(false);
		m_Category3.SetActive(false);
		m_Category4.SetActive(false);
		m_Description.SetActive(false);*/

		m_Description.SetActive(true);

	/*	m_InputFieldSize.SetActive(true);
		m_InputFieldSizeOther.SetActive(false);
		m_InputTargetYield.SetActive(true);
		m_InputTargetYieldOther.SetActive(false);
		m_InputActualYield.SetActive(true);
		m_InputActualYieldOther.SetActive(false);*/
		
		//m_TextDate.GetComponent<UnityEngine.UI.Text>().text = "Survey date: " + PlayerPrefs.GetString ("CurQuestObsTime");

		//UpdateCountriesList();


		float posy = PlayerPrefs.GetFloat("ContentViewY");
		//m_ContentView.transform.position = new Vector3(0.0f, posy, 0.0f);
		m_ContentView.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
		
		/*m_BtnStartDate.SetActive(false);
		m_BtnStartDateSet.SetActive(false);
		m_TextStartDate.SetActive(false);
		
		m_BtnEndDate.SetActive(false);
		m_BtnEndDateSet.SetActive(false);
		m_TextEndDate.SetActive(false);*/
		
		m_WaterSourceTypeOther.SetActive(false);
		
		/*
		if (PlayerPrefs.GetInt("Survey_StartDateSet") == 0 )
		{
			m_BtnStartDate.SetActive(true);
			m_BtnStartDateSet.SetActive(false);
			m_TextStartDate.SetActive(false);
		}
		else
		{	
			m_BtnStartDate.SetActive(false);
			m_BtnStartDateSet.SetActive(true);
			m_TextStartDate.SetActive(true);
			m_TextStartDate.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("Survey_StartDate");
		}
	
		if (PlayerPrefs.GetInt("Survey_EndDateSet") == 0)
		{
			m_BtnEndDate.SetActive(true);
			m_BtnEndDateSet.SetActive(false);
			m_TextEndDate.SetActive(false);
		}
		else
		{	
			m_BtnEndDate.SetActive(false);
			m_BtnEndDateSet.SetActive(true);
			m_TextEndDate.SetActive(true);
			m_TextEndDate.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("Survey_EndDate");
		}*/
		
		
		if (PlayerPrefs.HasKey("m_WaterSourceTypeCombo"))
		{
			m_WaterSourceTypeCombo.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceTypeCombo");
		}
		if (PlayerPrefs.HasKey("m_WaterAbandonedCombo"))
		{
			m_WaterAbandonedCombo.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterAbandonedCombo");
		}
		
		if (PlayerPrefs.HasKey("m_WaterSourceQuality"))
		{
			m_WaterSourceQuality.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceQuality");
		}

		
		if (PlayerPrefs.HasKey("m_WaterSourceFunctional"))
		{
			m_WaterSourceFunctional.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceFunctional");
			if(m_WaterSourceFunctional.GetComponent<Dropdown>().value == 0)
				SelectWaterFunctional(0);
		}
		else
		{
			SelectWaterFunctional(0);
		}

		
		if (PlayerPrefs.HasKey("m_WaterSourceNearby"))
		{
			m_WaterSourceNearby.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceNearby");
		}

		
		if (PlayerPrefs.HasKey("m_WaterSourceResponsible"))
		{
			m_WaterSourceResponsible.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceResponsible");
		}

		if (PlayerPrefs.HasKey("m_WaterSourceComboNoWater"))
		{
			m_WaterSourceComboNoWater.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceComboNoWater");
		}
		
		if (PlayerPrefs.HasKey("m_WaterSourceComboDrinkable"))
		{
			m_WaterSourceComboDrinkable.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceComboDrinkable");
		}
		
		if (PlayerPrefs.HasKey("m_WaterSourceComboPublic"))
		{
			m_WaterSourceComboPublic.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceComboPublic");
		}
		
		if (PlayerPrefs.HasKey("m_WaterSourceComboUsed"))
		{
			m_WaterSourceComboUsed.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceComboUsed");
		}
		
		if (PlayerPrefs.HasKey("m_WaterSourceComboWaterTreated"))
		{
			m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceComboWaterTreated");
		}
		
		if (PlayerPrefs.HasKey("m_WaterSourceComboPayment"))
		{
			m_WaterSourceComboPayment.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceComboPayment");
		}

		
		if (PlayerPrefs.HasKey("m_WaterSourceComboDistanceWalked"))
		{
			m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceComboDistanceWalked");
		}
		
		if (PlayerPrefs.HasKey("m_WaterSourceComboQueue"))
		{
			m_WaterSourceComboQueue.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_WaterSourceComboQueue");
		}

		if (PlayerPrefs.HasKey("m_WaterSourceTypeOther"))
		{
			m_WaterSourceTypeOther.GetComponentInChildren<InputField>().text =
				PlayerPrefs.GetString("m_WaterSourceTypeOther");
		}
		if (PlayerPrefs.HasKey("m_WaterSourceInputNoWater"))
		{
			m_WaterSourceInputNoWater.GetComponentInChildren<InputField>().text =
				PlayerPrefs.GetString("m_WaterSourceInputNoWater");
		}
		if (PlayerPrefs.HasKey("m_WaterSourceComboWaterComment"))
		{
			m_WaterSourceComboWaterComment.GetComponentInChildren<InputField>().text =
				PlayerPrefs.GetString("m_WaterSourceComboWaterComment");
		}

		
	/*	m_ClosestVillageName.GetComponentInChildren<InputField>().text = PlayerPrefs.GetString("m_ClosestVillageName");
		
		m_InputFieldSize.GetComponentInChildren<InputField>().text = PlayerPrefs.GetString("m_FieldSize");
		m_InputFieldSizeOther.GetComponentInChildren<InputField>().text = PlayerPrefs.GetString("m_FieldSizeOther");
		if (PlayerPrefs.HasKey("m_FieldSizeType"))
		{
			m_FieldSizeCombo.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_FieldSizeType");
		}
		Debug.Log("m_FieldSizeType: " + PlayerPrefs.GetInt("m_FieldSizeType"));

		
		if (PlayerPrefs.HasKey("m_ComboFrom"))
		{
			m_ComboFrom.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_ComboFrom");
		}
		if (PlayerPrefs.HasKey("m_ComboTo"))
		{
			m_ComboTo.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_ComboTo");
		}
		
		if (PlayerPrefs.HasKey("m_ComboPlantDay"))
		{
			m_ComboPlantDay.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_ComboPlantDay");
		}
		if (PlayerPrefs.HasKey("m_ComboPlantMonth"))
		{
			m_ComboPlantMonth.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_ComboPlantMonth");
		}
		if (PlayerPrefs.HasKey("m_ComboHarvestDay"))
		{
			m_ComboHarvestDay.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_ComboHarvestDay");
		}
		if (PlayerPrefs.HasKey("m_ComboHarvestMonth"))
		{
			m_ComboHarvestMonth.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_ComboHarvestMonth");
		}
		
		if (PlayerPrefs.GetInt("m_Production") == 0)
		{
			m_BtnProduction.SetActive(true);
			m_TextProduction.SetActive(false);
			m_EditProduction.SetActive(false);
		}
		else
		{
			m_BtnProduction.SetActive(false);
			m_TextProduction.SetActive(true);
			m_EditProduction.SetActive(true);

			string strprod = "";
			int iprod = PlayerPrefs.GetInt("m_Production");
			if (iprod == 1)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_1");
			} else if (iprod == 2)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_2");
			}
			else if (iprod == 3)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_4");
			}
			else if (iprod == 5)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_1_1");
			}
			else if (iprod == 6)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_1_2");
			}
			else if (iprod == 7)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_1_4");
			}
			else if (iprod == 8)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_3_1");
			}
			else if (iprod == 9)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_3_2");
			}
			else if (iprod == 10)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_3_3");
			}
			else if (iprod == 11)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_3_4");
			}
			else if (iprod == 12)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_1_3_1");
			}
			else if (iprod == 13)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_1_3_2");
			}
			else if (iprod == 20)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2");
			}
			else if (iprod == 21)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_1");
			}
			else if (iprod == 22)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_3");
			}
			else if (iprod == 23)
			{
				strprod = LocalizationSupport.GetString("Production_A_1_1_2_1_3");
			}
			
			
			m_TextProduction.GetComponent<Text>().text = "Production: " + strprod;
		}

		if (PlayerPrefs.GetInt("m_Agroforestry") == 0)
		{
			m_BtnAgroforestry.SetActive(true);
			m_TextAgroforestry.SetActive(false);
			m_EditAgroforestry.SetActive(false);
		}
		else
		{
			m_BtnAgroforestry.SetActive(false);
			m_TextAgroforestry.SetActive(true);
			m_EditAgroforestry.SetActive(true);

			string strprod = "";
			int iprod = PlayerPrefs.GetInt("m_Agroforestry");

			if (iprod == 1)
			{
				strprod = LocalizationSupport.GetString("NotUsed");
			}
			else if (iprod == 2)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1_2");
			}
			else if (iprod == 3)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1_3");
			}
			else if (iprod == 4)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1_1_1");
			}
			else if (iprod == 5)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1_1_2");
			}
			else if (iprod == 6)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1_1_3");
			}
			else if (iprod == 7)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1_1_4");
			}
			else if (iprod == 8)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1_1_5_1");
			}
			else if (iprod == 9)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1_1_5_2");
			}
			else if (iprod == 20)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1");
			}
			else if (iprod == 21)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1_1");
			}
			else if (iprod == 22)
			{
				strprod = LocalizationSupport.GetString("Agroforestry_1_1_5");
			}
			m_TextAgroforestry.GetComponent<Text>().text = "Agroforestry: " + strprod;
		}
		*/
	m_TextTitle.GetComponent<Text>().text = LocalizationSupport.GetString("SurveyTitle");
		
		/*var dropdown = m_ComboCrops.GetComponent<Dropdown>();
		dropdown.options.Clear();

		string selectItem = "< " + LocalizationSupport.GetString("SelectCrop") + " >";
		dropdown.captionText.text = selectItem;
		dropdown.options.Add(new Dropdown.OptionData(selectItem));
		dropdown.options.Add(new Dropdown.OptionData(LocalizationSupport.GetString("Crop_1")));
		dropdown.options.Add(new Dropdown.OptionData(LocalizationSupport.GetString("Crop_2")));
		dropdown.options.Add(new Dropdown.OptionData(LocalizationSupport.GetString("Crop_3")));
		dropdown.options.Add(new Dropdown.OptionData(LocalizationSupport.GetString("Crop_4")));
		dropdown.options.Add(new Dropdown.OptionData(LocalizationSupport.GetString("Crop_5")));
		dropdown.options.Add(new Dropdown.OptionData(LocalizationSupport.GetString("Crop_6")));

		m_InputOtherCropPlaceholder.GetComponent<Text>().text = LocalizationSupport.GetString("InputOtherCrop");
		m_InputOtherCrop.SetActive(false);
		
		
		if (PlayerPrefs.HasKey("m_CropCombo"))
		{
			m_CropCombo.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_CropCombo");
		}
		if (PlayerPrefs.HasKey("m_CropOther"))
		{
			m_CropOther.GetComponent<InputField>().text = PlayerPrefs.GetString("m_CropOther");
		}
		
		if (PlayerPrefs.HasKey("m_ComboFrom"))
		{
			m_ComboFrom.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_ComboFrom");
		}
		if (PlayerPrefs.HasKey("m_ComboTo"))
		{
			m_ComboTo.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_ComboTo");
		}
		
		
		if (PlayerPrefs.HasKey("m_TargetYield"))
		{
			m_TargetYield.GetComponent<InputField>().text = PlayerPrefs.GetString("m_TargetYield");
		}
		if (PlayerPrefs.HasKey("m_TargetYieldCombo"))
		{
			m_TargetYieldCombo.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_TargetYieldCombo");
		}
		if (PlayerPrefs.HasKey("m_TargetYieldOther"))
		{
			m_TargetYieldOther.GetComponent<InputField>().text = PlayerPrefs.GetString("m_TargetYieldOther");
		}
		
		if (PlayerPrefs.HasKey("m_ActualYield"))
		{
			m_ActualYield.GetComponent<InputField>().text = PlayerPrefs.GetString("m_ActualYield");
		}
		if (PlayerPrefs.HasKey("m_ActualYieldCombo"))
		{
			m_ActualYieldCombo.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_ActualYieldCombo");
		}
		if (PlayerPrefs.HasKey("m_ActualYieldOther"))
		{
			m_ActualYieldOther.GetComponent<InputField>().text = PlayerPrefs.GetString("m_ActualYieldOther");
		}
		
		
		
		m_Peril1Other.SetActive(false);
		m_Peril2Other.SetActive(false);
		m_Peril3Other.SetActive(false);
		m_Peril4Other.SetActive(false);
		m_Peril5Other.SetActive(false);
		
		m_Peril1Other_Other.SetActive(false);
		m_Peril2Other_Other.SetActive(false);
		m_Peril3Other_Other.SetActive(false);
		m_Peril4Other_Other.SetActive(false);
		m_Peril5Other_Other.SetActive(false);
		
		if (PlayerPrefs.HasKey("m_Peril1"))
		{
			m_Peril1.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril1");
		}
		if (PlayerPrefs.HasKey("m_Peril2"))
		{
			m_Peril2.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril2");
		}
		if (PlayerPrefs.HasKey("m_Peril3"))
		{
			m_Peril3.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril3");
		}
		if (PlayerPrefs.HasKey("m_Peril4"))
		{
			m_Peril4.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril4");
		}
		if (PlayerPrefs.HasKey("m_Peril5"))
		{
			m_Peril5.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril5");
		}
		
		if (PlayerPrefs.HasKey("m_Peril1Other"))
		{
			m_Peril1Other.GetComponent<InputField>().text = PlayerPrefs.GetString("m_Peril1Other");
		}
		if (PlayerPrefs.HasKey("m_Peril2Other"))
		{
			m_Peril2Other.GetComponent<InputField>().text = PlayerPrefs.GetString("m_Peril2Other");
		}
		if (PlayerPrefs.HasKey("m_Peril3Other"))
		{
			m_Peril3Other.GetComponent<InputField>().text = PlayerPrefs.GetString("m_Peril3Other");
		}
		if (PlayerPrefs.HasKey("m_Peril4Other"))
		{
			m_Peril4Other.GetComponent<InputField>().text = PlayerPrefs.GetString("m_Peril4Other");
		}
		if (PlayerPrefs.HasKey("m_Peril5Other"))
		{
			m_Peril5Other.GetComponent<InputField>().text = PlayerPrefs.GetString("m_Peril5Other");
		}
		
		if (PlayerPrefs.HasKey("m_Peril1Impact"))
		{
			m_Peril1Impact.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril1Impact");
		}
		if (PlayerPrefs.HasKey("m_Peril2Impact"))
		{
			m_Peril2Impact.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril2Impact");
		}
		if (PlayerPrefs.HasKey("m_Peril3Impact"))
		{
			m_Peril3Impact.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril3Impact");
		}
		if (PlayerPrefs.HasKey("m_Peril4Impact"))
		{
			m_Peril4Impact.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril4Impact");
		}
		if (PlayerPrefs.HasKey("m_Peril5Impact"))
		{
			m_Peril5Impact.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril5Impact");
		}
		
		if (PlayerPrefs.HasKey("m_Peril1_Other"))
		{
			m_Peril1_Other.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril1_Other");
		}
		if (PlayerPrefs.HasKey("m_Peril2_Other"))
		{
			m_Peril2_Other.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril2_Other");
		}
		if (PlayerPrefs.HasKey("m_Peril3_Other"))
		{
			m_Peril3_Other.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril3_Other");
		}
		if (PlayerPrefs.HasKey("m_Peril4_Other"))
		{
			m_Peril4_Other.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril4_Other");
		}
		if (PlayerPrefs.HasKey("m_Peril5_Other"))
		{
			m_Peril5_Other.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_Peril5_Other");
		}
		
		if (PlayerPrefs.HasKey("m_Peril1Other_Other"))
		{
			m_Peril1Other_Other.GetComponent<InputField>().text = PlayerPrefs.GetString("m_Peril1Other_Other");
		}
		if (PlayerPrefs.HasKey("m_Peril2Other_Other"))
		{
			m_Peril2Other_Other.GetComponent<InputField>().text = PlayerPrefs.GetString("m_Peril2Other_Other");
		}
		if (PlayerPrefs.HasKey("m_Peril3Other_Other"))
		{
			m_Peril3Other_Other.GetComponent<InputField>().text = PlayerPrefs.GetString("m_Peril3Other_Other");
		}
		if (PlayerPrefs.HasKey("m_Peril4Other_Other"))
		{
			m_Peril4Other_Other.GetComponent<InputField>().text = PlayerPrefs.GetString("m_Peril4Other_Other");
		}
		if (PlayerPrefs.HasKey("m_Peril5Other_Other"))
		{
			m_Peril5Other_Other.GetComponent<InputField>().text = PlayerPrefs.GetString("m_Peril5Other_Other");
		}
		
		if (PlayerPrefs.HasKey("m_PerilOtherImpact_1"))
		{
			m_PerilOtherImpact_1.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_PerilOtherImpact_1");
		}
		if (PlayerPrefs.HasKey("m_PerilOtherImpact_2"))
		{
			m_PerilOtherImpact_2.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_PerilOtherImpact_2");
		}
		if (PlayerPrefs.HasKey("m_PerilOtherImpact_3"))
		{
			m_PerilOtherImpact_3.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_PerilOtherImpact_3");
		}
		if (PlayerPrefs.HasKey("m_PerilOtherImpact_4"))
		{
			m_PerilOtherImpact_4.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_PerilOtherImpact_4");
		}
		if (PlayerPrefs.HasKey("m_PerilOtherImpact_5"))
		{
			m_PerilOtherImpact_5.GetComponent<Dropdown>().value = PlayerPrefs.GetInt("m_PerilOtherImpact_5");
		}
		
		if (PlayerPrefs.GetInt("CameFromFromCamera") == 1)
		{
		
		}*/
	}




	private bool m_bSetScrollPosition = true;
	private int m_ScrollPositionIter = 0;

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			OnBackClicked ();
		}

		if (m_bSetScrollPosition)
		{
			m_ScrollPositionIter++;
			if (m_ScrollPositionIter > 1)
			{
				float posy = PlayerPrefs.GetFloat("ContentViewY");
				m_ContentView.transform.position = new Vector3(0.0f, posy, 0.0f);
				m_bSetScrollPosition = false;
				m_ImageLoading.SetActive(false);
			}
		}
	}

	public GameObject m_Question1;
	
	
	public GameObject m_BtnProduction;
	public GameObject m_TextProduction;
	public GameObject m_EditProduction;
	
	public GameObject m_BtnAgroforestry;
	public GameObject m_TextAgroforestry;
	public GameObject m_EditAgroforestry;
	
    void UpdateText()
    {
		m_Question1.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyQuestion1");

		m_ButtonBack.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("Back");

		/*m_SurveyTitle.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyTitle");
		m_TextCropType.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyCropType");

		m_BtnSelectCropType1.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveySelectCropType");
		m_BtnSelectCropType2.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyAddAnotherCrop");
		m_BtnSelectCropType3.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyAddAnotherCrop");
		m_BtnSelectCropType4.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyAddAnotherCrop");


		m_PlantHeightTitle.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("PlantHeight");
		m_PlantHeightPlaceholder.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("PlantHeightInput");

    	  m_PhenTitle.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen");
		m_Phen1.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen1");
		m_Phen2.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen2");
		m_Phen3.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen3");
		m_Phen4.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen4");
		m_Phen5.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen5");
		m_Phen6.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen6");
		m_Phen7.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen7");
		m_Phen8.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen8");
		m_Phen9.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen9");
		m_Phen10.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen10");
		m_Phen11.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen11");
		m_Phen12.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen12");
		m_Phen13.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhen13");
		m_PhenPlaceholder.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyPhenEnterOther");


		m_DamageTitle.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyDamage");
		m_Damage1.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyDamage1");
		m_Damage2.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyDamage2");
		m_Damage3.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyDamage3");
		m_Damage4.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyDamage4");
		m_Damage5.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyDamage5");
		m_Damage6.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyDamage6");
		m_Damage7.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyDamage7");
		m_DamagePlaceholder.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyDamageEnterOther");


		m_ManTitle.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyManagement");
		m_Man1.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyManagement1");
		m_Man2.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyManagement2");
		m_Man3.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyManagement3");
		m_Man4.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyManagement4");
		m_Man5.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyManagement5");
		m_Man6.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyManagement6");
		m_Man7.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyManagement7");
		m_Man8.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyManagement8");
		m_ManPlaceholder.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyManagementEnterOther");

		m_AdditionalComments.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyAdditionalComments");
		m_InputRemarksPlaceholder.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyAdditionalCommentsEnter");
		*/
		int m_NrQuestsDone = 0;
		if (PlayerPrefs.HasKey("NrQuestsDone"))
		{
			m_NrQuestsDone = PlayerPrefs.GetInt("NrQuestsDone");
		}
		else
		{
			m_NrQuestsDone = 0;
		}

		m_NrQuestsDone++;
		/*m_SurveyNumber.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("SurveyNumber") + " " + m_NrQuestsDone;
		m_TextFieldSize.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("FieldSize");
		m_PlaceholderFieldSize.GetComponentInChildren<UnityEngine.UI.Text>().text = LocalizationSupport.GetString("FieldSizePlaceholder");
*/

		/*if(m_Page >= 2)
        {
			m_Question1.SetActive(false);
        } else
        {
			m_Question1.SetActive(true);
        }
		m_TextPage.GetComponentInChildren<UnityEngine.UI.Text>().text = m_Page + 1 + " / " + 4;*/

    }

	public void NextClicked () {
		
	}

	//bool m_bInMain = true;
	public void OnBackClicked()
	{
		/*if (!m_bInMain)
		{
			m_CategoryMain.SetActive(true);
			m_Category1.SetActive(false);
			m_Category2.SetActive(false);
			m_Category3.SetActive(false);
			m_Category4.SetActive(false);
			m_Description.SetActive(false);
			m_bInMain = true;
			m_Page = 0;
			UpdateText();
		}
		else if (m_Page > 0)
		{
			m_CategoryMain.SetActive(true);
			m_Category1.SetActive(false);
			m_Category2.SetActive(false);
			m_Category3.SetActive(false);
			m_Category4.SetActive(false);
			m_Description.SetActive(false);
			m_bInMain = true;
			m_Page = 0;
			UpdateText();
		}
		else

		{*/
		//Application.LoadLevel("DemoMap");
		Application.LoadLevel("MultiplePhotos");
		//}
	}

	public GameObject m_WaterAbandonedQuestion;
	public GameObject m_WaterAbandonedCombo;
	public GameObject m_WaterAbandonedOther;

	public void SelectWaterAbandoned(int value)
	{
		Debug.Log("SelectWaterAbandoned: " + value);
		if (value == 9)
		{
			m_WaterAbandonedOther.SetActive(true);
		}
		else
		{
			m_WaterAbandonedOther.SetActive(false);
		}
	}
	public void SelectWaterTreated(int value)
	{
		Debug.Log("SelectWaterTreated: " + value);
		if (value == 9)
		{
			m_WaterSourceTreatedOther.SetActive(true);
		}
		else
		{
			m_WaterSourceTreatedOther.SetActive(false);
		}
	}

	public GameObject m_WaterSourceTypeCombo;
	public GameObject m_WaterSourceTypeOther;
	public void SelectWaterSourceType(int value)
	{
		Debug.Log("Water source type: " + value);
		if (value == 12)
		{
			m_WaterSourceTypeOther.SetActive(true);
		}
		else
		{
			m_WaterSourceTypeOther.SetActive(false);
		}
/*
		string data = "";
		if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 1 + ","; // Stream
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 7+ ",";//Spring
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 8+ ","; // Hand-dug well
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 4)
		{
			data += "" + 9+ ",";//Borehole with handpump
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 5)
		{
			data += "" + 10+ ",";//Rainwater
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 6)
		{
			data += "" + 11+ ",";//Piped-borne water
		}  else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 7)
		{
			data += "" + 12+ ",";//Water vendors
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 8)
		{
			data += "" + 2+ ",";//River
		}  else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 9)
		{
			data += "" + 5+ ",";//Ground hole
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 10)
		{
			data += "" + 13 + ",";//Lagoon
		} 
		else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 11)
		{
			data += "" + 6 + ",";
			data += "\"waterTypeOther\":\"" + m_WaterSourceTypeOther.GetComponentInChildren<InputField>().text;
			data += "\",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		Debug.Log("data: " + data);*/
	}

 
	public void SelectWaterFunctional(int value)
	{
		Debug.Log("Water functional: " + value);
		if (value == 3 || value == 4)
		{
			m_WaterAbandonedQuestion.SetActive(true);
			m_WaterAbandonedCombo.SetActive(true);
			
			if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 5)
				m_WaterAbandonedOther.SetActive(true);
			else
				m_WaterAbandonedOther.SetActive(false);
			
			
			m_WaterSourceTextDrinkable.SetActive(false);
			m_WaterSourceComboDrinkable.SetActive(false);
			
			m_WaterSourceComboUsed.SetActive(false);
			m_WaterSourceUsedText.SetActive(false);
			
			m_WaterSourceComboWaterTreated.SetActive(false);
			m_WaterSourceWaterTreatedText.SetActive(false);
			m_WaterSourceTreatedOther.SetActive(false);
			
			m_WaterSourceComboPayment.SetActive(false);
			m_WaterSourcePaymentText.SetActive(false);
			
			
			m_WaterSourceResponsible.SetActive(false);
			m_WaterSourceResponsibleText.SetActive(false);
			
			m_WaterSourceComboDistanceWalked.SetActive(false);
			m_WaterSourceDistanceWalkedText.SetActive(false);
			
			m_WaterSourceComboQueue.SetActive(false);
			m_WaterSourceQueueText.SetActive(false);
			
			m_WaterSourceQuality.SetActive(false);
			m_WaterSourceQualityText.SetActive(false);
			
			m_WaterSourceComboWaterComment.SetActive(false);
			m_WaterSourceWaterCommentText.SetActive(false);
			
			m_ButtonNextAbandoned.SetActive(true);
			m_ButtonNext.SetActive(false);
		}
		else
		{
			m_WaterAbandonedQuestion.SetActive(false);
			m_WaterAbandonedCombo.SetActive(false);
			m_WaterAbandonedOther.SetActive(false);
			
			m_WaterSourceTextDrinkable.SetActive(true);
			m_WaterSourceComboDrinkable.SetActive(true);
			
			m_WaterSourceComboUsed.SetActive(true);
			m_WaterSourceUsedText.SetActive(true);
			
			m_WaterSourceComboWaterTreated.SetActive(true);
			m_WaterSourceWaterTreatedText.SetActive(true);
			
			if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 9)
				m_WaterSourceTreatedOther.SetActive(true);
			else
				m_WaterSourceTreatedOther.SetActive(false);
			
			m_WaterSourceComboPayment.SetActive(true);
			m_WaterSourcePaymentText.SetActive(true);
			
			m_WaterSourceResponsible.SetActive(true);
			m_WaterSourceResponsibleText.SetActive(true);
			
			m_WaterSourceComboDistanceWalked.SetActive(true);
			m_WaterSourceDistanceWalkedText.SetActive(true);
			
			m_WaterSourceQuality.SetActive(true);
			m_WaterSourceQualityText.SetActive(true);
			
			m_WaterSourceComboWaterComment.SetActive(true);
			m_WaterSourceWaterCommentText.SetActive(true);
			
			
			m_WaterSourceComboQueue.SetActive(true);
			m_WaterSourceQueueText.SetActive(true);
			
			m_ButtonNextAbandoned.SetActive(false);
			m_ButtonNext.SetActive(true);
		}
	}

	public GameObject m_WaterSourceFunctional;
	public GameObject m_WaterSourceNearby;
	public GameObject m_WaterSourceResponsible;
	public GameObject m_WaterSourceResponsibleText;
	
	public GameObject m_WaterSourceQuality;
	public GameObject m_WaterSourceQualityText;
	public void SelectWaterSourceQuality(int value)
	{
		Debug.Log("Water quality type: " + value);
		/*if (value == 5 && false)
		{
			m_WaterSourceTextNoWater.SetActive(true);
			m_WaterSourceComboNoWater.SetActive(true);
			if(m_bWaterWhyNoWaterOther)
				m_WaterSourceInputNoWater.SetActive(true);
			else
				m_WaterSourceInputNoWater.SetActive(false);
			
			m_WaterSourceTextDrinkable.SetActive(false);
			m_WaterSourceComboDrinkable.SetActive(false);
		}
		else
		{	
			m_WaterSourceTextNoWater.SetActive(false);
			m_WaterSourceComboNoWater.SetActive(false);
			m_WaterSourceInputNoWater.SetActive(false);
			
			m_WaterSourceTextDrinkable.SetActive(true);
			m_WaterSourceComboDrinkable.SetActive(true);
		}*/
	}
	
	public GameObject m_WaterSourceTextNoWater;
	public GameObject m_WaterSourceComboNoWater;
	public GameObject m_WaterSourceInputNoWater;

	public void SelectNoWaterAvailable(int value)
	{
		Debug.Log("SelectNoWaterAvailable: " + value);

		/*if (value == 3)
		{
			m_WaterSourceInputNoWater.SetActive(true);
			m_bWaterWhyNoWaterOther = true;
		}
		else
		{*/
			m_WaterSourceInputNoWater.SetActive(false);
			m_bWaterWhyNoWaterOther = false;
		//}
	}


	public GameObject m_WaterSourceTextDrinkable;
	public GameObject m_WaterSourceComboDrinkable;
	
	
	public GameObject m_WaterSourceComboPublic;
	
	public GameObject m_WaterSourceComboUsed;
	public GameObject m_WaterSourceUsedText;
	public GameObject m_WaterSourceComboWaterTreated;
	public GameObject m_WaterSourceTreatedOther;
	public GameObject m_WaterSourceWaterTreatedText;
	public GameObject m_WaterSourceComboPayment;
	public GameObject m_WaterSourcePaymentText;
	public GameObject m_WaterSourceComboDistanceWalked;
	public GameObject m_WaterSourceDistanceWalkedText;
	public GameObject m_WaterSourceComboWaterComment;
	public GameObject m_WaterSourceWaterCommentText;
	
	public GameObject m_WaterSourceComboQueue;
	public GameObject m_WaterSourceQueueText;
	
/*
	public GameObject m_SurveyNumber;
	public GameObject m_TextFieldSize;
	public GameObject m_InputFieldSize;
	public GameObject m_InputFieldSizeOther;
	public GameObject m_PlaceholderFieldSize;
	

	public void ChangeFieldSize(int value)
	{
		if (value == 3)
		{
			m_InputFieldSize.SetActive(false);
			m_InputFieldSizeOther.SetActive(true);
		}
		else
		{
			m_InputFieldSize.SetActive(true);
			m_InputFieldSizeOther.SetActive(false);
		}
	}
	
	
	public GameObject m_InputTargetYield;
	public GameObject m_InputTargetYieldOther;
	public GameObject m_InputActualYield;
	public GameObject m_InputActualYieldOther;
	
	public void ChangeTargetYield(int value)
	{
		if (value == 2)
		{
			m_InputTargetYield.SetActive(false);
			m_InputTargetYieldOther.SetActive(true);
		}
		else
		{
			m_InputTargetYield.SetActive(true);
			m_InputTargetYieldOther.SetActive(false);
		}
	}
	public void ChangeActualYield(int value)
	{
		if (value == 2)
		{
			m_InputActualYield.SetActive(false);
			m_InputActualYieldOther.SetActive(true);
		}
		else
		{
			m_InputActualYield.SetActive(true);
			m_InputActualYieldOther.SetActive(false);
		}
	}
	
	
	public GameObject m_ComboCrops;
	public GameObject m_InputOtherCrop;
	public GameObject m_InputOtherCropPlaceholder;
	public void ChangeCropType(int value)
	{
		if (value == 6)
		{
			m_InputOtherCrop.SetActive(true);
		}
		else
		{
			m_InputOtherCrop.SetActive(false);
		}
	}
	
	
	public GameObject m_BtnPlantDate;
	public GameObject m_TextPlantDate;
	public GameObject m_EditPlantDate;
	
	public GameObject m_BtnHarvestDate;
	public GameObject m_TextHarvestDate;
	public GameObject m_EditHarvestDate;
	
	
	public GameObject m_Question6Sum;

	public GameObject m_ComboPeril1Impact;
	public GameObject m_ComboPeril2Impact;
	public GameObject m_ComboPeril3Impact;
	public GameObject m_ComboPeril4Impact;
	public GameObject m_ComboPeril5Impact;

	public void OnPeril1Selected(int value)
	{
		Debug.Log("Peril1 selected: " + value);
		if (value == 10)
		{
			m_Peril1Other.SetActive(true);
		}
		else
		{
			m_Peril1Other.SetActive(false);
		}
	}
	public void OnPeril2Selected(int value)
	{
		Debug.Log("Peril2 selected: " + value);
		if (value == 10)
		{
			m_Peril2Other.SetActive(true);
		}
		else
		{
			m_Peril2Other.SetActive(false);
		}
	}
	public void OnPeril3Selected(int value)
	{
		Debug.Log("Peril3 selected: " + value);
		if (value == 10)
		{
			m_Peril3Other.SetActive(true);
		}
		else
		{
			m_Peril3Other.SetActive(false);
		}
	}
	public void OnPeril4Selected(int value)
	{
		Debug.Log("Peril4 selected: " + value);
		if (value == 10)
		{
			m_Peril4Other.SetActive(true);
		}
		else
		{
			m_Peril4Other.SetActive(false);
		}
	}
	public void OnPeril5Selected(int value)
	{
		Debug.Log("Peril5 selected: " + value);
		if (value == 10)
		{
			m_Peril5Other.SetActive(true);
		}
		else
		{
			m_Peril5Other.SetActive(false);
		}
	}
	
	
	
	public void OnPeril1Selected_Other(int value)
	{
		Debug.Log("Peril1_Other selected: " + value);
		if (value == 10)
		{
			m_Peril1Other_Other.SetActive(true);
		}
		else
		{
			m_Peril1Other_Other.SetActive(false);
		}
	}
	public void OnPeril2Selected_Other(int value)
	{
		Debug.Log("Peril2_Other selected: " + value);
		if (value == 10)
		{
			m_Peril2Other_Other.SetActive(true);
		}
		else
		{
			m_Peril2Other_Other.SetActive(false);
		}
	}
	public void OnPeril3Selected_Other(int value)
	{
		Debug.Log("Peril3_Other selected: " + value);
		if (value == 10)
		{
			m_Peril3Other_Other.SetActive(true);
		}
		else
		{
			m_Peril3Other_Other.SetActive(false);
		}
	}
	public void OnPeril4Selected_Other(int value)
	{
		Debug.Log("Peril4_Other selected: " + value);
		if (value == 10)
		{
			m_Peril4Other_Other.SetActive(true);
		}
		else
		{
			m_Peril4Other_Other.SetActive(false);
		}
	}
	public void OnPeril5Selected_Other(int value)
	{
		Debug.Log("Peril5 selected: " + value);
		if (value == 10)
		{
			m_Peril5Other_Other.SetActive(true);
		}
		else
		{
			m_Peril5Other_Other.SetActive(false);
		}
	}
	

	public void OnPerilImpactSelected(int value)
	{
		Debug.Log("OnPerilImpactSelected: " + value);
		Dropdown d1 = m_ComboPeril1Impact.GetComponent<Dropdown>();
		Dropdown d2 = m_ComboPeril2Impact.GetComponent<Dropdown>();
		Dropdown d3 = m_ComboPeril3Impact.GetComponent<Dropdown>();
		Dropdown d4 = m_ComboPeril4Impact.GetComponent<Dropdown>();
		Dropdown d5 = m_ComboPeril5Impact.GetComponent<Dropdown>();

		float sum = 0.0f;
		if (d1.value == 1)
		{
			sum += 20.0f;
		}
		if (d1.value == 2)
		{
			sum += 40.0f;
		}
		if (d1.value == 3)
		{
			sum += 60.0f;
		}
		if (d1.value == 4)
		{
			sum += 80.0f;
		}
		if (d1.value == 5)
		{
			sum += 100.0f;
		}
		
		if (d2.value == 1)
		{
			sum += 20.0f;
		}
		if (d2.value == 2)
		{
			sum += 40.0f;
		}
		if (d2.value == 3)
		{
			sum += 60.0f;
		}
		if (d2.value == 4)
		{
			sum += 80.0f;
		}
		if (d2.value == 5)
		{
			sum += 100.0f;
		}
		
		if (d3.value == 1)
		{
			sum += 20.0f;
		}
		if (d3.value == 2)
		{
			sum += 40.0f;
		}
		if (d3.value == 3)
		{
			sum += 60.0f;
		}
		if (d3.value == 4)
		{
			sum += 80.0f;
		}
		if (d3.value == 5)
		{
			sum += 100.0f;
		}
		
		
		if (d4.value == 1)
		{
			sum += 20.0f;
		}
		if (d4.value == 2)
		{
			sum += 40.0f;
		}
		if (d4.value == 3)
		{
			sum += 60.0f;
		}
		if (d4.value == 4)
		{
			sum += 80.0f;
		}
		if (d4.value == 5)
		{
			sum += 100.0f;
		}
		
		
		if (d5.value == 1)
		{
			sum += 20.0f;
		}
		if (d5.value == 2)
		{
			sum += 40.0f;
		}
		if (d5.value == 3)
		{
			sum += 60.0f;
		}
		if (d5.value == 4)
		{
			sum += 80.0f;
		}
		if (d5.value == 5)
		{
			sum += 100.0f;
		}
		
		m_Question6Sum.GetComponent<Text>().text = "Sum: " + sum + "%";
	}

	*/

	void SaveData()
    {
		int m_NrQuestsDone = 0;
		if (PlayerPrefs.HasKey("NrQuestsDone"))
		{
			m_NrQuestsDone = PlayerPrefs.GetInt("NrQuestsDone");
		}
		else
		{
			m_NrQuestsDone = 0;
		}

		//PlayerPrefs.SetString("m_InputRemarks", m_InputRemarks.text);


		float lat = Input.location.lastData.latitude;
		float lng = Input.location.lastData.longitude;


		
		Debug.Log("Save data 1");
		
		string data = "{";

		
		data += "\"userPositionSurveyCreatedLat\":" + lat;
		data += ",";
		data += "\"userPositionSurveyCreatedLng\":" + lng;
		data += ",";

		data += "\"userPositionSurveyStartedLat\":" + PlayerPrefs.GetFloat("CurQuestEndPositionX");
		data += ",";
		data += "\"userPositionSurveyStartedLng\":" + PlayerPrefs.GetFloat("CurQuestEndPositionY");
		data += ",";
		
		
		data += "\"waterSourceLocation\":\"" + PlayerPrefs.GetFloat("CurCropPositionX") + ";" + PlayerPrefs.GetFloat("CurCropPositionY");
		data += "\",";

		data += "\"volunteerLocation\":\"" + PlayerPrefs.GetFloat("CurQuestEndPositionX") + ";" + PlayerPrefs.GetFloat("CurQuestEndPositionY");
		data += "\",";
		
		
		
			
		/*
		string userdate = PlayerPrefs.GetString ("CurQuestObsTime");
		//userdate += " 00:00:00.000000";
		//data += ",";
		data += "\"dateObservation\":" + "\"" + userdate + "\"";*/
		
		string datesurveycreation = System.DateTime.Now.ToString ("yyyy-MM-dd hh:mm:ss");
		datesurveycreation += ".000000";
		data += "\"dateSurveyCreation\":" + "\"" + datesurveycreation + "\"";
		data += ",";

		data += "\"waterType\":";
		/*if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 1 + ","; // Stream
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 7+ ",";//Spring
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 8+ ","; // Hand-dug well
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 4)
		{
			data += "" + 9+ ",";//Borehole with handpump
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 5)
		{
			data += "" + 10+ ",";//Rainwater
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 6)
		{
			data += "" + 11+ ",";//Piped-borne water
		}  else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 7)
		{
			data += "" + 12+ ",";//Water vendors
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 8)
		{
			data += "" + 2+ ",";//River
		}  else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 9)
		{
			data += "" + 5+ ",";//Ground hole
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 10)
		{
			data += "" + 13 + ",";//Lagoon
		} 
		else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 11)
		{
			data += "" + 6 + ",";
			data += "\"waterTypeOther\":\"" + m_WaterSourceTypeOther.GetComponentInChildren<InputField>().text;
			data += "\",";
		}
		else
		{
			data += "" + 0 + ",";
		}*/
		if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 1 + ","; // Stream
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 7+ ",";//Spring
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 8+ ","; // Hand-dug well
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 4)
		{
			data += "" + 9+ ",";//Borehole with handpump
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 5)
		{
			data += "" + 10+ ",";//Rainwater
		}
		/* else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 6)
		{
			data += "" + 11+ ",";//Piped-borne water
		} */
		 else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 6)
		{
			data += "" + 12+ ",";//Water vendors
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 7)
		{
			data += "" + 2+ ",";//River
		}  else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 8)
		{
			data += "" + 5+ ",";//Ground hole
		} else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 9)
		{
			data += "" + 13 + ",";//Lagoon
		}  
		else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 10)
		{
			data += "" + 14 + ",";//Borehole with tap
		}  
		else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 11)
		{
			data += "" + 15 + ",";//Solar-powered borehole
		} 

		else if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 12)
		{
			data += "" + 6 + ",";
			data += "\"waterTypeOther\":\"" + m_WaterSourceTypeOther.GetComponentInChildren<InputField>().text;
			data += "\",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		
		data += "\"waterSourceFunc\":";
		if (m_WaterSourceFunctional.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceFunctional.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceFunctional.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else if (m_WaterSourceFunctional.GetComponent<Dropdown>().value == 4)
		{
			data += "" + 3 + ",";
		}
		else if (m_WaterSourceFunctional.GetComponent<Dropdown>().value == 5)
		{
			data += "" + 4 + ",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		
		/*if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 1 + ","; // Stream
		} else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 2+ ",";//Spring
		} else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 3+ ",";//Spring
		} else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 4)
		{
			data += "" + 4+ ",";//Spring
		} 
		else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 5)
		{
			data += "" + 5 + ",";
			data += "\"whySourceAbandonedOther\":\"" + m_WaterAbandonedOther.GetComponentInChildren<InputField>().text;
			data += "\",";
		}
		else
		{
			data += "" + 0 + ",";
		}*/

		bool bAbandoned = false;
		if (m_WaterSourceFunctional.GetComponent<Dropdown>().value == 3 ||
		    m_WaterSourceFunctional.GetComponent<Dropdown>().value == 4)
		{
			bAbandoned = true;
		}

		if(bAbandoned)
		{
			data += "\"whySourceAbandoned\":";
			if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 1)
			{
				data += "" + 10 + ","; // Broken parts
			} else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 2)
			{
				data += "" + 11+ ",";//Spring
			} else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 3)
			{
				data += "" + 12+ ",";//Spring
			} else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 4)
			{
				data += "" + 13+ ",";//Spring
			} 
			else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 5)
			{
				data += "" + 14+ ",";//Spring
			} else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 6)
			{
				data += "" + 15+ ",";//Spring
			} else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 7)
			{
				data += "" + 16+ ",";//Spring
			} else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 8)
			{
				data += "" + 17+ ",";//Spring
			} else if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 9)
			{
				data += "" + 18 + ",";
				data += "\"whySourceAbandonedOther\":\"" + m_WaterAbandonedOther.GetComponentInChildren<InputField>().text;
				data += "\",";
			}
			else
			{
				data += "" + 0 + ",";
			}
		} 

		
		
		data += "\"waterSourceNearby\":";
		if (m_WaterSourceNearby.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceNearby.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceNearby.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		data += "\"waterState\":";
		if (m_WaterSourceQuality.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceQuality.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceQuality.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else if (m_WaterSourceQuality.GetComponent<Dropdown>().value == 4)
		{
			data += "" + 3 + ",";
		}
		else if (m_WaterSourceQuality.GetComponent<Dropdown>().value == 5)
		{
			data += "" + 4 + ",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		data += "\"waterDrinkable\":";
		if (m_WaterSourceComboDrinkable.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceComboDrinkable.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceComboDrinkable.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		data += "\"waterPublic\":";
		if (m_WaterSourceComboPublic.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceComboPublic.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceComboPublic.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		
		data += "\"waterUsed\":";
		if (m_WaterSourceComboUsed.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceComboUsed.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceComboUsed.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		data += "\"waterTreated\":";
		if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 4)
		{
			data += "" + 3 + ",";
		}
		else if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 5)
		{
			data += "" + 4 + ",";
		}
		else if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 6)
		{
			data += "" + 5 + ",";
		}
		else if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 7)
		{
			data += "" + 6 + ",";
		}
		else if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 8)
		{
			data += "" + 7 + ",";
		}
		else if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 9)
		{
			data += "" + 8 + ",";
			data += "\"waterTreatedOther\":\"" + m_WaterSourceTreatedOther.GetComponentInChildren<InputField>().text;
			data += "\",";
		}
		else
		{
			data += "" + 0 + ",";
		}

		
		data += "\"responsibleForFetch\":";
		if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 4)
		{
			data += "" + 3 + ",";
		}
		else if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 5)
		{
			data += "" + 4 + ",";
		}
		else if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 6)
		{
			data += "" + 5 + ",";
		}
		else if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 7)
		{
			data += "" + 6 + ",";
		}
		else if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 8)
		{
			data += "" + 7 + ",";
		}
		else if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 9)
		{
			data += "" + 8 + ",";
		}
		else if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 10)
		{
			data += "" + 9 + ",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		data += "\"waterFree\":";
		if (m_WaterSourceComboPayment.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceComboPayment.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceComboPayment.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		data += "\"peopleTravelToWaterSource\":";
		if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 4)
		{
			data += "" + 3 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 5)
		{
			data += "" + 4 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 6)
		{
			data += "" + 5 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 7)
		{
			data += "" + 6 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 8)
		{
			data += "" + 7 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 9)
		{
			data += "" + 8 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 10)
		{
			data += "" + 9 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 11)
		{
			data += "" + 10 + ",";
		}
		else if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 12)
		{
			data += "" + 11 + ",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		
		data += "\"doYouQueue\":";
		if (m_WaterSourceComboQueue.GetComponent<Dropdown>().value == 1)
		{
			data += "" + 0 + ",";
		}
		else if (m_WaterSourceComboQueue.GetComponent<Dropdown>().value == 2)
		{
			data += "" + 1 + ",";
		}
		else if (m_WaterSourceComboQueue.GetComponent<Dropdown>().value == 3)
		{
			data += "" + 2 + ",";
		}
		else if (m_WaterSourceComboQueue.GetComponent<Dropdown>().value == 4)
		{
			data += "" + 3 + ",";
		}
		else if (m_WaterSourceComboQueue.GetComponent<Dropdown>().value == 5)
		{
			data += "" + 4 + ",";
		}
		else
		{
			data += "" + 0 + ",";
		}
		
		
		float timeNeeded = Time.realtimeSinceStartup - m_FillOutStart;
		data += "\"timeNeededInSecForFillingOut\":";
		data += "" + timeNeeded + ",";
		
		data += "\"comment\":\"";
		data += m_WaterSourceComboWaterComment.GetComponentInChildren<InputField>().text;
		data += "\",";

		data += "\"operatingSystem\":\"";
		data += "" + SystemInfo.operatingSystem;
		data += "\",";

		data += "\"appVersion\":\"";
		data += "3";
		data += "\"";

		data += "}";
		Debug.Log("CropData: " + data);
		PlayerPrefs.SetString("Quest_" + m_NrQuestsDone + "_CropDesc", data);
		

		PlayerPrefs.SetInt("Quest_" + m_NrQuestsDone + "_NrLandUses", 0);// m_CurNrLandUses);

		PlayerPrefs.SetInt("Quest_" + m_NrQuestsDone + "_LandCoverId", 0);//m_CurLandCoverId);
																		  //PlayerPrefs.SetInt ("Quest_" + m_NrQuestsDone + "_LandUseId", m_CurLandUseId);
		PlayerPrefs.SetInt("Quest_" + m_NrQuestsDone + "_LandUseId", 0);

		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "LandCover" + "_Heading", Input.compass.trueHeading);
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "LandCover" + "_AccX", Input.acceleration.x);
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "LandCover" + "_AccY", Input.acceleration.y);
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "LandCover" + "_AccZ", Input.acceleration.z);
		
		
		/*
		PlayerPrefs.SetInt("m_ComboFrom", m_ComboFrom.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_ComboTo", m_ComboTo.GetComponent<Dropdown>().value);
		
		
		PlayerPrefs.SetInt("m_ComboPlantDay", m_ComboPlantDay.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_ComboPlantMonth", m_ComboPlantMonth.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_ComboHarvestDay", m_ComboHarvestDay.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_ComboHarvestMonth", m_ComboHarvestMonth.GetComponent<Dropdown>().value);
		
*/
		
		
		
		
		
		// Save timings
		string strtime = PlayerPrefs.GetString("CurQuestSelectedTime");
		PlayerPrefs.SetString("Quest_" + m_NrQuestsDone + "_" + "SelectedTime", strtime);

		strtime = PlayerPrefs.GetString("CurQuestStartQuestTime");
		PlayerPrefs.SetString("Quest_" + m_NrQuestsDone + "_" + "StartQuestTime", strtime);


		// Save player positions
		float fvalue = PlayerPrefs.GetFloat("CurQuestStartPositionX");
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "StartPositionX", fvalue);
		fvalue = PlayerPrefs.GetFloat("CurQuestStartPositionY");
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "StartPositionY", fvalue);
		fvalue = PlayerPrefs.GetFloat("CurQuestEndPositionX");
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "EndPositionX", fvalue);
		fvalue = PlayerPrefs.GetFloat("CurQuestEndPositionY");
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "EndPositionY", fvalue);

		fvalue = PlayerPrefs.GetFloat("CurDistanceWalked");
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "DistanceWalked", fvalue);

		int nrpositions = PlayerPrefs.GetInt("CurQuestNrPositions");
		PlayerPrefs.SetInt("Quest_" + m_NrQuestsDone + "_" + "NrPositions", nrpositions);
		for (int pos = 0; pos < nrpositions; pos++)
		{
			float posx = PlayerPrefs.GetFloat("CurQuestPositionX_" + pos);
			float posy = PlayerPrefs.GetFloat("CurQuestPositionY_" + pos);

			PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "PositionX_" + pos, posx);
			PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "PositionY_" + pos, posy);
		}






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
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "LandCover" + "_Tilt", tilted);




		float curcroppositionx = PlayerPrefs.GetFloat("CurCropPositionX");
		float curcroppositiony = PlayerPrefs.GetFloat("CurCropPositionY");

		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "LandCover" + "_Lat", curcroppositionx);
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "LandCover" + "_Lng", curcroppositiony);
		/*
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "LandCover" + "_Lat", lat);
		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "LandCover" + "_Lng", lng);*/

		PlayerPrefs.SetFloat("Quest_" + m_NrQuestsDone + "_" + "LandCover" + "_Accuracy", Input.compass.headingAccuracy);
		PlayerPrefs.SetFloat("Quest_" + "_Accuracy", Input.location.lastData.horizontalAccuracy);

		
		
		PlayerPrefs.SetInt("m_WaterSourceTypeCombo", m_WaterSourceTypeCombo.GetComponent<Dropdown>().value);
		PlayerPrefs.SetString("m_WaterSourceTypeOther", m_WaterSourceTypeOther.GetComponentInChildren<InputField>().text);
		
		PlayerPrefs.SetInt("m_WaterAbandonedCombo", m_WaterAbandonedCombo.GetComponent<Dropdown>().value);
		PlayerPrefs.SetString("m_WaterAbandonedOther", m_WaterAbandonedOther.GetComponentInChildren<InputField>().text);
		PlayerPrefs.SetString("m_WaterTreatedOther", m_WaterSourceTreatedOther.GetComponentInChildren<InputField>().text);
		
		PlayerPrefs.SetInt("m_WaterSourceQuality", m_WaterSourceQuality.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_WaterSourceComboNoWater", m_WaterSourceComboNoWater.GetComponent<Dropdown>().value);
		PlayerPrefs.SetString("m_WaterSourceInputNoWater", m_WaterSourceInputNoWater.GetComponentInChildren<InputField>().text);
		PlayerPrefs.SetInt("m_WaterSourceComboDrinkable", m_WaterSourceComboDrinkable.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_WaterSourceComboPublic", m_WaterSourceComboPublic.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_WaterSourceComboUsed", m_WaterSourceComboUsed.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_WaterSourceComboWaterTreated", m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_WaterSourceComboPayment", m_WaterSourceComboPayment.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_WaterSourceComboDistanceWalked", m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value);
		PlayerPrefs.SetString("m_WaterSourceComboWaterComment", m_WaterSourceComboWaterComment.GetComponentInChildren<InputField>().text);
		
		PlayerPrefs.SetInt("m_WaterSourceFunctional", m_WaterSourceFunctional.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_WaterSourceNearby", m_WaterSourceNearby.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_WaterSourceResponsible", m_WaterSourceResponsible.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_WaterSourceComboQueue", m_WaterSourceComboQueue.GetComponent<Dropdown>().value);
		
		
		
		/*PlayerPrefs.SetString("m_ClosestVillageName", m_ClosestVillageName.GetComponentInChildren<InputField>().text);
		PlayerPrefs.SetString("m_FieldSize", m_InputFieldSize.GetComponentInChildren<InputField>().text);
		PlayerPrefs.SetInt("m_FieldSizeType", m_FieldSizeCombo.GetComponent<Dropdown>().value);
		PlayerPrefs.SetString("m_FieldSizeOther", m_InputFieldSizeOther.GetComponentInChildren<InputField>().text);

		
		
		PlayerPrefs.SetInt("m_CropCombo", m_CropCombo.GetComponent<Dropdown>().value);
		PlayerPrefs.SetString("m_CropOther", m_CropOther.GetComponentInChildren<InputField>().text);
		
		
		
		PlayerPrefs.SetInt("m_TargetYieldCombo", m_TargetYieldCombo.GetComponent<Dropdown>().value);
		PlayerPrefs.SetString("m_TargetYield", m_TargetYield.GetComponentInChildren<InputField>().text);
		PlayerPrefs.SetString("m_TargetYieldOther", m_TargetYieldOther.GetComponentInChildren<InputField>().text);
	
		PlayerPrefs.SetInt("m_ActualYieldCombo", m_ActualYieldCombo.GetComponent<Dropdown>().value);
		PlayerPrefs.SetString("m_ActualYield", m_ActualYield.GetComponentInChildren<InputField>().text);
		PlayerPrefs.SetString("m_ActualYieldOther", m_ActualYieldOther.GetComponentInChildren<InputField>().text);
		
	
		PlayerPrefs.SetInt("m_Peril1", m_Peril1.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril2", m_Peril2.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril3", m_Peril3.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril4", m_Peril4.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril5", m_Peril5.GetComponent<Dropdown>().value);
		
		
		PlayerPrefs.SetString("m_Peril1Other", m_Peril1Other.GetComponent<InputField>().text);
		PlayerPrefs.SetString("m_Peril2Other", m_Peril2Other.GetComponent<InputField>().text);
		PlayerPrefs.SetString("m_Peril3Other", m_Peril3Other.GetComponent<InputField>().text);
		PlayerPrefs.SetString("m_Peril4Other", m_Peril4Other.GetComponent<InputField>().text);
		PlayerPrefs.SetString("m_Peril5Other", m_Peril5Other.GetComponent<InputField>().text);
	
		PlayerPrefs.SetInt("m_Peril1Impact", m_Peril1Impact.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril2Impact", m_Peril2Impact.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril3Impact", m_Peril3Impact.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril4Impact", m_Peril4Impact.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril5Impact", m_Peril5Impact.GetComponent<Dropdown>().value);
		
		
		
		
		PlayerPrefs.SetInt("m_Peril1_Other", m_Peril1_Other.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril2_Other", m_Peril2_Other.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril3_Other", m_Peril3_Other.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril4_Other", m_Peril4_Other.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_Peril5_Other", m_Peril5_Other.GetComponent<Dropdown>().value);
		
		
		PlayerPrefs.SetString("m_Peril1Other_Other", m_Peril1Other_Other.GetComponent<InputField>().text);
		PlayerPrefs.SetString("m_Peril2Other_Other", m_Peril2Other_Other.GetComponent<InputField>().text);
		PlayerPrefs.SetString("m_Peril3Other_Other", m_Peril3Other_Other.GetComponent<InputField>().text);
		PlayerPrefs.SetString("m_Peril4Other_Other", m_Peril4Other_Other.GetComponent<InputField>().text);
		PlayerPrefs.SetString("m_Peril5Other_Other", m_Peril5Other_Other.GetComponent<InputField>().text);
		
		
		PlayerPrefs.SetInt("m_PerilOtherImpact_1", m_PerilOtherImpact_1.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_PerilOtherImpact_2", m_PerilOtherImpact_2.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_PerilOtherImpact_3", m_PerilOtherImpact_3.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_PerilOtherImpact_4", m_PerilOtherImpact_4.GetComponent<Dropdown>().value);
		PlayerPrefs.SetInt("m_PerilOtherImpact_5", m_PerilOtherImpact_5.GetComponent<Dropdown>().value);
		*/
	
		PlayerPrefs.Save();

    }

	public void SaveSurvey()
	{
		bool bAbandoned = false;
		if (m_WaterSourceFunctional.GetComponent<Dropdown>().value == 3 ||
		    m_WaterSourceFunctional.GetComponent<Dropdown>().value == 4)
		{
			bAbandoned = true;
		}
		
	    string[] options2 = { "Ok" };

	    if (m_WaterSourceTypeCombo.GetComponent<Dropdown>().value == 0)
	    {
		    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourceType"), options2);
		    return;
	    }
	    
	    
	    if (m_WaterSourceFunctional.GetComponent<Dropdown>().value == 0)
	    {
		    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourceFunctional"), options2);
		    return;
	    }
	    
	    if (m_WaterSourceNearby.GetComponent<Dropdown>().value == 0)
	    {
		    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourceNearby"), options2);
		    return;
	    }

	    if (bAbandoned == false)
	    {
		    if (m_WaterSourceQuality.GetComponent<Dropdown>().value == 0)
		    {
			    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourceQuality"), options2);
			    return;
		    }
	    }


	    if (bAbandoned == false)
	    {
		    if (m_WaterSourceComboDrinkable.GetComponent<Dropdown>().value == 0)
		    {
			    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourceDrinkable"), options2);
			    return;
		    }
	    }

	    if (m_WaterSourceComboPublic.GetComponent<Dropdown>().value == 0)
	    {
		    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourcePublic"), options2);
		    return;
	    }

	    if (bAbandoned == false)
	    {
		    if (m_WaterSourceComboUsed.GetComponent<Dropdown>().value == 0)
		    {
			    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourceUsed"), options2);
			    return;
		    }
	    }

	    if (bAbandoned == false)
	    {
		    if (m_WaterSourceComboWaterTreated.GetComponent<Dropdown>().value == 0)
		    {
			    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourceTreated"), options2);
			    return;
		    }
	    }

	    if (bAbandoned == false)
	    {
		    if (m_WaterSourceResponsible.GetComponent<Dropdown>().value == 0)
		    {
			    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourceResponsible"), options2);
			    return;
		    }
	    }

	    if (bAbandoned == false)
	    {
		    if (m_WaterSourceComboPayment.GetComponent<Dropdown>().value == 0)
		    {
			    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourcePayment"), options2);
			    return;
		    }
	    }

	    if (bAbandoned == false)
	    {
		    if (m_WaterSourceComboDistanceWalked.GetComponent<Dropdown>().value == 0)
		    {
			    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterWaterSourceDistance"), options2);
			    return;
		    }
	    }

	    if (bAbandoned == false)
	    {
		    if (m_WaterSourceComboQueue.GetComponent<Dropdown>().value == 0)
		    {
			    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterQueue"), options2);
			    return;
		    }
	    }
	    
	    

	    if (bAbandoned)
	    {
		    if (m_WaterAbandonedCombo.GetComponent<Dropdown>().value == 0)
		    {
			    messageBox.Show("", LocalizationSupport.GetString("SurveyEnterAbandoned"), options2);
			    return;
		    }
	    }




	    SaveData();

		Application.LoadLevel("QuestFinished");
		//Application.LoadLevel("MultiplePhotos");
		//Application.LoadLevel("CameraOnePic");
		//Application.LoadLevel("QuestFinished");
	}

	public void EditObsTime()
	{
		SaveData();
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.SetInt("WhichDate", 1);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}

	public void EditObsTimeStart()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 2);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}

	public void EditObsTimeEnd()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 3);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}

	public void SelectProduction()
	{
		SaveData();
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("Production");
	}

	public void SelectAgroforestry()
	{
		SaveData();
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("Agroforestry");
	}
	
	public void SelectPlantDate()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 4);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	
	public void SelectHarvestDate()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 5);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	
	
	public void SelectPerilStart_1()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 10);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	public void SelectPerilStart_2()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 11);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	public void SelectPerilStart_3()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 12);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	public void SelectPerilStart_4()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 13);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	public void SelectPerilStart_5()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 14);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	
	public void SelectPerilEnd_1()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 15);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	public void SelectPerilEnd_2()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 16);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	public void SelectPerilEnd_3()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 17);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	public void SelectPerilEnd_4()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 18);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	public void SelectPerilEnd_5()
	{
		SaveData();
		PlayerPrefs.SetInt("WhichDate", 19);
		float posy = m_ContentView.transform.position.y;
		PlayerPrefs.SetFloat("ContentViewY", posy);
		PlayerPrefs.Save();
		Application.LoadLevel("DatePicker");
	}
	
	
	public void OnDescriptionClicked()
	{
		Application.OpenURL("https://www.dropbox.com/s/xhhk4j8hmi8ib1l/CropSystems.pdf?dl=0");
	}
}




