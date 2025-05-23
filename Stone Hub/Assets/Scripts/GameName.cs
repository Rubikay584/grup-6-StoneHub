using UnityEngine;
using TMPro;

public class GameName : MonoBehaviour
{
    private TextMeshProUGUI tmpText;

    void OnValidate() {
        tmpText = GetComponent<TextMeshProUGUI>();
        
        tmpText.text = transform.parent.name;
        #if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(tmpText);
        #endif
    }
}