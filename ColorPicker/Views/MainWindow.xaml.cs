using System.Text.RegularExpressions;
using System.Windows;

namespace ColorPicker;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void PreviewTextInputHandler(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        Regex regex = new("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);

        if (int.TryParse(e.Text, out int result))
        {
            if (result < 0)
            {
                //e.Text = 0;
            }
            else if (result > 255)
            {
                //e.Text = 255;
            }
        }
        // e.Handled = ;
    }
}
