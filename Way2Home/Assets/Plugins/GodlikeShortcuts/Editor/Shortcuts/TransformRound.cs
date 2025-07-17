using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GodlikeShortcuts.Shortcuts
{
    public class TransformRound : MonoBehaviour
    {
        public static int RoundIndex;

        private static Dictionary<int, float> _roundFactorsMap = new Dictionary<int, float>
        {
            {0, 0.5f}, {1, 1f}, {2, 5f}, {3, 10f}
        };

        [MenuItem("GodlikeShortcuts/Shortcuts/Round All &r")]
        private static void RoundAll()
        {
            Transform[] selection = Selection.transforms;
            if (selection.Length <= 0) 
                return;
        
            Undo.RegisterCompleteObjectUndo(selection, "Selected Objects");

            Undo.RecordObjects(selection, "Round Transforms");
            foreach (Transform transform in selection)
            {
                RoundTransformPosition(transform, GetFactor());
                RoundTransformRotation(transform, GetFactor());
                RoundTransformScale(transform, GetFactor());
            }
        }
    
        [MenuItem("GodlikeShortcuts/Shortcuts/Round Position &r")]
        private static void RoundPosition()
        {
            Transform[] selection = Selection.transforms;
            if (selection.Length <= 0) 
                return;
        
            Undo.RegisterCompleteObjectUndo(selection, "Selected Objects");

            Undo.RecordObjects(selection, "Round Position &r");
            foreach (Transform transform in selection)
            {
                RoundTransformPosition(transform, GetFactor());
            }
        }
    
        [MenuItem("GodlikeShortcuts/Shortcuts/Round Rotation &r")]
        private static void RoundRotation()
        {
            Transform[] selection = Selection.transforms;
            if (selection.Length <= 0) 
                return;
        
            Undo.RegisterCompleteObjectUndo(selection, "Selected Objects");

            Undo.RecordObjects(selection, "Round Rotation");
            foreach (Transform transform in selection)
            {
                RoundTransformRotation(transform, GetFactor());
            }
        }
    
        [MenuItem("GodlikeShortcuts/Shortcuts/Round Scale &r")]
        private static void RoundScale()
        {
            Transform[] selection = Selection.transforms;
            if (selection.Length <= 0) 
                return;
        
            Undo.RegisterCompleteObjectUndo(selection, "Selected Objects");

            Undo.RecordObjects(selection, "Round Scale");
            foreach (Transform transform in selection)
            {
                RoundTransformScale(transform, GetFactor());
            }
        }

        [MenuItem("GodlikeShortcuts/Shortcuts/Round SizeDelta &r")]
        private static void RoundSizeDelta()
        {
            Transform[] selection = Selection.transforms;
            if (selection.Length <= 0) 
                return;

            foreach (Transform transform in selection)
            {
                if (!transform.TryGetComponent(out RectTransform rect))
                    return;
            }
            
            Undo.RegisterCompleteObjectUndo(selection, "Selected Objects");
            
            Undo.RecordObjects(selection, "Round Scale");
            foreach (Transform transform in selection)
            {
                RoundTransformSizeDelta(transform, GetFactor());
            }
        }

        private static void RoundTransformPosition(Transform target, float factor)
        {
            Vector3 roundTargetPosition = target.localPosition;

            roundTargetPosition.x = Mathf.Round(roundTargetPosition.x / factor) * factor;
            roundTargetPosition.y = Mathf.Round(roundTargetPosition.y / factor) * factor;
            roundTargetPosition.z = Mathf.Round(roundTargetPosition.z / factor) * factor;

            target.localPosition = roundTargetPosition;
        }

        private static void RoundTransformRotation(Transform target, float factor)
        {
            Vector3 roundTargetRotation = target.localRotation.eulerAngles;

            roundTargetRotation.x = Mathf.Round(roundTargetRotation.x / factor) * factor;
            roundTargetRotation.y = Mathf.Round(roundTargetRotation.y / factor) * factor;
            roundTargetRotation.z = Mathf.Round(roundTargetRotation.z / factor) * factor;

            target.localRotation = Quaternion.Euler(roundTargetRotation);
        }

        private static void RoundTransformScale(Transform target, float factor)
        {
            Vector3 roundTargetScale = target.localScale;

            roundTargetScale.x = Mathf.Round(roundTargetScale.x / factor) * factor;
            roundTargetScale.y = Mathf.Round(roundTargetScale.y / factor) * factor;
            roundTargetScale.z = Mathf.Round(roundTargetScale.z / factor) * factor;

            target.localScale = roundTargetScale;
        }

        private static void RoundTransformSizeDelta(Transform target, float factor)
        {
            RectTransform rect = target.GetComponent<RectTransform>();
            Vector2 roundSizeDelta = rect.sizeDelta;

            roundSizeDelta.x = Mathf.Round(roundSizeDelta.x / factor) * factor;
            roundSizeDelta.y = Mathf.Round(roundSizeDelta.y / factor) * factor;

            rect.sizeDelta = roundSizeDelta;
        }

        private static float GetFactor()
        {
            return _roundFactorsMap[RoundIndex];
        }
    }
}
