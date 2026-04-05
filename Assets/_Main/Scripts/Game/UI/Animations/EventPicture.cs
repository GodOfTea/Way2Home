using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Animations
{
    public class EventPicture : MonoBehaviour
    {
        private readonly string _thresholdPropertyName = "_Threshold";
        
        [SerializeField] private Image _mainPicture;
        [SerializeField] private Image _fakePicture;

        public Sprite Current => _mainPicture.sprite;

        [Header("Burn Animation")]
        [SerializeField] private float _burnAnimationTime;

        private Material _burnMaterial;
        
        private void Start()
        {
            _burnMaterial = _fakePicture.material;
            _fakePicture.gameObject.SetActive(false);
        }

        public void ChangeQuestionPicture(Sprite next)
        {
            _mainPicture.sprite = next;
        }

        public void ChangeAnswerPicture(Sprite next)
        {
            _fakePicture.sprite = _mainPicture.sprite;
            _fakePicture.gameObject.SetActive(true);

            _mainPicture.sprite = next;

            DOVirtual.Float(0f, 1f, _burnAnimationTime,
                value => _burnMaterial.SetFloat(_thresholdPropertyName, value)).
                SetEase(Ease.OutCubic).OnComplete(ReversePictures);
        }

        private void ReversePictures()
        {
            _fakePicture.gameObject.SetActive(false);
            _burnMaterial.SetFloat(_thresholdPropertyName, 0f);
        }
    }
}
