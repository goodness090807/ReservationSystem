using Dapper;
using ReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ReservationSystem.Repository
{
    public class ReservationRepository
    {
        private readonly BaseRepository _respository;

        public ReservationRepository(BaseRepository BaseRepository)
        {
            _respository = BaseRepository;
        }

        public int AddBookingInfo(BooksInfoModel booksInfo)
        {
            string sql = @"INSERT INTO booksinfo (uid, username, service, tostoredatetime, createdatetime)
                                Values (@UID, @UserName, @Service, @ToStoreDateTime, @CreateDateTime)";

            return _respository.Execute(sql, booksInfo);
        }
        public BooksInfoModel GetBookingInfo(string uid)
        {
            string sql = @"SELECT uid, username, (SELECT servicename FROM serviceinfo WHERE serviceid = booksinfo.service) service
                            , tostoredatetime, createdatetime FROM booksinfo WHERE uid=@uid";

            //設定參數
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@uid", uid, DbType.String, ParameterDirection.Input);

            return _respository.Query<BooksInfoModel>(sql, parameters);
        }

        public int DeleteBookingInfo(string uid)
        {
            string sql = @"DELETE FROM booksinfo WHERE uid=@uid";

            //設定參數
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@uid", uid, DbType.String, ParameterDirection.Input);

            return _respository.Execute(sql, parameters);
        }
    }
}
