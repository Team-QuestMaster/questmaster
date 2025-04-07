using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalenderManager : MonoBehaviour
{
  [SerializeField] private List<TextMeshProUGUI> _calenderText;


  /// <summary>
  /// 캘린더에 정보 추가
  /// </summary>
  /// <param name="day">추가할 날짜</param>
  /// <param name="calenderText">정보: 퀘스트 이름, 성공확률</param>
  public void AddCalenderText(int day ,string calenderText)
  {
    // TODO: 성공 확률에 따라 성공 확률 텍스트 컬러 변경
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
