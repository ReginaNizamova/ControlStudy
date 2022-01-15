using System.Linq;
using System.Windows.Controls;

namespace Authorization.Pages
{
    public partial class AdminJournalPage : Page
    {
        public AdminJournalPage()
        {
            InitializeComponent();

            dataGridAdmin.ItemsSource = ControlStudyEntities.GetContext().Sessions.ToList();
        }
    }
}
