using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(LevelDataSO))]
public class PuzzleLevelDataDrawer : Editor
{
    private const string PROPERTY_PATH_WIDTH = "m_width";
    private const string PROPERTY_PATH_HEIGHT = "m_height";
    private const string PROPERTY_PATH_PATTERN = "m_patternFlatter";
    private const string PROPERTY_PATH_MOVES = "Moves";

    private SerializedProperty sWidth;
    private SerializedProperty sHeight;
    private SerializedProperty sPattern;
    private SerializedProperty sMoves;

    private void OnEnable()
    {
        sWidth = serializedObject.FindProperty(PROPERTY_PATH_WIDTH);
        sHeight = serializedObject.FindProperty(PROPERTY_PATH_HEIGHT);
        sPattern = serializedObject.FindProperty(PROPERTY_PATH_PATTERN);
        sMoves = serializedObject.FindProperty(PROPERTY_PATH_MOVES);

    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement visualElement = new VisualElement();
        ScrollView scrollView = new ScrollView(ScrollViewMode.Vertical);
        GroupBox groupBoxSettings = new GroupBox("Settings");
        IMGUIContainer imguiContainerSettings = new IMGUIContainer(() =>
        {
            using (new GUILayout.VerticalScope())
            {
                using (var changeCheckScope = new EditorGUI.ChangeCheckScope())
                {
                    int uWidth = EditorGUILayout.IntField("Width", sWidth.intValue);
                    sWidth.intValue = uWidth > 0 ? uWidth : 1;
                    int uHeight = EditorGUILayout.IntField("Height", sHeight.intValue);
                    sHeight.intValue = uHeight > 0 ? uHeight : 1;
                    int uMoves = EditorGUILayout.IntField("Move", sMoves.intValue);
                    sMoves.intValue = uMoves > 0 ? uMoves : 1;
                    if (GUILayout.Button("Update Size"))
                        sPattern.arraySize = sWidth.intValue * sHeight.intValue;
                    if (changeCheckScope.changed)
                        serializedObject.ApplyModifiedProperties();
                }
            }
        });
        groupBoxSettings.Add(imguiContainerSettings);
        GroupBox groupBoxGemFieldEditor = new GroupBox();
        IMGUIContainer imguiContainerFieldEditor = new IMGUIContainer(() =>
        {
            using (new GUILayout.VerticalScope())
            {
                using (var changeCheckScope = new EditorGUI.ChangeCheckScope())
                {
                    if (sPattern.arraySize == sWidth.intValue * sHeight.intValue)
                    {
                        using (new GUILayout.VerticalScope())
                        {
                            for (int y = 0; y < sHeight.intValue; ++y)
                            {
                                using (new GUILayout.HorizontalScope())
                                {
                                    GUILayout.FlexibleSpace();
                                    for (int x = 0; x < sWidth.intValue; ++x)
                                    {
                                        var sValue = sPattern.GetArrayElementAtIndex(y * sWidth.intValue + x);
                                        sValue.boolValue = GUILayout.Toggle(sValue.boolValue, GUIContent.none);
                                    }
                                    GUILayout.FlexibleSpace();
                                }
                            }
                        }
                        GUILayout.Space(16.0f);
                        using (new GUILayout.HorizontalScope())
                        {
                            if (GUILayout.Button("Select All"))
                            {
                                for (int i = 0; i < sPattern.arraySize; ++i)
                                {
                                    var sValue = sPattern.GetArrayElementAtIndex(i);
                                    sValue.boolValue = true;
                                }
                            }
                            if (GUILayout.Button("Deselect All"))
                            {
                                for (int i = 0; i < sPattern.arraySize; ++i)
                                {
                                    var sValue = sPattern.GetArrayElementAtIndex(i);
                                    sValue.boolValue = false;
                                }
                            }
                            if (GUILayout.Button("Inverse All"))
                            {
                                for (int i = 0; i < sPattern.arraySize; ++i)
                                {
                                    var sValue = sPattern.GetArrayElementAtIndex(i);
                                    sValue.boolValue = !sValue.boolValue;
                                }
                            }
                            GUILayout.FlexibleSpace();
                        }
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("Cannot display the gem field editor because of size is invalid.\nPlease update size at first", MessageType.Error, true);
                    }
                    if (changeCheckScope.changed)
                        serializedObject.ApplyModifiedProperties();
                }
            }
        });
        groupBoxGemFieldEditor.Add(imguiContainerFieldEditor);
        scrollView.Add(groupBoxSettings);
        scrollView.Add(groupBoxGemFieldEditor);
        visualElement.Add(scrollView);
        return visualElement;
    }

    public override bool UseDefaultMargins()
    {
        return false;
    }
}
