using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Lecture Info",menuName = "SObj/New Lecture Info")]
public class SO_Lecture : ScriptableObject
{
    public DayType whichDayInWeek;
    public LectureType lectureType;
    public LectureInfo[] fundemantalLecture;
    public LectureInfo[] advancedLectures;
}

public enum LectureType { Rest, EasterEgg, Design, MultiMedia, Art3D, UnityDev, Art2D }
public enum DayType { Mon,Tue,Wed,Thu,Fri,Sat,Sun}

[System.Serializable]
public class LectureInfo
{
    public string nameChi;
    public SO_Teacher teacherData;
}