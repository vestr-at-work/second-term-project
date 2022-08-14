using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace smart_ascii_gen {
    public partial class Form_main : Form {

        private String asciiCharsSorted = "@%#*+=-:. ";
        public Form_main() {
            InitializeComponent();
        }

        private void button_generate_Click(object sender, EventArgs e) {
            Bitmap inputImage = (Bitmap)pictureBox_original.Image;
            int asciiWidth = trackBar_ascii_width.Value;
            int luminosityMinimum = trackBar_lum_min.Value;
            int luminosityMaximum = trackBar_lum_max.Value;
            int sampleJump = inputImage.Width / asciiWidth;
            StringBuilder generatedAscii = new StringBuilder();

            for (int j = 0; j < inputImage.Height; j += (2*sampleJump)) {
                for (int i = 0; i < inputImage.Width; i += sampleJump) {
                    Color pixelColor = inputImage.GetPixel(i, j);
                    byte pixelLuminosity = (byte)(.299 * pixelColor.R + .587 * pixelColor.G + .114 * pixelColor.B);

                    int charIndex = getCharIndex(pixelLuminosity, luminosityMinimum, luminosityMaximum);
                    generatedAscii.Append(asciiCharsSorted[charIndex]);

                }
                generatedAscii.Append("\n");
            }
            richTextBox_output.Text = generatedAscii.ToString();
            
        }

        private int getCharIndex(byte luminosityValue, int rangeMinimum, int rangeMaximum) {
            
            int range = rangeMaximum - rangeMinimum;

            if (range < 0) {
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
                pictureBox_original.Image = new Bitmap(openFile.FileName);
            }
        }
    }
}
