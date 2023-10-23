using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using static System.Windows.Forms.AxHost;
using System.Linq.Expressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Collections.Generic;

namespace _1_controls
{
    public partial class Form1 : Form
    {
        public string? name;
        public string? surname;
        public string? patronymic;
        public string? email;
        public string? number;
        public int? age;
        public string? sex;
        public string[]? social_network1;
        public string[]? social_network2;
        public string[]? social_network3;
        public List<string>? e_lang = new List<string>();
        public List<string>? lang = new List<string>();

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            monthCalendar1.MaxSelectionCount = 1;
            maskedTextBox1.ResetOnSpace = false;
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == String.Empty || textBox2.Text == String.Empty || textBox3.Text == String.Empty)
                MessageBox.Show("Invalid Full Name!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages["TabPage2"];
                name = textBox1.Text;
                surname = textBox2.Text;
                patronymic = textBox3.Text;
            }
        }
        private void ForbidSpace(KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
                e.Handled = e.KeyChar == ' ';
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ForbidSpace(e);
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            ForbidSpace(e);
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            ForbidSpace(e);
        }
        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            DateTime birthdate = new DateTime(monthCalendar1.SelectionRange.Start.Year, monthCalendar1.SelectionRange.Start.Month, monthCalendar1.SelectionRange.Start.Day);
            DateTime nowDate = new DateTime(monthCalendar1.TodayDate.Year, monthCalendar1.TodayDate.Month, monthCalendar1.TodayDate.Day);

            int mainDate = int.Parse(nowDate.ToString("yyyyMMdd"));

            int userDate = int.Parse(birthdate.ToString("yyyyMMdd"));

            int age = (mainDate - userDate) / 10000;

            label5.Text = Convert.ToString($"{age}");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(label5.Text) == 0)
                MessageBox.Show("Invalid Birth Date!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages["TabPage3"];
                age = Convert.ToInt32(label5.Text);
            }
        }
        private bool IsValidEmail(string eMail)
        {
            bool Result = false;

            try
            {
                var eMailValidator = new System.Net.Mail.MailAddress(eMail);
                Result = (eMail.LastIndexOf(".") > eMail.LastIndexOf("@"));
            }
            catch
            {
                Result = false;
            };

            return Result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Invalid Sex!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (maskedTextBox1.MaskCompleted == false)
            {
                MessageBox.Show("Invalid Number!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (IsValidEmail(textBox4.Text) == false)
            {
                MessageBox.Show("Invalid Email Address!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox4.Focus();
            }
            else
            {
                tabControl1.SelectedTab = tabControl1.TabPages["TabPage4"];

                if (radioButton1.Checked)
                    sex = "Мужчина";
                else sex = "Женщина";

                number = maskedTextBox1.Text;

                email = textBox4.Text;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            ForbidSpace(e);
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int)Keys.Space)
                e.KeyChar = '\0';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (((comboBox1.Text == comboBox2.Text || comboBox1.Text == comboBox3.Text) && textBox6.Enabled == true) ||
                ((comboBox2.Text == comboBox3.Text) && textBox7.Enabled == true))
                MessageBox.Show("Invalid Socials!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else tabControl1.SelectedTab = tabControl1.TabPages["TabPage5"];

            if (textBox5 is not null)
            {
                social_network1 = new string[2];
                social_network1[0] = comboBox1.Text;
                social_network1[1] = textBox5.Text;
            }
            if (textBox6 is not null)
            {
                social_network2 = new string[2];
                social_network2[0] = comboBox2.Text;
                social_network2[1] = textBox6.Text;
            }
            if (textBox7 is not null)
            {
                social_network3 = new string[2];
                social_network3[0] = comboBox3.Text;
                social_network3[1] = textBox7.Text;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                checkedListBox1.Enabled = true;
            else
            {
                bool state = false;
                checkedListBox1.Enabled = false;
                for (int i = 0; i < checkedListBox1.Items.Count; ++i)
                    checkedListBox1.SetItemCheckState(i, (state ? CheckState.Checked : CheckState.Unchecked));
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                checkedListBox2.Enabled = true;
            else
            {
                bool state = false;
                checkedListBox2.Enabled = false;
                for (int i = 0; i < checkedListBox2.Items.Count; ++i)
                    checkedListBox2.SetItemCheckState(i, (state ? CheckState.Checked : CheckState.Unchecked));
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                foreach (var checkedBox in checkedListBox1.CheckedItems)
                {
                    e_lang.Add(checkedBox.ToString());
                }
            }
            if (checkBox2.Checked)
            {
                foreach (var checkedBox in checkedListBox2.CheckedItems)
                {
                    lang.Add(checkedBox.ToString());
                }
            }

            string socials = string.Empty;
            List<string> e_languages = new List<string>();
            List<string> languages = new List<string>();

            string? path = Path.GetDirectoryName(Environment.ProcessPath);

            StreamWriter sw = new StreamWriter(path + "\\Test.txt");
            sw.WriteLine($"Имя: {name}\nФамилия: {surname}\n" +
                $"Отчество: {patronymic}\n" +
                $"Возраст: {age}\nПол: {sex}\n" +
                $"Номер: {number}\nПочта: {email}");

            if ((comboBox1 is not null && textBox5 is not null) && (comboBox2 is not null && textBox6 is not null) && (comboBox3 is not null && textBox7 is not null) && textBox7.Enabled == true)
            {
                socials = $"{social_network1[0]}: {social_network1[1]}\n" +
                                    $"{social_network2[0]}: {social_network2[1]}\n" +
                                    $"{social_network3[0]}: {social_network3[1]}";
                sw.WriteLine(socials);
            }
            else if ((comboBox1 is not null && textBox5 is not null) && (comboBox2 is not null && textBox6 is not null) && textBox6.Enabled == true)
            {
                socials = $"{social_network1[0]}: {social_network1[1]}\n" +
                    $"{social_network2[0]}: {social_network2[1]}";
                sw.WriteLine(socials);
            }
            else if (comboBox1 is not null && textBox5 is not null)
            {
                socials = $"{social_network1[0]}: {social_network1[1]}";
                sw.WriteLine(socials);
            }
            ///
            if (e_lang is not null)
            {
                sw.Write($"Языки програмированния: ");
                foreach (var e_lang in e_lang)
                {
                    e_languages.Add(e_lang.ToString());
                    sw.Write(e_lang + ' ');
                }
            }
            if (lang is not null)
            {
                sw.WriteLine();
                sw.Write($"Языки: ");
                foreach (var lang in lang)
                {
                    languages.Add(lang.ToString());
                    sw.Write(lang + ' ');
                }
            }

            string ShowElang()
            {
                string? content = null;
                foreach (string e_lang in e_languages)
                {
                    content += e_lang + ' ';
                }
                return content;
            }

            string ShowLang()
            {
                string? content = null;
                foreach (string lang in languages)
                {
                    content += lang + ' ';
                }
                return content;
            }

            sw.Close();

            MessageBox.Show($"Имя: {name}\nФамилия: {surname}\n" +
                $"Отчество: {patronymic}\n" +
                $"Возраст: {age}\nПол: {sex}\n" +
                $"Номер: {number}\nПочта: {email}\n{socials}\n" +
                $"Языки програмирования: {ShowElang()}\nЯзыки: {ShowLang()}", "Account", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            ForbidSpace(e);
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            ForbidSpace(e);
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            ForbidSpace(e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1 is null)
                textBox5.Enabled = false;
            else
            {
                textBox5.Enabled = true;
                comboBox2.Enabled = true;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2 is null)
                textBox6.Enabled = false;
            else
            {
                textBox6.Enabled = true;
                comboBox3.Enabled = true;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3 is null)
                textBox7.Enabled = false;
            else
                textBox7.Enabled = true;
        }
    }
}