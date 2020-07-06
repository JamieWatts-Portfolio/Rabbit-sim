using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    namespace AI {
    public class Metabolism : MonoBehaviour
    {
        public Metabolism(){}

        public Metabolism(byte MaxSaturation, float HungerThreshold, float MetabolismRate) {
            maxSaturation = MaxSaturation;
            hungerThreshold = HungerThreshold;
            metabolismRate = MetabolismRate;
        }

        /// <summary>Current level representing the entitie's hunger.</summary>
        public float saturation = 100;

        /// <summary>Maximum hunger level</summary>
        [Tooltip("Maximum total saturation")]
        public byte maxSaturation = 100;

        /// <summary>Level at which the entitiy becomes hungry</summary>
        [Tooltip("Level at which the entitiy becomes hungry")]
        public float hungerThreshold = 60;

        /// <summary>Is the entity currently hungry</summary>
        public bool isHungry {get; private set;}

        /// <summary>How much the saturation is decreased every frame</summary>
    	public float metabolismRate = 0.01f;
    
        private void start() => checkConfiguration();

        public void Update()
        {
            metabolise();
            checkThreshold();
        }

        /// <summary> Ensures the configuration is valid. </summary>
        private void checkConfiguration(){
            saturation = assertInRange(saturation, maxSaturation, 0);
            hungerThreshold = assertInRange(hungerThreshold, maxSaturation, 0);
        }

        /// <summary>Set <c>isHungry<c> if saturation is below hunger threshold</summary>
        private void checkThreshold() => isHungry = (saturation < hungerThreshold);

        /// <summary>Decreses saturation by metabolism rate</summary>
        private void metabolise() => saturation += -metabolismRate;

        /// <summary>Ensures that <param>value<param> is between <param>max<param> and <param>min<param></summary>
        /// <returns>returns value normalised to range</returns>
		private float assertInRange(float value) => assertInRange(value, maxSaturation, 0); 
        private float assertInRange(float value, float max, float min) => (value > max) ? max : (value < min) ? 0 : value; 

		/// <summary>Modifies saturation by parsed value. Asserts within range 0-maxSaturation.</summary>
		/// Automatically updates isHungry.
		public void modSaturation(float sat) { saturation = assertInRange(sat + saturation); checkThreshold();}
    }
}