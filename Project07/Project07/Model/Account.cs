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
        public string ID { get; set; } // 아이디
        public string Password { get; set; } // 비밀번호
        public string Name { get; set; } // 이름
        public string Age { get; set; } // 생년월일
        public string Sex { get; set; } // 성별
        public string Contact { get; set; } // 연락처
        public string Email { get; set; } // 이메일

        public override string ToString()
        {
            return $"{ID} - {Password}";
        }
    }
}
