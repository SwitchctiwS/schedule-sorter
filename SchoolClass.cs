using System;

namespace ScheduleSorter
{
    /// <summary>
    /// Class for school.
    /// </summary>
    class SchoolClass
    {
        private string name;
        DateTime time;

        internal SchoolClass(string name)
        {
            this.name = name;
        }
        internal SchoolClass(string name, DateTime time)
        {
            this.name = name;
            this.time = time;
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
        internal DateTime Time
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
            }
        }
    }
}
