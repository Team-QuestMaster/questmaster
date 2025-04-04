using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ReportUI : MonoBehaviour
{
    [SerializeField] private Image _report;
    [SerializeField] private TextMeshProUGUI _reportText;

    public void Initialize() { }

    public void ShowReportUI(string text)
    {
        _report.transform.DOMove(Vector3.zero, 1f);
        _reportText.text = text;
    }
}