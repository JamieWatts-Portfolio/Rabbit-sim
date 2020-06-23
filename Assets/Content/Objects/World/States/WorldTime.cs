
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Assertions;

namespace WorldGeneration {
    /// <summary> World time state machine behaviour </summary>
    /// Controlls world time and day night cycles.
    /// Modifies skyboxes and fog accordingly.
    public class WorldTime : StateMachineBehaviour
    {   
        #region Properties
        /// <summary> Current time in current period</summary>
        /// 0 - dayNightCycle for both day and night.
        public float semiTime;

        /// <summary> progress between each quater cycle </summary>
        /// used in fading fog in each quater.
        /// 0 - (semiCycleTime / 2) twice per semi cycle (four times per full cycle)
        public float quadriTime;

        /// <summary> Current half of the cycle </summary>
        /// true = night, false = day.
        public bool cycleState {get; private set;}

        /// <summary>When true, indicates that the cycle state has just been changed.</summary> 
        public bool cycleStateChangeFlag;
        
        /// <summary> Seconds length for entire day night cycle </summary>
        [Tooltip("Semilength of the entire day/night cycle")]
        public float semiCycleTime = 10;

        /// <summary> Stationary refference to the fog's previous colour for fade transitions </summary>
        public Color prevHue = new Color();
        #endregion

        #region Unity Editor properties
        [Header("Skybox")]
        [Tooltip("Skybox material to be used during day cycles")]
        public Material dayBox = null;
        
        [Tooltip("Skybox material to be used during night cycles")]
        public Material nightBox = null;
        
        [Header("Fog")]  
        [Tooltip("Fog color to be used during day cycles")]
        public Color dayFogHue = new Color();

        [Tooltip("Fog color to be used during night cycles")]
        public Color nightFogHue = new Color();
        #endregion

        #region configuration
        /// <summary> This state has just been entered in the world state machine. </summary>
        
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            prevHue = currentFogColor();
            ValidateConfiguration();
        } 

        /// <summary>For debugging, asserts that unity editor values have been configured.</summary>
        /// Due to the conditional nature of assertions, this method is not included in builds.
        [Conditional("DEVELOPER_DEBUG")]
        private void ValidateConfiguration(){
            Assert.IsNotNull(dayBox, "[World Time State Machine] Day sky box is not set in inspector");
            Assert.IsNotNull(nightBox, "[World Time State Machine] Day sky box is not set in inspector");
        }
        #endregion

        #region update
        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           UpdateDelta();
           UpdateFog();
           CheckBox(); 
        }

        /// <summary> Update the current world time, based on delta time </summary>
        private void UpdateDelta() { 
            semiTime += Time.deltaTime; 
            quadriTime += Time.deltaTime;
            CheckTime(); 
        }

        /// <summary>Triggers a state flip if semicycle time has surpassed</summary>
        private void CheckTime() { 
            if (quadriTime > semiCycleTime / 2) resetQuadriTime();
            cycleState = (semiTime > semiCycleTime) ? flipState() : cycleState;
        }

        /// <summary>Checks time state, and update skybox accordingly</summary>
        private void CheckBox(){
            if (cycleStateChangeFlag) {                     // if state has just changed
                cycleStateChangeFlag = false;               // lower flag to indicate it's been handled
                prevHue = nextFogColor();                   // Store fog colour for next transition 
                RenderSettings.skybox = nextBox();          // Change the skybox to the box for the next semicycle.
            }
        }

        // TODO test
        /// <summary>Modifies fog colour over time to fade between states</summary>
        private void UpdateFog(){
            if (semiCyclePercent() <= 50f)      
                fadeFog(currentFogColor());                                                 // If we're less than 50 percent of the way through the day, fade colour to current box colour
            else 
                fadeFog(nextFogColor());                                                    // Else begin fading to next box colour
        }   
        #endregion

        #region utility functions
        // TODO test
        /// <summary>Resets world time and flips cycle state</summary>
        /// Also raises state change flag
        private bool flipState(){
            semiTime = 0f;
            cycleStateChangeFlag = true;
            return cycleState = !cycleState;
        }

        /// <summary></summary>
        private void resetQuadriTime() {
            quadriTime = 0f;
            if (!cycleStateChangeFlag) {
               prevHue = currentFogColor();
            }
        } 

        /// <summary>Lerps fog colour torwards parsed colour based on world time</summary>
        private void fadeFog(Color nextColor) => RenderSettings.fogColor = Color.Lerp(prevHue, nextColor, 1 / (semiCycleTime / (quadriTime * 2))); //

        /// <summary>Current percentage of way through semi cycle</summary>
        private float semiCyclePercent() => (semiTime / semiCycleTime) * 100;

        /// <summary>Returns the skybox materal for the skybox in the next cycle state</summary>
        private Material nextBox() => cycleState ? dayBox : nightBox ;

        /// <summary>Returns the set hue for the current semicycle</summary>
        private Color currentFogColor() => cycleState ? nightFogHue : dayFogHue;

        /// <summary>Returns the set hue for the next semicycle</summary>
        private Color nextFogColor() => !cycleState ? dayFogHue : nightFogHue;

        #endregion
    }
}