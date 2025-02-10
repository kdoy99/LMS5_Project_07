using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public bool DupCheck_ID = false;
        public bool DupCheck_Contact = false;
        public string male_female = "";

        private void createAccount_Click(object sender, RoutedEventArgs e)
        {
            if (pwPwBox.Password.Length == 0 || nameTextBox.Text.Length == 0 || ageTextBox.Text.Length == 0 
                || male_female == "" || emailTextBox.Text.Length == 0)
            {
                MessageBox.Show("입력되지 않은 칸이 있습니다!");
                return;
            }

            if (ageTextBox.Text.Length != 6)
            {
                MessageBox.Show("생년월일을 제대로 입력해줘");
                return;
            }

            if (DupCheck_ID == true && DupCheck_Contact == true)
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
                    Sex = male_female,
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
                MessageBox.Show($"회원가입 완료! ID : {idTextBox.Text}");
                connection.Close();

                Close();
            }
            else
            {
                MessageBox.Show("중복 검사를 통과하고와라.");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }               
        private void Check_Num(object sender, TextCompositionEventArgs e)
        {
            // PreviewTextInput 사용, Regex를 이용해 숫자만 입력되게
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            // 최대 입력 길이 6
            ageTextBox.MaxLength = 6;
        }
        private void Check_ContactNum(object sender, TextCompositionEventArgs e)
        {
            // PreviewTextInput 사용, Regex를 이용해 숫자만 입력되게
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            // 최대 입력 길이 11
            contactTextBox.MaxLength = 11;
        }

        private void idDupCheck_Click(object sender, RoutedEventArgs e)
        {
            List<Account> idCheck;            
            
            // DB 연결
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Account>();
                idCheck = connection.Table<Account>().ToList();
            }
            // 반복문으로 중복 검사
            for (int i = 0; i < idCheck.Count; i++)
            {
                if (idCheck[i].ID == idTextBox.Text)
                {
                    MessageBox.Show("이미 존재하는 아이디입니다!");
                    DupCheck_ID = false;
                    return;
                }
            }
            // 중복 검사 통과 기록 남김
            if (idTextBox.Text.Length != 0)
            {
                MessageBox.Show("사용할 수 있는 아이디입니다!");
                DupCheck_ID = true;
            }
            else
            {
                MessageBox.Show("뭐라도 입력해주세요.");
                DupCheck_ID = false;
            }
            

        }

        private void contactCheck_Click(object sender, RoutedEventArgs e)
        {
            List<Account> contactCheck;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Account>();
                contactCheck = connection.Table<Account>().ToList();
            }
            // 반복문으로 중복 검사
            for (int i = 0; i < contactCheck.Count; i++)
            {
                if (contactCheck[i].Contact == contactTextBox.Text)
                {
                    MessageBox.Show($"이미 사용중인 번호입니다!");
                    DupCheck_Contact = false;
                    return;
                }
            }
            // 중복 검사 통과 기록 남김
            if (contactTextBox.Text.Length == 11)
            {
                MessageBox.Show("사용할 수 있는 번호입니다!");
                DupCheck_Contact = true;
            }
            else
            {
                MessageBox.Show("제대로 된 번호를 입력해!");
                DupCheck_Contact = false;
            }
        }

        private void maleButton_Checked(object sender, RoutedEventArgs e)
        {
            male_female = "male";
        }

        private void femaleButton_Checked(object sender, RoutedEventArgs e)
        {
            male_female = "female";
        }

        // 중복 검사를 통과해놓고, 아이디 번호 바꾸는 거 방지
        private void idTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DupCheck_ID = false;
        }

        private void contactTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DupCheck_Contact = false;
        }
    }
}
