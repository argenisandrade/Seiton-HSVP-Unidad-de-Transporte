
namespace Seiton
{
    partial class Form5
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.main_autBtn = new System.Windows.Forms.Button();
            this.main_informBtn = new System.Windows.Forms.Button();
            this.main_insertBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(28, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(262, 93);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(153, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(226, 25);
            this.label3.TabIndex = 10;
            this.label3.Text = "Unidad de Transporte ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(119, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(291, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Hospital San Vicente de Paul";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(124, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Ministerio de Salud Pública";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(324, 25);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(180, 93);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.main_autBtn);
            this.groupBox1.Controls.Add(this.main_informBtn);
            this.groupBox1.Controls.Add(this.main_insertBtn);
            this.groupBox1.Location = new System.Drawing.Point(70, 265);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 148);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // main_autBtn
            // 
            this.main_autBtn.Location = new System.Drawing.Point(119, 67);
            this.main_autBtn.Name = "main_autBtn";
            this.main_autBtn.Size = new System.Drawing.Size(155, 23);
            this.main_autBtn.TabIndex = 2;
            this.main_autBtn.Text = "Autorización de Salida";
            this.main_autBtn.UseVisualStyleBackColor = true;
            this.main_autBtn.Click += new System.EventHandler(this.main_autBtn_Click);
            // 
            // main_informBtn
            // 
            this.main_informBtn.Location = new System.Drawing.Point(119, 107);
            this.main_informBtn.Name = "main_informBtn";
            this.main_informBtn.Size = new System.Drawing.Size(155, 23);
            this.main_informBtn.TabIndex = 1;
            this.main_informBtn.Text = "Informes y Hojas de Ruta";
            this.main_informBtn.UseVisualStyleBackColor = true;
            this.main_informBtn.Click += new System.EventHandler(this.main_informBtn_Click);
            // 
            // main_insertBtn
            // 
            this.main_insertBtn.Location = new System.Drawing.Point(119, 26);
            this.main_insertBtn.Name = "main_insertBtn";
            this.main_insertBtn.Size = new System.Drawing.Size(155, 23);
            this.main_insertBtn.TabIndex = 0;
            this.main_insertBtn.Text = "Orden de Movilización";
            this.main_insertBtn.UseVisualStyleBackColor = true;
            this.main_insertBtn.Click += new System.EventHandler(this.main_insertBtn_Click);
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 435);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form5";
            this.Text = "Unidad de Transporte";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button main_informBtn;
        private System.Windows.Forms.Button main_insertBtn;
        private System.Windows.Forms.Button main_autBtn;
    }
}