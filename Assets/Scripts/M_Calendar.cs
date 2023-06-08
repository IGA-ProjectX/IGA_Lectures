using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class M_Calendar : MonoBehaviour
{
    public static M_Calendar instance;
    public Transform parent_Schedule;
    public TMP_Text text_Month;
    public TMP_Text text_Year;
    public TMP_Text text_CurrentDayInfo;

    private List<Day> days = new List<Day>();
    public Transform[] weeks;
    public static DateTime currDate = DateTime.Now;
    public SO_DayColor pallate;

    void Start()
    {
        instance = this;
        InitializeCalendar();
        GetComponent<M_LectureAllocator>().AssignNormalDayToWeekday(DateTime.Now.Year);
        UpdateCalendar(DateTime.Now.Year, DateTime.Now.Month);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) SwitchMonth(1);
        if (Input.GetKeyDown(KeyCode.DownArrow)) SwitchMonth(-1);
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    void InitializeCalendar()
    {
        //text_Year.text = DateTime.Now.ToString("yyyy").Substring(0, 2) + "\n" + DateTime.Now.ToString("yyyy").Substring(2, 2);
        text_Year.text = DateTime.Now.ToString("yyyy");
        text_Month.text = DateTime.Now.ToString("MM");
        text_CurrentDayInfo.text = DateTime.Now.ToString("MM" + "ÔÂ" + "dd" + "ÈÕ") + "\n" + DateTime.Now.DayOfWeek;
    }

    void UpdateCalendar(int year, int month)
    {
        DateTime temp = new DateTime(year, month, 1);
        currDate = temp;

        int startWeekDay = (int)currDate.DayOfWeek;
        if (startWeekDay == 0) startWeekDay = 7;
        //Debug.Log(startWeekDay);
        int totalNumOfMonth = DateTime.DaysInMonth(year, month);

        if (days.Count == 0)
        {
            for (int w = 0; w < 5; w++)
            {
                for (int i = 0; i < 7; i++)
                {
                    Day newDay;
                    int currDay = (w * 7) + i;
                    if (currDay < startWeekDay || currDay - startWeekDay >= totalNumOfMonth)
                    {
                        newDay = new Day(year,month, currDay - startWeekDay + 2, weeks[w].GetChild(i).gameObject);
                    }
                    else
                    {
                        newDay = new Day(year,month, currDay - startWeekDay + 2, weeks[w].GetChild(i).gameObject);
                    }
                    days.Add(newDay);
                }
            }
        }
        else for (int i = 0; i < 35; i++) days[i].UpdateDay(year, month, i - startWeekDay + 2);

        //GetComponent<M_LectureAllocator>().UpdateLectureInfoBaseOnDayIndex(days);
        GetComponent<M_LectureAllocator>().UpdateLectureInfo(days);
    }

    public void SwitchMonth(int direction)
    {
        int monthIndex = currDate.Month;
        if (direction < 0 && monthIndex > 1)
        {
            currDate = currDate.AddMonths(-1);
        }
        else if (direction > 0 && monthIndex < 12)
        {
            currDate = currDate.AddMonths(1);
        }

        text_Month.text = currDate.ToString("MM");
        UpdateCalendar(currDate.Year, currDate.Month);
    }
}

public class Day
{
    public int dayNum;
    public int monthNum;
    public int yearNum;
    public GameObject obj;

    public Day(int yearNum, int monthNum, int dayNum, GameObject obj)
    {
        this.dayNum = dayNum;
        this.monthNum = monthNum;
        this.yearNum = yearNum;
        this.obj = obj;
        UpdateDay(yearNum, monthNum, dayNum);
    }

    public void UpdateDay(int newYear, int newMonth, int newDayNum)
    {
        if (newDayNum >= 1 && newDayNum <= DateTime.DaysInMonth(newYear, newMonth))
        {
            dayNum = newDayNum;
            monthNum = newMonth;
            yearNum = newYear;
        }
        else if (newDayNum < 1)
        {
            yearNum = newYear;
            if (newMonth > 1)
            {
                dayNum = DateTime.DaysInMonth(newYear, newMonth - 1) + newDayNum;
                monthNum = newMonth - 1;
            }
            else
            {
                yearNum--;
                monthNum = 12;
                dayNum = DateTime.DaysInMonth(yearNum, monthNum) + newDayNum;
            }
        }
        else if (newDayNum > DateTime.DaysInMonth(newYear, newMonth))
        {
            dayNum = newDayNum - DateTime.DaysInMonth(newYear, newMonth);
            monthNum = newMonth + 1;
            yearNum = newYear;
        }
        obj.transform.Find("Date").GetComponent<TMP_Text>().text = monthNum.ToString() + "." + dayNum.ToString();
    }

    public void UpdateLecture(MonthDayLecture targetLecture)
    {
        if (M_Calendar.currDate.Month != monthNum)
        {
            obj.transform.Find("Advanced").GetComponent<Image>().color = M_Calendar.instance.pallate.otherMonth;
            obj.transform.Find("Fundamental").GetComponent<Image>().color = M_Calendar.instance.pallate.otherMonth;
        }
        else
        {
            obj.transform.Find("Advanced").GetComponent<Image>().color = M_Calendar.instance.pallate.lectureTypeColor[(int)targetLecture.lectureType];
            //obj.transform.Find("Advanced").GetComponentInChildren<TMPro.TMP_Text>().color = Color.white;
            obj.transform.Find("Fundamental").GetComponent<Image>().color = M_Calendar.instance.pallate.lectureTypeColor[(int)targetLecture.lectureType];
            //obj.transform.Find("Fundamental").GetComponentInChildren<TMPro.TMP_Text>().color = Color.white;
        }

        obj.transform.Find("Advanced").Find("Text").GetComponent<TMP_Text>().text = targetLecture.l_Advanced.nameChi;
        if (targetLecture.l_Advanced.teacherData != null)
        {
            Transform teacherObj = obj.transform.Find("Advanced").Find("Teacher");
            teacherObj.gameObject.SetActive(true);
            teacherObj.GetComponent<Image>().color = teacherObj.parent.GetComponent<Image>().color;
            teacherObj.Find("Text").GetComponent<TMP_Text>().text = targetLecture.l_Advanced.teacherData.nameEng + targetLecture.l_Advanced.teacherData.nameChi.Substring(0, 1);
        }
        else obj.transform.Find("Advanced").Find("Teacher").gameObject.SetActive(false);

        obj.transform.Find("Fundamental").Find("Text").GetComponent<TMP_Text>().text = targetLecture.l_Fundemantal.nameChi;
        if (targetLecture.l_Fundemantal.teacherData != null)
        {
            Transform teacherObj = obj.transform.Find("Fundamental").Find("Teacher");
            teacherObj.gameObject.SetActive(true);
            teacherObj.GetComponent<Image>().color = teacherObj.parent.GetComponent<Image>().color;
            teacherObj.Find("Text").GetComponent<TMP_Text>().text = targetLecture.l_Fundemantal.teacherData.nameEng + targetLecture.l_Fundemantal.teacherData.nameChi.Substring(0, 1);
        }
        else obj.transform.Find("Fundamental").Find("Teacher").gameObject.SetActive(false);

        if (targetLecture.classIndex != 0)
        {
            obj.transform.Find("Class Sequence").gameObject.SetActive(true);
            obj.transform.Find("Class Sequence").Find("Text").GetComponent<TMP_Text>().text = targetLecture.classIndex.ToString();
        }
        else obj.transform.Find("Class Sequence").gameObject.SetActive(false);
    }
}
