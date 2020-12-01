using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHealthBars : Imported.VisibleRenderers
{
    public List<DestructableObject> visibleDestructableObjects = new List<DestructableObject>();
    public GameObject HealthBarPrefab;

    void Update()
    {
        FindObjects();// this method is in the parent class, VisibleRenders.cs
        foreach(Renderer renderer in visibleRenderers) // visible Renders is a list in the parent class.
        {
            Transform potentialDestructibleObject;

            if(renderer.transform.parent != null) potentialDestructibleObject = renderer.transform.parent;//if it is a skinned MeshRender, then the Destructible object script will be on the parent.
            else potentialDestructibleObject = renderer.transform;// else it's a turret, therefore it will be on that object.

            if(potentialDestructibleObject.TryGetComponent(out DestructableObject obj))
            {
                if (!visibleDestructableObjects.Contains(obj))
                {
                    visibleDestructableObjects.Add(obj);
                    GameObject bar = Instantiate(HealthBarPrefab, transform);
                    bar.GetComponent<HealthBar>().obj = obj;
                }
            }
        }
        
    }
}
