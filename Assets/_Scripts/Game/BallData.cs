using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu]
public class BallData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Ball _ball;
    [SerializeField, HideInInspector] private string _id;

    public bool IsUnlocked;
    public string ID => _id;
    public string Name => _name;
    public Ball Ball => _ball;

#if UNITY_EDITOR
    private void OnEnable()
    {
        if (string.IsNullOrEmpty(ID))
        {
            _id = Guid.NewGuid().ToString();
            EditorUtility.SetDirty(this);
        }
    }
#endif
}

[Serializable]
public class SerializableBallData
{
    public string ID;
    public bool IsUnlocked;
}