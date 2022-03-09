using Common;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor for <see cref="GameObjectSpawner"/>
/// </summary>
[CustomEditor(typeof(GameObjectSpawner))]
public class GameObjectSpawnerEditor : Editor {
    private void OnSceneGUI() {
        var script = (GameObjectSpawner) target;
        
        // Draw circles representing spawns
        foreach (var spawn in script.spawns) {
            Handles.DrawWireDisc(new Vector3(spawn.x, 0, spawn.y) + script.transform.position, Vector3.up, script.spawnRadius);
        }
    }
}