using System.Data;
using System.Data.SqlClient;


//MyDataBaseController MyData = new MyDataBaseController(@"DESKTOP-G5EG346\SQLEXPRESS", "SC_Users");
//DataTable dt_user = MyData.Select("SELECT * FROM [dbo].[tbUsers]"); // получаем данные из таблицы
//MyData.Select("INSERT INTO [dbo].[tbUsers] VALUES (1, 'test', 'test')"); // заносим запись в таблицу

namespace Simple_Chat
{
    class MyDataBaseController
    {
        public string server { get; set; }
        public string DataBase { get; set; }

        public MyDataBaseController(string _server, string _DataBase)
        {
            server = _server;
            DataBase = _DataBase;
        }

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection($@"server={server};Trusted_Connection=Yes;DataBase={DataBase};");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }

    }
}
