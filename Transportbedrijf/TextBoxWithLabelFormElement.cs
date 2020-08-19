﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transportbedrijf
{
    class TextBoxWithLabelFormElement : FormElement
    {
        private const int textBoxOfsetDefault = 100;

        protected int textBoxOfset;

        protected Label label;
        protected TextBox textBox;

        public TextBoxWithLabelFormElement(Form form, string labelText):base(form)
        {
            this.form = form;
            textBoxOfset = textBoxOfsetDefault;

            label = new Label();
            label.Text = labelText;
            label.Width = textBoxOfset;
            form.Controls.Add(label);

            textBox = new TextBox();
            form.Controls.Add(textBox);
        }
        public string GetValue()
        {
            return textBox.Text;
        }
        protected override void AlterPosition(int widthOfset, int heightOfset)
        {
            label.Location = new System.Drawing.Point(widthOfset, heightOfset);
            textBox.Location = new System.Drawing.Point(widthOfset + textBoxOfset, heightOfset);
        }
        public void SetTextBoxOfset(int ofset)
        {
            textBoxOfset = ofset;
            label.Width = textBoxOfset;
        }
    }
    class TextBoxWithLabelAndRemoveButtonFormElement : TextBoxWithLabelFormElement
    {
        private const int removeButtonOfsetDefault = 110;
        private const string removeButtonText = "remove";

        private int removeButtonOfset;

        private Button removeButton;
        private Action<object> removeButtonLamba;
        public TextBoxWithLabelAndRemoveButtonFormElement(Form form, string labelText, Action<object> removeButtonLamba) : base(form, labelText)
        {
            removeButtonOfset = removeButtonOfsetDefault;
            removeButton = new Button();
            removeButton.Text = removeButtonText;
            removeButton.Click += new EventHandler(RemoveButtonFunction);
            form.Controls.Add(removeButton);

            this.removeButtonLamba = removeButtonLamba;
        }
        private void RemoveButtonFunction(object sender, EventArgs e)
        {
            form.Controls.Remove(label);
            form.Controls.Remove(textBox);
            form.Controls.Remove(removeButton);
            if (removeButtonLamba != null)
            {
                removeButtonLamba.Invoke(this);
            }
        }
        public void ChangePosition(int widthOfset, int heightOfset)
        {
            AlterPosition(widthOfset, heightOfset);
            removeButton.Location = new System.Drawing.Point(widthOfset + textBoxOfset + removeButtonOfset, heightOfset);
        }
        public void RemoveButtonOfset(int ofset)
        {
            removeButtonOfset = ofset;
        }
    }
}
