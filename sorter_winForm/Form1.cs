using sorter_winForm.Properties;
using System.IO;
using System.Windows.Forms;
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
        string path_directory;
        TextBox textBox1 = new TextBox();
        TextBox textBox2 = new TextBox();

        private void Form1_Load(object sender, EventArgs e)
        {
            Size = new Size(860, 640);
            Anchor = AnchorStyles.Top;
            MainMenu();
        }
        void Sorting()
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
            splitContainer2.Panel1.Controls.Add(textBox1);
            // splitContainer2.Panel2
            splitContainer2.Panel2.Controls.Add(pictureBox2);
            splitContainer2.Panel2.Controls.Add(textBox2);
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
            // button2
            btn2.Anchor = AnchorStyles.Top;
            btn2.Size = new Size(splitContainer1.Panel2.Width / 4, (int)(3f / 4f * splitContainer1.Panel2.Height));
            btn2.Location = new Point(splitContainer1.Location.X + splitContainer1.Panel2.Width / 2 - btn2.Width / 2 - splitContainer1.Panel2.Width / 3, splitContainer1.Location.Y + splitContainer1.Panel2.Height / 2 - btn2.Height / 2 - splitContainer1.Panel2.Height / 3);
            btn2.Name = "button2";
            btn2.TabIndex = 1;
            btn2.Text = "Сохранить";
            btn2.UseVisualStyleBackColor = true;
            // textBox1
            textBox1.Size = new Size((int)(splitContainer2.Panel1.Width * 0.9f), (int)(1f/10f * splitContainer2.Panel1.Height));
            textBox1.Location = new Point(splitContainer2.Panel1.Width / 2 - textBox1.Size.Width / 2, splitContainer2.Panel1.Height / 2 - 3 * textBox1.Size.Height);
            textBox1.ReadOnly = true;
            textBox1.Anchor = AnchorStyles.Top;
            // textBox2
            textBox2.Size = new Size((int)(splitContainer2.Panel2.Width * 0.9f), (int)(1f / 10f * splitContainer2.Panel2.Height));
            textBox2.Location = new Point(splitContainer2.Panel2.Width / 2 - textBox2.Size.Width / 2, splitContainer2.Panel2.Height / 2 - 3 * textBox2.Size.Height);
            textBox2.ReadOnly = true;
            textBox2.Anchor = AnchorStyles.Top;
            //setImage
            SetRandomImage(pictureBox1);
            SetRandomImage(pictureBox2);
            //
            splitContainer2.Panel1.MouseClick += (obj, e) => PressImage1(pictureBox1, pictureBox2);
            splitContainer2.Panel2.MouseClick += (obj, e) => PressImage2(pictureBox2, pictureBox1);
            pictureBox1.MouseClick += (obj, e) => PressImage1(pictureBox1, pictureBox2);
            pictureBox2.MouseClick += (obj, e) => PressImage2(pictureBox2, pictureBox1);

        }
        void PressImage1(object sender, PictureBox pictureBox2)
        {
            SortMethod((PictureBox)sender, pictureBox2);
        }
        void PressImage2(object sender, PictureBox pictureBoxe1)
        {
            SortMethod((PictureBox)sender, pictureBoxe1);
        }
        void SortMethod(PictureBox first, PictureBox second)
        {
            if (rankList.Count != numItem)
            {
                if (rankList.Count == 0)
                {
                    rankList.Add(first.Text);
                    rankList.Add(second.Text);
                    range = new Point(1, rankList.Count);
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
                    NextStep(first, second);
                }
            }
            if (rankList.Count == numItem)
            {
                Finish();
            }
        }
        void Finish()
        {
            WindowMassage("Все элементы отсортированы.");
        }
        void NextStep(PictureBox pictureBox1, PictureBox pictureBox2)
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
                    SetRandomImage(pictureBox2);
                    range.X = 1;
                    range.Y = rankList.Count;
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
                    SetRandomImage(pictureBox2);
                    range.X = 1;
                    range.Y = rankList.Count;
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
                    range.X = midle_item + 2;
                }
                else
                {
                    range.Y = midle_item;
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
            btn2.Click += (obj, e) => ContinueButton_Click();
            Controls.Add(btn2);
            btn1.Anchor = AnchorStyles.Top;
            btn2.Anchor = AnchorStyles.Top;
        }
        private void StartButton_Click()
        {
            Controls.Clear();
            EnteringPathFile();
        }
        private void ContinueButton_Click()
        {

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
                            WindowMassage("В каталоге слишком мало файлов.");
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
                        WindowMassage("В каталоге нет файлов.");
                        return;
                    }
                    Sorting();
                }
                else
                {
                    WindowMassage("Директория не найдена");
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
                allItemList.Remove(nameFile);
                pictureBox.Visible = false;
                VisibleText(pictureBox.Name, nameFile);
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
                pictureBox.Visible = false;
                VisibleText(pictureBox.Name, nameFile);
            }

        }
        private void VisibleText(string name, string nameFile)
        {
            TextBox textBox;
            if (name == "picture1")
            {
                textBox = textBox1;
            }
            else
            {
                textBox = textBox2;
            }
            textBox.Text = nameFile;
        }
    }
}
