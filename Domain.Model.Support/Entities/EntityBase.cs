using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Support.Entities
{
    /// <summary>
    /// Classe base per tutte le entità che implementano identity equality.
    /// </summary>
    /// <typeparam name="TId">Tipo dell'ID</typeparam>
    /// <typeparam name="T">Tipo della classe</typeparam>
    public abstract class EntityBase<TId, T> where T : EntityBase<TId, T>
    {
        private TId _id;
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public virtual TId Id
        {
            get { return _id; }
            set { _id = value; }
        }

        #region overrides
        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        public override string ToString()
        {
            return GetType().Name + " = "
                + (IsTransient() ? "(Transient)" : Id.ToString());
        }

        private int? _transientHashCode;
        /// <summary>
        /// Serves as a hash function for a particular type.
        /// If persistent, uses Id; if transient, uses temporary code.
        /// (This will not work if the entity is evicted and reloaded)
        /// </summary>
        public override int GetHashCode()
        {
            if (_transientHashCode.HasValue) return _transientHashCode.Value;
            if (IsTransient())
            {
                _transientHashCode = base.GetHashCode();
                return _transientHashCode.Value;
            }
            return Id.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        public override bool Equals(object obj)
        {
            T x = obj as T;
            if (x == null) return false;
            if (IsTransient() && x.IsTransient()) return ReferenceEquals(this, x);
            return (Id.Equals(x.Id));
        }

        /// <summary>
        /// Implements the operator == (Equals).
        /// </summary>
        public static bool operator ==(EntityBase<TId, T> first, EntityBase<TId, T> second)
        {
            return Equals(first, second);
        }

        /// <summary>
        /// Implements the operator != (Not Equals).
        /// </summary>
        public static bool operator !=(EntityBase<TId, T> first, EntityBase<TId, T> second)
        {
            return !(Equals(first, second));
        }

        /// <summary>
        /// Determines whether this instance is transient.
        /// </summary>
        public virtual bool IsTransient()
        {
            return Id == null || Id.Equals(default(TId));
        }
        #endregion
    }
}
