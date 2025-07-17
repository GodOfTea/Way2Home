using System;
using EventComponents;
using Game.UI.Animations;
using Game;
using Game.Economy;
using UI.Event;
using UnityEngine;
using Event = EventComponents.Event;

public class EventView : MonoBehaviour
{
    [Header("Left Side")]
    [SerializeField] private EventPicture _eventPicture;
    [SerializeField] private SwipeEventAnimation _swipeEventAnimation;

    [Header("Right Side")]
    [SerializeField] private QuestionWindow _questionWindow;
    [SerializeField] private AnswerWindow _answerWindow;

    private Event _currentEventData;
    private IndicatorsBank _indicatorsBank;

    public void Init(IEventSwitcher eventSwitcher, IndicatorsBank indicatorsBank)
    {
        _indicatorsBank = indicatorsBank;
        _answerWindow.Init(eventSwitcher, _eventPicture);
    }
    
    private void OnEnable()
    {
        _questionWindow.QuestionResolved += OnQuestionResolved;
    }

    private void OnDisable()
    {
        _questionWindow.QuestionResolved -= OnQuestionResolved;
    }

    public void ShowQuestion(Event eventData, bool isFirstEvent = false)
    {
        if (eventData == null)
            throw new Exception("Can't show Question");

        _answerWindow.Hide(immediate: isFirstEvent);
        
        if (isFirstEvent == false)
            _swipeEventAnimation.Play(_eventPicture.Current);
        _eventPicture.ChangeQuestionPicture(eventData.EventImage);
        
        _questionWindow.UpdateView(eventData);
        _questionWindow.Show(immediate: isFirstEvent);
        
        _currentEventData = eventData;
    }

    private void ShowAnswer(IAnswerResult result)
    {
        _questionWindow.Hide();
        
        _answerWindow.UpdateView(result);
        _answerWindow.Show();
    }

    private void OnQuestionResolved(string answerId)
    {
        var result = _currentEventData.GetResult(answerId);
        
        _indicatorsBank.UpdateIndicatorsValues(result.IndicatorsResultMap);
        ShowAnswer(result);
    }
}
