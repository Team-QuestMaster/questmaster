using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ReportUI : MonoBehaviour
{
    [SerializeField] private Image _report;
    [SerializeField] private TextMeshProUGUI _goldChangeText;
    [SerializeField] private TextMeshProUGUI _fameChangeText;
    [SerializeField] private TextMeshProUGUI _questResultText;
    [SerializeField] private TextMeshProUGUI _specialCommentText;

    public void Initialize() { }

    public void ShowReportUI(string text)
    {
        _report.gameObject.SetActive(true);
        _report.transform.DOMove(Vector3.zero, 1f);
    }

    public void HideReportUI()
    {
        _report.transform.DOMove(new Vector3(0,1003,0), 1f).OnComplete(() => _report.gameObject.SetActive(false));
    }

    void TextClear()
    {
        _goldChangeText.text = "";
        _fameChangeText.text = "";
        _questResultText.text = "";
        _specialCommentText.text = "";
        
    }

    void GoldText(int before, int after)
    {
        _goldChangeText.text = $"{before} -> {after}";
    }

    void FameText(int before, int after)
    {
        _fameChangeText.text = $"{before} -> {after}";
    }

    void QuestResultTextAdd(string text)
    {
        if (_questResultText.text == "")
        {
            _questResultText.text = text;
        }
        else
        {
            _questResultText.text += $"\n{text}";
        }
    }

    void SpecialCommentText(string text)
    {
        if(_specialCommentText.text == "")
        {
            _specialCommentText.text = text;
        }
        else
        {
            _specialCommentText.text += $"\n{text}";
        }
    }
    
    
}