using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

public static class DatabaseBootstrap
{
    public static readonly string dbPath = @"C:\CapgeminiQ5\app.db";

    public static string GetConnectionString() => $"Data Source={dbPath}";


    public static void Initialize()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

        using IDbConnection connection = new SqliteConnection(GetConnectionString());
        connection.Open();

        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS contacorrente (
                idcontacorrente TEXT PRIMARY KEY,
                numero INTEGER NOT NULL UNIQUE,
                nome TEXT NOT NULL,
                ativo INTEGER NOT NULL DEFAULT 0,
                CHECK (ativo IN (0,1))
            );

            CREATE TABLE IF NOT EXISTS movimento (
                idmovimento TEXT PRIMARY KEY,
                idcontacorrente TEXT NOT NULL,
                datamovimento TEXT NOT NULL,
                tipomovimento TEXT NOT NULL,
                valor REAL NOT NULL,
                CHECK (tipomovimento IN ('C','D')),
                FOREIGN KEY (idcontacorrente) REFERENCES contacorrente(idcontacorrente)
            );

            CREATE TABLE IF NOT EXISTS idempotencia (
                chave_idempotencia TEXT PRIMARY KEY,
                requisicao TEXT,
                resultado TEXT
            );
        ");

        connection.Execute(@"
            INSERT OR IGNORE INTO contacorrente (idcontacorrente, numero, nome, ativo) VALUES
            ('B6BAFC09-6967-ED11-A567-055DFA4A16C9', 123, 'Katherine Sanchez', 1),
            ('FA99D033-7067-ED11-96C6-7C5DFA4A16C9', 456, 'Eva Woodward', 1),
            ('382D323D-7067-ED11-8866-7D5DFA4A16C9', 789, 'Tevin Mcconnell', 1),
            ('F475F943-7067-ED11-A06B-7E5DFA4A16C9', 741, 'Ameena Lynn', 0),
            ('BCDACA4A-7067-ED11-AF81-825DFA4A16C9', 852, 'Jarrad Mckee', 0),
            ('D2E02051-7067-ED11-94C0-835DFA4A16C9', 963, 'Elisha Simons', 0);
        ");
    }
}
