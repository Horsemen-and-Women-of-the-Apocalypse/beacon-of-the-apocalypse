using UnityEngine;

namespace Common.Item {
    /// <summary>
    /// Component managing a battery item
    /// </summary>
    public class BatteryItem : AItem {
        [Tooltip("Level of battery gained when the item is used")]
        public float level = 5;

        public override void Visit(IVisitor visitor) {
            visitor.OnBattery(this);
        }

        public override void OnEnter() { }

        public override void OnExit() { }
    }
}