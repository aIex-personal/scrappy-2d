using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trivia_game
{
    //this is Trivia game
    public partial class Form1 : Form
    {
        // quiz game variables
        int correctAnswer;
        int questionNumber = 1;
        int score;
        int percentage;
        int totalQuestions;
        public Form1()
        {
            InitializeComponent();
            askQuestion(questionNumber);
            totalQuestions = 5;
        }
        private void ClickAnswerEvent(object sender, EventArgs e)
        {
            var senderObject = (Button)sender;
            int buttonTag = Convert.ToInt32(senderObject.Tag);
            if (buttonTag == correctAnswer)
            {
                score++;
            }
            if (questionNumber == totalQuestions)
            {
                // work out the percentage
                percentage = (int)Math.Round((double)(score * 100) / totalQuestions);
                MessageBox.Show(
                    "Trivia Game Ended!" + Environment.NewLine +
                    "You have answered " + score + " questions correctly." + Environment.NewLine +
                    "Your total percentage is " + percentage + "%" + Environment.NewLine +
                    "Click OK to play again"
                    );
                
                score = 0;
                questionNumber = 0;
                askQuestion(questionNumber);
            }
            questionNumber++;
            askQuestion(questionNumber);
        }
        private void askQuestion(int qnum)
        {
            switch (qnum)
            {
                case 1:
                    pictureBox1.Image = Properties.Resources.questions;
                    lblQuestion.Text = "What is the definition of Education for Sustainable Development (ESD)?"; // 125 characters right now:::
                    button1.Text = "Teaching about environmental conservation";
                    button2.Text = "Promoting a sustainable future through education";
                    button3.Text = "Focusing on economic development";
                    button4.Text = "None of the above";
                    correctAnswer = 2;
                    break;
                case 2:
                    pictureBox1.Image = Properties.Resources.questions;
                    lblQuestion.Text = "What are the three pillars of sustainability?";
                    button1.Text = "Social, environmental, and economic";
                    button2.Text = "Environmental, cultural, and technological";
                    button3.Text = "Economic, political, and cultural";
                    button4.Text = "None of the above";
                    correctAnswer = 1;
                    break;
                case 3:
                    pictureBox1.Image = Properties.Resources._1;
                    lblQuestion.Text = "Which of the following is a key principle of ESD?";
                    button1.Text = "Encouraging competition ";
                    button2.Text = "Focusing only on environmental issues";
                    button3.Text = "Promoting critical thinking";
                    button4.Text = "Ignoring social and economic issues";
                    correctAnswer = 3;
                    break;
                case 4:
                    pictureBox1.Image = Properties.Resources._2;
                    lblQuestion.Text = "What is the role of educators in promoting ESD?";
                    button1.Text = "To provide information about environmental issues";
                    button2.Text = "To promote critical thinking and problem-solving skills";
                    button3.Text = "To encourage sustainable behavior";
                    button4.Text = "All of the above";
                    correctAnswer = 4;
                    break;
                case 5:
                    pictureBox1.Image = Properties.Resources._3;
                    lblQuestion.Text = "Whats the name of this game?";
                    button1.Text = "Gears of War";
                    button2.Text = "Assassins Creed";
                    button3.Text = "Uncharted";
                    button4.Text = "Call of Duty";
                    correctAnswer = 1;
                    break;
                case 6:
                    pictureBox1.Image = Properties.Resources._4;
                    lblQuestion.Text = "What is the main characters name in the game above?";
                    button1.Text = "Drake";
                    button2.Text = "Lara Croft";
                    button3.Text = "Master Cheif";
                    button4.Text = "Markus";
                    correctAnswer = 3;
                    break;
                case 7:
                    pictureBox1.Image = Properties.Resources._5;
                    lblQuestion.Text = "Who was Geralt looking for in Witcher 3?";
                    button1.Text = "Victoria";
                    button2.Text = "Mr Donut";
                    button3.Text = "Ciri";
                    button4.Text = "Yennifer";
                    correctAnswer = 3;
                    break;
                case 8:
                    pictureBox1.Image = Properties.Resources.questions;
                    lblQuestion.Text = "Which city is the captial city of Denmark?";
                    button1.Text = "Ottawa";
                    button2.Text = "Berlin";
                    button3.Text = "London";
                    button4.Text = "Copenhagen";
                    correctAnswer = 4;
                    break;
            }
        }
    }
}
