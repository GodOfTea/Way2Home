using UnityEngine;

namespace Game.Economy.MoralComponents
{
    [CreateAssetMenu(fileName = "Moral Config", menuName = "Configs/MoralConfig")]
    public class MoralConfig : ScriptableObject
    {
        [SerializeField] private int _startMoralValue;
        [SerializeField] private int _negativeMoralValue;
        [SerializeField] private int _positiveMoralValue;

        public int StartMoralValue => _startMoralValue;
        public int NegativeMoralValue => _negativeMoralValue;
        public int PositiveMoralValue => _positiveMoralValue;
    }
}