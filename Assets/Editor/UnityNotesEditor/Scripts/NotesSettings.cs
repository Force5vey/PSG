using UnityEngine;

[CreateAssetMenu(fileName = "NotesSettings", menuName = "Notes/Settings")]
public class NotesSettings :ScriptableObject
{
   [Tooltip("Folder Path in Assets directory for Notes Collections.[Scriptable Object]")]
   public string notesFolderPath = "Assets/Editor/UnityNotesEditor/";

   [Tooltip("Project Name to be used in Notes Collection file name.")]
   public string projectName;

   [Tooltip("Icon for Low Priority")]
   public Texture2D lowPriorityIcon;

   [Tooltip("Icon for Medium Priority")]
   public Texture2D mediumPriorityIcon;

   [Tooltip("Icon for High Priority")]
   public Texture2D highPriorityIcon;

   [Tooltip("Icon for Critical Priority")]
   public Texture2D criticalPriorityIcon;
}

