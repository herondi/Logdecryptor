using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace LogDecryptor
{
    public partial class Form1 : Form
    {
        //new
        string filePath;

        public Form1()
        {
            InitializeComponent();
        }
        //new
        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = LoadFile.LoadLog();
            this.filePath = filePath;
            bool isSearch = false;
            string textToSearch = "";
            fillDataGrid(filePath, isSearch, textToSearch);
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //here my friend 
  
        public void fillDataGrid(string filePath, bool isSearch, string textToSearch)
        {
            //Clear dataGrid
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();

            int setPath = TestString(filePath);
            if (setPath == 0)
            {

                filePath = filePath.Replace(@"\", @"/");
                int counter = 0;
                groupBox1.Text = filePath;

                foreach (string line in System.IO.File.ReadLines(@filePath))
                {
                    if (!String.IsNullOrEmpty(line))
                    {

                        string[] toDecrypt = line.Split('|');
                        string decrypted = Cryptography.DecryptDes(toDecrypt[3]);
                        
                        if (isSearch == false) {
                            dataGridView1.Rows.Add(toDecrypt[0] + "     ***     " + toDecrypt[1] + "     ***     " + toDecrypt[2] + "     ***     " + decrypted + "     ***     " + toDecrypt[3].Substring(toDecrypt[3].Length - 10, 10));
                        }
                        else if(isSearch == true)
                        {
                            //search part of the string

                            if (TestStringCasesensitive(decrypted, textToSearch)
                                | TestStringCasesensitive(toDecrypt[0], textToSearch)
                                | TestStringCasesensitive(toDecrypt[1], textToSearch)
                                | TestStringCasesensitive(toDecrypt[2], textToSearch)
                                | TestStringCasesensitive(toDecrypt[3], textToSearch))


                            {
                                dataGridView1.Rows.Add(toDecrypt[0] + "     ***     " + toDecrypt[1] + "     ***     " + toDecrypt[2] + "     ***     " + decrypted + "     ***     " + toDecrypt[3].Substring(toDecrypt[3].Length - 10, 10));                             
                            }
                            else
                            {
                                //dataGridView1.Rows.Add("     ***     ");
                            }
                           
                        }
                    }

                    counter++;
                }

            }
            else {
                    dataGridView1.Rows.Add("Selecione um arquivo criptografado de logs");
             }

        }

        private bool TestStringCasesensitive(string decrypted, string textToSearch)
        {
            bool contains = decrypted.IndexOf(textToSearch, StringComparison.OrdinalIgnoreCase) >= 0;
            return contains;
        }

        int TestString(string s)
        {
            if (String.IsNullOrEmpty(s))
                return 1;
            else
                return 0;
        }
        //new
        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = this.filePath;
            bool isSearch = true;
            string textToSearch = textBox1.Text;
            fillDataGrid(filePath, isSearch, textToSearch);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
