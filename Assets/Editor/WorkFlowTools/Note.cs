using System;

[System.Serializable]
public class Note
{
   public string creationDate;
   public string title; // A brief title for the note
   public NoteCategory category; // The category of the note
   public bool completed; // Whether the note/task is completed
   public PriorityLevel priority; // Priority of the note/task

   public NoteStatus status; // Current status of the note
   public string dependencies; // Other notes/tasks this one depends on
   public string text; // The main content of the note
   public string fileName;
   public int lineNumber;


   public bool isExpanded;

   public Note()
   {
      creationDate = DateTime.Now.ToString("dd_MMM - HH: mm");
   }
}

public enum NoteCategory
{
   TODO,
   Bug,
   Feature,
   Improvement,
   Design,
   Testing,
   Documentation,
   Other
}

public enum PriorityLevel
{
   Low,
   Medium,
   High,
   Critical
}

public enum NoteStatus
{
   NotStarted,
   InProgress,
   OnHold,
   Completed,
   Abandoned
}
