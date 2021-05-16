using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;


namespace Тест_по_теории_вероятности
{
    public partial class Test : Form
    {
        int question_count;//Счетчик вопросов
        int correct_answers;//Количество правильных ответов
        int wrong_answers;//Количество неправильных
        int question_count_rand_ev;//Счетчик вопросов из 1й темы
        int question_count_rand_iv;//Счетчик вопросов из 2й темы
        string[] array; //Массив данных

        int correct_answers_number; //номер правильного ответа

        int selected_response; //Номер выбранного ответа
        
        StreamReader Reader; // Считывание из файла

        List <RandomEvents> random_events;//Список вопросов по теме случайные события
        List<RandomEvents> random_variables;//Список вопросов по теме случайные события

        public Test()
        {
            InitializeComponent();
        }


        void Start()
        {


            question_count = 0;
            correct_answers = 0;
            wrong_answers = 0;

            array = new string[10]; //Создали массив для 10 вопросов

            /*try
            {

                question_count = 0;
                correct_answers = 0;
                wrong_answers = 0;

                array = new string[10]; //Создали массив для 10 вопросов


            }

            catch(Exception)
            {
                MessageBox.Show("Ошибка текстового файла");
            }*/
            AddQuestions();
            Quest();
        }

        void AddQuestions()
        {
            var Encoding = System.Text.Encoding.GetEncoding(65001); //Подключаем Кириллицу
            Reader = new StreamReader(Directory.GetCurrentDirectory() + @"\test.txt", Encoding); //Обращаемся к нашему файлу с вопросами
            random_events = new List<RandomEvents>();
            while(!Reader.EndOfStream)
            {
                string ex = Reader.ReadLine();//Считываем задание
                string[] ans = new string[4];//Считываем массив ответов
                for(int i = 0; i < ans.Length;i++)
                    ans[i] = Reader.ReadLine();
                int cor_ans = Convert.ToInt32(Reader.ReadLine());//Считываем правильный ответ
                
                random_events.Add(new RandomEvents(ex, ans, cor_ans));//Добавляем 
                question_count_rand_ev++;
            }
            Reader.Close();
            Reader = new StreamReader(Directory.GetCurrentDirectory() + @"\test2.txt", Encoding);//Открываем файл с темой Случайные события
            random_variables = new List<RandomEvents>();
            while (!Reader.EndOfStream)
            {
                string ex = Reader.ReadLine();//Считываем задание
                string[] ans = new string[4];//Считываем массив ответов
                for (int i = 0; i < ans.Length; i++)
                    ans[i] = Reader.ReadLine();
                int cor_ans = Convert.ToInt32(Reader.ReadLine());//Считываем правильный ответ

                random_variables.Add(new RandomEvents(ex, ans, cor_ans));
                question_count_rand_iv++;
            }
        }
        //Смена вопроса
        void Quest()
        {
            Random rnd = new Random();
            int numb_ques = rnd.Next(0, question_count_rand_ev);

            label1.Text = random_events[numb_ques].Question;

            //Варианты ответа
            radioButton1.Text = random_events[numb_ques].Answers(0);
            radioButton2.Text = random_events[numb_ques].Answers(1);
            radioButton3.Text = random_events[numb_ques].Answers(2);
            radioButton4.Text = random_events[numb_ques].Answers(3);

            correct_answers_number = random_events[numb_ques].CorrectAnswer; // Считали правильный ответ

            random_events.RemoveAt(numb_ques);
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            button1.Enabled = false;
            question_count++; //Увеличили число вопросов

            if (question_count == 5)//Допилить
                button1.Text = "Завершить";

        }

        void Состояние_переключения(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button1.Focus(); // Корректное переключение
            RadioButton p = (RadioButton)sender; //Проверяем процесс переключения
            var t = p.Name;

            selected_response = int.Parse(t.Substring(11));//Тот вариант ответа, который выбрал пользователь(возвращает номоер кнопки введеной пользователем)
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(selected_response == correct_answers_number)
            {
                correct_answers++;
            }

            else
            {
                wrong_answers++;

                array[wrong_answers] = label1.Text;

            }

            if (button1.Text == "Начать заново")
            {
                button1.Text = "Следующий вопрос";

                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                Start();
                return;
            }

            if(button1.Text == "Завершить")
            {
                Reader.Close();

                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;

                label1.Text = string.Format("Тестирование завершено.\n" +
                    "Правильных ответов : {0} из {1}.\n" +
                    "Набранные баллы: {2:F2}.", correct_answers, question_count, (correct_answers * 5.0F) / question_count);

                button1.Text = "Начать тест заново";

                var Str = "error : \n\n";

                for (int i = 1; i <= wrong_answers; i++)
                    Str += array[i] + "\n";

                if(wrong_answers != 0)
                {
                    MessageBox.Show(Str,"Тест завершен");
                }

               
            }
            if (button1.Text == "Следующий вопрос")
            {
                Quest();
            }


        }

        //Выход
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            button1.Text = "Следующий вопрос";
            button2.Text = "Выйти";

            radioButton1.CheckedChanged += new EventHandler(Состояние_переключения);
            radioButton2.CheckedChanged += new EventHandler(Состояние_переключения);
            radioButton3.CheckedChanged += new EventHandler(Состояние_переключения);
            radioButton4.CheckedChanged += new EventHandler(Состояние_переключения);


            Start();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
