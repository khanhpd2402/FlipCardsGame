using System;
using System.Collections.Generic;
using FlipCardsGame.Models;

public class QuestionManager
{
    private static QuestionManager _instance;
    private List<QuizItem> _availableQuestions;
    private QuizItem _currentQuestion;

    // Singleton pattern
    public static QuestionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new QuestionManager();
            }
            return _instance;
        }
    }

    private QuestionManager()
    {
        _availableQuestions = new List<QuizItem>();
    }

    public void LoadQuestions(List<QuizItem> questions)
    {
        _availableQuestions = new List<QuizItem>(questions);
    }

    public QuizItem GetRandomQuestion()
    {
        if (_availableQuestions.Count == 0)
        {
            _currentQuestion = null; // Không có câu hỏi nào để trả về
            return null;
        }

        var random = new Random();
        int index = random.Next(0, _availableQuestions.Count);
        _currentQuestion = _availableQuestions[index];

        return _currentQuestion;
    }

    public void RemoveQuestion(QuizItem question)
    {
        _availableQuestions.Remove(question);
    }

    public void ResetQuestions()
    {
        _availableQuestions.Clear();
    }

    public QuizItem GetCurrentQuestion()
    {
        return _currentQuestion;
    }
}