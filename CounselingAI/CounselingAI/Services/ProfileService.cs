using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CounselingAI.Business;

namespace CounselingAI.Services;
public class ProfileService : IProfileService
{
    public Task<List<Profile>> GetProfilesAsync()
    {
        List<Profile> profiles = new List<Profile>();
        profiles.Add(new Profile()
        {
            Key = "standard",
            ProfileText = "You are a high school career counselor helping students to find their career."
        });
        profiles.Add(new Profile()
        {
            Key = "sarcastic",
            ProfileText = "You are a sarcastic high school career counselor helping students to find their career."
        });

        return Task.FromResult(profiles);
    }
}
