using System.Windows;
using System.Windows.Controls;

namespace PierreCyberSecurityChatbotGUI
{
    public partial class InputDialog : Window
    {
        public string ResponseText { get; set; }

        public InputDialog(string prompt)
        {
            InitializeComponent();
            Title = "Pierre - Cybersecurity Bot";
            promptText.Text = prompt;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ResponseText = inputBox.Text;
            DialogResult = true;
        }

        private void InitializeComponent()
        {
            // XAML would go here, but we'll do it in code for simplicity
            Width = 300;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;

            var stack = new StackPanel();

            var prompt = new TextBlock { Margin = new Thickness(10) };
            promptText = prompt;

            inputBox = new TextBox { Margin = new Thickness(10) };

            var button = new Button
            {
                Content = "OK",
                Width = 80,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            button.Click += OkButton_Click;

            stack.Children.Add(prompt);
            stack.Children.Add(inputBox);
            stack.Children.Add(button);

            Content = stack;
        }

        private TextBlock promptText;
        private TextBox inputBox;
    }
}