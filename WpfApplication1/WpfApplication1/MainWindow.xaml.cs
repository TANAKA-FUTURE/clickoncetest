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
    }
}
