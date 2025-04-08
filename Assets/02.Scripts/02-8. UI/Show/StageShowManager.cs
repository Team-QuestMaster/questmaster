using UnityEngine;

public class StageShowManager : Singleton<StageShowManager>
{
    public ShowResult ShowResult;
    public ShowCharacter ShowCharacter;
    public ShowQuest ShowQuest;
    public ShowIDCard ShowIDCard;
    public MiniCharacterUI MiniCharacter;

    // 낮 등장 이벤트 모음
    public void Appear()
    {
        MiniCharacter.MakeMiniCharacters();   // 미니 캐릭터 셋팅
        ShowCharacter.Appear();               // LD 캐릭터 등장
        ShowQuest.Appear();                   // 퀘스트 등장
        ShowIDCard.Appear();
    }
}
