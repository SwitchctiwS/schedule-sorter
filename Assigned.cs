using System;

namespace ScheduleSorter
{
    /// <summary>
    /// Assignment for a specific class.
    /// </summary>
    class Assigned
    {
        private string name;
        private string description;
        private DateTime dueDate;
        private SchoolClass sClass;

        internal Assigned(string name, DateTime dueDate, SchoolClass sClass)
        {
            this.name = name;
            this.dueDate = dueDate;
            this.sClass = sClass;
        }
        internal Assigned(string name, string description, DateTime dueDate, SchoolClass sClass)
        {
            this.name = name;
            this.description = description;
            this.dueDate = dueDate;
            this.sClass = sClass;
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
                return this.name;
            }
            set
            {
                this.name = value;
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
        internal SchoolClass SClass
        {
            get
            {
                return this.sClass;
            }
            set
            {
                this.sClass = value;
            }
        }
    }
}