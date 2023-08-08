using System.IO;
using UnityEditor;
using UnityEngine;

namespace Gs2.Editor.ResourceTree.Core
{
    [InitializeOnLoad]
    public class ProjectTabDragAndDrop
    {
        static ProjectTabDragAndDrop()
        {
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowItemGUI;
        }

        private static void OnProjectWindowItemGUI(string guid, Rect selectionRect)
        {
            var current = Event.current;
            switch (current.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!selectionRect.Contains(current.mousePosition)) break;
                    
                    if (DragAndDrop.GetGenericData("Gs2Resource") is ScriptableObject asset)
                    {
                        DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                        DragAndDrop.activeControlID = GUIUtility.GetControlID( FocusType.Passive );

                        if (current.type == EventType.DragPerform)
                        {
                            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                            if (!AssetDatabase.IsValidFolder(assetPath)) {
                                assetPath = Path.GetDirectoryName(assetPath);
                            }
                            if (Directory.Exists(assetPath))
                            {
                                AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath + "/" + asset.name + ".asset"));
                                AssetDatabase.Refresh();

                                DragAndDrop.AcceptDrag();
                                current.Use();
                            }
                        }
                        
                        current.Use();
                    }
                    break;
            }
        }
    }
}
