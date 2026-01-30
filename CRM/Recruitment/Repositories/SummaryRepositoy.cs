using Recruitment.Areas.Identity.Data;
using Recruitment.Data;

namespace Recruitment.Repositories
{
	public interface ISummaryRepositoy : IGenericRepository<Summary>
	{ 
	
	}

	public class SummaryRepositoy : GenericRepository<Summary>, ISummaryRepositoy
    {
		public SummaryRepositoy(RecruitmentContext context) : base(context) { }
	}
}
