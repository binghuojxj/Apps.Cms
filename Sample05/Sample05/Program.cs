using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Sample05
{
    class Program
    {
        /// <summary>
        /// 测试插入单条数据
        /// </summary>
        static void test_insert()
        {
            var content = new Content
            {
                title = "标题1",
                content = "内容1",

            };
            using (var conn = new SqlConnection(@"Data Source=.\mssql2012;User ID=sa;Password=123;Initial Catalog=Sample;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"INSERT INTO [Content]
                (title, [content], status, add_time, modify_time)
                VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_insert：插入了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试一次批量插入两条数据
        /// </summary>
        static void test_mult_insert()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                title = "批量插入标题1",
                content = "批量插入内容1",

            },
               new Content
            {
                title = "批量插入标题2",
                content = "批量插入内容2",

            },
        };

            using (var conn = new SqlConnection(@"Data Source=.\mssql2012;User ID=sa;Password=123;Initial Catalog=Sample;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"INSERT INTO [Content]
                (title, [content], status, add_time, modify_time)
                VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_insert：插入了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试一次批量插入多条comment
        /// </summary>
        static void test_mult_insert_comment()
        {
            List<Comment> comments = new List<Comment>() {
               new Comment
            {
                content_id = 5,
                content = "注释1"

            },
               new Comment
            {
                content_id = 5,
                content = "注释2"
            },
               new Comment
            {
                content_id = 5,
                content = "注释3"
            }
        };

            using (var conn = new SqlConnection(@"Data Source=.\mssql2012;User ID=sa;Password=123;Initial Catalog=Sample;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"INSERT INTO [Comment]
                (content_id, [content], add_time)
                VALUES   (@content_id,@content,@add_time)";
                var result = conn.Execute(sql_insert, comments);
                Console.WriteLine($"test_mult_insert：插入了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试删除单条数据
        /// </summary>
        static void test_del()
        {
            var content = new Content
            {
                id = 2,

            };
            using (var conn = new SqlConnection(@"Data Source=.\mssql2012;User ID=sa;Password=123;Initial Catalog=Sample;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"DELETE FROM [Content]
                WHERE   (id = @id)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_del：删除了{result}条数据！");
            }
        }

        /// <summary>
        /// 测试一次批量删除两条数据
        /// </summary>
        static void test_mult_del()
        {
            List<Content> contents = new List<Content>() {
                   new Content
                {
                    id=3,

                },
                   new Content
                {
                    id=4,

                },
            };

            using (var conn = new SqlConnection(@"Data Source=.\mssql2012;User ID=sa;Password=123;Initial Catalog=Sample;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"DELETE FROM [Content]
                WHERE   (id = @id)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_del：删除了{result}条数据！");
            }
        }

        /// <summary>
        /// 查询单条指定的数据
        /// </summary>
        static void test_select_one()
        {
            using (var conn = new SqlConnection(@"Data Source=.\mssql2012;User ID=sa;Password=123;Initial Catalog=Sample;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"select * from [dbo].[content] where id=@id";
                var result = conn.QueryFirstOrDefault<Content>(sql_insert, new { id = 5 });
                Console.WriteLine($"test_select_one：查到的数据为：");
            }
        }

        /// <summary>
        /// 查询多条指定的数据
        /// </summary>
        static void test_select_list()
        {
            using (var conn = new SqlConnection(@"Data Source=.\mssql2012;User ID=sa;Password=123;Initial Catalog=Sample;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"select * from [dbo].[content] where id in @ids";
                var result = conn.Query<Content>(sql_insert, new { ids = new int[] { 6, 7 } });
                Console.WriteLine($"test_select_one：查到的数据为：");
            }
        }

        /// <summary>
        /// 测试关联查询
        /// </summary>
        static void test_select_content_with_comment()
        {
            using (var conn = new SqlConnection(@"Data Source=.\mssql2012;User ID=sa;Password=123;Initial Catalog=Sample;Pooling=true;Max Pool Size=100;"))
            {
                string sql_insert = @"select * from content where id=@id;
select * from comment where content_id=@id;";
                using (var result = conn.QueryMultiple(sql_insert, new { id = 5 }))
                {
                    var content = result.ReadFirstOrDefault<ContentWithComment>();
                    content.comments = result.Read<Comment>();
                    Console.WriteLine($"test_select_content_with_comment:内容5的评论数量{content.comments.Count()}");
                }

            }
        }
        static void Main(string[] args)
        {
            test_mult_insert_comment();
            test_select_content_with_comment();
            Console.ReadKey();
        }
    }
}
