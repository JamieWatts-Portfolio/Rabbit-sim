using UnityEngine;
using System.Collections.Generic;


namespace utility{

     public struct TransformData {
     public Vector3 position;
     public Quaternion rotation;
     
     public Vector3 localPosition;
     public Vector3 localScale;
     public Quaternion localRotation;
     
     public Transform parent;
 }

public class tools {

    public static Vector3 random3(Vector2 x, Vector2 y, Vector2 z){
        float xRan = Random.Range(x.x, x.y);
        float yRan = Random.Range(y.x, y.y);
        float zRan = Random.Range(z.x, z.y);
        return new Vector3(xRan,yRan,zRan);
    }

/// <summary>Picks a random point within a transform</summary>
/// Using position and scale, calculates a random location in 3d space.
/// <param name = "bounds">spacial transform to use in creating a point.</param>
/// <param name = "ignoreY">If true, Y will be 0.</param>
public static Vector3 randomInTransform(Transform bounds, bool ignoreY) {
    Vector3 scale = bounds.localScale;

    Vector2 x = new Vector2(1 / scale.x, 1 * scale.x);
    Vector2 y = ignoreY ? new Vector2(0,0) : new Vector2(1 / scale.y, 1 * scale.y);
    Vector2 z = new Vector2(1 / scale.z, 1 * scale.z);
    return random3(z,y,z);
}

public static Vector3 stripY(Vector3 toStrip){
    return new Vector3(toStrip.x,0,toStrip.z);
    
}

public static Vector3 randomDirection(Vector3 position){
    return new Vector3(Random.Range(position.x - 1f, position.x + 1f), 0, Random.Range(position.y - 1f, position.y + 1f)).normalized;
}
     
     public static TransformData Clone( Transform transform )
     {
         TransformData td = new TransformData();
         
         td.position = transform.position;
         td.localPosition = transform.localPosition;
         
         td.rotation = transform.rotation;
         td.localRotation = transform.localRotation;
         
         td.localScale = transform.localScale;
         
         td.parent = transform.parent;
         
         return td;
     }


     public static T RandomInList<T>(List<T> list) => list[Random.Range(0, list.Capacity)];

    /// <summary>Uses a local size and scale to calculate the global size</summary>
    public static float CalculateGlobalSize(float size, float scale, bool negative) {
        float actual = size * scale;
       return (negative) ? negate(actual) : actual ;
    }

    /// <summary>mathematically flips the polarity of a float</summary>
    public static float negate(float i) => i * -1;

    ///<summary>Sets the relation between the parsed object's transforms</summary>
    public static void setParent(GameObject child, GameObject parent) => child.transform.parent = parent.transform;


     
 }

}