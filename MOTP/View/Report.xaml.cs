using System.Windows;
using System.Windows.Documents;

namespace MOTP.View
{
    public partial class Report : Window
    {
        private string richreport = "";
        private Home _home;
        public Report(Home home, string richreportHome)
        {
            InitializeComponent();
            _home = home;
            richreport = richreportHome;
        }

        private void BTN_Copy_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(new TextRange(RTB_Report.Document.ContentStart, RTB_Report.Document.ContentEnd).Text.Trim());

            Close();
        }

        private void RTB_Report_Loaded(object sender, RoutedEventArgs e)
        {
            FlowDocument document = new FlowDocument();
            Paragraph paragraph = new Paragraph();
            paragraph.Inlines.Add(new Bold(new Run(richreport)));
            document.Blocks.Add(paragraph);
            RTB_Report.Document = document;

            Clipboard.SetText(new TextRange(RTB_Report.Document.ContentStart, RTB_Report.Document.ContentEnd).Text.Trim());
        }
    }
}
