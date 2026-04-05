using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GodlikeShortcuts.Shortcuts
{
    public class HierarchyObjectMover : MonoBehaviour
    {
        [MenuItem("GodlikeShortcuts/Shortcuts/Move Up &r")]
        private static void MoveUp()
        {
            GameObject[] selection = Selection.gameObjects;
            if (selection.Length != 1) 
                return;
        
            Undo.RegisterCompleteObjectUndo(selection, "Selected Objects");

            GameObject selected = selection[0];
            int index = selected.transform.GetSiblingIndex();
        
            if (index <= 0) 
                return;
        
            Undo.RecordObject(selected.transform, "Move Object");
            selected.transform.SetSiblingIndex(index - 1);
        }
    
        [MenuItem("GodlikeShortcuts/Shortcuts/Move Down &r")]
        private static void MoveDown()
        {
            GameObject[] selection = Selection.gameObjects;
            if (selection.Length != 1) 
                return;
        
            Undo.RegisterCompleteObjectUndo(selection, "Selected Objects");

            GameObject selected = selection[0];
            int index = selected.transform.GetSiblingIndex();

            Scene scene = SceneManager.GetActiveScene();
        
            int objectsCount = selected.transform.parent == null ? 
                scene.rootCount - 1 : selected.transform.parent.childCount - 1;
        
            if (index >= objectsCount) 
                return;
        
            Undo.RecordObject(selected.transform, "Move Object");
            selected.transform.SetSiblingIndex(index + 1);
        }
    }
}
