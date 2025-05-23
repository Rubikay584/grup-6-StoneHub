using UnityEngine;
using System.Diagnostics;
using System.IO;

public class ExternalLauncher : MonoBehaviour {
    public void LaunchGame(string gameName) {
        string exePath = Path.Combine(Application.dataPath, $"../Builds/{gameName}/{gameName}.exe");
        exePath = Path.GetFullPath(exePath);

        if (File.Exists(exePath)) {
            Process.Start(exePath);
        } else {
            UnityEngine.Debug.LogError($"No s'ha trobat l'executable del joc: {exePath}");
        }
    }

    public void ExitApp() {
        Application.Quit();
    }
}
