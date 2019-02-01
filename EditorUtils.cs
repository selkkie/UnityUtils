using UnityEngine;
using UnityEditor;
 
public class EditorUtils : Editor
{

    //FOLD ALL WINDOWS
    //Taken from febucci at https://www.febucci.com/2018/11/unity-tips-collection/
    //This code is released under the MIT license: https://opensource.org/licenses/MIT
    [MenuItem("Window/Fold all")]
    static void UnfoldSelection()
    {
        EditorApplication.ExecuteMenuItem("Window/General/Hierarchy");
        var hierarchyWindow = EditorWindow.focusedWindow;
        var expandMethodInfo = hierarchyWindow.GetType().GetMethod("SetExpandedRecursive");
        foreach (GameObject root in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            expandMethodInfo.Invoke(hierarchyWindow, new object[] { root.GetInstanceID(), false });
        }
    }

    //GROUP OBJECTS
    //Taken from bjennings76 at https://answers.unity.com/questions/118306/grouping-objects-in-the-hierarchy.html
    [MenuItem("GameObject/Group Selected %g")]
    private static void GroupSelected()
    {
        if (!Selection.activeTransform) return;
        var go = new GameObject(Selection.activeTransform.name + " Group");
        Undo.RegisterCreatedObjectUndo(go, "Group Selected");
        go.transform.SetParent(Selection.activeTransform.parent, false);
        foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
        Selection.activeGameObject = go;
    }
}
 
