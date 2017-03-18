using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;
using Aladdin.HASP;


namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ToolStripMenuItem fileItem = new ToolStripMenuItem("File");
            menuStrip1.Items.Add(fileItem);

            ToolStripMenuItem openFile = new ToolStripMenuItem("Open file");
            openFile.Click += button1_Click;
            fileItem.DropDownItems.Add(openFile);

            ToolStripMenuItem sortFile = new ToolStripMenuItem("Sort file");
            openFile.Click += button2_Click;
            fileItem.DropDownItems.Add(sortFile);

            ToolStripMenuItem aboutItem = new ToolStripMenuItem("Help");
            aboutItem.Click += superFunction_Click;
            menuStrip1.Items.Add(aboutItem);
        }

        void superFunction_Click(object sender, EventArgs e)
        {
            SoundPlayer sp = new SoundPlayer(@"D:\joy.wav");
            sp.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path_to_file = "";

            OpenFileDialog opf = new OpenFileDialog();

            DialogResult dr = opf.ShowDialog();
            if (dr == DialogResult.OK)
            {
                path_to_file = opf.FileName;

                StreamReader sr = new StreamReader(path_to_file, Encoding.Default);
                string str = "";

                str = sr.ReadToEnd();

                richTextBox1.Text = str;
                sr.Close();
            }
            else if (dr == DialogResult.Cancel)
            {
                // to do nothing
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] textArray = richTextBox1.Text.Split(new char[] { ' ', '\n', '.', ',', ':', '\\' }, StringSplitOptions.RemoveEmptyEntries);

            // Checking a word for special symbols
            foreach (string word in textArray)
            {
                byte[] b = Encoding.Default.GetBytes(word);

                if (b[0] > 32 && b[0] < 48 ||
                    b[0] > 57 && b[0] < 65 ||
                    b[0] > 90 && b[0] < 97)
                {
                    StreamWriter sw = File.AppendText("D:\\output.txt");
                    sw.WriteLine(word + '\n');
                    sw.Close();
                }
            }

            // Checking a word for numbers
            foreach (string word in textArray)
            {
                byte[] b = Encoding.Default.GetBytes(word);
                
                if (b[0] > 47 && b[0] < 58)
                {
                    StreamWriter sw = File.AppendText("D:\\output.txt");
                    sw.WriteLine(word + '\n');
                    sw.Close();
                }
            }

            // Checking a word for russian
            foreach (string word in textArray)
            {
                byte[] b = Encoding.Default.GetBytes(word);

                if (b[0] > 191 && b[0] <= 255)
                {
                    StreamWriter sw = File.AppendText("D:\\output.txt");
                    sw.WriteLine(word + '\n');
                    sw.Close();
                }
            }

            // Checking a word for english
            foreach (string word in textArray)
            {
                byte[] b = Encoding.Default.GetBytes(word);

                if (b[0] > 64 && b[0] <= 91 ||
                    b[0] > 96 && b[0] < 123)
                {
                    StreamWriter sw = File.AppendText("D:\\output.txt");
                    sw.WriteLine(word + '\n');
                    sw.Close();
                }
            }

            MessageBox.Show("Sorting completed");
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
