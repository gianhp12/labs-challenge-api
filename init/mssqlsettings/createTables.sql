USE [LabsChallengeDb]
GO
CREATE TABLE Access_Control.Users(
    Id INT IDENTITY(1,1) PRIMARY KEY, 
    Name NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL,
    IsEmailConfirmed BIT NOT NULL,
    EmailConfirmationToken NVARCHAR(255) NOT NULL,
    EmailTokenRequestedAt DATETIME NOT NULL,
    CreatedAt DATETIME NOT NULL,
    UpdatedAt DATETIME NOT NULL
)
GO