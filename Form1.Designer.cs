
namespace smart_ascii_gen {
    partial class Form_main {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent() {
            this.pictureBox_original = new System.Windows.Forms.PictureBox();
            this.button_generate = new System.Windows.Forms.Button();
            this.button_browse = new System.Windows.Forms.Button();
            this.richTextBox_output = new System.Windows.Forms.RichTextBox();
            this.trackBar_lum_max = new System.Windows.Forms.TrackBar();
            this.trackBar_lum_min = new System.Windows.Forms.TrackBar();
            this.label_lum_min = new System.Windows.Forms.Label();
            this.label_lum_max = new System.Windows.Forms.Label();
            this.trackBar_ascii_width = new System.Windows.Forms.TrackBar();
            this.label_width = new System.Windows.Forms.Label();
            this.label_edge_threshold = new System.Windows.Forms.Label();
            this.trackBar_edge_threshold = new System.Windows.Forms.TrackBar();
            this.button_copy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_original)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_lum_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_lum_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_ascii_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_edge_threshold)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_original
            // 
            this.pictureBox_original.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox_original.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox_original.Location = new System.Drawing.Point(1001, 25);
            this.pictureBox_original.Name = "pictureBox_original";
            this.pictureBox_original.Size = new System.Drawing.Size(212, 180);
            this.pictureBox_original.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_original.TabIndex = 0;
            this.pictureBox_original.TabStop = false;
            // 
            // button_generate
            // 
            this.button_generate.Location = new System.Drawing.Point(1001, 296);
            this.button_generate.Name = "button_generate";
            this.button_generate.Size = new System.Drawing.Size(212, 59);
            this.button_generate.TabIndex = 2;
            this.button_generate.Text = "Generate";
            this.button_generate.UseVisualStyleBackColor = true;
            this.button_generate.Click += new System.EventHandler(this.button_generate_Click);
            // 
            // button_browse
            // 
            this.button_browse.Location = new System.Drawing.Point(1001, 224);
            this.button_browse.Name = "button_browse";
            this.button_browse.Size = new System.Drawing.Size(212, 59);
            this.button_browse.TabIndex = 1;
            this.button_browse.Text = "Browse";
            this.button_browse.UseVisualStyleBackColor = true;
            this.button_browse.Click += new System.EventHandler(this.button_browse_Click);
            // 
            // richTextBox_output
            // 
            this.richTextBox_output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_output.DetectUrls = false;
            this.richTextBox_output.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.richTextBox_output.Location = new System.Drawing.Point(27, 25);
            this.richTextBox_output.Name = "richTextBox_output";
            this.richTextBox_output.ReadOnly = true;
            this.richTextBox_output.Size = new System.Drawing.Size(947, 681);
            this.richTextBox_output.TabIndex = 4;
            this.richTextBox_output.Text = "";
            this.richTextBox_output.WordWrap = false;
            // 
            // trackBar_lum_max
            // 
            this.trackBar_lum_max.LargeChange = 20;
            this.trackBar_lum_max.Location = new System.Drawing.Point(1001, 388);
            this.trackBar_lum_max.Maximum = 255;
            this.trackBar_lum_max.Name = "trackBar_lum_max";
            this.trackBar_lum_max.Size = new System.Drawing.Size(212, 56);
            this.trackBar_lum_max.SmallChange = 10;
            this.trackBar_lum_max.TabIndex = 3;
            this.trackBar_lum_max.Value = 255;
            // 
            // trackBar_lum_min
            // 
            this.trackBar_lum_min.LargeChange = 20;
            this.trackBar_lum_min.Location = new System.Drawing.Point(1001, 459);
            this.trackBar_lum_min.Maximum = 255;
            this.trackBar_lum_min.Name = "trackBar_lum_min";
            this.trackBar_lum_min.Size = new System.Drawing.Size(212, 56);
            this.trackBar_lum_min.SmallChange = 10;
            this.trackBar_lum_min.TabIndex = 4;
            // 
            // label_lum_min
            // 
            this.label_lum_min.AutoSize = true;
            this.label_lum_min.Location = new System.Drawing.Point(998, 439);
            this.label_lum_min.Name = "label_lum_min";
            this.label_lum_min.Size = new System.Drawing.Size(231, 17);
            this.label_lum_min.TabIndex = 9;
            this.label_lum_min.Text = "Luminosity Range Minimum (0-255)";
            // 
            // label_lum_max
            // 
            this.label_lum_max.AutoSize = true;
            this.label_lum_max.Location = new System.Drawing.Point(998, 368);
            this.label_lum_max.Name = "label_lum_max";
            this.label_lum_max.Size = new System.Drawing.Size(234, 17);
            this.label_lum_max.TabIndex = 8;
            this.label_lum_max.Text = "Luminosity Range Maximum (0-255)";
            // 
            // trackBar_ascii_width
            // 
            this.trackBar_ascii_width.LargeChange = 20;
            this.trackBar_ascii_width.Location = new System.Drawing.Point(1001, 597);
            this.trackBar_ascii_width.Maximum = 150;
            this.trackBar_ascii_width.Minimum = 1;
            this.trackBar_ascii_width.Name = "trackBar_ascii_width";
            this.trackBar_ascii_width.Size = new System.Drawing.Size(212, 56);
            this.trackBar_ascii_width.SmallChange = 10;
            this.trackBar_ascii_width.TabIndex = 6;
            this.trackBar_ascii_width.Value = 80;
            // 
            // label_width
            // 
            this.label_width.AutoSize = true;
            this.label_width.Location = new System.Drawing.Point(998, 577);
            this.label_width.Name = "label_width";
            this.label_width.Size = new System.Drawing.Size(154, 17);
            this.label_width.TabIndex = 9;
            this.label_width.Text = "ASCII Art Width (0-150)";
            // 
            // label_edge_threshold
            // 
            this.label_edge_threshold.AutoSize = true;
            this.label_edge_threshold.Location = new System.Drawing.Point(998, 508);
            this.label_edge_threshold.Name = "label_edge_threshold";
            this.label_edge_threshold.Size = new System.Drawing.Size(160, 17);
            this.label_edge_threshold.TabIndex = 11;
            this.label_edge_threshold.Text = "Edge Threshold (0-255)";
            // 
            // trackBar_edge_threshold
            // 
            this.trackBar_edge_threshold.LargeChange = 20;
            this.trackBar_edge_threshold.Location = new System.Drawing.Point(1001, 528);
            this.trackBar_edge_threshold.Maximum = 255;
            this.trackBar_edge_threshold.Name = "trackBar_edge_threshold";
            this.trackBar_edge_threshold.Size = new System.Drawing.Size(212, 56);
            this.trackBar_edge_threshold.SmallChange = 10;
            this.trackBar_edge_threshold.TabIndex = 5;
            this.trackBar_edge_threshold.Value = 80;
            // 
            // button_copy
            // 
            this.button_copy.Location = new System.Drawing.Point(1001, 647);
            this.button_copy.Name = "button_copy";
            this.button_copy.Size = new System.Drawing.Size(212, 59);
            this.button_copy.TabIndex = 7;
            this.button_copy.Text = "Copy to Clipboard";
            this.button_copy.UseVisualStyleBackColor = true;
            this.button_copy.Click += new System.EventHandler(this.button_copy_Click);
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(1245, 730);
            this.Controls.Add(this.button_copy);
            this.Controls.Add(this.label_width);
            this.Controls.Add(this.trackBar_ascii_width);
            this.Controls.Add(this.label_edge_threshold);
            this.Controls.Add(this.trackBar_edge_threshold);
            this.Controls.Add(this.label_lum_max);
            this.Controls.Add(this.label_lum_min);
            this.Controls.Add(this.trackBar_lum_min);
            this.Controls.Add(this.trackBar_lum_max);
            this.Controls.Add(this.button_browse);
            this.Controls.Add(this.button_generate);
            this.Controls.Add(this.pictureBox_original);
            this.Controls.Add(this.richTextBox_output);
            this.Name = "Form_main";
            this.Text = "Smart ASCII Art Generator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_original)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_lum_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_lum_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_ascii_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_edge_threshold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_original;
        private System.Windows.Forms.Button button_generate;
        private System.Windows.Forms.Button button_browse;
        private System.Windows.Forms.RichTextBox richTextBox_output;
        private System.Windows.Forms.TrackBar trackBar_lum_max;
        private System.Windows.Forms.TrackBar trackBar_lum_min;
        private System.Windows.Forms.Label label_lum_min;
        private System.Windows.Forms.Label label_lum_max;
        private System.Windows.Forms.TrackBar trackBar_ascii_width;
        private System.Windows.Forms.Label label_width;
        private System.Windows.Forms.Label label_edge_threshold;
        private System.Windows.Forms.TrackBar trackBar_edge_threshold;
        private System.Windows.Forms.Button button_copy;
    }
}

