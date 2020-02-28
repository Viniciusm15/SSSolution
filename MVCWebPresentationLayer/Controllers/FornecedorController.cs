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
    public class FornecedorController : Controller
    {

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(FornecedorViewModel viewModel)
        {
            FornecedorService svc = new FornecedorService();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FornecedorViewModel, FornecedorDTO>();
            });
            IMapper mapper = configuration.CreateMapper();

            //Transforma o ClienteViewModel em um ClienteDTO.
            FornecedorDTO dto = mapper.Map<FornecedorDTO>(viewModel);

            try
            {
                svc.Insert(dto);

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
                FornecedorService svc = new FornecedorService();
                List<FornecedorDTO> fornecedores = svc.GetFornecedores().Result;

                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FornecedorDTO, FornecedorQueryViewModel>();
                });
                IMapper mapper = configuration.CreateMapper();

                //Transforma o ClienteDTO em um ClienteViewModel. (Lista de clientes)
                //Este objeto "dados" é uma lista de objetos ViewModel.
                List<FornecedorQueryViewModel> dados = mapper.Map<List<FornecedorQueryViewModel>>(fornecedores);
                
                return View(dados);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}