﻿namespace CourseManagement.Data.Models
{
    using System;
    using System.Collections.Generic;

    public class ApplicationUser
    {
        internal ApplicationUser()
        {
            this.IsBlocked = false;

            this.Courses = new List<Course>();

            this.Favorites = new List<FavoriteCourse>();
        }

        internal ApplicationUser(string username, string password, string firstName, string lastName, int roleId)
            : this()
        {
            this.Username = username;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.RoleId = roleId;
        }

        internal ApplicationUser(int id, string username, string password, string firstName, string lastName, int roleId)
            : this(username, password, firstName, lastName, roleId)
        {
            //Validate_If_Id_IsNull(id);

            this.Id = id;
        }

        public int Id { get; private set; }

        public string Username { get; private set; }

        public bool IsBlocked { get; private set; }

        public string Password { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public int RoleId { get; private set; }

        public virtual Role Role { get; private set; }

        public string Token { get; private set; }

        public virtual ICollection<Course> Courses { get; private set; }

        public virtual ICollection<FavoriteCourse> Favorites { get; private set; }

        //METHODS

        public void UpdateToken(string token)
        {
            this.Token = token;
        }

        public void UpdatePassword(string password)
        {
            this.UpdateStringField(this.Password, password);
        }

        public void UpdateFirstName(string firstName)
        {
            this.UpdateStringField(this.FirstName, firstName);
        }

        public void UpdateLastName(string lastName)
        {
            this.UpdateStringField(this.LastName, lastName);
        }

        public void Block()
        {
            this.IsBlocked = true;
        }

        public void Unblock()
        {
            this.IsBlocked = false;
        }

        private void UpdateStringField(string initial_field, string updated_field)
        {
            if (initial_field != updated_field)
            {
                initial_field = updated_field;
            }
        }
    }
}
