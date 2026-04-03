using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MOTP
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();

            if(true)
            {
                new MainWindow().Show();
                this.Close();
            }
        }

        private string Hash()
        {
            try
            {
                string str = $"{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}" + ")(w@)?f:RJM#pyFoRh0?n.5oYUsq7LN}#M*1YL*i";

                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(new UTF8Encoding().GetBytes(str));
                string encoded = BitConverter.ToString(hash);
                string[] encodedspl = encoded.Split('-');
                string result = $"{encodedspl[2]}{encodedspl[4]}{encodedspl[encodedspl.Length - 5]}{encodedspl[encodedspl.Length - 3]}";

                return result;
            }
            catch { return null; }
        }

        private void TB_Pas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (TB_Pas.Text == Hash())
                {
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    TB_Pas.Text = "";
                    System.Media.SystemSounds.Hand.Play();
                    //Close();
                }
            }
        }
    }
}
