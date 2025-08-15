namespace SmartFit.Model
{
    public class User
    {
        public Guid Id { get; set; }             // 使用者唯一識別碼
        public string Name { get; set; }         // 使用者名稱
        public int Age { get; set; }             // 年齡
        public double Height { get; set; }       // 身高 (cm)
        public double CurrentWeight { get; set; }// 當前體重 (kg)
        public double WeightGoal { get; set; }   // 目標體重 (kg)
        public string PasswordHash { get; set; } // 密碼加密
        public string Email { get; set; }        // Email（登入用）
        public DateTime CreatedAt { get; set; }  // 建立時間
    }
}
