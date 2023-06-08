using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class M_LectureAllocator : MonoBehaviour
{
    public SO_Lecture[] weekdayLectures;
    public SO_Holiday holidayList;


    private List<int> monList = new List<int>();
    private List<int> tueList = new List<int>();
    private List<int> wedList = new List<int>();
    private List<int> thuList = new List<int>();
    private List<int> friList = new List<int>();
    private List<int> satList = new List<int>();
    private List<int> sunList = new List<int>();

    private List<List<int>> allWeekdayList = new List<List<int>>();

    private List<MonthDayLecture> lectures_January = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_February = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_March = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_April = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_May = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_June = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_July = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_August = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_September = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_October = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_November = new List<MonthDayLecture>();
    private List<MonthDayLecture> lectures_December = new List<MonthDayLecture>();

    private int eg_FIndex = 0;
    private int eg_AIndex = 0;

    public void AssignNormalDayToWeekday(int year)
    {
        allWeekdayList.Add(monList);
        allWeekdayList.Add(tueList);
        allWeekdayList.Add(wedList);
        allWeekdayList.Add(thuList);
        allWeekdayList.Add(friList);
        allWeekdayList.Add(satList);
        allWeekdayList.Add(sunList);

        //初始化每个月的每天对应的课程
        for (int month = 1; month <= 12; month++)
        {
            switch (month)
            {
                case 1:
                    lectures_January = TargetMonthLecture(1);
                    break;
                case 2:
                    lectures_February = TargetMonthLecture(2);
                    break;
                case 3:
                    lectures_March = TargetMonthLecture(3);
                    break;
                case 4:
                    lectures_April = TargetMonthLecture(4);
                    break;
                case 5:
                    lectures_May = TargetMonthLecture(5);
                    break;
                case 6:
                    lectures_June = TargetMonthLecture(6);
                    break;
                case 7:
                    lectures_July = TargetMonthLecture(7);
                    break;
                case 8:
                    lectures_August = TargetMonthLecture(8);
                    break;
                case 9:
                    lectures_September = TargetMonthLecture(9);
                    break;
                case 10:
                    lectures_October = TargetMonthLecture(10);
                    break;
                case 11:
                    lectures_November = TargetMonthLecture(11);
                    break;
                case 12:
                    lectures_December = TargetMonthLecture(12);
                    break;
            }
        }

        //Debug.Log(FindLectureInDay(lectures_March, 29).lectureName + " " + FindLectureInDay(lectures_March, 29).classIndex);

        List<MonthDayLecture> TargetMonthLecture(int month)
        {
            //声明一个用于返回的list
            List<MonthDayLecture> temp = new List<MonthDayLecture>();

            //获取当前月份的可排课日期列表
            List<int> restDays = GetHolidayTimeInMonth(month);
            List<int> residueDays = new List<int>();
            int monthLength = DateTime.DaysInMonth(year, month);
            for (int i = 1; i <= monthLength; i++) residueDays.Add(i);
            if (restDays.Count != 0) foreach (int dayToDelete in restDays)
                    if (residueDays.Contains(dayToDelete)) residueDays.Remove(dayToDelete);
            //Debug.Log(month + " Original Length: " + monthLength + " Residue Length: " + residueDays.Count);

            for (int i = 0; i < restDays.Count; i++)
                if (restDays[i] <= monthLength)
                {
                    LectureInfo restLecture = new LectureInfo() { nameChi = "法定休息~", teacherData = null };
                    temp.Add(new MonthDayLecture(restDays[i], LectureType.Rest, restLecture, restLecture, 0, false));
                }
            

            //string restString = "";
            //foreach (int day in restDays) restString += day.ToString() + "  ";
            //Debug.Log(month + " : " + restString);
            //string residueString = "";
            //foreach (int day in residueDays) residueString += day.ToString() + "  ";
            //Debug.Log(month + " : " + residueString);


            //分配当前月份周N所对应的日期列表
            monList.Clear();
            tueList.Clear();
            wedList.Clear();
            thuList.Clear();
            friList.Clear();
            satList.Clear();
            sunList.Clear();

            string ws1 = "";
            string ws2 = "";
            string ws3 = "";
            string ws4 = "";
            string ws5 = "";
            string ws6 = "";
            string ws7 = "";

            foreach (int dayIndex in residueDays)
            {
                switch (new DateTime(year, month, dayIndex).DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        monList.Add(dayIndex);
                        ws1 += dayIndex + "  ";
                        break;
                    case DayOfWeek.Tuesday:
                        tueList.Add(dayIndex);
                        ws2 += dayIndex + "  ";
                        break;
                    case DayOfWeek.Wednesday:
                        wedList.Add(dayIndex);
                        ws3 += dayIndex + "  ";
                        break;
                    case DayOfWeek.Thursday:
                        thuList.Add(dayIndex);
                        ws4 += dayIndex + "  ";
                        break;
                    case DayOfWeek.Friday:
                        friList.Add(dayIndex);
                        ws5 += dayIndex + "  ";
                        break;
                    case DayOfWeek.Saturday:
                        satList.Add(dayIndex);
                        ws6 += dayIndex + "  ";
                        break;
                    case DayOfWeek.Sunday:
                        sunList.Add(dayIndex);
                        ws7 += dayIndex + "  ";
                        break;
                }
            }

            //string allWeekday = "周一" + ws1 + "\n" + "周二" + ws2 + "\n" + "周三" + ws3 + "\n" + "周四" + ws4 + "\n" + "周五" + ws5 + "\n" + "周六" + ws6 + "\n" + "周日" + ws7;
            //Debug.Log(allWeekday);
            //foreach (List<int> dayList in allWeekdayList) Debug.Log(dayList.Count);

            //预留彩蛋日和休息日的空间给与补课
            for (int i = 2; i < allWeekdayList.Count; i++)
            {
                LectureType lectureType = weekdayLectures[i].lectureType;
                LectureInfo l_AD = FindLectureInMonth(lectureType, month, true);
                LectureInfo l_FD = FindLectureInMonth(lectureType, month, false);
                bool isShift = false;

                if (allWeekdayList[i].Count == 4)
                {
                    for (int j = 0; j < allWeekdayList[i].Count; j++)
                        temp.Add(new MonthDayLecture(allWeekdayList[i][j], lectureType, l_FD, l_AD, j + 1, isShift));
                }
                else if (allWeekdayList[i].Count < 4)
                {
                    for (int j = 0; j < 4 - allWeekdayList[i].Count; j++)
                    {
                        allWeekdayList[i].Add(GetClosetShiftDay());
                    }
                    allWeekdayList[i].Sort((x, y) => x.CompareTo(y));

                    for (int j = 0; j < allWeekdayList[i].Count; j++)
                        temp.Add(new MonthDayLecture(allWeekdayList[i][j], lectureType, l_FD, l_AD, j + 1, isShift));
                }
                else if (allWeekdayList[i].Count > 4)
                {
                    for (int j = 0; j < allWeekdayList[i].Count; j++)
                    {
                        if (j <= 3) temp.Add(new MonthDayLecture(allWeekdayList[i][j], lectureType, l_FD, l_AD, j + 1, isShift));
                        else monList.Add(allWeekdayList[i][j]);
                    }
                }
            }

            //添加彩蛋课的课程排布
            for (int i = 0; i < tueList.Count; i++)
            {
                LectureInfo l_AD = FindEasterEgg(LectureType.EasterEgg, true);
                LectureInfo l_FD = FindEasterEgg(LectureType.EasterEgg, false);
                temp.Add(new MonthDayLecture(tueList[i], LectureType.EasterEgg, l_FD, l_AD, i + 1, false));
            }

            //添加休息日的信息排布
            for (int i = 0; i < monList.Count; i++)
            {
                //Debug.Log(month + " - " + i);
                LectureInfo noClassLecture = new LectureInfo() { nameChi = "不上课哦~", teacherData = null };
                temp.Add(new MonthDayLecture(monList[i], LectureType.Rest, noClassLecture, noClassLecture, 0, false));
            }

            //Debug.Log(month + " length: " + temp.Count + " " + monthLength);
            return temp;
        }

        LectureInfo FindLectureInMonth(LectureType lectureType,int targetMonth,bool isAdvanced)
        {
            if (isAdvanced)
            {
                int lectureCount = weekdayLectures[(int)lectureType].advancedLectures.Length;
                int targetSequence = targetMonth - 1;
                while (targetSequence > lectureCount - 1) targetSequence -= lectureCount;

                return weekdayLectures[(int)lectureType].advancedLectures[targetSequence];
            }
            else
            {
                int lectureCount = weekdayLectures[(int)lectureType].fundemantalLecture.Length;
                int targetSequence = targetMonth - 1;
                while(targetSequence > lectureCount - 1) targetSequence -= lectureCount;

                return weekdayLectures[(int)lectureType].fundemantalLecture[targetSequence];
            }
        }

        LectureInfo FindEasterEgg(LectureType lectureType, bool isAdvanced)
        {
            if (isAdvanced)
            {
                int lectureCount = weekdayLectures[(int)lectureType].advancedLectures.Length;
                int targetSequence = eg_AIndex;
                eg_AIndex++;
                if (eg_AIndex >= lectureCount) eg_AIndex = 0;

                return weekdayLectures[(int)lectureType].advancedLectures[targetSequence];
            }
            else
            {
                int lectureCount = weekdayLectures[(int)lectureType].fundemantalLecture.Length;
                int targetSequence = eg_FIndex;
                eg_FIndex++;
                if (eg_FIndex >= lectureCount) eg_FIndex = 0;

                return weekdayLectures[(int)lectureType].fundemantalLecture[targetSequence];
            }
        }

        List<int> GetHolidayTimeInMonth(int month)
        {
            List<int> tempList = new List<int>();
            foreach (MonthHoliday monthHoliday in holidayList.monthHolidays)
            {
                if (month == monthHoliday.monthIndex && monthHoliday.holidayIndex != null)
                {
                    foreach (RangedInt rangedInt in monthHoliday.holidayIndex)
                    {
                        for (int tempValue = rangedInt.minValue; tempValue <= rangedInt.maxValue; tempValue++)
                        {
                            tempList.Add(tempValue);
                        }
                    }
                }
            }
            return tempList;
        }

        int GetClosetShiftDay()
        {
            int returnInt = 0;
            if (monList.Count!=0)
            {
                returnInt = monList[0];
                monList.RemoveAt(0);
                return returnInt;
            }
            else
            {
                returnInt = tueList[0];
                tueList.RemoveAt(0);
                return returnInt;
            }
        }
    }

    public void UpdateLectureInfo(List<Day> days)
    {
        foreach (Day day in days)
        {
            //Debug.Log(day.monthNum);
            switch (day.monthNum)
            {
                case 1: 
                    day.UpdateLecture(FindLectureInDay(lectures_January, day.dayNum));
                    break;
                case 2:
                    day.UpdateLecture(FindLectureInDay(lectures_February, day.dayNum));
                    break;
                case 3:
                    day.UpdateLecture(FindLectureInDay(lectures_March, day.dayNum));
                    break;
                case 4:
                    day.UpdateLecture(FindLectureInDay(lectures_April, day.dayNum));
                    break;
                case 5:
                    day.UpdateLecture(FindLectureInDay(lectures_May, day.dayNum));
                    break;
                case 6:
                    day.UpdateLecture(FindLectureInDay(lectures_June, day.dayNum));
                    break;
                case 7:
                    day.UpdateLecture(FindLectureInDay(lectures_July, day.dayNum));
                    break;
                case 8:
                    day.UpdateLecture(FindLectureInDay(lectures_August, day.dayNum));
                    break;
                case 9:
                    day.UpdateLecture(FindLectureInDay(lectures_September, day.dayNum));
                    break;
                case 10:
                    day.UpdateLecture(FindLectureInDay(lectures_October, day.dayNum));
                    break;
                case 11:
                    day.UpdateLecture(FindLectureInDay(lectures_November, day.dayNum));
                    break;
                case 12:
                    day.UpdateLecture(FindLectureInDay(lectures_December, day.dayNum));
                    break;
            }
        }
    }

    MonthDayLecture FindLectureInDay(List<MonthDayLecture> targetMonth, int targetDay)
    {
        foreach (MonthDayLecture lecture in targetMonth)
        {
            if (lecture.dayIndex == targetDay) return lecture;
        }
        //Debug.Log("No Lecture Found" + targetMonth.Count + " " + targetDay);
        return null;
    }

    public class MonthDay
    {
        public int monthIndex;
        public int dayIndex;

        public MonthDay(int monthIndex, int dayIndex)
        {
            this.monthIndex = monthIndex;
            this.dayIndex = dayIndex;
        }
    }
}

public class MonthDayLecture
{
    public int dayIndex;
    public LectureType lectureType;
    public LectureInfo l_Fundemantal;
    public LectureInfo l_Advanced;
    public int classIndex;
    public bool isShift;

    public MonthDayLecture(int dayIndex, LectureType lectureType, LectureInfo l_Fundemantal, LectureInfo l_Advanced, int classIndex, bool isShift)
    {
        this.dayIndex = dayIndex;
        this.lectureType = lectureType;
        this.l_Fundemantal = l_Fundemantal;
        this.l_Advanced = l_Advanced;
        this.classIndex = classIndex;
        this.isShift = isShift;
    }
}



//private List<MonthDay> monList = new List<MonthDay>();
//private List<MonthDay> tueList = new List<MonthDay>();
//private List<MonthDay> wedList = new List<MonthDay>();
//private List<MonthDay> thuList = new List<MonthDay>();
//private List<MonthDay> friList = new List<MonthDay>();
//private List<MonthDay> satList = new List<MonthDay>();
//private List<MonthDay> sunList = new List<MonthDay>();

//public void AssignNormalDayToWeekday(int year)
//{

//for (int month = 1; month < 13; month++)
//{
//    int monthLength = DateTime.DaysInMonth(year, month);
//    for (int day = 1; day <= monthLength; day++)
//    {
//        switch (new DateTime(year, month, day).DayOfWeek)
//        {
//            case DayOfWeek.Monday:
//                monList.Add(new MonthDay(month, day));
//                break;
//            case DayOfWeek.Tuesday:
//                tueList.Add(new MonthDay(month, day));
//                break;
//            case DayOfWeek.Wednesday:
//                wedList.Add(new MonthDay(month, day));
//                break;
//            case DayOfWeek.Thursday:
//                thuList.Add(new MonthDay(month, day));
//                break;
//            case DayOfWeek.Friday:
//                friList.Add(new MonthDay(month, day));
//                break;
//            case DayOfWeek.Saturday:
//                satList.Add(new MonthDay(month, day));
//                break;
//            case DayOfWeek.Sunday:
//                sunList.Add(new MonthDay(month, day));
//                break;
//        }
//    }
//}

//allWeekdayList.Add(monList);
//allWeekdayList.Add(tueList);
//allWeekdayList.Add(wedList);
//allWeekdayList.Add(thuList);
//allWeekdayList.Add(friList);
//allWeekdayList.Add(satList);
//allWeekdayList.Add(sunList);

//Debug.Log(monList.Count + tueList.Count + wedList.Count + thuList.Count + friList.Count + satList.Count + sunList.Count);
//}

//public void UpdateLectureInfoBaseOnDayIndex(List<Day> days)
//{
//    for (int i = 0; i < days.Count; i++)
//    {
//        switch (new DateTime(days[i].yearNum, days[i].monthNum, days[i].dayNum).DayOfWeek)
//        {
//            case DayOfWeek.Monday:
//                days[i].UpdateLecture(GetLectureInfoDependOnWeekdaySequence(monList, days[i], weekdayLectures[0]));
//                break;
//            case DayOfWeek.Tuesday:
//                days[i].UpdateLecture(GetLectureInfoDependOnWeekdaySequence(tueList, days[i], weekdayLectures[1]));
//                break;
//            case DayOfWeek.Wednesday:
//                days[i].UpdateLecture(GetLectureInfoDependOnWeekdaySequence(wedList, days[i], weekdayLectures[2]));
//                break;
//            case DayOfWeek.Thursday:
//                days[i].UpdateLecture(GetLectureInfoDependOnWeekdaySequence(thuList, days[i], weekdayLectures[3]));
//                break;
//            case DayOfWeek.Friday:
//                days[i].UpdateLecture(GetLectureInfoDependOnWeekdaySequence(friList, days[i], weekdayLectures[4]));
//                break;
//            case DayOfWeek.Saturday:
//                days[i].UpdateLecture(GetLectureInfoDependOnWeekdaySequence(satList, days[i], weekdayLectures[5]));
//                break;
//            case DayOfWeek.Sunday:
//                days[i].UpdateLecture(GetLectureInfoDependOnWeekdaySequence(sunList, days[i], weekdayLectures[6]));
//                break;
//        }
//    }
//}

//private LectureInfo GetLectureInfoDependOnWeekdaySequence(List<MonthDay> weekdayList, Day targetDay, SO_Lecture weekdayLecture)
//{
//    int weekSequenceIndex = 0;
//    for (int i = 0; i < weekdayList.Count; i++)
//    {
//        if (weekdayList[i].monthIndex == targetDay.monthNum && weekdayList[i].dayIndex == targetDay.dayNum)
//        {
//            weekSequenceIndex = i + 1;
//        }
//    }

//    if (weekdayLecture.subLectures.Length!=0)
//    {
//        int round = weekSequenceIndex / 4 + 1;
//        int lectureIndex = round % weekdayLecture.subLectures.Length - 1;
//        if (lectureIndex == -1) lectureIndex = weekdayLecture.subLectures.Length - 1;

//        int lessonIndex = weekSequenceIndex % 4;

//        return weekdayLecture.subLectures[lectureIndex];

//    }
//    else
//    {
//        return null;
//    }
//}

