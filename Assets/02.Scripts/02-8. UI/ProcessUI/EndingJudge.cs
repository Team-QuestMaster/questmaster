using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingJudge : MonoBehaviour
{
    
    [SerializeField]
    private TextMeshProUGUI _dialText;

    private int _currentDial;
    
    [SerializeField]
    private CanvasGroup[] _canvasGroups;
    
    [SerializeField] 
    private Image _endingImage;
    [SerializeField] 
    private TextMeshProUGUI _endingName;
    [SerializeField] 
    private TextMeshProUGUI _endingMent;
    [SerializeField]
    private TextMeshProUGUI _endingStatFame;
    [SerializeField]
    private TextMeshProUGUI _endingStatGold;
    [SerializeField]
    private TextMeshProUGUI _endingStatNumOfCompletedQuests;
    [SerializeField] 
    private int[] _questGrade = { 1, 2 }; // 예시 기준값, 실제 게임 값에 맞춰 조정 필요
    [SerializeField] 
    private int[] _moneyGrade = { 100, 500 }; // 예시 기준값, 실제 게임 값에 맞춰 조정 필요
    [SerializeField] 
    private int[] _fameGrade = { 5, 15 }; // 예시 기준값, 실제 게임 값에 맞춰 조정 필요

    private int _fame;
    private int _money;
    private int _quest;

    private int _index;
    
    
    
    [SerializeField] 
    private Sprite[] _endingImages = new Sprite[27]; // 크기 27로 조정 (사용자 직접 할당 필요)

    private string[] _endingDials =
    {
        "당신은 약 한 달간, 길드의 일을 수행해 왔습니다.",
        "그 시간 동안,",
        "수많은 모험가들을 만나고,",
        "여러 의뢰를 수주하며,",
        "그들과 함께 이 세계의 여러 면을 알아갔습니다.",
        "그렇게 쌓인 당신의 기록은,",
        "과연, 이 길드를 어떤 모습으로 빚어냈을까요?"
    };

    string[] _endingMents = new string[]
    {
    "한때는 모험과 희망으로 가득 찼던 작은 길드. 그러나 재정적 지원 없이 시작한 여정은 언제나 험난했다. 퀘스트는 드물었고, 후원자도 없었다. 하나둘씩 떠나는 동료들, 녹슬어가는 무기, 닫힌 의뢰 게시판. 그렇게 아무도 모르게 사라져간 그들. 무너진 벽과 비어버린 서재, 낡은 장비만이 이 길드가 존재했음을 증명한다. 언젠가 누군가 이 잿더미 속에서 작은 불씨 하나를 발견하길 바랄 뿐이다.", // LLL - 000
    "이 길드는 전설이 되고 싶었던 것이 아니라, 오늘을 살아내는 것이 목표였다. 거대한 적도, 신비한 유적도 아닌, 매일 반복되는 사냥과 배달, 경비 임무가 일상이었다. 모험이라는 단어보다는 노동에 가까웠던 삶. 효율을 추구하고 시간에 맞춰 마감하는 일상이 반복되었지만, 그 안에서도 서로를 챙기며 작지만 단단한 유대가 피어났다. 비록 역사에는 기록되지 않더라도, 이들은 자신들만의 방식으로 세상을 지탱했다.", // LLM - 001
    "찬란한 전투도, 위대한 승리도 없었다. 오직 일과 일, 그리고 또 일이 있었을 뿐이다. 거대한 도시의 가장 밑바닥에서 시작한 이 길드는, 땀과 피로 하루하루를 쌓아올렸다. 명예는 없어도, 이들은 늘 최선을 다했고, 어느새 마을 사람들은 이들을 없으면 안 될 존재로 여기게 되었다. 이곳은 노동의 신전, 평범하지만 성실한 이들이 일궈낸 거대한 의지의 집결체다.", // LLH - 002
    "누구도 주목하지 않았고, 누구도 기대하지 않았던 작은 모험단. 하지만 그들은 조용히, 그러나 꾸준히 퀘스트를 해결하고, 마을 사람들을 도왔다. 아이들에게는 웃음을, 노인들에게는 안식을, 상인들에게는 안전한 길을 제공했다. 풍요롭지 않았지만, 그 마음만은 가장 부유했다. 이들이 심은 작은 선의의 씨앗은, 시간이 지나 더 넓은 땅으로 뿌리를 내리고 있었다.", // LML - 010
    "이 길드는 늘 누군가를 위해 존재했다. 위험을 무릅쓰고 약자를 구했고, 보상이 적어도 퀘스트를 마다하지 않았다. 큰 이름을 남긴 적은 없지만, 여행자들은 이들의 이야기를 전설처럼 입에 담았다. 비록 금전적으로 풍요롭지는 않았지만, 그들이 나눈 온정은 도시 구석구석에 스며들어 있었고, 이따금 무명의 사람들이 감사의 선물을 놓고 간다. 따뜻한 마음으로 이어진 유산은, 세상 그 무엇보다 강했다.", // LMM - 011
    
    "누구도 이들을 칭송하지 않았다. 의뢰 게시판에서 흔히 볼 수 있는 이름들 중 하나였고, 기사단의 후광도 없었다. 그러나 이들은 자신이 맡은 일을 묵묵히 해냈고, 위기의 순간마다 망설이지 않았다. 병든 아이를 치료하고, 무너진 마을을 복구하고, 전쟁 직전의 마을에 평화를 가져왔다. 그 누구도 그들의 이름을 기억하진 못하지만, 그들이 남긴 결과는 수천 명의 삶을 바꿔놓았다. 이들은 진정한 의미에서의 영웅이었다.", // LMH - 012
    "이들은 언제나 휘장과 훈장을 쫓았다. 명성이란 이름의 사슬에 묶여, 진짜 의미를 잃어가면서도 더 높은 평판을 얻기 위해 경쟁했다. 실속은 없었고, 구성원들의 마음은 점점 멀어졌으며, 내부는 공허한 껍데기만이 되었다. 결국 마지막으로 남은 것은 먼지 쌓인 상패와 가짜 미소 뿐이었다. 외부에서는 위대하게 보일지 몰라도, 그 속을 들여다본다면 그저 잊혀져야 할 망령에 불과했다.", // LHL - 020
    "금은 없었지만, 이들은 끝없는 도전과 탐구심으로 가득 찼다. 매일 밤늦도록 연구하고, 새로운 방식으로 퀘스트를 해결하며 그들만의 방식을 확립했다. 명성은 따라오지 않았지만, 그들이 남긴 기록과 방법론은 후대의 모험가들에게 큰 영향을 끼쳤다. 이들은 이상주의자였고, 동시에 실천가였다. 화려하진 않아도, 그들만의 작은 실험실은 세상의 변화를 예고하는 조용한 반란의 무대였다.", // LHM - 021
    "돈은 없었지만 명성은 있었다. 퀘스트도 많이 했고, 이름도 알려졌다. 그러나 그것은 언제나 허약한 토대 위에 세워졌다. 내부는 갈등이 끊이지 않았고, 재정은 언제나 빠듯했다. 결국 작은 실수가 도미노처럼 이어졌고, 그들은 아무 말 없이 해산했다. 기록 속 그들의 문장은 지금은 해독되지 않는다. 마치 부서진 언어처럼 남아, 그 의미를 잃었다.", // LHH - 022
    "풍족한 자금과 카리스마 있는 리더, 그리고 사람들의 기대를 등에 업고 시작한 이 길드는, 점차 오만과 욕망의 화염에 휩싸였다. 명성에 집착하고, 실리를 외면한 결과는 내부 분열과 붕괴였다. 찬란한 깃발은 나부끼지만, 그 안엔 고통과 배신, 그리고 잊혀진 약속들만이 남아 있다. 이 길드는 성공에 가까웠으나, 결국 그 불꽃은 타인을 태우고 스스로를 잿더미로 만들었다.", // HLL - 200
    
    "이들은 금전적 어려움 속에서도 작은 기적을 만들어냈다. 길드원 각자의 재능은 평범했지만, 서로를 믿고 도왔기에 가능했던 수많은 기회들. 부유하진 않았지만, 그들의 퀘스트는 늘 사람들의 환대를 받았다. 그들은 이름보다 사람을, 명성보다 신뢰를 쌓았다. 그리고 그 진심은 수많은 거리에 새겨졌다. 이들은 영웅도, 전설도 아니었지만, 누군가에게는 잊을 수 없는 빛이었다.", // HLM - 201
    "이 길드는 명성과 실리를 모두 추구했다. 그리고 그것에 성공했다. 매끄러운 시스템과 명확한 의사결정, 그리고 타협 없는 성과 중심의 운영. 이들은 수많은 퀘스트를 성공시켰고, 그 기록은 중앙 도서관의 한 켠에 ‘불멸의 사서단’이라는 이름으로 남게 되었다. 길드원들의 삶은 바쁘고 고단했지만, 그만큼 보람찼으며, 이들은 자기 몫 이상의 성과를 남겼다. 이 세상이 기억할 이름이 있다면, 바로 이들이다.", // HLH - 202
    "돈도, 명성도 적절히 부족했지만, 이 길드는 구성원 간의 신뢰와 우정으로 가득했다. 그들은 마치 작은 왕국처럼 서로를 돌보았고, 공동체를 이루었다. 외부의 평가와 상관없이, 그들만의 질서와 가치가 있었다. 이는 거대한 제국은 만들지 못했지만, 따뜻한 전통과 지속 가능한 문화로 이어졌다. 언젠가, 이곳에서 진짜 왕국이 태어나도 놀라지 않을 것이다.", // MLL - 100
    "길드는 특별한 사건 없이 잔잔하게 흘러갔다. 하지만 이들의 시간은 단단히 쌓였다. 차근차근 퀘스트를 해결하며, 대단하지 않지만 확실한 성취를 남긴 이들은 결국 한 지역의 전설이 되었다. 누군가는 이 길드를 ‘심심하다’고 했지만, 실제로 이들은 가장 이상적인 공동체 중 하나였다. 이름보다 실천이, 속도보다 방향이 중요하다는 것을 증명해낸 이들. 이들은 진정한 의미의 전설이었다.", // MLM - 101
    "이들은 명성을 바라보며 밤하늘을 올려다보았다. 누구보다 먼 꿈을 꾸었지만, 실리를 놓치지 않았고, 팀워크도 단단했다. 그들의 길은 거칠었지만, 서로에게 별이 되어 길을 밝혀주었다. 그 결과, 길드는 점점 유명해졌고, 각지에서 의뢰가 쏟아졌다. 이들은 별의 이름을 따서 길드 지부를 세우며 확장했고, 꿈을 현실로 만든 사례로 남게 되었다. 이들의 전설은 아직도 별자리로 이어지고 있다.", // MLH - 102
    
    "꿈을 품고 시작한 길드. 돈과 명성은 적었지만, 모두가 ‘우리의 이상’을 믿었다. 그러나 시간이 지나며 피로는 쌓이고, 기대는 실망으로 바뀌었다. 각자의 삶이 무너질수록 그 아름다웠던 공동체는 균열을 드러냈다. 결국 누구도 탓하지 않은 채, 조용히 흩어져 간 그들. 그 이상은 여전히 아름답지만, 누구도 다시는 그것을 말하지 않았다.", // MML - 110
    "이들은 모든 것을 조금씩 가졌고, 아무것도 완전히 갖지 못했다. 그래서일까, 이 길드는 늘 떠나는 이들이 많았다. 하지만 동시에 늘 누군가는 들어왔다. 변덕스럽고 유연하며, 자유로운 이 공동체는 한 자리에 머물기보단 흐르는 길이 되었다. 성장은 느렸지만, 그만큼 다양한 문화를 품었고, 언젠가 그 혼란 속에서 독특한 전통이 만들어졌다. 이들은 ‘형태 없는 길드’로 기억된다.", // MMM - 111
    "명성도 얻었고, 실리도 챙겼지만 그 과정은 매끄럽지 않았다. 내부의 갈등과 수많은 실패, 구조조정과 이탈. 그러나 매번 이들은 다시 꿰맸다. 길드는 마치 자주 찢어진 옷처럼 누덕누덕하지만, 그래도 입을 수 있었다. 중요한 건 꿰맨다는 사실이었다. 누구도 완벽하지 않았지만, 포기하지 않았기에 이들은 여기까지 왔다. 그 흔적들 속엔 의외로 많은 진심이 남아 있다.", // MMH - 112
    "명성은 하늘을 찔렀고, 검술 대회에서는 늘 우승했으며, 도시 곳곳에서 이름이 울려 퍼졌다. 그러나 그 실리는 언제나 불안정했다. 유지비는 높았고, 경쟁은 치열했으며, 내부는 늘 극한의 스트레스를 안고 있었다. 이들은 화려한 검무를 추었지만, 늘 칼끝에 서 있었다. 그리고 어느 날, 작은 균열이 치명적인 단절이 되었다. 그들은 춤을 멈추었고, 광장의 조명은 꺼졌다.", // MHL - 120
    "강한 자만이 살아남는 곳. 이 길드는 철저히 실리를 추구했으며, 그 과정에서 수많은 길드가 무너졌다. 그러나 결과적으로 명성도 따라왔다. 이들은 냉정했고, 효율적이었으며, 잔인하기도 했다. 길드원은 자주 교체되었고, 서로에 대한 신뢰보다 능력이 중요했다. 그런 이들이 왕관을 썼지만, 사람들은 그것을 ‘낙인’이라 불렀다. 오직 결과로 증명한 이들은, 결국 자신들도 그 무게에 짓눌리게 되었다.", // MHM - 121
    
    "명성과 실리를 모두 거머쥐었지만, 그것은 불안정한 성공이었다. 무리한 확장과 자만, 그리고 권력 다툼은 길드를 안에서부터 무너뜨렸다. 외부에서는 ‘전설’이라 불렸지만, 내부에서는 ‘끝’을 준비하고 있었다. 결국 화려한 외피는 껍질처럼 벗겨졌고, 길드는 몰락했다. 그러나 그 유산은 남아 후대의 반면교사로 회자되었다. 실패였지만, 위대한 실패였다.", // MHH - 122
    "실리는 있었지만, 사람은 없었다. 이 길드는 효율과 구조는 완벽했지만, 따뜻함이 없었다. 어느 순간부터 길드하우스의 식탁은 비어 있었다. 명성도 돈도 흘렀지만, 축배는 오지 않았다. 그리고 언젠가, 조용히 문을 닫았다. 그들은 누구보다 일을 잘했지만, 누구보다 외로웠다.", // HMM - 211
    "명성은 넘쳤지만, 실리도 감정도 부족했던 이들. 퀘스트는 많았지만 만족은 적었다. 그래서일까, 이 길드는 점점 무언가를 갈망하게 되었다. 어느 날, 그들은 전혀 다른 삶을 선택했다. 모닥불 앞에서 북을 두드리며, 새벽을 기다리는 이들. 지금도 어딘가에서 그들의 북소리가 들려온다고 한다. 그것은 변화의 전조, 혹은 새로운 시작.", // HML - 210
    "이들은 실력을 바탕으로 한 명성과 성취를 모두 이루었다. 하지만 그 순간을 지나자 모든 것이 정체되었다. 새로운 도전을 두려워했고, 과거의 영광을 반복하려 했다. 결국 이 길드는 ‘현재’가 아닌 ‘과거’로 존재하게 되었고, 사람들은 그들을 박물관에서만 보게 되었다. 그들의 명성은 영원하지만, 생명력은 사라졌다.", // HHM - 221
    "명성과 실리는 충분했다. 그러나 갈수록 퀘스트는 어려워졌고, 후속 세대는 자라지 않았다. 이들은 점점 늙어갔고, 한 명씩 줄어갔다. 그리고 마침내 마지막 퀘스트를 위해 남은 이들이 모였다. 눈빛은 여전히 뜨거웠고, 의지는 굳건했다. 그날 밤, 별이 유난히 밝았다. 그들은 돌아오지 않았지만, 누구도 그들을 잊지 않았다.", // HMH - 212
    
    "이들은 돈, 명성, 퀘스트 모두 완벽했다. 최고의 이상향이자, 모두가 꿈꾸던 길드. 그러나 그만큼의 부담도 컸다. 끝없는 기대와 경쟁 속에, 이들은 마지막 순간에 스스로 문을 닫기로 했다. 영원히 빛나기 위해, 스스로를 종언에 봉인한 것. 그 이후로, 이 길드의 이름은 금기어처럼 남았고, 전설이 되었다. 전설은, 끝났기에 아름다웠다.", // HHH - 222
    "돈은 없었지만 명성은 있었다. 퀘스트도 많이 했고, 이름도 알려졌다. 그러나 그것은 언제나 허약한 토대 위에 세워졌다. 내부는 갈등이 끊이지 않았고, 재정은 언제나 빠듯했다. 결국 작은 실수가 도미노처럼 이어졌고, 그들은 아무 말 없이 해산했다. 기록 속 그들의 문장은 지금은 해독되지 않는다. 마치 부서진 언어처럼 남아, 그 의미를 잃었다."  // LHH - 022
    };
    string[] _endingNames = new string[]
    {
        "잊혀진 불씨", // 000
        "길드 노동조합 제7지부", // 001
        "노동자의 전당", // 002
        "고요한 씨앗", // 010
        "정 많은 사람들", // 011
        
        "무명의 영웅들", // 012
        "명예에 묶인 망령", // 020
        "실험실의 도전자들", // 021
        "부서진 문장", // 022
        "검은 불꽃의 유산", // 200
        
        "반짝이는 거리의 이름 없는 별", // 201
        "불멸의 사서단", // 202
        "다정한 왕국", // 100
        "잔잔한 전설", // 101
        "별 헤는 자들의 연대기", // 102
        
        "천천히 무너진 이상향", // 110
        "바람을 걷는 자들", // 111
        "천을 꿰맨 자국처럼", // 112
        "칼날 위의 춤", // 120
        "낙인과 왕관", // 121
        
        "위대한 실패의 유산", // 122
        "오지 않는 만찬", // 210
        "영광의 박제", // 211
        "마지막 영웅단", // 212
        "전설의 종언", // 222
        
        "새벽을 부르는 북소리", // 221
        "부서진 문장"  // 210
    };

    private void Start()
    {
        GetGuildStat();
        JudgeEnding();
        _currentDial = 0;
        _dialText.DOText(_endingDials[_currentDial],1f);
    }

    public void EndingMentsButton()
    {
        _canvasGroups[1].alpha = 0;
        _canvasGroups[2].alpha = 1;
        _canvasGroups[2].interactable = true;
        _canvasGroups[2].blocksRaycasts = true;
        _endingName.text = "";
        _endingName.DOText(GetEndingName(_index), 3f).SetDelay(0.1f);
        switch (_fame)
        {
            case 0:
                _endingStatFame.text = "하";
                break;
            
            case 1:
                _endingStatFame.text = "중";
                break;
            
            case 2:
                _endingStatFame.text = "상";
                break;
            
        }
        
        
        switch (_money)
        {
            case 0:
                _endingStatGold.text = "하";
                break;
            
            case 1:
                _endingStatGold.text = "중";
                break;
            
            case 2:
                _endingStatGold.text = "상";
                break;
            
        }
        
        switch (_quest)
        {
            case 0:
                _endingStatNumOfCompletedQuests.text = "하";
                break;
            
            case 1:
                _endingStatNumOfCompletedQuests.text = "중";
                break;
            
            case 2:
                _endingStatNumOfCompletedQuests.text = "상";
                break;
            
        }
        
        _endingStatFame.DOFade(1, 0.1f).SetDelay(0.1f);
        _endingStatGold.DOFade(1, 0.1f).SetDelay(0.1f);
        _endingStatNumOfCompletedQuests.DOFade(1, 0.1f).SetDelay(0.1f);
    }
    
    public void ShowDial()
    {
        _canvasGroups[0].interactable = false;
        _canvasGroups[0].blocksRaycasts = false;

        _canvasGroups[1].DOFade(1, 0.5f);
        _canvasGroups[1].interactable = true;
        _canvasGroups[1].blocksRaycasts = true;

        _endingMent.text = "";
        _endingMent.DOText(GetEndingMent(_index), 10f).SetDelay(0.1f);

    }
    
    public void NextDial()
    {
        _currentDial++;
        if (_currentDial < _endingDials.Length)
        {
            _dialText.DOKill();
            _dialText.text = "";
            _dialText.DOText(_endingDials[_currentDial], 1f,false, ScrambleMode.None);
        }
        else
        {
            // 엔딩 데이터 가져오기
            Sprite endingImage = GetEndingImage(_index);
            string endingMent = GetEndingMent(_index);
            string endingName = GetEndingName(_index);
            
            // 결과 처리
            if (endingImage != null && endingMent != null && endingName != null)
            {
                EndingShow(_index);
            }
            else
            {
                Debug.LogError($"Error: Could not retrieve ending data for index {_index}. " +
                               "Please ensure all ending arrays (_endingImages, _endingMents, _endingNames) " +
                               "are properly populated and have the correct size (27).");
            }
        }
    }
    
    private void GetGuildStat()
    {
        _fame = GuildStatManager.Instance.Fame;
        _money = GuildStatManager.Instance.Gold;
        _quest = GuildStatManager.Instance.NumOfCompletedQuests;
    }
    private void JudgeEnding()
    {
        GradeEnding(); // 먼저 등급화

        // 3진법을 사용하여 인덱스 계산 (0 ~ 26)
        _index = _fame * 9 + _quest * 3 + _money;
        Debug.Log(_index);

    }
    private void GradeEnding()
    {
        if (_fame < _fameGrade[0])
        {
            _fame = 0;
        }
        else if (_fameGrade[0] <= _fame && _fame < _fameGrade[1])
        {
            _fame = 1;
        }
        else
        {
            _fame = 2;
        }

        if (_money < _moneyGrade[0])
        {
            _money = 0;
        }
        else if (_moneyGrade[0] <= _money && _money < _moneyGrade[1])
        {
            _money = 1;
        }
        else
        {
            _money = 2;
        }

        if (_quest < _questGrade[0])
        {
            _quest = 0;
        }
        else if (_questGrade[0] <= _quest && _quest < _questGrade[1])
        {
            _quest = 1;
        }
        else
        {
            _quest = 2;
        }
        
        Debug.Log($"명성치 : {_fame},돈 : {_money}, 퀘스트 : {_quest}");
    }
    void EndingShow(int endingIndex)
    {
        _canvasGroups[3].DOFade(0, 1f);
        _canvasGroups[3].interactable = false;
        _canvasGroups[3].blocksRaycasts = false;
        
        
        _canvasGroups[0].DOFade(1, 1f);//엔딩 이미지 출연
        _endingImage.sprite = GetEndingImage(endingIndex);
        _canvasGroups[0].interactable = true;
        _canvasGroups[0].blocksRaycasts = true;
        
        
        GetEndingStat();
    }
    private Sprite GetEndingImage(int index)
    {
        if (index >= 0 && index < _endingImages.Length)
        {
            return _endingImages[index];
        }
        else
        {
            Debug.LogError($"Error: Ending image index ({index}) is out of bounds (0-{_endingImages.Length - 1}).");
            return null; // 또는 기본 이미지 반환
        }
    }
    private string GetEndingMent(int index)
    {
        if (index >= 0 && index < _endingMents.Length)
        {
            return _endingMents[index];
        }
        else
        {
            Debug.LogError($"Error: Ending ment index ({index}) is out of bounds (0-{_endingMents.Length - 1}).");
            return null; // 또는 기본 텍스트 반환
        }
    }
    private string GetEndingName(int index)
    {
        if (index >= 0 && index < _endingNames.Length)
        {
            return _endingNames[index];
        }
        else
        {
            Debug.LogError($"Error: Ending name index ({index}) is out of bounds (0-{_endingNames.Length - 1}).");
            return null; // 또는 기본 이름 반환
        }
    }
    private void GetEndingStat()
    {
        _endingStatFame.text = _fame.ToString();
        _endingStatGold.text = _money.ToString();
        _endingStatNumOfCompletedQuests.text = _quest.ToString();
    }
}