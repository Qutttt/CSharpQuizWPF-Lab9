// <copyright file="Question.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CSharpQuizWPF.Models
{
    /// <summary>
    /// Представляет вопрос теста с вариантами ответов.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Gets or sets уникальный идентификатор вопроса.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets текст вопроса.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets массив вариантов ответов.
        /// </summary>
        public string[] Options { get; set; }

        /// <summary>
        /// Gets or sets индекс правильного ответа.
        /// </summary>
        public int CorrectIndex { get; set; }
    }
}