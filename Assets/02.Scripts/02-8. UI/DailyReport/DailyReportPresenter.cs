using System;
using UnityEngine;

public class DailyReportPresenter : MonoBehaviour
{
    [SerializeField]
    private DailyReportModel _dailyReportModel;
    
    private UI_DailyReport _dailyReport;

    private void Awake()
    {
        _dailyReport = GetComponent<UI_DailyReport>();
    }

    private void Start()
    {
        _dailyReportModel.OnDailyReportChanged += OnChangeDailyReportData;
    }

    private void OnChangeDailyReportData(DailyReportData data)
    {
        _dailyReport.RefreshDailyReport(data);
    }
}
