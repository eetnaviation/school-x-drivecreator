using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SchoolHecker_DriveMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitTimer();
        }

        private void drives_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void fetchDrivesList()
        {
            var driveList = DriveInfo.GetDrives();
            var currentDrives = new List<string>();

            foreach (DriveInfo drive in driveList)
            {
                if (drive.DriveType == DriveType.Removable)
                {
                    currentDrives.Add(drive.Name);

                    if (!drives.Items.Contains(drive.Name))
                    {
                        drives.Items.Add(drive.Name);
                    }
                }
            }

            for (int i = drives.Items.Count - 1; i >= 0; i--)
            {
                var driveName = (string)drives.Items[i];
                if (!currentDrives.Contains(driveName))
                {
                    drives.Items.RemoveAt(i);
                }
            }

            if (drives.Items.Count > 0)
            {
                drives.SelectedIndex = 0;
            }
            else
            {
                drives.SelectedIndex = -1;
            }

        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2000; //milliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            fetchDrivesList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void initBuild_Click(object sender, EventArgs e)
        {
            //Drive formatter
            string chosenDrive = drives.SelectedItem.ToString();
            if (MessageBox.Show("Are you sure you want to erase all data on chosen drive?", "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                
            }
            else
            {
                return;
            }

            // Key generation algorithm
            string usernameNumeric = "";
            try
            {
                string username = textBox1.Text;
                usernameNumeric = getAlphabetNumberFromString(username);
            }
            catch (Exception error)
            {
                string finalError = error.ToString();
                MessageBox.Show("Got error! Possible solution: Username textbox empty " + finalError);
                return;
            }
            string allowedActions = "";
            try
            {
                allowedActions = comboBox2.SelectedItem.ToString();
                if (allowedActions == "Standard" || allowedActions == "Pro" || allowedActions == "Ultimate")
                {

                }
                else
                {
                    MessageBox.Show("Invalid edition");
                    return;
                }
            }
            catch (Exception error)
            {
                string finalError = error.ToString();
                MessageBox.Show("Got error! (Possible solution: No edition selected) " + finalError);
                return;
            }
            var random = new Random();
            string productKey = "";
            if (allowedActions == "Standard")
            {
                productKey = random.Next(100, 201).ToString() + usernameNumeric;
            }
            else if (allowedActions == "Pro")
            {
                productKey = random.Next(300, 601).ToString() + usernameNumeric;
            }
            else if (allowedActions == "Ultimate")
            {
                productKey = random.Next(700, 901).ToString() + usernameNumeric;
            }
            string finalProductKey = dropCharacters(productKey);
            textBox2.Text = finalProductKey;
        }
        
        static string dropCharacters(string input)
        {
            //Drop all characters that are not numbers
            string output = Regex.Replace(input, "[^.0-9]", "");
            return output;
        }

        static int convertToNumbers(string input)
        {
            var ch = input[0];
            char c = ch;
            int index = char.ToUpper(c) - 64;
            int finalIndex = index;
            return finalIndex;
        }

        static string getAlphabetNumberFromString(string input)
        {
            string finalInput = getLetters(input);
            int length = 5;
            int trueLength = finalInput.Length;
            if (trueLength < 5)
            {
                length = trueLength;
            }
            string finalSum = "";

            for (int i = 0; i < length; i++)
            {
                int differentI = i - 1;
                string differentIString = differentI.ToString();
                string currentCharacter = finalInput.Substring(i, 1);

                finalSum = finalSum + convertToNumbers(currentCharacter);
            }
            string convertedFinalSum = finalSum.ToString();
            return convertedFinalSum;
        }

        static string getLetters(string input)
        {
            var myString = input;
            var onlyLetters = new String(myString.Where(Char.IsLetter).ToArray());
            return onlyLetters.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }
    }
}