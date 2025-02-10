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
    /// mainPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class mainPage : Window
    {
        public mainPage()
        {
            InitializeComponent();
            ReadNoticeDB();
        }        
        private void ReadNoticeDB()
        {
            List<Notice> notices;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath)) // databasePath2 : Notice 정보 들어있는 DB
            {
                connection.CreateTable<Notice>();
                notices = connection.Table<Notice>().ToList();
            }

            recommendList.ItemsSource = notices;
        }
    }
}
