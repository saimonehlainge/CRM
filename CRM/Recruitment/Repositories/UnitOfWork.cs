using Microsoft.AspNetCore.Identity;
using Recruitment.Areas.Identity.Data;
using Recruitment.Data;
using Recruitment.Services;

#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
namespace Recruitment.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICDDRepository CDDRepository { get; }
        IProvinceRepository ProvincesRepository { get; }
        IAnnouncementRepository AnnouncementRepository { get; }
        IPositionRepository PositionRepository { get; }
        IPrefixRepositoy PrefixRepositoy { get; }
        IReligionRepository ReligionRepository { get; }
        INationalityRepository NationalityRepository { get; }
        IMilitaryStatusRepository MilitaryStatusRepository { get; }
        IMarriedStatusRepository MarriedStatusRepository { get; }
        IQualificationRepository QualificationRepository { get; }
        ILanguageAbilityRepository LanguageAbilityRepository { get; }
        IHistoryCovidRepository HistoryCovidRepository { get; }
        IProjectRepository ProjectRepository { get; }
        IWorkOtherProvincesRepository WorkOtherProvincesRepository { get; }
        IRecruitStatusRepository RecruitStatusRepository { get; }
        IUsersRepository UsersRepository { get; }
        IDistrictsRepository DistrictRepository { get; }
        ISubdistrictsRepository SubdistrictsRepository { get; }
        ISummaryRepositoy SummaryRepositoy { get; }
        ICalendarRepository CalendarRepository { get; }
        IEmailSettingRepository EmailSettingRepository { get; }
        IRoleRepository RoleRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        IDepartmentRepository DepartmentRepository { get; }
        IPermissionRepository PermissionRepository { get; }
        IPermissionDetailRepository PermissionDetailRepository { get; }
        IPermissionSettingRepository PermissionSettingRepository { get; }
        ITeamRepository TeamRepository { get; }
        ICommentCDDRepository CommentCDDRepository { get; }
        ISetKpiCommRepository SetKpiCommRepository { get; }
        ICompanyRepository CompanyRepository { get; }
        IRecruitStatusFromRepository RecruitStatusFromRepository { get; }
        Task CompleteAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        #region Readonly
        private readonly RecruitmentContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly INotificationHub _notificationHub;
        private readonly UserManager<RecruitmentUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILoggerHelperRepository _loggerHelperRepository;
        #endregion

        public UnitOfWork(RecruitmentContext Context, INotificationHub notificationHub, UserManager<RecruitmentUser> userManager, IWebHostEnvironment webHostEnvironment, ILoggerHelperRepository loggerHelperRepository)
        {
            _context = Context ?? throw new ArgumentNullException(nameof(Context));
            _notificationHub = notificationHub ?? throw new ArgumentNullException(nameof(notificationHub));
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _loggerHelperRepository = loggerHelperRepository;
        }

        #region Properties
        private CDDRepository? _CDDRepository;
        private ProvinceRepository? _ProvincesRepository;
        private AnnouncementRepository? _AnnouncementRepository;
        private PositionRepository? _PositionRepository;
        private PrefixRepositoy? _PrefixRepository;
        private ReligionRepository? _ReligionRepository;
        private NationalityRepository? _nationalityRepository;
        private MilitaryStatusRepository? _militaryStatusRepository;
        private MarriedStatusRepository? _marriedStatusRepository;
        private QualificationRepository? _qualificationRepository;
        private LanguageAbilityRepository? _languageAbilityRepository;
        private HistoryCovidRepository? _historyCovidRepository;
        private ProjectRepository? _projectRepository;
        private WorkOtherProvincesRepository _workOtherProvincesRepository;
        private RecruitStatusRepository _recruitStatusRepository;
        private UsersRepository _usersRepository;
        private DistrictsRepository _districtsRepository;
        private SubdistrictsRepository _subdistrictsRepository;
        private SummaryRepositoy _summaryRepositoy;
        private CalendarRepository _calendarRepository;
        private EmailSettingRepository _emailSettingRepository;
        private RoleRepository _roleRepository;
        private UserRoleRepository _userRoleRepository;
        private DepartmentRepository _departmentRepository;
        private PermissionRepository _permissionRepository;
        private PermissionDetailRepository _permissionDetailRepository;
        private PermissionSettingRepository _permissionSettingRepository;
        private TeamRepository _teamRepository;
        private CommentCDDRepository _commentCDDRepository;
        private SetKpiCommRepository _setKpiCommRepository;
        private CompanyRepository _companyRepository;
        private RecruitStatusFromRepository _recruitStatusFromRepository;

        public ICDDRepository CDDRepository => _CDDRepository ??= new CDDRepository(_context, _accessor, _notificationHub, _webHostEnvironment, _loggerHelperRepository);
        public IProvinceRepository ProvincesRepository => _ProvincesRepository ??= new ProvinceRepository(_context);
        public IAnnouncementRepository AnnouncementRepository => _AnnouncementRepository ??= new AnnouncementRepository(_context);
        public IPositionRepository PositionRepository => _PositionRepository ??= new PositionRepository(_context);
        public IPrefixRepositoy PrefixRepositoy => _PrefixRepository ??= new PrefixRepositoy(_context);
        public IReligionRepository ReligionRepository => _ReligionRepository ??= new ReligionRepository(_context);
        public INationalityRepository NationalityRepository => _nationalityRepository ??= new NationalityRepository(_context);
        public IMilitaryStatusRepository MilitaryStatusRepository => _militaryStatusRepository ??= new MilitaryStatusRepository(_context);
        public IMarriedStatusRepository MarriedStatusRepository => _marriedStatusRepository ??= new MarriedStatusRepository(_context);
        public IQualificationRepository QualificationRepository => _qualificationRepository ??= new QualificationRepository(_context);
        public ILanguageAbilityRepository LanguageAbilityRepository => _languageAbilityRepository ??= new LanguageAbilityRepository(_context);
        public IHistoryCovidRepository HistoryCovidRepository => _historyCovidRepository ??= new HistoryCovidRepository(_context);
        public IProjectRepository ProjectRepository => _projectRepository ??= new ProjectRepository(_context);
        public IWorkOtherProvincesRepository WorkOtherProvincesRepository => _workOtherProvincesRepository ??= new WorkOtherProvincesRepository(_context);
        public IRecruitStatusRepository RecruitStatusRepository => _recruitStatusRepository ??= new RecruitStatusRepository(_context);
        public IUsersRepository UsersRepository => _usersRepository ??= new UsersRepository(_context, _userManager, _webHostEnvironment);
        public IDistrictsRepository DistrictRepository => _districtsRepository ??= new DistrictsRepository(_context);
        public ISubdistrictsRepository SubdistrictsRepository => _subdistrictsRepository ??= new SubdistrictsRepository(_context);
        public ISummaryRepositoy SummaryRepositoy => _summaryRepositoy ??= new SummaryRepositoy(_context);
        public ICalendarRepository CalendarRepository => _calendarRepository ??= new CalendarRepository(_context);
        public IEmailSettingRepository EmailSettingRepository => _emailSettingRepository ??= new EmailSettingRepository(_context);
        public IRoleRepository RoleRepository => _roleRepository ??= new RoleRepository(_context);
        public IUserRoleRepository UserRoleRepository => _userRoleRepository ??= new UserRoleRepository(_context);
        public IDepartmentRepository DepartmentRepository => _departmentRepository ??= new DepartmentRepository(_context);
        public IPermissionRepository PermissionRepository => _permissionRepository ??= new PermissionRepository(_context);
        public IPermissionDetailRepository PermissionDetailRepository => _permissionDetailRepository ??= new PermissionDetailRepository(_context);
        public IPermissionSettingRepository PermissionSettingRepository => _permissionSettingRepository ??= new PermissionSettingRepository(_context);
        public ITeamRepository TeamRepository => _teamRepository ??= new TeamRepository(_context);
        public ICommentCDDRepository CommentCDDRepository => _commentCDDRepository ??= new CommentCDDRepository(_context);
        public ISetKpiCommRepository SetKpiCommRepository => _setKpiCommRepository ??= new SetKpiCommRepository(_context);
        public ICompanyRepository CompanyRepository => _companyRepository ??= new CompanyRepository(_context);
        public IRecruitStatusFromRepository RecruitStatusFromRepository => _recruitStatusFromRepository ??= new RecruitStatusFromRepository(_context);

        #endregion

        #region Methods
        public async Task CompleteAsync() => await _context.SaveChangesAsync();

        #endregion

        #region Implements IDisposable

        #region Private Dispose Fields

        private bool _disposed;

        #endregion
        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
