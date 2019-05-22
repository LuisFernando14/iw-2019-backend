﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorialMoya001.Hubs
{
    public interface IActionHub
    {
        // Task QuestionScoreChange(Guid questionId, int score);
        // Task AnswerCountChange(Guid questionId, int answerCount);
        // Task AnswerAdded(Answer answer);
        // Task QuestionAdded(Question question);

        Task DeviceStatusChange(string deviceRowKey, string devicePartitionKey, bool status);
        Task DeviceIsOnChange(string deviceRowKey, string devicePartitionKey, bool isOn);
    }
}