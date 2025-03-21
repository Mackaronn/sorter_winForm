using Microsoft.VisualBasic.ApplicationServices;
using sorter_winForm.Properties;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.Design.AxImporter;
namespace sorter_winForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int numItem;
        List<string> rankList = new List<string>();
        List<string> allItemList = new List<string>();
        TextBox textBox = new TextBox();
        Point range;
        Point last_range;
        string path_directory;
        string path_result_directory;
        string name_last_file;
        string name_first_file;
        Label lable1 = new Label();
        Label lable2 = new Label();
        bool first_sort = false;
        string name_save = "save.txt";
        bool IsContinue = false;
        string image_save;

        private void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(860, 640);
            MainMenu();
        }
        private void Sorting()
        {
            Button btn1 = new Button();
            Button btn2 = new Button();
            SplitContainer splitContainer1;
            SplitContainer splitContainer2;
            PictureBox pictureBox1;
            PictureBox pictureBox2;

            Controls.Clear();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            // splitContainer1
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            splitContainer1.Size = new Size(ClientSize.Width, ClientSize.Height);
            splitContainer1.SplitterDistance = (int)((float)8.5f / 10f * ClientSize.Height);
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.IsSplitterFixed = true;
            Controls.Add(splitContainer1);
            // splitContainer1.Panel1
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // splitContainer1.Panel2
            splitContainer1.Panel2.Controls.Add(tableLayoutPanel);
            // splitContainer2
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Size = new Size(ClientSize.Width, splitContainer1.SplitterDistance);
            splitContainer2.SplitterDistance = ClientSize.Width / 2;
            splitContainer2.BorderStyle = BorderStyle.Fixed3D;
            splitContainer2.IsSplitterFixed = true;
            // splitContainer2.Panel1
            splitContainer2.Panel1.Controls.Add(pictureBox1);
            splitContainer2.Panel1.Controls.Add(lable1);
            // splitContainer2.Panel2
            splitContainer2.Panel2.Controls.Add(pictureBox2);
            splitContainer2.Panel2.Controls.Add(lable2);
            // pictureBox1
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Name = "picture1";
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabStop = false;
            // pictureBox2
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Name = "picture2";
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabStop = false;
            //tableLayoutPanel
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tableLayoutPanel.Controls.Add(btn1, 0, 0);
            tableLayoutPanel.Controls.Add(btn2, 1, 0);
            tableLayoutPanel.RowCount = 1;
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.Dock = DockStyle.Fill;
            // button1
            btn1.Anchor = AnchorStyles.Top;
            btn1.Size = new Size(splitContainer1.Panel2.Width / 4, (int)(3f / 4f * splitContainer1.Panel2.Height));
            btn1.Location = new Point(splitContainer1.Panel2.Width / 4 - btn1.Width / 2, splitContainer1.Panel2.Height / 2 - btn1.Height / 2);//new Point(100, splitContainer1.SplitterDistance + splitContainer1.Panel2.Height / 2 - btn1.Height / 2);
            btn1.Name = "button1";
            btn1.TabIndex = 0;
            btn1.Text = "Назад";
            btn1.UseVisualStyleBackColor = true;
            btn1.MouseDown += (obj, e) => RemoveButton(pictureBox1, pictureBox2, e);
            // button2
            btn2.Anchor = AnchorStyles.Top;
            btn2.Size = new Size(splitContainer1.Panel2.Width / 4, (int)(3f / 4f * splitContainer1.Panel2.Height));
            btn2.Location = new Point(splitContainer1.Location.X + splitContainer1.Panel2.Width / 2 - btn2.Width / 2 - splitContainer1.Panel2.Width / 3, splitContainer1.Location.Y + splitContainer1.Panel2.Height / 2 - btn2.Height / 2 - splitContainer1.Panel2.Height / 3);
            btn2.Name = "button2";
            btn2.TabIndex = 1;
            btn2.Text = "Сохранить";
            btn2.UseVisualStyleBackColor = true;
            btn2.MouseDown += (obj, e) => SaveButton(pictureBox2,e);
            // textBox1
            lable1.Size = new Size((int)(splitContainer2.Panel1.Width * 0.9f), (int)(1f/10f * splitContainer2.Panel1.Height));
            lable1.Location = new Point(splitContainer2.Panel1.Width / 2 - lable1.Size.Width / 2, splitContainer2.Panel1.Height / 2 - 3 * lable1.Size.Height);
            lable1.Anchor = AnchorStyles.Top;
            // textBox2
            lable2.Size = new Size((int)(splitContainer2.Panel2.Width * 0.9f), (int)(1f / 10f * splitContainer2.Panel2.Height));
            lable2.Location = new Point(splitContainer2.Panel2.Width / 2 - lable2.Size.Width / 2, splitContainer2.Panel2.Height / 2 - 3 * lable2.Size.Height);
            lable2.Anchor = AnchorStyles.Top;
            //setImage
            if (IsContinue)
            {
                SetMidleImage(pictureBox1);
                SetImage(pictureBox2, image_save);
            }
            else
            {
                SetRandomImage(pictureBox1);
                SetRandomImage(pictureBox2);
                name_first_file = pictureBox1.Text;
            }
            //
            splitContainer2.Panel1.MouseClick += (obj, e) => PressImage1(pictureBox1, pictureBox2, e);
            splitContainer2.Panel2.MouseClick += (obj, e) => PressImage2(pictureBox2, pictureBox1, e);
            pictureBox1.MouseClick += (obj, e) => PressImage1(pictureBox1, pictureBox2, e);
            pictureBox2.MouseClick += (obj, e) => PressImage2(pictureBox2, pictureBox1, e);
        }
        private void PressImage1(object sender, PictureBox pictureBox2, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SortMethod((PictureBox)sender, pictureBox2);
        }
        private void PressImage2(object sender, PictureBox pictureBoxe1, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                SortMethod((PictureBox)sender, pictureBoxe1);
        }
        private void SortMethod(PictureBox first, PictureBox second)
        {
            if (rankList.Count != numItem)
            {
                if (rankList.Count == 0)
                {
                    first_sort = true;
                    rankList.Add(first.Text);
                    rankList.Add(second.Text);
                    ChangeRange(1, 2, first.Name == "picture1" ? second : first);
                    if (first.Name == "picture1")
                    {
                        SetMidleImage(first);
                        SetRandomImage(second);
                    }
                    else
                    {
                        SetMidleImage(second);
                        SetRandomImage(first);
                    }
                }
                else
                {
                    first_sort = true;
                    NextStep(first, second);
                }
            }
            if (rankList.Count == numItem)
            {
                Finish();
            }
        }
        private void Finish()
        {
            SaveFiels();
            WindowMassage("Все файлы отсортированы и сохранены внутри директории!");

            Controls.Clear();
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            Controls.Add(tableLayoutPanel);
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = 3;
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10));
            tableLayoutPanel.RowCount = rankList.Count >= 10 ? 10 : rankList.Count;
            for (int i = 0; i < tableLayoutPanel.RowCount; i++)
            {
                Label nameFile = new Label();
                nameFile.Text = rankList[i];
                nameFile.Dock = DockStyle.Fill;
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 1f / tableLayoutPanel.RowCount));
                tableLayoutPanel.Controls.Add(nameFile, 0, i);
                try
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = Image.FromFile(path_directory + "\\" + rankList[i]);
                    pictureBox.Dock = DockStyle.Fill;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                    tableLayoutPanel.Controls.Add(pictureBox, 1, i);

                }
                catch {}
                Button button = new Button();
                button.Text = "Open";
                button.Name = path_result_directory + $"\\{i + 1}_" + rankList[i];
                button.Dock = DockStyle.Fill;
                button.MouseDown += (obj, e) => OpenFile(obj, e);
                tableLayoutPanel.Controls.Add(button, 2, i);
            }
        }
        private void OpenFile(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Button button = (Button)sender;
                string file = button.Name;
                Process.Start(new ProcessStartInfo("explorer", file));
            }
        }
        private void NextStep(PictureBox pictureBox1, PictureBox pictureBox2)
        {
            int n = 1;
            if (pictureBox1.Name != "picture1")
            {
                PictureBox copy = pictureBox1;
                pictureBox1 = pictureBox2;
                pictureBox2 = copy;
                n = -1;
            }
            int midle_item = (range.Y - range.X + 1) / 2 + range.X - 1; //index item
            if (range.Y + n == 0)
            {
                rankList.Insert(0, pictureBox2.Text);
                if (allItemList.Count != 0)
                {
                    ChangeRange(1, rankList.Count, pictureBox2);
                    SetRandomImage(pictureBox2);
                }
                else
                {
                    return;
                }
            }
            else if (range.Y == range.X || midle_item + n >= range.Y)
            {
                if (n == -1)
                {
                    rankList.Insert(range.Y - 1, pictureBox2.Text);
                }
                else
                {
                    rankList.Insert(range.Y, pictureBox2.Text);
                }
                allItemList.Remove(pictureBox2.Text);
                if (allItemList.Count != 0)
                {
                    ChangeRange(1, rankList.Count, pictureBox2);
                    SetRandomImage(pictureBox2);
                }
                else
                {
                    return;
                }
            }
            else
            {
                //изменение границ отбора
                if (n == 1)
                {
                    ChangeRange(midle_item + 2, range.Y, pictureBox2);
                }
                else
                {
                    ChangeRange(range.X, midle_item, pictureBox2);
                }
            }
            //назначение новой первой кнопки
            SetMidleImage(pictureBox1);
        }
        private void MainMenu()
        {
            Button btn1;
            Button btn2;
            Controls.Clear();
            btn1 = new Button();
            btn1.Size = new Size(410, 88);
            btn1.Location = new Point(ClientSize.Width / 2 - btn1.Width / 2, ClientSize.Height / 2 - btn1.Height / 2 - 100);
            btn1.Name = "button1";
            btn1.Text = "Начать сначала";
            btn1.Click += (obj, e) => StartButton_Click();
            Controls.Add(btn1);

            btn2 = new Button();
            btn2.Size = new Size(410, 88);
            btn2.Location = new Point(ClientSize.Width / 2 - btn2.Width / 2, ClientSize.Height / 2 - btn2.Height / 2 + 100);
            btn2.Name = "button2";
            btn2.Text = "Продолжить";
            btn2.MouseDown += (obj, e) => ContinueButton_Click(e);
            Controls.Add(btn2);
            btn1.Anchor = AnchorStyles.Top;
            btn2.Anchor = AnchorStyles.Top;
        }
        private void StartButton_Click()
        {
            Controls.Clear();
            EnteringPathFile();
        }
        private void ContinueButton_Click(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string path = Environment.CurrentDirectory + "\\" + name_save;
                try
                {
                    FileStream file = new FileStream(path, FileMode.Open);
                    StreamReader reader = new StreamReader(file);
                    string line;
                    string[] values;
                    //range
                    line = reader.ReadLine();
                    values = line.Split(",");
                    range.X = int.Parse(values[0]);
                    range.Y = int.Parse(values[1]);
                    //allList
                    line = reader.ReadLine();
                    values = line.Split(",");
                    allItemList = new List<string>();
                    for (int i = 0; i < values.Length-1; i++)
                    {
                        allItemList.Add(values[i]);
                    }
                    //rangList
                    line = reader.ReadLine();
                    values = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    rankList = new List<string>();
                    for (int i = 0; i < values.Length; i++)
                    {
                        rankList.Add(values[i]);
                    }
                    //path_directory
                    path_directory = reader.ReadLine();
                    //picture2
                    image_save = reader.ReadLine();
                    //numItem
                    numItem = int.Parse(reader.ReadLine());

                    reader.Close();
                    IsContinue = true;
                    Sorting();
            }
                catch
                {
                WindowMassage("Сохранений нет!", MessageBoxIcon.Error);
            }
        }
        }
        private void EnteringPathFile()
        {
            Button btn2;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Size = new Size(ClientSize.Width / 4 * 3, 40);
            textBox.Location = new Point(ClientSize.Width / 2 - textBox.Width / 2, ClientSize.Height / 2 - textBox.Height / 2 - 100);
            textBox.Name = "textBox1";
            textBox.TabIndex = 0;
            textBox.Text = "Введите путь";
            textBox.MouseClick += textBox1_TextClick;
            textBox.KeyPress += FileLoad;
            Controls.Add(textBox);
            textBox.Anchor = AnchorStyles.Top;


            btn2 = new Button();
            btn2.Size = new Size(410, 88);
            btn2.Location = new Point(ClientSize.Width / 2 - btn2.Width / 2, ClientSize.Height / 2 - btn2.Height / 2 + 100);
            btn2.Name = "button2";
            btn2.Text = "Назад";
            btn2.Click += (obj, e) => MainMenu();
            Controls.Add(btn2);
            btn2.Anchor = AnchorStyles.Top;
        }
        private void FileLoad(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (Directory.Exists(textBox.Text))
                {
                    DirectoryInfo directory = new DirectoryInfo(textBox.Text);

                    try
                    {
                        if (directory.GetFiles().Length <= 2)
                        {
                            WindowMassage("В каталоге слишком мало файлов.", MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            path_directory = directory.FullName;
                            FileInfo[] files = directory.GetFiles();
                            numItem = files.Length;
                            foreach (FileInfo file in files)
                            {
                                allItemList.Add(file.Name);
                            }
                        }
                    }
                    catch
                    {
                        WindowMassage("В каталоге нет файлов.", MessageBoxIcon.Error);
                        return;
                    }
                    Sorting();
                }
                else
                {
                    WindowMassage("Директория не найдена", MessageBoxIcon.Error);
                    return;
                }
            }
        }
        private void textBox1_TextClick(object sender, MouseEventArgs e)
        {
            TextBox text_box = (TextBox)sender;
            if (e.Button == MouseButtons.Left)
            {
                text_box.Select(0, text_box.TextLength);
            }
        }
        private void WindowMassage(string message)
        {
            MessageBox.Show(
                    message,
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
        }
        private void WindowMassage(string message, MessageBoxIcon icon)
        {
            MessageBox.Show(
                    message,
                    "Сообщение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RightAlign);
        }
        private void SetRandomImage(PictureBox pictureBox)
        {
            string nameFile = allItemList[new Random().Next(0, allItemList.Count)];
            try
            {
                pictureBox.Visible = true;
                pictureBox.Text = nameFile;
                pictureBox.Image = Image.FromFile(path_directory + "\\" + nameFile);
                allItemList.Remove(nameFile);
            }
            catch
            {
                if (!File.Exists(path_directory + "\\" + nameFile))
                {
                    allItemList.Remove(nameFile);
                }
                else
                {
                    allItemList.Remove(nameFile);
                    pictureBox.Visible = false;
                    VisibleText(pictureBox.Name, nameFile);
                }
            }
        }
        private void SetMidleImage(PictureBox pictureBox)
        {
            string nameFile = rankList[(range.Y - range.X + 1) / 2 + range.X - 1];
            try
            {
                pictureBox.Visible = true;
                pictureBox.Text = nameFile;
                pictureBox.Image = Image.FromFile(path_directory + "\\" + nameFile);
            }
            catch
            {
                if (!File.Exists(path_directory + "\\" + nameFile))
                {
                    rankList.Remove(nameFile);
                }
                else
                {
                    pictureBox.Visible = false;
                    VisibleText(pictureBox.Name, nameFile);
                }
            }
        }
        private void SetImage(PictureBox pictureBox, string nameFile)
        {
            try
            {
                pictureBox.Visible = true;
                pictureBox.Text = nameFile;
                pictureBox.Image = Image.FromFile(path_directory + "\\" + nameFile);
            }
            catch
            {
                pictureBox.Visible = false;
                VisibleText(pictureBox.Name, nameFile);
            }
        }
        private void VisibleText(string name, string nameFile)
        {
            Label textBox;
            if (name == "picture1")
            {
                textBox = lable1;
            }
            else
            {
                textBox = lable2;
            }
            textBox.Text = nameFile;
        }
        private void SaveFiels()
        {
            path_result_directory = path_directory + "\\Result";
            for (int i = 0; i < rankList.Count; i++)
            {
                Directory.CreateDirectory(path_result_directory);
                try
                {
                    File.Copy(path_directory + "\\" + rankList[i], path_result_directory + $"\\{i+1}_" + rankList[i]);
                }
                catch
                {
                    WindowMassage($"Неудалось сохранить файл {rankList[i]}", MessageBoxIcon.Error);
                }
            }
        }
        private void SaveButton(PictureBox pictureBox,MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string path = Environment.CurrentDirectory + "\\" + name_save;
               
                FileStream file = new FileStream(path, FileMode.Create);
                StreamWriter write = new StreamWriter(file);
                //range
                write.Write(range.X + "," + range.Y);
                //allList
                write.WriteLine();
                for (int i = 0;i < allItemList.Count;i++)
                {
                    write.Write(allItemList[i] + ",");
                }
                //rangList
                write.WriteLine();
                for (int i = 0; i < rankList.Count; i++)
                {
                    write.Write(rankList[i] + ",");
                }
                //path_directory
                write.WriteLine();
                write.Write(path_directory);
                //picture2
                write.WriteLine();
                write.Write(pictureBox.Text);
                //numItem
                write.WriteLine();
                write.Write(numItem);
                write.Close();
            }
        }
        private void RemoveButton(PictureBox pictureBox1, PictureBox pictureBox2, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && rankList.Count != 0 && first_sort)
            {
                if (rankList.Count == 2 && range.X == 1 && range.Y == 2)
                {
                    range = last_range;
                    allItemList.Add(pictureBox2.Text);
                    SetImage(pictureBox1, name_first_file);
                    SetImage(pictureBox2, name_last_file);
                    rankList.Remove(name_last_file);
                    rankList.Remove(name_first_file);
                }
                else
                {
                    range = last_range;
                    if (rankList.Remove(name_last_file))
                    {
                        allItemList.Add(pictureBox2.Text);
                    }
                    SetMidleImage(pictureBox1);
                    SetImage(pictureBox2, name_last_file);
                }
            }
           
        }
        private void ChangeRange(int begin, int end, PictureBox pictureBox2)
        {
            last_range = range;
            range = new Point(begin, end);
            name_last_file = pictureBox2.Text;
        }
    }
}
//при задание изображения может быть ошибка если во время сортировки что-то удалить