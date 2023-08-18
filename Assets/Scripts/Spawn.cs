using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : RoadPool
{
    [SerializeField] private Destroyer _destroyer;
    [SerializeField] private PlayerMove _playerScript;

    private int _spawnedRoadCount = 3;

    private void OnEnable()
    {
        _destroyer.RoadDestroyTriggerReached += OnRoadDestroyTriggerReached;
        _playerScript.ShitHappened += OnShitHappened;
    }

    private void OnDisable()
    {
        _destroyer.RoadDestroyTriggerReached -= OnRoadDestroyTriggerReached;
        _playerScript.ShitHappened -= OnShitHappened;
    }

    private void Start()
    {
        Initialize();
    }

    private void OnRoadDestroyTriggerReached(GameObject target)
    {
        target.SetActive(false);
        SpawnNewRoad();
    }

    private void SpawnNewRoad()
    {
        GameObject newRoad;
        GameObject lastSpawned;
        RoadMove roadScript;
        Vector3 roadScale;
        Vector3 position;

        if (TryGetObject(out newRoad, out lastSpawned) != false)
        {
            newRoad.transform.parent = null;
            roadScript = newRoad.GetComponentInChildren<RoadMove>();
            roadScale = lastSpawned.GetComponentInChildren<Road>().transform.localScale;

            if(newRoad == lastSpawned)
            {
                position = new Vector3(roadScale.x / 2, 0, 0);
            }
            else
            {
                position = new Vector3(lastSpawned.transform.position.x + roadScale.x, 0, 0);
                roadScript.SetPitching();
            }

            newRoad.transform.SetPositionAndRotation(position, Quaternion.identity);
            newRoad.SetActive(true);

            foreach (var gem in gemsByParentRoad[newRoad])
            {
                gem.SetActive(true);
            }
        }
    }

    public void OnShitHappened()
    {
        ResetPool();

        for (int i = 0; i < _spawnedRoadCount; i++)
        {
            SpawnNewRoad();
        }
    }
}
