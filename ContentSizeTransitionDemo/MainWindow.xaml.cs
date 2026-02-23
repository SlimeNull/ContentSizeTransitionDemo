using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContentSizeTransitionDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int _clickIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToggleContent_Click(object sender, RoutedEventArgs e)
        {
            _clickIndex++;
            var height = _clickIndex % 2 == 0 ? 50 : 150;

            container.Content = new TextBlock()
            {
                MinHeight = height,
                Background = Brushes.AliceBlue,
                Text = $"Height: {height}",
            };
        }
    }
}