// <copyright file="User.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CSharpQuizWPF.Core
{
    /// <summary>
    /// Представляет пользователя системы.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets уникальный идентификатор пользователя.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets имя пользователя.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets пароль пользователя.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets роль пользователя.
        /// </summary>
        public string Role { get; set; } = "User";
    }
}