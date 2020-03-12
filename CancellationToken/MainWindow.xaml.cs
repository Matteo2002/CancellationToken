using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace CancellationToken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        CancellationTokenSource ct1 = new CancellationTokenSource();

        CancellationTokenSource ct2 = new CancellationTokenSource();

        CancellationTokenSource ct3 = new CancellationTokenSource();

        private void Btn_start1_Click(object sender, RoutedEventArgs e)
        {
            if (ct1.Token.IsCancellationRequested)
            {
                ct1 = new CancellationTokenSource();
            }
                
            Task.Factory.StartNew(() => DoWork(100, lbl_Count1, 1000, ct1));
        }

        private void Btn_start2_Click(object sender, RoutedEventArgs e)
        {
            if (ct2.Token.IsCancellationRequested)
            {
                ct2 = new CancellationTokenSource();
            }
               
            int max = Convert.ToInt32(txt_Max2.Text);

            Task.Factory.StartNew(() => DoWork(max, lbl_Count2, 1000, ct2));
        }

        private void Btn_start3_Click(object sender, RoutedEventArgs e)
        {

            if (ct3.Token.IsCancellationRequested)
            {
                ct3 = new CancellationTokenSource();
            }
                
            int max = Convert.ToInt32(txt_Max3.Text);

            int delay = Convert.ToInt32(txt_Delay.Text);

            Task.Factory.StartNew(() => DoWork(max, lbl_Count3, delay, ct3));
        }

        private void DoWork(int max, Label label, int delay, CancellationTokenSource ct)
        {
            for (int i = 0; i <= max; i++)
            {
                if (ct.Token.IsCancellationRequested)
                {
                    break;
                }

                Dispatcher.Invoke(() => { Update(label, i); });

                Thread.Sleep(delay);
            }
        }

        private static void Update(Label label, int i)
        {
            label.Content = i;
        }

        private void Btn_stop1_Click(object sender, RoutedEventArgs e)
        {
            ct1.Cancel();
        }

        private void Btn_stop2_Click(object sender, RoutedEventArgs e)
        {
            ct2.Cancel();
        }

        private void Btn_stop3_Click(object sender, RoutedEventArgs e)
        {
            ct3.Cancel();
        }

        private void Btn_stop_Click(object sender, RoutedEventArgs e)
        {
            ct1.Cancel();
            ct2.Cancel();
            ct3.Cancel();
        }
    }
}
