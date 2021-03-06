﻿using System;
using System.Collections.Generic;
using System.Data;
using SiteServer.Plugin;
using SS.Poll.Model;

namespace SS.Poll.Provider
{
    public class LogDao
    {
        public const string TableName = "ss_poll_log";

        public static List<TableColumn> Columns => new List<TableColumn>
        {
            new TableColumn
            {
                AttributeName = nameof(LogInfo.Id),
                DataType = DataType.Integer
            },
            new TableColumn
            {
                AttributeName = nameof(LogInfo.PollId),
                DataType = DataType.Integer
            },
            new TableColumn
            {
                AttributeName = nameof(LogInfo.ItemIds),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(LogInfo.UniqueId),
                DataType = DataType.VarChar,
                DataLength = 200
            },
            new TableColumn
            {
                AttributeName = nameof(LogInfo.AddDate),
                DataType = DataType.DateTime
            },
            new TableColumn
            {
                AttributeName = nameof(LogInfo.AttributeValues),
                DataType = DataType.Text
            }
        };

        private readonly string _connectionString;
        private readonly IDataApi _helper;

        public LogDao(string connectionString, IDataApi dataApi)
        {
            _connectionString = connectionString;
            _helper = dataApi;
        }

        public void Insert(LogInfo logInfo)
        {
            string sqlString = $@"INSERT INTO {TableName}
(
    {nameof(LogInfo.PollId)},
    {nameof(LogInfo.ItemIds)},
    {nameof(LogInfo.UniqueId)},
    {nameof(LogInfo.AddDate)},
    {nameof(LogInfo.AttributeValues)}
) VALUES (
    @{nameof(LogInfo.PollId)}, 
    @{nameof(LogInfo.ItemIds)}, 
    @{nameof(LogInfo.UniqueId)},
    @{nameof(LogInfo.AddDate)},
    @{nameof(LogInfo.AttributeValues)}
)";

            var parameters = new List<IDataParameter>
            {
                _helper.GetParameter(nameof(logInfo.PollId), logInfo.PollId),
                _helper.GetParameter(nameof(logInfo.ItemIds), logInfo.ItemIds),
                _helper.GetParameter(nameof(logInfo.UniqueId), logInfo.UniqueId),
                _helper.GetParameter(nameof(logInfo.AddDate), logInfo.AddDate),
                _helper.GetParameter(nameof(logInfo.AttributeValues), logInfo.ToString())
            };

            _helper.ExecuteNonQuery(_connectionString, sqlString, parameters.ToArray());
        }

        public void DeleteAll(int pollId)
        {
            if (pollId <= 0) return;

            string sqlString = $"DELETE FROM {TableName} WHERE {nameof(LogInfo.PollId)} = {pollId}";
            _helper.ExecuteNonQuery(_connectionString, sqlString);
        }

        public void Delete(List<int> logIdList)
        {
            if (logIdList == null || logIdList.Count <= 0) return;
            string sqlString =
                $"DELETE FROM {TableName} WHERE {nameof(LogInfo.Id)} IN ({string.Join(",", logIdList)})";
            _helper.ExecuteNonQuery(_connectionString, sqlString);
        }

        public int GetCount(int pollId)
        {
            string sqlString =
                $"SELECT COUNT(*) FROM {TableName} WHERE {nameof(LogInfo.PollId)} = {pollId}";

            var count = 0;

            using (var rdr = _helper.ExecuteReader(_connectionString, sqlString))
            {
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    count = rdr.GetInt32(0);
                }
                rdr.Close();
            }

            return count;
        }

        public bool IsExists(int pollId, string uniqueId)
        {
            var sqlString =
                $"SELECT Id FROM {TableName} WHERE {nameof(LogInfo.PollId)} = @{nameof(LogInfo.PollId)} AND {nameof(LogInfo.UniqueId)} = @{nameof(LogInfo.UniqueId)}";

            var parameters = new List<IDataParameter>
            {
                _helper.GetParameter(nameof(LogInfo.PollId), pollId),
                _helper.GetParameter(nameof(LogInfo.UniqueId), uniqueId)
            };

            var exists = false;

            using (var rdr = _helper.ExecuteReader(_connectionString, sqlString, parameters.ToArray()))
            {
                if (rdr.Read() && !rdr.IsDBNull(0))
                {
                    exists = true;
                }
                rdr.Close();
            }

            return exists;
        }

        public List<LogInfo> GetPollLogInfoList(int pollId, int totalCount, int limit, int offset)
        {
            var pollLogInfoList = new List<LogInfo>();

            string sqlString =
                $@"SELECT {nameof(LogInfo.Id)},
    {nameof(LogInfo.PollId)},
    {nameof(LogInfo.ItemIds)},
    {nameof(LogInfo.UniqueId)},
    {nameof(LogInfo.AddDate)},
    {nameof(LogInfo.AttributeValues)}
            FROM {TableName} WHERE {nameof(LogInfo.PollId)} = {pollId}";

            using (var rdr = _helper.ExecuteReader(_connectionString, sqlString))
            {
                while (rdr.Read())
                {
                    var pollLogInfo = GetPollItemInfo(rdr);
                    if (!string.IsNullOrEmpty(pollLogInfo.AttributeValues))
                    {
                        pollLogInfoList.Add(pollLogInfo);
                    }
                }
                rdr.Close();
            }

            return pollLogInfoList;
        }

        public List<LogInfo> GetAllPollLogInfoList(int pollId)
        {
            var pollLogInfoList = new List<LogInfo>();

            string sqlString =
                $@"SELECT {nameof(LogInfo.Id)},
    {nameof(LogInfo.PollId)},
    {nameof(LogInfo.ItemIds)},
    {nameof(LogInfo.UniqueId)},
    {nameof(LogInfo.AddDate)},
    {nameof(LogInfo.AttributeValues)}
            FROM {TableName} WHERE {nameof(LogInfo.PollId)} = {pollId}";

            using (var rdr = _helper.ExecuteReader(_connectionString, sqlString))
            {
                while (rdr.Read())
                {
                    var pollLogInfo = GetPollItemInfo(rdr);
                    if (!string.IsNullOrEmpty(pollLogInfo.AttributeValues))
                    {
                        pollLogInfoList.Add(pollLogInfo);
                    }
                }
                rdr.Close();
            }

            return pollLogInfoList;
        }

        private static LogInfo GetPollItemInfo(IDataRecord rdr)
        {
            if (rdr == null) return null;

            var logInfo = new LogInfo();

            var i = 0;
            logInfo.Id = rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i);
            i++;
            logInfo.PollId = rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i);
            i++;
            logInfo.ItemIds = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            logInfo.UniqueId = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);
            i++;
            logInfo.AddDate = rdr.IsDBNull(i) ? DateTime.Now : rdr.GetDateTime(i);
            i++;
            logInfo.AttributeValues = rdr.IsDBNull(i) ? string.Empty : rdr.GetString(i);

            logInfo.Load(logInfo.AttributeValues);

            return logInfo;
        }
    }
}
