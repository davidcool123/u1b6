using System;
using System.Collections.Generic;
using System.IO;

class StudentJournal
{
    // ============================================
    // Constants - file paths (don't modify)
    // ============================================
    const string JournalFile = "journal.txt";
    const string BackupFile = "journal-backup.txt";


    // ============================================
    // TODO #1: Initialize your collections here
    // ============================================

    public static List<string> entries = new List<string>();




    // ============================================
    // Main Program - Menu loop (partially complete)
    // ============================================
    static void Main(string[] args)
    {
        // TODO #2: Load existing entries from file at startup
        // ============================================

        if (File.Exists(JournalFile))
        {
            string[] loaded = File.ReadAllLines(JournalFile);
            entries.AddRange(loaded);
        }

        bool running = true;

        while (running)
        {
            DisplayMenu();

            Console.Write("Enter choice (1-7): ");
            string input = Console.ReadLine();

            // ============================================
            // TODO #7: Add try-catch for menu input
            // ============================================

            int choice = int.Parse(input);

            switch (choice)
            {
                case 1:
                    AddEntry();
                    break;
                case 2:
                    ViewAllEntries();
                    break;
                case 3:
                    SearchEntries();
                    break;
                case 4:
                    SaveToFile();
                    break;
                case 5:
                    ExportEntries();
                    break;
                case 6:
                    CreateBackup();
                    break;
                case 7:
                    running = false;
                    Console.WriteLine("\n📖 Goodbye! Keep journaling!");
                    break;
                default:
                    Console.WriteLine("\n❌ Invalid choice. Please enter 1-7.");
                    break;
            }

            // End of try-catch should go here

            if (running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }


    // ============================================
    // Display Menu (complete - don't modify)
    // ============================================
    static void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("═══════════════════════════════════════════");
        Console.WriteLine("          📖 STUDENT JOURNAL");
        Console.WriteLine("═══════════════════════════════════════════");
        Console.WriteLine();
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("  1. Write a new entry");
        Console.WriteLine("  2. View all entries");
        Console.WriteLine("  3. Search entries");
        Console.WriteLine("  4. Save entries to file");
        Console.WriteLine("  5. Export entries to new file");
        Console.WriteLine("  6. Create backup");
        Console.WriteLine("  7. Exit");
        Console.WriteLine();
    }


    // ============================================
    // TODO #3: Add Entry Method
    // ============================================
    static void AddEntry()
    {
        Console.WriteLine("\n--- Write New Entry ---");

        // Step 1: Get the entry text from the user
        Console.Write("What's on your mind? ");
        string text = Console.ReadLine();

        // Check for empty input
        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine("❌ Entry cannot be empty!");
            return;
        }

        // TODO: Create a formatted entry string with date and text
        // Use DateTime.Now.ToString("yyyy-MM-dd HH:mm") to get the current date/time
        // Format it like: "[2025-01-15 14:30] Your text here"
        //
        // HINT:
        string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        string entry = $"[{timestamp}] {text}";


        entries.Add(entry);


        File.AppendAllText(JournalFile, entry + "\n");


        Console.WriteLine($"✅ Entry saved: \"{text}\"");
    }


    // ============================================
    // TODO #4: View All Entries
    // ============================================
    static void ViewAllEntries()
    {
        Console.WriteLine("\n--- All Journal Entries ---");

        // TODO: Check if the list is empty

        if (entries.Count == 0)
        {
            Console.WriteLine("No entries yet! Start writing to build your journal");
            return;
        }

        for (int i = 0; i < entries.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {entries[i]}");
        }
    }


    // ============================================
    // TODO #5: Search Entries
    // ============================================
    static void SearchEntries()
    {
        Console.WriteLine("\n--- Search Entries ---");

        Console.Write("Enter search term: ");
        string searchTerm = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            Console.WriteLine("❌ Search term cannot be empty!");
            return;
        }

        int found = 0;
        foreach (string entry in entries)
        {
            if (entry.ToLower().Contains(searchTerm.ToLower()))
            {
                Console.WriteLine($"  ✓ {entry}");
                found++;
            }
        }

        if (found == 0)
        {
            Console.WriteLine("No entries match your search.");
        }
        else
        {
            Console.WriteLine($"\nFound {found} matching entries.");
        }
    }


    static void SaveToFile()
    {
        Console.WriteLine("\n--- Save to File ---");
        try
        {
            File.WriteAllLines(JournalFile, entries);
            Console.WriteLine($"✅ Saved {entries.Count} entries to {JournalFile}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"❌ Error saving: {ex.Message}");
        }

        // YOUR CODE HERE:
    }


    // ============================================
    // BONUS: Export Entries to a New File
    // ============================================
    static void ExportEntries()
    {
        Console.WriteLine("\n--- Export Entries ---");
        Console.Write("Enter export filename (e.g., my-journal.txt): ");
        string exportPath = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(exportPath))
            {
                writer.WriteLine("=== My Journal Export ===");
                writer.WriteLine($"Exported on: {DateTime.Now}");
                writer.WriteLine($"Total entries: {entries.Count}");
                writer.WriteLine("─────────────────────────────");
                foreach (string entry in entries)
                {
                    writer.WriteLine(entry);
                }
            }

            Console.WriteLine($"✅ Exported to {exportPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Export failed: {ex.Message}");
        }

        // YOUR CODE HERE:
        Console.WriteLine("🚧 Bonus feature - implement if you have time!");
    }


    // ============================================
    // BONUS: Create Backup
    // ============================================
    static void CreateBackup()
    {
        Console.WriteLine("\n--- Create Backup ---");

        if (File.Exists(JournalFile))
        {
            File.Copy(JournalFile, BackupFile, true);
            Console.WriteLine($"✅ Backup created: {BackupFile}");
        }
        else
        {
            Console.WriteLine("❌ No journal file to back up!");
        }

        // YOUR CODE HERE:
        Console.WriteLine("🚧 Bonus feature - implement if you have time!");
    }
}


/*
 * ============================================
 * QUICK REFERENCE - Copy from here!
 * ============================================
 *
 * FILE WRITING:
 *   File.WriteAllText("file.txt", "content");      // Overwrite
 *   File.WriteAllLines("file.txt", stringArray);    // Overwrite
 *   File.AppendAllText("file.txt", "new content");  // Append
 *
 * FILE READING:
 *   string text = File.ReadAllText("file.txt");
 *   string[] lines = File.ReadAllLines("file.txt");
 *   bool exists = File.Exists("file.txt");
 *
 * FILE OPERATIONS:
 *   File.Copy("source.txt", "dest.txt", true);  // true = overwrite
 *   File.Delete("file.txt");
 *   File.Move("old.txt", "new.txt");
 *
 * STREAMWRITER:
 *   using (StreamWriter writer = new StreamWriter("file.txt"))
 *   {
 *       writer.WriteLine("text");
 *   }
 *   // To append: new StreamWriter("file.txt", true)
 *
 * STREAMREADER:
 *   using (StreamReader reader = new StreamReader("file.txt"))
 *   {
 *       string line;
 *       while ((line = reader.ReadLine()) != null)
 *       {
 *           Console.WriteLine(line);
 *       }
 *   }
 *
 * TRY-CATCH:
 *   try
 *   {
 *       // File operation
 *   }
 *   catch (IOException ex)
 *   {
 *       Console.WriteLine($"Error: {ex.Message}");
 *   }
 *
 * DATE/TIME:
 *   DateTime.Now.ToString("yyyy-MM-dd HH:mm")
 *   // Result: "2025-01-15 14:30"
 */