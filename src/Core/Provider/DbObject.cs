using System;

namespace DbFriend.Core.Provider
{
    public class DbObject : IDbObject, IEquatable<DbObject>
    {
        private readonly int _id;
        private readonly string _name;
        private readonly string _schema;
        private readonly string _type;

        public DbObject(int id, string name, string type, string schema)
        {
            _id = id;
            _name = name.Trim().ToLower();
            _type = type.Trim().ToLower();
            _schema = schema.Trim().ToLower();
        }

        #region IDbObject Members

        public string Schema
        {
            get { return _schema; }
        }

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public string Type
        {
            get { return _type; }
        }

        public int Compare(IDbObject x, IDbObject y)
        {
            return x.Id - y.Id;
        }

        public int Compare(object x, object y)
        {
            return ((IDbObject)x).Id - ((IDbObject)y).Id;
        }

        #endregion

        #region IEquatable<DbObject> Members

        public bool Equals(DbObject other)
        {
            return other._id == _id;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(DbObject)) return false;
            return Equals((DbObject)obj);
        }

        public override int GetHashCode()
        {
            return _id;
        }
    }
}