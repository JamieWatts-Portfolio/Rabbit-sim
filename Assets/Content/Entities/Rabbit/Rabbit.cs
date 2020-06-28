using AI;
using UnityEngine;
using utility;

namespace Rabbit {
public class Rabbit : IEntity {
    #region behaviour classes

    #endregion


    /// <summary> Generates a vector for within the scene. </summary>
    /// Used when rabbit is idle for generic movement
    public static Vector3 getNewRandomPosition() {
        return tools.randomInTransform(GameObject.Find("Grass_Flat_Ground").transform, true);        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("");
    }
}
}