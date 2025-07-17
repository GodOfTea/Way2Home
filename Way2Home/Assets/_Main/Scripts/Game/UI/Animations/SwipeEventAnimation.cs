using DG.Tweening;
using Infrastructure.Services.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Animations
{
    public class SwipeEventAnimation : MonoBehaviour
    {
        [SerializeField] private AudioTrack _changeSlideSound;
        [SerializeField] private Image _fakePicture;

        public void Play(Sprite lastEvent)
        {
            _changeSlideSound.PlayOneShot();
            Transform fakePictureTransform = _fakePicture.transform;
        
            fakePictureTransform.localPosition = Vector3.zero;
            _fakePicture.sprite = lastEvent;
            fakePictureTransform.gameObject.SetActive(true);
            fakePictureTransform.SetAsFirstSibling();

            Sequence animation = DOTween.Sequence();
        
            animation.Append(fakePictureTransform.DOLocalMoveY(120f, 0.3f));
            animation.AppendCallback(() => fakePictureTransform.SetAsLastSibling());
            animation.Append(fakePictureTransform.DOLocalMoveY(-1000f, 1f).SetEase(Ease.InQuad));
            animation.AppendCallback(() => fakePictureTransform.gameObject.SetActive(false));
        }
    }
}
