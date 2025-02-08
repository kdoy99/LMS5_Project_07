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
    /// newAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class newAccount : Window
    {
        public newAccount()
        {
            InitializeComponent();
        }

        private void createAccount_Click(object sender, RoutedEventArgs e)
        {
            // AddNewContactWindow 윈도우의 입력 TextBox 컨트롤에서 값을 가져와
            // Contact 데이터 모델 객체의 필드에 각각 값을 할당한다.
            Account account = new Account()
            {
                // 유저번호 = [PrimaryKey, AutoIncrement] 이기 때문에
                // 값을 지정하지 않아도, PK는 자동으로 AutoIncrement 된다.
                // 그외 Name, Email, Phone 등 프로퍼티에 값을 할당한다.       
                ID = idTextBox.Text,
                Password = pwPwBox.Password,
                Name = nameTextBox.Text,
                Age = ageTextBox.Text,
                Sex = sexTextBox.Text,
                Contact = contactTextBox.Text,
                Email = emailTextBox.Text
            };

            // 지정된 경로에 생성할 DB 연결 객체 생성
            SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            // Contact 클래스 정의를 기반으로 SQLite DB Table 생성 (테이블이 없을 경우, 있으면 X)
            connection.CreateTable<Account>();

            // UI 컨트롤에 입력된 데이터를 Account 객체 형태로, 생성한 SQLite DB Table에 삽입
            connection.Insert(account);

            // DB 연결을 닫음 (임시 구현)
            connection.Close();

            Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
