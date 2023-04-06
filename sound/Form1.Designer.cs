namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_play = new System.Windows.Forms.Button();
            this.text_box_path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.input_duration_rate = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label_info = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.input_duration_rate)).BeginInit();
            this.SuspendLayout();
            // 
            // button_play
            // 
            this.button_play.Location = new System.Drawing.Point(576, 30);
            this.button_play.Name = "button_play";
            this.button_play.Size = new System.Drawing.Size(159, 56);
            this.button_play.TabIndex = 0;
            this.button_play.Text = "Обработать и воспроизвести";
            this.button_play.UseVisualStyleBackColor = true;
            this.button_play.Click += new System.EventHandler(this.button_play_Click);
            // 
            // text_box_path
            // 
            this.text_box_path.Location = new System.Drawing.Point(81, 45);
            this.text_box_path.Name = "text_box_path";
            this.text_box_path.Size = new System.Drawing.Size(363, 26);
            this.text_box_path.TabIndex = 1;
            this.text_box_path.Text = "sound";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(142, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Название файла:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(597, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Во сколько раз увеличить (отрицательное число - во сколько раз укоротить)";
            // 
            // input_duration_rate
            // 
            this.input_duration_rate.Location = new System.Drawing.Point(81, 171);
            this.input_duration_rate.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.input_duration_rate.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            this.input_duration_rate.Name = "input_duration_rate";
            this.input_duration_rate.Size = new System.Drawing.Size(120, 26);
            this.input_duration_rate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 251);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Информация:";
            // 
            // label_info
            // 
            this.label_info.AutoSize = true;
            this.label_info.Location = new System.Drawing.Point(49, 296);
            this.label_info.Name = "label_info";
            this.label_info.Size = new System.Drawing.Size(0, 20);
            this.label_info.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_info);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.input_duration_rate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.text_box_path);
            this.Controls.Add(this.button_play);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.input_duration_rate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_play;
        private System.Windows.Forms.TextBox text_box_path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown input_duration_rate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_info;
    }
}

