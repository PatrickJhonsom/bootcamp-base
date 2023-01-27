using Dapper;
using System;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Tarefas.DTO;

namespace Tarefas.DAO
{
    public class TarefaDAO
    {
        private string DataSourceFile => Environment.CurrentDirectory + "AppTarefasDB.sqlite";
        public SQLiteConnection Connection => new SQLiteConnection("DataSource="+ DataSourceFile);
        
        public TarefaDAO()
        {
            if(!File.Exists(DataSourceFile))
            {
                CreateDatabase();
            }
        }
        
        private void CreateDatabase()
        {
            using(var con = Connection)
            {
                con.Open();
                con.Execute(
                    @"CREATE TABLE Tarefa
                    (
                        Id          integer primary key autoincrement,
                        Titulo      varchar(100) not null,
                        Descricao   varchar(100) not null,
                        Concluida   bool not null
                    )"
                );
            }
        }

        public void Criar(TarefaDTO tarefa)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute(
                    @"INSERT INTO Tarefa
                    (Titulo, Descricao, Concluida) VALUES
                    (@Titulo, @Descricao, @Concluida);", tarefa
                );
            }
        }
        
        public List<TarefaDTO> Consultar()
        {
            using (var con = Connection)
            {
                con.Open();
                var result = con.Query<TarefaDTO>(@"SELECT id, Titulo, Descricao, Concluida FROM tarefa").ToList();
                return result;
                ;
            }
        }  
        public TarefaDTO Consultar(int id)
        {
            using (var con = Connection)
            {
                con.Open();
                TarefaDTO  result = con.Query<TarefaDTO>
                (@"SELECT id, Titulo, Descricao, Concluida FROM tarefa WHERE id=@id", new{id}
                ).FirstOrDefault();
                return result;
            }
        }   

        public void Atualizar(TarefaDTO tarefa)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute(@"UPDATE Tarefa
                SET Titulo = @Titulo, Descricao = @Descricao, Concluida=@Concluida
                WHERE id = @id;",tarefa);
                
            }
        }

        public void Delete(int id)
        {
            using (var con = Connection)
            {
                con.Open();
                con.Execute(
                    @"DELETE  FROM tarefa WHERE id=@id", new{id}
                );                           
            }
        }                      
        
    }
}