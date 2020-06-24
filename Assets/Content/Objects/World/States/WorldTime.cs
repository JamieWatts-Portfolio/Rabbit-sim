using System.Diagnostics;
using UnityEngine;

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
        private bool cycleStateChangeFlag;
        #endregion

        #region Unity Editor properties    
        [Header("Day Night Cycle")]
        /// <summary> Seconds length for entire day night cycle </summary>
        [Tooltip("Semilength of the entire day/night cycle (x * 2 = total cycle length)")]
        public float semiCycleTime = 10;

        [Tooltip("Time taked to fade between skybox materails at the begining of each semicycle")]
        public float fadeTime = 10;

        [Header("Fog")]  
        [Tooltip("Fog color to be used during day cycles")]
        public Color dayFogHue = new Color();

        [Tooltip("Fog color to be used during night cycles")]
        public Color nightFogHue = new Color();
        #endregion

        #region configuration
        
        /// <summary> This state has just been entered in the world state machine. </summary>
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            semiTime = semiCycleTime / 2;       //Start at mid day, not at the begining.
        } 

        #endregion

        #region update

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
           UpdateDelta();       // Update world time based on delta time
           updateFade();        // Check for and update fog and box fades
        }

        /// <summary> Update the current world time, based on delta time </summary>
        private void UpdateDelta() { 
            semiTime += Time.deltaTime; 
            quadriTime += Time.deltaTime;
            CheckTime(); 
        }

        /// <summary>Triggers a state flip if semicycle time has surpassed</summary>
        private void CheckTime() { 
            if (quadriTime > semiCycleTime / 2) resetQuadriTime();              // If half of the semi cycle has passed, reset quadri time
            cycleState = (semiTime > semiCycleTime) ? flipState() : cycleState; // If semi cycle is over, flip state. Update cycle state: if state changed, to new state else keep the same.
        }
        #endregion

        #region utility functions
        // TODO test
        /// <summary>Resets world time and flips cycle state</summary>
        /// Also raises state change flag
        private bool flipState(){
            semiTime = 0f;                      // Clear semi time
            cycleStateChangeFlag = true;        // Raise state change flag
            return cycleState = !cycleState;    // invert cycle state and return it
        }

        /// <summary></summary>
        private void resetQuadriTime() {
            quadriTime = 0f;
            //if (!cycleStateChangeFlag) {
               //prevHue = currentFogColor();
            //}
        } 

        /// <summary>Lerps fog colour torwards parsed colour based on world time</summary>
        private void updateFade() {
            RenderSettings.skybox.SetFloat("_Blend", calcFade(semiTime));
            RenderSettings.fogColor = Color.Lerp(dayFogHue, nightFogHue, calcFade(semiTime)); // 
        } 


        /// <summary>Current percentage of way through semi cycle</summary>
        private float semiCyclePercent() => (semiTime / semiCycleTime) * 100;

        /// <summary>Mathematical curve function to calulate state of fade based on time of day, and fade time</summary>
        /// X axis = Time of cycle
        /// Y axis = Float between 0 and 1 for lerping between skyboxes and fog colors.
        /// 
        /// 1             ______________________________________
        /// :           /                                        \
        /// :          /                                          \                                          
        /// :         /                                            \
        /// :        /                                              \
        /// :       /                                                \ 
        /// :      /                                                  \
        /// 0     /                                                    \
        ///      0------------------------------------------------------SemiCycleTime
        private float calcFade(float cycle){
            float point;

            if (cycle < fadeTime)                                   // Leading curve
                point = Mathf.Lerp(0, 1, cycle / fadeTime);
            else if (cycle > semiCycleTime - fadeTime)              // Trailing curve
                point = Mathf.Lerp(1, 0, 1 - cycle / fadeTime);
            else                                                    // Flat top                                        
                point = 1f; 

            return cycleState ? point : 1 - point;                  // return with polarity modified based on cycle state;     day = flat is held low,      night = flat is held high.
        }

        #endregion
    }
}   