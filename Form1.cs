using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace smart_ascii_gen {
    public partial class Form_main : Form {

        //private String asciiCharsSorted = "@%#*+=-_:. ";
        private String asciiCharsSorted = "@#%S?*+=:,. ";
        //private String asciiCharsSorted = "@%#8EG4SJTn*1x(;~+:,-. ";

        private const double redConst = .299;
        private const double greenConst = .587;
        private const double blueConst = .144;

        Bitmap originalImage;
        Bitmap resizedImage;
        int newWidth = 300;
        Bitmap bluredGrayscaleImage;
        Bitmap edgesAnglesImage;

        public Form_main() {
            InitializeComponent();
        }

        private void button_generate_Click(object sender, EventArgs e) {
            Bitmap inputImage = (Bitmap)pictureBox_original.Image;
            int asciiArtWidth = trackBar_ascii_width.Value;
            int luminosityMinimum = trackBar_lum_min.Value;
            int luminosityMaximum = trackBar_lum_max.Value;
            int sampleStep = Math.Max(1, (inputImage.Width / asciiArtWidth));
            StringBuilder generatedAscii = new StringBuilder();

            for (int j = 0; j < inputImage.Height; j += (2*sampleStep)) {
                for (int i = 0; i < inputImage.Width; i += sampleStep) {
                    int avgLuminosity = getAvgLuminosity(i, j, sampleStep, inputImage);
                    int charIndex = getChar(avgLuminosity, luminosityMinimum, luminosityMaximum);
                    generatedAscii.Append(asciiCharsSorted[charIndex]);

                }
                generatedAscii.Append("\n");
            }
            richTextBox_output.Text = generatedAscii.ToString();
            
        }

        private int getAvgLuminosity(int x, int y, int step, Bitmap image) {
            Color pixelColor;
            int luminositySum = 0;
            int pixelCount = 0;
            int avgLuminosity = 0;
            if (step <= 2) {
                pixelColor = image.GetPixel(x, y);
                luminositySum = (int)(redConst * pixelColor.R + greenConst * pixelColor.G + blueConst * pixelColor.B);
                pixelCount++;
                
            }
            else {
                for (int j = -(step / 2); j < (step / 2); j++) {
                    for (int i = -(step / 2); i < (step / 2); i++) {
                        if (((y + j) >= 0 && (y + j) < image.Height) && ((x + i) >= 0 && (x + i) < image.Width)) {
                            pixelColor = image.GetPixel((x + i), (y + j));
                            luminositySum += (int)(redConst * pixelColor.R + greenConst * pixelColor.G + blueConst * pixelColor.B);
                            pixelCount++;
                        } 
                    }
                }
            }

            avgLuminosity = luminositySum / pixelCount;
            return avgLuminosity;
        }

        private int getChar(int luminosityValue, int rangeMinimum, int rangeMaximum) {
            
            int range = rangeMaximum - rangeMinimum;

            if (range <= 0) {
                rangeMinimum = 0;
                rangeMaximum = 255;
                range = 255;
            }

            float scaledValue = (float)Math.Min(1, (Math.Max(0, (luminosityValue - rangeMinimum)) / (float)range));
            int charIndex = (int)((asciiCharsSorted.Length - 1) * scaledValue);

            return charIndex;
            
        }

        private void button_browse_Click(object sender, EventArgs e) {
            OpenFileDialog openFile = new OpenFileDialog();
            if(openFile.ShowDialog() == DialogResult.OK) {
                originalImage = new Bitmap(openFile.FileName);
                int newHeight = (int)(originalImage.Height * (newWidth / (double)originalImage.Width));
                resizedImage = new Bitmap(originalImage, new Size(newWidth, newHeight));
                //resizedImage = resizeImage(originalImage, 800);
                pictureBox_original.Image = resizedImage;
            }
            bluredGrayscaleImage = gausianBlur(resizedImage);
            edgesAnglesImage = edgeDetection(bluredGrayscaleImage);
            //pictureBox_original.Image = edgesAnglesImage;
        }


        private Bitmap edgeDetection(Bitmap image) {
            Bitmap returnImage = new Bitmap(image);
            Color pixelColor;
            Color edgeAnglePixelColor;
            int pixelValue;
            int edgeValue;
            int edgeAngle;
            int[,] verticalMatrix;
            int[,] horizontalMatrix;
            int verticalMatrixSum;
            int horizontalMatrixSum;

            const int kernelSize = 3;
            verticalMatrix = new int[kernelSize, kernelSize] { { -1, -2, -1 }, { 0, 0, 0, }, { 1, 2, 1 } };
            horizontalMatrix = new int[kernelSize, kernelSize] { { -1, 0, 1 }, { -2, 0, 2, }, { -1, 0, 1 } };

            for (int y = 1; y < image.Height - 1; y++) {
                for (int x = 1; x < image.Width - 1; x++) {
                    verticalMatrixSum = 0;
                    horizontalMatrixSum = 0;

                    for (int j = -(kernelSize / 2); j <= (kernelSize / 2); j++) {
                        for (int i = -(kernelSize / 2); i <= (kernelSize / 2); i++) {
                            if (((y + j) >= 0 && (y + j) < image.Height) && ((x + i) >= 0 && (x + i) < image.Width)) {
                                pixelColor = image.GetPixel((x + i), (y + j));
                                pixelValue = (int)pixelColor.R; //we know that all the colors are the same since the image is grayscale
                                verticalMatrixSum += (pixelValue * verticalMatrix[i + (kernelSize / 2), j + (kernelSize / 2)]);
                                horizontalMatrixSum += (pixelValue * horizontalMatrix[i + (kernelSize / 2), j + (kernelSize / 2)]);
                            }
                        }
                    }
                    if (verticalMatrixSum == 0)
                        verticalMatrixSum++;

                    edgeValue = (int)Math.Min(255, (Math.Sqrt((verticalMatrixSum * verticalMatrixSum) + (horizontalMatrixSum * horizontalMatrixSum)) / 1000) * 255);
                    edgeAngle = (int)((Math.Atan(horizontalMatrixSum / (double)verticalMatrixSum) / (2 * Math.PI)) * 360) + 90;

                    edgeAnglePixelColor = Color.FromArgb(255, edgeAngle, edgeValue, 0);

                    returnImage.SetPixel(x, y, edgeAnglePixelColor);
                }
            }
            return returnImage;
        }

        private Bitmap gausianBlur(Bitmap originalImage, int kernelSize = 3) {
            Bitmap bluredImage = new Bitmap(originalImage);
            Color pixelColor;
            Color bluredPixelColor;
            int pixelLuminosity;
            int luminositySum = 0;
            int bluredPixelValue;
            //int pixelCount = 0;
            int[,] matrix;
            int matrixSum;

            if (kernelSize == 3) {
                matrix = new int[3, 3] { { 1, 2, 1 }, { 2, 4, 2, }, { 1, 2, 1 } };
                matrixSum = 16;
            }
            else {
                //nothing else yet implemented
                kernelSize = 3;
                matrix = new int[3, 3] { { 1, 2, 1 }, { 2, 4, 2, }, { 1, 2, 1 } };
                matrixSum = 16;
            }

            for (int y = 0; y < originalImage.Height; y++) {
                for (int x = 0; x < originalImage.Width; x++) {
                    luminositySum = 0;

                    for (int j = -(kernelSize / 2); j <= (kernelSize / 2); j++) {
                        for (int i = -(kernelSize / 2); i <= (kernelSize / 2); i++) {
                            if (((y + j) >= 0 && (y + j) < originalImage.Height) && ((x + i) >= 0 && (x + i) < originalImage.Width)) {
                                pixelColor = originalImage.GetPixel((x + i), (y + j));
                                pixelLuminosity = (int)(redConst * pixelColor.R + greenConst * pixelColor.G + blueConst * pixelColor.B);
                                luminositySum += (pixelLuminosity * matrix[i + (kernelSize / 2), j + (kernelSize / 2)]);
                                //pixelCount++;
                            }
                        }
                    }

                    bluredPixelValue = (int)Math.Min(255,(luminositySum / matrixSum));
                    bluredPixelColor = Color.FromArgb(255, bluredPixelValue, bluredPixelValue, bluredPixelValue);
                    bluredImage.SetPixel(x, y, bluredPixelColor);
                }

            }

            return bluredImage;
        }
    }
}
