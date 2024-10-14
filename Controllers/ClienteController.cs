using Fiap.Web.alunosII.Data;
using Fiap.Web.alunosII.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Web.alunosII.Controllers
{
    public class ClienteController : Controller
    { 


        private readonly DatabaseContext _databaseContext;


        public ClienteController(DatabaseContext databaseContext)
        { 
            _databaseContext = databaseContext;
            

        }

        public IActionResult Index() 
        {
            var _clientes = _databaseContext.Clientes.Include( c => c.Representante).ToList();  

           // Console.WriteLine(_clientes.Count);
            return View(_clientes);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var selectListRepresentantes =
                new SelectList( _databaseContext.Representantes.ToList(),
                                nameof(RepresentanteModel.RepresentanteId),
                                nameof(RepresentanteModel.NomeRepresentante));

            //Adiciona o SelectList a ViewBag para se enviado para a View
            //A propriedade Representantes é criada de forma dinâmica na ViewBag
            ViewBag.Representantes = selectListRepresentantes;

            return View(new ClienteModel());
        }
        [HttpPost]
        public IActionResult Create(ClienteModel clienteModel)
        {
            _databaseContext.Clientes.Add(clienteModel);
            _databaseContext.SaveChanges();

            Console.WriteLine("Cliente cadastrado com sucesso.!!!");
            TempData["mensagemSucesso"] = "Cliente cadastrado com sucesso.!!!" ;
            
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
             var selectListRepresentantes =
                 new SelectList( _databaseContext.Representantes.ToList(), 
                                                nameof(RepresentanteModel.RepresentanteId),
                                                 nameof(RepresentanteModel.NomeRepresentante));
           
            ViewBag.Representantes = selectListRepresentantes;

            var clienteConsultado = _databaseContext.Clientes.Find(id);
             return View(clienteConsultado);

        }

        [HttpGet]
        public IActionResult Delete(int id) 
        {
            var clienteConsultado = _databaseContext.Clientes.Find(id);
            var nome= clienteConsultado.Nome;
            var sobreN = clienteConsultado.Sobrenome;
            var clienteApagado =  nome + " " + sobreN;
            
            if (clienteConsultado != null)
            {
                _databaseContext.Clientes.Remove(clienteConsultado);
                _databaseContext.SaveChanges();
                Console.WriteLine("Os dados do cliente:");
                TempData["mensagemSucesso"] = $"Os dados do cliente: " + clienteApagado + " foram apagados com sucesso.!!";
            } else
            {
                TempData["mensagemSucesso"] = $"OPS !!! Cliente inexistente.";
            }            
            
            
            return RedirectToAction(nameof(Index)); 
        }


        [HttpPost]
        public IActionResult Edit(ClienteModel clienteModel)
        { 
            _databaseContext.Clientes.Update(clienteModel);
            _databaseContext.SaveChanges();

            TempData["mensagemSucesso"] = $"Os dados do cliente {clienteModel.Nome} {clienteModel.Sobrenome} foram alterados com sucesso";
            return RedirectToAction(nameof(Index));

        }
       /* public static List<ClienteModel> GerarClientesMocados() 
        {
            var clientes = new List<ClienteModel>();

            for (int i = 1; i < 5; i++)
            {
                var cliente = new ClienteModel
                {
                    ClienteId = i,
                    Nome = "Cliente" + i,
                    Sobrenome = "Sobrenome" + i,
                    Email = "Cliente" + i + "@example",
                    DataNascimento = DateTime.Now.AddYears(-30),
                    Observacao = "Observacao do Cliente " + i,
                    RepresentanteId = i,
                    Representante = new RepresentanteModel 
                    { 
                        RepresentanteId = i,
                        NomeRepresentante = "Representante" + i,
                        Cpf = "00000000191"
                    }
                };

                clientes.Add(cliente);
            }
            return clientes;
        }*/



    }
}
