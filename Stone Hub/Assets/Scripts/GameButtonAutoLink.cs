using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

[RequireComponent(typeof(Button))]
public class GameButtonAutoLink : MonoBehaviour {
    void OnValidate() {
        #if UNITY_EDITOR
        Button button = GetComponent<Button>();
        if (button == null) return;

        GameObject managerGO = GameObject.Find("GameManager");
        ExternalLauncher launcher = managerGO.GetComponent<ExternalLauncher>();

        int count = button.onClick.GetPersistentEventCount();
        for (int i = count - 1; i >= 0; i--) {
            UnityEventTools.RemovePersistentListener(button.onClick, i);
        }

        UnityEventTools.AddStringPersistentListener(
            button.onClick,
            launcher.LaunchGame,
            transform.name
        );

        EditorUtility.SetDirty(button);
        #endif
    }
}