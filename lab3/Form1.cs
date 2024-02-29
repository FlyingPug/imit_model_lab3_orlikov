using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

namespace lab3
{
    public partial class Form1 : Form
    {
        List<string> generations;
        Dictionary<string, char> rules;
        int size = 10;

        private Color aliveColor = Color.Green;
        private Color deadColor = Color.White;

        public Form1()
        {
            generations = new List<string>();
            rules = new Dictionary<string, char>();
            InitializeComponent();
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = size;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            generations.Add(new string('0', size));
            dataGridView1.RowCount = 300;

            timer1.Interval = 1;
        }

        private void StartStop_Click(object sender, EventArgs e)
        {
            string rule = ruleChoose.Text;
            string binaryCode = Convert.ToString(int.Parse(rule), 2).PadLeft(8, '0');

            if (timer1.Enabled) timer1.Stop();
            else
            {
                for (int i = 0; i < binaryCode.Count(); i++)
                {
                    rules[Convert.ToString(i, 2).PadLeft(3, '0')] = binaryCode[i];
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateGenerations();
            UpdateGridView();
        }

        private void UpdateGenerations()
        {
            var lastString = generations.Last();
            StringBuilder sd = new StringBuilder(lastString);
            var elementsCount = lastString.Count();

            sd[0] = rules["0" + lastString.Substring(0, 2)];
            for (int i = 1; i < elementsCount - 1; i++)
            {
                sd[i] = rules[lastString.Substring(i - 1, 3)];
            }
            sd[elementsCount - 1] = rules[lastString.Substring(elementsCount - 2, 2) + "0"];

            generations.Add(sd.ToString());
        }

        private void UpdateGridView()
        {
            int currentRow = generations.Count - 1;
            for (int i = 0; i < size; i++)
            {
                dataGridView1.Rows[currentRow].Cells[i].Style.BackColor = generations[currentRow][i] == '1' ? aliveColor : deadColor;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!timer1.Enabled && e.RowIndex == generations.Count - 1)
            {
                StringBuilder sd = new StringBuilder(generations.Last());
                sd[e.ColumnIndex] = sd[e.ColumnIndex] == '1' ? '0' : '1';
                generations[generations.Count - 1] = sd.ToString();
                UpdateGridView();
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}