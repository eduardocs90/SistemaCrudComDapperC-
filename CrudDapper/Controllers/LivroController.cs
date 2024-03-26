using CrudDapper.Models;
using CrudDapper.Services.LivroService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CrudDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {

        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetAllLivros()
        {
            IEnumerable < Livro> livros = await _livroInterface.GetAllLivros();// criamos uma variavel: IEnumerable < Livro> livros, do tipo <Livro> que tem o nome de livro e que recebe= await _livroInterface.GetAllLivros(); que vai la na ILivroInterface e acessa o metodo GetAllLivros

            if (!livros.Any()) //Aqui se não houver registros no banco retorna uma mensagem
            {
                return NotFound("Nenhum registro no localizado!");
            }

            return Ok(livros); //Aqui retorna a Função com a Lista de livros 
        }


        [HttpGet("{livroId}")]
        public async Task<ActionResult<Livro>> GetLivroById(int livroId) //Aqui Não retona uma lista e sim um unico Livro, portanto não usamos o IEnumerable
        {
            Livro livro = await _livroInterface.GetLivroById(livroId); // Aqui estanciamos Livro chamos ele de livro e ele recebe um await _livroInterface.GetLivroById(livroId); que vai na ILivroInterface, no metodo GetLivroById e recebe como parametro livroId que representa o id que o usúario vai digitar 

            if (livro == null) //se for nulo(não existir esse id)
            {
                return NotFound("Registro não localizado");
            }
            return Ok(livro);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Livro>>> CreateLivro(Livro livro)
        {
            IEnumerable<Livro> livros = await _livroInterface.CreateLivro(livro);

            return Ok(livros);
        }
        [HttpPut]
        public async Task<ActionResult<IEnumerable<Livro>>> UpdateLivro(Livro livro)
        {
            Livro registro = await _livroInterface.GetLivroById(livro.Id);//Na função UpdateLivro primeiro ela vai no banco de dados verificar se aquele id digitado pelo usuario existe
            if(registro == null)
            {
                return NotFound("Registro não localizado");
            }
            IEnumerable<Livro> livros = await _livroInterface.UpdateLivro(livro);// Se existir o id ele tras uma lista de livros e vai na ILivroInterface acessa o metodo UpdateLivro 
            return Ok(livros); // Aqui retorna o Livro editado
        }
        [HttpDelete("{livroId}")]
        public async Task<ActionResult<IEnumerable<Livro>>> DeleteLivro(int livroId)
        {
            Livro registro = await _livroInterface.GetLivroById(livroId);//Na função UpdateLivro primeiro ela vai no banco de dados verificar se aquele id digitado pelo usuario existe
            if (registro == null)
            {
                return NotFound("Registro não localizado");
            }
            IEnumerable<Livro> livros = await _livroInterface.DeleteLivro(livroId);
            return Ok(livros);

        }

    }

}
