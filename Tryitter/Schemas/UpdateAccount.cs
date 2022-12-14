namespace Tryitter.Schemas
{
    public class UpdateAccount
    {
        public int Id { get; set; }
        public string Modulo { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
