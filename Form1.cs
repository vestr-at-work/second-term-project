/* 
 * Smart ASCII Art Generator
 * David Petera, 1. year of studies
 * Summer term 2021/22
 * NPRG031 – Programming 2
*/

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace smart_ascii_gen {
    public partial class Form_main : Form {

        public Form_main() {
            InitializeComponent();
        }

        asciiArtGenerator generator = new asciiArtGenerator(); 

        private void button_generate_Click(object sender, EventArgs e) {
            //Acquring settings info from the form elements
            int asciiArtWidth = trackBar_ascii_width.Value;
            int luminosityMinimum = trackBar_lum_min.Value;
            int luminosityMaximum = trackBar_lum_max.Value;
            int edgeThreshold = trackBar_edge_threshold.Value;

            //Generating ASCII art and showing it to user through richTextBox element
            try {
                generator.generateArt(asciiArtWidth, luminosityMinimum, luminosityMaximum, edgeThreshold);
                richTextBox_output.Text = generator.outputASCII;
            }
            catch { }
        }

        
        private void button_browse_Click(object sender, EventArgs e) {
            //Handeling of the open file dialog and passing the input image to the generator
            OpenFileDialog openFile = new OpenFileDialog();
            if(openFile.ShowDialog() == DialogResult.OK) {
                try {
                    Bitmap inputImage = new Bitmap(openFile.FileName);
                    generator.newInput(inputImage);
                    pictureBox_original.Image = inputImage;
                }
                catch (ArgumentException) {
                    MessageBox.Show("Wrong file chosen.\nPlease choose file in compatible format.", "Input file Error");
                }
                
            }
        }

        private void button_copy_Click(object sender, EventArgs e) {
            //Copying the ASCII Art to clipboard
            try {
                Clipboard.SetText(richTextBox_output.Text);
            }
            catch { }
        }
    }

    public class asciiArtGenerator {
        //The main generator class containing all the necessary image transforming methods 

        //String of chars from which the ASCII art is built
        private String asciiCharsSorted = "@#%S?*+=:,. ";
        //private String asciiCharsSorted = "@%#*+=-_:. ";
        //private String asciiCharsSorted = "@%#8EG4SJTn*1x(;~+:,-. ";

        private const double redConst = .299;
        private const double greenConst = .587;
        private const double blueConst = .144;

        private Bitmap originalImage;
        private Bitmap resizedImage;
        private int newWidth = 300;
        private Bitmap bluredGrayscaleImage;
        private Bitmap edgesAnglesImage;

        public string outputASCII;

        private struct PixelInfo {
            public PixelInfo(int lum, int edge, int angle) {
                luminosityValue = lum;
                edgeValue = edge;
                edgeAngleValue = angle;
            }

            public int luminosityValue;
            public int edgeValue;
            public int edgeAngleValue;
        }

        public void newInput(Bitmap inputImage) {
            //Method in which input image is resized (for preformance reasons) and auxiliary images created

            originalImage = new Bitmap(inputImage);
            int newHeight = (int)(originalImage.Height * (newWidth / (double)originalImage.Width));
            resizedImage = new Bitmap(originalImage, new Size(newWidth, newHeight));

            bluredGrayscaleImage = gaussianBlur(resizedImage); 
            edgesAnglesImage = edgeDetection(bluredGrayscaleImage); //image with edge intensity in the red color channel and angle of the edge (0-180 degrees) in the green color channel
        }

        public void generateArt(int asciiArtWidth, int luminosityMin, int luminosityMax, int edgeThreshold) {
            //Method in which the ASCII art is generated and assembled

            if (resizedImage != null) {
                int sampleStep = Math.Max(1, (resizedImage.Width / asciiArtWidth));
                StringBuilder generatedASCII = new StringBuilder();

                for (int j = 0; j < resizedImage.Height; j += (2 * sampleStep)) {
                    for (int i = 0; i < resizedImage.Width; i += sampleStep) {
                        PixelInfo avgPixelInfo = getAvgPixelInfo(i, j, sampleStep);
                        generatedASCII.Append(getASCIIChar(avgPixelInfo, luminosityMin, luminosityMax, edgeThreshold));

                    }
                    generatedASCII.Append("\n");
                }

                outputASCII = generatedASCII.ToString();
            }
        }

        private PixelInfo getAvgPixelInfo(int x, int y, int step) {
            //Method which outputs average pixel values used by ASCII Art generator (luminosity, presence and the angle of the edge) of a given pixel area

            Color pixelColor;
            Color edgePixelColor;
            int luminositySum = 0;
            int edgeSum = 0;
            int angleSum = 0;
            int pixelCount = 0;
            PixelInfo pixelInfo;

            if (step <= 2) {
                pixelColor = resizedImage.GetPixel(x, y);
                luminositySum = (int)(redConst * pixelColor.R + greenConst * pixelColor.G + blueConst * pixelColor.B);
                pixelCount++;

            }
            else {
                for (int j = -(step / 2); j < (step / 2); j++) {
                    for (int i = -(step / 2); i < (step / 2); i++) {
                        if (((y + j) >= 0 && (y + j) < resizedImage.Height) && ((x + i) >= 0 && (x + i) < resizedImage.Width)) {
                            pixelColor = resizedImage.GetPixel((x + i), (y + j));
                            luminositySum += (int)(redConst * pixelColor.R + greenConst * pixelColor.G + blueConst * pixelColor.B);
                            edgePixelColor = edgesAnglesImage.GetPixel((x + i), (y + j));
                            edgeSum += (int)(edgePixelColor.R);
                            angleSum += (int)(edgePixelColor.G);
                            pixelCount++;
                        }
                    }
                }
            }
            //edgePixelColor = edgesAnglesImage.GetPixel(x, y);
            //pixelInfo = new PixelInfo(luminositySum / pixelCount, edgePixelColor.R, edgePixelColor.G);
            pixelInfo = new PixelInfo(luminositySum / pixelCount, edgeSum / pixelCount, angleSum / pixelCount);

            return pixelInfo;
        }

        private char getASCIIChar(PixelInfo avgPixelInfo, int rangeMinimum, int rangeMaximum, int edgeThreshold) {
            //Method which determins which ASCII character fits the most

            int range = rangeMaximum - rangeMinimum;
            //if range set wrong, reset rangeMinimum and rangeMaximum
            if (range <= 0) {
                rangeMinimum = 0;
                rangeMaximum = 255;
                range = 255;
            }

            //scale luminosity value according to the range
            float scaledValue = (float)Math.Min(1, (Math.Max(0, (avgPixelInfo.luminosityValue - rangeMinimum)) / (float)range));
            int charIndex = (int)((asciiCharsSorted.Length - 1) * scaledValue);

            //if edge in the area present and edge value higher than the threshold and luminosity value not too extreme (too light or too dark)
            if (avgPixelInfo.edgeValue > edgeThreshold && charIndex > asciiCharsSorted.Length * 0.15 && charIndex < asciiCharsSorted.Length * 0.85) {
                //and if edge angle is apropriate return "edge character"

                if (avgPixelInfo.edgeAngleValue > 45 && avgPixelInfo.edgeAngleValue < 60) {
                    return '\\';
                }
                else if (avgPixelInfo.edgeAngleValue > 85 && avgPixelInfo.edgeAngleValue < 95) {
                    return '|';
                }
                else if (avgPixelInfo.edgeAngleValue > 120 && avgPixelInfo.edgeAngleValue < 135) {
                    return '/';
                }
                else if (avgPixelInfo.edgeAngleValue < 5 || avgPixelInfo.edgeAngleValue > 175) {
                    return '_';
                }
            }

            //otherwise return normal character from sorted character set
            return asciiCharsSorted[charIndex];
        }

        private Bitmap gaussianBlur(Bitmap originalImage, int kernelSize = 3) {
            //Method in which blured image is generated using small Gausian blur, that helps with the getting rid of false positive edge detections
            Bitmap bluredImage = new Bitmap(originalImage);
            Color pixelColor;
            Color bluredPixelColor;
            int pixelLuminosity;
            int luminositySum = 0;
            int bluredPixelValue;
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
                            }
                        }
                    }

                    bluredPixelValue = (int)Math.Min(255, (luminositySum / matrixSum));
                    bluredPixelColor = Color.FromArgb(255, bluredPixelValue, bluredPixelValue, bluredPixelValue);
                    bluredImage.SetPixel(x, y, bluredPixelColor);
                }

            }

            return bluredImage;
        }

        private Bitmap edgeDetection(Bitmap image) {
            //Method in which edges are detected using Sobel operator
            //and their intensity and angle is saved in red and green color channel (respectively) of the output image

            Bitmap outputImage = new Bitmap(image);
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

            for (int y = 0; y < image.Height; y++) {
                for (int x = 0; x < image.Width; x++) {
                    verticalMatrixSum = 0;
                    horizontalMatrixSum = 0;

                    for (int j = -(kernelSize / 2); j <= (kernelSize / 2); j++) {
                        for (int i = -(kernelSize / 2); i <= (kernelSize / 2); i++) {
                            if (((y + j) >= 0 && (y + j) < image.Height) && ((x + i) >= 0 && (x + i) < image.Width)) {
                                pixelColor = image.GetPixel((x + i), (y + j));
                                pixelValue = (int)pixelColor.R; //we know that all the channels are the same since the image is grayscale
                                verticalMatrixSum += (pixelValue * verticalMatrix[i + (kernelSize / 2), j + (kernelSize / 2)]);
                                horizontalMatrixSum += (pixelValue * horizontalMatrix[i + (kernelSize / 2), j + (kernelSize / 2)]);
                            }
                        }
                    }

                    if (verticalMatrixSum == 0) //edge case scenario
                        verticalMatrixSum++;

                    //very basic aproach to normalizing the edge intensity to value between 0 and 255
                    edgeValue = (int)Math.Min(255, (Math.Sqrt((verticalMatrixSum * verticalMatrixSum) + (horizontalMatrixSum * horizontalMatrixSum)) / 1000) * 255);

                    //converting radians to degrees and shifting values to positive range
                    edgeAngle = (int)((Math.Atan(horizontalMatrixSum / (double)verticalMatrixSum) / (2 * Math.PI)) * 360) + 90;

                    edgeAnglePixelColor = Color.FromArgb(255, edgeValue, edgeAngle, 0);
                    outputImage.SetPixel(x, y, edgeAnglePixelColor);
                }
            }

            return outputImage;
        }
    }
}
