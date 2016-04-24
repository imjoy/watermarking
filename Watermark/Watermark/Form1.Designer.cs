namespace Watermark
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdwthostbtn = new System.Windows.Forms.Button();
            this.InverseRDWTBTN = new System.Windows.Forms.Button();
            this.InverseDCTBtn = new System.Windows.Forms.Button();
            this.DCTBTN = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.OriginalPic = new System.Windows.Forms.PictureBox();
            this.watermacPic = new System.Windows.Forms.PictureBox();
            this.RDWTImage = new System.Windows.Forms.PictureBox();
            this.RDWTWatermark = new System.Windows.Forms.PictureBox();
            this.svd_pso = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OriginalPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.watermacPic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RDWTImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RDWTWatermark)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.svd_pso);
            this.groupBox1.Controls.Add(this.rdwthostbtn);
            this.groupBox1.Controls.Add(this.InverseRDWTBTN);
            this.groupBox1.Controls.Add(this.InverseDCTBtn);
            this.groupBox1.Controls.Add(this.DCTBTN);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(8, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 575);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // rdwthostbtn
            // 
            this.rdwthostbtn.Location = new System.Drawing.Point(0, 91);
            this.rdwthostbtn.Name = "rdwthostbtn";
            this.rdwthostbtn.Size = new System.Drawing.Size(84, 40);
            this.rdwthostbtn.TabIndex = 5;
            this.rdwthostbtn.Text = "RDWT";
            this.rdwthostbtn.UseVisualStyleBackColor = true;
            this.rdwthostbtn.Click += new System.EventHandler(this.rdwthostbtn_Click);
            // 
            // InverseRDWTBTN
            // 
            this.InverseRDWTBTN.Location = new System.Drawing.Point(0, 339);
            this.InverseRDWTBTN.Name = "InverseRDWTBTN";
            this.InverseRDWTBTN.Size = new System.Drawing.Size(84, 40);
            this.InverseRDWTBTN.TabIndex = 4;
            this.InverseRDWTBTN.Text = "Inverse RDWT";
            this.InverseRDWTBTN.UseVisualStyleBackColor = true;
            this.InverseRDWTBTN.Click += new System.EventHandler(this.InverseRDWTBTN_Click);
            // 
            // InverseDCTBtn
            // 
            this.InverseDCTBtn.Location = new System.Drawing.Point(0, 280);
            this.InverseDCTBtn.Name = "InverseDCTBtn";
            this.InverseDCTBtn.Size = new System.Drawing.Size(84, 40);
            this.InverseDCTBtn.TabIndex = 3;
            this.InverseDCTBtn.Text = "Inverse DCT ";
            this.InverseDCTBtn.UseVisualStyleBackColor = true;
            this.InverseDCTBtn.Click += new System.EventHandler(this.InverseDCTBtn_Click);
            // 
            // DCTBTN
            // 
            this.DCTBTN.Location = new System.Drawing.Point(0, 151);
            this.DCTBTN.Name = "DCTBTN";
            this.DCTBTN.Size = new System.Drawing.Size(84, 40);
            this.DCTBTN.TabIndex = 2;
            this.DCTBTN.Text = "DCT IMAGE";
            this.DCTBTN.UseVisualStyleBackColor = true;
            this.DCTBTN.Click += new System.EventHandler(this.DCTBTN_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(96, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 40);
            this.button2.TabIndex = 1;
            this.button2.Text = "Watermark Image";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 40);
            this.button1.TabIndex = 0;
            this.button1.Text = "Original Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // OriginalPic
            // 
            this.OriginalPic.Location = new System.Drawing.Point(214, 30);
            this.OriginalPic.Name = "OriginalPic";
            this.OriginalPic.Size = new System.Drawing.Size(224, 224);
            this.OriginalPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.OriginalPic.TabIndex = 1;
            this.OriginalPic.TabStop = false;
            // 
            // watermacPic
            // 
            this.watermacPic.Location = new System.Drawing.Point(214, 267);
            this.watermacPic.Name = "watermacPic";
            this.watermacPic.Size = new System.Drawing.Size(224, 224);
            this.watermacPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.watermacPic.TabIndex = 2;
            this.watermacPic.TabStop = false;
            // 
            // RDWTImage
            // 
            this.RDWTImage.Location = new System.Drawing.Point(445, 30);
            this.RDWTImage.Name = "RDWTImage";
            this.RDWTImage.Size = new System.Drawing.Size(224, 224);
            this.RDWTImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RDWTImage.TabIndex = 3;
            this.RDWTImage.TabStop = false;
            // 
            // RDWTWatermark
            // 
            this.RDWTWatermark.Location = new System.Drawing.Point(445, 267);
            this.RDWTWatermark.Name = "RDWTWatermark";
            this.RDWTWatermark.Size = new System.Drawing.Size(224, 224);
            this.RDWTWatermark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RDWTWatermark.TabIndex = 4;
            this.RDWTWatermark.TabStop = false;
            // 
            // svd_pso
            // 
            this.svd_pso.Location = new System.Drawing.Point(0, 211);
            this.svd_pso.Name = "svd_pso";
            this.svd_pso.Size = new System.Drawing.Size(84, 56);
            this.svd_pso.TabIndex = 6;
            this.svd_pso.Text = "SVD + PSO + EMBED";
            this.svd_pso.UseVisualStyleBackColor = true;
            this.svd_pso.Click += new System.EventHandler(this.svd_pso_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1194, 598);
            this.Controls.Add(this.RDWTWatermark);
            this.Controls.Add(this.RDWTImage);
            this.Controls.Add(this.watermacPic);
            this.Controls.Add(this.OriginalPic);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OriginalPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.watermacPic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RDWTImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RDWTWatermark)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox OriginalPic;
        private System.Windows.Forms.PictureBox watermacPic;
        private System.Windows.Forms.PictureBox RDWTImage;
        private System.Windows.Forms.PictureBox RDWTWatermark;
        private System.Windows.Forms.Button DCTBTN;
        private System.Windows.Forms.Button InverseDCTBtn;
        private System.Windows.Forms.Button InverseRDWTBTN;
        private System.Windows.Forms.Button rdwthostbtn;
        private System.Windows.Forms.Button svd_pso;
    }
}

