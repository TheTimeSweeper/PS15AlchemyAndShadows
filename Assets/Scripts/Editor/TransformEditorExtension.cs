
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TransformEditorExtension : MonoBehaviour
{

    //rescales transform without affecting children
    //detaches children, sets scale to (1,1,1), reattaches children
    [MenuItem("CONTEXT/Transform/Reset Scale Safely")]
    public static void safeRescale()
    {

        for (int i = 0; i < Selection.transforms.Length; i++)
        {
            safeRescaleTransform(Selection.transforms[i]);
        }
    }

    //multiplies rect size by current scale, then resets scale to (1,1,1)
    [MenuItem("CONTEXT/RectTransform/Cleanup Rect Scale")]
    public static void cleanupScale()
    {

        for (int i = 0; i < Selection.transforms.Length; i++)
        {
            cleanupScaleTransform(Selection.transforms[i]);
        }
    }

    public static void safeRescaleTransform(Transform selected)
    {

        Undo.RecordObject(selected, "reset scale safely");

        List<Transform> children = new List<Transform>();

        for (int i = selected.childCount - 1; i >= 0; i--)
        {

            Transform child = selected.GetChild(0);
            Undo.RecordObject(child, "reset scale safely");

            children.Add(child);
            child.parent = selected.transform.parent;
        }

        selected.localScale = Vector3.one;
        for (int i = 0; i < children.Count; i++)
        {
            children[i].parent = selected;
        }
    }

    public static void cleanupScaleTransform(Transform selected)
    {

        RectTransform rect = selected.GetComponent<RectTransform>();

        if (rect == null)
            return;

        Undo.RecordObject(rect, "cleanup rect scale");

        rect.sizeDelta *= rect.localScale;
        rect.sizeDelta = new Vector2(Mathf.RoundToInt(rect.sizeDelta.x), Mathf.RoundToInt(rect.sizeDelta.y));
        rect.localScale = new Vector3(rect.sizeDelta.x > 0 ? 1 : -1, rect.sizeDelta.y > 0 ? 1 : -1, 1);
        rect.sizeDelta = new Vector2(Mathf.Abs(rect.sizeDelta.x), Mathf.Abs(rect.sizeDelta.y));
    }
}