-- Reference schema for GymPro. The API creates these tables automatically with EF Core EnsureCreated.
CREATE DATABASE GymManagementDB;
GO
USE GymManagementDB;
GO

CREATE TABLE Members (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(120) NOT NULL,
    Email NVARCHAR(160) NOT NULL UNIQUE,
    MobileNumber NVARCHAR(20) NOT NULL,
    Gender NVARCHAR(20) NOT NULL,
    Age INT NOT NULL,
    Address NVARCHAR(300) NOT NULL,
    JoiningDate DATE NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedDate DATETIME2 NULL
);
GO
CREATE TABLE Trainers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TrainerName NVARCHAR(120) NOT NULL,
    Email NVARCHAR(160) NOT NULL,
    MobileNumber NVARCHAR(20) NOT NULL,
    Specialization NVARCHAR(100) NOT NULL,
    Experience INT NOT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedDate DATETIME2 NULL
);
GO
CREATE TABLE Memberships (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PlanName NVARCHAR(80) NOT NULL,
    DurationInMonths INT NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Description NVARCHAR(300) NOT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedDate DATETIME2 NULL
);
GO
CREATE TABLE Payments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MemberId INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL,
    PaymentDate DATE NOT NULL,
    PaymentMode NVARCHAR(30) NOT NULL,
    PaymentStatus NVARCHAR(30) NOT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Payments_Members FOREIGN KEY(MemberId) REFERENCES Members(Id)
);
GO
