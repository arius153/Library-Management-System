using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement
{
    public class User
    {
        public string name { get; }
        public List<int> takenBookIds { get; set; }
        public User(string name)
        {
            this.name = name;
            takenBookIds = new List<int>();
        }

        public int takeBook(int id)
        {
            if (takenBookIds.Count >= 3)
            {
                //User has to many books already
                return -1;
            }
            takenBookIds.Add(id);
            //Book is taken, all guci
            return 0;
        }
        
        public int returnBook(int id)
        {
            if (!takenBookIds.Contains(id))
            {
                //User does not have this book
                return -1;
            }
            takenBookIds.Remove(id);
            //Ale is gucci
            return 0;
        }
    }
}
