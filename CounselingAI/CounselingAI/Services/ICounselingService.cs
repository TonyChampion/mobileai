using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounselingAI.Services;
public interface ICounselingService
{
    Task<CounselingResponse> QueryAIAsync(CounselingRequest request, CancellationToken cancellationToken = default);
}
