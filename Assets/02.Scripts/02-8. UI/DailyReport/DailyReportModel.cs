using System;
using UnityEngine;

public class DailyReportModel : MonoBehaviour
{
    // 데일리 레포트에 나타날 데이터를 관리
    private DailyReportData _data;
    public DailyReportData Data
    {
        get => _data;
        set
        {
            _data = value;
            OnDailyReportChanged?.Invoke(_data);
        }
    }

    public event Action<DailyReportData> OnDailyReportChanged;

    public void AddSpecialCommentText(string comment)
    {
        Data.AddSpecialCommentText(comment);
    }
}
