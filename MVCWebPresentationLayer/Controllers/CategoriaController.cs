using AutoMapper;
using BLL.Impl;
using Common;
using DTO;
using MVCWebPresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCWebPresentationLayer.Controllers
{
    public class CategoriaController : Controller
    {
        public async Task<ActionResult> Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Cadastrar(CategoriaViewModel viewModel)
        {
            CategoriaService svc = new CategoriaService();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoriaViewModel, CategoriaDTO>();
            });
            IMapper mapper = configuration.CreateMapper();

            //Transforma o ClienteViewModel em um ClienteDTO.
            CategoriaDTO dto = mapper.Map<CategoriaDTO>(viewModel);

            try
            {
                await svc.Insert(dto);

                //Se funcionou, para a página inicial.
                return RedirectToAction("Index", "Categoria");
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
                CategoriaService svc = new CategoriaService();
                List<CategoriaDTO> categorias = await svc.GetData();

                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CategoriaDTO, CategoriaQueryViewModel>();
                });
                IMapper mapper = configuration.CreateMapper();

                //Transforma o ClienteDTO em um ClienteViewModel. (Lista de clientes)
                //Este objeto "dados" é uma lista de objetos ViewModel.
                List<CategoriaQueryViewModel> dados = mapper.Map<List<CategoriaQueryViewModel>>(categorias);

                return View(dados);
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}