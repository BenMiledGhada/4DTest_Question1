using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace _4Dtest_1
{
    public partial class MainWindow : Window
    {
        public GridGenerator gridGenerator;
        public MainWindow()
        {
            InitializeComponent();
            Initialization();
        }
        /// <summary>
        /// TextBox and windows settings initialization, and creating the class for the grid generator
        /// </summary>
        private void Initialization()
        {
            WindowState = WindowState.Maximized;
            gridGenerator = new GridGenerator(640, 480, 2);

            heightBox.Text = "480";
            widthBox.Text = "640";
            pitchBox.Text = "2";
            dpiBox.Text = "96";
        }
        /// <summary>
        /// function that gets the settings from the settings boxes and passes them to the grid generatorclass
        /// it catch an error if there  is one in the settings and revert to the initialization state
        /// </summary>
        private void ImageSettings()
        {
            try
            {
                int width = Int32.Parse(widthBox.Text);
                int height = Int32.Parse(heightBox.Text);
                int dpi = Int32.Parse(dpiBox.Text);
                int pitch = Int32.Parse(pitchBox.Text);
                gridGenerator.SetGridSettings(width, height, pitch, dpi);
            }
            catch
            {
                MessageBox.Show("Error in settings, Reverting to default settings", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                heightBox.Text = "480";
                widthBox.Text = "640";
                pitchBox.Text = "2";
                dpiBox.Text = "96";
                gridGenerator.SetGridSettings(640, 480, 2);
            }
        }
        /// <summary>
        /// Generate Button Event Click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ImageSettings();
            BitmapSource[] images = gridGenerator.GenerateImages();
            firstImage.Source = images[0];
            secondImage.Source = images[1];
        }
    }
}
