// <copyright file="AuthManager.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CSharpQuizWPF.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Управляет аутентификацией и регистрацией пользователей.
    /// </summary>
    public class AuthManager
    {
        private readonly List<User> users = new List<User>();
        private int nextId = 1;

        /// <summary>
        /// Регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Созданный объект пользователя.</returns>
        /// <exception cref="ArgumentException">Если поля пустые.</exception>
        /// <exception cref="InvalidOperationException">Если пользователь уже существует.</exception>
        public User Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Поля не могут быть пустыми");
            }

            if (this.users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Пользователь уже существует");
            }

            var user = new User { Id = this.nextId++, Username = username, Password = password };
            this.users.Add(user);
            return user;
        }

        /// <summary>
        /// Выполняет вход пользователя по имени и паролю.
        /// </summary>
        /// <param name="username">Имя пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Объект пользователя при успешном входе.</returns>
        /// <exception cref="InvalidOperationException">Если имя или пароль неверны.</exception>
        public User Login(string username, string password)
        {
            var user = this.users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            if (user == null || user.Password != password)
            {
                throw new InvalidOperationException("Неверное имя пользователя или пароль");
            }

            return user;
        }

        /// <summary>
        /// Проверяет, является ли пароль достаточно надёжным.
        /// </summary>
        /// <param name="password">Пароль для проверки.</param>
        /// <returns>True, если пароль надёжный; иначе False.</returns>
        public bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            return password.Length >= 8 && password.Any(char.IsDigit);
        }
    }
}