using Game.Character;
using UnityEditor;
using UnityEngine;

namespace Game.Editor.CustomEditors
{
    [CustomEditor(typeof(UseUnityCamera), false)]
    public class UseUnityCameraEditor : UnityEditor.Editor
    {
        private SerializedProperty _p_useMainCamera;
        private SerializedProperty _p_assignedCamera;
        private SerializedProperty _p_cameraSocket;

        private readonly GUIContent _useMainCameraLabel = new("Use Main Camera", "If checked, the MainCamera will be picked from the scene and attached to the camera socket");
        private readonly GUIContent _assignedCameraLabel = new("Camera", "Assign the custom camera if automatically picking MainCamera is undesired");
        private readonly GUIContent _cameraSocketaLabel = new("Camera Socket", "The transform the camera will be attached to");

        private void OnEnable()
        {
            _p_useMainCamera = serializedObject.FindProperty("useMainCamera");
            _p_assignedCamera = serializedObject.FindProperty("assignedCamera");
            _p_cameraSocket = serializedObject.FindProperty("cameraSocket");
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            _p_useMainCamera.boolValue = EditorGUILayout.ToggleLeft(_useMainCameraLabel, _p_useMainCamera.boolValue, EditorStyles.whiteMiniLabel);

            if (!_p_useMainCamera.boolValue)
            {
                EditorGUI.indentLevel++;
                BsrEditorTool.BeginColor(Colors.red, () => _p_assignedCamera.objectReferenceValue == null, ColorOptions.Background);
                _p_assignedCamera.objectReferenceValue = EditorGUILayout.ObjectField(_assignedCameraLabel, _p_assignedCamera.objectReferenceValue, typeof(Camera), true);
                BsrEditorTool.EndColor(ColorOptions.Background);
                EditorGUI.indentLevel--;
            }

            _p_cameraSocket.objectReferenceValue = EditorGUILayout.ObjectField(_cameraSocketaLabel, _p_cameraSocket.objectReferenceValue, typeof(Transform), true);
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}