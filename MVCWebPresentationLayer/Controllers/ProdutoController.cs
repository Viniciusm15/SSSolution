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
    //BudleConfigs - CSS.
    //Layout - Contém os CSS utilizados.
    //Todos os scripts devem estar no final da página.

    public class ProdutoController : Controller
    {
        public async Task<ActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(ProdutoViewModel viewModel)
        {
            ProdutoService svc = new ProdutoService();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProdutoViewModel, ProdutoDTO>();
            });
            IMapper mapper = configuration.CreateMapper();

            //Transforma o ProdutoViewModel em um ProdutoDTO.
            ProdutoDTO dto = mapper.Map<ProdutoDTO>(viewModel);

            try
            {
                await svc.Insert(dto);

                //Se funcionou, para a página inicial.
                return RedirectToAction("Index", "Produto");
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

        public async Task<ActionResult> Index()
        {
            try
            {
                ProdutoService svc = new ProdutoService();
                List<ProdutoDTO> produtos = await svc.GetData();

                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProdutoDTO, ProdutoQueryViewModel>();
                });
                IMapper mapper = configuration.CreateMapper();

                //Transforma o ClienteDTO em um ClienteViewModel. (Lista de clientes)
                //Este objeto "dados" é uma lista de objetos ViewModel.
                List<ProdutoQueryViewModel> dados = mapper.Map<List<ProdutoQueryViewModel>>(produtos);

                return View(dados);
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}