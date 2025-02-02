namespace WinFormTest
{
    partial class Backup
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
            checkBox1 = new CheckBox();
            groupBox1 = new GroupBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox5 = new CheckBox();
            checkBox6 = new CheckBox();
            checkBox7 = new CheckBox();
            trackBar1 = new TrackBar();
            label1 = new Label();
            groupBox2 = new GroupBox();
            button1 = new Button();
            dataGridView1 = new DataGridView();
            button2 = new Button();
            button3 = new Button();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(28, 36);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(55, 19);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "MON";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(checkBox7);
            groupBox1.Controls.Add(checkBox6);
            groupBox1.Controls.Add(checkBox5);
            groupBox1.Controls.Add(checkBox4);
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(checkBox2);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Location = new Point(36, 431);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(622, 76);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Backup Day";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(89, 36);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(46, 19);
            checkBox2.TabIndex = 1;
            checkBox2.Text = "TUE";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(141, 36);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(52, 19);
            checkBox3.TabIndex = 2;
            checkBox3.Text = "WED";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(199, 36);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(49, 19);
            checkBox4.TabIndex = 3;
            checkBox4.Text = "THU";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(254, 36);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(42, 19);
            checkBox5.TabIndex = 4;
            checkBox5.Text = "FRI";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(302, 36);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(47, 19);
            checkBox6.TabIndex = 5;
            checkBox6.Text = "SAT";
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(355, 36);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(50, 19);
            checkBox7.TabIndex = 6;
            checkBox7.Text = "SUN";
            checkBox7.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(73, 130);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(299, 45);
            trackBar1.TabIndex = 2;
            trackBar1.Scroll += trackBar1_Scroll;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(173, 99);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 3;
            label1.Text = "chunk size";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBox2);
            groupBox2.Controls.Add(textBox1);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(trackBar1);
            groupBox2.Location = new Point(33, 24);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(454, 360);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Backup Option";
            // 
            // button1
            // 
            button1.Location = new Point(253, 91);
            button1.Name = "button1";
            button1.Size = new Size(46, 30);
            button1.TabIndex = 6;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(682, 24);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(402, 512);
            dataGridView1.TabIndex = 5;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // button2
            // 
            button2.Location = new Point(411, 29);
            button2.Name = "button2";
            button2.Size = new Size(98, 30);
            button2.TabIndex = 7;
            button2.Text = "AUTORUN";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(555, 29);
            button3.Name = "button3";
            button3.Size = new Size(46, 30);
            button3.TabIndex = 8;
            button3.Text = "button3";
            button3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(31, 37);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 7;
            label2.Text = "label2";
            label2.Click += this.label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(31, 65);
            label3.Name = "label3";
            label3.Size = new Size(39, 15);
            label3.TabIndex = 8;
            label3.Text = "label3";
            label3.Click += label3_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(76, 34);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(356, 23);
            textBox1.TabIndex = 9;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(76, 62);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(356, 23);
            textBox2.TabIndex = 10;
            // 
            // Backup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1113, 557);
            Controls.Add(dataGridView1);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Backup";
            Text = "Backup";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private CheckBox checkBox1;
        private GroupBox groupBox1;
        private CheckBox checkBox7;
        private CheckBox checkBox6;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private TrackBar trackBar1;
        private Label label1;
        private GroupBox groupBox2;
        private Button button1;
        private DataGridView dataGridView1;
        private Button button3;
        private Button button2;
        private Label label3;
        private Label label2;
        private TextBox textBox2;
        private TextBox textBox1;
    }
}