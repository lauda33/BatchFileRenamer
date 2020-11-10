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
using System.Diagnostics;

namespace FileRenamer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string filePath;
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                filePath = folderDialog.SelectedPath.ToString();
            }
            textBox1.Text = filePath;

        }

        private void textKeyword_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string keyword;
            int seed = 0;
            bool seedDefined = false;
            keyword = textKeyword.Text;
            try
            {
                seed = Convert.ToInt32(textSeed.Text);
                seedDefined = true;

            }
            catch
            {
                MessageBox.Show("Please write an integer number as seed.");
                textSeed.Clear();
            }

                if (keyword != "" && seedDefined != false && textBox1.Text != "")
                {

                    // First, we'll clean the existed options.json
                    if (File.Exists("options.json"))
                    {
                        File.Delete("options.json");
                    }                    

                    // Now we'll declare a new FileStream in order to create new options.json
                    FileStream fs = new FileStream(@"options.json",FileMode.OpenOrCreate,FileAccess.Write);

                    StreamWriter sw = new StreamWriter(fs);

                    String dict = "";


                    // This is the dict type of python. We will read this in python
                    Array filePathParts;

                    filePathParts = filePath.Split('\\');

                    string newFilePath = "";

                    foreach(string filePathPart in filePathParts) {

                        newFilePath = newFilePath + filePathPart + @"\\";
                    }

                    dict = "{"+ '"' + "keyword" +'"'+":"+ '"'+keyword +'"' + ","+'"'+"seed"+'"'+ ":" + seed.ToString() + ","+ '"'+ "path" +'"' + ":" + '"' + newFilePath +  '"' +"}";



                   sw.WriteLine(dict);

                    // We saved the file.
                    sw.Flush();

                    // We closed the StreamWriter

                    sw.Close();



                    // Now let's start the python script.
                    Process pr = new Process();
                    pr.StartInfo.FileName = "renamer.pyw";
                    pr.Start();

                    // Process finished message
                    MessageBox.Show("Your files succesfully renamed! ");

                }

                else
                {
                    MessageBox.Show("Please give all the infos correctly");
                }
 

    



            
        }
    }
}
