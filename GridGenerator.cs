using System;
using System.Windows.Media.Imaging;

namespace _4Dtest_1
{
    public class GridGenerator
    {
        private const double dpiUnit = 25.4;

        private int width;

        private int height;

        private int pitchLength;

        private int dpi;

        public GridGenerator(int Width, int Heigth, int PitchLengthInMm, int Dpi = 96)
        {
            SetGridSettings(Width, Heigth, PitchLengthInMm, Dpi);
        }

        /// <summary>
        /// a function to set the parameters of the class 
        /// </summary>
        /// <param name="Width">Width of the image of the gratting</param>
        /// <param name="Heigth">Heigth of the image of the gratting </param>
        /// <param name="PitchLengthInMm">the length of pitch that user want in Mm</param>
        /// <param name="Dpi">Dpi of the Monitor used; if not set it is automatically set to 96 </param>
        public void SetGridSettings(int Width, int Heigth, int PitchLengthInMm, int Dpi = 96)
        {
            dpi = Dpi;
            width = Width;
            height = Heigth;
            pitchLength = (int)(Dpi * (PitchLengthInMm / dpiUnit));
        }

        /// <summary>
        /// Function for generating the pattern's array of the question 1-1
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateFirstGrid()
        {
            byte[] grid = new byte[height * width];

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    int position = (width * j) + i;
                    int counter = (i + 1) / (pitchLength / 2);

                    if ((counter % 2) == 0)
                    { grid[position] = 0; }
                    else
                    { grid[position] = 255; }
                }
            }
            return grid;
        }

        /// <summary>
        /// Function for generating the array pattern's array of the question 1-2
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateSecondGrid()
        {
            int size = height * width;
            byte[] grid = new byte[size];
            double frequency = Math.PI / ((height * height) + (width * width));

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    int position = (width * j) + i;
                    int horizontalCounter = (i + 1) / pitchLength;
                    int verticalCounter = (j + 1) / pitchLength;

                    if ((horizontalCounter % 2) == 0 || (verticalCounter % 2) == 0)
                    { grid[position] = (byte)(255 - 255 * (Math.Cos(position * frequency))); }
                    else
                    { grid[position] = (byte)(255 * Math.Cos(position * frequency)); }
                }
            }
            return grid;
        }

        /// <summary>
        /// Function for converting the patterns from byte arrays into images
        /// </summary>
        /// <returns></returns>
        public BitmapSource[] GenerateImages()
        {
            byte[] firstGrid = GenerateFirstGrid();
            byte[] secondGrid = GenerateSecondGrid();

            BitmapSource firstImage = BitmapSource.Create(width, height, dpi, dpi, System.Windows.Media.PixelFormats.Gray8, null, firstGrid, width);
            BitmapSource secondImage = BitmapSource.Create(width, height, dpi, dpi, System.Windows.Media.PixelFormats.Gray8, null, secondGrid, width);

            BitmapSource[] images = { firstImage, secondImage };

            return images;
        }
    }
}

