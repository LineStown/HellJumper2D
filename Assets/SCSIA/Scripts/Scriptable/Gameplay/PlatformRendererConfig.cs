using UnityEngine;

[CreateAssetMenu(fileName = "PlatformRendererConfig", menuName = "Scriptable Objects/PlatformRendererConfig")]
public class PlatformRendererConfig : ScriptableObject
{
    [Header("Platform renderer prefabs")]
    [SerializeField] public GameObject[] _platformRendererPrefabs;
}
