namespace ThermometerCalibration
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TS1 = new System.Windows.Forms.TextBox();
            this.TS2 = new System.Windows.Forms.TextBox();
            this.TS3 = new System.Windows.Forms.TextBox();
            this.HS1 = new System.Windows.Forms.TextBox();
            this.HS2 = new System.Windows.Forms.TextBox();
            this.HS3 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TempBar = new CircularProgressBar.CircularProgressBar();
            this.humibar = new CircularProgressBar.CircularProgressBar();
            this.Tsteady = new System.Windows.Forms.TextBox();
            this.Hsteady = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TimeTSteady = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.TimeHSteady = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "初始化";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 18;
            this.listBox1.Items.AddRange(new object[] {
            "设备列表"});
            this.listBox1.Location = new System.Drawing.Point(12, 47);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(283, 310);
            this.listBox1.TabIndex = 1;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 376);
            this.progressBar1.Maximum = 200;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(702, 33);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Status";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(300, 47);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 37);
            this.button2.TabIndex = 4;
            this.button2.Text = "校准开始";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(36, 141);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(229, 28);
            this.textBox1.TabIndex = 5;
            this.textBox1.Visible = false;
            // 
            // TS1
            // 
            this.TS1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS1.Location = new System.Drawing.Point(304, 120);
            this.TS1.Name = "TS1";
            this.TS1.Size = new System.Drawing.Size(95, 28);
            this.TS1.TabIndex = 6;
            this.TS1.Text = "15.00";
            this.TS1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TS2
            // 
            this.TS2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS2.Location = new System.Drawing.Point(304, 154);
            this.TS2.Name = "TS2";
            this.TS2.Size = new System.Drawing.Size(95, 28);
            this.TS2.TabIndex = 7;
            this.TS2.Text = "20.00";
            this.TS2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TS3
            // 
            this.TS3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TS3.Location = new System.Drawing.Point(304, 188);
            this.TS3.Name = "TS3";
            this.TS3.Size = new System.Drawing.Size(95, 28);
            this.TS3.TabIndex = 8;
            this.TS3.Text = "30.00";
            this.TS3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // HS1
            // 
            this.HS1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HS1.Location = new System.Drawing.Point(304, 261);
            this.HS1.Name = "HS1";
            this.HS1.Size = new System.Drawing.Size(95, 28);
            this.HS1.TabIndex = 9;
            this.HS1.Text = "40.00";
            this.HS1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // HS2
            // 
            this.HS2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HS2.Location = new System.Drawing.Point(304, 295);
            this.HS2.Name = "HS2";
            this.HS2.Size = new System.Drawing.Size(95, 28);
            this.HS2.TabIndex = 10;
            this.HS2.Text = "60.00";
            this.HS2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // HS3
            // 
            this.HS3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HS3.Location = new System.Drawing.Point(304, 329);
            this.HS3.Name = "HS3";
            this.HS3.Size = new System.Drawing.Size(95, 28);
            this.HS3.TabIndex = 11;
            this.HS3.Text = "80.00";
            this.HS3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(301, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 24);
            this.label2.TabIndex = 12;
            this.label2.Text = "温度设置点";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(301, 234);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "湿度设置点";
            // 
            // TempBar
            // 
            this.TempBar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.TempBar.AnimationSpeed = 500;
            this.TempBar.BackColor = System.Drawing.Color.Transparent;
            this.TempBar.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.TempBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TempBar.InnerColor = System.Drawing.SystemColors.ButtonFace;
            this.TempBar.InnerMargin = 2;
            this.TempBar.InnerWidth = -1;
            this.TempBar.Location = new System.Drawing.Point(424, 18);
            this.TempBar.MarqueeAnimationSpeed = 2000;
            this.TempBar.Maximum = 50;
            this.TempBar.Minimum = 10;
            this.TempBar.Name = "TempBar";
            this.TempBar.OuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.TempBar.OuterMargin = -25;
            this.TempBar.OuterWidth = 26;
            this.TempBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.TempBar.ProgressWidth = 10;
            this.TempBar.SecondaryFont = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TempBar.Size = new System.Drawing.Size(135, 130);
            this.TempBar.StartAngle = 270;
            this.TempBar.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.TempBar.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.TempBar.SubscriptText = "";
            this.TempBar.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.TempBar.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.TempBar.SuperscriptText = "";
            this.TempBar.TabIndex = 14;
            this.TempBar.Text = "36.51℃";
            this.TempBar.TextMargin = new System.Windows.Forms.Padding(0);
            this.TempBar.Value = 50;
            // 
            // humibar
            // 
            this.humibar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.humibar.AnimationSpeed = 500;
            this.humibar.BackColor = System.Drawing.Color.Transparent;
            this.humibar.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.humibar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.humibar.InnerColor = System.Drawing.SystemColors.ButtonFace;
            this.humibar.InnerMargin = 2;
            this.humibar.InnerWidth = -1;
            this.humibar.Location = new System.Drawing.Point(573, 17);
            this.humibar.MarqueeAnimationSpeed = 2000;
            this.humibar.Name = "humibar";
            this.humibar.OuterColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.humibar.OuterMargin = -25;
            this.humibar.OuterWidth = 26;
            this.humibar.ProgressColor = System.Drawing.Color.MediumBlue;
            this.humibar.ProgressWidth = 10;
            this.humibar.SecondaryFont = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.humibar.Size = new System.Drawing.Size(135, 130);
            this.humibar.StartAngle = 270;
            this.humibar.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.humibar.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.humibar.SubscriptText = "";
            this.humibar.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.humibar.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.humibar.SuperscriptText = "";
            this.humibar.TabIndex = 15;
            this.humibar.Text = "36.51%";
            this.humibar.TextMargin = new System.Windows.Forms.Padding(0);
            this.humibar.Value = 68;
            // 
            // Tsteady
            // 
            this.Tsteady.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tsteady.Location = new System.Drawing.Point(607, 177);
            this.Tsteady.Name = "Tsteady";
            this.Tsteady.Size = new System.Drawing.Size(65, 28);
            this.Tsteady.TabIndex = 16;
            this.Tsteady.Text = "0.1";
            this.Tsteady.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Hsteady
            // 
            this.Hsteady.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hsteady.Location = new System.Drawing.Point(607, 211);
            this.Hsteady.Name = "Hsteady";
            this.Hsteady.Size = new System.Drawing.Size(65, 28);
            this.Hsteady.TabIndex = 17;
            this.Hsteady.Text = "2";
            this.Hsteady.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label4.Location = new System.Drawing.Point(434, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 24);
            this.label4.TabIndex = 18;
            this.label4.Text = "温箱温度稳定范围±";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label5.Location = new System.Drawing.Point(434, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 24);
            this.label5.TabIndex = 19;
            this.label5.Text = "温箱湿度稳定范围±";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label6.Location = new System.Drawing.Point(676, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 24);
            this.label6.TabIndex = 20;
            this.label6.Text = "℃";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label7.Location = new System.Drawing.Point(678, 211);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 24);
            this.label7.TabIndex = 21;
            this.label7.Text = "%";
            // 
            // TimeTSteady
            // 
            this.TimeTSteady.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeTSteady.Location = new System.Drawing.Point(607, 245);
            this.TimeTSteady.Name = "TimeTSteady";
            this.TimeTSteady.Size = new System.Drawing.Size(65, 28);
            this.TimeTSteady.TabIndex = 22;
            this.TimeTSteady.Text = "9";
            this.TimeTSteady.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label8.Location = new System.Drawing.Point(434, 249);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(154, 24);
            this.label8.TabIndex = 23;
            this.label8.Text = "温度稳定等待时间";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label9.Location = new System.Drawing.Point(678, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 24);
            this.label9.TabIndex = 24;
            this.label9.Text = "min";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label10.Location = new System.Drawing.Point(678, 279);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 24);
            this.label10.TabIndex = 27;
            this.label10.Text = "min";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.label11.Location = new System.Drawing.Point(434, 282);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(154, 24);
            this.label11.TabIndex = 26;
            this.label11.Text = "湿度稳定等待时间";
            // 
            // TimeHSteady
            // 
            this.TimeHSteady.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeHSteady.Location = new System.Drawing.Point(607, 278);
            this.TimeHSteady.Name = "TimeHSteady";
            this.TimeHSteady.Size = new System.Drawing.Size(65, 28);
            this.TimeHSteady.TabIndex = 25;
            this.TimeHSteady.Text = "9";
            this.TimeHSteady.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 421);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.TimeHSteady);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TimeTSteady);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Hsteady);
            this.Controls.Add(this.Tsteady);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HS3);
            this.Controls.Add(this.HS2);
            this.Controls.Add(this.HS1);
            this.Controls.Add(this.TS3);
            this.Controls.Add(this.TS2);
            this.Controls.Add(this.TS1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.TempBar);
            this.Controls.Add(this.humibar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox TS1;
        private System.Windows.Forms.TextBox TS2;
        private System.Windows.Forms.TextBox TS3;
        private System.Windows.Forms.TextBox HS1;
        private System.Windows.Forms.TextBox HS2;
        private System.Windows.Forms.TextBox HS3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CircularProgressBar.CircularProgressBar TempBar;
        private CircularProgressBar.CircularProgressBar humibar;
        private System.Windows.Forms.TextBox Tsteady;
        private System.Windows.Forms.TextBox Hsteady;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TimeTSteady;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TimeHSteady;
    }
}

