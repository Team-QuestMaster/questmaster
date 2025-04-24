using UnityEngine;

public class StageShowManager : Singleton<StageShowManager>
{
    public ShowResult ShowResult;
    public ShowCharacter ShowCharacter;
    public UI_ShowQuest ShowQuest;
    public ShowIDCard ShowIDCard;
    public MiniCharacterUI MiniCharacter;

    // �� ���� �̺�Ʈ ����
    public void Appear()
    {
        MiniCharacter.MakeMiniCharacters();   // �̴� ĳ���� ����
        ShowCharacter.Appear();               // LD ĳ���� ����
       
    }

    public void AppearEventSet()
    {
        ShowCharacter.AppearEventSet();
        ShowQuest.AppearEventSet();
    }
}
