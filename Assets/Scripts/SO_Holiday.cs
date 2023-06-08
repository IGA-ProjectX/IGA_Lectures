using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Holiday Info", menuName = "SObj/New Holiday Info")]
public class SO_Holiday : ScriptableObject
{
    public MonthHoliday[] monthHolidays;
}

[System.Serializable]
public class MonthHoliday
{
    public int monthIndex;
    [MinMaxRangeInt(1, 31)]
    public RangedInt[] holidayIndex;
}
