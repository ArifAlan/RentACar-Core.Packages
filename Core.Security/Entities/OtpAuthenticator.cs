using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Entities
{
    public class OtpAuthenticator : Entity<int>
    {
        public int UserId { get; set; }
        public byte[] SecretKey { get; set; }
        public bool IsVerified { get; set; }

        public virtual User User { get; set; } = null!;

        public OtpAuthenticator()
        {
            SecretKey = Array.Empty<byte>();
        }

        public OtpAuthenticator(int userId, byte[] secretKey, bool isVerified)
        {
            UserId = userId;
            SecretKey = secretKey;
            IsVerified = isVerified;
        }

        public OtpAuthenticator(int id, int userId, byte[] secretKey, bool isVerified)
            : base(id)
        {
            UserId = userId;
            SecretKey = secretKey;
            IsVerified = isVerified;
        }
    }
}
//Yazılımda OTP (One-Time Password), genellikle güvenli kimlik doğrulama süreçlerinde kullanılan, bir defa kullanımlık olarak üretilen şifrelerdir. Bu şifreler genellikle belirli bir süre geçerlidir ve her kullanımda değişir, böylece güvenliği artırır.



