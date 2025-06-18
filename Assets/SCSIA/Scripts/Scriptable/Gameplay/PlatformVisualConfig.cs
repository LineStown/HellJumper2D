using UnityEngine;

[CreateAssetMenu(fileName = "PlatformVisual", menuName = "Scriptable Objects/PlatformVisual")]
public class PlatformVisualConfig : ScriptableObject
{
    [Header("Platform rendere prefabs")]
    [SerializeField] public GameObject[] _platformRendererPrefabs;
}
