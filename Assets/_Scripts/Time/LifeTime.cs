using System;

public class LifeTime
{
    public int Day;
    public int Month;
    public int Year;
    public int Hour;
    public int Minute;
    public int Second;

    public LifeTime(int day, int month, int year, int hour, int minute, int second)
    {
        Day = day;
        Month = month;
        Year = year;
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    public void CalculateTime()
    {
        bool calculated = false;
        while (!calculated)
        {
            calculated = true;

            if (Second >= Timer.MinuteSize)
            {
                Minute++;
                Second = Second - Timer.MinuteSize;
                calculated = false;
            }

            if (Minute >= Timer.HourSize)
            {
                Hour++;
                Minute = Minute - Timer.HourSize;
                calculated = false;
            }

            if (Hour >= Timer.DaySize)
            {
                Day++;
                Hour = 0;
                calculated = false;
            }

            if (Day > Timer.DayPerMonth)
            {
                Month++;
                Day = 1;
                calculated = false;
            }

            if (Month >= Timer.MonthPerYear)
            {
                Year++;
                Month = 1;
                calculated = false;
            }
        }
    }

    public static bool Later(LifeTime time1, LifeTime time2)
    {
        DateTime t1 = CreateFromTime(time1);
        DateTime t2 = CreateFromTime(time2);
        return t1.CompareTo(t2) > 0;
    }

    public static bool Earlier(LifeTime time1, LifeTime time2)
    {
        DateTime t1 = CreateFromTime(time1);
        DateTime t2 = CreateFromTime(time2);
        return t1.CompareTo(t2) < 0;
    }

    public static bool Equal(LifeTime time1, LifeTime time2)
    {
        DateTime t1 = CreateFromTime(time1);
        DateTime t2 = CreateFromTime(time2);
        return t1.CompareTo(t2) == 0;
    }

    public LifeTime AddTime(int day, int month, int year, int hour, int minute, int second)
    {
        LifeTime lt = new LifeTime(Day + day, Month + month, Year + year, Hour + hour, Minute + minute, Second + second);
        lt.CalculateTime();
        return lt;
    }

    private static DateTime CreateFromTime(LifeTime time1)
    {
        return new DateTime(time1.Year, time1.Month, time1.Day, time1.Hour, time1.Minute, time1.Second);
    }

    public LifeTime Clone()
    {
        return (LifeTime) this.MemberwiseClone();
    }

    public override string ToString()
    {
        return string.Format("{0:00}", Day) + "/"
              + string.Format("{0:00}", Month) + "/" + Year + "  \n"
              + string.Format("{0:00}", Hour) + ":"
              + string.Format("{0:00}", Minute);
    }
}
