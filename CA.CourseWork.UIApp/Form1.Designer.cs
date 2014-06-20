namespace CA.CourseWork.UIApp
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
            this.inputTextArea = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cryptoType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.isParallel = new System.Windows.Forms.CheckBox();
            this.log = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.encodeButton = new System.Windows.Forms.Button();
            this.decodeButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.keyTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.keyLengthTextbox = new System.Windows.Forms.TextBox();
            this.generateKey = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.inputLengthTextbox = new System.Windows.Forms.TextBox();
            this.generateInput = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputTextArea
            // 
            this.inputTextArea.Location = new System.Drawing.Point(12, 37);
            this.inputTextArea.Multiline = true;
            this.inputTextArea.Name = "inputTextArea";
            this.inputTextArea.Size = new System.Drawing.Size(197, 198);
            this.inputTextArea.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Входные данные";
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(524, 37);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.Size = new System.Drawing.Size(197, 391);
            this.outputTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(524, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Выходные данные";
            // 
            // cryptoType
            // 
            this.cryptoType.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cryptoType.FormattingEnabled = true;
            this.cryptoType.Items.AddRange(new object[] {
            "ГОСТ 28147-89",
            "DES",
            "AES128"});
            this.cryptoType.Location = new System.Drawing.Point(232, 46);
            this.cryptoType.Name = "cryptoType";
            this.cryptoType.Size = new System.Drawing.Size(278, 33);
            this.cryptoType.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(243, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(233, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Алгоритм шифрования";
            // 
            // isParallel
            // 
            this.isParallel.AutoSize = true;
            this.isParallel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.isParallel.Location = new System.Drawing.Point(232, 85);
            this.isParallel.Name = "isParallel";
            this.isParallel.Size = new System.Drawing.Size(286, 29);
            this.isParallel.TabIndex = 6;
            this.isParallel.Text = "Параллельные вычисления";
            this.isParallel.UseVisualStyleBackColor = true;
            // 
            // log
            // 
            this.log.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.log.Location = new System.Drawing.Point(232, 238);
            this.log.Multiline = true;
            this.log.Name = "log";
            this.log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(278, 141);
            this.log.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(227, 210);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "LOG:";
            // 
            // encodeButton
            // 
            this.encodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.encodeButton.Location = new System.Drawing.Point(232, 120);
            this.encodeButton.Name = "encodeButton";
            this.encodeButton.Size = new System.Drawing.Size(278, 39);
            this.encodeButton.TabIndex = 9;
            this.encodeButton.Text = "Зашифровать";
            this.encodeButton.UseVisualStyleBackColor = true;
            this.encodeButton.Click += new System.EventHandler(this.encodeButton_Click);
            // 
            // decodeButton
            // 
            this.decodeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.decodeButton.Location = new System.Drawing.Point(232, 166);
            this.decodeButton.Name = "decodeButton";
            this.decodeButton.Size = new System.Drawing.Size(278, 41);
            this.decodeButton.TabIndex = 10;
            this.decodeButton.Text = "Расшифровать";
            this.decodeButton.UseVisualStyleBackColor = true;
            this.decodeButton.Click += new System.EventHandler(this.decodeButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearButton.Location = new System.Drawing.Point(232, 385);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(278, 43);
            this.clearButton.TabIndex = 11;
            this.clearButton.Text = "Очистить лог";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // keyTextBox
            // 
            this.keyTextBox.Location = new System.Drawing.Point(12, 269);
            this.keyTextBox.Multiline = true;
            this.keyTextBox.Name = "keyTextBox";
            this.keyTextBox.Size = new System.Drawing.Size(197, 159);
            this.keyTextBox.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(17, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 25);
            this.label5.TabIndex = 13;
            this.label5.Text = "Ключ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(12, 440);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "Длина ключа";
            // 
            // keyLengthTextbox
            // 
            this.keyLengthTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.keyLengthTextbox.Location = new System.Drawing.Point(154, 437);
            this.keyLengthTextbox.Name = "keyLengthTextbox";
            this.keyLengthTextbox.Size = new System.Drawing.Size(100, 30);
            this.keyLengthTextbox.TabIndex = 15;
            // 
            // generateKey
            // 
            this.generateKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.generateKey.Location = new System.Drawing.Point(80, 473);
            this.generateKey.Name = "generateKey";
            this.generateKey.Size = new System.Drawing.Size(174, 34);
            this.generateKey.TabIndex = 16;
            this.generateKey.Text = "Сгенерировать";
            this.generateKey.UseVisualStyleBackColor = true;
            this.generateKey.Click += new System.EventHandler(this.generateKey_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(260, 440);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(220, 25);
            this.label7.TabIndex = 17;
            this.label7.Text = "Количество символов";
            // 
            // inputLengthTextbox
            // 
            this.inputLengthTextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputLengthTextbox.Location = new System.Drawing.Point(487, 440);
            this.inputLengthTextbox.Name = "inputLengthTextbox";
            this.inputLengthTextbox.Size = new System.Drawing.Size(100, 30);
            this.inputLengthTextbox.TabIndex = 18;
            // 
            // generateInput
            // 
            this.generateInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.generateInput.Location = new System.Drawing.Point(417, 473);
            this.generateInput.Name = "generateInput";
            this.generateInput.Size = new System.Drawing.Size(170, 34);
            this.generateInput.TabIndex = 19;
            this.generateInput.Text = "Сгенерировать";
            this.generateInput.UseVisualStyleBackColor = true;
            this.generateInput.Click += new System.EventHandler(this.generateInput_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 514);
            this.Controls.Add(this.generateInput);
            this.Controls.Add(this.inputLengthTextbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.generateKey);
            this.Controls.Add(this.keyLengthTextbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.keyTextBox);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.decodeButton);
            this.Controls.Add(this.encodeButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.log);
            this.Controls.Add(this.isParallel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cryptoType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.inputTextArea);
            this.Name = "Form1";
            this.Text = "Computer Architecture. Course Work. Cryptography Algorithms";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputTextArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cryptoType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox isParallel;
        private System.Windows.Forms.TextBox log;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button encodeButton;
        private System.Windows.Forms.Button decodeButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.TextBox keyTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox keyLengthTextbox;
        private System.Windows.Forms.Button generateKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox inputLengthTextbox;
        private System.Windows.Forms.Button generateInput;
    }
}

