using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UdajovkySem1.StructureTester;

namespace UdajovkySem1
{
    public partial class MainForm : Form
    {
        private ApplicationCore _appCore;
        private string _selectedTree;

        public MainForm()
        {
            InitializeComponent();
            _appCore = new ApplicationCore();
            LoadComboBoxes();
        }

        private void button_GenerateInsert_MouseClick(object sender, MouseEventArgs e)
        {
            if (int.TryParse(textBox_GeneratorInsert_Count.Text, out int count) &&
                double.TryParse(textBox_GeneratorInsert_GPSX1_min.Text, out double gpsX1Min) &&
                double.TryParse(textBox_GeneratorInsert_GPSX1_max.Text, out double gpsX1Max) &&
                double.TryParse(textBox_GeneratorInsert_GPSY1_min.Text, out double gpsY1Min) &&
                double.TryParse(textBox_GeneratorInsert_GPSY1_max.Text, out double gpsY1Max) &&
                double.TryParse(textBox_GeneratorInsert_GPSX2_min.Text, out double gpsX2Min) &&
                double.TryParse(textBox_GeneratorInsert_GPSX2_max.Text, out double gpsX2Max) &&
                double.TryParse(textBox_GeneratorInsert_GPSY2_min.Text, out double gpsY2Min) &&
                double.TryParse(textBox_GeneratorInsert_GPSY2_max.Text, out double gpsY2Max))
            {
                _appCore.GenerateInsert(count, gpsX1Min, gpsX1Max, gpsY1Min, gpsY1Max, gpsX2Min, gpsX2Max, gpsY2Min, gpsY2Max);
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox_SelectTree_SelectedValueChanged(object sender, EventArgs e)
        {
            _selectedTree = comboBox_SelectTree.SelectedItem.ToString();
            string treeString = _appCore.PrintSelectedTree(_selectedTree);
            textBox_PrintTree.Text = treeString;
        }

        private void button_Find_RealEstate_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox_Find_RealEstate_X.Text, out double x) &&
                double.TryParse(textBox_Find_RealEstate_Y.Text, out double y))
            {
                char.TryParse(comboBox_Find_RealEstate_X.SelectedItem.ToString(), out char directionX);
                char.TryParse(comboBox_Find_RealEstate_Y.SelectedItem.ToString(), out char directionY);
                try
                {
                    string foundRealEstate = _appCore.FindRealEstate(directionX, directionY, x, y);
                    textBox_SubPrint.Text = foundRealEstate;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Find_PlotOfLand_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox_Find_PlotOfLand_X.Text, out double x) &&
                double.TryParse(textBox_Find_PlotOfLand_Y.Text, out double y))
            {
                char.TryParse(comboBox_Find_PlotOfLand_X.SelectedItem.ToString(), out char directionX);
                char.TryParse(comboBox_Find_PlotOfLand_Y.SelectedItem.ToString(), out char directionY);
                try
                {
                    string foundPlotsOfLand = _appCore.FindPlotOfLand(directionX, directionY, x, y);
                    textBox_SubPrint.Text = foundPlotsOfLand;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Find_All_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox_Find_All_X1.Text, out double x1) &&
                double.TryParse(textBox_Find_All_Y1.Text, out double y1) &&
                double.TryParse(textBox_Find_All_X2.Text, out double x2) &&
                    double.TryParse(textBox_Find_All_Y2.Text, out double y2))
            {
                char.TryParse(comboBox_Find_All_X1.SelectedItem.ToString(), out char directionX1);
                char.TryParse(comboBox_Find_All_Y1.SelectedItem.ToString(), out char directionY1);
                char.TryParse(comboBox_Find_All_X2.SelectedItem.ToString(), out char directionX2);
                char.TryParse(comboBox_Find_All_Y2.SelectedItem.ToString(), out char directionY2);
                try
                {
                    string foundAll = _appCore.FindAll(directionX1, directionY1, x1, y1, directionX2, directionY2, x2, y2);
                    textBox_SubPrint.Text = foundAll;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                PrintTree();
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_Insert_RealEstate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_Insert_RealEstate_Number.Text, out int number) &&
                double.TryParse(textBox_Insert_RealEstate_X1.Text, out double x1) &&
                double.TryParse(textBox_Insert_RealEstate_Y1.Text, out double y1) &&
                double.TryParse(textBox_Insert_RealEstate_X2.Text, out double x2) &&
                double.TryParse(textBox_Insert_RealEstate_Y2.Text, out double y2))
            {
                char.TryParse(comboBox_Insert_RealEstate_X1.SelectedItem.ToString(), out char directionX1);
                char.TryParse(comboBox_Insert_RealEstate_Y1.SelectedItem.ToString(), out char directionY1);
                char.TryParse(comboBox_Insert_RealEstate_X2.SelectedItem.ToString(), out char directionX2);
                char.TryParse(comboBox_Insert_RealEstate_Y2.SelectedItem.ToString(), out char directionY2);

                string description = textBox_Insert_RealEstate_Description.Text;
                try
                {
                    _appCore.InsertRealEstate(number, description, directionX1, directionY1, x1, y1, directionX2, directionY2, x2, y2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintTree()
        {
            string treeString = _appCore.PrintSelectedTree(_selectedTree);
            textBox_PrintTree.Text = treeString;
        }

        private void LoadComboBoxes()
        {
            comboBox_SelectTree.Items.Add("Plots of Land Tree");
            comboBox_SelectTree.Items.Add("Real Estates Tree");
            comboBox_SelectTree.Items.Add("All GPS Positions Tree");
            comboBox_SelectTree.SelectedIndex = 0;

            comboBox_Find_RealEstate_X.Items.Add("N");
            comboBox_Find_RealEstate_X.Items.Add("S");
            comboBox_Find_RealEstate_Y.Items.Add("E");
            comboBox_Find_RealEstate_Y.Items.Add("W");
            comboBox_Find_RealEstate_X.SelectedIndex = 0;
            comboBox_Find_RealEstate_Y.SelectedIndex = 0;

            comboBox_Find_PlotOfLand_X.Items.Add("N");
            comboBox_Find_PlotOfLand_X.Items.Add("S");
            comboBox_Find_PlotOfLand_Y.Items.Add("E");
            comboBox_Find_PlotOfLand_Y.Items.Add("W");
            comboBox_Find_PlotOfLand_X.SelectedIndex = 0;
            comboBox_Find_PlotOfLand_Y.SelectedIndex = 0;

            comboBox_Find_All_X1.Items.Add("N");
            comboBox_Find_All_X1.Items.Add("S");
            comboBox_Find_All_Y1.Items.Add("E");
            comboBox_Find_All_Y1.Items.Add("W");
            comboBox_Find_All_X2.Items.Add("N");
            comboBox_Find_All_X2.Items.Add("S");
            comboBox_Find_All_Y2.Items.Add("E");
            comboBox_Find_All_Y2.Items.Add("W");
            comboBox_Find_All_X1.SelectedIndex = 0;
            comboBox_Find_All_Y1.SelectedIndex = 0;
            comboBox_Find_All_X2.SelectedIndex = 1;
            comboBox_Find_All_Y2.SelectedIndex = 1;

            comboBox_Insert_RealEstate_X1.Items.Add("N");
            comboBox_Insert_RealEstate_X1.Items.Add("S");
            comboBox_Insert_RealEstate_Y1.Items.Add("E");
            comboBox_Insert_RealEstate_Y1.Items.Add("W");
            comboBox_Insert_RealEstate_X2.Items.Add("N");
            comboBox_Insert_RealEstate_X2.Items.Add("S");
            comboBox_Insert_RealEstate_Y2.Items.Add("E");
            comboBox_Insert_RealEstate_Y2.Items.Add("W");
            comboBox_Insert_RealEstate_X1.SelectedIndex = 0;
            comboBox_Insert_RealEstate_Y1.SelectedIndex = 0;
            comboBox_Insert_RealEstate_X2.SelectedIndex = 1;
            comboBox_Insert_RealEstate_Y2.SelectedIndex = 1;
        }
    }
}
