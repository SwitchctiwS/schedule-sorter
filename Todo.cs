using System;
using System.Globalization;

namespace ScheduleSorter
{
    /// <summary>
    /// Thing that needs to be done.
    /// </summary>
    class Todo
    {
        private string name;
        private string description = string.Empty;
        private DateTime dueDate;

        internal Todo()
        {
            name = "empty";
            description = "empty";
            dueDate = new DateTime();
        }
        internal Todo(string name)
        {
            this.name = name;
        }
        internal Todo(string name, DateTime dueDate)
        {
            this.dueDate = dueDate;
            this.name = name;
        }
        internal Todo(string name, DateTime dueDate, string description)
        {
            this.name = name;
            this.description = description;
            this.dueDate = dueDate;
        }

        internal string Name
        {
            get
            {
                return this.name;
            }    
            set
            {
                this.name = value;
            }
        }
        internal string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        internal DateTime DueDate
        {
            get
            {
                return this.dueDate;
            }
            set
            {
                this.dueDate = value;
            }
        }
    }
}
