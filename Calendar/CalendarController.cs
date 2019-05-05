using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Phoenix_Kiber_Ogurchik
{
    public class CalendarController : MonoBehaviour
    {
        public GameObject _calendarPanel;
        public GameObject _OnClickPanle;
        public Text _yearNumText;
        public Text _monthNumText;
        public Color nowColor = Color.gray;
        public Color defaulColor = Color.white;

        public Core core;

        public GameObject _item;

        public List<GameObject> _dateItems = new List<GameObject>();
        const int _totalDateNum = 42;

        private DateTime _dateTime;
        public static CalendarController _calendarInstance;

        NoteElement noteElement;

        void Start()
        {
            noteElement = _OnClickPanle.GetComponent<NoteElement>();
            noteElement.core = core;

            _calendarInstance = this;
            Vector3 startPos = _item.transform.localPosition;
            _dateItems.Clear();
            _dateItems.Add(_item);

            for (int i = 1; i < _totalDateNum; i++)
            {
                GameObject item = GameObject.Instantiate(_item) as GameObject;
                item.name = "Item" + (i + 1).ToString();
                item.transform.SetParent(_item.transform.parent);
                item.transform.localScale = Vector3.one;
                item.transform.localRotation = Quaternion.identity;
                item.transform.localPosition = new Vector3((i % 7) * 31 + startPos.x, startPos.y - (i / 7) * 25, startPos.z);

                _dateItems.Add(item);
            }

            _dateTime = DateTime.Now;

            CreateCalendar();
        }

        void CreateCalendar()
        {
            DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
            int index = GetDays(firstDay.DayOfWeek);

            int date = 0;
            for (int i = 0; i < _totalDateNum; i++)
            {
                Text label = _dateItems[i].GetComponentInChildren<Text>();
                _dateItems[i].SetActive(false);

                if (i >= index)
                {
                    DateTime thatDay = firstDay.AddDays(date);
                    if (thatDay.Month == firstDay.Month)
                    {
                        _dateItems[i].SetActive(true);

                        label.text = (date + 1).ToString();

                        if (thatDay.Date == DateTime.Now.Date)
                            _dateItems[i].GetComponent<Image>().color = nowColor;
                        else
                            _dateItems[i].GetComponent<Image>().color = defaulColor;

                        date++;
                    }
                }
            }
            _yearNumText.text = _dateTime.Year.ToString();
            _monthNumText.text = GetMonth(_dateTime.Month);
        }

        int GetDays(DayOfWeek day)
        {
            switch (day)
            {
                case DayOfWeek.Monday: return 0;
                case DayOfWeek.Tuesday: return 1;
                case DayOfWeek.Wednesday: return 2;
                case DayOfWeek.Thursday: return 3;
                case DayOfWeek.Friday: return 4;
                case DayOfWeek.Saturday: return 5;
                case DayOfWeek.Sunday: return 6;
            }

            return 0;
        }

        string GetMonth(int m)
        {
            switch (m)
            {
                case 1: return "Январь";
                case 2: return "Февраль";
                case 3: return "Март";
                case 4: return "Апрель";
                case 5: return "Май";
                case 6: return "Июнь";
                case 7: return "Июль";
                case 8: return "Август";
                case 9: return "Сентябрь";
                case 10: return "Октябрь";
                case 11: return "Ноябрь";
                case 12: return "Декабрь";
            }

            return "Январь";
        }

        public void YearPrev()
        {
            _dateTime = _dateTime.AddYears(-1);
            CreateCalendar();
        }

        public void YearNext()
        {
            _dateTime = _dateTime.AddYears(1);
            CreateCalendar();
        }

        public void MonthPrev()
        {
            _dateTime = _dateTime.AddMonths(-1);
            CreateCalendar();
        }

        public void MonthNext()
        {
            _dateTime = _dateTime.AddMonths(1);
            CreateCalendar();
        }

        Text _target;
        public void OnDateItemClick(string day)
        {
            _OnClickPanle.SetActive(true);
            noteElement.SetElement(int.Parse(day), _dateTime.Month, _dateTime.Year);

            //отправляем на панель ифнорамцию о той дате по которой мы кликнули
            //при создании заметки сохраянем эту дату и при отображении в календаре меняем ей цвет

            //в панели заметок можно создать заметку или посмотреть расписание на это день
        }
    }
}
