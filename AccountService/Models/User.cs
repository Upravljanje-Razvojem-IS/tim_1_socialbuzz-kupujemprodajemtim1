namespace AccountService.Models
{
    public class User
    {
        /// <summary>
        /// Id of a User.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Users email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Users password.
        /// </summary>
        public string Password { get; set; }
    }
}
