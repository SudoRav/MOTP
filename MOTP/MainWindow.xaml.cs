using Google.Apis.Auth.OAuth2;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MOTP
{

    public partial class MainWindow : Window
    {
        static readonly string googleCientId = "974181248114-2u0idur9gmlnv6jumt2lfo1b8e2kikmf.apps.googleusercontent.com";
        static readonly string googleLientSecret = "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";

        private static UserCredential Login(string googleClientId, string googleClientSecret)
        {
            try
            {
                ClientSecrets secrets = new ClientSecrets()
                {
                    ClientId = googleClientId,
                    ClientSecret = googleClientSecret
                };

                return GoogleWebAuthorizationBroker.AuthorizeAsync(secrets,
                    new[] { "https://www.googleapis.com/auth/drive.readonly" },
                    "user",
                    CancellationToken.None).Result;
            }
            catch { Environment.Exit(0); return null; }
        }

        public MainWindow()
        {
            InitializeComponent();

            if (Stat.Settings.timeEnd == DateTime.MinValue)
                Stat.Settings.timeEnd = DateTime.Now.AddDays(2);
        }

        public void ActiveCheck(DateTime now)
        {
            if (now <= Stat.Settings.timeEnd)
                return;

            MessageBox.Show("Время активно сеанса превышено, приложение завершит свою работу немедленно.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Stop);
            Environment.Exit(0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            if (MessageBox.Show("Закрыть текущее окно?\nЭто приведёт к безвозвратной потере внесённых данных!", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                e.Cancel = false;

                Environment.Exit(0);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //new Home().FoneAutoSave();
        }

        public bool SetTBCol(TextBox TB, bool result, Brush brushError = null, Brush selError = null, Brush bruhStandart = null, Brush selStandart = null)
        {
            if (brushError == null)
                brushError = Brushes.DarkRed;
            if (selError == null)
                selError = Brushes.Red;

            if (bruhStandart == null)
                bruhStandart = (Brush)new BrushConverter().ConvertFrom("#FFABADB3");
            if (selError == null)
                selStandart = (Brush)new BrushConverter().ConvertFrom("#FF0078D7");


            if (result)
            {
                TB.BorderBrush = brushError;
                TB.SelectionBrush = (Brush)new BrushConverter().ConvertFrom("#FFD70000");

                return true;
            }
            else
            {
                TB.BorderBrush = bruhStandart;
                TB.SelectionBrush = selStandart;

                return false;
            }
        }
    }
}
