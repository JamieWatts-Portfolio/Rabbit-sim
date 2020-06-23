using UnityEngine;

namespace AI {
    public class TestDesire : Desire {
        public TestDesire() : base (10,0.5f,"testDesire1") {
        }

        public override void executeUpdate(){
                UnityEngine.Debug.Log("test update");
        }

        /// <inheritdocs>
        public override void executeEnter(){
                UnityEngine.Debug.Log("test enter");
        }

        /// <inheritdocs>
        public override void executeExit(){
                UnityEngine.Debug.Log("test exit");
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}