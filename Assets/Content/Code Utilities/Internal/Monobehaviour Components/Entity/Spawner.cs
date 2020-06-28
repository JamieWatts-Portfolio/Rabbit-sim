using System.Collections.Generic;
using utility;
using UnityEngine;
using UnityEngine.Assertions;
using CodeMonkey.Utils;

namespace EntityMonocomponent {
    public class Spawner : MonoBehaviour
    {
        #region properties
            #region editor settings
                #region Settings Header
        [Header("Settings")]
        /// <summary>Object to be spawned in patch</summary>
        /// parsed to GameObject.Instantiate to create a clone.
        [Tooltip("Object to be spawned on the patch")]
        public List<GameObject> spawnPrefabs = new List<GameObject>();

        /// <summary>String to tag all spawned objects with</summary>
        [Tooltip("String to tag all spawned objects with")]
        public string childTag = "";

        /// <summary>Spawn a object at the GO's position, that is untagged.</summary>
        /// Used in carrot patches to create a permenent carrot that is ignored by the rabbits.
        [Tooltip("Spawn a object at the GO's position, that is untagged.")]
        public bool untaggedDefault = true;
        #endregion Settings Header

                #region Spawn Attempts Header
        [Header("Spawn attempts")]
        /// <summary>Chance of spawning an object on each try</summary>
        [Tooltip("Chance [0%-100%] that a spawn attempt will be successful")]
        public byte spawnChance = 100;

        /// <summary>Maximum number of objects that can spawn in this patch</summary>
        /// Automatically alters at runtime to account for prefab children.
        [Tooltip("Maximum number of objects that can spawn in this patch")]
        public byte maxSpawnCount = 2;

        /// <summary>Minimum time to wait between each spawn</summary>
        /// This time (s) must pass after a sucessful spawn
        [Tooltip("Minimum time to wait between each spawn (s)")]
        public float minSpawnTime = 5;

        /// <summary>Minimum time to wait between each attempt</summary>
        /// Wait time between rejected spawn attempts
        /// This time (s) must pass before spawn attempts can be made again.
        [Tooltip("Minimum time to wait between failed spawn attempts")]
        public float minAttemptTime = 5;
        #endregion Spawn Attempts

                #region Scene Header
        [Header("Scene")]
        [Tooltip("Position randomisation scale exponent; effects how large of an area random positions will be in. 0 = no randomness.")]
        public float randomPositionExponent = 2f;

        /// <summary>Spawn origin offset. Use to match object mesh centre</summary>
        [Tooltip("Spawn origin offset. Use to match object mesh centre")]
        public float xOffset, yOffset, zOffset;

        #endregion Scene
        #endregion editor settings

            #region private
        /// <summary>Delta Time passed since last successful spawn</summary>
        private float lastSpawnSuccess = 0;

        /// <summary>Delta Time passed since last spawn attempt (Failed or successful)</summary>
        private float lastSpawnAttempt = 0;

        /// <summary>Spawner is valid and will be active</summary>
        private bool ValidConfig;
        #endregion
        #endregion
        
        #region monobehavour
        void Start()
        {
            enabled = CheckConfiguration();                                // Check component inspector configuration
            if (enabled) return;                                           // If not valid for operation, return.
            

            if (untaggedDefault) Spawn(1);                                 // Create untagged default, if desired; before max count so it's counted.
            maxSpawnCount += (byte) gameObject.transform.childCount;       // Account for exsisting number of children, i.e from prefab.
        }

        /// <summary>Validate editor settings and determine if spawner may activate.</summary>
        private bool CheckConfiguration()
        {
            ValidConfig = false;
            Assert.IsNotNull(spawnPrefabs);
            if (spawnPrefabs == null)
            {
                Debug.LogWarning("PatchSpawner with no object to spawn");
                return ValidConfig;
            }

            bool spawnChanceValid = spawnChance <= 100 && spawnChance > 0;
            Assert.IsTrue(spawnChanceValid);
            if (!spawnChanceValid)
            {
                Debug.LogWarning("PatchSpawner spawn chance out of range [0-100]!");
                return ValidConfig;
            }

            ValidConfig = true;
            return ValidConfig;
        }

        // Update is called once per frame
        void Update()
        {
            TrySpawn();
            UpdateDelta();
        }
        #endregion

        #region spawner
        /// <summary>attempts to spawn based on spawn state and probability<summary>
        private void TrySpawn(){
            if (lastSpawnSuccess                < minSpawnTime   ||             // Only start an attempt if minimum time has passed since last success
                lastSpawnAttempt                < minAttemptTime ||             // Only attempt if minimim time since the last attempt has passed
                gameObject.transform.childCount > maxSpawnCount ) return;       // Don't spawn more than permitted at once
            
            if (Random.Range(0, 100)            > spawnChance   ) {             // determine if this fails or passes
                lastSpawnAttempt = 0;                                                    
                return;  
            }    
            Spawn(Random.Range(1, maxSpawnCount));
            ResetDelta();
        }

        /// <summary>Updates delta time values</summary>
        private void UpdateDelta(){
            lastSpawnAttempt += Time.deltaTime;
            lastSpawnSuccess += Time.deltaTime;
        }

        /// <sumary>Resets delta values to 0</summary>
        private void ResetDelta(){
            lastSpawnAttempt = 0;
            lastSpawnSuccess = 0;
        }

        /// <summary>Spawns <c>instantiable</c> at specified location.</summary>
        private void Spawn(int quantity)
        {
            for (int currentSpawn = 0; currentSpawn <= quantity; currentSpawn++)                        // for quantity
            {  
                bool whattheactualliviningfuck = checkChildCount();
                if (whattheactualliviningfuck) {return;}                                                // If max count is reached, reject all further spawning
                Vector3 UPosition = gameObject.transform.position + (UtilsClass.GetRandomDir() * randomPositionExponent); 
                Vector3 newPosition = Relatise(UPosition);                                              // Update position with object offset

                GameObject newSpawn = Instantiate(getRandomPrefab(), newPosition , Quaternion.identity);// spawn new object as specified
                utility.tools.setParent(newSpawn, gameObject);                                          // set as child
                if (childTag != "") newSpawn.tag = childTag;
            }
        }
        #endregion

        #region functional utilities
        /// <summary>Returns a random prefab</summary>
        private GameObject getRandomPrefab() => tools.RandomInList<GameObject>(spawnPrefabs);

        /// <summary>Modifies position with editor set offsets</summary>
        private Vector3 Relatise(Vector3 position) => new Vector3(position.x + xOffset, gameObject.transform.position.y + yOffset, position.z + zOffset);

        /// <summary>Checks if the childcount has been met or surpassed</summary>
        /// <returns><c>true</c> if child count has been met or surpassed</returns>
        /// <summary><c>false</c> if the max child count has not yet been met</summary>
        private bool checkChildCount() => (gameObject.transform.childCount >= maxSpawnCount);
        #endregion
    }
}