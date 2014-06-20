using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CA.CourseWork.Crypto;
using CA.CourseWork.Crypto.Interfaces;

namespace CA.CourseWork.UIApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void encodeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(inputTextArea.Text) || String.IsNullOrEmpty(keyTextBox.Text))
                {
                    throw new Exception("Не заполнены поля!");
                }
                IEncryptable encoder = new AESCryptoEncoder();
                switch ((String)cryptoType.SelectedItem)
                {
                    case "DES":
                        encoder = new DESCryptoEncoder();
                        log.Text += String.Format("Алгоритм DES c входными данными длиной {0} байт выполнился за ",
                            inputTextArea.Text.Length*2);
                        break;
                    case "AES128":
                        encoder = new AESCryptoEncoder();
                        log.Text += String.Format("Алгоритм AES128 c входными данными длиной {0} байт выполнился за ",
                            inputTextArea.Text.Length*2);
                        break;
                    case "ГОСТ 28147-89":
                        encoder = new GOSTCryptoEncoder();
                        log.Text += String.Format("Алгоритм ГОСТ 28147-89 c входными данными длиной {0} байт выполнился за ",
                            inputTextArea.Text.Length*2);
                        break;
                    default:
                        throw new Exception("Алгоритм не выбран!");
                }

                var timer = new Stopwatch();
                timer.Start();
                outputTextBox.Text = encoder.Encode(inputTextArea.Text, keyTextBox.Text, isParallel.Checked);
                timer.Stop();
                log.Text += String.Format("{0} ms.{1}", timer.ElapsedMilliseconds, Environment.NewLine);
            }
            catch (Exception exception)
            {
                log.Text += "ERROR!" + Environment.NewLine;
                MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void decodeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(inputTextArea.Text) || String.IsNullOrEmpty(keyTextBox.Text))
                {
                    throw new Exception("Не заполнены поля!");
                }
                IDecryptable decoder = new AESCryptoDecoder();
                switch ((String)cryptoType.SelectedItem)
                {
                    case "DES":
                        decoder = new DESCryptoDecoder();
                        log.Text += String.Format("Алгоритм DES c входными данными длиной {0} байт выполнился за ",
                            inputTextArea.Text.Length * 2);
                        break;
                    case "AES128":
                        decoder = new AESCryptoDecoder();
                        log.Text += String.Format("Алгоритм AES128 c входными данными длиной {0} байт выполнился за ",
                            inputTextArea.Text.Length * 2);
                        break;
                    case "ГОСТ 28147-89":
                        decoder = new GOSTCryptoDecoder();
                        log.Text += String.Format("Алгоритм ГОСТ 28147-89 c входными данными длиной {0} байт выполнился за ",
                            inputTextArea.Text.Length * 2);
                        break;
                    default:
                        throw new Exception("Алгоритм не выбран!");
                }

                var timer = new Stopwatch();
                timer.Start();
                outputTextBox.Text = decoder.Decode(inputTextArea.Text, keyTextBox.Text, isParallel.Checked);
                timer.Stop();
                log.Text += String.Format("{0} ms.{1}", timer.ElapsedMilliseconds, Environment.NewLine);
            }
            catch (Exception exception)
            {
                log.Text += "ERROR!" + Environment.NewLine;
                MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            log.Text = String.Empty;
        }

        private void generateKey_Click(object sender, EventArgs e)
        {
            try
            {
                int length = int.Parse(keyLengthTextbox.Text);
                
                var buffer = new byte[length];

                var random = new Random();
                for (int i = 0; i < length; i++)
                {
                    random.NextBytes(buffer);    
                }

                keyTextBox.Text = Encoding.Unicode.GetString(buffer);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void generateInput_Click(object sender, EventArgs e)
        {
            try
            {
                int length = int.Parse(inputLengthTextbox.Text);

                var buffer = new byte[length];

                var random = new Random();
                for (int i = 0; i < length; i++)
                {
                    random.NextBytes(buffer);
                }

                inputTextArea.Text = Encoding.Unicode.GetString(buffer);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
