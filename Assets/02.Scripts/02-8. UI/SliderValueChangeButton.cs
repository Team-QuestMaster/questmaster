using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SliderValueChangeButton : MonoBehaviour
{
    enum Purpose
    {
        Subtraction = -1,
        Plus = 1,
    }
    
    [SerializeField]
    private Slider _targetSlider;

    [SerializeField]
    private Purpose _purpose = Purpose.Plus;

    [SerializeField]
    private float amount;

    private void Awake()
    {
        // 타겟이 없으면 자동으로 부모
        if (ReferenceEquals(_targetSlider, null))
        {
            if (!transform.parent.TryGetComponent(out _targetSlider))
            {
                Debug.LogError("타겟 슬라이더를 셋팅 혹은 버튼을 슬라이더의 자식으로 둬야합니다.");
            }
        }
        Button button = GetComponent<Button>();
        button.onClick.AddListener(() => _targetSlider.value += amount * (int)_purpose);
    }
}
