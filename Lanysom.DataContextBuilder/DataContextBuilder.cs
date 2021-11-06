using Newtonsoft.Json.Linq;
using System;

namespace Lanysom.DataContextBuilder
{
    public abstract class DataContextBuilder
    {
        protected object _context;

        public static SupportedPlatforms For => new();

        public virtual DataContextBuilder Initialize<TDataContext>() where TDataContext : new()
        {
            _context = new TDataContext();
            return this;
        }

        public virtual DataContextBuilder Initialize<TDataContext>(Func<TDataContext> factory)
        {
            throw new NotImplementedException();
        }

        public virtual DataContextBuilder Feed<TEntity>(string pathToTestData) where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public virtual DataContextBuilder Feed<TEntity>(string pathToTestData, Func<JObject, TEntity> entityFactory) where TEntity : new()
        {
            throw new NotImplementedException();
        }

        public virtual TDataContext Build<TDataContext>() where TDataContext : class
        {
            return _context as TDataContext;
        }
    }

    public class SupportedPlatforms
    {

    }
}
