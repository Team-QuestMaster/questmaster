using System;
using UnityEngine;

public class DailyReportPresenter : MonoBehaviour
{
    [SerializeField]
    private DailyReportModel _dailyReportModel;
    
    private UI_DailyReport _dailyReportView;

    private void Awake()
    {
        _dailyReport = GetComponent<UI_DailyReport>();
    }

    private void Start()
    {
        _dailyReportModel.OnDailyReportChanged += _dailyReport.RefreshDailyReport;
    }

}
