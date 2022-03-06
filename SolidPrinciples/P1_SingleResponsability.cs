using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.SolidPrinciples.SingleResponsability
{
    class Journal
    {
        private readonly Dictionary<int, string> journalEntries = new Dictionary<int, string>();

        private int numberOfEntries = 0;

        public int AddJournalEntry(string journalEntry)
        {
            journalEntries.Add(++numberOfEntries, journalEntry);
            return numberOfEntries;
        }

        public void RemoveJournalEntry(int entryNumberToBeRemoved)
        {
            
            for(int i = entryNumberToBeRemoved; i < numberOfEntries; i++)
            {
                journalEntries[i] = journalEntries[i + 1];
            }

            journalEntries.Remove(numberOfEntries);
            numberOfEntries--;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach(KeyValuePair<int,string> kp in journalEntries)
            {
                sb.Append($"{kp.Key}: {kp.Value}" + "\n");
            }

            return sb.ToString().Substring(0, sb.Length - 1);
        }
    }

    public class Persistence
    {
        public static void SaveToFile(Object journal, string fileName, bool overWrite = false)
        {
            if( overWrite || !File.Exists(fileName))
            {
                File.WriteAllText(fileName, journal.ToString());
            }
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            Journal j = new Journal();
            j.AddJournalEntry("a1");
            j.AddJournalEntry("a2");
            j.AddJournalEntry("a3");
            j.AddJournalEntry("a4");
            j.RemoveJournalEntry(2);
            Console.WriteLine(j.ToString());
            Console.ReadLine();
        }

    }
    
}
