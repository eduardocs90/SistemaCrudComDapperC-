using CrudDapper.Models;
using System.Threading.Tasks;

namespace CrudDapper.Services.LivroService
{
    public interface ILivroInterface
    {
        Task<IEnumerable<Livro>> GetAllLivros(); //Esse IEnumerable Representa uma lista (Então esse metodo vai retornar uma lista de Livro
        Task<Livro> GetLivroById(int LivroId); //Repare que aqui não usamos o IEnumerable porquenão retornaremo uma lista, retornaremos apenas um livro e por isso passamos como parametro o int id, porque vamos buscar esse livro através do id 
        Task<IEnumerable<Livro>> CreateLivro(Livro livro);//Aqui vai retornar uma lista(IEnumerable) e ele recebe como parametro um Livro livro porque vai inserir um novo livro no banco de dados e retorar uma lista com esse novo livro incluso 
        Task<IEnumerable<Livro>> UpdateLivro(Livro livro);//Aqui vai retornar uma lista de livros e recebe Livro livro como parametro porque vamos pegar o obejeto Livro ediata-lo inseri-lo no banco e retornar esse livro editado na lista 
        Task<IEnumerable<Livro>> DeleteLivro(int livroId);//Aqui vamos retornar uma lista de livro sem o Livro deletado, recebe como paramêtro Livro livroID porque vamos deletar o Livro pelo livroId 

    }
}
