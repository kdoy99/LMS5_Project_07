using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Project07.Model
{
    public class Account
    {
        [PrimaryKey, AutoIncrement]
        public int UserNumber { get; set; }
        public string ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Sex { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"{ID} - {Password}";
        }
    }
}
