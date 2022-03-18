using UnityEngine;

namespace Common.Item
{
    /// <summary>
    /// Component managing a flash item
    /// </summary>
    public class FlashItem : AItem
    {
        [Tooltip("Intensity for the flash")]
        public float flashIntensity = 1;

        public override void Visit(IVisitor visitor)
        {
            visitor.OnFlash(this);
        }

        public override void OnEnter() { }

        public override void OnExit() { }
    }
}