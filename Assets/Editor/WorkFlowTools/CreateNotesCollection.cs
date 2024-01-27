using UnityEngine;
using UnityEditor;

// Utility class for creating new NotesCollection assets with a specific naming convention
public static class CreateNotesCollection
{
   [MenuItem("Tools/Create/Note Collection")]
   public static void CreateMyAsset()
   {
      // Create a new instance of NotesCollection
      NotesCollection asset = ScriptableObject.CreateInstance<NotesCollection>();

      // Generate the path and file name for the new asset
      string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath($"{GetFolderPath()}/{GetAssetName()}.asset");

      // Create and save the new asset
      AssetDatabase.CreateAsset(asset, assetPathAndName);
      AssetDatabase.SaveAssets();
      EditorUtility.FocusProjectWindow();
      Selection.activeObject = asset;
   }

   // Get the project-specific folder path for storing NotesCollection assets
   private static string GetFolderPath()
   {
      // Replace with your actual folder path within the Assets directory
      return "Assets/Editor/WorkFlowTools/";
   }

   // Generate a unique file name based on the current date and project name
   private static string GetAssetName()
   {
      string projectName = "PSG";
      string date = System.DateTime.Now.ToString("dd_MMM");
      return $"{date}_{projectName}_Notes";
   }
}
