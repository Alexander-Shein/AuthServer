using System.Collections.Generic;
using System.Linq;

namespace AuthGuard.Core
{
    public class OperationResult
    {
        public bool Succeed => Errors.Any();
        public ICollection<Error> Errors { get; set; } = new List<Error>();
    }
}