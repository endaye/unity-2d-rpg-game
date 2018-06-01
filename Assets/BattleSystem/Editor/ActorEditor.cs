using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Actor))]
public class ActorEditor : Editor
{
    [MenuItem("Assets/Create/Actor")]
    public static void CreateActor()
    {
        AssetUtil.CreateScriptableObject<Actor>();
    }
}
