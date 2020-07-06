using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    [SerializeField] GameObject _artefactPrefab;
    [SerializeField] GameObject _keyPrefab;

    private void Awake()
    {
        GameObject[] _locationsOfArtefactsGO = GameObject.FindGameObjectsWithTag("LocationOfTheArtifact");
        GameObject _artefactParrentGO = GameObject.Find("Artefacts");
        Spawner.Spawn(_artefactPrefab, _locationsOfArtefactsGO, 6, _artefactParrentGO);
        
        GameObject[] _locationsOfKeyGO = GameObject.FindGameObjectsWithTag("KeySpawn");
        GameObject _keyParrentGO = GameObject.Find("Keys");
        Spawner.Spawn(_keyPrefab, _locationsOfKeyGO, 5, _keyParrentGO);
    }
}
