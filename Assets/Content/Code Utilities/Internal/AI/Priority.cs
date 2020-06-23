using UnityEngine;
using System;
using System.Collections.Generic;

namespace AI {

[Serializable]   
/// <summary>Collection of desires automatically ordered by level of desire</summary>
public class Priority : MonoBehaviour {

    public Priority get => this;
    public List<Desire> desires = new List<Desire>();
    public Desire topPriority {get; private set;} = null;
    public Desire OverridePriority = null;
    [HideInInspector] public AIController parent;

        /// <summary>framely updates</summary>
        /// updates desires, then orders and selects priorities based on levels of desire.
        public void update() {
            updateAll();
            executeTop();
        }

        /// <summary>itterates over every desire, and triggers thier update.</summary>
        private void updateAll() {
            Desire top = null;
            foreach (Desire des in desires) {            // For all desires,
                try {
                    des.update();                        
                } catch (Exception e) {
                    Debug.LogError("Exception of type " + e.GetType() + " caught in " + des.name + " priority.executeUpdate");
                }                                   
                top = Desire.compare(des, top);             // See if it's higher than the current temp top priority
            }
            Debug.Log("top: " + top.Name + " @ " + top.Actual); 
            Debug.Log("sec: " + desires[0].Name + " @ " + desires[0].Actual);
            setTop(top);
        }
        public Priority add(Desire toAdd) {desires.Add(toAdd); return this;}

        private void setTop(Desire newTop){
            if (topPriority != null) {
                if (topPriority == newTop) return;
                 try {
                    topPriority.executeExit();                            
                } catch (Exception e) {
                    Debug.LogError("Exception of type " + e.GetType() + " caught in " + topPriority.name + " priority.executeExit");
                }
                }
            topPriority = newTop;
            try {
                if (topPriority != null) {assertParent(topPriority); topPriority.executeEnter();}
            } catch (Exception e) {
               Debug.LogError("'" + e.Message + "' caught in " + topPriority.name + " priority.executeEnter");
            }
        }

        private void executeTop(){
            if (OverridePriority == null && topPriority != null){
                topPriority.executeUpdate();
            }
        }

        private bool isTop(Desire tocheck) {
            if (topPriority == null)
                return false;
            else
                return tocheck.Equals(topPriority);
        }

        public void reset(Desire desire){
            if (isTop(desire)){
                setTop(null);
                desire.reset();
            }
        }

        public void assertParent(Desire desire){
            if (desire.Parent == null) desire.Parent = parent;
        }
    }
}