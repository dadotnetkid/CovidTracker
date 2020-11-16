using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Models.Repository
{
    public partial class UnitOfWork : IDisposable
    {
        public DbContext context;

        public UnitOfWork()
        {
            context = ModelDb.Create();//(/*DataSource.ConnectionString ?? context.Database.Connection.ConnectionString*/);
        }

        public UnitOfWork(DbContext dbContext)
        {
            this.context = dbContext;
        }
        public UnitOfWork(UnitOfWorkSettings settings)
        {

        }

        private GenericRepository<Patients> _PatientsRepo;
        public GenericRepository<Patients> PatientsRepo
        {
            get
            {
                if (this._PatientsRepo == null)
                    this._PatientsRepo = new GenericRepository<Patients>(context);
                return _PatientsRepo;
            }
            set { _PatientsRepo = value; }
        }
        private GenericRepository<BatchNumbers> _BatchNumbersRepo;
        public GenericRepository<BatchNumbers> BatchNumbersRepo
        {
            get
            {
                if (this._BatchNumbersRepo == null)
                    this._BatchNumbersRepo = new GenericRepository<BatchNumbers>(context);
                return _BatchNumbersRepo;
            }
            set { _BatchNumbersRepo = value; }
        }
        private GenericRepository<Functions> _FunctionsRepo;
        public GenericRepository<Functions> FunctionsRepo
        {
            get
            {
                if (this._FunctionsRepo == null)
                    this._FunctionsRepo = new GenericRepository<Functions>(context);
                return _FunctionsRepo;
            }
            set { _FunctionsRepo = value; }
        }

        public UnitOfWork(bool lazyLoadingEnabled, bool proxyCreationEnabled)
        {
            context = ModelDb.Create(DataSource.ConnectionString);//(/*DataSource.ConnectionString ?? context.Database.Connection.ConnectionString*/);
            this.context.Configuration.LazyLoadingEnabled = lazyLoadingEnabled;
            this.context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        }

        

        private GenericRepository<Users> usersRepo;
        public GenericRepository<Users> UsersRepo
        {
            get
            {
                if (this.usersRepo == null)
                    this.usersRepo = new GenericRepository<Users>(context);
                return usersRepo;
            }
            set { usersRepo = value; }
        }

        private GenericRepository<UserRoles> userRolesRepo;
        public GenericRepository<UserRoles> UserRolesRepo
        {
            get
            {
                if (this.userRolesRepo == null)
                    this.userRolesRepo = new GenericRepository<UserRoles>(context);
                return userRolesRepo;
            }
            set { userRolesRepo = value; }
        }




        public void Save()
        {
            context.SaveChanges();

        }


        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }

    public class UnitOfWorkSettings
    {
        private bool _proxyCreationEnabled;
        private bool _lazyLoading;

        public bool LazyLoading
        {
            get
            {
                if (_lazyLoading == null)
                    _lazyLoading = true;
                return _lazyLoading;
            }
            set => _lazyLoading = value;
        }

        public bool AsNoTracking { get; set; }

        public bool ProxyCreationEnabled
        {
            get
            {
                if (_proxyCreationEnabled == null)
                    _proxyCreationEnabled = true;
                return _proxyCreationEnabled;
            }
            set => _proxyCreationEnabled = value;
        }
    }
}