using System;
using System.Drawing;
using System.Windows.Forms;

namespace Learning_to_draw
{
    public partial class Form1 : Form
    {
        int[] exportLeds = new int[0];
        int[] exportColour = new int[0];

        int red = 0;
        int green = 0;
        int blue = 0;

        int newRow = 0;


        bool[,] matrix = new bool[40,16];
        public Form1()
        {
            
            InitializeComponent();
            DrawGrid();
        }
        public void DrawGrid()
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            int xBound = pictureBox1.Width;
            int yBound = pictureBox1.Height;

            int i = 0;
            int j;

            using (Graphics gr = Graphics.FromImage(bmp))
            {
                // Create pen.
                Pen blackPen = new Pen(Color.Black, 1);

                for (i = 0; i < 40; i++)
                {
                    for (j = 0; j < 15; j++)
                    {
                        Rectangle columns = new Rectangle(i*15, j*15, 10, 10);
                        gr.DrawRectangle(blackPen, columns);
                    }

                    Rectangle rows = new Rectangle(i*15, j*15, 10, 10);
                    gr.DrawRectangle(blackPen, rows);
                }
            }
            pictureBox1.Image = bmp;
        }
        private void Draw_Click_1(object sender, EventArgs e)
        {
            //DrawRectangleRectangle();
            DrawGrid();
        }
        private void Clear_Click(object sender, EventArgs e)
        {
            //Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //pictureBox1.Image = bmp;
            DrawGrid();
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    matrix[i,j] = false;
                }
            }
            exportColour = new int[0];
            exportLeds = new int[0];
        }
        void pictureBox1_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Bitmap bmp = (Bitmap)pictureBox1.Image;
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                SolidBrush blueBrush = new SolidBrush(Color.FromArgb(red,green,blue));
                SolidBrush erasor = new SolidBrush(pictureBox1.BackColor);
                Pen blackPen = new Pen(Color.Black, 10);
                for (int i = 0; i < 40; i++)
                {
                    for (int j = 0; j < 16; j++)
                    {
                        if (e.X > (i * 15) -2.5f && e.X < i * 15 + 12.5f)
                        {
                            if (e.Y > (j * 15) -2.5f && e.Y < j * 15 + 12.5f)
                            {
                                if (matrix[i,j] == false)
                                {
                                    if (i > 7 && i < 16)
                                        newRow = 120;
                                    if (i > 15 && i < 24)
                                        newRow = 240;
                                    if (i > 23 && i < 32)
                                        newRow = 360;
                                    if (i > 31 && i < 40)
                                        newRow = 480;

                                    Rectangle rect = new Rectangle(i * 15 +1, j * 15 +1, 9, 9);
                                    gr.FillRectangle(blueBrush, rect);
                                    matrix[i, j] = true;

                                    Array.Resize(ref exportLeds, exportLeds.Length + 1);
                                    exportLeds[exportLeds.Length - 1] = (i + newRow) + ((j * 8) + 1);
                                    Array.Resize(ref exportColour, exportColour.Length +3); 
                                    exportColour[exportColour.Length - 3] = red;
                                    exportColour[exportColour.Length - 2] = green;
                                    exportColour[exportColour.Length - 1] = blue;
                                }
                                else
                                {
                                    Rectangle rect = new Rectangle(i * 15 +1, j * 15 +1, 9, 9);
                                    gr.FillRectangle(erasor, rect);
                                    matrix[i, j] = false;
                                }
                            }
                        }
                    }
                }
            }
            pictureBox1.Image = bmp;
        }
        private void export_Click(object sender, EventArgs e)
        {
            calledByExportArray();
            calledByTextBox2();
            calledByExportColor();
        }
        void calledByExportArray()
        {
            string output = "{";
            string[] stringArray = new string[exportLeds.Length];
            stringArray = Array.ConvertAll(exportLeds, x => x.ToString());
            for (int x = 0; x < stringArray.Length; x++)
            {
                output = output + stringArray[x] + ',';
            }
            output = output.Remove(output.Length - 1, 1);
            output = output + '}';
            textBox1.Text = output;
            output = "";
        }
        void calledByExportColor()
        {
            
            /*
            int count = 0;
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (matrix[i, j])
                        count++;
                }
            }
            int[] export = new int[count * 3];
            for (int i = 0; i < export.Length; i++)
            {
                export[i] = red;
                export[i + 1] = green;
                export[i + 2] = blue;
                i++;
                i++;
            }
            */

            string output = "{";
            string[] stringArray = new string[exportColour.Length];
            stringArray = Array.ConvertAll(exportColour, x => x.ToString());
            for (int x = 0; x < stringArray.Length; x++)
            {
                output = output + stringArray[x] + ',';
            }
            output = output.Remove(output.Length - 1, 1);
            output = output + '}';
            exportColor.Text = output;
            output = "";
        }
        void calledByTextBox2() {
            int count = 0;
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (matrix[i, j])
                        count++;
                }
            }
            textBox2.Text = count.ToString();
        }
        void calledByExport(int count)
        {
            int i;
            int j;

            int newRow = 0;

            for (i = 0; i < 40; i++)
            {
                for (j = 0; j < 16; j++)
                {
                    
                    if (matrix[i, j])
                    {
                        if (i > 7 && i < 16)
                            newRow = 120;
                        if (i > 15 && i < 24)
                            newRow = 240;
                        if (i > 23 && i < 32)
                            newRow = 360;
                        if (i > 31 && i < 40)
                            newRow = 480;

                        if (count > 0)
                        {
                            if (exportLeds[count - 1] != (i + newRow) + ((j * 8) + 1))
                            {
                                exportLeds[count] = (i + newRow) + ((j * 8) + 1);
                                return;
                            }
                        }
                        else
                        {
                            exportLeds[count] = (i + newRow) + ((j * 8) + 1);
                            return;
                        }
                    }
                }
            }
        }
        private void textBox2_Click(object sender, EventArgs e)
        {
            calledByTextBox2();
        }
        private void redSlider_ValueChanged(object sender, EventArgs e)
        {
            red = redSlider.Value;
            colorPreview.BackColor = Color.FromArgb(red, green, blue);

        }
        private void greenSlider_Scroll(object sender, EventArgs e)
        {
            green = greenSlider.Value;
            colorPreview.BackColor = Color.FromArgb(red,green,blue);
        }
        private void blueSlider_Scroll(object sender, EventArgs e)
        {
            blue = blueSlider.Value;
            colorPreview.BackColor = Color.FromArgb(red, green, blue);
        }
        private void exportColor_Click(object sender, EventArgs e)
        {
            calledByExportColor();
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            /*
            int[] export = new int[(40 * 16)];

            int i;
            int j;

            int newRow = 0;

            int count = 0;

            for (i = 0; i < 40; i++)
            {
                for (j = 0; j < 16; j++)
                {
                    if (matrix[i, j])
                    {
                        if (i > 7 && i < 16)
                            newRow = 120;
                        if (i > 15 && i < 24)
                            newRow = 240;
                        if (i > 23 && i < 32)
                            newRow = 360;
                        if (i > 31 && i < 40)
                            newRow = 480;

                        export[count] = (i + newRow) + ((j * 8) + 1);
                        count++;
                    }
                }
            }
            Array.Sort(export);
            string output = "{";
            string[] stringArray = new string[40 * 16];
            stringArray = Array.ConvertAll(export, x => x.ToString());
            for (int x = 0; x < stringArray.Length; x++)
            {
                if (stringArray[x] != "0")
                {
                    output = output + stringArray[x] + ',';
                }
            }
            output = output.Remove(output.Length - 1, 1);
            output = output + '}';
            textBox1.Text = output;
            output = "";
            */
        }
    }
}