namespace IdentityApi.Entities
{
    public class Identity
    {
        public int Id { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }
    }
}
