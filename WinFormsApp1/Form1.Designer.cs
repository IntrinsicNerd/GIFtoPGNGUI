namespace WinFormsApp1
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
            components = new System.ComponentModel.Container();
            button1 = new Button();
            openFileDialog1 = new OpenFileDialog();
            richTextBox1 = new RichTextBox();
            button2 = new Button();
            timer1 = new System.Windows.Forms.Timer(components);
            label3 = new Label();
            label4 = new Label();
            LSR = new NumericUpDown();
            LSG = new NumericUpDown();
            LSB = new NumericUpDown();
            RSB = new NumericUpDown();
            RSG = new NumericUpDown();
            RSR = new NumericUpDown();
            checkBox2 = new CheckBox();
            sizeud = new NumericUpDown();
            label1 = new Label();
            dhbox = new TextBox();
            lhbox = new TextBox();
            colswit = new Button();
            label2 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)LSR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LSG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LSB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RSB).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RSG).BeginInit();
            ((System.ComponentModel.ISupportInitialize)RSR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)sizeud).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(13, 12);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(88, 27);
            button1.TabIndex = 1;
            button1.Text = "Load";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(10, 93);
            richTextBox1.Margin = new Padding(4, 3, 4, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(248, 239);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // button2
            // 
            button2.Location = new Point(10, 60);
            button2.Margin = new Padding(4, 3, 4, 3);
            button2.Name = "button2";
            button2.Size = new Size(175, 27);
            button2.TabIndex = 4;
            button2.Text = "Click for black on bottom";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // timer1
            // 
            timer1.Interval = 500;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(227, 12);
            label3.Name = "label3";
            label3.Size = new Size(37, 15);
            label3.TabIndex = 16;
            label3.Text = "Light:";
            label3.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(230, 51);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 17;
            label4.Text = "Dark:";
            label4.Visible = false;
            // 
            // LSR
            // 
            LSR.Location = new Point(264, 8);
            LSR.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            LSR.Name = "LSR";
            LSR.Size = new Size(46, 23);
            LSR.TabIndex = 18;
            LSR.Visible = false;
            LSR.ValueChanged += LSR_ValueChanged;
            // 
            // LSG
            // 
            LSG.Location = new Point(316, 8);
            LSG.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            LSG.Name = "LSG";
            LSG.Size = new Size(42, 23);
            LSG.TabIndex = 19;
            LSG.Visible = false;
            LSG.ValueChanged += LSG_ValueChanged;
            // 
            // LSB
            // 
            LSB.Location = new Point(364, 8);
            LSB.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            LSB.Name = "LSB";
            LSB.Size = new Size(45, 23);
            LSB.TabIndex = 20;
            LSB.Visible = false;
            LSB.ValueChanged += LSB_ValueChanged;
            // 
            // RSB
            // 
            RSB.Location = new Point(365, 49);
            RSB.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            RSB.Name = "RSB";
            RSB.Size = new Size(44, 23);
            RSB.TabIndex = 23;
            RSB.Visible = false;
            RSB.ValueChanged += RSB_ValueChanged;
            // 
            // RSG
            // 
            RSG.Location = new Point(316, 49);
            RSG.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            RSG.Name = "RSG";
            RSG.Size = new Size(42, 23);
            RSG.TabIndex = 22;
            RSG.Visible = false;
            RSG.ValueChanged += RSG_ValueChanged;
            // 
            // RSR
            // 
            RSR.Location = new Point(264, 49);
            RSR.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            RSR.Name = "RSR";
            RSR.Size = new Size(46, 23);
            RSR.TabIndex = 21;
            RSR.Visible = false;
            RSR.ValueChanged += RSR_ValueChanged;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(110, 1);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(111, 34);
            checkBox2.TabIndex = 24;
            checkBox2.Text = "GIF marks \r\ndeparted square";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // sizeud
            // 
            sizeud.Location = new Point(276, 159);
            sizeud.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            sizeud.Name = "sizeud";
            sizeud.Size = new Size(71, 23);
            sizeud.TabIndex = 25;
            sizeud.Value = new decimal(new int[] { 400, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(265, 126);
            label1.Name = "label1";
            label1.Size = new Size(99, 30);
            label1.TabIndex = 26;
            label1.Text = "Size (Likely won't\r\n need to change):";
            // 
            // dhbox
            // 
            dhbox.Location = new Point(289, 48);
            dhbox.Name = "dhbox";
            dhbox.Size = new Size(100, 23);
            dhbox.TabIndex = 27;
            dhbox.Text = "FFFFFF";
            dhbox.Visible = false;
            dhbox.Leave += dhbox_Leave;
            // 
            // lhbox
            // 
            lhbox.Location = new Point(289, 8);
            lhbox.Name = "lhbox";
            lhbox.Size = new Size(100, 23);
            lhbox.TabIndex = 28;
            lhbox.Text = "FFFFFF";
            lhbox.Visible = false;
            lhbox.Leave += lhbox_Leave;
            // 
            // colswit
            // 
            colswit.Location = new Point(364, 78);
            colswit.Name = "colswit";
            colswit.Size = new Size(56, 24);
            colswit.TabIndex = 29;
            colswit.Text = "RGB";
            colswit.UseVisualStyleBackColor = true;
            colswit.Visible = false;
            colswit.Click += colswit_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(270, 12);
            label2.Name = "label2";
            label2.Size = new Size(14, 15);
            label2.TabIndex = 30;
            label2.Text = "#";
            label2.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(270, 51);
            label5.Name = "label5";
            label5.Size = new Size(14, 15);
            label5.TabIndex = 31;
            label5.Text = "#";
            label5.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(421, 345);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(colswit);
            Controls.Add(lhbox);
            Controls.Add(dhbox);
            Controls.Add(label1);
            Controls.Add(sizeud);
            Controls.Add(checkBox2);
            Controls.Add(RSB);
            Controls.Add(RSG);
            Controls.Add(RSR);
            Controls.Add(LSB);
            Controls.Add(LSG);
            Controls.Add(LSR);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(richTextBox1);
            Controls.Add(button1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Gif to PGN Converter";
            ((System.ComponentModel.ISupportInitialize)LSR).EndInit();
            ((System.ComponentModel.ISupportInitialize)LSG).EndInit();
            ((System.ComponentModel.ISupportInitialize)LSB).EndInit();
            ((System.ComponentModel.ISupportInitialize)RSB).EndInit();
            ((System.ComponentModel.ISupportInitialize)RSG).EndInit();
            ((System.ComponentModel.ISupportInitialize)RSR).EndInit();
            ((System.ComponentModel.ISupportInitialize)sizeud).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private Label label3;
        private Label label4;
        private NumericUpDown LSR;
        private NumericUpDown LSG;
        private NumericUpDown LSB;
        private NumericUpDown RSB;
        private NumericUpDown RSG;
        private NumericUpDown RSR;
        private CheckBox checkBox2;
        private NumericUpDown sizeud;
        private Label label1;
        private TextBox dhbox;
        private TextBox lhbox;
        private Button colswit;
        private Label label2;
        private Label label5;
    }
}
