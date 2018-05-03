﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Simulation;

namespace HandwritingRecognition
{
    public partial class MainForm : Form
    {
        DrawPad Pad;
        Brain Brain;

        public MainForm()
        {
            InitializeComponent();

            Pad = new DrawPad();
            Pad.Location = new Point(10, 10);
            Pad.Size = new Size(500, 500);
            Pad.ImageSize = new Size(300, 300);
            Pad.LineWidth = 13;
            Pad.BackColor = Color.Black;
            Controls.Add(Pad);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string structure = File.ReadAllText(@"network.brain");
            Brain = new Brain(structure, 28 * 28, 10, 3, 16);
        }

        private void Ok_button_Click(object sender, EventArgs e)
        {
            Pad.RescaleImage(28, 28);
            Pad.CenterImage();
            Pad.Image.Save(@"img.png");
            //Pad.Refresh();

            float[] input = Pad.ImageToFloat();
            float[] output = Brain.Think(input);
            out_label.Text = Training.OutputNumber(output).ToString();
            output_label.Text = string.Empty;

            for (int i = 0; i < output.Length; i++)
            {
                output_label.Text += i + ": " + output[i].ToString("0.00") + "\n";
            }
        }

        private void Reset_button_Click(object sender, EventArgs e)
        {
            Pad.ResetImage();
        }
    }
}
