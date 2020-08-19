using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transportbedrijf
{
    public partial class Form1 : Form
    {
        private const string cargoVolumeString = "volume of cargo in cubic meters";
        private const string cargoWeightString = "weight of cargo in kilograms";
        private const string kmLocalString = "kilometers traveled in the netherlands";
        private const string kmAwayString = "kilimeters traveled outside of the netherlands";
        private const string cargoValueString = "valuge of the cargo";
        private const string calculateCostButtonString = "calculate tranport cost";
        private const string messageBoxCostString = "the transport costs ";

        private const int widthMargin = 10;
        private const int heightMargin = 10;
        private const int rowHeight = 30;
        private const int labelLength = 220;

        private TextBoxWithLabelFormElement cargoVolume;
        private TextBoxWithLabelFormElement cargoWeight;
        private RadioButtonsFormElement cargoFormOption;
        private TextBoxWithLabelFormElement kmLocal;
        private TextBoxWithLabelFormElement kmAway;
        private TextBoxWithLabelFormElement cargoValue;
        private Button calculateCostButton;

        public Form1()
        {
            InitializeComponent();
            InitializeElements();
            ResetPosition();
        }
        private void InitializeElements()
        {
            cargoVolume = new TextBoxWithLabelFormElement(this, cargoVolumeString);
            cargoVolume.SetTextBoxOfset(labelLength);
            cargoWeight = new TextBoxWithLabelFormElement(this, cargoWeightString);
            cargoWeight.SetTextBoxOfset(labelLength);
            cargoFormOption = new RadioButtonsFormElement(this, TransportCost.GetCargoFormOption());
            kmLocal = new TextBoxWithLabelFormElement(this, kmLocalString);
            kmLocal.SetTextBoxOfset(labelLength);
            kmAway = new TextBoxWithLabelFormElement(this, kmAwayString);
            kmAway.SetTextBoxOfset(labelLength);
            cargoValue = new TextBoxWithLabelFormElement(this, cargoValueString);
            cargoValue.SetTextBoxOfset(labelLength);
            calculateCostButton = new Button();
            calculateCostButton.Text = calculateCostButtonString;
            calculateCostButton.Click += new EventHandler(CalculateCostButtonFunction);
            Controls.Add(calculateCostButton);
        }
        private void ResetPosition()
        {
            int row = 0;
            cargoVolume.ChangePosition(widthMargin, heightMargin + rowHeight * row);
            row++;
            cargoWeight.ChangePosition(widthMargin, heightMargin + rowHeight * row);
            row++;
            cargoFormOption.ChangePosition(widthMargin, heightMargin + rowHeight * row);
            row++;
            kmLocal.ChangePosition(widthMargin, heightMargin + rowHeight * row);
            row++;
            kmAway.ChangePosition(widthMargin, heightMargin + rowHeight * row);
            row++;
            cargoValue.ChangePosition(widthMargin, heightMargin + rowHeight * row);
            row++;
            calculateCostButton.Location = new Point(widthMargin, heightMargin + rowHeight * row);
        }

        private double calculateCost()
        {
            int volume;
            try
            {
                volume = int.Parse(cargoVolume.GetValue());
            }
            catch (FormatException exception)
            {
                volume = 0;
            }
            int weight;
            try
            {
                weight = int.Parse(cargoWeight.GetValue());
            }
            catch (FormatException exception)
            {
                weight = 0;
            }
            string formOption = cargoFormOption.GetValue();
            int kmLocal;
            try
            {
                kmLocal = int.Parse(this.kmLocal.GetValue());
            }
            catch (FormatException exception)
            {
                kmLocal = 0;
            }
            int kmAway;
            try
            {
                kmAway = int.Parse(this.kmAway.GetValue());
            }
            catch (FormatException exception)
            {
                kmAway = 0;
            }
            int value;
            try
            {
                value = int.Parse(cargoValue.GetValue());
            }
            catch (FormatException exception)
            {
                value = 0;
            }
            return TransportCost.CalculateCost(volume, weight, formOption, kmLocal, kmAway, value);
        }
        private void ShowCost()
        {
            MessageBox.Show(messageBoxCostString + calculateCost());
        }

        private void CalculateCostButtonFunction(object sender, EventArgs e)
        {
            ShowCost();
        }
    }
}
