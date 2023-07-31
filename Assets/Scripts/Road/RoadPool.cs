using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadPool : MonoBehaviour
{
    [SerializeField] protected List<GameObject> roadPool;

    protected Dictionary<GameObject, List<GameObject>> gemsByParentRoad = new Dictionary<GameObject, List<GameObject>>();

    protected void Initialize()
    {
        Gem[] gems;
        List<GameObject> gemObjects;

        for (int i = 0; i < roadPool.Count; i++)
        {
            var spawned = Instantiate(roadPool[i]);
            spawned.SetActive(false);
            roadPool[i] = spawned;
        }

        foreach (var road in roadPool)
        {
            gems = road.GetComponentsInChildren<Gem>();
            gemObjects = new List<GameObject>();

            foreach (var gem in gems)
            {
                gemObjects.Add(gem.gameObject);
            }

            gemsByParentRoad.Add(road, gemObjects);
        }
    }

    protected void ResetPool()
    {
        foreach (var road in roadPool)
        {
            road.SetActive(false);
        }
    }

    protected bool TryGetObject(out GameObject result, out GameObject lastSpawned)
    {
        var notActive = roadPool.Where(p => p.activeSelf == false);
        var active = roadPool.Where(p => p.activeSelf == true);

        if (active.Count() == 0)
        {
            result = roadPool[0];
            lastSpawned = result;
        }
        else
        {
            result = notActive.ElementAt(Random.Range(0, notActive.Count()));
            lastSpawned = active.OrderBy(p => p.transform.position.x).LastOrDefault();
        }

        return result != null;
    }
}
