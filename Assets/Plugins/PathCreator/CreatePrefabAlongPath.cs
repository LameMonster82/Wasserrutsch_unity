using UnityEngine;
using PathCreation;
using UnityEditor;
using System.Linq;

[ExecuteInEditMode]
public class CreatePrefabAlongPath : MonoBehaviour
{
    [SerializeField] private GameObject prefabObject;
    [SerializeField] private PathCreator pathCurve;

    BoxCollider prefabCollider;
    MeshRenderer prefMesh;

    private void Awake()
    {
        prefabCollider = prefabObject.GetComponentInChildren<BoxCollider>();
        prefMesh = prefabObject.GetComponentInChildren<MeshRenderer>();
    }

    public void InstantiatePrefab()
    {
        var tempList = transform.Cast<Transform>().ToList();

        foreach ( Transform child in tempList)
        {
            DestroyImmediate(child.gameObject);
        }

        for (float i = 0; i < pathCurve.path.length; i += prefMesh.bounds.size.z)
        {
            Instantiate(prefabObject, pathCurve.path.GetPointAtDistance(i), pathCurve.path.GetRotationAtDistance(i), transform);
        }
    }

    public void Delete()
    {
        var tempList = transform.Cast<Transform>().ToList();
        foreach (Transform child in tempList)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}

[CustomEditor(typeof(CreatePrefabAlongPath))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUI.BeginChangeCheck();
        CreatePrefabAlongPath script = (CreatePrefabAlongPath)target;
        if(GUILayout.Button("Build Object"))
        {
            script.InstantiatePrefab();
        }

        if (GUILayout.Button("Delete"))
        {
            script.Delete();
        }
    }
}
