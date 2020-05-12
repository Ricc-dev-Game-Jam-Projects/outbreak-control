using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl
{
    public LifeTime MyCurrentTime;

    private Timer timer;

    public TimeControl(Timer timer)
    {
        this.timer = timer;

        MyCurrentTime = timer.lifeTime;

        timer.HourEvent += Timer_HourEvent;
    }

    private void Timer_HourEvent(object sender, TimerEventArgs time)
    {
        if(time.timer.Hour >= 21 && time.timer.Hour <= 24)
        {
            GoodNight();
            timer.Day++;
            SetDay();
            timer.CalculateTime();
            timer.OnDayPassed();
        }
        if(time.timer.Hour == 8)
        {
            GoodMorning();
        }
        MyCurrentTime = timer.lifeTime;
    }

    public void SpeedDay()
    {
        timer.Hour = 20;
        timer.Minute = 59;
    }

    public void SpeedMonth()
    {
        timer.Day = Timer.DayPerMonth;
        SetDay();
        timer.OnMonthPassed();
    }

    private void SetDay()
    {
        timer.Hour = 8;
        timer.Minute = 0;
        timer.Second = 0;
    }

    private void GoodNight()
    {
        Debug.Log("<color=purple>Boa noite! São 21horas...</color>");
        Debug.Log("<color=purple>Dormindo...</color>");
    }

    private void GoodMorning()
    {
        Debug.Log("<color=purple>Bom dia! Data de hoje >>> " + timer.ToString() + "</color>");

    }
}

