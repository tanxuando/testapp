using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
    public enum MoodNameType
    {
        Happy, Depressed, Confused, Anxious, Spitting, Sad, Numb
    }
    public class MoodDTO
    {
        public string Name { get; set; }
        public int Level { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var a = Convert.ToString(17734058512, 2);

            var constr = @"Server=HVJPSUEJMQ8PZ1N\SQL2012;Database=TalklifeDb40c;UID=sa;pwd=123456";
            var comdstr = @"SELECT Id,NameMoodIcon  FROM dbo.Talklife_Question WHERE 
                NameMoodIcon='Happy'
                OR NameMoodIcon='Confused'
                OR NameMoodIcon='Anxious' 
                OR NameMoodIcon='Spitting' 
                OR NameMoodIcon='Sad'
                OR NameMoodIcon='Numb'
                OR NameMoodIcon='Depressed'";
            SqlConnection con2 = new SqlConnection(constr);
            con2.Open();
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand(comdstr, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    int i = 0;
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var str = @"UPDATE dbo.Talklife_Question SET NameMoodIconTemp={0} WHERE Id={1}";
                            var in64 = GetIn64(reader.GetString(1));
                            str = string.Format(str, in64, reader.GetInt32(0));
                            SqlCommand command2 = new SqlCommand(str, con2);
                            command2.ExecuteNonQuery();
                            i++;
                            Console.WriteLine(i);
                        }
                    }
                }
            }
            /* var a = Convert.ToString(1024, 2);
           a = Convert.ToString(33554432, 2);*/
            /* SqlConnection connection = new SqlConnection(@"Server=HVJPSUEJMQ8PZ1N\SQL2012;Database=TalklifeDb40c;UID=sa;pwd=123456");
             SqlConnection connection1 = new SqlConnection();
             SqlCommand command = new SqlCommand(, connection);
             connection.Open();
             connection1.Open();
             SqlDataReader reader = command.ExecuteReader();*/


        }

        static Int64 GetIn64(string name)
        {
            Int64 result = 0;
            try
            {
                var index = (((MoodNameType)Enum.Parse(typeof(MoodNameType), name)).GetHashCode() * 5 + 1) - 1;
                result += (Int64)Math.Pow(2, index);
            }
            catch
            {
            }
            return result;
        }
        static Int64 GetList(string name)
        {
            Int64 result = 0;
            try
            {
                var index = (((MoodNameType)Enum.Parse(typeof(MoodNameType), name)).GetHashCode() * 5 + 1) - 1;
                result += (Int64)Math.Pow(2, index);
            }
            catch
            {
            }
            return result;
        }
    }
}
