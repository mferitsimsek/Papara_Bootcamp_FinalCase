namespace Papara.CaptainStore.Application.Tools
{
    public class GenerateRandomCode
    {
        public static string GenerateRandomCouponCode(string startsWith)
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var randomCode = startsWith + new string(Enumerable.Repeat(allowedChars, 7)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return randomCode;
        }
    }
   
}
