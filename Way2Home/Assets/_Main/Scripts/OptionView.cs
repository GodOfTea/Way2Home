using System;
using EventComponents;
using Infrastructure.Services;
using Infrastructure.Services.Localization;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OptionView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ILocalizable
{
    [SerializeField] private TMP_Text _optionText;

    [Header("Images")]
    [SerializeField] private Image _dot;
    [SerializeField] private Image _redStar;

    private ILocalizationService _localizationService;
    private IAnswer _answer;
    private Button _button;

    public event Action<string> OptionChoose;

    public void SetAnswer(IAnswer answer)
    {
        _answer = answer;
        SetText();
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
        
        _dot.gameObject.SetActive(true);
        _redStar.gameObject.SetActive(false);
        
        _localizationService = AllServices.Container.Single<ILocalizationService>();
        _localizationService.AddLocalizable(this);
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Choose);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Choose);
    }

    private void Choose()
    {
        OptionChoose?.Invoke(_answer.AnswerId);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _dot.gameObject.SetActive(false);
        _redStar.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _dot.gameObject.SetActive(true);
        _redStar.gameObject.SetActive(false);
    }

    private void SetText() =>
        _optionText.text = _localizationService.GetLocalizationEventText(_answer.AnswerId);

    public void UpdateLocalization()
    {
        if (_answer != null)
            SetText();
    }
}
