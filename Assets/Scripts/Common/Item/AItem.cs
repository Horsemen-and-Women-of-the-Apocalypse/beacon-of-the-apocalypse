using UnityEngine;

namespace Common.Item {
    /// <summary>
    /// Base component for items
    /// </summary>
    public abstract class AItem : MonoBehaviour, ITargetable {
        /// <summary>
        /// Visit the current item
        /// </summary>
        /// <param name="visitor">Visitor</param>
        public abstract void Visit(IVisitor visitor);

        public abstract void OnEnter(); // TODO: Display borders

        public abstract void OnExit(); // TODO: Hide borders

        /// <summary>
        /// Visitor of <see cref="AItem"/>
        /// </summary>
        public interface IVisitor {
            /// <summary>
            /// Callback called when a <see cref="BatteryItem"/> is encountered
            /// </summary>
            /// <param name="item">Battery</param>
            public void OnBattery(BatteryItem item);

            /// <summary>
            /// Callback called when a <see cref="FlashItem"/> is encountered
            /// </summary>
            /// <param name="item">Flash</param>
            public void OnFlash(FlashItem item);

            /// <summary>
            /// Callback called when a <see cref="SonarItem"/> is encountered
            /// </summary>
            /// <param name="item">Sonar</param>
            public void OnSonar(SonarItem item);
        }
    }
}