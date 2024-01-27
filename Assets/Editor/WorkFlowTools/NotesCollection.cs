using UnityEngine;
using System.Collections.Generic;

// ScriptableObject representing a collection of notes
[CreateAssetMenu(fileName = "NewNotesCollection", menuName = "Notes/NoteCollection")]
public class NotesCollection :ScriptableObject
{
   // List of notes in this collection
   public List<Note> notes = new List<Note>();
}

