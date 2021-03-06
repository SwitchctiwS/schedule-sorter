﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using static System.Console;
using System.IO;

namespace ScheduleSorter {
    class Program {
        static string filePath = @"F:\Schedule.txt";

        static List<Assigned> assignmentList = new List<Assigned>();
        static List<Todo> todoList = new List<Todo>();
        static List<SchoolClass> sClassList = new List<SchoolClass>();

        enum ExitCode {Success = 1, Failure = 2};
        enum Type {None = -1, Assignment = 1, Class = 2, Todo = 3};

        static void Main(string[] args) {
            StartUp();

            while (true) {
                Clear();

                WriteLine("1. Create");
                WriteLine("2. Read");
                WriteLine("3. Update");
                WriteLine("4. Delete");
                WriteLine();
                WriteLine("9. Exit");

                int choice = Int32.Parse(ReadLine());
                Clear();

                // Put in try/catch loop
                switch (choice) {
                    case (1):
                        Create();
                        break;
                    case (2):
                        Read();
                        break;
                    case (3):
                        Update();
                        break;
                    case (4):
                        Delete();
                        break;
                    case (9):
                        Exit();
                        break;
                    default:
                        WriteLine("That was not a valid input");
                        break;
                }
            }
        }

        // Core methods
        private static void Create() {
            WriteLine("Please enter assignment's name:");
            string aName = ReadLine();

            if (aName.Length == 0) {
                return;
            }

            WriteLine($"\nPlease enter {aName} due date (dd/MM/yyyy h:mmtt):"); // Put in try/catch loop
            DateTime dueDate = DateTime.ParseExact(ReadLine(), "dd/MM/yyyy h:mmtt", CultureInfo.InvariantCulture);

            WriteLine($"\nPlease enter {aName} class:");
            SchoolClass sClass = new SchoolClass(ReadLine());

            Assigned assignment = new Assigned(aName, dueDate, sClass);
            assignmentList.Add(assignment);

            // The parameters for DateTime are always needed because that's the way I write dates
            WriteLine($"\n{assignment.Name} is due at {assignment.DueDate.ToString("dd/MM/yyyy hh:mmtt", CultureInfo.InvariantCulture)} for {sClass.Name}");

            WriteLine("\nPress any key to return to menu...");
            ReadKey();
        }

        private static void Delete() {
            WriteLine("Please enter the name of the completed assignment:");
            string deleteAssignment = ReadLine();

            if (deleteAssignment.Length == 0) {
                return;
            }

            WriteLine();

            if (assignmentList.Exists(assignment => assignment.Name == deleteAssignment)) {
                assignmentList.Remove(assignmentList.Find(assignment => assignment.Name == deleteAssignment));
                WriteLine($"Deleted {deleteAssignment}.");
            } else {
                WriteLine($"{deleteAssignment} does not exist.");
            }

            WriteLine();
            WriteLine("Press any key to return to menu...");
            ReadKey();
        }

        private static void Exit() {
            WriteLine("Saving changes...");

            /*
            string[] assignmentNames = new string[assignmentList.Count];
            for (int i = 0; i < assignmentList.Count; i++)
            {
                assignmentNames[i] = assignmentList[i].Name;
            }
            foreach(Assigned assignment in assignmentList)
            {
                File.WriteAllLines(filePath, assignmentNames);
            }
            */

            WriteLine("Done.");

            WriteLine();
            WriteLine("Press any key to exit...");
            ReadKey();

            Environment.Exit((int)ExitCode.Success);

            // To save everything, make a giant string[] with headers and names for Assignments, Todo, Classes, then WriteAllLines that giant string[]
            // This will eliminate the need to write a little bit at a time
        }

        private static void Read() {
            WriteLine("---Assignments---");
            foreach(Assigned assignment in assignmentList) {
                WriteLine($"{assignment.Name}, {assignment.DueDate}, {assignment.SClass.Name}, {assignment.Description}");
            }

            WriteLine();
            WriteLine("---Todo---");
            foreach (Todo todo in todoList) {
                WriteLine($"{todo.Name}, {todo.DueDate}, {todo.Description}");
            }

            WriteLine();
            WriteLine("---Classes---");
            foreach (SchoolClass sClass in sClassList) {
                WriteLine($"{sClass.Name}, {sClass.Time}");
            }

            WriteLine();
            WriteLine("Press any key to return to menu...");
            ReadKey();
        }

        private static void Update() {

        }

        // Side methods
        static void StartUp() {
            string[] allText = File.ReadAllLines(filePath); // Reads all text from file
            
            foreach (string text in allText) { // Formats text depending on how line starts
                if (text.StartsWith("class")) { // Please change this
                    CreateFromString(text.Remove(0, 8), Type.Class);
                }

                if (text.StartsWith("todo")) { // It's so bad
                    CreateFromString(text.Remove(0, 6), Type.Todo);
                }
            }
            
            foreach (string text in allText) { // This is last because Assigned needs a SchoolClass 
                if (text.StartsWith("assignment")) { // I hate looking at it
                    CreateFromString(text.Remove(0, 12), Type.Assignment);
                }
            }

            //assignmentList.Sort();
            //sClassList.Sort();
            //todoList.Sort();
        }

        static void CreateFromString(string text, Type type) {
            List<string[]> stringList = SplitAndList(text);
            AddFromString(stringList, type);
        } 

        static List<string[]> SplitAndList(string allText) { // Outputs a list of strings that can be edited further
            string[] halfSplitText = allText.Split(';');

            List<string[]> fullSplitList = new List<string[]>();

            for (int i = 0; i < halfSplitText.Length; i++) {
                fullSplitList.Add(halfSplitText[i].Split(','));
            }

            return fullSplitList;
        }

        static void AddFromString(List<string[]> stringList, Type type) {
            for (int i = 0; i < stringList.Count; i++) {
                string name = stringList[i][0];
                DateTime time = DateTime.ParseExact(stringList[i][1], "dd/MM/yyyy h:mmtt", CultureInfo.InvariantCulture);

                if (type == Type.Assignment) {
                    SchoolClass sClass = sClassList.Find(assignmentClass => assignmentClass.Name == stringList[i][2]);

                    if (stringList[i].Length > 3) {
                        string description = stringList[i][3];

                        assignmentList.Add(new Assigned(name, time, sClass, description));
                    } else {
                        assignmentList.Add(new Assigned(name, time, sClass));
                    }
                } else if (type == Type.Todo) {
                    if (stringList[i].Length > 2) {
                        string description = stringList[i][2];

                        todoList.Add(new Todo(name, time, description));
                    }
                    else { 
                        todoList.Add(new Todo(name, time));
                    }
                } else {
                    if (stringList[i].Length > 1) {
                        sClassList.Add(new SchoolClass(name, time));
                    } else {
                        sClassList.Add(new SchoolClass(name));
                    }
                }
            }
        }
    }
}
