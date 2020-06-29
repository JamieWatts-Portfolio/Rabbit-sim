using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.AI;
using utility;
using System;
using DevConsole;
using System.Diagnostics;
using CodeMonkey.Utils;


// TODO tooltips on inspector visible properties
namespace WorldGeneration
{
    /// <summary>World generation mesh factory component</summary>
    /// Generates a mesh, rendering it with a mesh filter on the object to which this is applied.
    /// Clears all children from applied game object before generation.
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshCollider))]
    [ConsoleCommand(new String[] { "world", "wld" })]
    public class WorldFactory : MonoBehaviour
    {
        #region properties
        [Header("Mesh generation")]
        #region mesh properties
        /// <summary>Maximum triangle count</summary> 
        /// Meshes have a maximum triangle count of 65000, this is used to prevent triangle overflow.
        private static readonly ushort MAX_TRIS = 65000;

        /// <summary>filter object to use for rendering the generated mesh</summary>
        private MeshFilter filter;

        /// <summary>World's mesh collider.</summary>
        private new MeshCollider collider = null;

        /// <summary>Raycast hitpoints used in world generation</summary>
        private RaycastHit hit;

        /// <summary>Size of the world this factory will generate</summary>
        public byte xSize = 20, zSize = 20;
        private static readonly byte DEFAULT_X = 100, DEFAULT_Z = 100;

        /// <summary>determines the overall scale of the perlin generation</summary>
        public float perlinScale { get; set; } = 0.1f;

        /// <summary>determines the overall scale of the perlin generation</summary>
        public float heightZoom = 2f;

        #endregion
        [Header("Spawn objects")]
        #region spawn properties

        
        /// <summary>Editor set list of objects that can be instantiated as trees.</summary>
        [Tooltip("Trees are spawned in clusters, according to cluster properties.")]
        public List<GameObject> foliage = new List<GameObject>();

        /// <summary>Editor set list of objects that can be instantiated as structures.</summary>
        [Tooltip("structures are spawned as provided, alone.")]
        public List<GameObject> structures = new List<GameObject>();
        
        /// <summary> Treated same as structures, but every item will be placed in the world at least once. </summary>
        [Tooltip("Treated same as structures, but every item will be placed in the world at least once.")]
        public List<GameObject> assertedStructures = new List<GameObject>();

        /// <summary>Editor set list of objects that can be instantiated as entities</summary>
        [Tooltip("Entities are spawned after all world population to ensure the navmesh is ready for them.")]
        public List<GameObject> entities = new List<GameObject>();

        [Header("Cluster settings")]
        /// <summary>Maximum number of trees that may spawn</summary>
        public byte maxFoliageClusters = 5;

        /// <summary>Maximum number of trees that may spawn</summary>
        public byte maxStructures = 5;

        /// <summary>Maximum distance from cluster origin which items may spawn<summary>
        public byte clusterSpread = 10;

        /// <summary>Maximum quantity of objects in a cluster</summary>
        public byte maxClusterSize = 5;

        [Header("Other")]
        /// <summary>Height of cast rays. Must be higher than highest point made by perlin mesh</summary>
        public byte rayHeight = 30;
        #endregion
        #endregion

        #region component
        /// <summary>Sets up the generation environment then creates and populates mesh</summary> 
        public void Start()
        {
            Configure();                                                                                // Check configuration, and get object references.
            filter.mesh = generate();                                                                   // Allocate the mesh filter a newly generated mesh
            collider.sharedMesh = filter.mesh;                                                          // Set collider to same shared mesh.
            populateWorld();                                                                            // Populate the world with Structures, foliage and entities.

        }

        /// <summary>Checks the component's configuration, and prepares it to generate.</summary>
        private void Configure()
        {
            if (triCount() > MAX_TRIS)
            {                                                                                           // Check for triangle overflow. (Mesh class has a limit of 65k triangles)
                UnityEngine.Debug.Log("[World gen] Invalid configuration; Mesh tri overflow: x" + xSize + ", * z" + zSize + " * 6 > " + MAX_TRIS);
                UnityEngine.Debug.Log("[World gen] Using default world size.");
                xSize = DEFAULT_X; zSize = DEFAULT_Z;                                                   // Use default maximum size
            }

            filter = GetComponent<MeshFilter>();                                                        // Get reference to required mesh filter
            Assert.IsNotNull(filter);                                                                   // Ensure there is one - There should be, since it's required by class annotation.

            //TODO this {get, assert} can be made functional.
            collider = GetComponent<MeshCollider>();                                                    // same for mesh collider.
            Assert.IsNotNull(collider);

            foreach (Transform child in gameObject.transform)                                           // Destroy any and all children to start from a clear slate.
                GameObject.Destroy(child.gameObject);                                                   // Usually not required, but when regenerating the world from this factory this is vital.
        }

        /// <summary>Mono Behaviour draw gizmo</summary>
        /// Used to render generated verticies with spheres.
        private void OnDrawGizmos()
        {
            if (filter == null                            // If There's no vertex's to draw,
            || filter.mesh.vertices == null)               // Or there's no mesh
                return;                                    // Chicken out.
            foreach (Vector3 vert in filter.mesh.vertices)
            { // Else, for every vert,
                Gizmos.DrawSphere(vert, .1f);              // Draw a sphere at it's location, with size .1f.
            }
        }
        #endregion

        #region generation routine

        /// <summary>Creates a new unpopulated perlin modified mesh</summary>
        /// Properties defined factory level.
        private Mesh generate()
        {
            Mesh mesh = new Mesh();                 // Create a new, blank, mesh
            mesh.vertices = generateVerticies();    // Generate and set verticies
            mesh.triangles = generateTriangles();   // Calculate triangles from verts
            mesh.RecalculateNormals();              // Use these values to calculate normals for proper lighting
            return mesh;
        }

        #region mesh
        private Vector3[] generateVerticies()
        {
            Vector3[] verts = new Vector3[(xSize + 1) * (zSize + 1)];    // Create temp array for verticies

            for (int vert = 0, zItt = 0; zItt <= zSize; zItt++)         // Itterate over z,
                for (int xItt = 0; xItt <= xSize; xItt++)
                {              // Itterate over x,
                    float y = Mathf.PerlinNoise(xItt * 0.1f, zItt * 0.1f) * 2f;
                    verts[vert] = new Vector3(xItt, y, zItt);           // Creating a vert at x, 0, y.
                    vert++;                                             // increase itterator.
                }
            return verts;
        }

        /// <summary>Generates triangle definitions</summary>
        /// Definitions created using factory level properties
        /// Accounts for tri rotation for correct rendering direction.
        private int[] generateTriangles()
        {
            int[] triangles = new int[triCount()];                     // Create array for all triangles.
            for (int vert = 0, tris = 0, zitt = 0; zitt < zSize; zitt++)
            {
                for (int xitt = 0; xitt < xSize; xitt++)               // For every quad
                {

                    triangles[tris + 0] = vert + 0;                    // Define left tri
                    triangles[tris + 1] = vert + xSize + 1;
                    triangles[tris + 2] = vert + 1;

                    triangles[tris + 3] = vert + 1;                    // Define right tri
                    triangles[tris + 4] = vert + xSize + 1;
                    triangles[tris + 5] = vert + xSize + 2;

                    vert++;                                            // Move to next quad
                    tris += 6;                                         // Count the values just added for this quad.
                }
                vert++;                                                // Skip last quad (Quad spills from end of one line to the beginning of the next, causing tris to  
            }                                                          // cover entire rows)
            return triangles;
        }
        #endregion

        #region population

        /// <summary>Populates world with structures, entities and foliage</summary>
        private void populateWorld()
        {
            tools.setParent(generateFoliage(), gameObject);          //Generate foliage as a child of this component's gameobject
            tools.setParent(generateStructures(), gameObject);
            UpdateNavMesh();
            //generateEntities
        }

        private GameObject generateStructures(){
            GameObject structureDescendant = new GameObject("Structures");

            foreach (GameObject structure in assertedStructures)                                    // Place every asserted structure
                placeObject(structure, structureDescendant);

            for (int currentStructure = 1; currentStructure <= maxStructures; currentStructure++)   // Place random structures
                placeObject(pickRandomStructure(), structureDescendant);
            
            return structureDescendant;
        }


        /// <summary>Generates foliage</summary>
         private GameObject generateFoliage()
        {
            GameObject foliageDescendant = new GameObject("Foliage");                             //Create a container object for all tree clusters

            for (int currentCluster = 1; currentCluster <= maxFoliageClusters; currentCluster++)  // place random foliage
                tools.setParent(generateCluster(pickRandomFoliage(), tools.PickRandomLocation(collider, gameObject.transform, rayHeight)), foliageDescendant);

            return foliageDescendant;
        }

        /// <summary>Creates a cluster based on the parsed object</summary>
        /// Cluster is based on parsed origin
        private GameObject generateCluster(GameObject clusterObject, Vector3 origin)
        {
            GameObject newCluster = new GameObject("Foliage Cluster");                       // Create a blank container for this cluster.
            for (int clusterSize = 0; clusterSize <= UnityEngine.Random.Range(1, maxClusterSize); clusterSize++)
            {
                Vector3 childLocation = origin + (UtilsClass.GetRandomDir() * clusterSpread); // Calculate location based on cluser origin, random direction, and cluster spread.
                childLocation.y = tools.getHeightAt(childLocation.x, childLocation.z, rayHeight, collider);              // Raycast to get the height at the selected location 
                if (childLocation.y == -999f) continue;                                       // skip if raycast missed, position was off the edge of the world OR world mesh was above ray height (See rayHeight property)
                placeObject(clusterObject, childLocation, newCluster);                        // Place child in cluster
            }
            return newCluster;
        }

        #endregion
        #endregion

        #region functional utilities

     
        /// <summary>if a nav mesh is attached, it is prompted to bake a nav mesh.</summary>
        private void UpdateNavMesh()
        {
            NavMeshSurface surface = GetComponent<NavMeshSurface>();
            if (surface == null) return;
            surface.BuildNavMesh();
        }

        private void placeObject(GameObject toPlace, GameObject parentDescenant) =>  placeObject(toPlace, tools.PickRandomLocation(collider, gameObject.transform, rayHeight), parentDescenant);

        private void placeObject(GameObject toPlace, Vector3 location,GameObject parentDescenant){
            GameObject newObject = Instantiate(toPlace, location, toPlace.transform.rotation); // generate new object
            tools.setParent(newObject, parentDescenant);                                       // set it's parent
        }



        /// <summary>Determines if a vector's position is blank<summary>
        /// true if all planes are 0.
        private bool vectorIsEmpty(Vector3 vector) => vector.x == 0 && vector.y == 0 && vector.z == 0;

        /// <summary>Randomly returns a tree from the editor defined list of trees</summary>
        private GameObject pickRandomFoliage() => tools.RandomInList<GameObject>(foliage);

        /// <summary>Randomly returns a structure from the editor defined list of structures</summary>
        private GameObject pickRandomStructure() => tools.RandomInList<GameObject>(structures);

        /// <summary>Random tree count using the max number of trees permitted</summary> 
        [Obsolete("Cluster sizes are now in use instead.")]
        private int randomTreeCount() => UnityEngine.Random.Range(0, calculateMaxTreeCount());

        /// <summary>Uses the area and tree permittence to calculate the max number of trees permitted</summary>
        private int calculateMaxTreeCount() => (int)Math.Round(calculateLocalArea()) * maxFoliageClusters;

        /// <summary>Calculates the 2d area of the mesh</summary>
        /// Ignores fluctuation due to perlin
        private float calculateLocalArea() => xSize * zSize;

        private int triCount() => (int)calculateLocalArea() * 6;

        #endregion

        /// This region is a right mess, but it's functional ಠ_ಠ
        #region Console Command
        public static string Help(string command, bool verbose)
        {
            return verbose ? "Uses the world factory to generate a new world." : "starts over";
        }

        public static string Execute(string[] tokens)
        {
            if (tokens != null && tokens.Length >= 0)
                switch (tokens[0].ToLower())
                {
                    case "regen":
                        return regen();
                    case "set":
                        if (tokens.Length == 3)
                        {
                            switch (tokens[1])
                            {
                                case "worldsize":
                                    GetFactory().xSize = byte.Parse(tokens[2]);
                                    GetFactory().zSize = byte.Parse(tokens[2]);
                                    return "Size set, use 'world regen' to see.";

                                case "foliageclusters":
                                    GetFactory().maxFoliageClusters = byte.Parse(tokens[2]);
                                    return "Max foliage clusters set, use 'world regen' to see.";
                                case "perlinzoom":
                                    GetFactory().perlinScale = float.Parse(tokens[2]);
                                    return "Perlin zoom set, use 'world regen' to see.";
                                case "heightzoom":
                                    GetFactory().heightZoom = float.Parse(tokens[2]);
                                    return "Height set, use 'world regen' to see.";
                                case "clusterspread":
                                    GetFactory().clusterSpread = byte.Parse(tokens[2]);
                                    return "Cluster spread set, use 'world regen' to see.";
                                default:
                                    return "Usage: World set {worldsize, foliageclusters, clusterspread, perlinzoom, heightzoom.}";
                            }
                        }
                        else
                        {
                            return "Usage: World set {worldsize, foliageclusters, clusterspread, perlinzoom, heightzoom.}";
                        }
                }
            return "Usage: World {regen, set}";
        }

        public static List<string> FetchAutocompleteOptions(string command, string[] tokens)
        {
            return null;
        }

        public static string regen()
        {
            WorldFactory factory = GetFactory();
            var sw = Stopwatch.StartNew();
            sw.Start();
            factory.Start();
            sw.Stop();
            return "Generated a new world in " + sw.ElapsedMilliseconds + "ms.";
        }

        private static WorldFactory GetFactory() => GameObject.Find("World").GetComponent<WorldFactory>();
        #endregion
    }
}