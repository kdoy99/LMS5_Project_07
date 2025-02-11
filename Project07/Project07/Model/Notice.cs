using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Project07.Model
{
    public partial class Notice
    {
        [PrimaryKey, AutoIncrement]
        public int NoticeNumber { get; set; } // 고용정보 넘버
        public string CompanyInfo { get; set; } // 회사명
        public string Task { get; set; } // 담당 업무
        public string Position { get; set; } // 포지션
        public string Qualification { get; set; } // 자격 요건
        public string Place { get; set; } // 근무지
        public string Welfare { get; set; } // 복지
        public string Preference { get; set; } // 우대 조건        
    }
}
