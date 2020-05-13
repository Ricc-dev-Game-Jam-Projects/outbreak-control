using System.Threading;
using UnityEngine;

public class TimerEventArgs
{
    public Timer timer;

    public TimerEventArgs(Timer timer)
    {
        this.timer = timer;
    }
}

public class Timer
{
    public LifeTime lifeTime;

    public int Day {
        get {
            return lifeTime.Day;
        }
        set {
            lifeTime.Day = value;
        }
    }
    public int Month {
        get {
            return lifeTime.Month;
        }
        set {
            lifeTime.Month = value;
        }
    }
    public int Year {
        get {
            return lifeTime.Year;
        }
        set {
            lifeTime.Year = value;
        }
    }
    public int Hour {
        get {
            return lifeTime.Hour;
        }
        set {
            lifeTime.Hour = value;
        }
    }
    public int Minute {
        get {
            return lifeTime.Minute;
        }
        set {
            lifeTime.Minute = value;
        }
    }
    public int Speed = 1;
    public float Second {
        get {
            return lifeTime.Second;
        }
        set {
            lifeTime.Second = (int)value;
        }
    }

    public static readonly int DayPerMonth = 30;
    public static readonly int MonthPerYear = 12;

    public int SecondPerReal = 16;
    public static readonly int MinuteSize = 60;
    public static readonly int HourSize = 60;
    public static readonly int DaySize = 24;

    public bool Running;

    public delegate void TimerHandler(object sender, TimerEventArgs time);

    public event TimerHandler DayEvent;
    public event TimerHandler HourEvent;
    public event TimerHandler MinuteEvent;
    public event TimerHandler MonthEvent;

    private Thread timer; 

    public Timer(int day, int month, int year, int hour, int minute, float second)
    {
        lifeTime = new LifeTime(day, month, year, hour, minute, (int)second);
        timer = new Thread(Tick);
    }

    public Timer()
    {
        lifeTime = new LifeTime(1, 1, 1, 0, 0, 0);

        timer = new Thread(Tick);

        Debug.Log("<color=purple>Bom dia! Data de hoje >>> " + ToString() + "</color>");
    }

    public void Start()
    {
        Running = true;
        timer.Start();
    }

    public override string ToString()
    {
        return lifeTime.ToString();
    }

    public void Tick()
    {
        while (Running)
        {
            //Thread.Sleep(1000 / (SecondPerReal * Speed));
            Thread.Sleep(5000);
            CalculateTime();
        }
    }

    public void CalculateTime()
    {
        Day++;
        OnDayPassed();
        /*Second++;

        if (Second >= MinuteSize)
        {
            Minute++;
            Second = Second - MinuteSize;
            OnMinutePassed();
        }

        if (Minute >= HourSize)
        {
            Hour++;
            Minute = Minute - HourSize;
            OnHourPassed();
        }

        if (Hour >= DaySize)
        {
            Day++;
            Hour = 0;
            OnDayPassed();
        }

        if (Day > DayPerMonth)
        {
            Month++;
            Day = 1;
        }

        if (Month >= MonthPerYear)
        {
            Year++;
            Month = 1;
        }*/
    }

    public void OnMinutePassed()
    {
        MinuteEvent?.Invoke(this, new TimerEventArgs(this));
    }

    public void OnHourPassed()
    {
        HourEvent?.Invoke(this, new TimerEventArgs(this));
    }

    public void OnDayPassed()
    {
        DayEvent?.Invoke(this, new TimerEventArgs(this));
    }

    public void OnMonthPassed()
    {
        MonthEvent?.Invoke(this, new TimerEventArgs(this));
    }
}
