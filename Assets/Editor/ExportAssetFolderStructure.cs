using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class ExportAssetFolderStructure : MonoBehaviour
{
    [MenuItem("Tools/Export/Export Asset Folder Structure")]
    private static void ExportStructure()
    {
        string path = EditorUtility.SaveFilePanel("Save Asset Structure", "", "AssetStructure", "txt");
        if (string.IsNullOrEmpty(path)) return;

        StringBuilder sb = new StringBuilder();
        ProcessDirectory("Assets", sb, "");

        File.WriteAllText(path, sb.ToString());
        Debug.Log("Asset structure exported to " + path);
    }

    private static void ProcessDirectory(string dirPath, StringBuilder sb, string indent)
    {
        string[] subDirs = AssetDatabase.GetSubFolders(dirPath);
        string[] files = Directory.GetFiles(dirPath);

        foreach (string file in files)
        {
            if (file.EndsWith(".meta")) continue;
            sb.AppendLine(indent + Path.GetFileName(file));
        }

        foreach (string dir in subDirs)
        {
            sb.AppendLine(indent + Path.GetFileName(dir) + "/");
            ProcessDirectory(dir, sb, indent + "  ");
        }
    }
}
