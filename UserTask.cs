using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList
{
    public class Task
    {
        public bool ActiveTask { get; set; } // Declaring properties of my class.
        public string? TaskTitle { get; set; }
        public string? TaskDescription { get; set; }
        public int TaskNumber { get; set; }

        public Task(bool activeTask, string taskTitle, string taskDescription, int taskNumber) // Constructor, names the variables
        {
            this.ActiveTask = activeTask;
            this.TaskTitle = taskTitle;
            this.TaskDescription = taskDescription;
            this.TaskNumber = taskNumber;
        }

        public static Task CreateTask(int currentCount)
        {
            Console.WriteLine("--- NEW TASK--- ");
            bool activeTask = true;

            Console.WriteLine("Task Name: ");
            string? taskTitle = Console.ReadLine();

            Console.WriteLine("Task Description: ");
            string? taskDescription = Console.ReadLine();

            int taskNumber = currentCount + 1; // This SHOULD assign each new task a numerical value corresponding with its place on the list. Sequentially ascending as the list grows. and descending as items are removed.

            Task newtask = new Task(activeTask,
                                 taskTitle,
                                 taskDescription,
                                 taskNumber);
            return newtask;
        }

    }
}
