#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ClearPlayerPrefsMenuItem
{
    [MenuItem("Tools/Clear PlayerPrefs")]
    private static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        UnityEngine.Debug.Log("PlayerPrefs cleared.");
    }
}
#endif