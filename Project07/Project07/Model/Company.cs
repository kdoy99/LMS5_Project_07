using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Project07.Model
{
    public partial class Company
    {
        [PrimaryKey, AutoIncrement]
        public int CompanyNumber { get; set; } // 회사정보 넘버
        public string CompanyName { get; set; } // 회사명
        public int CompanyYears { get; set; } // 설립년도
        public string CompanyForms { get; set; } // 기업형태
        public string CompanyPersons { get; set; } // 사원수
        public string CompanySales { get; set; } // 매출액
        public string CompanyHomepage { get; set; } // 홈페이지
        public string CompanyHead { get; set; } // 대표자        
    }
}
