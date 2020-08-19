using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transportbedrijf
{
    class RadioButtonsFormElement : FormElement
    {
        private const int radioButtonOfsetDefault = 120;

        private const string exceptionNoOptionSelected = "no radio option selected";

        private int radioButtonOfset;

        private GroupBox group;
        private RadioButton[] radioButtons;

        private string[] options;

        public RadioButtonsFormElement(Form form, string[] options) : base(form)
        {
            this.options = CopyStringArray(options);
            group = new GroupBox();
            radioButtons = new RadioButton[options.Length];
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i] = new RadioButton();
                group.Controls.Add(radioButtons[i]);
                radioButtons[i].Text = this.options[i];
                form.Controls.Add(radioButtons[i]);
            }

            radioButtonOfset = radioButtonOfsetDefault;
        }
        protected override void AlterPosition(int widthOfset, int heightOfset)
        {
            for (int i = 0; i < radioButtons.Length; i++)
            {
                radioButtons[i].Location = new System.Drawing.Point(widthOfset + radioButtonOfset * i, heightOfset);
            }
        }
        public string GetValue()
        {
            foreach(RadioButton radioButton in radioButtons)
            {
                if (radioButton.Checked == true)
                {
                    return radioButton.Text;
                }
            }
            throw new Exception(exceptionNoOptionSelected);
        }
        public void SetRadioButtonOfset(int ofset)
        {
            radioButtonOfset = ofset;
        }
        private string[] CopyStringArray(string[] toCopy)
        {
            string[] toReturn = new string[toCopy.Length];
            for(int i=0; i < toReturn.Length; i++)
            {
                toReturn[i] = toCopy[i];
            }
            return toReturn;
        }
    }
}
