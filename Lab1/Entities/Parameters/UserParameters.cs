using System;

namespace Lab1.Entities.Parameters
{
    public class UserParameters : QueryStringParameters
    {
        public String FirstNameSearch { get; set; } = "";

        public String LastNameSearch { get; set; } = "";
    }
}