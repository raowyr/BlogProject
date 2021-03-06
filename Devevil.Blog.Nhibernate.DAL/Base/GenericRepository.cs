﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using Devevil.Blog.Infrastructure.Core.RepositoryPattern;
using Devevil.Blog.Infrastructure.Core.Entities;

namespace Devevil.Blog.Nhibernate.DAL.Base
{
    /// <summary>
    /// Repository Generico.
    /// Normalmente è una classe base per respositories di entità personalizzati.
    /// Non dovrebbe essere esposto all'esterno del DAL.
    /// </summary>
    /// <typeparam name="T">Parametro generico, rappresenta il tipo di entità</typeparam>
    /// <remarks>
    /// Tutte le operazioni sono wrappate con <see cref="TransactionRquired"/>.
    /// Se NON vi è una Transazione aperta, allora viene aperta e al termine viene committata automaticamente.
    /// Se CI STA una transazione aperta, niente viene committato, il commit deve essere effettuato a livello più alto.
    /// </remarks>
    public class GenericRepository<T> : IRepository<T>
    {
        readonly ISession _session;
        private ISession Session
        {
            get { return _session; }
        }

        public GenericRepository(ISession session)
        {
            _session = session;
        }

        #region Read
        /// <summary>
        /// Ritorna l'entità che possiede l'ID specificato.
        /// Lancia una eccezione se non presente nel Database.
        /// </summary>
        public T Load(object id)
        {
            using (TransactionRequired transaction = new TransactionRequired(_session))
            {
                return Session.Load<T>(id);
            }
        }

        /// <summary>
        /// Gets the Id. Returns null if there is no matching row
        /// </summary>
        public T GetById(object id)
        {
            using (TransactionRequired transaction = new TransactionRequired(_session))
            {
                return Session.Get<T>(id);
            }
        }

        /// <summary>
        /// Finds all records. Consider <see cref="FindPage"/> for large result sets.
        /// </summary>
        public ICollection<T> FindAll()
        {
            using (TransactionRequired transaction = new TransactionRequired(_session))
            {
                return Session.CreateCriteria(typeof(T)).List<T>();
            }
        }

        /// <summary>
        /// Counts the number of records.
        /// </summary>
        public int Count()
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            using (TransactionRequired transaction = new TransactionRequired(_session))
            {
                return criteria.SetProjection(NHibernate.Criterion.Projections.RowCount()).UniqueResult<int>();
            }
        }

        /// <summary>
        /// Finds records by page.
        /// </summary>
        /// <param name="pageStartRow">The page start row.</param>
        /// <param name="pageSize">Size of the page.</param>
        public IList<T> FindPage(int pageStartRow, int pageSize)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            criteria.SetFirstResult(pageStartRow);
            criteria.SetMaxResults(pageSize);
            using (TransactionRequired transaction = new TransactionRequired(_session))
            {
                return criteria.List<T>();
            }
        }

        /// <summary>
        /// Finds records by page, sorted.
        /// </summary>
        public IList<T> FindSortedPage(int pageStartRow, int pageSize, string sortBy, bool descending)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));

            if (descending)
                criteria.AddOrder(NHibernate.Criterion.Order.Desc(sortBy));
            else
                criteria.AddOrder(NHibernate.Criterion.Order.Asc(sortBy));

            criteria.SetFirstResult(pageStartRow);
            criteria.SetMaxResults(pageSize);
            using (TransactionRequired transaction = new TransactionRequired(_session))
            {
                return criteria.List<T>();
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Saves the specified object within a transaction.
        /// </summary>
        public virtual void Save(T entity)
        {
            using (TransactionRequired transaction = new TransactionRequired(_session))
            {
                Session.Save(entity);
                transaction.Commit(); //flush to database
            }
        }

        /// <summary>
        /// Saves the specified object within a transaction.
        /// </summary>
        public virtual void SaveOrUpdate(T entity)
        {
            using (TransactionRequired transaction = new TransactionRequired(_session))
            {
                Session.SaveOrUpdate(entity);
                transaction.Commit(); //flush to database
            }
        }

        /// <summary>
        /// Deletes the specified object within a transaction.
        /// </summary>
        public virtual void Delete(T entity)
        {
            using (TransactionRequired transaction = new TransactionRequired(_session))
            {
                Session.Delete(entity);
                transaction.Commit(); //flush to database
            }
        }
        #endregion
    }
}
