using System.Drawing.Printing;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.Utils.Serializing;

namespace MDIProject
{
    public partial class Form1 : Form
    {
        DialogResult MessageBoxResult;
        string filePath = "";
        string FileNameWithoutExtension;
        bool   IsFileOpened = false;
      
        enum enSystemLan {Eng  = 1 , Arb = 2  , Rus = 3};
        enSystemLan SystemLang = enSystemLan.Eng; 
       
        void ChangeFormTitle(string Sympol = "" , string Title = "")
        {

            this.Text = Sympol + Title;
        }
        void ChangeFormTitleByTxtBoxStatuse()
        {

            if (rchtxtbx.Text != "")
            {
                ChangeFormTitle("*", "NoteBook");

            }
            else
            {
                ChangeFormTitle("", "NoteBook");
            }
        }
        void ChangeFormTitleByFileName()
        {
            ChangeFormTitle("*", FileNameWithoutExtension + " - " + "NoteBook");
        }
        void ShowMessageFileSavedSuccessfully()
        {
            switch (SystemLang)
            {
                case enSystemLan.Eng:
                    MessageBox.Show("File Saved Successfully");
                    break;
                case enSystemLan.Arb:
                    MessageBox.Show("تم حفظ الملف بنجاح");
                    break;
                case enSystemLan.Rus:
                    MessageBox.Show("Файл успешно сохранен");
                break;
            }
        }
        bool SetContorolsEnaibalty(Control control, bool Enaibalty)
        {
            return (control.Enabled == Enaibalty);
        }
        bool SetControlsVisibalty(Control control, bool Visibalty)
        {
            return (control.Visible = Visibalty);
        }
        bool CheckItem(ToolStripMenuItem Item)
        {
            Item.Enabled = true;
            Item.Checked = false;
            return true;
        }
        void ClearTextBoxNotes()
        {
            rchtxtbx.Clear();
            ChangeFormTitleByTxtBoxStatuse();
            filePath = "";
            FileNameWithoutExtension = "";
            IsFileOpened = false;
        }
        void ShowMessageBox(string Message , string Caption)
        {
            MessageBoxResult = MessageBox.Show(Message , Caption , MessageBoxButtons.YesNoCancel , MessageBoxIcon.Question); 

        }
        void MessageBoxResultDailog(DialogResult Result)
        {
            switch (Result)
            {
                case DialogResult.Yes:
                    SaveChangesOnCurrentFileOrNewFile();
                    ClearTextBoxNotes();
                    break;

                case DialogResult.No:
                    ClearTextBoxNotes();
                    return;
                break;

                case DialogResult.Cancel:
                return;
                break;
            }
        }
        void SaveChangesOnCurrentFileOrNewFile()
        {
            if (filePath == "")
            {
                IsFileOpened = CreatNewFileAndSave();
                
                return;
            }
            File.WriteAllText(filePath, rchtxtbx.Text);
            ChangeFormTitle("Saved..*", FileNameWithoutExtension + " - " + "NoteBook");
        }
        bool CreatNewFileAndSave()
        {
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.Title = "Notes";
            saveFileDialog1.Filter = "txt Files (*.txt) | *.txt | All Files (*.*) | *.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileNameWithoutExtension = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
                filePath = saveFileDialog1.FileName;
                File.WriteAllText(filePath, rchtxtbx.Text);
                ShowMessageFileSavedSuccessfully();
                return true;
            }
            return false;

        }
        bool OpenCurrnetTextFile()
        {
            openFileDialog1.Title = "Open Note";
            openFileDialog1.DefaultExt = ""; 
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "txt files (*.txt) | *.txt";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                 filePath = openFileDialog1.FileName;
                 FileNameWithoutExtension =  Path.GetFileNameWithoutExtension(filePath);
                 rchtxtbx.Clear();
                 rchtxtbx.Text = File.ReadAllText(filePath);
                 IsFileOpened = true;
                  return true; 
            }
            return false;
        }
        void SpiltToolsMenuEnablity()
        {
            
            if (rchtxtbx.SelectedText.Length > 0)
            {
               cutToolStripMenuItem.Enabled  = true;
               copyToolStripMenuItem.Enabled = true;

                CopyButtonOfEditeToolStrip.Enabled = true;
                LeftToRightButtonOfEditeToolStrip.Enabled = true;
                RightToLeftButtonOfEditeToolStrip.Enabled = true;
            }
            else
            {
               cutToolStripMenuItem.Enabled  = false;
               copyToolStripMenuItem.Enabled = false;

                CopyButtonOfEditeToolStrip.Enabled = false;
                LeftToRightButtonOfEditeToolStrip.Enabled = false;
                RightToLeftButtonOfEditeToolStrip.Enabled = false;

            }
            saveToolStripMenuItem.Enabled = true; 
            return; 

        }

        // system lang resuorces
        void LoadAribclang()
        {

            this.RightToLeft = RightToLeft.Yes;

            fileToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Files;
            newToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.New;
            openToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Open;
            saveToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Save;
            printToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Print;
            
            exitToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Exite;

            // Edite Menu 
            editToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Edite;
            undoToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Undo;
            redoToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Redo_;
            cutToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Cut;
            copyToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Copy;
            pasteToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Paste;
            selectAllToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.SelectAll;

            // Tools Menu 
            toolsToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Tools;
            shortmenuToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Short_Menu_;
            languagesToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Languages;
            helpToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Helps;
            themToolStripMenuItem.Text = Properties.ResourecesLang.ArabicLang.Theme;



        }
        void LoadEnglishLang()
        {
            this.RightToLeft = RightToLeft.No;
            fileToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Files;
            newToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.New;
            openToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Open;
            saveToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Save;
            printToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Print;
           
            exitToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Exite;

            // Edite Menu 
            editToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Edite;
            undoToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Undo;
            redoToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Redo_;
            cutToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Cut;
            copyToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Copy;
            pasteToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Paste;
            selectAllToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.SelectAll;

            // Tools Menu 
            toolsToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Tools;
            shortmenuToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Short_Menu_;
            languagesToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Languages;
            helpToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Helps;
            themToolStripMenuItem.Text = Properties.ResourecesLang.Englishlang.Theme;
        }
        void LoadRussianLang()
        {
            this.RightToLeft = RightToLeft.No;
            fileToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Files;
            newToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.New;
            openToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Open;
            saveToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Save;
            printToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Print;
            exitToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Exite;

            // Edite Menu 
            editToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Edite;
            undoToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Undo;
            redoToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Redo_;
            cutToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Cut;
            copyToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Copy;
            pasteToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Paste;
            selectAllToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.SelectAll;

            // Tools Menu 
            toolsToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Tools;
            shortmenuToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Short_Menu_;
            languagesToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Languages;
            helpToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Helps;
            themToolStripMenuItem.Text = Properties.ResourecesLang.RussianLang.Theme;
        }
        //-------------------

        // system Theme ground 
        void LightTheme()
        {
            

            rchtxtbx.BackColor = Color.FromArgb(255, 255, 255);
            rchtxtbx.ForeColor = Color.FromArgb(0, 0, 0); 

            StripMenuEdite.BackColor = Color.FromArgb(255, 255, 255);
            StripMenuEdite.ForeColor = Color.FromArgb(255, 255, 255);

            MenuStrip1.BackColor = Color.FromArgb(255, 255, 255); ;
            foreach (ToolStripMenuItem item in MenuStrip1.Items)
            {
                item.BackColor = Color.FromArgb(255, 255, 255);
                item.ForeColor = Color.FromArgb(0, 0, 0);
            }
        }
        void NightTheme()
        {

            
          
           
            rchtxtbx.BackColor = Color.FromArgb(79, 74, 108);
            rchtxtbx.ForeColor = Color.FromArgb(255, 255, 255, 255); 

            StripMenuEdite.BackColor = Color.FromArgb(240, 240, 240);
            StripMenuEdite.ForeColor = Color.FromArgb(255, 255, 255);

            MenuStrip1.BackColor = Color.FromArgb(165, 158, 188); ;
            foreach (ToolStripMenuItem item in MenuStrip1.Items)
            {
                item.BackColor = Color.FromArgb(165, 158, 188);
            }

        }
        //--------------

        public Form1()
        {
            InitializeComponent();


        }

        private void rchtxtbx_SelectionChanged(object sender, EventArgs e)
        {
            rchtxtbx.Refresh();
            rchtxtbx.Update();

            if (IsFileOpened)
            {
                ChangeFormTitleByFileName();

            }
            else
            {
                ChangeFormTitleByTxtBoxStatuse();
            }

               SpiltToolsMenuEnablity();


        }

        private void ShortMenuTool_Click(object sender, EventArgs e)
        {
            shortmenuToolStripMenuItem.Checked = !shortmenuToolStripMenuItem.Checked;

            if (shortmenuToolStripMenuItem.Checked)
            {
                SetControlsVisibalty(StripMenuEdite, true);
            }
            else
            {
                SetControlsVisibalty(StripMenuEdite, false);
            }
        }
        private void ProjectLanguage_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
            ((ToolStripMenuItem)sender).Enabled = false;

            if (((ToolStripMenuItem)sender).Tag == "Eng")
            {
                SystemLang = enSystemLan.Eng; 
                CheckItem(arabiclangItem);
                CheckItem(russlangItem);
                LoadEnglishLang();
                return;
            }
            if (((ToolStripMenuItem)sender).Tag == "Arabic")
            {
                
                SystemLang = enSystemLan.Arb; 
                CheckItem(englishlangItem);
                CheckItem(russlangItem);
                LoadAribclang();
                return;
            }
            if (((ToolStripMenuItem)sender).Tag == "Rus")
            {
                SystemLang = enSystemLan.Rus;

                CheckItem(englishlangItem);
                CheckItem(arabiclangItem);
                LoadRussianLang();
                return;

            }

            return;

        }
        private void ProjectThemeMoode_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !((ToolStripMenuItem)sender).Checked;
            ((ToolStripMenuItem)sender).Enabled = false;

            if (((ToolStripMenuItem)sender).Tag == "NightMoode")
            {
                CheckItem(lightToolStripMenuItem);
                 NightTheme();
                 return;

            }
            if (((ToolStripMenuItem)sender).Tag == "LightMoode")
            {
                CheckItem(nightToolStripMenuItem);
                LightTheme();
                return;

            }
           

        }
        private void NewTextFile_Click(object sender, EventArgs e)
        {
            if(rchtxtbx.Text != "" && SystemLang == enSystemLan.Eng)
            {
                ShowMessageBox("Would You Like To Save Changes !! " , "Notebook");
                MessageBoxResultDailog(MessageBoxResult);
                
                return;
            }
            if (rchtxtbx.Text != "" && SystemLang == enSystemLan.Rus)
            {
                ShowMessageBox("Хотите ли вы сохранить изменения?", "Блокнот");
                MessageBoxResultDailog(MessageBoxResult);
                return;
            }
            if (rchtxtbx.Text != "" && SystemLang == enSystemLan.Arb)
            {
                ShowMessageBox("هل ترغب في حفظ التغييرات؟", "دفتر الملاحظات");
                MessageBoxResultDailog(MessageBoxResult);
                return;
            }
            
        }
        private void OpenTextFile_Click(object sender, EventArgs e)
        {
             
             NewTextFile_Click(sender, e);
             IsFileOpened = OpenCurrnetTextFile();
              ChangeFormTitle("", FileNameWithoutExtension + " - " + "NoteBook");
        }
        private void SaveTextFile_Click(object sender, EventArgs e)
        {
            SaveChangesOnCurrentFileOrNewFile();
        }
        private void UndoText_Click(object sender, EventArgs e)
        {
            rchtxtbx.Undo();

        }
        private void RedoText_Click(object sender, EventArgs e)
        {
            rchtxtbx.Redo();
        }
        private void CutText_Click(object sender, EventArgs e)
        {
            rchtxtbx.Cut();
        }
        private void CopyText_Click(object sender, EventArgs e)
        {

            rchtxtbx.Copy();
        }
        private void PasteText_Click(object sender, EventArgs e)
        {

            rchtxtbx.Paste();
        }
        private void SelecetAllText_Click(object sender, EventArgs e)
        {
            rchtxtbx.SelectAll();
        }
        private void PrintTextDocumnet_Click(object sender, EventArgs e)
        {
            printDialog1.ShowDialog(this);
        }
        private void TextFont_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = false;
            fontDialog1.ShowApply = false;
            fontDialog1.ShowHelp  = false;
            fontDialog1.ShowEffects = false; 

            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                rchtxtbx.Font = fontDialog1.Font;
                ; 
                return; 
            }
        }
        private void AlignmentTextToLeft_Click(object sender, EventArgs e)
        {
            rchtxtbx.SelectionAlignment = HorizontalAlignment.Left; 
        }
        private void AlignmentTextToRight_Click(object sender, EventArgs e)
        {
            rchtxtbx.SelectionAlignment = HorizontalAlignment.Right;
        }
        private void ClearText_Click(object sender, EventArgs e)
        {
            rchtxtbx.ClearUndo(); 
            rchtxtbx.Clear(); 
        }
        private void TextForeColor_Click(object sender, EventArgs e)
        {
             colorDialog1.ShowHelp = false;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                rchtxtbx.ForeColor = colorDialog1.Color;

            }
        }

       
    }
}

