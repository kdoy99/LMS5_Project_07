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
using SQLite;
using Project07.Model;

namespace Project07
{    
    public partial class DetailInfo : Window
    {
        public Notice detailNotice;
        public DetailInfo(Notice notice)
        {
            InitializeComponent();
            detailNotice = notice;
            ReadDetailDB();
        }

        private void ReadDetailDB()
        {
            int number = detailNotice.NoticeNumber;
            List<Detail> details;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath)) // databasePath : Notice 정보 들어있는 DB
            {
                connection.CreateTable<Detail>();
                details = connection.Table<Detail>().ToList();
            }

            DetailView.Children.Clear();

            DetailView.Children.Add(CreateTextBlock($"회사명: {details[number].CompanyName}", 16, true));
            DetailView.Children.Add(CreateTextBlock($"담당 업무: {details[number].detailTask}", 12, true));
            DetailView.Children.Add(CreateTextBlock($"포지션: {details[number].detailPosition}", 12, true));
            DetailView.Children.Add(CreateTextBlock($"자격 요건: {details[number].detailQualification}", 12, true));
            DetailView.Children.Add(CreateTextBlock($"근무지: {details[number].detailPlace}", 12, true));
            DetailView.Children.Add(CreateTextBlock($"복지: {details[number].detailWelfare}", 12, true));
            DetailView.Children.Add(CreateTextBlock($"우대 조건: {details[number].detailPreference}", 12, true));
        }

        private TextBlock CreateTextBlock(string text, int fontSize, bool isBold = false)
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
            mainPage mainPage = new mainPage();
            Close();
            mainPage.ShowDialog();
        }
    }
}
