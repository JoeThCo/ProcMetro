using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomGenerator))]
public class RoomGeneratorEditor : Editor
{
    RoomGenerator roomGenerator;

    private void OnEnable()
    {
        roomGenerator = (RoomGenerator)target;
    }

    public override void OnInspectorGUI()
    {
        if (DrawDefaultInspector())
        {

        }

        if (GUILayout.Button("Make Room"))
        {
            roomGenerator.MakeRoom();
        }
    }
}
