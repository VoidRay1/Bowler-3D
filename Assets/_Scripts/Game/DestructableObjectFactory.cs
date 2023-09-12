using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DestructableObjectFactory : ScriptableObject
{
    [SerializeField] private DestructableObject _cocaColaBottle;
    [SerializeField] private DestructableObject _cocaColaCan;
    [SerializeField] private DestructableObject _3LJar;

    [SerializeField] private List<DestructableObject> _destructableObjects = new List<DestructableObject>();

    public DestructableObject GetDestructableObject(DestructableObjectType destructableObjectType)
    {
        switch(destructableObjectType)
        {
            case DestructableObjectType.CocaColaBottle: 
                return _cocaColaBottle;
            case DestructableObjectType.CocaColaCan:
                return _cocaColaCan;
            case DestructableObjectType.Jar3L:
                return _3LJar;
        }
        return null;
    }

    public DestructableObject GetRandomDestructableObject()
    {
        return _destructableObjects[Random.Range(0, _destructableObjects.Count)];
    }
}