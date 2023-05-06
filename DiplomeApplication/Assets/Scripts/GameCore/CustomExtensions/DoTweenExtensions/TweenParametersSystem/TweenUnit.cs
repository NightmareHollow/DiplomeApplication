using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem
{
    [System.Serializable]
    public class TweenUnit
    {
        [Min(0.001f), SerializeField] private float duration = 1f;
        [Min(0f), SerializeField] private float delay;

        [HorizontalLine(color: EColor.Green)]

        [SerializeField] private Ease tweenEase = Ease.InOutSine;

        [SerializeField] private TweenTargetRelationType targetRelationType = TweenTargetRelationType.To;

        [HorizontalLine(color: EColor.Green)]

        [AllowNesting, OnValueChanged("LoopsValueChanged")]
        [SerializeField] private int loops = 1;

        [AllowNesting, ShowIf("NeedLoopType")]
        [SerializeField] private LoopType loopType = LoopType.Restart;

        public float Duration => duration;
        public float Delay => delay;

        public Ease TweenEase => tweenEase;
        public TweenTargetRelationType TargetRelationType => targetRelationType;

        public int Loops => loops;

        public LoopType TweenLoopType 
            => (NeedLoopType()) ? loopType : LoopType.Restart;

        public float FullTweenDuration()
        {
            if (loops < 0)
            {
                return Mathf.Infinity;
            }

            float fullDuration = delay + duration * Loops;
            return fullDuration;
        }

        private bool NeedLoopType()
            => loops is < 0 or > 1;

#if UNITY_EDITOR
        
        private void LoopsValueChanged()
        {
            if (loops is < 0 and not -1)
            {
                loops = -1;
            }
        }
        
#endif
    }
}