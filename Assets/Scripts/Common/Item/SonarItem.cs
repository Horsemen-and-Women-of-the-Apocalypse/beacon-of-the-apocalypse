using UnityEngine;

namespace Common.Item
{
    /// <summary>
    /// Component managing a sonar item
    /// </summary>
    public class SonarItem : AItem
    {
        public override void Visit(IVisitor visitor)
        {
            visitor.OnSonar(this);
        }

        public override void OnEnter() { }

        public override void OnExit() { }
    }
}
