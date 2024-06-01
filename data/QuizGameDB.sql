CREATE DATABASE QuizGameDB;
GO

CREATE TABLE QuizItems  (
    QuestionID INT PRIMARY KEY IDENTITY(1,1),
    QuestionText NVARCHAR(max) NOT NULL,
    AnswerA NVARCHAR(max) NOT NULL,
	AnswerB NVARCHAR(max) NOT NULL,
	AnswerC NVARCHAR(max) NOT NULL,
	AnswerD NVARCHAR(max) NOT NULL,
	AnswerCorrect NVARCHAR(max) NOT NULL,
);

CREATE TABLE GroupPlay  (
    GroupID INT PRIMARY KEY IDENTITY(1,1),
    GroupName NVARCHAR(max) NOT NULL,
    Score Int NOT NULL DEFAULT 100, 
);

CREATE TABLE Challenge (
    ChallengeID INT PRIMARY KEY IDENTITY(1,1),
    Content NVARCHAR(max) NOT NULL, 
	ChallengeType NVARCHAR(max) NOT NULL, 
	ChallengeValue int
);
