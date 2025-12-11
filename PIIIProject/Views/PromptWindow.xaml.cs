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
using System.Windows.Shapes;

namespace PIIIProject.Views
{
    /// <summary>
    /// Interaction logic for PromptWindow.xaml
    /// </summary>
    public partial class PromptWindow : Window
    {
        public string _selectedButton;

        public PromptWindow(string button1Content, string button2Content, string errorMSG, string title)
        {
            InitializeComponent();

            if (button2Content != string.Empty)
                backBtn2.Visibility = Visibility.Visible;

            backBtn.Content = button1Content;
            backBtn2.Content = button2Content;

            pageTitle.Text = title;
            pageContent.Text = errorMSG;
        }

        private void returnBtn_Clicked(object sender, RoutedEventArgs e)
        {
            this.Close();

            Button b = (Button)sender;

            _selectedButton = b.Content.ToString();
        }
    }
}
