using UnityEngine;

public class StageShowManager : Singleton<StageShowManager>
{
    public ShowResult ShowResult;
    public ShowCharacter ShowCharacter;
    public ShowQuest ShowQuest;
    public ShowIDCard ShowIDCard;
    public MiniCharacterUI MiniCharacter;

    // �� ���� �̺�Ʈ ����
    public void Appear()
    {
        MiniCharacter.MakeMiniCharacters();   // �̴� ĳ���� ����
        ShowCharacter.Appear();               // LD ĳ���� ����
        ShowQuest.Appear();                   // ����Ʈ ����
        ShowIDCard.Appear();
    }

    public void AppearEventSet()
    {
        ShowCharacter.AppearEventSet();
        ShowQuest.AppearEventSet();
    }
}
