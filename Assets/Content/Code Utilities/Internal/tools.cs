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

    public struct TransformData
    {
        public Vector3 position;
        public Quaternion rotation;

        public Vector3 localPosition;
        public Vector3 localScale;
        public Quaternion localRotation;

        public Transform parent;

        public static TransformData CloneTransform(Transform transform)
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
    }

    public class tools
    {

        public static Vector3 random3(Vector2 x, Vector2 y, Vector2 z)
        {
            float xRan = Random.Range(x.x, x.y);
            float yRan = Random.Range(y.x, y.y);
            float zRan = Random.Range(z.x, z.y);
            return new Vector3(xRan, yRan, zRan);
        }

        /// <summary>Picks a random point within a transform</summary>
        /// Using position and scale, calculates a random location in 3d space.
        /// <param name = "bounds">spacial transform to use in creating a point.</param>
        /// <param name = "ignoreY">If true, Y will be 0.</param> //TODO change ignore to raycast
        public static Vector3 randomInTransform(Transform bounds, bool ignoreY)
        {
            Vector3 scale = bounds.lossyScale;

            Vector2 x = new Vector2(bounds.position.x, 1 * scale.x);
            Vector2 y = ignoreY ? new Vector2(0, 0) : new Vector2(bounds.position.y, 1 * scale.y);
            Vector2 z = new Vector2(bounds.position.z, 1 * scale.z);
            return random3(z, y, z);
        }

        public static Vector3 stripY(Vector3 toStrip)
        {
            return new Vector3(toStrip.x, 0, toStrip.z);

        }

        public static Vector3 randomDirection(Vector3 position)
        {
            return new Vector3(Random.Range(position.x - 1f, position.x + 1f), 0, Random.Range(position.y - 1f, position.y + 1f)).normalized;
        }

        public static T RandomInList<T>(List<T> list) => (list == null || list.Capacity == 0) ? default(T) : list[Random.Range(0, list.Capacity - 1)];

        /// <summary>Uses a local size and scale to calculate the global size</summary>
        public static float CalculateGlobalSize(float size, float scale, bool negative)
        {
            float actual = size * scale;
            return (negative) ? negate(actual) : actual;
        }

        /// <summary>mathematically flips the polarity of a float</summary>
        public static float negate(float i) => i * -1;

        ///<summary>Sets the relation between the parsed object's transforms</summary>
        public static void setParent(GameObject child, GameObject parent) => child.transform.parent = parent.transform;

        /// <summary>Ray casts a random location on the mesh, and ray casts for mesh height.</summary>
        /// <returns>A random location on the mesh, with correct height.</returns>
        public static Vector3 PickRandomLocation(MeshCollider Collider, Transform transform1, float rayHeight)
        {
            float x = PickPointOnAxis(Collider.sharedMesh.bounds.size.x, transform1.localScale.x);
            float z = PickPointOnAxis(Collider.sharedMesh.bounds.size.z, transform1.localScale.z);
            return new Vector3(x, getHeightAt(x, z, rayHeight, Collider), z);                                                          // Successful cast, return point
        }

        /// <summary>Picks a random poin on an axis</summary>
        /// calculates global size, and chooses a point.
        /// Assumes centre is at local centre of axis.
        public static float PickPointOnAxis(float localsize, float localscale) => UnityEngine.Random.Range(0, CalculateGlobalSize(localsize, localscale, false));

        /// <summary>Ray casts to find mesh height at the specified x and y</summary>
        public static float getHeightAt(float x, float z, float rayHeight, MeshCollider collider)
        {
            UnityEngine.Debug.DrawRay(new Vector3(x, 20, z), -Vector3.up, Color.black, 100f);
            Ray ray = new Ray(new Vector3(x, rayHeight, z), -Vector3.up);                  // Create a new ray at the specified location, at height, facing towards the ground.
            int retry = 5;
            RaycastHit hit;
            while (retry >= 0)
            {
                if (!collider.Raycast(ray, out hit, 1000))                                 // Cast ray
                {
                    retry--;                                                               // Failed to hit mesh
                    continue;                                                              // try again
                }
                return hit.point.y;
            }
            // UnityEngine.Debug.LogWarning("World generation ray cast failed to hit mesh after five attempts.");
            return -999f;
        }


    }

}