using AutoMapper;
using BLL.Impl;
using Common;
using DTO;
using MVCWebPresentationLayer.Mock;
using MVCWebPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCWebPresentationLayer.Controllers
{
    /*HTML: define a estrutura da página.
     *CSS: define os estilos para rendenização das tags HTML. 
     *Javascript: define comportamento. (lógica de programação)
     *Bootstrap: conjunto de entradas .css e funções javascript, que permitem que o seu site seja responsivo.
     *JQuery: framework javascript de quality of life.
     *TypeScript: superset javascript. (adiciona herança, polimorfismo, interface, tipagem)
     */
    public class ClienteController : Controller
    {

        public async Task<ActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(ClienteViewModel viewModel)
        {
            ClienteService svc = new ClienteService();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteViewModel, ClienteDTO>();
            });
            IMapper mapper = configuration.CreateMapper();

            //Transforma o ClienteViewModel em um ClienteDTO.
            ClienteDTO dto = mapper.Map<ClienteDTO>(viewModel);

            try
            {
                await svc.Insert(dto);

                //Se funcionou, para a página inicial.
                return RedirectToAction("Index", "Home");
            }
            catch (NecoException ex)
            {
                //Se caiu aqui, o ClienteService lançou a exceção por validação de campos.
                ViewBag.ValidationErrors = ex.Erros;
            }
            catch (Exception ex)
            {
                //Se caiu aqui, o ClienteService lançou a exceção genérica, provavelmente por falha de acesso ao banco.
                ViewBag.ErrorMessage = ex.Message;
            }

            //Se chegou aqui temos erros.
            return View();
        }

        //meusite.com/Cliente
        //meusite.com/Cliente/Index
        public async Task<ActionResult> Index()
        {
            try
            {
                ClienteService svc = new ClienteService();
                List<ClienteDTO> clientes = await svc.GetData();

                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ClienteDTO, ClienteQueryViewModel>();
                });
                IMapper mapper = configuration.CreateMapper();

                //Transforma o ClienteDTO em um ClienteViewModel. (Lista de clientes)
                //Este objeto "dados" é uma lista de objetos ViewModel.
                List<ClienteQueryViewModel> dados = mapper.Map<List<ClienteQueryViewModel>>(clientes);

                return View(dados);
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}