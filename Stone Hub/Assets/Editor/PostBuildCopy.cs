using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.IO;

public class PostBuildCopy {
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
        string buildFolder = Path.GetDirectoryName(pathToBuiltProject);
        string sourceBuildsFolder = Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Builds");
        string destinationBuildsFolder = Path.Combine(buildFolder, "Builds");

        if (Directory.Exists(destinationBuildsFolder)) {
            Directory.Delete(destinationBuildsFolder, true);
        }

        if (Directory.Exists(sourceBuildsFolder)) {
            CopyDirectory(sourceBuildsFolder, destinationBuildsFolder);
            Debug.Log($"Copiat Builds a {destinationBuildsFolder}");
        } else {
            Debug.LogWarning("No s'ha trobat la carpeta Builds a l'arrel del projecte.");
        }
    }

    static void CopyDirectory(string sourceDir, string destinationDir) {
        Directory.CreateDirectory(destinationDir);

        foreach (string file in Directory.GetFiles(sourceDir)) {
            string destFile = Path.Combine(destinationDir, Path.GetFileName(file));
            File.Copy(file, destFile, true);
        }

        foreach (string folder in Directory.GetDirectories(sourceDir)) {
            string destFolder = Path.Combine(destinationDir, Path.GetFileName(folder));
            CopyDirectory(folder, destFolder);
        }
    }
}