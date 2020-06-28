using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Monobehaviour component to disable objects in the hierarchy at runtime</summary>
public class RuntimeDisabler : MonoBehaviour
{
    void Start() => gameObject.SetActive(false);
}
