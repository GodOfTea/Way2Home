using System;
using DG.Tweening;
using Infrastructure.Services.Audio;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler
{
    [Header("Screens")]
    [SerializeField] private ScreenBase _backTo;
    [SerializeField] private ScreenBase _currentScreen;

    [Header("Audio")]
    [SerializeField] private AudioTrack _clickSound;

    [Header("Elements")]
    [SerializeField] private Image _arrow;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private Visual _visual;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayAnimation(_visual.EnterColor, _visual.EnterScale);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayAnimation(_visual.DefaultColor, _visual.DefaultScale);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayAnimation(_visual.DefaultColor, _visual.DefaultScale);

        _clickSound.PlayOneShot();
        _currentScreen.HideImmediate();
        _backTo.Show();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayAnimation(_visual.DownColor, _visual.DownScale);
    }

    private void PlayAnimation(Color color, float scale)
    {
        _arrow.transform.DOScale(scale, _visual.AnimationTime).SetEase(Ease.Linear);
        _text.transform.DOScale(scale, _visual.AnimationTime).SetEase(Ease.Linear);
        _arrow.DOColor(color, _visual.AnimationTime).SetEase(Ease.Linear);
        _text.DOColor(color, _visual.AnimationTime).SetEase(Ease.Linear);
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
