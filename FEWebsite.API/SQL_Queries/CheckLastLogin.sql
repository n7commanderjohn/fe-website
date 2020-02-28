-- SQLite
SELECT Id, Username, PasswordHash, PasswordSalt, Birthday, AccountCreated, LastLogin, Gender, AboutMe, Alias, Email
FROM "Users"
ORDER by LastLogin desc;
