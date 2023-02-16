using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ChallengeApp
{
    public class NamedObject
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public NamedObject(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
