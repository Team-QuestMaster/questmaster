public class DailyReportData
{
    private int _beforeGold;
    public int BeforeGold { get { return _beforeGold; } set { _beforeGold = value; } }
    
    private int _afterGold;
    public int AfterGold { get { return _afterGold; } set { _afterGold = value; } }
    
    private int _beforeFame;
    public int BeforeFame { get { return _beforeFame; } set { _beforeFame = value; } }
    
    private int _afterFame;
    public int AfterFame { get { return _afterFame; } set { _afterFame = value; } }
    
    private string _questResult;
    public string QuestResult { get { return _questResult; } set { _questResult = value; } }
    
    private string _specialComment;
    public string SpecialComment { get { return _specialComment; } set { _specialComment = value; } }
    
    public void AddQuestResultText(string text)
    {
        if (string.IsNullOrEmpty(_questResult))
        {
            _questResult = text;
        }
        else
        {
            _questResult += $"\n{text}";
        }
    }

    public void AddSpecialCommentText(string text)
    {
        if(string.IsNullOrEmpty(_specialComment))
        {
            _specialComment = text;
        }
        else
        {
            _specialComment += $"\n{text}";
        }
    }
}
