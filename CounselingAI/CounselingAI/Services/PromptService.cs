using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounselingAI.Services;
public class PromptService : IPromptService
{
    public Task<List<Prompt>> GetPromptAsync()
    {
        List<Prompt> prompts = new List<Prompt>();

        prompts.Add(new Prompt()
        {
            Key = "standard",
            PromptText = "Please give me a profile of a [career] who lives in [location]? Include detailed work activities, average salary and range, labor market outlook, education requirements, skills requirements, credentials needed, compatible interests, and a description of a typical day in the life."
        });

        return Task.FromResult(prompts);
    }
}
