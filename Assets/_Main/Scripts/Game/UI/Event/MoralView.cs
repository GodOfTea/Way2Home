using TMPro;
using UnityEngine;

namespace UI.Event
{
    /* TODO: Добавить вид морали в зависимости от значения */
    public class MoralView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moralValueText;
        
        public void UpdateMoralValue(int moralValue)
        {
            _moralValueText.text = $"{moralValue}";
        }
    }
}
