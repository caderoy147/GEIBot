namespace Kauda
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.xStepNum = new System.Windows.Forms.TextBox();
            this.yStepNum = new System.Windows.Forms.TextBox();
            this.zStepNum = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.xStepStart = new System.Windows.Forms.Button();
            this.xClock = new System.Windows.Forms.PictureBox();
            this.xCounterClock = new System.Windows.Forms.PictureBox();
            this.yCounterClock = new System.Windows.Forms.PictureBox();
            this.yClock = new System.Windows.Forms.PictureBox();
            this.zCounterClock = new System.Windows.Forms.PictureBox();
            this.zClock = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.xClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCounterClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCounterClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zCounterClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zClock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM7";
            // 
            // xStepNum
            // 
            this.xStepNum.Location = new System.Drawing.Point(70, 66);
            this.xStepNum.Name = "xStepNum";
            this.xStepNum.Size = new System.Drawing.Size(100, 20);
            this.xStepNum.TabIndex = 0;
            this.xStepNum.TextChanged += new System.EventHandler(this.xStepNum_TextChanged);
            // 
            // yStepNum
            // 
            this.yStepNum.Location = new System.Drawing.Point(70, 126);
            this.yStepNum.Name = "yStepNum";
            this.yStepNum.Size = new System.Drawing.Size(100, 20);
            this.yStepNum.TabIndex = 1;
            this.yStepNum.TextChanged += new System.EventHandler(this.yStepNum_TextChanged);
            // 
            // zStepNum
            // 
            this.zStepNum.Location = new System.Drawing.Point(70, 185);
            this.zStepNum.Name = "zStepNum";
            this.zStepNum.Size = new System.Drawing.Size(100, 20);
            this.zStepNum.TabIndex = 2;
            this.zStepNum.TextChanged += new System.EventHandler(this.zStepNum_TextChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(108, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y Axis";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(108, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Z Axis";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(286, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(99, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "X Axis";
            // 
            // xStepStart
            // 
            this.xStepStart.Location = new System.Drawing.Point(195, 12);
            this.xStepStart.Name = "xStepStart";
            this.xStepStart.Size = new System.Drawing.Size(75, 23);
            this.xStepStart.TabIndex = 15;
            this.xStepStart.Text = "start";
            this.xStepStart.UseVisualStyleBackColor = true;
            this.xStepStart.Click += new System.EventHandler(this.xStepStart_Click);
            // 
            // xClock
            // 
            this.xClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xClock.Image = ((System.Drawing.Image)(resources.GetObject("xClock.Image")));
            this.xClock.Location = new System.Drawing.Point(286, 60);
            this.xClock.Name = "xClock";
            this.xClock.Size = new System.Drawing.Size(75, 26);
            this.xClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.xClock.TabIndex = 16;
            this.xClock.TabStop = false;
            this.xClock.Click += new System.EventHandler(this.xClock_Click);
            // 
            // xCounterClock
            // 
            this.xCounterClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xCounterClock.Image = ((System.Drawing.Image)(resources.GetObject("xCounterClock.Image")));
            this.xCounterClock.Location = new System.Drawing.Point(195, 60);
            this.xCounterClock.Name = "xCounterClock";
            this.xCounterClock.Size = new System.Drawing.Size(75, 26);
            this.xCounterClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.xCounterClock.TabIndex = 17;
            this.xCounterClock.TabStop = false;
            this.xCounterClock.Click += new System.EventHandler(this.xCounterClock_Click);
            // 
            // yCounterClock
            // 
            this.yCounterClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yCounterClock.Image = ((System.Drawing.Image)(resources.GetObject("yCounterClock.Image")));
            this.yCounterClock.Location = new System.Drawing.Point(195, 120);
            this.yCounterClock.Name = "yCounterClock";
            this.yCounterClock.Size = new System.Drawing.Size(75, 26);
            this.yCounterClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.yCounterClock.TabIndex = 19;
            this.yCounterClock.TabStop = false;
            this.yCounterClock.Click += new System.EventHandler(this.yCounterClock_Click);
            // 
            // yClock
            // 
            this.yClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.yClock.Image = ((System.Drawing.Image)(resources.GetObject("yClock.Image")));
            this.yClock.Location = new System.Drawing.Point(286, 120);
            this.yClock.Name = "yClock";
            this.yClock.Size = new System.Drawing.Size(75, 26);
            this.yClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.yClock.TabIndex = 18;
            this.yClock.TabStop = false;
            this.yClock.Click += new System.EventHandler(this.yClock_Click);
            // 
            // zCounterClock
            // 
            this.zCounterClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zCounterClock.Image = ((System.Drawing.Image)(resources.GetObject("zCounterClock.Image")));
            this.zCounterClock.Location = new System.Drawing.Point(195, 179);
            this.zCounterClock.Name = "zCounterClock";
            this.zCounterClock.Size = new System.Drawing.Size(75, 26);
            this.zCounterClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.zCounterClock.TabIndex = 21;
            this.zCounterClock.TabStop = false;
            this.zCounterClock.Click += new System.EventHandler(this.zCounterClock_Click);
            // 
            // zClock
            // 
            this.zClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zClock.Image = ((System.Drawing.Image)(resources.GetObject("zClock.Image")));
            this.zClock.Location = new System.Drawing.Point(286, 179);
            this.zClock.Name = "zClock";
            this.zClock.Size = new System.Drawing.Size(75, 26);
            this.zClock.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.zClock.TabIndex = 20;
            this.zClock.TabStop = false;
            this.zClock.Click += new System.EventHandler(this.zClock_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(99, 225);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Z Axis";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(556, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Z Axis";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(70, 241);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 24;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(195, 235);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 26);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(286, 235);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(75, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 26;
            this.pictureBox2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(532, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 27;
            this.button1.Text = "start";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(286, 291);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(75, 26);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 31;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(195, 291);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(75, 26);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 30;
            this.pictureBox4.TabStop = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(70, 297);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(99, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "Z Axis";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.zCounterClock);
            this.Controls.Add(this.zClock);
            this.Controls.Add(this.yCounterClock);
            this.Controls.Add(this.yClock);
            this.Controls.Add(this.xCounterClock);
            this.Controls.Add(this.xClock);
            this.Controls.Add(this.xStepStart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.zStepNum);
            this.Controls.Add(this.yStepNum);
            this.Controls.Add(this.xStepNum);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.xClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCounterClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCounterClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zCounterClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zClock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox xStepNum;
        private System.Windows.Forms.TextBox yStepNum;
        private System.Windows.Forms.TextBox zStepNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button xStepStart;
        private System.Windows.Forms.PictureBox xClock;
        private System.Windows.Forms.PictureBox xCounterClock;
        private System.Windows.Forms.PictureBox yCounterClock;
        private System.Windows.Forms.PictureBox yClock;
        private System.Windows.Forms.PictureBox zCounterClock;
        private System.Windows.Forms.PictureBox zClock;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label7;
    }
}

