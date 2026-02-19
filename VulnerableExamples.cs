using System;
using Microsoft.EntityFrameworkCore;

public class VulnerableExamples
{
    private readonly DbContext _db;

    public VulnerableExamples(DbContext db)
    {
        _db = db;
    }

    // ✅ Deve ser detectado como "Hardcoded secret"
    public string GetApiKey()
    {
        return "sk_live_1234567890_SUPER_SECRET_KEY_EXAMPLE";
    }

    // ✅ Deve ser detectado como "SQL Injection" (string concatenada)
    public void UnsafeSqlQuery(string userInput)
    {
        var sql = "SELECT * FROM Users WHERE Name = '" + userInput + "'";
        _db.Database.ExecuteSqlRaw(sql);
    }

    // ✅ Deve ser detectado como "SQL Injection" (interpolação)
    public void UnsafeSqlQuery2(string userInput)
    {
        _db.Database.ExecuteSqlRaw($"SELECT * FROM Users WHERE Name = '{userInput}'");
    }
}
