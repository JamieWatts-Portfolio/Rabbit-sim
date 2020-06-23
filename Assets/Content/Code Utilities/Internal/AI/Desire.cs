using System.Collections;
using UnityEngine;
using System;

namespace AI{

[Serializable]
/// <summary>Type classification of an item of desire for use in AI</summary>
public abstract class Desire : MonoBehaviour {

    #region properties

    /// <summary>Determines the default desire</summary>
    public float Base {get; private set;}

    /// <summary>Change in desire over time</summary>
    /// ActualDesire = TimeDesire * Time.DeltaTime.
    public float Time {get; private set;}

    /// <summary>The current level of desire</summary>
    /// 0 = no change over time.
    public float Actual {get; private set;}

    /// <summary>Client friendly name of this desire</summary>
    public string Name {get; private set;} = "";

    /// <summary>Parenting ai controller for runtime referencing<summary>
    public AIController Parent {get;set;}

    #endregion 

    /// <summary> Create a new item of desire </summary>
    public Desire(float baseDesire, float time, string name){
        Base = baseDesire;                      
        Time = time;
        Name = name;
        Actual = Base;
    }

        #region interactions
        /// <summary>Framely update<summary>
        /// Used to modify desire over time using time since last frame
        public void update() => Actual += Time * UnityEngine.Time.deltaTime;

        /// <summary>resets the desire to the base level</summary>
        public void reset() => Actual = Base;

        /// <summary>Adjoins <param>mod</param> to <c>Actual</c></summary>
        /// can be used positively or negatively.
        public void modify(float mod) => Actual += mod;

        #endregion

        #region static

        /// <summary>comares the desire level two desires</summary>
        /// <returns><c>Desire</c> with highest level of desire</returns>
        public static Desire compare(Desire a, Desire b){
            return (a == null) ? (b == null) ? null : b : (b == null) ? a : (a.Actual > b.Actual) ? a : b;
        }

        #endregion

        #region overrides

        /// <summary>Framely updates when desire is top priority</summary>
        public virtual void executeUpdate(){}

        /// <summary>Invoked when desire becomes top priority</summary>
        public virtual void executeEnter(){}

        /// <summary>Invoked when this desire is no longer the top priority</summary>
        public virtual void executeExit(){}

        #endregion
    }


}