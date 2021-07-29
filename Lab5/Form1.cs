/* Author : Siddharth Choudhury
 * Name: Lab 5: NETD 2202
 * Description: Creating a notepad application
 * Date: July 29, 2021
 */
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

namespace Lab5
{
    public partial class FormNotepad : Form
    {
        private string filestr;

        private SaveFileDialog saveFileDialog;

        public FormNotepad()
        {
            InitializeComponent();
            filestr = " ";
        }

        String FileNameCurrent = string.Empty;

        #region Menu Strip Functions 
        private void NewFile()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.textBox1.Text))
                {

                    if (MessageBox.Show("Are you sure you want to open a new file without saving?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        this.textBox1.Text = string.Empty;
                        this.Text = "Untitled";
                    }
                }
                else
                {
                    this.textBox1.Text = string.Empty;
                    this.Text = "Untitled";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error while creating a new file!");
            }
            finally
            {

            }
        }

        private void SaveFile()
        {
            try
            {
                if (filestr == " ")
                {
                    DialogResult dr = saveFileDialog1.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        StreamWriter writefile = new StreamWriter(saveFileDialog1.FileName);
                        writefile.Write(textBox1.Text);
                        writefile.Close();

                        filestr = saveFileDialog1.FileName;
                    }
                }
                else
                {
                    StreamWriter writefile = new StreamWriter(filestr);
                    writefile.Write(textBox1.Text);
                    writefile.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving the file!");
            }
            finally
            {

            }
        }

        private void OpenFile()
        {
            try
            {
                DialogResult dr = openFileDialog1.ShowDialog();
                if(dr == DialogResult.OK)
                {
                    StreamReader read = new StreamReader(openFileDialog1.FileName);
                    textBox1.Text = read.ReadToEnd();
                    read.Close();

                    filestr = openFileDialog1.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error opening the file!");
            }
            finally
            {
                //openFileDialog = null;
            }
        }

        private void SaveFileAs()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.textBox1.Text))
                {
                    saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Text file (*.txt) | *.txt";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog.FileName, this.textBox1.Text);
                        this.Text = saveFileDialog.FileName;
                    }
                }
                else
                {
                    MessageBox.Show("The file you are trying to save has no contents");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving the file!");
            }
            finally
            {

            }
        }

        private void CloseFile()
        {
            if (MessageBox.Show("Are you sure you want to close the currently opened file?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                NewFile();
            }
        }
        private void ExitApplication()
        {
            if (!string.IsNullOrEmpty(this.textBox1.Text))
            {
                ConfirmClose();
                Application.Exit();
            }
            else
            { 
                if (MessageBox.Show("Are you sure you want to exit?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Application.Exit();
                    }
            }
        }

        private void Copy()
        {
            if (textBox1.SelectionLength > 0)
            {
                textBox1.Copy();
            }
        }

        private void Cut()
        {
            if (textBox1.SelectedText != " ")
            {
                textBox1.Cut();
            }
        }

        private void Paste()
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) == true)
            {
                textBox1.Paste();
            }
        }

        private void Font()
        {
            DialogResult dr = fontDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
            }
        }

        private void ConfirmClose()
        {
                if (MessageBox.Show("Would you like to save the changes before closing this file?", "Save Changes", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SaveFile();
                }
        }




        #endregion

        private void FormNotepad_Load(object sender, EventArgs e)
        {

        }

        #region Menu Strip Events



        private void toolStripMenuItemNew_Click_1(object sender, EventArgs e)
        {
            NewFile();
        }

        private void toolStripMenuItemOpen_Click_1(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void toolStripMenuItemSave_Click_1(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void toolStripMenuItemSaveAs_Click_1(object sender, EventArgs e)
        {
            SaveFileAs();
        }

        private void toolStripMenuItemClose_Click_1(object sender, EventArgs e)
        {
            CloseFile();
        }

        private void toolStripMenuItemExit_Click_1(object sender, EventArgs e)
        {
            ExitApplication();
        }

        private void toolStripMenuItemCopy_Click_1(object sender, EventArgs e)
        {
           Copy();
        }

        private void toolStripMenuItemCut_Click_1(object sender, EventArgs e)
        {
           Cut();
        }

        private void toolStripMenuItemPaste_Click_1(object sender, EventArgs e)
        {
            Paste();
        }

        private void toolStripMenuItemFont_Click(object sender, EventArgs e)
        {
            Font();
        }


        private void toolStripMenuItemAbout_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("NETD 2202 \r\nLab 5 \r\nSiddharth Choudhury", "About");
        }

        #endregion

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                toolStripMenuItemCut.Enabled = true;
                toolStripMenuItemCopy.Enabled = true;
            }
            else
            {
                toolStripMenuItemCut.Enabled = false;
                toolStripMenuItemCopy.Enabled = false;
            }
        }
    }
}
