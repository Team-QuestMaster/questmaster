using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalenderManager : MonoBehaviour
{
  [SerializeField] private List<TextMeshProUGUI> _calenderText;


  public void AddCalenderText(int day ,string calenderText)
  {
    if (_calenderText[day].text == "")
    {
      _calenderText[day].text = calenderText;
    }
    else
    {
      _calenderText[day].text += $"\n{calenderText}";
    }
  }

  public void CellClear(int day)
  {
    _calenderText[day].text = "";
  }
  
  
}
