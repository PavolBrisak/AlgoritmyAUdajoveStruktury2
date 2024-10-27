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
                    string foundRealEstate = "Found these Real estates:\n" +
                                             _appCore.FindRealEstate(directionX, directionY, x, y);
                    textBox_SubPrint.Text = foundRealEstate;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
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
                    string foundPlotsOfLand = "Found these Plots of land:\n" +
                                              _appCore.FindPlotOfLand(directionX, directionY, x, y);
                    textBox_SubPrint.Text = foundPlotsOfLand;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
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
                    string foundAll = "Found these objects:\n" + _appCore.FindAll(directionX1, directionY1, x1, y1,
                        directionX2, directionY2, x2, y2);
                    textBox_SubPrint.Text = foundAll;
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }

                PrintTree();
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
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
                    _appCore.InsertRealEstate(number, description, directionX1, directionY1, x1, y1, directionX2,
                        directionY2, x2, y2);
                    textBox_SubPrint.Text = "Inserted real estate.";
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }

                PrintTree();
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void button_Insert_PlotOfLand_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_Insert_PlotOfLand_Number.Text, out int number) &&
                double.TryParse(textBox_Insert_PlotOfLand_X1.Text, out double x1) &&
                double.TryParse(textBox_Insert_PlotOfLand_Y1.Text, out double y1) &&
                double.TryParse(textBox_Insert_PlotOfLand_X2.Text, out double x2) &&
                double.TryParse(textBox_Insert_PlotOfLand_Y2.Text, out double y2))
            {
                char.TryParse(comboBox_Insert_PlotOfLand_X1.SelectedItem.ToString(), out char directionX1);
                char.TryParse(comboBox_Insert_PlotOfLand_Y1.SelectedItem.ToString(), out char directionY1);
                char.TryParse(comboBox_Insert_PlotOfLand_X2.SelectedItem.ToString(), out char directionX2);
                char.TryParse(comboBox_Insert_PlotOfLand_Y2.SelectedItem.ToString(), out char directionY2);

                string description = textBox_Insert_PlotOfLand_Description.Text;
                try
                {
                    _appCore.InsertPlotOfLand(number, description, directionX1, directionY1, x1, y1, directionX2,
                        directionY2, x2, y2);
                    textBox_SubPrint.Text = "Inserted plot of land.";
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }

                PrintTree();
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void PrintTree()
        {
            string treeString = _appCore.PrintSelectedTree(_selectedTree);
            textBox_PrintTree.Text = treeString;
        }

        private void button_GenerateInsert_PlotOfLand_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_GeneratorInsert_PlotOfLand_Count.Text, out int count) &&
                double.TryParse(textBox_GeneratorInsert_PlotOfLand_Max.Text, out double max) &&
                double.TryParse(textBox_GeneratorInsert_PlotOfLand_Min.Text, out double min) &&
                int.TryParse(textBox_GeneratorInsert_PlotOfLand_DesMiesta.Text, out int desMiesta))
            {
                try
                {
                    _appCore.GenerateInsertPlotOfLand(count, min, max, desMiesta);
                    textBox_SubPrint.Text = "Generated and inserted plots of land.";
                    PrintTree();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void button_GenerateInsert_RealEstate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_GeneratorInsert_RealEstate_Count.Text, out int count) &&
                double.TryParse(textBox_GeneratorInsert_RealEstate_Max.Text, out double max) &&
                double.TryParse(textBox_GeneratorInsert_RealEstate_Min.Text, out double min) &&
                int.TryParse(textBox_GeneratorInsert_RealEstate_DesMiesta.Text, out int desMiesta))
            {
                try
                {
                    _appCore.GenerateInsertRealEstate(count, min, max, desMiesta);
                    textBox_SubPrint.Text = "Generated and inserted real estates.";
                    PrintTree();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void LoadComboBoxes()
        {
            comboBox_SelectTree.Items.Add("Plots of Land Tree");
            comboBox_SelectTree.Items.Add("Real Estates Tree");
            comboBox_SelectTree.Items.Add("All GPS Positions Tree");
            comboBox_SelectTree.Items.Add("Test Data Tree");
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
            comboBox_Find_All_X2.SelectedIndex = 0;
            comboBox_Find_All_Y2.SelectedIndex = 0;

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
            comboBox_Insert_RealEstate_X2.SelectedIndex = 0;
            comboBox_Insert_RealEstate_Y2.SelectedIndex = 0;

            comboBox_Insert_PlotOfLand_X1.Items.Add("N");
            comboBox_Insert_PlotOfLand_X1.Items.Add("S");
            comboBox_Insert_PlotOfLand_Y1.Items.Add("E");
            comboBox_Insert_PlotOfLand_Y1.Items.Add("W");
            comboBox_Insert_PlotOfLand_X2.Items.Add("N");
            comboBox_Insert_PlotOfLand_X2.Items.Add("S");
            comboBox_Insert_PlotOfLand_Y2.Items.Add("E");
            comboBox_Insert_PlotOfLand_Y2.Items.Add("W");
            comboBox_Insert_PlotOfLand_X1.SelectedIndex = 0;
            comboBox_Insert_PlotOfLand_Y1.SelectedIndex = 0;
            comboBox_Insert_PlotOfLand_X2.SelectedIndex = 0;
            comboBox_Insert_PlotOfLand_Y2.SelectedIndex = 0;
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button_GenerateFind_Both_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_GeneratorFind_Count.Text, out int count))
            {
                try
                {
                    string found = "Found plots of land and real estaes:\n" + _appCore.GenerateFindBoth(count);
                    textBox_SubPrint.Text = found;
                    PrintTree();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void button_GenerateFind_RealEstate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_GeneratorFind_Count.Text, out int count))
            {
                try
                {
                    string found = "Found real estates.\n" + _appCore.GenerateFindRealEstate(count);
                    textBox_SubPrint.Text = found;
                    PrintTree();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void button_GenerateFind_PlotOfLand_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_GeneratorFind_Count.Text, out int count))
            {
                try
                {
                    string found = _appCore.GenerateFindPlotOfLand(count);
                    textBox_SubPrint.Text = found;
                    PrintTree();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void button_GenerateDelete_PlotOfLand_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_GeneratorDelete_Count.Text, out int count))
            {
                try
                {
                    _appCore.GenerateDeletePlotOfLand(count);
                    textBox_SubPrint.Text = "Deleted plots of land.";
                    PrintTree();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void button_GenerateDelete_RealEstate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_GeneratorDelete_Count.Text, out int count))
            {
                try
                {
                    _appCore.GenerateDeleteRealEstate(count);
                    textBox_SubPrint.Text = "Deleted real estates.";
                    PrintTree();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void button_Test_Insert_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_Test_Insert_Count.Text, out int count) &&
                double.TryParse(textBox_Test_Insert_Max.Text, out double max) &&
                double.TryParse(textBox_Test_Insert_Min.Text, out double min) &&
                int.TryParse(textBox_Test_Insert_DesMiesta.Text, out int desMiesta) &&
                double.TryParse(textBox_Test_Insert_DuplicitPercentage.Text, out double duplicityPercentage))
            {
                try
                {
                    _appCore.TestInsert(count, min, max, desMiesta, duplicityPercentage);
                    textBox_SubPrint.Text = "Test data inserted into the Data Tree.";
                    PrintTree();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void button_Test_Find_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_Test_Find_Count.Text, out int count))
            {
                try
                {
                    string found = "Found test data:\n" + _appCore.TestFind(count);
                    textBox_SubPrint.Text = found;
                    PrintTree();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }

        private void button_Test_Delete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_Test_Delete_Count.Text, out int count))
            {
                try
                {
                    string deleted = _appCore.TestDelete(count);
                    textBox_SubPrint.Text = deleted;
                    PrintTree();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }
            }
            else
            {
                ShowErrorMessage("Please enter valid numeric values in all fields.");
            }
        }
    }
}
