using System.Collections;
using System.Collections.Generic;
using UI.Dates;
using UnityEngine;
using UnityEngine.UI;

public class EventSystemDatePicker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if ((!LocalizationSupport.StringsLoaded))
            LocalizationSupport.LoadStrings();
        
        var dropdown = m_ComboDay.GetComponent<Dropdown>();
        dropdown.options.Clear();
        
        for (int iter = 1; iter <= 32; iter++)
        {
            dropdown.options.Add(new Dropdown.OptionData(iter + ""));
        }
        dropdown.value = 1;
        dropdown.value = 0;
        
        dropdown = m_ComboMonth.GetComponent<Dropdown>();
        dropdown.options.Clear();
        
        for (int iter = 1; iter <= 13; iter++)
        {
            dropdown.options.Add(new Dropdown.OptionData(iter + ""));
        }
        dropdown.value = 1;
        dropdown.value = 0;
        
        
        dropdown = m_ComboYear.GetComponent<Dropdown>();
        dropdown.options.Clear();
        
        for (int iter = 2000; iter <= 2022; iter++)
        {
            dropdown.options.Add(new Dropdown.OptionData(iter + ""));
        }
        dropdown.value = 1;
        dropdown.value = 0;

        UseGregCalendar();
        
        
        m_TextDay.GetComponent<Text>().text = LocalizationSupport.GetString("Day");
        m_TextMonth.GetComponent<Text>().text = LocalizationSupport.GetString("Month");
        m_TextYear.GetComponent<Text>().text = LocalizationSupport.GetString("Year");
        m_TextCalendar.GetComponent<Text>().text = LocalizationSupport.GetString("Calendar");
    }

    public GameObject m_ButtonNext;
    public DatePicker m_DatePicker;
    
    public GameObject m_TextDay;
    public GameObject m_TextMonth;
    public GameObject m_TextYear;
    public GameObject m_TextCalendar;
    public GameObject m_ComboDay;
    public GameObject m_ComboMonth;
    public GameObject m_ComboYear;
    public GameObject m_ComboCalendar;

    public GameObject m_Calendar;
    //public GameObject m_BtnUseLocalCalendar;
    //public GameObject m_BtnUseGregCalendar;

    private bool m_bUseLocalCalendar = false;
    public void UseLocalCalendar()
    {
        m_Calendar.SetActive(false);
       // m_BtnUseLocalCalendar.SetActive(false);
        m_ComboDay.SetActive(true);
        m_ComboMonth.SetActive(true);
        m_ComboYear.SetActive(true);
        m_TextDay.SetActive(true);
        m_TextMonth.SetActive(true);
        m_TextYear.SetActive(true);
        m_ButtonNext.SetActive(true);
       // m_BtnUseGregCalendar.SetActive(true);
        m_bUseLocalCalendar = true;
    }
    
    public void UseGregCalendar()
    {
        m_Calendar.SetActive(true);
      //  m_BtnUseLocalCalendar.SetActive(true);
        m_ComboDay.SetActive(false);
        m_ComboMonth.SetActive(false);
        m_ComboYear.SetActive(false);
        m_TextDay.SetActive(false);
        m_TextMonth.SetActive(false);
        m_TextYear.SetActive(false);
      //  m_BtnUseGregCalendar.SetActive(false);
        m_bUseLocalCalendar = false;
    }

    public void ChangeCalendar(int value)
    {
        if(value == 0)
            UseGregCalendar();
        else UseLocalCalendar();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_bUseLocalCalendar)
        {
            m_ButtonNext.SetActive(true);
        }
        else
        {
            m_ButtonNext.SetActive(false);

            SerializableDate date = m_DatePicker.SelectedDate;

            string strdate = date.ToString();


            // Debug.Log(strdate);
            if (strdate != null)
            {
                m_ButtonNext.SetActive(true);
            }
        }
    }

    public void OnBackClicked()
    {
        PlayerPrefs.SetInt("CameFromFromCamera", 1);
        PlayerPrefs.Save();
        Application.LoadLevel("QuestionsEShape");
    }

    public void SaveDate()
    {
        string selecteddate = "";//date.Date.ToString("yyyy-MM-dd");

        if (m_ComboCalendar.GetComponent<Dropdown>().value == 1)
        {
            string day = m_ComboDay.GetComponent<Dropdown>().options[m_ComboDay.GetComponent<Dropdown>().value].text;
            string month = m_ComboMonth.GetComponent<Dropdown>().options[m_ComboMonth.GetComponent<Dropdown>().value].text;
            string year = m_ComboYear.GetComponent<Dropdown>().options[m_ComboYear.GetComponent<Dropdown>().value].text;
            selecteddate =
                year + "-" + month + "-" + day;
        }
        else
        {
            SerializableDate date = m_DatePicker.SelectedDate;
            string strdate = date.ToString();
            selecteddate = date.Date.ToString("yyyy-MM-dd");
        }

        if (PlayerPrefs.GetInt("WhichDate") == 1)
        {
            PlayerPrefs.SetString("CurQuestObsTime", selecteddate);

            PlayerPrefs.SetInt("CameFromFromCamera", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 2)
        {
            PlayerPrefs.SetString("Survey_StartDate", selecteddate);

            PlayerPrefs.SetInt("Survey_StartDateSet", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 3)
        {
            PlayerPrefs.SetString("Survey_EndDate", selecteddate);

            PlayerPrefs.SetInt("Survey_EndDateSet", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 4)
        {
            PlayerPrefs.SetString("m_PlantDate", selecteddate);
            Debug.Log("Saved date 4: " + selecteddate);

            PlayerPrefs.SetInt("m_PlantDateSet", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 5)
        {
            PlayerPrefs.SetString("m_HarvestDate", selecteddate);
            Debug.Log("Saved date 5: " + selecteddate);

            PlayerPrefs.SetInt("m_HarvestDateSet", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 10)
        {
            PlayerPrefs.SetString("m_PerilFrom_1", selecteddate);
            Debug.Log("Saved date 10: " + selecteddate);

            PlayerPrefs.SetInt("m_PerilFromSet_1", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 11)
        {
            PlayerPrefs.SetString("m_PerilFrom_2", selecteddate);
            Debug.Log("Saved date 11: " + selecteddate);

            PlayerPrefs.SetInt("m_PerilFromSet_2", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 12)
        {
            PlayerPrefs.SetString("m_PerilFrom_3", selecteddate);
            Debug.Log("Saved date 12: " + selecteddate);

            PlayerPrefs.SetInt("m_PerilFromSet_3", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 13)
        {
            PlayerPrefs.SetString("m_PerilFrom_4", selecteddate);
            Debug.Log("Saved date 13: " + selecteddate);

            PlayerPrefs.SetInt("m_PerilFromSet_4", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 14)
        {
            PlayerPrefs.SetString("m_PerilFrom_5", selecteddate);
            Debug.Log("Saved date 14: " + selecteddate);

            PlayerPrefs.SetInt("m_PerilFromSet_5", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 15)
        {
            PlayerPrefs.SetString("m_PerilTo_1", selecteddate);
            Debug.Log("Saved date 15: " + selecteddate);

            PlayerPrefs.SetInt("m_PerilToSet_1", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 16)
        {
            PlayerPrefs.SetString("m_PerilTo_2", selecteddate);
            Debug.Log("Saved date 16: " + selecteddate);

            PlayerPrefs.SetInt("m_PerilToSet_2", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 17)
        {
            PlayerPrefs.SetString("m_PerilTo_3", selecteddate);
            Debug.Log("Saved date 17: " + selecteddate);

            PlayerPrefs.SetInt("m_PerilToSet_3", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 18)
        {
            PlayerPrefs.SetString("m_PerilTo_4", selecteddate);
            Debug.Log("Saved date 18: " + selecteddate);

            PlayerPrefs.SetInt("m_PerilToSet_4", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
        else if (PlayerPrefs.GetInt("WhichDate") == 19)
        {
            PlayerPrefs.SetString("m_PerilTo_5", selecteddate);
            Debug.Log("Saved date 19: " + selecteddate);

            PlayerPrefs.SetInt("m_PerilToSet_5", 1);
            PlayerPrefs.Save();
            Application.LoadLevel("QuestionsEShape");
        } 
    }
}
