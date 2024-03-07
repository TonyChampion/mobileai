using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounselingAI.Business;

namespace CounselingAI.Services;
public interface IProfileService
{
    Task<List<Profile>> GetProfilesAsync();
}

