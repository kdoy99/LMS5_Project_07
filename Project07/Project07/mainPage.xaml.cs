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
using System.Windows.Media.Animation;
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
        List<Notice> notices;
        public mainPage()
        {
            InitializeComponent();
            FilterComboBox.SelectedIndex = 0;
            ReadNoticeDB();
        }        
        private void ReadNoticeDB()
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath)) // databasePath : Notice 정보 들어있는 DB
            {
                connection.CreateTable<Notice>();
                notices = ((connection.Table<Notice>().ToList()).OrderBy(Notice => Notice.CompanyInfo)).ToList();
            }

            if (notices != null)
            {
                RecommendList.ItemsSource = notices;
            }            
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

        public string filter;
        private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox? searchTextBox = sender as TextBox;            

            if (filter == "System.Windows.Controls.ComboBoxItem: 회사명")
            {
                var searchResultList = notices.Where(Notice => Notice.CompanyInfo.ToLower().Contains(searchTextBox.Text.ToLower()));
                RecommendList.ItemsSource = searchResultList;
            }
            else if (filter == "System.Windows.Controls.ComboBoxItem: 담당 업무")
            {
                var searchResultList = notices.Where(Notice => Notice.Task.ToLower().Contains(searchTextBox.Text.ToLower()));
                RecommendList.ItemsSource = searchResultList;
            }
            else if (filter == "System.Windows.Controls.ComboBoxItem: 포지션")
            {
                var searchResultList = notices.Where(Notice => Notice.Position.ToLower().Contains(searchTextBox.Text.ToLower()));
                RecommendList.ItemsSource = searchResultList;
            }
            else if (filter == "System.Windows.Controls.ComboBoxItem: 자격요건")
            {
                var searchResultList = notices.Where(Notice => Notice.Qualification.ToLower().Contains(searchTextBox.Text.ToLower()));
                RecommendList.ItemsSource = searchResultList;
            }
            else if (filter == "System.Windows.Controls.ComboBoxItem: 근무지")
            {
                var searchResultList = notices.Where(Notice => Notice.Place.ToLower().Contains(searchTextBox.Text.ToLower()));
                RecommendList.ItemsSource = searchResultList;
            }
            else if (filter == "System.Windows.Controls.ComboBoxItem: 복지")
            {
                var searchResultList = notices.Where(Notice => Notice.Welfare.ToLower().Contains(searchTextBox.Text.ToLower()));
                RecommendList.ItemsSource = searchResultList;
            }
            else if (filter == "System.Windows.Controls.ComboBoxItem: 우대조건")
            {
                var searchResultList = notices.Where(Notice => Notice.Preference.ToLower().Contains(searchTextBox.Text.ToLower()));
                RecommendList.ItemsSource = searchResultList;
            }
        }
        private void FilterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox? comboBox = sender as ComboBox;
            filter = comboBox.SelectedItem.ToString();            
        }
    }
}
