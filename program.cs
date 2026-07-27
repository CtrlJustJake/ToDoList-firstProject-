using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            bool IsRunning = true; // While Loop run condition

            List<Task> ToDoList = new List<Task>(); // Generate the empty list

            while (IsRunning == true) // Beginning of While Loop, MAIN APPLICATION
            {
                Console.WriteLine("--- To Do List ---");
                if (ToDoList.Count == 0)
                {
                    Console.WriteLine("Your To Do list is empty\nWould you like to create a new task?\n Select Y/N: ");
                    string? userResponse = Console.ReadLine().Trim().ToUpper();
                    if (userResponse == "Y")
                    {
                        Task newtask = Task.CreateTask(ToDoList.Count); // Call method from UserTask.cs
                        ToDoList.Add(newtask); //Adds return from Task.CreateTask to list ToDoList
                    }
                }
                else if (ToDoList.Count > 0)
                {
                    Console.Clear(); // Refresh the screen so old prompts and inputs are no longer visible.

                    foreach (var task in ToDoList)
                    {
                        Console.WriteLine($"{task.TaskNumber}: {task.TaskTitle}: {task.TaskDescription}"); // prints out each task title AND the numerical assigned to each task.
                    }

                    Console.WriteLine("\nWhat would you like to do?\n1. Create a new task \n2. Modify a task\n3. Delete a task\n4. Close To Do List ");
                    string? userResponse = Console.ReadLine().Trim().ToUpper();

                    if (userResponse == "1")
                    {
                        Task newtask = Task.CreateTask(ToDoList.Count);
                        ToDoList.Add(newtask);
                    }
                    else if (userResponse == "2") // We can now select a task by matching a numerical input with that 
                    {
                        Console.WriteLine("\nWhich task would you like to modify? \n ");
                        foreach (var task in ToDoList)
                        {
                            Console.WriteLine($"{task.TaskNumber}: {task.TaskTitle}\n");
                        }

                        userResponse = Console.ReadLine();

                        int.TryParse(userResponse, out int targetNumber);

                        if (ToDoList.Any(t => t.TaskNumber == targetNumber)) // issue, first task is modified accurately but all other tasks are overwriting task one properties.
                        {
                            var selectedTask = ToDoList.FirstOrDefault(t => t.TaskNumber == targetNumber);

                            Console.WriteLine($"{selectedTask.TaskTitle}: {selectedTask.TaskDescription}"); // Fixed, needed to add a 'Dot Operator' to communicate the member being printed.

                            Console.WriteLine("\nWhat would you like to change?\n1. Title\n2. Description\n ");

                            Console.WriteLine($"selected task = {(selectedTask.TaskTitle)}\n"); // Let's test what this chunk is storing for targetNumber and selectedTask against my own inputs.
                            userResponse = Console.ReadLine();

                            int.TryParse(userResponse, out int selectionNumber);

                            var modifyingTask = ToDoList.FirstOrDefault(t => t.TaskNumber == selectionNumber);

                            if (selectionNumber == 1) // this and following if statement appear to be functioning as intended.
                            {
                                Console.WriteLine("\nInput new task title: ");

                                string newTitle = Console.ReadLine();

                                selectedTask.TaskTitle = newTitle;

                                Console.WriteLine("Title updated!");

                            }
                            if (selectionNumber == 2)
                            {
                                Console.WriteLine("\nInput new task description: ");

                                string newDescription = Console.ReadLine();

                                selectedTask.TaskDescription = newDescription;
                            }
                        }
                        else // This should catch any user input that does not match with a targetNumber or SelectedTask value.
                        {
                            Console.WriteLine("Invalid Command");
                            Console.ReadLine();

                        }
                    }
                    else if (userResponse == "3") // Build out deleting tasks from list
                    {
                        Console.WriteLine("\nWhich task would you like to delete? \n ");
                        foreach (var task in ToDoList)
                        {
                            Console.WriteLine($"{task.TaskNumber}: {task.TaskTitle}\n");
                        }

                        userResponse = Console.ReadLine();
                        int.TryParse(userResponse, out int targetNumber); // Parse user input string into an int named "targetNumber"

                        if (ToDoList.Any(t => t.TaskNumber == targetNumber)) // Need to include instructions to modify following tasks to adjust TaskNumber down by 1.
                        {
                            var selectedTask = ToDoList.FirstOrDefault(t => t.TaskNumber == targetNumber);

                            Console.WriteLine($"Are you sure you want to delete {selectedTask.TaskTitle} from your list?\n(Y/N)");

                            userResponse = Console.ReadLine().Trim().ToUpper();

                            if (userResponse == "Y")
                            {
                                Console.Clear();

                                ToDoList.Remove(selectedTask);

                                var tasksToShift = ToDoList.Where(t => t.TaskNumber > targetNumber);
                                foreach (var task in tasksToShift) { task.TaskNumber--; } // Previous method was creating a reference to a potenntially non-existent object in the event that
                            }                                                            // you attempted to delete the only item remaining on the list... We switched to this.

                            else if (userResponse == "N") { continue; }

                            else { Console.WriteLine("Invalid Input. Please try again"); }
                        }
                    }

                    else if (userResponse == "4")
                    {
                        IsRunning = false;
                    }

                    else
                    {
                        Console.WriteLine("Invalid Input \nPlease Try again ");
                    }
                }
            }
        }
    }
}
