using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project07.Model;
using SQLite;

namespace Project07
{
    /// <summary>
    /// CompanyInfo.xaml에 대한 상호 작용 논리
    /// </summary>    
    public partial class CompanyInfo : Window
    {
        public Notice CompanyNumber;
        public CompanyInfo(Notice detail)
        {
            InitializeComponent();
            CompanyNumber = detail;
            ReadCompanyDB();
        }
        private void ReadCompanyDB()
        {
            int number = CompanyNumber.NoticeNumber;
            List<Company> company;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath)) // databasePath : Notice 정보 들어있는 DB
            {
                connection.CreateTable<Company>();
                company = connection.Table<Company>().ToList();
            }

            DetailView.Children.Clear();

            DetailView.Children.Add(CreateTextBlockforCompany($"회사명: {company[number].CompanyName}", 16, true));
            DetailView.Children.Add(CreateTextBlockforCompany($"설립년도: {company[number].CompanyYears} 년", 12, true));
            DetailView.Children.Add(CreateTextBlockforCompany($"기업 형태: {company[number].CompanyForms}", 12, true));
            DetailView.Children.Add(CreateTextBlockforCompany($"사원수: {company[number].CompanyPersons} 명", 12, true));
            DetailView.Children.Add(CreateTextBlockforCompany($"매출액: {company[number].CompanySales} 억", 12, true));
            DetailView.Children.Add(CreateTextBlockforCompany($"홈페이지: {company[number].CompanyHomepage}", 12, true));
            DetailView.Children.Add(CreateTextBlockforCompany($"대표자명: {company[number].CompanyHead}", 12, true));
        }

        private TextBlock CreateTextBlockforCompany(string text, int fontSize, bool isBold = false)
        {

            TextBlock textBlock = new TextBlock
            {
                Text = text,
                FontSize = fontSize,
                Margin = new Thickness(0, 5, 0, 5),
                TextWrapping = TextWrapping.Wrap
            };

            if (isBold)
            {
                textBlock.FontWeight = FontWeights.Bold;
            }

            return textBlock;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DetailInfo detailInfo = new DetailInfo(CompanyNumber);
            Close();
            detailInfo.ShowDialog();
        }
    }
}
