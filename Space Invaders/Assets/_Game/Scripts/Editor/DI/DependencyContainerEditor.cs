using System.Linq;
using Runtime.DI;
using Runtime.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Editor.DI
{
    [CustomEditor(typeof(DependencyContainer))]
    public class DependencyContainerEditor : UnityEditor.Editor
    {
        private SerializedProperty requestersProperty;
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            DependencyContainer container = (DependencyContainer)target;

            if (GUILayout.Button("Find IRequesters"))
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation / This is rarely done on Editor, so we can afford it
                MonoBehaviour[] newRequesters = Resources.FindObjectsOfTypeAll<MonoBehaviour>()
                    .Where(x => x is IRequester && x.gameObject.scene.name != null)
                    .ToArray();

                container.SetRequesters(newRequesters);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}