using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Phoenix_Kiber_Ogurchik
{
    public class Core : MonoBehaviour
    {
        public string newsUrl;
        public Transform curSelectedDay;
        public Transform curDayPosition;

        public DateTime startDate = new DateTime(2018, 8, 27);

        public Day[] days = new Day[6];

        public Parametrs parametrs;

        bool aboutProjectOpen;
        bool enterGroupOpen;
        bool newsOpen;
        bool calendarOpen;
        bool notesOpen;

        private int cur_day = 0;
        private List<GameObject> lessons_inst = new List<GameObject>();
        private string groupNumber = "";
        private bool weChengeGroup;
        private Week_Type curWeek = Week_Type.Chis;
        private Week_Type checkWeek;

        private int sp_day;
        private int sp_month;
        private DateTime sp_Date;

        private const string GetScheduleUrl = "http://localhost/ordeal_server_alfa_1/GetGroupRP.php";
        

        private void Start()
        {
            Screen.orientation = ScreenOrientation.Portrait;
            curSelectedDay.position = curDayPosition.position;
            cur_day = 0;

            GetCurrntWeek();

            if (PlayerPrefs.HasKey("Group")) //проверим на наличие сохраненного номера группы
                groupNumber = PlayerPrefs.GetString("Group");

            if (!string.IsNullOrEmpty(groupNumber)) //если мы получили номер группы, тогда получаем распиание
            {
                if(PlayerPrefs.HasKey("CurWeek"))
                {
                    parametrs.useSpecWeek = bool.Parse(PlayerPrefs.GetString("CurWeek"));

                    if (parametrs.useSpecWeek)
                        parametrs.useWeekToggler.isOn = true;
                    else
                        parametrs.defaultWeekToggler.isOn = true;
                }

                if (PlayerPrefs.HasKey("LastSession")) //проверим на наличие сохраненного расписания
                {
                    LoadSavedData(); //получим расписание из памяти
                }
                else
                    GetData(); //получим расписание через интернет

                parametrs.groupInput.text = groupNumber; //если номер группы сохранен, вставим его значение в поле ввода
            }
            else
            {
                parametrs.dropdown.value = 1; //перейдем к панели ввода группы еслт ранее группа не задавалась
            }
        }

        private void GetCurrntWeek()
        {
            DateTime curDate = DateTime.Now;
            DateTime counter = startDate;

            int weekT = 0;

            while (curDate.Date > counter.Date)
            {
                weekT++;
                counter = counter.Date.AddDays(7);

                if (curDate.Date <= counter.Date)
                {
                    if ((weekT % 2) == 0)
                        curWeek = Week_Type.Znam;
                    else
                        curWeek = Week_Type.Chis;

                    break;
                }
            }

            checkWeek = curWeek;
        }

        #region Menu
        public void SetMenuDropDown(int value) //действия при изменении знаачения Dropdown
        {
            switch (value)
            {
                case 0: //расписание
                    if (aboutProjectOpen)
                    {
                        parametrs.aboutProject.SetActive(false); //закроем панель если открыта
                        aboutProjectOpen = false;
                    }
                    if (enterGroupOpen)
                    {
                        parametrs.groupObject.SetActive(false); //закроем панель если открыта
                        enterGroupOpen = false;
                    }
                    if (newsOpen)
                    {
                        parametrs.newsPanel.SetActive(false); //закроем панель если открыта
                        enterGroupOpen = false;
                    }
                    if (calendarOpen)
                    {
                        parametrs.calendarPanel.SetActive(false); //закроем панель если открыта
                        calendarOpen = false;
                    }
                    if (notesOpen)
                    {
                        parametrs.notesPanel.SetActive(false);
                        notesOpen = false;
                    }

                    parametrs.groupInfo.text = "Группа: " + groupNumber + (parametrs.useSpecWeek ? " " + ((checkWeek == Week_Type.Chis) ? "ЧИСЛ" : "ЗНАМ") : "");

                    break;
                case 1: //настройки
                    if (aboutProjectOpen)
                    {
                        parametrs.aboutProject.SetActive(false); //закроем панель если открыта
                        aboutProjectOpen = false;
                    }
                    if (newsOpen)
                    {
                        parametrs.newsPanel.SetActive(false); //закроем панель если открыта
                        enterGroupOpen = false;
                    }
                    if (calendarOpen)
                    {
                        parametrs.calendarPanel.SetActive(false); //закроем панель если открыта
                        calendarOpen = false;
                    }
                    if (notesOpen)
                    {
                        parametrs.notesPanel.SetActive(false);
                        notesOpen = false;
                    }
                    parametrs.groupObject.SetActive(true); //откроем панель для задания номера группы
                    enterGroupOpen = true;
                    break;
                case 2: //календарь
                    if (aboutProjectOpen)
                    {
                        parametrs.aboutProject.SetActive(false); //закроем панель если открыта
                        aboutProjectOpen = false;
                    }
                    if (enterGroupOpen)
                    {
                        parametrs.groupObject.SetActive(false); //закроем панель если открыта
                        enterGroupOpen = false;
                    }
                    if (newsOpen)
                    {
                        parametrs.newsPanel.SetActive(false); //закроем панель если открыта
                        enterGroupOpen = false;
                    }
                    if(notesOpen)
                    {
                        parametrs.notesPanel.SetActive(false);
                        notesOpen = false;
                    }
                    parametrs.groupInfo.text = "Календарь";
                    parametrs.calendarPanel.SetActive(true);
                    calendarOpen = true;
                    break;
                case 3:
                    if (aboutProjectOpen)
                    {
                        parametrs.aboutProject.SetActive(false); //закроем панель если открыта
                        aboutProjectOpen = false;
                    }
                    if (enterGroupOpen)
                    {
                        parametrs.groupObject.SetActive(false); //закроем панель если открыта
                        enterGroupOpen = false;
                    }
                    if (newsOpen)
                    {
                        parametrs.newsPanel.SetActive(false); //закроем панель если открыта
                        enterGroupOpen = false;
                    }
                    if (calendarOpen)
                    {
                        parametrs.calendarPanel.SetActive(false); //закроем панель если открыта
                        calendarOpen = false;
                    }

                    parametrs.groupInfo.text = "Заметки";
                    parametrs.notesPanel.SetActive(true);
                    notesOpen = true;
                    break;
                case 4: //новости
                    if (aboutProjectOpen)
                    {
                        parametrs.aboutProject.SetActive(false); //закроем панель если открыта
                        aboutProjectOpen = false;
                    }
                    if (enterGroupOpen)
                    {
                        parametrs.groupObject.SetActive(false); //закроем панель если открыта
                        enterGroupOpen = false;
                    }
                    if (calendarOpen)
                    {
                        parametrs.calendarPanel.SetActive(false); //закроем панель если открыта
                        calendarOpen = false;
                    }
                    if (notesOpen)
                    {
                        parametrs.notesPanel.SetActive(false);
                        notesOpen = false;
                    }

                    parametrs.newsPanel.SetActive(true);
                    parametrs.rss_News.GetNews();
                    newsOpen = true;
                    parametrs.groupInfo.text = "Новости";
                    break;
                case 5: //о программе
                    if (enterGroupOpen)
                    {
                        parametrs.groupObject.SetActive(false); //закроем панель если открыта
                        enterGroupOpen = false;
                    }
                    if(newsOpen)
                    {
                        parametrs.newsPanel.SetActive(false); //закроем панель если открыта
                        enterGroupOpen = false;
                    }
                    if (calendarOpen)
                    {
                        parametrs.calendarPanel.SetActive(false); //закроем панель если открыта
                        calendarOpen = false;
                    }
                    if (notesOpen)
                    {
                        parametrs.notesPanel.SetActive(false);
                        notesOpen = false;
                    }
                    parametrs.aboutProject.SetActive(true); //откроем панель "О проекте"
                    aboutProjectOpen = true;
                    break;
            }
        }
        #endregion

        #region Schedule

        public void Generate() //генерация структуры расписания
        {
            if (lessons_inst.Count != 0)
            {
                for (int i = 0; i < lessons_inst.Count; i++)
                {
                    Destroy(lessons_inst[i]);
                    lessons_inst.RemoveAt(i);
                    i--;
                }
            }

            for (int l = 0; l < days[cur_day].lessons.Length; l++)
            {
                if (parametrs.useSpecWeek && (days[cur_day].lessons[l].week == checkWeek || days[cur_day].lessons[l].week == Week_Type.Default))
                {
                    GenLessons(l);
                }
                else if(!parametrs.useSpecWeek)
                {
                    GenLessons(l);
                }
            }
        }

        void GenLessons(int l)
        {
            GameObject go = Instantiate(parametrs.element_Prefab, parametrs.parent);
            Text t = go.GetComponentInChildren<Text>();

            t.text = GetCurentLesson(days[cur_day].lessons[l], go.GetComponent<Image>());

            lessons_inst.Add(go);
        }

        public void Set_Day(int day) //задаем день
        {
            if (cur_day != day)
            {
                cur_day = day;
                Generate();
            }
        }

        public void Set_Cur_Day(int day, int month)
        {
            sp_Date = new DateTime(DateTime.Now.Year, month, day);
        }

        public void ChengeDay(Transform newPosition) //изменяем позицию для выбранного дня
        {
            curDayPosition = newPosition;
            curSelectedDay.position = newPosition.position;
        } 

        string GetCurentLesson(Lesson lesson, Image lImg) //генерация текста для одного предмета
        {
            string l = "<size=6>";

            switch (lesson.hour)
            {
                case Hour.h8_m10:
                    l += "8:10";
                    break;
                case Hour.h9_m55:
                    l += "9:55";
                    break;
                case Hour.h11_m40:
                    l += "11:40";
                    break;
                case Hour.h13_m35:
                    l += "13:35";
                    break;
                case Hour.h15_m20:
                    l += "15:20";
                    break;
                case Hour.h17_m05:
                    l += "17:05";
                    break;
                case Hour.h18_m50:
                    l += "18:50";
                    break;
                case Hour.h20_m25:
                    l += "20:25";
                    break;
            }

            switch (lesson.week)
            {
                case Week_Type.Default:
                    l += " ";
					lImg.color = parametrs.default_color;
                    break;
                case Week_Type.Chis:
                    string w = " ЧИСЛ. ";
                    l += w;
                    lImg.color = parametrs.chisl_color;
                    break;
                case Week_Type.Znam:
                    w = " ЗНАМ. ";
                    l += w;
                    lImg.color = parametrs.znam_color;
                    break;
            }

            switch (lesson.lessonType)
            {
                case LessonType.LK:
                    l += "ЛК</size>\n";
                    break;
                case LessonType.PZ:
                    l += "ПЗ</size>\n";
                    break;
                case LessonType.LB2:
                    l += "ЛБ</size>\n";
                    break;
                case LessonType.LB4:
                    l += "ЛБ 4ч.</size>\n";
                    break;
                case LessonType.Null:
                    l += "</size>\n";
                    break;
            }

            l += lesson.lesson_Name + "\n<size=7>" + lesson.teachersName;

            if (!string.IsNullOrEmpty(lesson.room))
                l += " " + "а." + lesson.room + "</size>";
            else
                l += "</size>";

            if (!string.IsNullOrEmpty(lesson.dates))
                l += "\n<size=7>" + lesson.dates + "</size>";


            return l;
        } 

        public void LoadSavedData() //загрузка сохраненного расписания
        {
            string msg = PlayerPrefs.GetString("LastSession");
            GetCurrentList(msg);
        }

        public void GetCurrentList(string msg) //преобразование сообщения в расписание
        {
            string dop;

            dop = GetDataValue(msg, "Monday:", "|");
            if (!string.IsNullOrEmpty(dop))
                days[0].lessons = GetLessons(dop.ToString());

            dop = GetDataValue(msg, "Tuesday:", "|");
            if (!string.IsNullOrEmpty(dop))
                days[1].lessons = GetLessons(dop.ToString());

            dop = GetDataValue(msg, "Wednesday:", "|");
            if (!string.IsNullOrEmpty(dop))
                days[2].lessons = GetLessons(dop.ToString());

            dop = GetDataValue(msg, "Thursday:", "|");
            if (!string.IsNullOrEmpty(dop))
                days[3].lessons = GetLessons(dop.ToString());

            dop = GetDataValue(msg, "Friday:", "|");
            if (!string.IsNullOrEmpty(dop))
                days[4].lessons = GetLessons(dop.ToString());

            dop = GetDataValue(msg, "Saturday:", "|");
            if (!string.IsNullOrEmpty(dop))
                days[5].lessons = GetLessons(dop.ToString());

            Generate();

            parametrs.groupInfo.text = "Группа: " + groupNumber + (parametrs.useSpecWeek ? " " + ((checkWeek == Week_Type.Chis) ? "ЧИСЛ" : "ЗНАМ") : "");

            weChengeGroup = true;

            parametrs.dropdown.value = 0;
        } 
        #endregion

        #region News
        public void NewsPage(string url)
        {
            Application.OpenURL(url);
        }


        #endregion

        #region Netwroking
        public void SetGroup(string grNumber) //задаем группу
        {
            if (groupNumber != grNumber)
            {
                groupNumber = grNumber;

                weChengeGroup = false;
            }
            else
            {
                weChengeGroup = true;
            }
        }

        public void SetOnlyCurrentWeek(bool value)
        {
            if(parametrs.useSpecWeek != value)
            {
                parametrs.useSpecWeek = value;
                parametrs.useSpecialDayToggler.isOn = false;
                checkWeek = curWeek;
                PlayerPrefs.SetString("CurWeek", value.ToString());
            }
        }

        public void SetOnlyNextWeek(bool value)
        {
            if (parametrs.useSpecWeek != value)
            { 
                parametrs.useSpecWeek = value;

                if (curWeek == Week_Type.Chis)
                    checkWeek = Week_Type.Znam;
                else
                    checkWeek = Week_Type.Chis;
            }
        }

        public void SetSpecialDay(bool value)
        {
            parametrs.getSpecialDay = value;

            parametrs.specialDaySet.SetActive(value); 
            parametrs.specialMonthSet.SetActive(value);

            parametrs.useWeekToggler.isOn = false;
        }

        public void Set_SpecDay(int day)
        {
            sp_day = day;
        }

        public void Set_SpecMonth(int month)
        {
            sp_month = month;
        }

        void GetData() //получаем расписание если оно требуется
        {
            if (!weChengeGroup)
                StartCoroutine(GetDataFromPHP(groupNumber));
            else
            {
                parametrs.dropdown.value = 0;
                Generate();

                parametrs.groupInfo.text = "Группа: " + groupNumber + (parametrs.useSpecWeek ? " " + ((checkWeek == Week_Type.Chis) ? "ЧИСЛ" : "ЗНАМ") : "");
            }
        }

        IEnumerator GetDataFromPHP(string groupID) //отправляем запрос к php скрипту, получаем от него ответ и решаем что с ним делать
        {
            WWWForm form = new WWWForm();
            form.AddField("idPost", groupID);

            WWW www = new WWW(GetScheduleUrl, form);
            yield return www;
            string msg = Encoding.UTF8.GetString(www.bytes); //перекодируем ответ в UTF8

            if (string.IsNullOrEmpty(msg)) //если получен пустой ответ или вообще не получен выдаем ошибку
            {
                parametrs.errorPanel.SetActive(true);
                parametrs.errorPanel.GetComponentInChildren<Text>().text = parametrs.errorNullMsg;
            }
            else
            {
                if (msg.Contains("*")) //если есть данный символ, значит группа не найдена
                {
                    parametrs.errorPanel.SetActive(true);
                    parametrs.errorPanel.GetComponentInChildren<Text>().text = parametrs.errorGroupNotFound;
                }
                else
                {
                    PlayerPrefs.SetString("LastSession", msg); //сохраняем полученный результат
                    PlayerPrefs.SetString("Group", groupNumber); //сохраняем группу
                    GetCurrentList(msg);
                }
            }
        }

        string GetDataValue(string data, string index, string indexOf) //преобразования строки
        {
            string value = data.Substring(data.IndexOf(index) + index.Length);
            if (value.Contains(indexOf)) value = value.Remove(value.IndexOf(indexOf));
            return value;
        }

        Lesson[] GetLessons(string day) //генерация их строки раписания на один день
        {
            string[] Lessons = day.Split('!');

            Lesson[] cur_day = new Lesson[Lessons.Length];
            for (int i = 0; i < Lessons.Length; i++)
            {
                string[] data = Lessons[i].Split('/');
                Lesson l = new Lesson();
                l.week = (Week_Type)int.Parse(data[0]);
                l.lesson_Name = data[1];
                l.hour = (Hour)int.Parse(data[2]);
                l.lessonType = (LessonType)int.Parse(data[3]);
                l.room = data[4];
                l.teachersName = data[5];

                if (data.Length-1 == 6)
                    l.dates = data[6];

                cur_day[i] = l;
            }

            return cur_day;
        }
        #endregion
    }

    [System.Serializable]
    public class Day
    {
        public Lesson[] lessons;
    }

    [System.Serializable]
    public class Lesson
    {
        public Week_Type week;
        
        public string lesson_Name;
        public Hour hour;
        public LessonType lessonType;
        public string room;
        public string teachersName;
        public string dates;
    }

    [System.Serializable]
    public class Parametrs
    {
        public Dropdown dropdown;
        public Text groupInfo;
        public InputField groupInput;
        public Toggle useWeekToggler;
        public Toggle defaultWeekToggler;
        public Toggle useSpecialDayToggler;
        public GameObject groupObject;
        public GameObject aboutProject;
        public GameObject errorPanel;
        public GameObject specialDaySet;
        public GameObject specialMonthSet;
        public GameObject newsPanel;
        public GameObject calendarPanel;
        public GameObject notesPanel;
        public Transform parent;
        public GameObject element_Prefab;

        public rss_news rss_News;

        public bool useSpecWeek;
        public bool getSpecialDay;

		public Color default_color = Color.gray;
        public Color chisl_color = Color.cyan;
        public Color znam_color = Color.gray;
        public string errorNullMsg = "Ошибка, отсутствует соединение с сервером";
        public string errorGroupNotFound = "Ошибка, данная группа не найдена";
    }

    public enum Week_Type
    {
        Default, Chis, Znam
    }

    public enum Hour
    {
        h8_m10, h9_m55, h11_m40, h13_m35, h15_m20, h17_m05, h18_m50, h20_m25
    }

    public enum LessonType
    {
        LK, PZ, LB2, LB4, Null
    }
}
