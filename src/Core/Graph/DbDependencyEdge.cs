using System;
using DbFriend.Core.Provider;
using QuickGraph;

namespace DbFriend.Core.Graph
{
    /// <summary>
    /// 
    /// </summary>
    public class DbDependencyEdge : IDbDependencyEdge
    {
        private readonly IDbObject _child;
        private readonly IDbObject _parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbDependencyEdge"/> class.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <param name="child">The child.</param>
        public DbDependencyEdge(IDbObject parent, IDbObject child)
        {
            _parent = parent;
            _child = child;
        }

        #region IEdge<IDbObject> Members

        public IDbObject Source
        {
            get { return _parent; }
        }

        public IDbObject Target
        {
            get { return _child; }
        }

        #endregion

        #region IEquatable<DbDependencyEdge> Members

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.
        ///                 </param>
        public bool Equals(DbDependencyEdge other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other._child, _child) && Equals(other._parent, _parent);
        }

        #endregion

        public bool Equals(IDbDependencyEdge other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. 
        ///                 </param><exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.
        ///                 </exception><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (DbDependencyEdge)) return false;
            return Equals((DbDependencyEdge) obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((_child != null ? _child.GetHashCode() : 0)*397) ^ (_parent != null ? _parent.GetHashCode() : 0);
            }
        }

        public static bool operator ==(DbDependencyEdge left, DbDependencyEdge right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DbDependencyEdge left, DbDependencyEdge right)
        {
            return !Equals(left, right);
        }
    }
}