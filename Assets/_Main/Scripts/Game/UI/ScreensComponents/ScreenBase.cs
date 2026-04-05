using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(
    typeof(Canvas), 
    typeof(CanvasGroup), 
    typeof(GraphicRaycaster))]
public abstract class ScreenBase : MonoBehaviour
{
    [SerializeField] protected float _fadeDuration = .3f;
    
    protected Canvas _canvas;
    protected CanvasGroup _group;
    
    public void Initialize()
    {
        _canvas = GetComponent<Canvas>();
        _group = GetComponent<CanvasGroup>();
        gameObject.SetActive(true);
    }
    
    public virtual void Show()
    {
        _canvas.enabled = true;
        StopAllCoroutines();
        StartCoroutine(Fade(_group.alpha, 1, _fadeDuration));
    }

    public virtual void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(_group.alpha, 0, _fadeDuration));
    }
    
    public virtual void ShowImmediate()
    {
        _canvas.enabled = true;
        _group.alpha = 1;
    }

    public virtual void HideImmediate()
    {
        _canvas.enabled = false;
        _group.alpha = 0;
    }
    
    private IEnumerator Fade(float startValue, float endValue, float duration)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            _group.alpha = Mathf.MoveTowards(startValue, endValue, currentTime / duration);
            yield return null;
            currentTime += Time.deltaTime;
        }
        
        _group.alpha = endValue;

        if (endValue == 0)
        {
            _canvas.enabled = false;
        }
    }
}
