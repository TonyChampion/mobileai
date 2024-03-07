using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounselingAI.Services;
public interface IPromptService
{
    Task<List<Prompt>> GetPromptAsync();
}
