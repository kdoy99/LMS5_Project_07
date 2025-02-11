using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Project07.Model
{
    public partial class Detail
    {
        [PrimaryKey, AutoIncrement]
        public int DetailNumber { get; set; } // 고용정보 넘버
        public string CompanyName { get; set; } // 회사명
        public string detailTask { get; set; } // 담당 업무
        public string detailPosition { get; set; } // 포지션
        public string detailQualification { get; set; } // 자격 요건
        public string detailPlace { get; set; } // 근무지
        public string detailWelfare { get; set; } // 복지
        public string detailPreference { get; set; } // 우대 조건        
    }
}
