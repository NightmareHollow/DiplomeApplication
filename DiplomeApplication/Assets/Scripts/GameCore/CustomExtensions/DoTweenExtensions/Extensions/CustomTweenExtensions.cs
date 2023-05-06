using DG.Tweening;
using GameCore.CustomExtensions.DoTweenExtensions.TweenParametersSystem;

namespace GameCore.CustomExtensions.DoTweenExtensions.Extensions
{
    public static class CustomTweenExtensions
    {
        public static TweenTargetRelationType FindAnimTargetRelation(TweenTargetRelationType defaultRelation,
            bool playForward)
        {
            TweenTargetRelationType resultRelationType = (playForward) ?
                GetTargetRelationTypeOpposite(defaultRelation) : defaultRelation;
            return resultRelationType;
        }

        public static Ease FindOppositeEase(Ease fromEase)
        {
            return fromEase switch
            {
                Ease.InBack => Ease.OutBack,
                Ease.InBounce => Ease.OutBounce,
                Ease.InCirc => Ease.OutCirc,
                Ease.InCubic => Ease.OutCubic,
                Ease.InElastic => Ease.OutElastic,
                Ease.InExpo => Ease.OutExpo,
                Ease.InFlash => Ease.OutFlash,
                Ease.InQuad => Ease.OutQuad,
                Ease.InQuart => Ease.OutQuart,
                Ease.InQuint => Ease.OutQuint,
                Ease.InSine => Ease.OutSine,
                Ease.OutBack => Ease.InBack,
                Ease.OutBounce => Ease.InBounce,
                Ease.OutCirc => Ease.InCirc,
                Ease.OutCubic => Ease.InCubic,
                Ease.OutElastic => Ease.InElastic,
                Ease.OutExpo => Ease.InExpo,
                Ease.OutFlash => Ease.InFlash,
                Ease.OutQuad => Ease.InQuad,
                Ease.OutQuart => Ease.InQuart,
                Ease.OutQuint => Ease.InQuint,
                Ease.OutSine => Ease.InSine,
                _ => fromEase
            };
        }

        private static TweenTargetRelationType GetTargetRelationTypeOpposite(TweenTargetRelationType fromType)
            => (fromType == TweenTargetRelationType.From) ? TweenTargetRelationType.To : TweenTargetRelationType.From;
    }
}