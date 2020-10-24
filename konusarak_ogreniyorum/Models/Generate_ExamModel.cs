using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.Data.SQLite;

namespace konusarak_ogreniyorum.Models
{
    public class Generate_ExamModel
    {   
        public static string  selected_exam = null;
        public string selected_exam_content = null;

        public static string[] questions = new string [4];
        public static string[] answers = new string[4];
        public static string[] first_question_answers = new string[4];
        public static string[] second_question_answers = new string[4];
        public static string[] third_question_answers = new string[4];
        public static string[] fourth_question_answers = new string[4];

        public static List<string> listed_news = new List<string>();
        public static List<string> listed_news_contents = new List<string>();



        public static void generate_data()
        {
            Uri url = new Uri("https://www.wired.com/");
            WebClient client = new WebClient();
            string html = client.DownloadString(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(html);
            HtmlNodeCollection news = document.DocumentNode.SelectNodes("//a");


            int count = 0;
            foreach(var newss in news)
            {
                if (newss.Attributes["href"].Value.Contains("/story/") && !newss.Attributes["href"].Value.Contains("wired.com"))
                {
                    if (!listed_news.Contains(newss.Attributes["href"].Value))
                    {
                        if (count <5)
                        {
                            listed_news.Add(newss.Attributes["href"].Value);
                            count++;
                        }
                    }
                    
                }
            }

            

            for (int i = 0; i < 5; i++)
            {
                Uri url1 = new Uri("https://www.wired.com" + listed_news[i]);
                WebClient client1 = new WebClient();
                string html1 = client.DownloadString(url1);
                HtmlAgilityPack.HtmlDocument document1 = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html1);
                //HtmlNodeCollection news1 = document.DocumentNode.SelectNodes("//a");

                listed_news[i] = document.DocumentNode.SelectSingleNode("//head/title").InnerText;
                listed_news_contents.Add(document.DocumentNode.SelectSingleNode("//script[@type='application/ld+json']").InnerText);
                /*foreach (var newss1 in news1)
                {
                    listed_news[i] = newss1.Attributes["//head/title"].Value;

                }*/



            }
            write_to_database();
        }
        public static void write_to_database()
        {
            string path1 = "Data source = db/db.db";
            SQLiteConnection con1 = new SQLiteConnection(path1);
            con1.Open();

            string query1 = "DELETE FROM EXAMS";

            //,'" + listed_news_contents[i] + "'
            SQLiteCommand cmd1 = new SQLiteCommand(query1, con1);
            cmd1.ExecuteNonQuery();

            con1.Close();


            for (int i = 0; i < 5; i++)
            {
                string path = "Data source = db/db.db";
                SQLiteConnection con = new SQLiteConnection(path);
                con.Open();

                string query = "INSERT INTO Exams (ID,NAME) VALUES (" +i+ ",'" + listed_news[i]+ "')";

                //,'" + listed_news_contents[i] + "'
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
        public static void write_to_database1()
        {
            for (int i = 0; i < 5; i++)
            {
                string path = "Data source = db/db.db";
                SQLiteConnection con = new SQLiteConnection(path);
                con.Open();

                string query = "INSERT INTO Exams (1_QUESTION,1_1_CHOICE,1_2_CHOICE,1_3_CHOICE,1_4_CHOICE,1_CORRECT_CHOICE,2_QUESTION,2_1_CHOICE,2_2_CHOICE,2_3_CHOICE,2_4_CHOICE,2_CORRECT_CHOICE,3_QUESTION,3_1_CHOICE,3_2_CHOICE,3_3_CHOICE,3_4_CHOICE,3_CORRECT_CHOICE,4_QUESTION,4_1_CHOICE,4_2_CHOICE,4_3_CHOICE,4_4_CHOICE,4_CORRECT_CHOICE) VALUES ('" + questions[0] + "','" + first_question_answers[0] + "','" + first_question_answers[1] + "','" + first_question_answers[2] + "','" + first_question_answers[3] + "','" + answers[0] + "','" + questions[1] + "','" + second_question_answers[0] + "','" + second_question_answers[1] + "','" + second_question_answers[2] + "','" + second_question_answers[3] + "','" + answers[1] + "','" + questions[2] + "','" + third_question_answers[0] + "','" + third_question_answers[1] + "','" + third_question_answers[2] + "','" + third_question_answers[3] + "','" + answers[2] + "','" + questions[3] + "','" + fourth_question_answers[0] + "','" + fourth_question_answers[1] + "','" + fourth_question_answers[2] + "','" + fourth_question_answers[3] + "','" + answers[3] + "') WHERE NAME == '" + selected_exam + "'";

                //,'" + listed_news_contents[i] + "'
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.ExecuteNonQuery();
            }
        }
        public static string find_content()
        {
            for (int i = 0; i < 5; i++)
            {
                if(listed_news[i] == selected_exam)
                {
                    return listed_news_contents[i];
                }
            }
            return null;
        }
    }
}
