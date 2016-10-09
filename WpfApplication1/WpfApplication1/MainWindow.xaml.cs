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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Deployment.Application;
using System.Data.SQLite;


namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            label1.Content = global::WpfApplication1.Resources.Resource1.String3;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!ApplicationDeployment.IsNetworkDeployed)
            {
                MessageBox.Show("ClickOnce を使用していません");
                return;
            }

            // 強制的にアップグレード
            ApplicationDeployment deploy;
            deploy = ApplicationDeployment.CurrentDeployment;
            deploy.Update();
            MessageBox.Show("更新終了。再起動します");
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // ClickOnce でインストールされていた場合、バージョン表示
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                // 現在のバージョンをタイトルバーに表示する
                ApplicationDeployment deploy = ApplicationDeployment.CurrentDeployment;
                this.Title = deploy.CurrentVersion.ToString();

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!ApplicationDeployment.IsNetworkDeployed)
            {
                MessageBox.Show("ClickOnce を使用していません");
                return;
            }

            string db_file = ApplicationDeployment.CurrentDeployment.DataDirectory+@"\test.mdb";

            MessageBox.Show(db_file);
            using (var connection = new SQLiteConnection("DATA Source="+db_file))
            {
                connection.Open();
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    //command.CommandText = "create table hoge(id INTEGER PRIMARY KEY AUTOINCREMENT, field1 TEXT, field2 REAL, field3 BLOB)";
                    //command.ExecuteNonQuery();
                }
                connection.Close();
            }

            MessageBox.Show("DB接続　正常終了");
        }
    }
}
