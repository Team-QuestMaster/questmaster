using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalenderManager : MonoBehaviour
{
  [SerializeField] private List<TextMeshProUGUI> _calenderText;


  public void AddCalenderText(int day ,string calenderText)
  {
    int indexDay = day - 1;
    if (_calenderText[indexDay].text == "")
    {
      _calenderText[indexDay].text = calenderText;
    }
    else
    {
      _calenderText[indexDay].text += $"\n{calenderText}";
    }
  }

  public void CellClear(int day)
  {
    int indexDay = day - 1;
    _calenderText[indexDay].text = "";
  }
  
  
}
