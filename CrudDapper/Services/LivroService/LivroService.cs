using CrudDapper.Models;
using Dapper;
using System.Data.SqlClient;

namespace CrudDapper.Services.LivroService
{
    public class LivroService : ILivroInterface
    {
        private readonly IConfiguration _configuration;//Criamos uma variavel privada de leitura IConfiguration _configuration para acessar o appsettings.json
        private readonly string getConnection;
        public LivroService(IConfiguration configuration)// Esse (IConfiguration configuration) é preciso para acessar o appsettings.json
        {
            _configuration = configuration;
            getConnection = _configuration.GetConnectionString("DefaultConnection");// Busca a nossa string de conexão
        }
        public async Task<IEnumerable<Livro>> GetAllLivros()
        {
            using (var con = new SqlConnection(getConnection)) //O using aqui garante que quando o usuario fizer uso do método, abra uma conexão com o banco e após a conclusão do método a conexão fecha, assim evita erros de varios usarios usando o metodo o que pode dar erro no banco de dados, repare que passamos como paramêtro a nossa string de conexão(getConnection)
            {
                var sql = "select * from Livros"; //Aqui criamos uma variavel e passamos uma string pra ela, essa string é um comando sql que dá um get em todos os registros da tabela livro

                return await con.QueryAsync<Livro>(sql);//Esse con.QueryAsync<Livro> vai pegar nossa conexão, ir na tabela Livro e rodar a variavel sql que recebeu o valor de strig e essa string é um comando sql que mostras todos os registros da tabela Livro
            }
        }

        public async Task<Livro> GetLivroById(int livroId)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "select * from Livros where id = @Id";  //Essa string na variavél sql permique que o usuario encontre um livro pelo Id // O @Id Repressenta um parametro por nome Id e esse paramêtro é chamado na linha de baixo

                return await con.QueryFirstOrDefaultAsync<Livro>(sql, new { Id = livroId }); // A con.QueryFirstOrDefaultAsync Recebe tanto a nossa variavel sql quanto um objeto com paramêtros, então instaciamos um new {Id = livroId} Id recebe o livroId (que é o Id que o usúario vai digitar)
            }
        }
        public async Task<IEnumerable<Livro>> CreateLivro(Livro livro)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "insert into Livros (titulo, autor) values (@titulo, @autor)";  // Aqui a variavél sql recebe uma string que é um comando de criar um novo elemento dentro do banco e entre os parenteses ficam as colunas na qual esse comando tem que criar, no caso aqui são apenas duas (titulo, autor) e o values representa os valores que serão inseridos nas colulas que são: @titulo, @autor
                await con.ExecuteAsync(sql, livro);                                       // aqui ele executa o comando 2=que está armazenado na variavél sql, e também armazena nas colunas titulo e autor os valores digitados pelo usúario
                return await con.QueryAsync<Livro>("select * from livros");               // Aqui ele retorna a tabela inteira com o novo Livro Criado
            }
        }
        public async Task<IEnumerable<Livro>> UpdateLivro(Livro livro)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "update livros set titulo = @titulo, autor = @autor where id = @id";
                await con.ExecuteAsync(sql, livro);
                return await con.QueryAsync<Livro>("select * from livros");
            }
        }


        public async Task<IEnumerable<Livro>> DeleteLivro(int livroId)
        {
            using (var con = new SqlConnection(getConnection))
            {
                var sql = "delete from livros where id = @Id";
                await con.ExecuteAsync(sql, new {Id = livroId});

                return await con.QueryAsync<Livro>("select * from livros");
            }
        }



    }
}
