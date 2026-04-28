// <copyright file="QuizEngine.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CSharpQuizWPF.Core
{
    using System.Collections.Generic;
    using CSharpQuizWPF.Models;

    /// <summary>
    /// Управляет логикой прохождения тестов по уровням.
    /// </summary>
    public class QuizEngine
    {
        private readonly Dictionary<int, List<Question>> levels = new Dictionary<int, List<Question>>();
        private int currentLevel;
        private int score;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuizEngine"/> class.
        /// Инициализирует новый экземпляр класса <see cref="QuizEngine"/> с тестовыми данными.
        /// </summary>
        public QuizEngine()
        {
            this.levels[1] = new List<Question>
            {
                new Question { Id = 1, Text = "Какой тип для целых чисел?", Options = new string[] { "int", "float", "string", "bool" }, CorrectIndex = 0 },
                new Question { Id = 2, Text = "Что выводит Console.WriteLine(\"Hi\")?", Options = new string[] { "Hi", "Error", "Nothing", "Hi\\n" }, CorrectIndex = 0 },
            };
            this.levels[2] = new List<Question>
            {
                new Question { Id = 3, Text = "Модификатор доступа только внутри класса?", Options = new string[] { "public", "private", "protected", "internal" }, CorrectIndex = 1 },
                new Question { Id = 4, Text = "Что такое 'var'?", Options = new string[] { "Тип данных", "Ключевое слово неявной типизации", "Интерфейс", "Атрибут" }, CorrectIndex = 1 },
            };
        }

        /// <summary>
        /// Запускает указанный уровень теста, сбрасывая прогресс.
        /// </summary>
        /// <param name="level">Номер уровня для запуска.</param>
        /// <returns>True, если уровень найден и запущен; иначе False.</returns>
        public bool StartLevel(int level)
        {
            if (!this.levels.ContainsKey(level))
            {
                return false;
            }

            this.currentLevel = level;
            this.SetCurrentNumber(0);
            this.SetScore(0);
            return true;
        }

        /// <summary>
        /// Возвращает текущий вопрос теста.
        /// </summary>
        /// <returns>Объект текущего вопроса.</returns>
        public Question GetCurrent() => this.levels[this.currentLevel][this.GetCurrentNumber()];

        /// <summary>
        /// Проверяет, есть ли следующие вопросы в текущем уровне.
        /// </summary>
        /// <returns>True, если есть следующие вопросы; иначе False.</returns>
        public bool HasNext() => this.GetCurrentNumber() < this.levels[this.currentLevel].Count;

        /// <summary>
        /// Отправляет выбранный ответ и проверяет его правильность.
        /// </summary>
        /// <param name="selectedIndex">Индекс выбранного варианта ответа.</param>
        /// <returns>True, если ответ правильный; иначе False.</returns>
        public bool SubmitAnswer(int selectedIndex)
        {
            bool correct = selectedIndex == this.GetCurrent().CorrectIndex;
            if (correct)
            {
                this.SetScore(this.GetScore() + 1);
            }

            this.SetCurrentNumber(this.GetCurrentNumber() + 1);
            return correct;
        }

        /// <summary>
        /// Gets количество правильных ответов в текущем уровне.
        /// </summary>
        /// <returns>Текущее количество набранных баллов.</returns>
        public int GetScore()
        {
            return this.score;
        }

        /// <summary>
        /// Sets количество правильных ответов в текущем уровне.
        /// </summary>
        /// <param name="value">Новое значение счёта.</param>
        private void SetScore(int value)
        {
            this.score = value;
        }

        /// <summary>
        /// Gets общее количество вопросов в текущем уровне.
        /// </summary>
        /// <returns>Общее число вопросов в уровне.</returns>
        public int GetTotal()
        {
            return this.levels[this.currentLevel].Count;
        }

        private int currentNumber;

        /// <summary>
        /// Gets номер текущего вопроса (индекс).
        /// </summary>
        /// <returns>Индекс текущего вопроса, начиная с нуля.</returns>
        public int GetCurrentNumber()
        {
            return this.currentNumber;
        }

        /// <summary>
        /// Sets номер текущего вопроса (индекс).
        /// </summary>
        /// <param name="value">Новый индекс текущего вопроса.</param>
        private void SetCurrentNumber(int value)
        {
            this.currentNumber = value;
        }
    }
}