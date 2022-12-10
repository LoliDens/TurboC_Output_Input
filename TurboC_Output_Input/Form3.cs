using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace TurboC_Output_Input
{
   
    public partial class Form3 : Form
    {
        List<Question> questions = new List<Question>()
        {
            new Question("Что означает from в функции ioperm(from, num, turn on)?",new string[]{ "первый порт", "количество подряд идущих", "последний аргумент", "доступ к портам"},"первый порт"),
            new Question("Нужно ли иметь права root для вызова ioperm() в стандартном \r\nспособе?",new string[]{ "да", "нет", "зависит от ситуации"},"да"),
            new Question("Что представляет из себя процедура ввода/вывода в стандартном \r\nспособе?",new string[]{ "библиотеки", "встроенные макроопределения", "исходный текст"},"встроенные макроопределения")
        };

        int _amountRigthAnswers = 0;
        int _numberQuedtion = 0;
        bool isWork = true;
        bool isExit = false;

        public Form3()
        {
            InitializeComponent();            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            questions[0].ShowQuetion(label1, radioButton1, radioButton2, radioButton3, radioButton4);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (isExit) 
            {
                Form1 form = new Form1();
                form.Show();
                this.Hide();
            }
           
            if (isWork)
            {
                bool isCorrectAnswer = false;

                switch (questions[_numberQuedtion].NumberCorrectAnswer) 
                {
                    case 1:
                        isCorrectAnswer = radioButton1.Checked;
                        break;
                    case 2:
                        isCorrectAnswer = radioButton2.Checked;
                        break;
                    case 3:
                        isCorrectAnswer = radioButton3.Checked;
                        break;
                    case 4:
                        isCorrectAnswer = radioButton4.Checked;
                        break;
                }

                if (isCorrectAnswer)
                    _amountRigthAnswers++;

                if (_numberQuedtion + 1 == questions.Count - 1) 
                {
                    button1.Text = "Завершить тест";
                }

                _numberQuedtion ++;
                questions[_numberQuedtion].ShowQuetion(label1, radioButton1, radioButton2, radioButton3, radioButton4);

                if (_numberQuedtion + 1 == questions.Count) 
                {
                    isWork = false;
                }
                 
            }
            else 
            {
                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;

                label1.Text = $"Тест успешно пройдет.\nВы ответили на {_amountRigthAnswers} из {questions.Count} вопросов";
                label1.Location = new Point(250,150);

                button1.Text = "Выйти в меню";
                isExit = true;
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }

    class Question
    {
        public string TextQuestion { get; private set; }
        public string[] Answers { get; private set; }
        public string CorrectAnswer { get; private set; }
        public int AmountAnswers { get; private set; }
        public int NumberCorrectAnswer { get; private set; }

        public Question(string textQuestion, string[] answers, string correctAnswer)
        {
            TextQuestion = textQuestion;
            Answers = answers;
            CorrectAnswer = correctAnswer;
            AmountAnswers = answers.Length;

            for (int i = 0; i < Answers.Length; i++) 
            {
                var answer = answers[i];

                if (answer == CorrectAnswer) 
                {
                    NumberCorrectAnswer = i + 1;
                    break;
                }
            }
        }

        public void ShowQuetion(Label lab1, RadioButton rb1, RadioButton rb2, RadioButton rd3, RadioButton rd4)
        {
            lab1.Text = TextQuestion;            

            rb1.Text = Answers[0];
            rb2.Text = Answers[1];

            if (AmountAnswers == 2)
            {
                rd3.Visible = false;
                rd4.Visible = false;
            }
            else if (AmountAnswers == 3)
            {
                rd4.Visible = false;
                rd3.Visible = true;
                rd3.Text = Answers[2];
            }
            else
            {
                rd3.Visible = true;
                rd4.Visible = true;
                rd3.Text = Answers[2];
                rd4.Text = Answers[3];
            }
        }
    }
}

