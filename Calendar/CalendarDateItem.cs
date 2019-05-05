using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Phoenix_Kiber_Ogurchik
{
    public class CalendarDateItem : MonoBehaviour
    {
        public void OnDateItemClick()
        {
            CalendarController._calendarInstance.OnDateItemClick(gameObject.GetComponentInChildren<Text>().text);
        }
    }
}
