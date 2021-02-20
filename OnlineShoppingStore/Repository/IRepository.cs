using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace OnlineShoppingStore.Repository
{
    public interface IRepository<Tbl_Entity> where Tbl_Entity : class
    {
        IEnumerable<Tbl_Entity> GetProduct();
        IEnumerable<Tbl_Entity> GetAllRecords();
        IQueryable<Tbl_Entity> GetAllRecordsIQueryable();
        int GetAllRecordCount();
        void Add(Tbl_Entity entity);
        void Update(Tbl_Entity entity);
        void UpdateByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict);
        Tbl_Entity GetFirstOrDefault(int recordId);
        void Remove(Tbl_Entity entity);
        void RemoveByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict);
        void RemoveRangeByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict);
        void InactiveAndDeleteMarkByWhereClause(Expression<Func<Tbl_Entity, bool>> wherePredict, Action<Tbl_Entity> ForEachPredict);
        Tbl_Entity GetFirstOrDefaultByParameter(Expression<Func<Tbl_Entity, bool>> wherePredict);
        IEnumerable<Tbl_Entity> GetListParameter(Expression<Func<Tbl_Entity, bool>> wherePredict);
        IEnumerable<Tbl_Entity> GetResultBySqlProcedure(string query, params object[] parameters);

        IEnumerable<Tbl_Entity> GetRecordsToShow(int PageNo, int PageSize, int CurrentPage, Expression<Func<Tbl_Entity, bool>> wherePredict, Expression<Func<Tbl_Entity, int>> orderByPredict);
    }
}