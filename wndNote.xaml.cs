using System.Windows;

namespace Notes
{
    /// <summary>
    /// Логика взаимодействия для wndNote.xaml
    /// </summary>
    public partial class wndNote : Window
    {
        public Note Note { get; set; }
        public wndNote(Note note)
        {
            InitializeComponent();

            Note = note;
            DataContext = Note;
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
