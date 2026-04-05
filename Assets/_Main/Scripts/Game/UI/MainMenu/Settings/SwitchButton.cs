using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
    [SerializeField] private Image _arrow;

    [Space]
    [SerializeField] private Visual _visual;
    
    public event Action Pressed;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayAnimation(_visual.EnterColor, _visual.EnterScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayAnimation(_visual.DefaultColor, _visual.DefaultScale);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayAnimation(_visual.DownColor, _visual.DownScale);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayAnimation(_visual.DefaultColor, _visual.DefaultScale);
        Pressed?.Invoke();
    }

    private void PlayAnimation(Color color, float scale)
    {
        _arrow.transform.DOScale(scale, _visual.AnimationTime).SetEase(Ease.Linear);
        _arrow.DOColor(color, _visual.AnimationTime).SetEase(Ease.Linear);
    }
    
    [Serializable]
    private class Visual
    {
        public Color DefaultColor;
        public Color EnterColor;
        public Color DownColor;

        [Space]
        public float DefaultScale;
        public float EnterScale;
        public float DownScale;

        public float AnimationTime = 0.3f;
    }
}