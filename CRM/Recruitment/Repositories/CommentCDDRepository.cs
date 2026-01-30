using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
    public interface ICommentCDDRepository : IGenericRepository<CommentCDD>
    {

    }

    public class CommentCDDRepository : GenericRepository<CommentCDD>, ICommentCDDRepository
    {
        public CommentCDDRepository(RecruitmentContext context) : base(context) { }
    }
}
