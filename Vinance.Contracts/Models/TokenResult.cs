namespace Vinance.Contracts.Models
{
    public class TokenResult
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
    }
}