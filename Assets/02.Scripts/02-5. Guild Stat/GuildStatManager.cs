using System;
using UnityEngine;

public class GuildStatManager : Singleton<GuildStatManager>
{
    // 길드 수치 관리
    // 명성, 돈
    // 명성은 0~올릴 수 있는 만큼
    // 돈은 세금으로 음수 가능, 아이템 구매 시 음수로 구매 불가~올릴 수 있는 만큼

    protected override void Awake()
    {
        base.Awake();
    }

    private int _fame;
    public int Fame
    {
        get => _fame;
        set
        {
            _fame = Mathf.Max(0, value);
            OnFameChanged?.Invoke();
        }
    }

    private int _gold;
    public int Gold
    {
        get => _gold;
        set
        {
            _gold = value;
            OnGoldChanged?.Invoke();
        }
    }

    public event Action OnFameChanged;
    public event Action OnGoldChanged;
    
    /// <summary>
    /// 기능: 돈이 사용이 가능한지 확인 후 사용
    /// 즉, 돈이 amount이상 있는 경우 소모
    /// Tip: 0이하로 내리고 싶은 경우 Gold속성 활용
    /// </summary>
    /// <param name="amount">소모량</param>
    /// <returns>amount만큼 소모 가능 = true, 사용 불가능 = false</returns>
    public bool TryConsumeGold(int amount)
    {
        if (_gold - amount < 0)
        {
            return false;
        }
        
        _gold -= amount;
        OnGoldChanged?.Invoke();
        return true;
    }
}