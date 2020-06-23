using UnityEngine;
using UnityEngine.Assertions;
using CodeMonkey.Utils;

public class PatchSpawner : MonoBehaviour
{
    #region properties
    #region editor settings
    [Header("Settings")]
    /// <summary>Object to be spawned in patch</summary>
    /// parsed to GameObject.Instantiate to create a clone.
    [Tooltip("Object to be spawned on the patch")]
    public GameObject instantiable;

    /// <summary>Maximum number of carrots that can spawn in this patch</summary>
    [Tooltip("Maximum number of carrots that can spawn in this patch")]
    public int maxSpawnCount;

    /// <summary>String to tag all spawned objects with</summary>
    [Tooltip("String to tag all spawned objects with")]
    public string tag;

    /// <summary>Minimum time to wait between each spawn</summary>
    /// This time (s) must pass after a sucessful spawn
    [Tooltip("Minimum time to wait between each spawn (s)")]
    public float minSpawnTime = 5;

    /// <summary>Minimum time to wait between each attempt</summary>
    /// Wait time between rejected spawn attempts
    /// This time (s) must pass before spawn attempts can be made again.
    [Tooltip("Minimum time to wait between failed spawn attempts")]
    public float minAttemptTime = 5;

    /// <summary>Chance of spawning an object on each try</summary>
    [Tooltip("Chance [0%-100%] that a spawn attempt will be successful")]
    public byte spawnChance = 100;

    /// <summary>Spawn origin offset. Use to match object mesh centre</summary>
    [Tooltip("Spawn origin offset. Use to match object mesh centre")]
    public float xOffset, yOffset, zOffset;

    /// <summary>Spawn a object at the GO's position, that is untagged.</summary>
    /// Used in carrot patches to create a permenent carrot that is ignored by the rabbits.
    [Tooltip("Spawn a object at the GO's position, that is untagged.")]
    public bool untaggedDefault = true;

    /// <summary>Determines if the spawned objects are children</summary>
    [Tooltip("Determines if the spawned objects are children")]
    public bool areChildren = true;

    #endregion

    #region private
    /// <summary>Delta Time passed since last successful spawn</summary>
    private float lastSpawnSuccess = 0;

    /// <summary>Delta Time passed since last spawn attempt (Failed or successful)</summary>
    private float lastSpawnAttempt = 0;

    /// <summary>Spawner is valid and will be active</summary>
    private bool ValidConfig;
    #endregion
    #endregion
    void Start()
    {
        if (!CheckConfiguration()) return;          // If not valid for operation, return.
        if (untaggedDefault) Spawn(1, false);
    }

    /// <summary>Validate editor settings and determine if spawner may activate.</summary>
    private bool CheckConfiguration()
    {
        ValidConfig = false;
        Assert.IsNotNull(instantiable);
        if (instantiable == null)
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

    /// <summary>attempts to spawn based on spawn state and probability<summary>
    private void TrySpawn(){
        if (lastSpawnSuccess                < minSpawnTime  ) return;      // Only start an attempt if minimum time has passed since last success
        if (lastSpawnAttempt                < minAttemptTime) return;      // Only attempt if minimim time since the last attempt has passed
        if (gameObject.transform.childCount > maxSpawnCount ) return;      // Don't spawn more than permitted at once
        if (Random.Range(0, 100)            > spawnChance   ) {            // determine if this fails or passes
            lastSpawnAttempt = 0;                                                    
            return;  
        }    
        Spawn(Random.Range(1, maxSpawnCount), true);
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
    private void Spawn(int quantity, bool randomPosition)
    {
        for (int currentSpawn = 0; currentSpawn <= quantity; currentSpawn++)                        // for quantity
        {  
            Vector3 UPosition = randomPosition ? gameObject.transform.position + UtilsClass.GetRandomDir(): gameObject.transform.position; 
            Vector3 newPosition = Relatise(UPosition);                                                    // Update position with object offset

            GameObject newSpawn = Instantiate(instantiable, newPosition , Quaternion.identity);     // spawn new object as specified
            if (areChildren) utility.tools.setParent(newSpawn, gameObject);                         // set as child
            if (tag != "") newSpawn.tag = tag;
        }
    }

    /// <summary>Modifies position with editor set offsets</summary>
    private Vector3 Relatise(Vector3 position) => new Vector3(position.x + xOffset, position.y + yOffset, position.z + zOffset);
}
