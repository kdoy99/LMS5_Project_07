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

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath)) // databasePath : Notice 정보 들어있는 DB
            {
                connection.CreateTable<Notice>();
                notices = connection.Table<Notice>().ToList();
            }

            RecommendList.ItemsSource = notices;
        }              

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO : 회원정보 말소까지 추가
            MainWindow main = new MainWindow();
            Close();
            main.ShowDialog();
        }

        public void RecommendList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // 마우스 이벤트가 발생한 곳에 OriginalSource를 사용
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            while ((dep != null) && !(dep is ListViewItem))
            {
                // 더블 클릭한 대상중 ListViewItem이 있을 때까지
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            
        
            // ListView에서 선택된 항목을 가져옴
            Notice selectedNotice = RecommendList.SelectedItem as Notice;
            

            DetailInfo detail = new DetailInfo(selectedNotice);
            Close();
            detail.ShowDialog();            
        }       
    }
}
