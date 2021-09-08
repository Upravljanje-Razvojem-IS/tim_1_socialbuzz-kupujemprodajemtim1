namespace AccountService.Dtos
{
    public class AccountUpdateDto
    {
        /// <summary>
        /// Id of an Account.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Balance of an Account.
        /// </summary>
        public decimal Total { get; set; }
    }
}
