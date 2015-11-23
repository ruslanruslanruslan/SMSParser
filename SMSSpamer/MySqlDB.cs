using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SMSSpamer
{

  class Message
  {
    public Message() { }
    public Message(string _id, string _number, string _message)
    {
      id = _id;
      number = _number;
      message = _message;
    }
    public string id { get; set; }
    public string number { get; set; }
    public string message { get; set; }
  }

  class MySqlDB : IDisposable
  {
    private string m_Server;
    private int m_Port;
    private string m_Database;
    private string m_Login;
    private string m_Password;
    private MySqlConnection m_mySqlConnection;

    public MySqlDB(string _Login, string _Password, string _Server, int _Port, string _Database)
    {
      Login = _Login;
      Password = _Password;
      Server = _Server;
      Port = _Port;
      Database = _Database;
    }

    public string Server
    {
      get { return m_Server; }
      set { m_Server = value; }
    }
    public int Port
    {
      get { return m_Port; }
      set { m_Port = value; }
    }
    public string Database
    {
      get { return m_Database; }
      set { m_Database = value; }
    }
    public string Login
    {
      get { return m_Login; }
      set { m_Login = value; }
    }
    public string Password
    {
      get { return m_Password; }
      set { m_Password = value; }
    }
    public string ConnectionString
    {
      get
      {
        return "server=" + Server + ";port=" + Convert.ToString(Port) +
          ";database=" + Database + ";user=" + Login + ";password=" + Password + ";";
      }
    }

    public void Close()
    {
      if (m_mySqlConnection != null)
        m_mySqlConnection.Close();
    }

    public MySqlConnection mySqlConnection
    {
      get
      {
        if (m_mySqlConnection == null)
          m_mySqlConnection = new MySqlConnection(ConnectionString);
        if (m_mySqlConnection.State == System.Data.ConnectionState.Broken || m_mySqlConnection.State == System.Data.ConnectionState.Closed)
        {
          try
          {
            m_mySqlConnection.Open();
          }
          catch (Exception ex)
          {
            throw new Exception("MySql error: Невозможно соединиться с сервером базы данных: " + ex.Message, ex);
          }
        }
        return m_mySqlConnection;
      }
    }

    private List<string> GetMewMessagesId()
    {
      const string sql = "call sp_get_SMS_NextID;";
      var ids = new List<string>();
      MySqlDataReader reader = null;
      try
      {
        reader = new MySqlCommand(sql, mySqlConnection).ExecuteReader();
        while (reader.Read())
          ids.Add(reader.GetString(0));
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] " + ex.Message, ex);
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
      return ids;
    }

    public int GetSMSLeft()
    {
      const string sql = "select fn_check_sms_IsAllowedForSending(null);";
      int count = 0;
      MySqlDataReader reader = null;
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        reader = cmd.ExecuteReader();
        count = Convert.ToInt32(reader.GetString(0));
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] " + ex.Message, ex);
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
      return count;
    }

    private string ListToString(List<string> list)
    {
      var result = string.Empty;
      foreach (var str in list)
      {
        if (result != string.Empty)
          result += ", ";
        result += str;
      }
      return result;
    }

    public List<Message> GetMessagePacket()
    {
      var ids = ListToString(GetMewMessagesId());
      if (ids == string.Empty)
        return new List<Message>();
      var sql = "select pk_id, tel_num, message from fct_smsspamer where pk_id in (" + ids + ");";
      var messages = new List<Message>();
      MySqlDataReader reader = null;
      try
      {
        reader = new MySqlCommand(sql, mySqlConnection).ExecuteReader();
        while (reader.Read())
          messages.Add(new Message(reader.GetString(0), reader.GetString(1), reader.GetString(2)));
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] " + ex.Message, ex);
      }
      finally
      {
        if (reader != null)
          reader.Close();
      }
      return messages;
    }

    public void SetMessageSent(string id)
    {
      const string sql = "call sp_set_SMS_sent(@id);";
      try
      {
        var cmd = new MySqlCommand(sql, mySqlConnection);
        cmd.Prepare();
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
      }
      catch (Exception ex)
      {
        throw new Exception("MySql error: [" + sql + "] [id = " + id + "]: " + ex.Message, ex);
      }
    }

    ~MySqlDB()
    {
      Dispose(false);
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      Close();
    }
  }
}
