using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Phoenix_Kiber_Ogurchik
{
    public class NoteElement : MonoBehaviour
    {
        public int day;
        public int month;
        public int year;
        public Core core;

        public GameObject[] days;

        public void SetElement(int _day, int _month, int _year)
        {
            day = _day;
            month = _month;
            year = _year;
        }

        public void GetTimeTable()
        {
            core.SetMenuDropDown(0);

            DateTime date = new DateTime(year, month, day);

            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    core.Set_Day(0);
                    core.ChengeDay(days[0].transform);
                    break;
                case DayOfWeek.Monday:
                    core.Set_Day(0);
                    core.ChengeDay(days[0].transform);
                    break;
                case DayOfWeek.Tuesday:
                    core.Set_Day(1);
                    core.ChengeDay(days[1].transform);
                    break;
                case DayOfWeek.Wednesday:
                    core.Set_Day(2);
                    core.ChengeDay(days[2].transform);
                    break;
                case DayOfWeek.Thursday:
                    core.Set_Day(3);
                    core.ChengeDay(days[3].transform);
                    break;
                case DayOfWeek.Friday:
                    core.Set_Day(4);
                    core.ChengeDay(days[4].transform);
                    break;
                case DayOfWeek.Saturday:
                    core.Set_Day(5);
                    core.ChengeDay(days[5].transform);
                    break;
            }

            gameObject.SetActive(false);
        }

        public void SetNewNote()
        {
            core.SetMenuDropDown(3);
            gameObject.SetActive(false);
        }
    }
}
