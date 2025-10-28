using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public static Collectables instance { get; private set; }

    public event Action<GameObject> OnCollectableGrabbed;

    LinkedList<GameObject> collectablesList;
    public LinkedList<GameObject> CollectablesList { get => collectablesList; }

    void Awake()
    {
        instance = this;
        collectablesList = new LinkedList<GameObject>();
    }

    private void OnEnable()
    {
        OnCollectableGrabbed += Collect;
    }

    private void OnDisable()
    {
        OnCollectableGrabbed -= Collect;
    }

    public void NotifyCollectableGrabbed(GameObject collectable)
    {
        OnCollectableGrabbed(collectable);
    }

    private void Collect(GameObject collectable)
    {
        collectablesList.AddLast(collectable);
        Debug.Log(collectablesList.Count);
    }
}
